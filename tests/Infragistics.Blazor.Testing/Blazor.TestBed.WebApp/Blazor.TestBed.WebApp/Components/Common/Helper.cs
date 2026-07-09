using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Converters;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Blazor.TestBed.WebApp.Components.Common
{
    public static class Helper
    {
        public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }

    //short helper class to ignore some properties from serialization
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (this.ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
                property.PropertyName = "Ignored" + property.PropertyName;
                return property;
            }

            string actualName = ReflectionUtils.Capitalize(ReflectionUtils.GetActualPropertyName((PropertyInfo)member));
            if (property.PropertyName != actualName)
            {
                property.PropertyName = actualName; // some properties are renamed...
            }

            if (((PropertyInfo)member).PropertyType.IsEnum)
            {
                property.Converter = new BlazorEnumConverter();
            }

            if (((PropertyInfo)member).PropertyType == typeof(double))
            {
                property.Converter = new DoubleToIntConverter();
            }
            return property;
        }
    }

    public class DoubleToIntConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert.ToInt32(value));
        }
    }

    public class BlazorEnumConverter : StringEnumConverter
    {
        public BlazorEnumConverter() : base()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var customNameAttr = fieldInfo.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().Name == "WCEnumNameAttribute");
            if (customNameAttr != null)
            {
                var attrValue = customNameAttr?.GetType().GetProperty("Name").GetValue(customNameAttr).ToString();
                writer.WriteValue(attrValue);
            }
            else
            {
                base.WriteJson(writer, value, serializer);
            }
        }
    }
}
