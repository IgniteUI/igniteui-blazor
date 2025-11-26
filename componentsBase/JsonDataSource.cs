using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;

namespace IgniteUI.Blazor.Controls
{
    internal interface IJSDataSourceItem
    {
        Guid Id { get; }
    }

    internal interface IJSDataSource
    {
        string GetDataIntentsAsJson();
        bool SuppressModifications { get; set; }
        JSDataSourceType DataSourceType { get; }
        bool IsSent { get; set; }
        bool HasId(Guid id);
        bool HasId(string id);
        //IJSDataSourceItem LookupById(Guid id);
        object LookupOriginal(Guid id);
        object LookupOriginal(string id);
        bool HasOriginal(object item);
        Guid IdFromOriginal(object item);
        IJSDataSourceItem NotifyInsertItem(object data, int index, Object item);
        IJSDataSourceItem NotifyRemoveItem(object data, int index, object oldItem);
        void NotifyClearItems(Object data);
        IJSDataSourceItem NotifySetItem(Object data, int index, Object oldItem, Object newItem) ;
        IJSDataSourceItem NotifyUpdateItem(object data, int index, object item);
        void InsertItemWithId(string id, int index, Object item);
    }

    internal enum JSDataSourceType
    {
        Json,
        Unmarshalled
    }

    internal class JsonDataSource
        : IJSDataSource 
        {
            public bool SuppressModifications { get; set; }
            public JSDataSourceType DataSourceType
            {
                get
                {
                    return JSDataSourceType.Json;
                }
            }

            public bool IsSent { get; set; }
        private object _originalData;

        public string GetDataIntentsAsJson()
        {
            if (_schema != null)
            {
                return _schema.GetDataIntentsAsJson();
            }
            return null;
        }

        private List<IJSDataSourceItem> _data = new List<IJSDataSourceItem>();
        private Dictionary<Guid, IJSDataSourceItem> _uuidToItem = new Dictionary<Guid, IJSDataSourceItem>();
        private Dictionary<IJSDataSourceItem, object> _itemToOriginal = new Dictionary<IJSDataSourceItem, object>();
        private Dictionary<object, IJSDataSourceItem> _originalToItem = new Dictionary<object, IJSDataSourceItem>();
        private JSDataSourceSchema _parentSchema = null;
        private DataSourceManager _manager = null;
        private string _parentId = null;
        private bool _dateCacheReady = false;

        private Dictionary<Guid, Dictionary<string, JsonDataSource>> _subDataSources = new Dictionary<Guid, Dictionary<string, JsonDataSource>>();

        public bool DateCacheReady { get { return _dateCacheReady; } }

        public static IJSDataSource CreateWithSchema(Object data, JSDataSourceSchema schema, DataSourceManager manager, string parentId) 
        {
            if (data == null) {
                return null;
            }
            
            if (data.GetType().IsArray) {
                return JsonDataSource.CreateFromArray((Object[])data, schema, manager, parentId);
            } else if (data is IList) {
                return JsonDataSource.CreateFromIList((IList)data, schema, manager, parentId);
            } else if (data is IEnumerable) {
                return JsonDataSource.CreateFromIEnumerable((IEnumerable)data, schema, manager, parentId);
            }
            return null;
        }

        private void Listen(object data)
        {
            if (data is INotifyCollectionChanged)
            {
                _originalData = data;
                var i = (INotifyCollectionChanged)data;
                i.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SuppressModifications)
            {
                return;
            }
                
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
					if (e.NewItems != null)
					{
						for (var i = 0; i < e.NewItems.Count; i++)
						{
							var item = e.NewItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
                            _manager.NotifyInsertItem(refName , e.NewStartingIndex + i, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Remove:
                {
					if (e.OldItems != null)
					{
						for (var i = 0; i < e.OldItems.Count; i++)
						{
							var item = e.OldItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyRemoveItem(refName, e.OldStartingIndex, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Replace:
                {
					if (e.OldItems != null)
					{
						for (var i = 0; i < e.OldItems.Count; i++)
						{
							var item = e.OldItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyRemoveItem(refName, e.OldStartingIndex, item);
						}
					}
					if (e.NewItems != null)
					{
						for (var i = 0; i < e.NewItems.Count; i++)
						{
							var item = e.NewItems[i];
                            var refName = _manager.GetRefId(_originalData);
                            if (refName == null) {
                                return;
                            }
							_manager.NotifyInsertItem(refName , e.NewStartingIndex + i, item);
						}
					}
					break;
                }
				case NotifyCollectionChangedAction.Reset:
                {
                    var refName = _manager.GetRefId(_originalData);
                    if (refName == null) {
                        return;
                    }
					_manager.NotifyClearItems(refName);
					break;
                }
            }
        }

        public bool HasId(Guid id)
        {
            return (_uuidToItem.ContainsKey(id));
        }
        public bool HasId(string id)
        {
            if (id.Contains("/"))
            {
                var parentIds = id.Split('/');
                var guid = Guid.Parse(parentIds[0]);
                if (_subDataSources.ContainsKey(guid))
                {
                    foreach (var data in _subDataSources[guid].Values)
                    {
                        if (data.HasId(id.Replace(parentIds[0] + "/", "")))
                            return true;
                    }
                }
                return false;
            }
            else
            {
                Guid uuid = Guid.Parse(id);
                return HasId(uuid);
            }
        }

        public IJSDataSourceItem LookupById(Guid id) 
        {
            if (_uuidToItem.ContainsKey(id)) {
                return _uuidToItem[id];
            }
            return null;
        }

        public object LookupOriginal(Guid id)
        {
            return ToOriginal(LookupById(id));
        }
        public object LookupOriginal(string id)
        {
            if (id.Contains("/"))
            {
                var parentIds = id.Split('/');
                var guid = Guid.Parse(parentIds[0]);
                if (_subDataSources.ContainsKey(guid))
                {
                    foreach (var data in _subDataSources[guid].Values)
                    {
                        var original = data.LookupOriginal(id.Replace(parentIds[0] + "/", ""));
                        if (original != null)
                            return original;
                    }
                }
                return null;
            }
            else
            {
                Guid uuid = Guid.Parse(id);
                return LookupOriginal(uuid);
            }
        }

        public bool HasOriginal(object item) 
        {
            return _originalToItem.ContainsKey(item);
        }

        public Guid IdFromOriginal(object item)
        {
            var itm = FromOriginal(item);
            if (item == null)
            {
                return Guid.Empty;
            }
            return itm.Id;
        }

        public IJSDataSourceItem FromOriginal(object item) {
            if (_originalToItem.ContainsKey(item)) {
                return _originalToItem[item];
            }

            return null;
        }

        public Object ToOriginal(IJSDataSourceItem item) {
            if (item == null)
            {
                return null;
            }

            if (_itemToOriginal.ContainsKey(item)) {
                return _itemToOriginal[item];
            }

            return null;
        }

        public static IJSDataSource Create(Object data, DataSourceManager manager) {
            if (data == null) {
                return null;
            }

            if (data.GetType().IsArray) {
                return JsonDataSource.CreateFromArray((Object[])data, null, manager, null);
            } else if (data is IList) {
                return JsonDataSource.CreateFromIList((IList)data, null, manager, null);
            } else if (data is IEnumerable) {
                return JsonDataSource.CreateFromIEnumerable((IEnumerable)data, null, manager, null);
            }
            return null;
        }

        private static IJSDataSource CreateFromIEnumerable(IEnumerable data, JSDataSourceSchema schema, DataSourceManager manager, string parentId) 
        {
            JsonDataSource newData = new JsonDataSource();
            newData._manager = manager;
            newData._parentSchema = schema;
            newData._parentId = parentId;
            foreach (var item in data) {
                newData.Add(item);
            }
            newData.Listen(data);
            return newData;
        }

        private static IJSDataSource CreateFromIList(IList data, JSDataSourceSchema schema, DataSourceManager manager, string parentId) 
        {
            //Console.WriteLine("test json");
            //DateTime testTime = DateTime.Now;
            //var ser = System.Text.Json.JsonSerializer.Serialize(data);
            
            //Console.WriteLine("end test json:" + (DateTime.Now - testTime).TotalMilliseconds);
            //Console.WriteLine("begin create from list");
            DateTime startTime = DateTime.Now;
            JsonDataSource newData = new JsonDataSource();
            newData._manager = manager;
            newData._parentSchema = schema;
            newData._parentId = parentId;
            var count = data.Count;
            for (int i = 0; i < count; i++) {
                newData.Add(data[i]);
            }
            newData.Listen(data);
            //Console.WriteLine("end create from list: " + (DateTime.Now - startTime).TotalMilliseconds);
            return newData;
        }

        private static IJSDataSource CreateFromArray(object[] data, JSDataSourceSchema schema, DataSourceManager manager, string parentId) 
        {
            JsonDataSource newData = new JsonDataSource();
            newData._parentSchema = schema;
            newData._manager = manager;
            newData._parentId = parentId;
            for (int i = 0; i < data.Length; i++) {
                newData.Add(data[i]);
            }
            newData.Listen(data);
            return newData;
        }

        private JSDataSourceSchema _schema = null;

        private void Add(object item) {
            if (_schema == null)
            {
                EnsureSchema(item);
            }
            var schema = _schema;

            if (item == null)
            {
                schema = null;
            }

            JsonDataSourceItem itemJson = JsonDataSourceItem.Create(item, schema, _manager, _parentId);
            OnAddItem(itemJson, item);
            _data.Add(itemJson);

            if (schema != null)
            {
                for (int i = 0; i < schema.PropertyNames.Length; i++)
                {
                    var propertyName = schema.PropertyNames[i];
                    var propertyType = schema.PropertyTypes[i];
                    if (propertyType == JSDataSourceSchemaType.ObjectValue)
                    {
                        var subSchema = schema.GetSubSchema(propertyName);
                        if (subSchema != null)
                        {
                            var propValue = (JsonDataSourceItem)itemJson.GetValue(propertyName);
                            if (propValue.Source != null)
                            {
                                if (!_subDataSources.ContainsKey(itemJson.Id))
                                {
                                    _subDataSources[itemJson.Id] = new Dictionary<string, JsonDataSource>();
                                }
                                _subDataSources[itemJson.Id].Add(propertyName, (JsonDataSource)propValue.Source);
                            }
                        }
                    }
                }
            }
        }

        private void OnAddItem(IJSDataSourceItem itemJson, Object item) {
            _uuidToItem[itemJson.Id] = itemJson;
            if (item != null) {
                _originalToItem[item] = itemJson;
                _itemToOriginal[itemJson] = item;
            }
        }

        private void EnsureSchema(object item) 
        {          
            if (item != null && _schema == null) {
                //Console.WriteLine("begin esnure schema");
                DateTime startTime = DateTime.Now;

                if (_parentSchema != null) {
                    if (_parentSchema.ItemSchema != null) {
                        _schema = _parentSchema.ItemSchema;
                    }
                }
                _schema = JsonDataSourceItem.ExtractSchema(item);
                if (_parentSchema != null && _parentSchema.ItemSchema == null) {
                    _parentSchema.ItemSchema = _schema;
                }
                //Console.WriteLine("end ensure schema: " + (DateTime.Now - startTime).TotalMilliseconds);
            }
        }

        public IJSDataSourceItem NotifyInsertItem(object data, int index, Object item) {
            EnsureSchema(item);
            IJSDataSourceItem itemJson = JsonDataSourceItem.Create(item, _schema, _manager);
            OnAddItem(itemJson, item);
            _data.Insert(index, itemJson);
            return itemJson;
        }

        public IJSDataSourceItem NotifyRemoveItem(object data, int index, object oldItem) {
            EnsureSchema(oldItem);
            IJSDataSourceItem itemJson = _data[index];
            OnRemove(itemJson, oldItem);
            _data.RemoveAt(index);
            return itemJson;
        }

        private void OnRemove(IJSDataSourceItem itemJson, object item) 
        {
            if (_uuidToItem.ContainsKey(itemJson.Id)) {
                _uuidToItem.Remove(itemJson.Id);
            }
            if (_itemToOriginal.ContainsKey(itemJson)) {
                Object original = _itemToOriginal[itemJson];
                if (_originalToItem.ContainsKey(original)) {
                    _originalToItem.Remove(original);
                }
                _itemToOriginal.Remove(itemJson);
            }
            if (item != null &&
                _originalToItem.ContainsKey(item)) {
                _itemToOriginal.Remove((IJSDataSourceItem)item);
            }
        }

        public void NotifyClearItems(Object data) {
            for (int i = 0; i < _data.Count; i++) {
                IJSDataSourceItem item = _data[i];
                OnRemove(item, null);
            }
            _data.Clear();
            _schema = null;
            if (data.GetType().IsArray) {
                object[] dataArr = (object[])data;
                for (int i = 0; i < dataArr.Length; i++) {
                    Add(dataArr[i]);
                }
            } else if (data is IList) {
                IList dataList = (IList)data;
                for (int i = 0; i < dataList.Count; i++) {
                    Add(dataList[i]);
                }
            } else if (data is IEnumerable) {
                IEnumerable dataIter = (IEnumerable)data;
                foreach (object item in dataIter) {
                    Add(item);
                }
            }
        }

        public IJSDataSourceItem NotifySetItem(Object data, int index, Object oldItem, Object newItem) 
        {
            EnsureSchema(newItem);
            IJSDataSourceItem itemJson = JsonDataSourceItem.Create(newItem, _schema, _manager);
            IJSDataSourceItem old = _data[index];
            OnRemove(old, oldItem);
            OnAddItem(itemJson, newItem);
            _data[index] = itemJson;
            return itemJson;
        }
        public IJSDataSourceItem this[int i]
        {
            get { return _data[i]; }
          
        }
        
        public IJSDataSourceItem NotifyUpdateItem(object data, int index, object item) 
        {
            EnsureSchema(item);
            JsonDataSourceItem itemJson = null;
            if (HasOriginal(item))
            {
                itemJson = (JsonDataSourceItem)_originalToItem[item];
                itemJson.Refresh(item, _schema, _manager);
            }
            else
            {
                itemJson = JsonDataSourceItem.Create(item, _schema, _manager);
            }
            _data[index] = itemJson;
            return itemJson;
        }

        public string GetDateCacheAsJson()
        {
            String ret = "null";
            if (_data != null && _data.Count > 0)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Text.Json.Utf8JsonWriter uw = new System.Text.Json.Utf8JsonWriter(ms))
                    {
                        GetDateCacheAsJson(uw);

                        uw.Flush();
                        ret = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    }
                }

                _dateCacheReady = true;
            }
            return ret;
        }

        private void GetDateCacheAsJson(System.Text.Json.Utf8JsonWriter writer)
        {
            if (_data != null && _data.Count > 0)
            {
                writer.WriteStartArray();

                var item = (JsonDataSourceItem)_data[0];
                if (item.Source != null)
                {
                    for (int i = 0; i < _data.Count; i++)
                    {
                        item = (JsonDataSourceItem)_data[0];
                        JsonDataSource ds = (JsonDataSource)item.Source;
                        ds.GetDateCacheAsJson(writer);
                    }
                }
                else
                {
                    item.GetDateCacheAsJson(writer);
                }
                writer.WriteEndArray();
            }
        }

        public string ToJson()
        {
            //Console.WriteLine("begin to json");
            DateTime startTime = DateTime.Now;
            String ret;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                using (System.Text.Json.Utf8JsonWriter uw = new System.Text.Json.Utf8JsonWriter(ms))
                {
                //RendererSerializer ser = new RendererSerializer(uw);
                
                ToJson(uw);
                uw.Flush();
                ret = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            //Console.WriteLine("end to json: " + (DateTime.Now - startTime).TotalMilliseconds);
            return ret;
        }

        public void ToJson(System.Text.Json.Utf8JsonWriter writer, System.Text.Json.JsonEncodedText? propertyName = null) {
            //Console.WriteLine("begin to json");
            DateTime startTime = DateTime.Now;
            //String[] items = new String[_data.Count];

            if (propertyName != null)
            {
                writer.WriteStartArray(propertyName.Value);
            }
            else
            {
                writer.WriteStartArray();
            }
            for (int i = 0; i < _data.Count; i++) {
                JsonDataSourceItem item = (JsonDataSourceItem)_data[i];
                //String ser = "null";
                if (!item.IsNull) {
                    item.ToJson(writer);
                    //ser = item.ToJson();
                } else {
                    writer.WriteNullValue();
                }
                //items[i] = ser;
            }
            writer.WriteEndArray();

            //var content = String.Join(", ", items);
            //Console.WriteLine("end to json: " + (DateTime.Now - startTime).TotalMilliseconds);
            //return "[" + content + "]";
        }

        public void InsertItemWithId(string id, int index, Object item)
        {
            EnsureSchema(item);
            IJSDataSourceItem itemJson = JsonDataSourceItem.CreateWithId(item, Guid.Parse(id), _schema, _manager);
            OnAddItem(itemJson, item);
            _data.Insert(index, itemJson);
        }
    }
}