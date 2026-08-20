using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests around <see cref="BaseRendererControl.DisposeAsync"/> — the async disposal
/// path must be resilient to JS interop failures, since disposal typically runs
/// while the Blazor circuit / JS runtime is being torn down and any interop call
/// can legitimately throw.
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

    [Fact]
    public void Dispose_WhenInteropThrowsJSException_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new JSException("simulated JS failure during cleanup"));

        var ex = Record.Exception(() => ((IDisposable)instance).Dispose());
        Assert.Null(ex);
    }

    [Fact]
    public void Dispose_WhenCircuitDisconnected_DoesNotThrow()
    {
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new JSDisconnectedException("circuit gone"));

        var ex = Record.Exception(() => ((IDisposable)instance).Dispose());
        Assert.Null(ex);
    }

    [Fact]
    public void Dispose_WhenUnexpectedExceptionFromInterop_IsSwallowed()
    {
        // The synchronous path is fire-and-forget; the catch-all inside
        // TrySendCleanupAsync must prevent an UnobservedTaskException from ever
        // being surfaced by the ignored Task.
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        JSInterop.Setup<object>("igSendMessage", _ => true)
            .SetException(new InvalidOperationException("unexpected interop failure"));

        var ex = Record.Exception(() => ((IDisposable)instance).Dispose());
        Assert.Null(ex);
    }

    [Fact]
    public void Dispose_CalledTwice_IsIdempotent()
    {
        var cut = Render<IgbButton>();
        var instance = (IDisposable)cut.Instance;

        instance.Dispose();

        var ex = Record.Exception(() => instance.Dispose());
        Assert.Null(ex);
    }

    [Fact]
    public async Task Dispose_ThenDisposeAsync_IsIdempotent()
    {
        // Sync-then-async: the async path must observe the disposedValue flag
        // set by Dispose and return without re-entering cleanup.
        var cut = Render<IgbButton>();
        var instance = cut.Instance;

        ((IDisposable)instance).Dispose();

        var ex = await Record.ExceptionAsync(async () =>
            await ((IAsyncDisposable)instance).DisposeAsync());
        Assert.Null(ex);
    }

}
