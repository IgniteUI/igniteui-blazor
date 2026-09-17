namespace IgniteUI.Blazor.Controls
{
    public partial class IgbCombo<T> : IDataSourceNotifications
    {
        /// <inheritdoc />
        public void NotifyInsertItem(object dataSource, int index, object item)
        {
            if (TryGetDataSourceRef(dataSource, out var manager, out var refName))
            {
                manager.NotifyInsertItem(refName, index, item);
            }
        }

        /// <inheritdoc />
        public void NotifyRemoveItem(object dataSource, int index, object oldItem)
        {
            if (TryGetDataSourceRef(dataSource, out var manager, out var refName))
            {
                manager.NotifyRemoveItem(refName, index, oldItem);
            }
        }

        /// <inheritdoc />
        public void NotifyClearItems(object dataSource)
        {
            if (TryGetDataSourceRef(dataSource, out var manager, out var refName))
            {
                manager.NotifyClearItems(refName);
            }
        }

        /// <inheritdoc />
        public void NotifySetItem(object dataSource, int index, object oldItem, object newItem)
        {
            if (TryGetDataSourceRef(dataSource, out var manager, out var refName))
            {
                manager.NotifySetItem(refName, index, oldItem, newItem);
            }
        }

        /// <inheritdoc />
        public void NotifyUpdateItem(object dataSource, int index, object item, bool syncDataOnly = false)
        {
            if (TryGetDataSourceRef(dataSource, out var manager, out var refName))
            {
                manager.NotifyUpdateItem(refName, index, item, syncDataOnly);
            }
        }

        /// <inheritdoc />
        public void SuspendNotifications(object dataSource)
        {
            DataSourceManager?.SuspendNotifications(dataSource);
        }

        /// <inheritdoc />
        public void ResumeNotifications(object dataSource, bool notify = true)
        {
            DataSourceManager?.ResumeNotifications(dataSource, notify);
        }

        private bool TryGetDataSourceRef(object dataSource, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out DataSourceManager? manager, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? refName)
        {
            manager = DataSourceManager;
            if (manager == null || !manager.HasRefId(dataSource))
            {
                refName = null;
                return false;
            }
            refName = manager.GetRefId(dataSource);
            return true;
        }
    }
}
