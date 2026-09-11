using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class DynamicContentHolderTests
{
    [Fact]
    public async Task TypedDynamicContent_WhenComponentIsSetAfterGetInstanceAsync_ReturnsCurrentComponent()
    {
        var content = new TypedDynamicContent(typeof(TestComponent))
        {
            ControlType = typeof(TestComponent)
        };
        var task = content.GetInstanceAsync();

        var component = new TestComponent();
        content.Component = component;

        Assert.Same(component, await task);
    }

    [Fact]
    public async Task TypedDynamicContent_WhenComponentChangesToNull_FaultsPendingTask()
    {
        var content = new TypedDynamicContent(typeof(TestComponent))
        {
            ControlType = typeof(TestComponent)
        };
        var task = content.GetInstanceAsync();

        content.Component = null;

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => task);
        Assert.Equal("Component is null.", exception.Message);
    }

    private sealed class TestComponent
    {
    }
}
