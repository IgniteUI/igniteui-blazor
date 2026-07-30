using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Shared JS → .NET event-pipeline semantics that per-component contracts don't
/// cover: two-way binding propagation out of a JS-originated event, the delta
/// state-sync it must send back, and dispatch without a bound handler being a
/// no-op. (Per-member dispatch/args/registration coverage lives in each
/// component's contract.)
/// </summary>
public class InteropEventTests : BlazorComponentTestBase
{
    [Fact]
    public void BoolDetailEvent_FromJs_PropagatesTwoWayBinding()
    {
        var selected = false;
        var cut = Render<IgbChip>(parameters => parameters
            .Add(c => c.Select, _ => { })
            .Add(c => c.SelectedChanged, value => selected = value));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", """{"detail": true}""");

        Assert.True(selected);
        Assert.True(cut.Instance.Selected);
    }

    [Fact]
    public void BoolDetailEvent_FromJs_SendsDeltaStateSyncBack()
    {
        var cut = Render<IgbChip>(parameters =>
            parameters.Add(c => c.Select, _ => { }));
        var containerId = Interop.ContainerIdOf(cut);

        Interop.RaiseEvent(containerId, "Select", """{"detail": true}""");

        // Two-way propagation out of the handler must sync the changed state back to JS.
        var delta = Interop.StateSyncs.LastOrDefault(s => s.ContainerId == containerId && s.IsDelta);
        Assert.NotNull(delta);
        Assert.True(delta.State.GetProperty("selected").GetBoolean());
    }

    [Fact]
    public void Event_WithoutBoundHandler_IsIgnored()
    {
        var cut = Render<IgbBanner>();

        // No handler bound — dispatch must be a no-op rather than an error.
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing");
    }
}
