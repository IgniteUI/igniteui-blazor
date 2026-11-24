using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text.Json;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbCombo<TValue, TItem>
    {
        // The combo exposes RenderFragment templates (ItemTemplate / GroupHeaderTemplate)
        // that are hosted client-side through an <igc-lite-template-content> element. That
        // hosting routes back into the Blazor renderer via AdjustDynamicContent, which
        // requires the DynamicContentHolder to be present. Like the grid and charts, the
        // combo therefore needs dynamic content enabled so the holder is created.
        private protected override bool NeedsDynamicContent
        {
            get
            {
                return true;
            }
        }

        // A new Data keeps the selection (igniteui-webcomponents 7.5), as in remote filtering, where each
        // search replaces the data, and the web component also keeps selected items that are removed
        // from the data. When an item leaves the data source, .NET forgets its uuid, so a selected or
        // pending item that the client sends back would resolve to null. Keep every item that leaves the
        // data, then ask the client which of them it still holds (its selection and the pending selection
        // of the confirm mode) and drop the rest.
        private readonly Dictionary<Guid, TItem> _retainedItems = new();
        private readonly Dictionary<object, Guid> _retainedIds = new(ReferenceEqualityComparer.Instance);
        private bool _pruneScheduled;

        private void EnsureItemReleaseTracking()
        {
            var manager = DataSourceManager;
            if (manager != null && manager.ItemReleased == null)
            {
                manager.ItemReleased = OnItemReleased;
            }
        }

        private void OnItemReleased(Guid id, object item)
        {
            // Primitive items cross by value, not as uuid refs, so they need no mapping.
            if (item is not TItem typed || Type.GetTypeCode(item.GetType()) != TypeCode.Object)
            {
                return;
            }
            if (_retainedIds.TryGetValue(item, out var oldId) && oldId != id)
            {
                _retainedItems.Remove(oldId);
            }
            _retainedItems[id] = typed;
            _retainedIds[item] = id;
            SchedulePruneRetainedItems();
        }

        private void SchedulePruneRetainedItems()
        {
            if (_pruneScheduled)
            {
                return;
            }
            _pruneScheduled = true;
            _ = PruneRetainedItemsAsync();
        }

        private async Task PruneRetainedItemsAsync()
        {
            try
            {
                // Let the change that released the items reach the client first.
                await Task.Yield();
                _pruneScheduled = false;
                if (!IsReady)
                {
                    // Before the client component exists, it holds nothing yet; the next release tries again.
                    return;
                }
                var candidates = _retainedItems.Keys.ToList();
                var held = ReturnToObjectArray<string>(await InvokeMethod("___heldItemIds", Array.Empty<object?>(), Array.Empty<string>()));
                var heldIds = new HashSet<string>(held.Where(id => id != null), StringComparer.OrdinalIgnoreCase);
                // Only the items retained before the query: the ones released since then wait for the next one.
                foreach (var id in candidates.Where(id => !heldIds.Contains(id.ToString())))
                {
                    if (_retainedItems.Remove(id, out var item) && item is not null)
                    {
                        _retainedIds.Remove(item);
                    }
                }
            }
            catch (Exception)
            {
                // Best effort, in the background: the component is gone or the circuit is down. Keep the
                // items; the next release tries again.
                _pruneScheduled = false;
            }
        }

        private protected override object? FindRetainedItem(Guid id)
        {
            return _retainedItems.TryGetValue(id, out var item) ? item : null;
        }

        private protected override Guid FindRetainedItemId(object item)
        {
            return _retainedIds.TryGetValue(item, out var id) ? id : Guid.Empty;
        }

        // The web component calls the item template with a plain { item } context and the group
        // header template with { key, item }, which arrive here as property bags whose entries
        // are still unresolved.
        private protected override object? ConvertTemplateContext(string? templateId, object? context)
        {
            if (context is not Dictionary<string, object?> args)
            {
                return base.ConvertTemplateContext(templateId, context);
            }

            if (templateId != null && templateId == _groupHeaderTemplateTemplateId)
            {
                // The web component gives the group value as key; "Other" stands for the items
                // without a value in the GroupKey field. Its deprecated synthetic item is ignored.
                return new IgbComboGroupHeaderContext
                {
                    Key = args.TryGetValue("key", out var keyValue) ? ToKey(ConvertReturnValue(keyValue)) : null,
                };
            }

            if (templateId != null && templateId == _itemTemplateTemplateId && args.TryGetValue("item", out var item))
            {
                // A data item crosses as a uuid ref; resolve it against the combo's data source.
                // Primitive items cross by value instead, so coerce those to TItem. Anything that is
                // still no TItem becomes null, which the template content never renders.
                return ToItem(ConvertReturnValue(item)) is TItem typed ? typed : null;
            }

            return base.ConvertTemplateContext(templateId, context);
        }

        // SelectAllItems, DeselectAllItems and CommitSelection change the web component's value without
        // emitting igcChange, so the Change handler never copies it back. Read it after the call instead,
        // so Value and @bind-Value follow, as they do for a user's selection.
        private async Task SyncValueFromClientAsync()
        {
            ApplyValueFromClient(await GetCurrentValueAsync());
        }

        private void SyncValueFromClient()
        {
            ApplyValueFromClient(GetCurrentValue());
        }

        private void ApplyValueFromClient(TValue[]? value)
        {
            value ??= Array.Empty<TValue>();
            if ((_value ?? Array.Empty<TValue>()).SequenceEqual(value))
            {
                return;
            }

            _value = value;
            OnPropertyPropagatedOut(RendererName, "Value");
            if (!EventCallback<TValue[]>.Empty.Equals(ValueChanged))
            {
                ObserveHandlerTask(ValueChanged.InvokeAsync(value));
            }
        }

        private static object? ToKey(object? key)
        {
            if (key is JsonElement { ValueKind: JsonValueKind.True or JsonValueKind.False } boolKey)
            {
                return boolKey.GetBoolean();
            }
            if (key is JsonElement { ValueKind: JsonValueKind.Null })
            {
                return null;
            }
            return key;
        }

        private static object? ToItem(object? value)
        {
            if (value == null || value is TItem)
            {
                return value;
            }
            if (value is JsonElement { ValueKind: JsonValueKind.True or JsonValueKind.False } boolValue)
            {
                value = boolValue.GetBoolean();
            }
            var target = Nullable.GetUnderlyingType(typeof(TItem)) ?? typeof(TItem);
            if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(target) && !target.IsEnum)
            {
                try
                {
                    return Convert.ChangeType(value, target, CultureInfo.InvariantCulture);
                }
                catch (Exception e) when (e is InvalidCastException or FormatException or OverflowException)
                {
                    return null;
                }
            }
            return value;
        }
    }
}
