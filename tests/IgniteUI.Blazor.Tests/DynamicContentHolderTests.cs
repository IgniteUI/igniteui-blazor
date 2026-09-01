using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DynamicContentHolderTests
{
    [Fact]
    public async Task TypedDynamicContent_GetInstanceAsync_CompletesWithCurrentComponent()
    {
        var content = new TypedDynamicContent(typeof(string)) { ControlType = typeof(string) };
        var pending = content.GetInstanceAsync();
        var instance = new object();

        content.Component = instance;

        Assert.Same(instance, await pending);
    }

    [Fact]
    public async Task TypedDynamicContent_GetInstanceAsync_WhenComponentIsCleared_CompletesWithNull()
    {
        var content = new TypedDynamicContent(typeof(string)) { ControlType = typeof(string) };
        var pending = content.GetInstanceAsync();

        content.Component = null;

        Assert.Null(await pending);
    }
}
