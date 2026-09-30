using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Each Ignite UI package talks to its own client interop module: the components of another package (the full
/// IgniteUI.Blazor) override <c>InteropModulePath</c>, and their traffic and load requests go to that module only.
/// </summary>
public class PerPackageInteropTests : BlazorComponentTestBase
{
    private const string OtherPath = "./_content/IgniteUI.Blazor/other/interop.js";

    private BunitJSInterop LiteModule => ((RendererMessageInteropHarness)Interop).Module;

    [Fact]
    public void Component_WithOwnInteropModule_SendsItsMessagesThere()
    {
        var other = JSInterop.SetupModule(OtherPath);
        other.Setup<bool>("checkReady", _ => true).SetResult(true);
        other.SetupVoid("waitForLoaded", _ => true).SetVoidResult();
        Interop.PrimeReady();

        var otherCut = Render<OtherPackageCalendar>();
        var liteCut = Render<IgbCalendar>();
        var otherId = Interop.ContainerIdOf(otherCut);
        var liteId = Interop.ContainerIdOf(liteCut);
        Interop.MakeReady();

        otherCut.WaitForAssertion(() => Assert.Contains(other.Invocations, i => i.Identifier == "sendMessage" && (string?)i.Arguments[0] == otherId));
        liteCut.WaitForAssertion(() => Assert.Contains(LiteModule.Invocations, i => i.Identifier == "sendMessage" && (string?)i.Arguments[0] == liteId));

        Assert.DoesNotContain(LiteModule.Invocations, i => i.Arguments.Count > 0 && (string?)i.Arguments[0] == otherId);
        Assert.DoesNotContain(other.Invocations, i => i.Arguments.Count > 0 && (string?)i.Arguments[0] == liteId);
    }

    [Fact]
    public void ModuleLoader_TracksLoadRequestsPerInteropModule()
    {
        var other = JSInterop.SetupModule(OtherPath);

        ModuleLoader.Load(IgniteUIBlazor, "SharedNameModule", OtherPath);

        Assert.True(ModuleLoader.IsLoadRequested(IgniteUIBlazor, "SharedNameModule", OtherPath));
        Assert.False(ModuleLoader.IsLoadRequested(IgniteUIBlazor, "SharedNameModule"));
        Assert.Contains(other.Invocations, i => i.Identifier == "requestLoad" && (string?)i.Arguments[0] == "SharedNameModule");
        Assert.DoesNotContain(LiteModule.Invocations, i => i.Identifier == "requestLoad" && (string?)i.Arguments[0] == "SharedNameModule");

        ModuleLoader.Load(IgniteUIBlazor, "SharedNameModule");

        Assert.True(ModuleLoader.IsLoadRequested(IgniteUIBlazor, "SharedNameModule"));
        Assert.Contains(LiteModule.Invocations, i => i.Identifier == "requestLoad" && (string?)i.Arguments[0] == "SharedNameModule");
        Assert.Single(other.Invocations, i => i.Identifier == "requestLoad");
    }

    private sealed class OtherPackageCalendar : IgbCalendar
    {
        private protected override string InteropModulePath => OtherPath;
    }
}
