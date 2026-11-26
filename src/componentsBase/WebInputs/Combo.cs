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
        //
        // The same item can come back in the new data under a new uuid, as when a remote search returns
        // instances of an in-memory list again. The web component compares items by reference, so the
        // client then holds two objects for it: the selected one under the old uuid, and the one in the
        // list under the new uuid. An item the client is confirmed to hold therefore goes to the client
        // under its old uuid, so Deselect reaches the object in the selection.
        //
        // Releases come from whatever thread changes the bound collection, and the prune continues on the
        // thread pool without a sync context, so every access to this state goes through _retainedLock.
        private readonly object _retainedLock = new();
        // Every retained uuid; an item released more than once has one per release.
        private readonly Dictionary<Guid, TItem> _retainedItems = new();
        // The uuid of the latest release of each retained item.
        private readonly Dictionary<object, Guid> _retainedIds = new(ReferenceEqualityComparer.Instance);
        // The uuid under which the client held each retained item at the last prune.
        private readonly Dictionary<object, Guid> _heldIds = new(ReferenceEqualityComparer.Instance);
        // Goes up whenever _heldIds is set from a newer view of the client than a running prune has.
        private int _heldVersion;
        private int _pruneScheduled;

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
            // An earlier uuid of the item stays: the client may still hold the item under it.
            lock (_retainedLock)
            {
                _retainedItems[id] = typed;
                _retainedIds[item] = id;
            }
            SchedulePruneRetainedItems();
        }

        // After a Change, the client holds exactly the items of the new value: in confirm mode the commit
        // copies the pending selection. Drop the held uuids of the other items now. The deferred prune
        // comes too late for a Select that the Change handler makes, which would otherwise send a
        // deselected item under a uuid the client no longer resolves.
        private void OnClientSelectionChanged(TValue[]? newValue)
        {
            // With a ValueKey, the value holds keys, which tell nothing about the items.
            if (newValue != null && string.IsNullOrEmpty(ValueKey))
            {
                lock (_retainedLock)
                {
                    if (_heldIds.Count > 0)
                    {
                        var kept = new HashSet<object>(newValue.OfType<object>(), ReferenceEqualityComparer.Instance);
                        foreach (var item in _heldIds.Keys.Where(item => !kept.Contains(item)).ToList())
                        {
                            _heldIds.Remove(item);
                        }
                        _heldVersion++;
                    }
                }
            }
            OnClientSelectionChanged();
        }

        // A selection call changes the items that the client holds. Read them right after it, not in
        // the deferred prune, so that the next call sends each item under the uuid the client holds.
        private void RefreshHeldIds()
        {
            if (IsReady && HasRetainedItems())
            {
                ApplyHeldItemIds(ReturnToObjectArray<string>(InvokeMethodSync("___heldItemIds", Array.Empty<object?>(), Array.Empty<string>())));
            }
        }

        private async Task RefreshHeldIdsAsync()
        {
            if (IsReady && HasRetainedItems())
            {
                ApplyHeldItemIds(ReturnToObjectArray<string>(await InvokeMethod("___heldItemIds", Array.Empty<object?>(), Array.Empty<string>())));
            }
        }

        private bool HasRetainedItems()
        {
            lock (_retainedLock)
            {
                return _retainedItems.Count > 0;
            }
        }

        private void ApplyHeldItemIds(string[] held)
        {
            var heldIds = new HashSet<string>(held.Where(id => id != null), StringComparer.OrdinalIgnoreCase);
            lock (_retainedLock)
            {
                _heldIds.Clear();
                foreach (var (id, item) in _retainedItems)
                {
                    if (item is not null && heldIds.Contains(id.ToString()))
                    {
                        _heldIds[item] = id;
                    }
                }
                _heldVersion++;
            }
        }

        // The client's selection changed, so the retained items it holds may have too.
        private void OnClientSelectionChanged()
        {
            bool any;
            lock (_retainedLock)
            {
                any = _retainedItems.Count > 0;
            }
            if (any)
            {
                SchedulePruneRetainedItems();
            }
        }

        private void SchedulePruneRetainedItems()
        {
            if (Interlocked.Exchange(ref _pruneScheduled, 1) == 1)
            {
                return;
            }
            _ = PruneRetainedItemsAsync();
        }

        private async Task PruneRetainedItemsAsync()
        {
            try
            {
                // Let the change that released the items reach the client first.
                await Task.Yield();
                Interlocked.Exchange(ref _pruneScheduled, 0);
                if (!IsReady)
                {
                    // Before the client component exists, it holds nothing yet; the next release tries again.
                    return;
                }
                List<Guid> candidates;
                int version;
                lock (_retainedLock)
                {
                    candidates = _retainedItems.Keys.ToList();
                    version = _heldVersion;
                }
                var held = ReturnToObjectArray<string>(await InvokeMethod("___heldItemIds", Array.Empty<object?>(), Array.Empty<string>()));
                var heldIds = new HashSet<string>(held.Where(id => id != null), StringComparer.OrdinalIgnoreCase);
                // Only the items retained before the query: the ones released since then wait for the next one.
                lock (_retainedLock)
                {
                    if (version != _heldVersion)
                    {
                        // The selection changed during the query, so its answer is stale. Every change
                        // of the version schedules a prune of its own, which decides instead.
                        return;
                    }
                    foreach (var id in candidates)
                    {
                        if (!_retainedItems.TryGetValue(id, out var item) || item is null)
                        {
                            continue;
                        }
                        if (heldIds.Contains(id.ToString()))
                        {
                            _heldIds[item] = id;
                            continue;
                        }
                        _retainedItems.Remove(id);
                        if (_retainedIds.TryGetValue(item, out var latestId) && latestId == id)
                        {
                            _retainedIds.Remove(item);
                        }
                        if (_heldIds.TryGetValue(item, out var heldId) && heldId == id)
                        {
                            _heldIds.Remove(item);
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // Best effort, in the background: the component is gone or the circuit is down. Keep the
                // items; the next release tries again.
                Interlocked.Exchange(ref _pruneScheduled, 0);
            }
            catch (ObjectDisposedException)
            {
                // Best effort, in the background: the component is gone or the circuit is down. Keep the
                // items; the next release tries again.
                Interlocked.Exchange(ref _pruneScheduled, 0);
            }
            catch (InvalidOperationException)
            {
                // Best effort, in the background: the component is gone or the circuit is down. Keep the
                // items; the next release tries again.
                Interlocked.Exchange(ref _pruneScheduled, 0);
            }
        }

        private protected override object? FindRetainedItem(Guid id)
        {
            lock (_retainedLock)
            {
                return _retainedItems.TryGetValue(id, out var item) ? item : null;
            }
        }

        private protected override Guid FindClientItemId(object item)
        {
            lock (_retainedLock)
            {
                // The object the client holds for an item that is also in the current data is the old one.
                if (_heldIds.TryGetValue(item, out var heldId))
                {
                    return heldId;
                }
            }
            var id = base.FindClientItemId(item);
            if (id != Guid.Empty)
            {
                return id;
            }
            lock (_retainedLock)
            {
                return _retainedIds.TryGetValue(item, out var retainedId) ? retainedId : Guid.Empty;
            }
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
            var value = await GetCurrentValueAsync();
            await RefreshHeldIdsAsync();
            ApplyValueFromClient(value);
        }

        private void SyncValueFromClient()
        {
            var value = GetCurrentValue();
            RefreshHeldIds();
            ApplyValueFromClient(value);
        }

        // CancelSelection drops the pending selection, and the items only it held, without changing the value.
        private async Task SyncHeldIdsFromClientAsync()
        {
            await RefreshHeldIdsAsync();
            OnClientSelectionChanged();
        }

        private void SyncHeldIdsFromClient()
        {
            RefreshHeldIds();
            OnClientSelectionChanged();
        }

        private void ApplyValueFromClient(TValue[]? value)
        {
            OnClientSelectionChanged();
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
