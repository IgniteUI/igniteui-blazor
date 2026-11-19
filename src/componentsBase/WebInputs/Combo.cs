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
        // search replaces the data. Replacing Data releases the old data source and its uuid -> item map,
        // so the selected items of the old data would come back from the client as null. Keep the map
        // entries of the items that are still in Value; the entries go once their items leave Value.
        private readonly Dictionary<Guid, TItem> _retainedItems = new();
        private string? _valueKeyMemberName;
        private Func<TItem, object?>? _valueKeyReader;

        private void RetainSelectedItems(IEnumerable<TItem>? oldData)
        {
            var manager = DataSourceManager;
            var selected = new HashSet<object>(_value.Where(v => v is not null).Cast<object>());

            foreach (var entry in _retainedItems.Where(e => !IsSelected(selected, e.Value)).ToList())
            {
                _retainedItems.Remove(entry.Key);
            }

            if (oldData == null || manager == null || selected.Count == 0)
            {
                return;
            }

            foreach (var item in oldData)
            {
                if (item is not null && IsSelected(selected, item))
                {
                    var id = manager.FindItemId(item);
                    if (id != Guid.Empty)
                    {
                        _retainedItems[id] = item;
                    }
                }
            }
        }

        private bool IsSelected(HashSet<object> selected, TItem item)
        {
            var value = ValueOf(item);
            return value is not null && selected.Contains(value);
        }

        // The value an item selects: its ValueKey member, or the item itself without a ValueKey.
        private object? ValueOf(TItem item)
        {
            if (string.IsNullOrEmpty(ValueKey))
            {
                return item;
            }

            if (_valueKeyMemberName != ValueKey)
            {
                _valueKeyMemberName = ValueKey;
                _valueKeyReader = CreateMemberReader(ValueKey);
            }
            return _valueKeyReader?.Invoke(item);
        }

        private static Func<TItem, object?>? CreateMemberReader(string name)
        {
            // TItem carries DynamicallyAccessedMembers.All, so its members survive trimming.
            const System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
            var property = typeof(TItem).GetProperty(name, flags)
                ?? typeof(TItem).GetProperty(name, flags | System.Reflection.BindingFlags.IgnoreCase);
            if (property != null && property.GetIndexParameters().Length == 0)
            {
                return item => property.GetValue(item);
            }
            var field = typeof(TItem).GetField(name, flags)
                ?? typeof(TItem).GetField(name, flags | System.Reflection.BindingFlags.IgnoreCase);
            if (field != null)
            {
                return item => field.GetValue(item);
            }
            return null;
        }

        private protected override object? FindRetainedItem(Guid id)
        {
            return _retainedItems.TryGetValue(id, out var item) ? item : null;
        }

        private protected override Guid FindRetainedItemId(object item)
        {
            foreach (var entry in _retainedItems)
            {
                if (ReferenceEquals(entry.Value, item))
                {
                    return entry.Key;
                }
            }
            return Guid.Empty;
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
            if (_value.SequenceEqual(value))
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
