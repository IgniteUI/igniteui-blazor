namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Reports changes made to a bound data collection, so the component updates without the collection being reassigned.
    /// A collection implementing <see cref="System.Collections.Specialized.INotifyCollectionChanged"/> reports insertions, removals and replacements on its own;
    /// use these methods for other collections, and for changes to an item's properties, which no collection reports.
    /// </summary>
    public interface IDataSourceNotifications
    {
        /// <summary>Reports that <paramref name="refItem"/> was inserted into <paramref name="dataSource"/> at <paramref name="index"/>.</summary>
        void NotifyInsertItem(object dataSource, int index, object refItem);

        /// <summary>Reports that <paramref name="oldItem"/> was removed from <paramref name="dataSource"/> at <paramref name="index"/>.</summary>
        void NotifyRemoveItem(object dataSource, int index, object oldItem);

        /// <summary>Reports that <paramref name="dataSource"/> was emptied.</summary>
        void NotifyClearItems(object dataSource);

        /// <summary>Reports that the item at <paramref name="index"/> in <paramref name="dataSource"/> was replaced by <paramref name="newItem"/>.</summary>
        void NotifySetItem(object dataSource, int index, object oldItem, object newItem);

        /// <summary>
        /// Reports that properties of <paramref name="refItem"/>, at <paramref name="index"/> in <paramref name="dataSource"/>, changed.
        /// With <paramref name="syncDataOnly"/> only the component's copy of the item is refreshed and the component does not re-render.
        /// </summary>
        void NotifyUpdateItem(object dataSource, int index, object refItem, bool syncDataOnly = false);

        /// <summary>Stops reporting changes to <paramref name="dataSource"/> until <see cref="ResumeNotifications"/>, so many changes can be applied as one update.</summary>
        void SuspendNotifications(object dataSource);

        /// <summary>Resumes reporting changes to <paramref name="dataSource"/>. With <paramref name="notify"/> the component reloads the collection's current contents.</summary>
        void ResumeNotifications(object dataSource, bool notify = true);
    }
}
