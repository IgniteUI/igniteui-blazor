using System.Reflection;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    public class TestUtil
    {
        private static readonly List<string> excluded = new List<string>();
        public static List<string> GetComponentsForTesting()
        {
            var asm = Assembly.Load("IgniteUI.Blazor.Lite");
            var classes = asm.GetTypes().Where(p =>
                  p.Namespace == "IgniteUI.Blazor.Controls" &&
                  p.Name.StartsWith("Igb") &&
                  p.IsSubclassOf(typeof(BaseRendererControl)) &&
                  !p.Name.Contains("Base")
            ).ToList();
            return classes.Select(x => x.IsGenericType ? x.Name[..x.Name.IndexOf('`')] : x.Name).Where(x => !excluded.Contains(x)).ToList();
        }
    }
}
