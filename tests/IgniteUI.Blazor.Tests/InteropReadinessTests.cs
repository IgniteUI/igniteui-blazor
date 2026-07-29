using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Covers the readiness gate: API methods must not reach JS before the component
/// reports ready, and both readiness paths (the natural check during rendering and
/// a later JS "loaded" signal) unlock method invocation.
/// </summary>
public class InteropReadinessTests : BlazorComponentTestBase
{
    [Fact]
    public async Task MethodInvocation_BeforeReady_Throws()
    {
        var cut = RenderComponent<IgbBanner>();

        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.Instance.ShowAsync());
        Assert.Null(Interop.FindCall("show"));
    }

    [Fact]
    public async Task PrimeReady_ReadiesComponent_ThroughNaturalRenderFlow()
    {
        Interop.PrimeReady();
        Interop.SetupMethodResult("show", InteropReturn.Bool(true));
        var cut = RenderComponent<IgbBanner>();

        var shown = await cut.Instance.ShowAsync();

        Assert.True(shown);
        Assert.NotNull(Interop.FindCall("show", Interop.ContainerIdOf(cut)));
    }

    [Fact]
    public async Task MakeReady_ReadiesComponents_RenderedBeforeTheLoadedSignal()
    {
        var cut = RenderComponent<IgbBanner>();
        Interop.SetupMethodResult("hide", InteropReturn.Bool(false));

        Interop.MakeReady();
        var hidden = await cut.Instance.HideAsync();

        Assert.False(hidden);
        Assert.NotNull(Interop.FindCall("hide", Interop.ContainerIdOf(cut)));
    }
}
