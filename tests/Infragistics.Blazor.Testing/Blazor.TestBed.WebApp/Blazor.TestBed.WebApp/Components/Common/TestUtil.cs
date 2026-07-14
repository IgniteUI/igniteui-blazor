using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using IgniteUI.Blazor.Controls;

namespace Blazor.TestBed.WebApp.Components.Common
{
    public static class TestUtil
    {
        public static bool PropertyValuesAreEqual(object? serverValue, string? clientValue, PropertyInfo prop) {
            string serverString = serverValue is Enum ?
                EnumActualValue(serverValue) :
                JsonConvert.SerializeObject(serverValue, new JsonSerializerSettings()
            { ContractResolver = new IgnorePropertiesResolver(ReflectionUtils.IgnoredProps().ToArray()), NullValueHandling = NullValueHandling.Ignore });
            if (serverString == clientValue) {
                return true;
            }
            if (clientValue == null)
            {
                clientValue = "null";
            }
            if (prop.PropertyType.Name == "Double" && serverValue != null)
            {
                serverString = Convert.ToInt32(serverValue).ToString();
            }

            if (prop.PropertyType == typeof(DateTime))
            {
                serverString = JsonConvert.DeserializeObject<DateTime>(serverString).ToShortDateString();
                clientValue = JsonConvert.DeserializeObject<DateTime>(clientValue).ToShortDateString();
            }

            if (prop.PropertyType == typeof(DateTime[]))
            {
                var serverDates = JsonConvert.DeserializeObject<DateTime[]>(serverString);
                var clientDates = JsonConvert.DeserializeObject<DateTime[]>(clientValue);
                if (serverDates == null || clientDates == null)
                {
                    return false;
                }

                var serverArray = serverDates.Select(x => x.ToShortDateString());
                var clientArray = clientDates.Select(x => x.ToShortDateString());
                serverString = string.Join(",", serverArray);
                clientValue = string.Join(",", clientArray);
            }

            if (prop.PropertyType == typeof(IgbDateRangeDescriptor[])) {
                var serverDateRangeArray = serverValue as IgbDateRangeDescriptor[];
                var clientDateArray = JsonConvert.DeserializeObject<IgbDateRangeDescriptor[]>(clientValue);
                if (serverDateRangeArray == null || clientDateArray == null || serverDateRangeArray.Length != clientDateArray.Length) {
                    return false;
                }
                for (int i = 0; i < serverDateRangeArray.Length; i++)
                {
                    if (serverDateRangeArray[i].Type != clientDateArray[i].Type) {
                        return false;
                    }

                    DateTime[] serverDateArray = (DateTime[])serverDateRangeArray[i].DateRange;
                    string[] serverStringArray = serverDateArray.Select(x => x.ToShortDateString()).ToArray();
                    var clientRangeArray = ((JArray)clientDateArray[i].DateRange).ToObject<DateTime[]>();
                    if (clientRangeArray == null)
                    {
                        return false;
                    }
                    string[] clientStringArray = clientRangeArray.Select(x => x.ToShortDateString()).ToArray();
                    serverString = string.Join(",", serverStringArray);
                    clientValue = string.Join(",", clientStringArray);
                }
            }

            // no spcial attribute for data source properties ATM, match by name
            if (prop.Name == "Data" || prop.Name == "DataSource")
            {
                var objRes = JsonConvert.DeserializeObject<List<NwindDataItem>>(clientValue);
                if (serverValue is not NwindData serverData || serverData.Count != objRes?.Count)
                {
                    return false;
                }

            }
            else
            {
                // could be transforming the client value to the the server value type, but this is simpler.
                if (serverString?.Trim('"').ToLower() != clientValue?.Trim('"').ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        public static string EnumActualValue(object serverValue) {
            var enumName = serverValue.ToString();
            if (string.IsNullOrEmpty(enumName))
            {
                return string.Empty;
            }

            FieldInfo? fieldInfo = serverValue.GetType().GetField(enumName);
            var customNameAttr = fieldInfo?.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().Name == "WCEnumNameAttribute");
            if (customNameAttr != null)
            {
                var attrValue = customNameAttr.GetType().GetProperty("Name")?.GetValue(customNameAttr)?.ToString();
                return attrValue ?? enumName.ToLowerInvariant();
            }
            else {
                return enumName.ToLowerInvariant();
            }

        }
    }
}
