using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Covers the JS → .NET event path: payloads originating on the JS side deserialize
/// into the expected event-args types and reach handlers bound via EventCallback
/// parameters, including two-way binding propagation.
/// </summary>
public class InteropEventTests : BlazorComponentTestBase
{
    [Fact]
    public void VoidEvent_FromJs_ReachesBoundHandler()
    {
        IgbVoidEventArgs? received = null;
        var cut = RenderComponent<IgbBanner>(parameters =>
            parameters.Add(b => b.Closing, args => received = args));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing");

        Assert.NotNull(received);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void BoolDetailEvent_FromJs_DeserializesIntoArgs(bool detail)
    {
        IgbComponentBoolValueChangedEventArgs? received = null;
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(c => c.Select, args => received = args));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", $$"""{"detail": {{detail.ToString().ToLowerInvariant()}}}""");

        Assert.NotNull(received);
        Assert.Equal(detail, received!.Detail);
    }

    [Fact]
    public void BoolDetailEvent_FromJs_PropagatesTwoWayBinding()
    {
        var selected = false;
        var cut = RenderComponent<IgbChip>(parameters => parameters
            .Add(c => c.Select, _ => { })
            .Add(c => c.SelectedChanged, value => selected = value));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", """{"detail": true}""");

        Assert.True(selected);
        Assert.True(cut.Instance.Selected);
    }

    [Fact]
    public void Event_WithoutBoundHandler_IsIgnored()
    {
        var cut = RenderComponent<IgbBanner>();

        // No handler bound — dispatch must be a no-op rather than an error.
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing");
    }
}
