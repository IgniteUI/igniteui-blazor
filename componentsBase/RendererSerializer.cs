using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace IgniteUI.Blazor.Controls 
{

    internal partial class RendererSerializer {
        public RendererSerializer(SerializationContext context, ComponentBase component, string name)
        {
            _name = name;
            _context = context;
            _component = component;
        }

        private string _name;
        private ComponentBase _component;

        private SerializationContext _context;

        //private List<string> _properties = new List<string>();
        private string _type = null;

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public void AddBooleanProp(string propertyName, bool value) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }
            _context.Writer.WriteBoolean(propertyName, value);
            //_properties.Add("\"" + propertyName + "\"" + ": " + value.ToString(CultureInfo.InvariantCulture).ToLower());
        }

        public void AddStringProp(string propertyName, string value) {
            if (_context.Filter != null)
            {
                if (propertyName != "name" && propertyName != "type")
                {
                    if (!_context.Filter(_name, propertyName))
                    {
                        return;
                    }
                }
            }
            _context.Writer.WriteString(propertyName, value);
            //_properties.Add("\"" + propertyName + "\"" + ": \"" + (value == null ? "null" : value) + "\"");
        }

        public void AddPrimitiveProp(object val)
        {
            if (val is Array)
            {
                var objArr = (IList)val;
                _context.Writer.WriteStartArray();
                for (var i = 0; i < objArr.Count; i++)
                {
                    var subVal = objArr[i];
                    AddPrimitiveProp(subVal);
                }
                _context.Writer.WriteEndArray();
            }
            else if (val is double)
            {
                _context.Writer.WriteNumberValue((double)val);
            }
            else if (val is int)
            {
                _context.Writer.WriteNumberValue((int)val);
            }
            else if (val is long)
            {
                _context.Writer.WriteNumberValue((long)val);
            }
            else if (val is short)
            {
                _context.Writer.WriteNumberValue((short)val);
            }
            else if (val is bool)
            {
                _context.Writer.WriteBooleanValue((bool)val);
            }
            else if (val is DateTime)
            {
                _context.Writer.WriteStringValue("@d:" + ((DateTime)val).ToString("o"));
            }
            else if (val is string)
            {
                _context.Writer.WriteStringValue(val.ToString());
            }
            else
            {
                // ObjectToParam this thing
                if (_component is BaseRendererElement)
                {
                    (_component as BaseRendererElement).ObjectToParam(_context, val);
                }
                else if (_component is BaseRendererControl)
                {
                    (_component as BaseRendererControl).ObjectToParam(_context, val);
                }
            }
        }

        public void AddPrimitiveProp(string propertyName, object val) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (val is Array)
            {
                var objArr = (IList)val;
                _context.Writer.WriteStartArray(propertyName);
                for (var i = 0; i < objArr.Count; i++)
                {
                    var subVal = objArr[i];
                    AddPrimitiveProp(subVal);
                }
                _context.Writer.WriteEndArray();
            } 
            else if (val is double)
            {
                _context.Writer.WriteNumber(propertyName, (double)val);
            }
            else if (val is int)
            {
                _context.Writer.WriteNumber(propertyName, (int)val);
            }
            else if (val is long)
            {
                _context.Writer.WriteNumber(propertyName, (long)val);
            }
            else if (val is short)
            {
                _context.Writer.WriteNumber(propertyName, (short)val);
            }
            else if (val is bool)
            {
                _context.Writer.WriteBoolean(propertyName, (bool)val);
            }
            else if (val is DateTime)
            {
                _context.Writer.WriteString(propertyName, "@d:" + ((DateTime)val).ToString("o"));
            }
            else if (val is string)
            {
                _context.Writer.WriteString(propertyName, val.ToString());
            }
            else
            {
                // ObjectToParam this thing
                if (_component is BaseRendererElement)
                {
                    (_component as BaseRendererElement).ObjectToParam(_context, propertyName, val);
                }
                else if (_component is BaseRendererControl)
                {
                    (_component as BaseRendererControl).ObjectToParam(_context, propertyName, val);
                }
            }
        }

        public void AddArrayProp(string propertyName, object values) {
            bool containsSub = false;
            var valuesArray = values as object[];
            if (values != null)
            {
                for (int i = 0; i < valuesArray.Length; i++) {
                    object val = valuesArray[i];
                    if (val is BaseRendererControl || val is BaseRendererElement) {
                        containsSub = true;
                        break;
                    }
                }
            }
            var context = _context;
            if (!containsSub && _context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
                else
                {
                    context = new SerializationContext(_context.Writer, null);
                }
            }
            if (values == null) {
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                context.Writer.WriteNull(propertyName);
                return;
            }
            //string[] strValues = new string[values.Length];
            context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < valuesArray.Length; i++) {
                object val = valuesArray[i];
                if (val is String) {
                    context.Writer.WriteStringValue((string)val);
                    //strValues[i] = "\"" + (string)val + "\"";
                } else if (val is JsonSerializable) {
                    ((JsonSerializable)val).Serialize(context);
                }
                else {
                    if (val is double)
                    {
                        context.Writer.WriteNumberValue((double)val);
                    }
                    else if (val is int)
                    {
                        context.Writer.WriteNumberValue((int)val);
                    }
                    else if (val is long)
                    {
                        context.Writer.WriteNumberValue((long)val);
                    }
                    else if (val is short)
                    {
                        context.Writer.WriteNumberValue((long)val);
                    }
                    else if (val is bool)
                    {
                        context.Writer.WriteBooleanValue((bool)val);
                    }
                    else if (val is DateTime)
                    {
                        context.Writer.WriteStringValue(((DateTime)val).ToString("o"));
                    }
                    else if (val is string)
                    {
                        context.Writer.WriteStringValue(val.ToString());
                    }
                    else
                    {
                        if (_component is BaseRendererElement)
                        {
                            (_component as BaseRendererElement).ObjectToParam(context, val);
                        }
                        else if (_component is BaseRendererControl)
                        {
                            (_component as BaseRendererControl).ObjectToParam(context, val);
                        }
                    }
                }
            }
            context.Writer.WriteEndArray();
            //_properties.Add("\"" + propertyName + "\"" + ": [" + String.Join(", ", strValues) + " ]");
        }

        protected string Camelize(string value)
        {
            if (value == null || value.Length == 0) {
                return value;
            }
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }


        public void AddEnumProp(string propertyName, Enum value) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            _context.Writer.WriteString(propertyName, Camelize(value.ToString()));
            //_properties.Add("\"" + propertyName + "\"" + ": \"" + (value.ToString()) + "\"" );
        }

        // private String toPascal(String name) {
        //     String[] parts = TextUtils.split(name, "_");
        //     for (int i = 0; i < parts.length; i++) {
        //         parts[i] = parts[i].substring(0, 1) + parts[i].substring(1).toLowerCase();
        //     }
        //     return TextUtils.join("", parts);
        // }

        public void AddNumberProp(String propertyName, Object value) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }
            if (value is double)
            {
                _context.Writer.WriteNumber(propertyName, (double)value);
            }
            else if (value is int)
            {
                _context.Writer.WriteNumber(propertyName, (int)value);
            }
            else if (value is TimeSpan)
            {
                _context.Writer.WriteNumber(propertyName, ((TimeSpan)value).TotalMilliseconds);
            }
            else if (value is IConvertible)
            {
                _context.Writer.WriteNumber(propertyName, ((IConvertible)value).ToDouble(CultureInfo.InvariantCulture));
            }
            else
            {
                _context.Writer.WriteString(propertyName, value?.ToString());
            }
            //_properties.Add("\"" + propertyName + "\"" + ": " + Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        public void AddDateTimeProp(String propertyName, DateTime? value) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }
            _context.Writer.WriteString(propertyName, value != null ? value.Value.ToString("o") : null);
            //_properties.Add("\"" + propertyName + "\"" + ": \"" + value.ToString("o") + "\"");
        }

        public void Start(string propertyName = null)
        {
            if (propertyName != null)
            {
                _context.Writer.WriteStartObject(propertyName);
            }
            else
            {
                _context.Writer.WriteStartObject();
            }
        }

        public void End() {
            _context.Writer.WriteString("type", Type);
            _context.Writer.WriteEndObject();
        }

        public void AddSerializableProp(String propertyName, JsonSerializable value) {
            var context = _context;
                
            if (value == null) {

                if (_context.Filter != null)
                {
                    if (!_context.Filter(_name, propertyName))
                    {
                        return;
                    }
                    else
                    {
                        context = new SerializationContext(_context.Writer, null);
                    }
                }          
                context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }

            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                }
                else
                {
                    context = new SerializationContext(_context.Writer, null);
                }
            }   
            value.Serialize(context, propertyName);
            //_properties.Add("\"" + propertyName + "\"" + ": " + value.Serialize());
        }

        public void AddStringArrayProp(String propertyName, string[] values) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (values == null) {
                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }
            //Console.WriteLine("parsing brush array");
            var v = values;
            //v = v.Trim();
            
            var parts = values;
            for (var i = 0; i < parts.Length; i++) {
                parts[i] = parts[i].Trim();
                //Console.WriteLine("brush part: " + parts[i]);
            }

            _context.Writer.WriteStartArray(propertyName);
            //string[] strValues = new string[parts.Length];
            for (int i = 0; i < parts.Length; i++) {
                string val = parts[i];
                _context.Writer.WriteStringValue(val);
                //strValues[i] = "\"" + val + "\"";
            }
            _context.Writer.WriteEndArray();

            // var arrayParts = string.Join(", ", strValues);
            // //Console.WriteLine("arrayParts: " + arrayParts);
            // _properties.Add("\"" + propertyName + "\"" + ": [" + arrayParts + " ]");
        }

        public void AddDateArrayProp(String propertyName, DateTime[] values) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (values == null) {
                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }
            //Console.WriteLine("parsing brush array");
            var v = values;
            //v = v.Trim();
            
            var parts = values;
            // for (var i = 0; i < parts.Length; i++) {
            //     parts[i] = parts[i].Trim();
            //     //Console.WriteLine("brush part: " + parts[i]);
            // }

            //string[] strValues = new string[parts.Length];
            //var result = string.Join(",", values.Select(d => d.ToString("o")));
            //_context.Writer.WriteString(propertyName, result);
            // needed because of refactoring in the wc calendar
            // PR: https://github.com/IgniteUI/igniteui-webcomponents/pull/1200


            _context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < parts.Length; i++)
            {
                DateTime val = parts[i];
                _context.Writer.WriteStringValue(val.ToString("o"));
            }
            _context.Writer.WriteEndArray();

            // var arrayParts = string.Join(", ", strValues);
            // //Console.WriteLine("arrayParts: " + arrayParts);
            // _properties.Add("\"" + propertyName + "\"" + ": [" + arrayParts + " ]");
        }

        private Regex _colorSplitRegex = new Regex("[\\s,]+(?![^(]*\\))");
        public void AddStringArrayProp(String propertyName, string values) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (values == null) {
                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }
            //Console.WriteLine("parsing brush array");
            var v = values;
            v = v.Trim();
            
            var parts = _colorSplitRegex.Split(v);
            for (var i = 0; i < parts.Length; i++) {
                parts[i] = parts[i].Trim();
                //Console.WriteLine("brush part: " + parts[i]);
            }

            _context.Writer.WriteStartArray(propertyName);
            //string[] strValues = new string[parts.Length];
            for (int i = 0; i < parts.Length; i++) {
                string val = parts[i];
                _context.Writer.WriteStringValue(val);
                //strValues[i] = "\"" + val + "\"";
            }
            _context.Writer.WriteEndArray();

            // var arrayParts = string.Join(", ", strValues);
            // //Console.WriteLine("arrayParts: " + arrayParts);
            // _properties.Add("\"" + propertyName + "\"" + ": [" + arrayParts + " ]");
        }

        public void AddEnumArrayProp(String propertyName, object values) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (values == null) {
                _context.Writer.WriteNull(propertyName);
                return;
                //_properties.Add("\"" + propertyName + "\"" + ": null");
            }
            IList vals = (IList)(object)values;
            //string[] strValues = new string[vals.Count];
            _context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < vals.Count; i++) {
                Enum val = (Enum)vals[i];
                _context.Writer.WriteStringValue(Camelize(val.ToString()));
                //strValues[i] = "\"" + val.ToString() + "\"";
            }
            _context.Writer.WriteEndArray();
            //_properties.Add("\"" + propertyName + "\"" + ": [" + string.Join(", ", strValues) + " ]");
        }

        public void AddIntArrayProp(String propertyName, int[] values) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (values == null) {
                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }
            //string[] strValues = new string[values.Length];
            _context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < values.Length; i++) {
                int val = values[i];
                //strValues[i] = val.ToString(CultureInfo.InvariantCulture);
                _context.Writer.WriteNumberValue(val);
            }
            _context.Writer.WriteEndArray();
            //_properties.Add("\"" + propertyName + "\"" + ": [" + string.Join(", ", strValues) + " ]");
        }

        public void AddDoubleArrayProp(string propertyName, double[] numbers) {
            if (_context.Filter != null)
            {
                if (!_context.Filter(_name, propertyName))
                {
                    return;
                }
            }

            if (numbers == null) {
                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }

            //List<string> items = new List<string>();
            _context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < numbers.Length; i++) {
                //string c = numbers[i].ToString(CultureInfo.InvariantCulture);
                //items.Add(c);
                _context.Writer.WriteNumberValue(numbers[i]);
            }
            _context.Writer.WriteEndArray();
            //_properties.Add("\"" + propertyName + "\": " + "[" + string.Join(", ", items) + "]");
        }

        public void AddSerializableArrayProp<T>(string propertyName, T[] array) where T: JsonSerializable {
            if (array == null) {
                if (_context.Filter != null)
                {
                    if (!_context.Filter(_name, propertyName))
                    {
                        return;
                    }
                }

                _context.Writer.WriteNull(propertyName);
                //_properties.Add("\"" + propertyName + "\"" + ": null");
                return;
            }


            var context = _context;
            if (_context.Filter != null)
            {
                if (_context.Filter(_name, propertyName))
                {
                    context = new SerializationContext(_context.Writer, null);
                }
            }
            //List<string> items = new List<string>();
            context.Writer.WriteStartArray(propertyName);
            for (int i = 0; i < array.Length; i++) {
                //string c = numbers[i].ToString(CultureInfo.InvariantCulture);
                //items.Add(c);
                var item = (JsonSerializable)array[i];
                if (item == null)
                {
                    context.Writer.WriteNullValue();
                }
                else
                {
                    item.Serialize(context);
                }
            }
            context.Writer.WriteEndArray();
            //_properties.Add("\"" + propertyName + "\"" + ": " + coll.Serialize());
        }

        public void AddCollectionProp<T>(string propertyName, BaseCollection<T> coll) {
            var context = _context;
            if (_context.Filter != null)
            {
                if (_context.Filter(_name, propertyName))
                {
                    context = new SerializationContext(_context.Writer, null);
                }
            }

            coll.Serialize(context, propertyName);
            //_properties.Add("\"" + propertyName + "\"" + ": " + coll.Serialize());
        }
    }
}
