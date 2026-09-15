using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Semantics of the shared method-invocation pipeline that per-component contracts
/// don't exercise: contracts stub replies that already carry the value, so the
/// deferred return path — the reply says the result comes later, and a separate
/// completion message delivers it — is proven here. (Identifier/argument/return-decoding
/// coverage, sync twins included, lives in each component's
/// <see cref="ComponentContract{TComponent}"/>.)
/// </summary>
public class MethodInteropTests : BlazorComponentTestBase
{
    [Fact]
    public async Task DeferredReturn_StaysPendingUntilJsDeliversTheResult()
    {
        Interop.PrimeReady();
        Interop.SetupMethodResult("toggle", InteropReturn.Deferred);
        var cut = Render<IgbBanner>();

        var pending = Interop.OnDispatcher(cut.Instance.ToggleAsync);
        Assert.False(pending.IsCompleted);

        Interop.CompleteDeferred(Interop.RequireCall("toggle"), InteropReturn.Bool(true));

        Assert.True(await pending);
    }
}
