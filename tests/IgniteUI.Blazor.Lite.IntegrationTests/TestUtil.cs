using IgniteUI.Blazor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    public class TestUtil
    {
        private static List<string> excluded = new List<string>();
        public static List<string> GetComponentsForTesting()
        {
            var result = new List<string>();
            var asm = Assembly.Load("IgniteUI.Blazor.Lite");
            var classes = asm.GetTypes().Where(p =>
                  p.Namespace == "IgniteUI.Blazor.Controls" &&
                  p.Name.StartsWith("Igb") &&
                  p.IsSubclassOf(typeof(BaseRendererControl)) &&
                  !p.Name.Contains("Base")
            ).ToList();
            foreach ( var c in classes) {
                try
                {
                    var instance = Activator.CreateInstance(c);
                    var a = c.GetProperty("Type")?.GetValue(instance)?.ToString()?.StartsWith("Web");
                    if (a == true)
                    {
                        result.Add(c.Name);
                    }
                }
                catch (Exception)
                {
                }
            }
           

            return result.Where(x => !excluded.Contains(x)).ToList();
        }
    }
}
