using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Blazor.TestBed.WebApp.Components.Common
{
    public static class ReflectionUtils
    {
        // Exluded props as per the component config
        public static List<string> excludedProps = new List<string>() { };
        //properties that depend on other properties to be set, as per the component config
        public static List<string> dependantProps = new List<string>() { };
        //methods that depend on other properties to be set, as per the component config
        public static List<string> dependantMethods = new List<string>() { };

        // gets the full list of valid properties for type
        public static List<PropertyInfo> GetValidProps(Type type)
        {
            List<string> basePropNames = (typeof(BaseRendererControl)).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToList();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            // exclude readonly, broken component specific props, dependant props, props from the base renderer, templates and events
            // exlude Name as it is used as unique identifier, we don't want to dynamically set it
            .Where(x => x.CanWrite && x.Name != "Name" && !excludedProps.Contains(x.Name) && !dependantProps.Contains(x.Name) && !basePropNames.Contains(x.Name) && !x.Name.EndsWith("Script") &&
            !IsEventCallback(x) && !IsRenderFragment(x)).ToList();
            return props;
        }

        public static List<MethodInfo> GetValidMethods(Type componentType)
        {
            MethodInfo[] methodInfos = componentType.GetMethods(BindingFlags.Public | BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName).ToArray();
            var validMethods = methodInfos
             // only async in this env.
            .Where(x => x.Name.EndsWith("Async"))
            // this is not user settable but exist in all classes.
            .Where(x => x.Name != "SetNativeElementAsync" && x.Name != "SetParametersAsync")
            // exclude methods that are just wrappers to get existing props values. They don't actually match with existing client methods. They follow the naming Get{PropName}Async.
            .Where(x => !(x.Name.StartsWith("Get") && x.Name.EndsWith("Async")))
            //exclude methods that depend on some condition, based on component specific configuration.
            .Where(x => !ReflectionUtils.dependantMethods.Contains(x.Name));
            return validMethods.ToList();
        }

            public static string GetActualPropertyName(PropertyInfo propertyInfo)
        {
            var customNameAttr = propertyInfo.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().Name == "WCWidgetMemberNameAttribute");
            // extract actual name from attribute
            var actualName = customNameAttr != null ? (((WCWidgetMemberNameAttribute)customNameAttr).Name) : propertyInfo.Name;
            var clientPropName = ReflectionUtils.Camelize(actualName);
            return clientPropName;
        }

        public static Type GetComponentByType(string type)
        {
            var asm = Assembly.Load("IgniteUI.Blazor.Lite");
            var classes = asm.GetTypes().Where(p =>
                 p.Namespace == "IgniteUI.Blazor.Controls" &&
                  p.Name.StartsWith("Igb")
            ).ToList();

            var match = classes.Find(x => x.Name == type);
            return match;
        }

        public static List<string> IgnoredProps()
        {
            List<string> all = new List<string>();
            List<string> baseRenderedControlPropNames = (typeof(BaseRendererControl)).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToList();
            List<string> baseRendererElementPropNames = (typeof(BaseRendererElement)).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToList();
            all = all.Concat(baseRenderedControlPropNames).ToList();
            all = all.Concat(baseRendererElementPropNames).ToList();
            all = all.Concat(excludedProps).ToList();
            all.Add("Name");
            return all;
        }

        public static bool IsRenderFragment(PropertyInfo info)
        {
            return info.PropertyType.IsGenericType &&
            (info.PropertyType.GetGenericTypeDefinition() == typeof(RenderFragment<object>) ||
            info.PropertyType.GetGenericTypeDefinition() == typeof(RenderFragment<>) ||
            info.PropertyType.GetGenericTypeDefinition() == typeof(RenderFragment)
            );
        }

        public static bool IsEventCallback(PropertyInfo info)
        {
            return info.PropertyType.IsGenericType &&
            (info.PropertyType.GetGenericTypeDefinition() == typeof(EventCallback<object>) ||
            info.PropertyType.GetGenericTypeDefinition() == typeof(EventCallback<>) ||
            info.PropertyType.GetGenericTypeDefinition() == typeof(EventCallback)
            );
        }

        public static List<string> Get2WayBindingEvents(Type componentType)
        {
            var sourceByType = componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(IsEventCallback)
                .Select(x => x.DeclaringType)
                .Where(x => x != null)
                .Distinct()
                .ToDictionary(x => x, GetSourceCodeForType);

            var twoWayBindingEvents = componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(IsEventCallback)
                .Where(x => x.DeclaringType != null)
                .Where(x => EventSetterContainsPropagation(sourceByType[x.DeclaringType], x.Name))
                .Select(x => x.Name)
                .ToList();

            return twoWayBindingEvents;
        }

        private static bool EventSetterContainsPropagation(string source, string eventName)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            var pattern = $@"EventCallback(?:<[^>]+>)?\s+{Regex.Escape(eventName)}\s*\{{[\s\S]*?set\s*\{{[\s\S]*?OnPropertyPropagatedOut\(";
            return Regex.IsMatch(source, pattern, RegexOptions.Singleline);
        }

        private static string GetSourceCodeForType(Type type)
        {
            var componentName = type.Name.Replace("Igb", "");
            if (type == null)
            {
                return string.Empty;
            }

            var repoRoot = FindRepositoryRoot();
            if (string.IsNullOrEmpty(repoRoot))
            {
                return string.Empty;
            }

            var filePath = Path.Combine(repoRoot, "components", "Blazor", componentName + ".cs");
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(repoRoot, "componentsBase", componentName + ".cs");
            }

            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        private static string FindRepositoryRoot()
        {
            var current = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (current != null)
            {
                if (File.Exists(Path.Combine(current.FullName, "IgniteUI.Blazor.Lite.csproj")))
                {
                    return current.FullName;
                }

                current = current.Parent;
            }

            return string.Empty;
        }

        // extract tag name from descriptions metadata
        public static string GetComponentSelector(object component, string name)
        {
            var componentName = name.Replace("Igb", "");
            //kebab cased
            return "igc-" + System.Text.RegularExpressions.Regex.Replace(componentName, "(?<!^)([A-Z])", "-$1").ToLowerInvariant();
        }

        public static object CreateRenderFragmentForType(Type propertyType, object value)
        {
            // Ensure that the type is not null
            if (propertyType == null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            // Get the method that creates RenderFragment<T> using reflection
            var methodInfo = typeof(ReflectionUtils)
                .GetMethod(nameof(CreateTypedRenderFragment), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(propertyType);

            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Cannot find method for creating RenderFragment for type '{propertyType.Name}'.");
            }

            // Invoke the generic method to create RenderFragment<T>
            return methodInfo.Invoke(null, new object[] { value });
        }

        private static RenderFragment<T> CreateTypedRenderFragment<T>(T value)
        {
            return context =>
            {
                return builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, $"Rendering value of type {typeof(T).Name}: {value}");
                    builder.CloseElement();
                };
            };
        }



        public static string Camelize(string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }

        public static string Capitalize(string value) {
            if (value == null || value.Length == 0)
            {
                return value;
            }
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static object GetSimpleType(Type type)
        {
            object value = null;
            if (type == typeof(string))
            {
                value = "parameter";
            }
            else if (type == typeof(bool))
            {
                value = true;
            }
            else if (type == typeof(double))
            {
                value = 100.0;
            }
            else if (type == typeof(DateTime))
            {
                value = (DateTime.Parse("11/11/2011")).ToUniversalTime();
            }
            else if (type == typeof(Dictionary<string, string>))
            {
                value = new Dictionary<string, string>();
            }
            return value;
        }

        public static object GetObjectOfType(Type type, bool isData = false, int depth = 0)
        {
            object value = GetSimpleType(type);

            if (CustomTypes.PredefinedTypes.ContainsKey(type.Name))
            {
                // this is a special complex type with predefined value
                return CustomTypes.PredefinedTypes[type.Name];
            }

            if (type.IsArray)
            {
                if (depth != 0)
                {
                    return null;
                }

                var arrayElem = GetObjectOfType(type.GetElementType(), false, 1);
                if (arrayElem != null)
                {
                    value = Array.CreateInstance(type.GetElementType(), 1);
                    ((Array)value).SetValue(arrayElem, 0);
                }
                else
                {
                    value = Array.CreateInstance(type.GetElementType(), 0);
                }
            }

            if (isData)
            {
                // data source
                value = new NwindData();
            }

            if (value == null)
            {
                value = Activator.CreateInstance(type);
                if (!type.GetProperties().Any() && value is not Enum)
                {
                    return null;
                }
                var props = GetValidProps(type);
                foreach (var p in props)
                {
                    object propValue = GetObjectOfType(p.PropertyType);
                    p.SetValue(value, propValue);
                }
            }

            return value;

        }
    }
}

