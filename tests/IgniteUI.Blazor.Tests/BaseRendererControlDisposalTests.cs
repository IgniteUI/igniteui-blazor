using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests around <see cref="BaseRendererControl.DisposeAsync"/> — the async disposal
/// path must be resilient to JS interop failures, since disposal typically runs
/// while the Blazor circuit / JS runtime is being torn down and any interop call
/// can legitimately throw. <see cref="BaseRendererControl"/> intentionally does
/// not implement <see cref="IDisposable"/> per Blazor guidance
/// (https://learn.microsoft.com/aspnet/core/blazor/components/component-disposal):
/// when both are implemented the framework only invokes the async overload.
/// </summary>
public class BaseRendererControlDisposalTests : BlazorComponentTestBase
{
    [Fact]
    public async Task DisposeAsync_WhenInteropThrowsJSException_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        // Make the cleanup interop call fail the way a mis-behaving JS side would.
        // The most recently registered matching handler wins in bUnit, so this
        // overrides the default "return undefined" answer for igSendMessage.
        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new JSException("simulated JS failure during cleanup"));

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());

        Assert.Null(ex);
    }

    [Fact]
    public async Task DisposeAsync_WhenCircuitDisconnected_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new JSDisconnectedException("circuit gone"));

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());

        Assert.Null(ex);
    }

    [Fact]
    public async Task DisposeAsync_WhenInteropCanceled_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new TaskCanceledException("host shutting down"));

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());

        Assert.Null(ex);
    }

    [Fact]
    public async Task DisposeAsync_WhenObjectAlreadyDisposed_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new ObjectDisposedException("JSRuntime"));

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());

        Assert.Null(ex);
    }

    [Fact]
    public async Task DisposeAsync_WhenUnexpectedExceptionFromInterop_IsSwallowed()
    {
        // The final catch-all in TrySendCleanupAsync should keep DisposeAsync
        // from ever propagating an unexpected failure out of the dispose path.
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new InvalidOperationException("unexpected interop failure"));

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());

        Assert.Null(ex);
    }

    [Fact]
    public async Task DisposeAsync_CalledTwice_IsIdempotent()
    {
        var cut = Render<IgbButton>();
        var instance = (IAsyncDisposable)cut.Instance;

        await instance.DisposeAsync();

        var ex = await Record.ExceptionAsync(async () => await instance.DisposeAsync());
        Assert.Null(ex);
    }
}
