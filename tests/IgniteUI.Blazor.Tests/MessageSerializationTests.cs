using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Round-trips the serialization surface that crosses the interop boundary as
/// *messages* (component state descriptions and their deltas) — as opposed to the
/// rendered-attribute output covered by the existing attribute/enum tests.
/// Asserted structurally via JSON properties, not full-string compares.
/// </summary>
public class MessageSerializationTests : BlazorComponentTestBase
{
    [Fact]
    public void SerializedState_ContainsOnlyTouchedProps_CamelCased()
    {
        var cut = RenderComponent<IgbCarousel>(parameters => parameters
            .Add(c => c.Vertical, true)
            .Add(c => c.Interval, 5000.0));

        var state = Interop.SerializeState(cut.Instance);

        Assert.Equal("mainControl", state.GetProperty("name").GetString());
        Assert.Equal("WebCarousel", state.GetProperty("type").GetString());
        Assert.True(state.GetProperty("vertical").GetBoolean());
        Assert.Equal(5000.0, state.GetProperty("interval").GetDouble());
        // Untouched properties stay out of the payload entirely.
        Assert.False(state.TryGetProperty("hideNavigation", out _));
    }

    [Fact]
    public void SerializedState_WritesCamelCaseEnumWireValues()
    {
        var cut = RenderComponent<IgbCarousel>(parameters =>
            parameters.Add(c => c.AnimationType, HorizontalTransitionAnimation.Fade));

        var state = Interop.SerializeState(cut.Instance);

        Assert.Equal("fade", state.GetProperty("animationType").GetString());
    }

    [Fact]
    public void SerializedState_WritesWCEnumNameWireValues()
    {
        var cut = RenderComponent<IgbButtonGroup>(parameters =>
            parameters.Add(c => c.Selection, ButtonGroupSelection.SingleRequired));

        var state = Interop.SerializeState(cut.Instance);

        Assert.Equal("single-required", state.GetProperty("selection").GetString());
    }

    [Fact]
    public void PropertyPropagation_FromJsEvent_SendsDeltaStateSync()
    {
        var cut = RenderComponent<IgbChip>(parameters =>
            parameters.Add(c => c.Select, _ => { }));
        var containerId = Interop.ContainerIdOf(cut);

        Interop.RaiseEvent(containerId, "Select", """{"detail": true}""");

        var delta = Interop.StateSyncs.LastOrDefault(s => s.ContainerId == containerId && s.IsDelta);
        Assert.NotNull(delta);
        Assert.True(delta!.State.GetProperty("selected").GetBoolean());
    }

    [Fact]
    public async Task ReadyComponent_SendsFullStateDescription()
    {
        Interop.PrimeReady();
        var cut = RenderComponent<IgbChat>();
        var containerId = Interop.ContainerIdOf(cut);

        // Any immediate interop call deterministically flushes the queued message pipeline.
        await cut.Instance.ScrollToMessageAsync("flush");

        var full = Interop.StateSyncs.FirstOrDefault(s => s.ContainerId == containerId && !s.IsDelta);
        Assert.NotNull(full);
        Assert.Equal("WebChat", full!.State.GetProperty("type").GetString());
        Assert.Equal("mainControl", full.State.GetProperty("name").GetString());
    }
}
