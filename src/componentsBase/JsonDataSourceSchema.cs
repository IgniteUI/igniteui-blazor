using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{

    internal class JSDataSourceSchema {
        public bool IsDataSource = false;
        public bool IsDictionary = false;

        private Dictionary<string, bool> _checkedArray = new Dictionary<string, bool>();

        public Action NotifyModified { get; set; }

        private bool HasDataIntents()
        {
            if (IsDataSource)
            {
                //Console.WriteLine("checking has intents");
                if (ItemSchema != null)
                {
                    //Console.WriteLine("has item schema");
                    return ItemSchema.HasDataIntents();
                }
                if (_subSchemas.ContainsKey("___self"))
                {
                    //Console.WriteLine("checking self intents");
                    if (_subSchemas["___self"] != null)
                    {
                        //Console.WriteLine("has item schema");
                        return _subSchemas["___self"].HasDataIntents();
                    }
                }
            }
            else
            {
                for (var i = 0; i < PropertyDataIntents.Length; i++)
                {
                    var prop = PropertyNames[i];
                    if (_subSchemas.ContainsKey(prop))
                    {
                        var sub = _subSchemas[prop];
                        if (sub != null)
                        {
                            if (sub.HasDataIntents())
                            {
                                return true;
                            }
                        }
                    }

                    if (PropertyDataIntents[i] != null)
                    {
                        return true;
                    }
                }
                for (var i = 0; i < FieldDataIntents.Length; i++)
                {
                    var field = FieldNames[i];
                    if (_subSchemas.ContainsKey(field))
                    {
                        var sub = _subSchemas[field];
                        if (sub != null)
                        {
                            if (sub.HasDataIntents())
                            {
                                return true;
                            }
                        }
                    }

                    if (FieldDataIntents[i] != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void WriteDataIntentsAsJson(string propertyName, System.Text.Json.Utf8JsonWriter uw)
        {
            if (propertyName != null)
            {
                uw.WriteStartObject(propertyName);
            }
            else
            {
                uw.WriteStartObject();
            }

        //Console.WriteLine("emitting json");
            if (IsDataSource)
            {
                //Console.WriteLine("is data source");
                if (ItemSchema != null)
                {

                    //Console.WriteLine("has item schema");
                    uw.WriteBoolean("subProps", true);
                    ItemSchema.WriteDataIntentsAsJson("subIntents", uw);
                }
                if (_subSchemas.ContainsKey("___self"))
                {
                    //Console.WriteLine("checking self intents");
                    if (_subSchemas["___self"] != null)
                    {
                        //Console.WriteLine("has item schema");
                        uw.WriteBoolean("subProps", true);
                        _subSchemas["___self"].WriteDataIntentsAsJson("subIntents", uw);
                    }
                }
            }
            else
            {
                for (var i = 0; i < PropertyNames.Length; i++)
                {
                    var currProp = PropertyNames[i];
                    var intents = PropertyDataIntents[i];
                
                    if (_subSchemas.ContainsKey(currProp))
                    {
                        if (_subSchemas[currProp].HasDataIntents())
                        {
                            var sub = _subSchemas[currProp];
                            if (sub.IsDataSource)
                            {
                                uw.WriteStartObject(currProp);
                                uw.WriteBoolean("subProps", true);
                                sub.WriteDataIntentsAsJson("subIntents", uw);
                                uw.WriteEndObject();
                            }
                        }
                    }
                    else
                    {
                        if (intents != null)
                        {
                            //uw.WriteStartObject();
                            uw.WriteStartArray(currProp);
                            for (var j = 0; j < intents.Length; j++)
                            {
                                var currIntent = intents[j];
                                var val = currIntent.Intent;
                                uw.WriteStringValue(val);
                            }
                            uw.WriteEndArray();
                            //uw.WriteEndObject();
                        }
                    }
                }


                for (var i = 0; i < Fields.Length; i++)
                {
                    var currProp = FieldNames[i];
                    var intents = FieldDataIntents[i];
                
                    if (_subSchemas.ContainsKey(currProp))
                    {
                        if (_subSchemas[currProp].HasDataIntents())
                        {
                            var sub = _subSchemas[currProp];
                            if (sub.IsDataSource)
                            {
                                uw.WriteStartObject(currProp);
                                uw.WriteBoolean("subProps", true);
                                sub.WriteDataIntentsAsJson("subIntents", uw);
                                uw.WriteEndObject();
                            }
                        }
                    }
                    else
                    {
                        if (intents != null)
                        {
                            //uw.WriteStartObject();
                            uw.WriteStartArray(currProp);
                            for (var j = 0; j < intents.Length; j++)
                            {
                                var currIntent = intents[j];
                                var val = currIntent.Intent;
                                uw.WriteStringValue(val);
                            }
                            uw.WriteEndArray();
                        // uw.WriteEndObject();
                        }
                    }
                }
            }

            uw.WriteEndObject();
        }

        public string GetDataIntentsAsJson()
        {
            if (!HasDataIntents())
            {
                return null;
            }
            //Console.WriteLine("writing data intents");
            String ret;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Text.Json.Utf8JsonWriter uw = new System.Text.Json.Utf8JsonWriter(ms))
                {
                    WriteDataIntentsAsJson(null, uw);

                    uw.Flush();
                    ret = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return ret;
        }

        private static void GetPropertiesFromType(Type type, List<PropertyInfo> properties)
        {
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                GetPropertiesFromType(type.BaseType, properties);
            }

            PropertyInfo[] props = type.GetRuntimeProperties().ToArray();
            for (int i = 0; i < props.Length; i++)
            {
                if (!properties.Where(p => p.Name == props[i].Name).Any())
                {
                    properties.Add(props[i]);
                }
            }
        }

        private static void GetFieldsFromType(Type type, List<FieldInfo> fields)
        {
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                GetFieldsFromType(type.BaseType, fields);
            }

            FieldInfo[] flds = type.GetRuntimeFields().ToArray();
            for (int i = 0; i < flds.Length; i++)
            {
                if (!fields.Where(f => f.Name == flds[i].Name).Any())
                {
                    fields.Add(flds[i]);
                }
            }
        }

        public static JSDataSourceSchema CreateFromDictionary(IDictionary item) {
            JSDataSourceSchema s = new JSDataSourceSchema();
            s.IsDictionary = true;
            List<string> names = new List<string>();
            List<Type> types = new List<Type>();
            foreach (var key in item.Keys)
            {
                if (key is string)
                {
                    if (item[key] == null)
                    {
                        continue;
                    }

                    List<IDataIntentAttribute> dataIntents = null;
                    names.Add((string)key);
                    s._buildingPropertiesDataIntents.Add(null);

                    
                    Type ret = item[key].GetType();
                    types.Add(ret);

                    JSDataSourceSchemaType type = s.ResolveSchemaType(ret);
                    //if (type == JsonDataSourceSchemaType.OBJECT_VALUE) {
                    //    _subSchemas.put(method.getName().substring(3, 4).toLowerCase() + method.getName().substring(4), create(ret));
                    //}
                    s._buildingPropertiesTypes.Add(type);
                }
            }
            
            s.Properties = s._buildingProperties.ToArray();
            s.PropertyTypes = s._buildingPropertiesTypes.ToArray();
            s.PropertyDataIntents = s._buildingPropertiesDataIntents.ToArray();
            s.PropertyNames = names.ToArray();
            s.PropertyGetters = new Func<object, object>[names.Count];
            s.TypedPropertyGetters = new Delegate[names.Count];

            var itemProp = item.GetType().GetProperty("Item");
            
            for (int i = 0; i < names.Count; i++) {
                var key = names[i];
                s.PropertyGetters[i] = (o) => ((IDictionary)o)[key];
            }
             for (int i = 0; i < names.Count; i++) {
                var key = names[i];
                s.TypedPropertyGetters[i] = s.GetTypedDictionaryValueGetter(item.GetType(), types[i], itemProp, key);
            }
            s.JsonPropertyNames = new System.Text.Json.JsonEncodedText[names.Count];
            for (int i = 0; i < names.Count; i++) {
                s.JsonPropertyNames[i] = System.Text.Json.JsonEncodedText.Encode(names[i]);
            }

            s.Fields = s._buildingFields.ToArray();
            s.FieldTypes = s._buildingFieldsTypes.ToArray();
            s.FieldDataIntents = s._buildingFieldsDataIntents.ToArray();
            s.FieldNames = new string[] {};
            s.FieldGetters = new Func<object, object>[s.FieldNames.Length];
            s.TypedFieldGetters = new Delegate[s.FieldNames.Length];
            
            return s;
        }

        public static JSDataSourceSchema Create(Type c) {
            JSDataSourceSchema s = new JSDataSourceSchema();

            // RS TFS272017 - We need to grab the property infos from the base types if they exist
            // so when a user provides the datasource with a list of a base type and adds multiple different
            // classes within that list all derived from that base type we can handle obtaining property values
            // without issue.
            List<PropertyInfo> properties = new List<PropertyInfo>();
            GetPropertiesFromType(c, properties);

            List<FieldInfo> fields = new List<FieldInfo>();
            GetFieldsFromType(c, fields);

            for (int i = 0; i < properties.Count; i++) {
                var curr = properties[i];
                if (curr.DeclaringType == typeof(Object) || curr.GetIndexParameters().Length > 0) {
                    continue;
                }
            
                if (curr.GetMethod != null && curr.GetMethod.IsPublic) {
                    if (!curr.GetMethod.IsStatic)
                    {
                        s.AddProperty(curr);
                    }
                }
            }

            for (int i = 0; i < fields.Count; i++) {
                FieldInfo curr = fields[i];
                if (curr.DeclaringType == typeof(Object)) {
                    continue;
                }
                if (curr.IsPublic && !curr.IsSpecialName && !curr.IsStatic) {
                    s.AddField(curr);
                }
            }
            s.Commit();
            return s;
        }

        public object ResolveValue(String name, Object item, Func<object, object> propGetter, JsonDataSourceItem jsonItem, JSDataSourceSchemaType type, DataSourceManager manager) {
            if (item == null)
            {
                return null;
            }

            try {
                object value =  propGetter(item);
                if (type == JSDataSourceSchemaType.ObjectValue) {
                    return GetSubObject(name, value, jsonItem, manager);
                }
                return value;
            } catch (Exception e) {
                return null;
            }
        }

        private object GetSubObject(String name, Object value, JsonDataSourceItem rootItem, DataSourceManager manager) {
            bool checkedArray = _checkedArray.ContainsKey(name);
            if (!checkedArray && value != null) {
                _checkedArray[name] = true;
                if (value.GetType().IsArray || value is IEnumerable || value is IList) {
                    _subSchemas[name] = BuildSubObjectSchema(value);

                    if (NotifyModified != null)
                    {
                        NotifyModified();
                    }
                }
            }
            if (!_subSchemas.ContainsKey(name)) {
                if (value == null) {
                    return JsonDataSourceItem.Create(value, null, manager, rootItem);
                }
                _subSchemas[name] = JsonDataSourceItem.ExtractSchema(value);
                if (NotifyModified != null)
                {
                    NotifyModified();
                }
            }
            JSDataSourceSchema subSchema = _subSchemas[name];

            return  JsonDataSourceItem.Create(value, subSchema, manager, rootItem);
        }

        public JSDataSourceSchema BuildSubObjectSchema(object subObject)
        {
            if (subObject == null)
            {
                return null;
            }

            var schema = JsonDataSourceItem.ExtractSchema(subObject);
            JSDataSourceSchema itemSchema = null;
            if (subObject is IEnumerable)
            {
                var collection = subObject as IEnumerable;
                foreach (var item in collection)
                {
                    if (item != null)
                    {
                        subObject = item;
                        itemSchema = JsonDataSourceItem.ExtractSchema(item);
                        break;
                    }
                }
                if (itemSchema != null)
                {
                    schema.SetSubSchema("Items", itemSchema);
                }
            }
            else
            {
                itemSchema = schema;
            }

            if (itemSchema != null)
            {
                for (int i = 0; i < itemSchema.PropertyTypes.Length; i++)
                {
                    if (itemSchema.PropertyTypes[i] == JSDataSourceSchemaType.ObjectValue)
                    {
                        var obj = itemSchema.PropertyGetters[i](subObject);
                        itemSchema.SetSubSchema(itemSchema.PropertyNames[i], BuildSubObjectSchema(obj));
                    }
                }
            }
            return schema;
        }

        public object ResolveFieldValue(String name, Object item, Func<object, object> fieldGetter, JsonDataSourceItem rootItem, JSDataSourceSchemaType type, DataSourceManager manager) {
            try {
                object value = fieldGetter(item);
                if (type == JSDataSourceSchemaType.ObjectValue) {
                    return GetSubObject(name, value, rootItem, manager);
                }
                return value;
            } catch (Exception e) {
                return null;
            }
        }

        public String RenderDate(object value) 
        {
             if (value is DateTime) {
                return "@d:" + ((DateTime)value).ToString("o");
            }
           
            return "null";
        }

        private JSDataSourceSchema _itemSchema = null;

        public JSDataSourceSchema ItemSchema 
        {
            get 
            {
                return _itemSchema;
            }
            set 
            {
                _itemSchema = value;
            }
        }

        public bool IsPrimitive { get; set; }
        public JSDataSourceSchemaType PrimitiveType { get; set; }

        private List<PropertyInfo> _buildingProperties = new List<PropertyInfo>();
        private List<JSDataSourceSchemaType> _buildingPropertiesTypes = new List<JSDataSourceSchemaType>();
        private List<IDataIntentAttribute[]> _buildingPropertiesDataIntents = new List<IDataIntentAttribute[]>();
        private List<FieldInfo> _buildingFields = new List<FieldInfo>();
        private List<JSDataSourceSchemaType> _buildingFieldsTypes = new List<JSDataSourceSchemaType>();
        private List<IDataIntentAttribute[]> _buildingFieldsDataIntents = new List<IDataIntentAttribute[]>();

        private Dictionary<string, JSDataSourceSchema> _subSchemas = new Dictionary<string, JSDataSourceSchema>();

        public bool IsNullable(string propertyName)
        {
            if (IsDictionary)
            {
                //TODO: maybe true here.
                return false;
            }
            for (int i = 0; i < PropertyNames.Length; i++)
            {
                if (PropertyNames[i] == propertyName)
                {
                    return IsNullable(Properties[i].PropertyType);
                }
            }
            return false;
        }
        public bool IsNullable(Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public JSDataSourceSchema GetSubSchema(string propertyName)
        {
            if (_subSchemas.ContainsKey(propertyName))
            {
                return _subSchemas[propertyName];
            }
            return null;
        }
        public void SetSubSchema(string propertyName, JSDataSourceSchema schema)
        {
            _subSchemas[propertyName] = schema;
        }

        public JSDataSourceSchemaType ResolveSchemaType(Type type) 
        {
            if (type == typeof(double))
            {
                return JSDataSourceSchemaType.DoubleValue;
            }
            if (type == typeof(string))
            {
                return JSDataSourceSchemaType.StringValue;
            }
            if (type == typeof(float))
            {
                return JSDataSourceSchemaType.SingleValue;
            }
            if (type == typeof(int))
            {
                return JSDataSourceSchemaType.IntValue;
            }
            if (type == typeof(short))
            {
                return JSDataSourceSchemaType.ShortValue;
            }
            if (type == typeof(byte)) {
                return JSDataSourceSchemaType.ByteValue;
            }
            if (type == typeof(decimal)) {
                return JSDataSourceSchemaType.DecimalValue;
            }
            if (type == typeof(long))
            {
                return JSDataSourceSchemaType.LongValue;
            }
            if (type == typeof(bool))
            {
                return JSDataSourceSchemaType.BooleanValue;
            }
            if (type == typeof(DateTime) || typeof(DateTime).IsAssignableFrom(type))
            {
                return JSDataSourceSchemaType.DateTimeValue;
            }
            if (type.IsEnum)
            {
                var underlyingType = Enum.GetUnderlyingType(type);
                return ResolveSchemaType(underlyingType);
            }
            if (IsNullable(type))
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                var resolvedType = ResolveSchemaType(underlyingType);// + ((int)JSDataSourceSchemaType.Size / 2);
                switch (resolvedType)
                {
                    case JSDataSourceSchemaType.BooleanValue:
                        return JSDataSourceSchemaType.NullableBooleanValue;
                    case JSDataSourceSchemaType.ByteValue:
                        return JSDataSourceSchemaType.NullableByteValue;
                    case JSDataSourceSchemaType.CalendarValue:
                        return JSDataSourceSchemaType.NullableCalendarValue;
                    case JSDataSourceSchemaType.DateTimeValue:
                        return JSDataSourceSchemaType.NullableDateTimeValue;
                    case JSDataSourceSchemaType.DecimalValue:
                        return JSDataSourceSchemaType.NullableDecimalValue;
                    case JSDataSourceSchemaType.DoubleValue:
                        return JSDataSourceSchemaType.NullableDoubleValue;
                    case JSDataSourceSchemaType.IntValue:
                        return JSDataSourceSchemaType.NullableIntValue;
                    case JSDataSourceSchemaType.LongValue:
                        return JSDataSourceSchemaType.NullableLongValue;
                    case JSDataSourceSchemaType.ShortValue:
                        return JSDataSourceSchemaType.NullableShortValue;
                    case JSDataSourceSchemaType.SingleValue:
                        return JSDataSourceSchemaType.NullableSingleValue;
                }
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                Type enumerableType = null;
                if (type.IsArray)
                {
                    enumerableType = type.GetElementType();
                }
                else if (type.IsGenericType)
                {
                    enumerableType = type.GetGenericArguments()[0];
                }
                if (enumerableType != null)
                {
                    var resolvedType = ResolveSchemaType(enumerableType);
                    switch (resolvedType)
                    {
                        case JSDataSourceSchemaType.BooleanValue:
                            return JSDataSourceSchemaType.BooleanArrayValue;
                        case JSDataSourceSchemaType.ByteValue:
                            return JSDataSourceSchemaType.ByteArrayValue;
                        case JSDataSourceSchemaType.CalendarValue:
                            return JSDataSourceSchemaType.CalendarArrayValue;
                        case JSDataSourceSchemaType.DateTimeValue:
                            return JSDataSourceSchemaType.DateTimeArrayValue;
                        case JSDataSourceSchemaType.DecimalValue:
                            return JSDataSourceSchemaType.DecimalArrayValue;
                        case JSDataSourceSchemaType.DoubleValue:
                            return JSDataSourceSchemaType.DoubleArrayValue;
                        case JSDataSourceSchemaType.IntValue:
                            return JSDataSourceSchemaType.IntArrayValue;
                        case JSDataSourceSchemaType.LongValue:
                            return JSDataSourceSchemaType.LongArrayValue;
                        case JSDataSourceSchemaType.ShortValue:
                            return JSDataSourceSchemaType.ShortArrayValue;
                        case JSDataSourceSchemaType.SingleValue:
                            return JSDataSourceSchemaType.SingleArrayValue;
                        case JSDataSourceSchemaType.StringValue:
                            return JSDataSourceSchemaType.StringArrayValue;
                    }
                }
            }

            return JSDataSourceSchemaType.ObjectValue;
        }

        public void AddProperty(PropertyInfo prop) 
        {
            _buildingProperties.Add(prop);

            List<IDataIntentAttribute> dataIntents = null;
            var attrs = prop.GetCustomAttributes();
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    if (attr is IDataIntentAttribute)
                    {
                        if (dataIntents == null)
                        {
                            dataIntents = new List<IDataIntentAttribute>();
                        }
                        dataIntents.Add((IDataIntentAttribute)attr);
                    }
                }
            }   
            _buildingPropertiesDataIntents.Add(dataIntents == null ? null : dataIntents.ToArray());

            Type ret = prop.PropertyType;

            JSDataSourceSchemaType type = ResolveSchemaType(ret);
            //if (type == JsonDataSourceSchemaType.OBJECT_VALUE) {
            //    _subSchemas.put(method.getName().substring(3, 4).toLowerCase() + method.getName().substring(4), create(ret));
            //}
            _buildingPropertiesTypes.Add(type);
        }

        public void AddField(FieldInfo curr) 
        {
            _buildingFields.Add(curr);
            Type ret = curr.FieldType;
            JSDataSourceSchemaType type = ResolveSchemaType(ret);

            List<IDataIntentAttribute> dataIntents = null;
            var attrs = curr.GetCustomAttributes();
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    if (attr is IDataIntentAttribute)
                    {
                        if (dataIntents == null)
                        {
                            dataIntents = new List<IDataIntentAttribute>();
                        }
                        dataIntents.Add((IDataIntentAttribute)attr);
                    }
                }
            }   
            _buildingFieldsDataIntents.Add(dataIntents == null ? null : dataIntents.ToArray());
            //if (type == JsonDataSourceSchemaType.OBJECT_VALUE) {
            //    _subSchemas.put(curr.getName(), create(ret));
            //}
            _buildingFieldsTypes.Add(type);
        }

        public PropertyInfo[] Properties;
        public Func<object, object>[] PropertyGetters;
        public Func<object, object>[] FieldGetters;
        public Delegate[] TypedPropertyGetters;
        public Delegate[] TypedFieldGetters;
        public JSDataSourceSchemaType[] PropertyTypes;
        public IDataIntentAttribute[][] PropertyDataIntents;
        public System.Text.Json.JsonEncodedText[] JsonPropertyNames;
        public System.Text.Json.JsonEncodedText[] JsonFieldNames;
        public String[] PropertyNames;
        public String[] FieldNames;
        public FieldInfo[] Fields;
        public JSDataSourceSchemaType[] FieldTypes;
        public IDataIntentAttribute[][] FieldDataIntents;

        private System.Linq.Expressions.UnaryExpression GetConversion(Type type, System.Linq.Expressions.Expression expression)
        {
            var isValueType = type.IsValueType;
            var isGenericType = type.IsGenericType;
            var isNullable = isGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isValueType && !(isGenericType && isNullable))
            {
                return System.Linq.Expressions.Expression.Convert(expression, type);
            }
            else
            {
                return System.Linq.Expressions.Expression.TypeAs(expression, type);
            }
        }

        private Func<object, object> GetPropertyValueGetter(Type type, PropertyInfo propertyInfo)
        {
            //var propertyInfo = type.GetProperty(propertyName);

            //if (propertyInfo != null)
            {
                System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object), "obj");
                System.Linq.Expressions.UnaryExpression conversion = this.GetConversion(type, param);

                System.Linq.Expressions.UnaryExpression prop = System.Linq.Expressions.Expression.TypeAs(System.Linq.Expressions.Expression.Property(conversion, propertyInfo), typeof(object));
                var getPropertyValue = (Func<object, object>)System.Linq.Expressions.Expression.Lambda(prop, param).Compile();

                return getPropertyValue;
            }
        }

        private Delegate GetTypedPropertyValueGetter(Type type, PropertyInfo propertyInfo)
        {
            //var propertyInfo = type.GetProperty(propertyName);

            //if (propertyInfo != null)
            {
                System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object), "obj");
                System.Linq.Expressions.UnaryExpression conversion = this.GetConversion(type, param);

                System.Linq.Expressions.Expression prop = System.Linq.Expressions.Expression.Property(conversion, propertyInfo);
                if (propertyInfo.PropertyType.IsEnum)
                {
                    var underlyingType = Enum.GetUnderlyingType(propertyInfo.PropertyType);
                    conversion = GetConversion(underlyingType, prop);
                    return System.Linq.Expressions.Expression.Lambda(conversion, param).Compile();
                }
                var getPropertyValue = System.Linq.Expressions.Expression.Lambda(prop, param).Compile();

                return getPropertyValue;
            }
        }

        private Delegate GetTypedDictionaryValueGetter(Type dictType, Type valueType, PropertyInfo itemProp, string key)
        {
            //var propertyInfo = type.GetProperty(propertyName);

            //if (propertyInfo != null)
            {
                System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object), "obj");
                System.Linq.Expressions.UnaryExpression conversion = this.GetConversion(dictType, param);

                System.Linq.Expressions.Expression strIndex = System.Linq.Expressions.ConstantExpression.Constant(key);
                System.Linq.Expressions.Expression prop = System.Linq.Expressions.Expression.Property(conversion, itemProp, strIndex);
                System.Linq.Expressions.UnaryExpression retConversion = this.GetConversion(valueType, prop);
                if (valueType.IsEnum)
                {
                    var underlyingType = Enum.GetUnderlyingType(valueType);
                    retConversion = GetConversion(underlyingType, retConversion);
                }
                var getPropertyValue = System.Linq.Expressions.Expression.Lambda(retConversion, param).Compile();

                return getPropertyValue;
            }
        }

        private Func<object, object> GetFieldValueGetter(Type type, FieldInfo fieldInfo)
        {
            //var propertyInfo = type.GetProperty(propertyName);

            //if (propertyInfo != null)
            {
                System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object), "obj");
                System.Linq.Expressions.UnaryExpression conversion = this.GetConversion(type, param);

                System.Linq.Expressions.UnaryExpression prop = System.Linq.Expressions.Expression.TypeAs(System.Linq.Expressions.Expression.Field(conversion, fieldInfo), typeof(object));
                var getPropertyValue = (Func<object, object>)System.Linq.Expressions.Expression.Lambda(prop, param).Compile();

                return getPropertyValue;
            }
        }

        private Delegate GetTypedFieldValueGetter(Type type, FieldInfo fieldInfo)
        {
            //var propertyInfo = type.GetProperty(propertyName);

            //if (propertyInfo != null)
            {
                System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object), "obj");
                System.Linq.Expressions.UnaryExpression conversion = this.GetConversion(type, param);

                System.Linq.Expressions.Expression prop = System.Linq.Expressions.Expression.Field(conversion, fieldInfo);
                if (fieldInfo.FieldType.IsEnum)
                {
                    var underlyingType = Enum.GetUnderlyingType(fieldInfo.FieldType);
                    conversion = GetConversion(underlyingType, prop);
                    return System.Linq.Expressions.Expression.Lambda(conversion, param).Compile();
                }
                var getPropertyValue = System.Linq.Expressions.Expression.Lambda(prop, param).Compile();

                return getPropertyValue;
            }
        }

        public void Commit() {
            Properties = _buildingProperties.ToArray();
            PropertyTypes = _buildingPropertiesTypes.ToArray();
            PropertyDataIntents = _buildingPropertiesDataIntents.ToArray();
            PropertyNames = new String[_buildingProperties.Count];
            PropertyGetters = new Func<object, object>[_buildingProperties.Count];
            TypedPropertyGetters = new Delegate[_buildingProperties.Count];
            for (int i = 0; i < _buildingProperties.Count; i++) {
                PropertyNames[i] = _buildingProperties[i].Name;
            }
            for (int i = 0; i < _buildingProperties.Count; i++) {
                PropertyGetters[i] = GetPropertyValueGetter(_buildingProperties[i].DeclaringType, _buildingProperties[i]);
            }
             for (int i = 0; i < _buildingProperties.Count; i++) {
                TypedPropertyGetters[i] = GetTypedPropertyValueGetter(_buildingProperties[i].DeclaringType, _buildingProperties[i]);
            }
            JsonPropertyNames = new System.Text.Json.JsonEncodedText[_buildingProperties.Count];
            for (int i = 0; i < _buildingProperties.Count; i++) {
                JsonPropertyNames[i] = System.Text.Json.JsonEncodedText.Encode(_buildingProperties[i].Name);
            }

            Fields = _buildingFields.ToArray();
            FieldNames = new String[_buildingFields.Count];
            FieldTypes = _buildingFieldsTypes.ToArray();
            FieldDataIntents = _buildingFieldsDataIntents.ToArray();
            FieldGetters = new Func<object, object>[_buildingFields.Count];
            TypedFieldGetters = new Func<object, object>[_buildingFields.Count];
            for (int i = 0; i < _buildingFields.Count; i++) {
                FieldNames[i] = _buildingFields[i].Name;
            }
            for (int i = 0; i < _buildingFields.Count; i++) {
                FieldGetters[i] = GetFieldValueGetter(_buildingFields[i].DeclaringType, _buildingFields[i]);
            }
            for (int i = 0; i < _buildingFields.Count; i++) {
                TypedFieldGetters[i] = GetTypedFieldValueGetter(_buildingFields[i].DeclaringType, _buildingFields[i]);
            }
            JsonFieldNames = new System.Text.Json.JsonEncodedText[_buildingFields.Count];
            for (int i = 0; i < _buildingFields.Count; i++) {
                JsonFieldNames[i] = System.Text.Json.JsonEncodedText.Encode(_buildingFields[i].Name);
            }

            if (NotifyModified != null)
            {
                NotifyModified();
            }
        }
    }

    internal enum JSDataSourceSchemaType {
        DoubleValue,
        IntValue,
        LongValue,
        StringValue,
        CalendarValue,
        DateTimeValue,
        BooleanValue,
        ObjectValue,
        DecimalValue,
        ByteValue,
        ShortValue,
        SingleValue,

        NullableDoubleValue,
        NullableIntValue,
        NullableLongValue,
        NullableCalendarValue,
        NullableDateTimeValue,
        NullableBooleanValue,
        NullableDecimalValue,
        NullableByteValue,
        NullableShortValue,
        NullableSingleValue,

        DoubleArrayValue,
        IntArrayValue,
        LongArrayValue,
        StringArrayValue,
        CalendarArrayValue,
        DateTimeArrayValue,
        BooleanArrayValue,
        DecimalArrayValue,
        ByteArrayValue,
        ShortArrayValue,
        SingleArrayValue
    }
}