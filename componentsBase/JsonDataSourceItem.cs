using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Globalization;

namespace IgniteUI.Blazor.Controls
{

    internal class JsonDataSourceItem
        : IJSDataSourceItem 
        {
        private Guid _id;
        private bool _isNull = false;
        private bool _isDataSource = true;
        private IJSDataSource _source = null;
        private string _parentId = null;
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        private Dictionary<string, JSDataSourceSchemaType> _valueTypes = new Dictionary<string, JSDataSourceSchemaType>();

        public bool IsNull 
        {
            get
            {
                return _isNull;
            }
        }

        public JsonDataSourceItem() {
            _id = Guid.NewGuid();
        }
        public JsonDataSourceItem(Guid id) {
            _id = id;
        }

        public Guid Id 
        {
            get
            {
                return _id;
            }
        }

        public IJSDataSource Source
        {
            get { return _source; }
        }

        public string ParentId
        {
            get
            {
                return _parentId;
            }
        }

        public object GetValue(string key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return null;
        }

        public static JSDataSourceSchema ExtractSchema(object item) {
            if (item == null) {
                return  null;
            }

            Type c = item.GetType();
            if (c.IsArray) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                s.Commit();
                return s;
            }
            else if (item is IList) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                s.Commit();
                return s;
            }
            else if (item is Dictionary<string, object>) {
                return JSDataSourceSchema.CreateFromDictionary((IDictionary)item);
            }
            else if (item is IEnumerable && !(item is string)) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsDataSource = true;
                s.Commit();
                return s;
            }

            if (c.IsPrimitive || c == typeof(string)) {
                JSDataSourceSchema s = new JSDataSourceSchema();
                s.IsPrimitive = true;
                s.PrimitiveType = s.ResolveSchemaType(c);
                s.Commit();
                return s;
            }
            

            return JSDataSourceSchema.Create(c);
        }

        public static JsonDataSourceItem Create(object item, JSDataSourceSchema schema, DataSourceManager manager) {
            JsonDataSourceItem newItem = new JsonDataSourceItem();
            newItem.Read(item, schema, manager);
            return newItem;
        }
        public static JsonDataSourceItem Create(object item, JSDataSourceSchema schema, DataSourceManager manager, JsonDataSourceItem parentItem) {
            JsonDataSourceItem newItem = new JsonDataSourceItem();
            newItem._parentId = parentItem.ParentId != null ? parentItem.ParentId + "/" + parentItem.Id.ToString() : parentItem.Id.ToString();
            newItem.Read(item, schema, manager);
            return newItem;
        }
        public static JsonDataSourceItem Create(object item, JSDataSourceSchema schema, DataSourceManager manager, string parentId) {
            JsonDataSourceItem newItem = new JsonDataSourceItem();
            newItem._parentId = parentId;
            newItem.Read(item, schema, manager);
            return newItem;
        }

        public static JsonDataSourceItem CreateWithId(object item, Guid id, JSDataSourceSchema schema, DataSourceManager manager) {
            JsonDataSourceItem newItem = new JsonDataSourceItem(id);
            newItem.Read(item, schema, manager);
            return newItem;
        }

        public void Refresh(object item, JSDataSourceSchema schema, DataSourceManager manager)
        {
            Read(item, schema, manager);
        }

        private JSDataSourceSchema _schema = null;
        private void Read(Object item, JSDataSourceSchema schema, DataSourceManager manager) {
            if (schema == null) {
                _isNull = true;
                return;
            }
            _schema = schema;
            if (_schema.IsDataSource) {
                //Console.WriteLine("in read");
                IJSDataSource source = JsonDataSource.CreateWithSchema(item, _schema, manager, _parentId);
                _source = source;
                return;
            }
            if (schema.IsPrimitive) {
                _values["value"] = item;
                _valueTypes["value"] = schema.PrimitiveType;
            }
            for (int i = 0; i < schema.PropertyNames.Length; i++) {
                String name = schema.PropertyNames[i];
                Func<object, object> propGetter = schema.PropertyGetters[i];
                JSDataSourceSchemaType type = schema.PropertyTypes[i];
                Object val = schema.ResolveValue(name, item, propGetter, this, type, manager);

                _values[name] = val;
                _valueTypes[name] = type;
            }
            for (int i = 0; i < schema.Fields.Length; i++) {
                String name = schema.Fields[i].Name;
                Func<object, object> fieldGetter = schema.FieldGetters[i];
                JSDataSourceSchemaType type = schema.FieldTypes[i];
                Object val = schema.ResolveFieldValue(name, item, fieldGetter, this, type, manager);

                _values[name] = val;
                _valueTypes[name] = type;
            }
        }

        public string ToJson() 
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                using (System.Text.Json.Utf8JsonWriter uw = new System.Text.Json.Utf8JsonWriter(ms))
                {
                //RendererSerializer ser = new RendererSerializer(uw);
                
                ToJson(uw);
                uw.Flush();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        
        }

        public void GetDateCacheAsJson(System.Text.Json.Utf8JsonWriter writer, string parentKey = null)
        {
            if (_isNull)
            {
                return;
            }

            if (_schema.IsDataSource)
            {
                var itemSchema = _schema.GetSubSchema("Items");
                GetDateCacheAsJson(itemSchema, writer, parentKey + "[]");
            }
            else
            {
                GetDateCacheAsJson(_schema, writer, parentKey);
            }
        }
        public void GetDateCacheAsJson(JSDataSourceSchema schema, System.Text.Json.Utf8JsonWriter writer, string parentKey = null)
        {
            if (schema == null)
            {
                return;
            }

            parentKey = parentKey != null ? parentKey + "." : "";
            for (int i = 0; i < schema.PropertyTypes.Length; i++)
            {
                if (schema.PropertyTypes[i] == JSDataSourceSchemaType.DateTimeValue ||
                    schema.PropertyTypes[i] == JSDataSourceSchemaType.NullableDateTimeValue)
                {
                    writer.WriteStringValue(parentKey + schema.PropertyNames[i]);
                }
                if (schema.PropertyTypes[i] == JSDataSourceSchemaType.ObjectValue)
                {
                    var subSchema = schema.GetSubSchema(schema.PropertyNames[i]);
                    if (subSchema != null)
                    {
                        if (subSchema.IsDataSource)
                        {
                            GetDateCacheAsJson(subSchema.GetSubSchema("Items"), writer, parentKey + schema.PropertyNames[i] + "[]");
                        }
                        else
                        {
                            GetDateCacheAsJson(subSchema, writer, parentKey + schema.PropertyNames[i]);
                            //((JsonDataSourceItem)_values[schema.PropertyNames[i]]).GetDateCacheAsJson(writer, parentKey + schema.PropertyNames[i]);
                        }
                    }
                }
            }
        }

        public void ToJson(System.Text.Json.Utf8JsonWriter writer) 
        {
            if (_isNull) {
                writer.WriteNullValue();
                return;
            }
            if (_source != null) {
                ((JsonDataSource)_source).ToJson(writer);
                return;
            }
            if (_schema.IsPrimitive) {
                ValueToJson("value", new System.Text.Json.JsonEncodedText(), writer);
                return; 
            }

            writer.WriteStartObject();
            
            var propertyNames = _schema.PropertyNames;
            var jsonPropertyNames = _schema.JsonPropertyNames;
            var len = propertyNames.Length;
            for (var i = 0; i < len; i++)
            {
                ValueToJson(propertyNames[i], jsonPropertyNames[i], writer);
            }
            var fieldNames = _schema.FieldNames;
            var jsonFieldNames = _schema.JsonFieldNames;
            len = fieldNames.Length;
            for (var i = 0; i < len; i++)
            {
                ValueToJson(fieldNames[i], jsonFieldNames[i], writer);
            }

            writer.WriteString("___id", _parentId != null ? _parentId + "/" + _id.ToString() : _id.ToString());

            writer.WriteEndObject();
        }

        public void ToJson(System.Text.Json.Utf8JsonWriter writer, System.Text.Json.JsonEncodedText propertyName) 
        {
            if (_isNull) {
                writer.WriteNull(propertyName);
                return;
            }
            if (_source != null) {
                ((JsonDataSource)_source).ToJson(writer, propertyName);
                return;
            }

            writer.WriteStartObject(propertyName);
            
            var propertyNames = _schema.PropertyNames;
            var jsonPropertyNames = _schema.JsonPropertyNames;
            var len = propertyNames.Length;
            for (var i = 0; i < len; i++)
            {
                ValueToJson(propertyNames[i], jsonPropertyNames[i], writer);
            }

            writer.WriteString("___id", _id);

            writer.WriteEndObject();
        }

        private void ValueToJson(String key, System.Text.Json.JsonEncodedText prop, System.Text.Json.Utf8JsonWriter writer) {
            Object value = _values[key];
            if (value == null)
            {
                if (prop.Equals(default))
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteNull(prop);
                }
                return;
            }

            JSDataSourceSchemaType type = _valueTypes[key];

            switch (type) {
                case JSDataSourceSchemaType.IntValue:
                case JSDataSourceSchemaType.NullableIntValue:
                    if (prop.Equals(default)) 
                    {
                        // in case we have no prop because the value is not an object but a primitive type
                        writer.WriteNumberValue((int)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteNumber(prop, (int)value);
                    break;
                case JSDataSourceSchemaType.ByteValue:
                case JSDataSourceSchemaType.NullableByteValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((byte)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteNumber(prop, (byte)value);
                    break;
                case JSDataSourceSchemaType.LongValue:
                case JSDataSourceSchemaType.NullableLongValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((long)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteNumber(prop, (long)value);
                    break;
                case JSDataSourceSchemaType.ShortValue:
                case JSDataSourceSchemaType.NullableShortValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((short)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteNumber(prop, (short)value);
                    break;
                case JSDataSourceSchemaType.SingleValue:
                case JSDataSourceSchemaType.NullableSingleValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((float)value);
                        break;
                    }
                    if (value == null || float.IsNaN((float)value))
                    {
                        writer.WriteNull(prop);
                        break;
                    }
                    writer.WriteNumber(prop, (float)value);
                    break;
                case JSDataSourceSchemaType.DecimalValue:
                case JSDataSourceSchemaType.NullableDecimalValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((decimal)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteNumber(prop, (decimal)value);
                    break;
                case JSDataSourceSchemaType.DoubleValue:
                case JSDataSourceSchemaType.NullableDoubleValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteNumberValue((double)value);
                        break;
                    }
                    if (value == null || double.IsNaN((double)value))
                    {
                        writer.WriteNull(prop);
                        break;
                    }
                    writer.WriteNumber(prop, (double)value);
                    break;
                case JSDataSourceSchemaType.BooleanValue:
                case JSDataSourceSchemaType.NullableBooleanValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteBooleanValue((bool)value);
                        break;
                    }
                    if (value == null)
                    {
                        writer.WriteNull(prop); break;
                    }
                    writer.WriteBoolean(prop, (bool)value);
                    break;
                case JSDataSourceSchemaType.StringValue:
                    if (prop.Equals(default)) 
                    {
                        writer.WriteStringValue((string)value);
                        break;
                    }
                    writer.WriteString(prop, (string)value);
                    break;
                case JSDataSourceSchemaType.CalendarValue:
                case JSDataSourceSchemaType.NullableCalendarValue:
                    if (_schema == null)
                    {
                        writer.WriteNull(prop);
                        break;
                    }
                    writer.WriteString(prop, _schema.RenderDate(value));
                    break;
                case JSDataSourceSchemaType.DateTimeValue:
                case JSDataSourceSchemaType.NullableDateTimeValue:
                     if (_schema == null)
                    {
                        writer.WriteNull(prop);
                        break;
                    }
                    writer.WriteString(prop, _schema.RenderDate(value));
                    break;
                case JSDataSourceSchemaType.ObjectValue:
                    if (value == null)
                    {
                        writer.WriteNull(prop);
                        break;
                    }
                    ((JsonDataSourceItem) value).ToJson(writer, prop);
                    break;
                default:
                    writer.WriteNull(key);
                    break;
            }
        }


        public String ToIdJson() {
            return "{ \"refType\": \"uuid\", \"id\": \"" + _id.ToString() + "\" }";
        }
    }

}