using IgniteUI.Blazor.Controls;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Tests;

public class InteropModuleTests
{
    [Fact]
    public async Task Post_WhileImporting_DispatchesInCallOrder()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        var calls = new List<int>();

        interop.Post(_ => calls.Add(1));
        interop.Post(_ => calls.Add(2));
        js.CompleteImport();
        await interop.GetAsync();
        interop.Post(_ => calls.Add(3));

        Assert.Equal(new[] { 1, 2, 3 }, calls);
    }

    [Fact]
    public async Task Post_DispatchFailing_DoesNotKeepLaterCallsQueued()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        var calls = new List<int>();

        interop.Post(_ => throw new JSException("boom"));
        interop.Post(_ => calls.Add(2));
        js.CompleteImport();
        await interop.GetAsync();

        Assert.Equal(new[] { 2 }, calls);
    }

    [Fact]
    public async Task GetAsync_AfterImportFailedSynchronously_Retries()
    {
        // Prerendering / not-yet-connected circuit: the import fails without ever yielding.
        var js = new FakeJSRuntime { FailNextImportSynchronously = true };
        var interop = new InteropModule(js, "./m.js");

        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => interop.GetAsync());

        var retry = interop.GetAsync();
        js.CompleteImport();
        Assert.Same(js.Module, await retry);
    }

    [Fact]
    public async Task GetAsync_AfterImportTimedOut_Retries()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");

        var first = interop.GetAsync();
        js.TimeOutImport();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => first);
        Assert.True(first.IsCanceled);

        var retry = interop.GetAsync();
        js.CompleteImport();
        Assert.Same(js.Module, await retry);
    }

    [Fact]
    public async Task Post_AfterImportFailedSynchronously_IsDispatchedOnRetry()
    {
        var js = new FakeJSRuntime { FailNextImportSynchronously = true };
        var interop = new InteropModule(js, "./m.js");
        var calls = new List<int>();

        interop.Post(_ => calls.Add(1));
        interop.Post(_ => calls.Add(2));
        js.CompleteImport();
        await Task.Yield();

        Assert.Equal(new[] { 2 }, calls);
    }

    [Fact]
    public async Task DisposeAsync_WhileImporting_DisposesTheModuleOnceItArrives()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        var load = interop.GetAsync();

        await interop.DisposeAsync();
        js.CompleteImport();
        await Assert.ThrowsAsync<ObjectDisposedException>(() => load);
        Assert.True(js.Module.Disposed);
        Assert.Null(interop.Loaded);
    }

    private sealed class FakeJSRuntime : IJSRuntime
    {
        private TaskCompletionSource<IJSObjectReference>? _import;

        public bool FailNextImportSynchronously { get; set; }

        public FakeModule Module { get; } = new FakeModule();

        public void CompleteImport() => _import!.SetResult(Module);

        // How JS interop reports a timeout (e.g. during a reconnect): the call is canceled, not faulted.
        public void TimeOutImport() => _import!.SetCanceled();

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            Assert.Equal("import", identifier);
            if (FailNextImportSynchronously)
            {
                FailNextImportSynchronously = false;
                return ValueTask.FromException<TValue>(new InvalidOperationException("JavaScript interop calls cannot be issued at this time."));
            }
            _import = new TaskCompletionSource<IJSObjectReference>(TaskCreationOptions.RunContinuationsAsynchronously);
            return Import<TValue>(_import.Task);
        }

        // Awaited rather than continued, so a canceled import stays canceled instead of turning into a fault.
        private static async ValueTask<TValue> Import<TValue>(Task<IJSObjectReference> import) => (TValue)await import;

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
            => InvokeAsync<TValue>(identifier, args);
    }

    private sealed class FakeModule : IJSObjectReference
    {
        public bool Disposed { get; private set; }

        public ValueTask DisposeAsync()
        {
            Disposed = true;
            return ValueTask.CompletedTask;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) => default;

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => default;
    }
}
