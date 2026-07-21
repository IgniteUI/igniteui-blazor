using System;

namespace IgniteUI.Blazor.Controls
{

    internal interface RefSink {
        void OnRefChanged(String refName, Object refValue);
        void OnRefNotifyInsertItem(IJSDataSource dataSource, String refName, int index, Object refItem);
        void OnRefNotifyRemoveItem(IJSDataSource dataSource, String refName, int index, Object oldItem);
        void OnRefNotifyClearItems(IJSDataSource dataSource, String refName, Object refValue);
        void OnRefNotifySetItem(IJSDataSource dataSource, String refName, int index, Object oldItem, Object newItem);
        void OnRefNotifyUpdateItem(IJSDataSource dataSource, String refName, int index, Object refItem, bool syncDataOnly);
    }
}