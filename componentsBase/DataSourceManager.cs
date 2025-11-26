using System;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls 
{
    internal class DataSourceManager {
        public DataSourceManager(RefSink sink, RuntimeHelper helper) {
            // ChunkAmount = -1;
            // ChunkSlicingWait = -1;
            _helper = helper;
            _refSink = sink;
        }

        private RuntimeHelper _helper;

        // public int ChunkAmount { get; set; }
        // public int ChunkSlicingWait { get; set; }

        private RefSink _refSink;
        private Dictionary<string, object> _refs = new Dictionary<string, object>();
        private Dictionary<string, object> _refsById = new Dictionary<string, object>();
        private Dictionary<string, IJSDataSource> _dataSources = new Dictionary<string, IJSDataSource>();
        private Dictionary<object, string> _idLookup = new Dictionary<object, string>();
        private Dictionary<string, bool> _suspensionLookup = new Dictionary<string, bool>();

        public object FindItem(Guid id) 
        {
            foreach (var data in
                _dataSources.Values) {
                if (data.HasId(id)) {
                    return data.LookupOriginal(id);
                }
            }
            return null;
        }
        public object FindItem(string id)
        {
            foreach (var data in _dataSources.Values) {
                if (data.HasId(id)) {
                    return data.LookupOriginal(id);
                }
            }
            return null;
        }

        // public IJSDataSourceItem FindJsonItem(object item) 
        // {
        //     foreach (var data in
        //             _dataSources.Values) {
        //         if (data.HasOriginal(item)) {
        //             return data.FromOriginal(item);
        //         }
        //     }
        //     return null;
        // }

        public Guid FindItemId(object item)
        {
            foreach (var data in
                _dataSources.Values) {
                if (data != null && data.HasOriginal(item)) {
                    return data.IdFromOriginal(item);
                }
            }
            return Guid.Empty;
        }

        public string OnRefChanged(string path, object data) 
        {
            string id = null;
            if (_refs.ContainsKey(path)) {
                object obj = _refs[path];
                string oldId = GetRefId(obj);
                id = oldId;

                if (data == null ||
                obj != data) {
                    DecrementRef(oldId);

                    if (!_refCount.ContainsKey(oldId)) {
                        _refs.Remove(path);
                    }
                }
            }

            if (data != null) {
                string newId = GetRefId(data);
                id = newId;

                _refs[path] = data;
                _refsById[id] = data;
                IncrementRef(id);
                if (!_dataSources.ContainsKey(id)) {
                    if (_helper.IsInproc && !_helper.IsForcedJsonDataMarshalling)
                    {
                        //Console.WriteLine("unmarshalled datasource");
                        _dataSources[id] = UnmarshalledDataSource.Create(data, this, _helper);
                    }
                    else
                    {
                        //Console.WriteLine("json datasource");
                        _dataSources[id] = JsonDataSource.Create(data, this);
                    }
                }
                _idLookup[data] = id;
                _refSink.OnRefChanged(id, _dataSources[id]);
            }


            if (data == null)
            {
                id = null;
            }
            return id;
        }

        private Dictionary<string, int> _refCount = new Dictionary<string, int>();

        private void IncrementRef(string id) {
            int refCount = 0;
            if (_refCount.ContainsKey(id)) {
                refCount = _refCount[id];
            }
            refCount++;
            _refCount[id] = refCount;
        }

        void DecrementRef(string id) {
            int refCount = 0;
            if (_refCount.ContainsKey(id)) {
                refCount = _refCount[id];
            }
            refCount--;
            if (refCount > 0) {
                _refCount[id] = refCount;
            }
            else {
                _refCount.Remove(id);
                if (_dataSources.ContainsKey(id)) {
                    Object data = _dataSources[id];
                    if (data != null && _idLookup.ContainsKey(data)) {
                        _idLookup.Remove(data);
                    } else if (_refsById.ContainsKey(id) && _idLookup.ContainsKey(_refsById[id])) {
                        _idLookup.Remove(_refsById[id]);
                    }
                    _dataSources.Remove(id);
                    _refsById.Remove(id);
                    _refSink.OnRefChanged(id, null);
                }
            }
        }

        public void NotifyInsertItem(string refName, int index, object refItem)
        {
            if (_suspensionLookup.ContainsKey(refName) && _suspensionLookup[refName]) {
                return;
            }

            //Console.WriteLine("notifying insert item");
            if (_refsById.ContainsKey(refName)) {
                //Console.WriteLine("found by id");
                object data = _refsById[refName];
                IJSDataSource dataSource = _dataSources[refName];
                IJSDataSourceItem newItem = dataSource.NotifyInsertItem(data, index, refItem);
                _refSink.OnRefNotifyInsertItem(dataSource, refName, index, newItem);
            }
        }
        public void NotifyRemoveItem(String refName, int index, Object oldItem) {
            if (_suspensionLookup.ContainsKey(refName) && _suspensionLookup[refName]) {
                return;
            }

            if (_refsById.ContainsKey(refName)) {
                Object data = _refsById[refName];
                IJSDataSource dataSource = _dataSources[refName];
                IJSDataSourceItem oldItemJson = dataSource.NotifyRemoveItem(data, index, oldItem);
                _refSink.OnRefNotifyRemoveItem(dataSource, refName, index, oldItemJson);
            }
        }
        public void NotifyClearItems(string refName) {
            if (_suspensionLookup.ContainsKey(refName) && _suspensionLookup[refName]) {
                return;
            }

            if (_refsById.ContainsKey(refName)) {
                Object data = _refsById[refName];
                IJSDataSource dataSource = _dataSources[refName];
                dataSource.NotifyClearItems(data);
                _refSink.OnRefNotifyClearItems(dataSource, refName, dataSource);
            }
        }
        public void NotifySetItem(string refName, int index, object oldItem, object newItem)
        {
            if (_suspensionLookup.ContainsKey(refName) && _suspensionLookup[refName]) {
                return;
            }
            if (_refsById.ContainsKey(refName)) {
                object data = _refsById[refName];
                IJSDataSource dataSource = _dataSources[refName];
                IJSDataSourceItem oldItemJson = dataSource.DataSourceType == JSDataSourceType.Json ? ((JsonDataSource)dataSource)[index] : null;
                IJSDataSourceItem newItemJson = dataSource.NotifySetItem(data, index, oldItem, newItem);
                _refSink.OnRefNotifySetItem(dataSource, refName, index, oldItemJson, newItemJson);
            }
        }
        public void NotifyUpdateItem(string refName, int index, object refItem, bool syncDataOnly)
        {
            if (_suspensionLookup.ContainsKey(refName) && _suspensionLookup[refName]) {
                return;
            }
            if (_refsById.ContainsKey(refName)) {
                object data = _refsById[refName];
                IJSDataSource dataSource = _dataSources[refName];
                IJSDataSourceItem newItemJson = dataSource.NotifyUpdateItem(data, index, refItem);
                _refSink.OnRefNotifyUpdateItem(dataSource, refName, index, newItemJson, syncDataOnly);
            }
        }

        public bool HasRefId(object dataSource) {
            if (_idLookup.ContainsKey(dataSource)) {
                return true;
            }
            return false;
        }

        public string GetRefId(object dataSource) {
            if (_idLookup.ContainsKey(dataSource)) {
                return _idLookup[dataSource];
            }
            Guid id = Guid.NewGuid();
            String strId = id.ToString();
            _idLookup[dataSource] = strId;

            return strId;
        }

        public void SuspendNotifications(object dataSource)
        {
            if (!HasRefId(dataSource)) {
                return;
            }
            var refName = GetRefId(dataSource);
            _suspensionLookup[refName] = true;
        }

        public void ResumeNotifications(object dataSource, bool notify = true)
        {
            if (!HasRefId(dataSource)) {
                return;
            }
            var refName = GetRefId(dataSource);
            if (_suspensionLookup.ContainsKey(refName)) {
                _suspensionLookup[refName] = false;
                if (notify)
                    NotifyClearItems(refName);
            }
        }

        public IJSDataSource GetDataSource(string id)
        {
            if (_dataSources.ContainsKey(id))
            {
                return _dataSources[id];
            }
            return null;
        }

        public void Log()
        {
            Console.WriteLine("");
            Console.WriteLine("============= ID Lookup");
            foreach (var item in _idLookup)
            {
                Console.WriteLine($"({item.Key.ToString()}:\"{item.Value}\")");
            }

            Console.WriteLine("============= JSON DataSource IDs");

            foreach (var item in _dataSources)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine("============= Refs");

            foreach (var item in _refs)
            {
                Console.WriteLine($"({item.Key}:{item.Value.ToString()})");
            }

            Console.WriteLine("");
        }
    }
}