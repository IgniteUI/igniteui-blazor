using System.Reflection;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class LifecycleWrapperApiTests
{
    [Fact]
    public void ComponentApis_DoNotExposeBrowserLifecycleCallbacks()
    {
        var lifecycleMethodNames = new HashSet<string>(StringComparer.Ordinal)
        {
            "ConnectedCallbackAsync",
            "ConnectedCallback",
            "DisconnectedCallbackAsync",
            "DisconnectedCallback"
        };

        var lifecycleWrappers = typeof(IgbTree).Assembly.ExportedTypes
            .Where(type => type.IsClass && typeof(BaseRendererControl).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            .Where(method => lifecycleMethodNames.Contains(method.Name));

        Assert.Empty(lifecycleWrappers);
    }
}
