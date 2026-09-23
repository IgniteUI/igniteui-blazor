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

    [Fact]
    public async Task ReturnDeliveredBeforeTheCallRegisters_StillCompletesIt()
    {
        Interop.PrimeReady();
        // Held open so the return arrives while the call is still suspended on its send, with
        // nothing registered for it yet; the promise reply then sends it looking for a stored one.
        var reply = Interop.WithholdMethodReply("toggle");
        var cut = Render<IgbBanner>();

        var pending = Interop.OnDispatcher(cut.Instance.ToggleAsync);
        Interop.CompleteDeferred(Interop.RequireCall("toggle"), InteropReturn.Bool(true));
        reply(InteropReturn.Deferred);

        // A dropped return never completes, so this is bounded to fail rather than hang the run.
        Assert.True(
            await Task.WhenAny(pending, Task.Delay(TimeSpan.FromSeconds(10))) == pending,
            "the call never completed - the return that arrived before it registered was dropped");
        Assert.True(await pending);
    }
}
