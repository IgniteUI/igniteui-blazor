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

    [Fact]
    public async Task PostVoid_WhileImporting_InvokesInCallOrderWithArguments()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");

        interop.PostVoid("first", 1, "a");
        interop.PostVoid("second");
        Assert.Empty(js.Module.Invocations);

        js.CompleteImport();
        await interop.GetAsync();

        Assert.Collection(js.Module.Invocations,
            i => { Assert.Equal("first", i.Identifier); Assert.Equal(new object?[] { 1, "a" }, i.Args); },
            i => { Assert.Equal("second", i.Identifier); Assert.Empty(i.Args!); });
    }

    [Fact]
    public async Task PostVoid_AfterImport_InvokesImmediately()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        var load = interop.GetAsync();
        js.CompleteImport();
        await load;

        interop.PostVoid("send", "msg");

        var invocation = Assert.Single(js.Module.Invocations);
        Assert.Equal("send", invocation.Identifier);
        Assert.Equal(new object?[] { "msg" }, invocation.Args);
    }

    public static TheoryData<string> FailureModes => new TheoryData<string>
    {
        "throws", "faults", "disconnects", "cancels",
    };

    [Theory]
    [MemberData(nameof(FailureModes))]
    public async Task PostVoid_InvocationFailing_DoesNotStopLaterCalls(string failure)
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        js.Module.Behavior = (identifier, _) => identifier == "bad" ? Fail(failure) : default;

        // Queued: dispatched by the import's drain loop.
        interop.PostVoid("bad");
        interop.PostVoid("good", 1);
        js.CompleteImport();
        await interop.GetAsync();
        Assert.NotNull(interop.Loaded);

        // Already loaded: dispatched directly.
        interop.PostVoid("bad");
        interop.PostVoid("good", 2);

        Assert.Equal(new[] { "bad", "good", "bad", "good" }, js.Module.Invocations.Select(i => i.Identifier));
    }

    [Theory]
    [InlineData("disconnects")]
    [InlineData("cancels")]
    public async Task InvokeAndForgetAsync_CircuitGone_CompletesSuccessfully(string failure)
    {
        var module = new FakeModule { Behavior = (_, _) => Fail(failure) };

        var task = InteropModule.InvokeAndForgetAsync(module, "send", new object?[] { "msg" });
        await task;

        Assert.True(task.IsCompletedSuccessfully);
        Assert.Equal("send", Assert.Single(module.Invocations).Identifier);
    }

    [Theory]
    [InlineData("throws")]
    [InlineData("faults")]
    public async Task InvokeAndForgetAsync_OtherFailure_FaultsTheTask(string failure)
    {
        var module = new FakeModule { Behavior = (_, _) => Fail(failure) };

        var task = InteropModule.InvokeAndForgetAsync(module, "send", null);

        await Assert.ThrowsAsync<JSException>(() => task);
        Assert.True(task.IsFaulted);
    }

    [Theory]
    [InlineData("throws")]
    [InlineData("faults")]
    public async Task AwaitedCalls_ClientError_ReportedWithTheCall(string failure)
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        js.Module.Behavior = (_, _) => Fail(failure);
        var import = interop.GetAsync();
        js.CompleteImport();
        await import;

        var voidError = await Assert.ThrowsAsync<JSException>(() => interop.InvokeVoidAsync("send").AsTask());
        var valueError = await Assert.ThrowsAsync<JSException>(() => interop.InvokeAsync<bool>("check").AsTask());

        Assert.Contains("'send'", voidError.Message);
        Assert.Equal("boom", Assert.IsType<JSException>(voidError.InnerException).Message);
        Assert.Contains("'check'", valueError.Message);
        Assert.Equal("boom", Assert.IsType<JSException>(valueError.InnerException).Message);
    }

    [Fact]
    public async Task AwaitedCalls_CircuitGone_ReachesTheCallerUnchanged()
    {
        var js = new FakeJSRuntime();
        var interop = new InteropModule(js, "./m.js");
        js.Module.Behavior = (_, _) => Fail("disconnects");
        var import = interop.GetAsync();
        js.CompleteImport();
        await import;

        var error = await Assert.ThrowsAsync<JSDisconnectedException>(() => interop.InvokeVoidAsync("send").AsTask());

        Assert.Equal("gone", error.Message);
    }

    private static ValueTask Fail(string failure) => failure switch
    {
        "throws" => throw new JSException("boom"),
        "faults" => FaultLater(new JSException("boom")),
        "disconnects" => FaultLater(new JSDisconnectedException("gone")),
        "cancels" => CancelLater(),
        _ => throw new ArgumentOutOfRangeException(nameof(failure)),
    };

    private static async ValueTask FaultLater(Exception ex)
    {
        await Task.Yield();
        throw ex;
    }

    private static async ValueTask CancelLater()
    {
        await Task.Yield();
        throw new TaskCanceledException();
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

        public List<(string Identifier, object?[]? Args)> Invocations { get; } = new();

        // What an invocation does; it may throw synchronously, as a real call can before its task is created.
        public Func<string, object?[]?, ValueTask> Behavior { get; set; } = (_, _) => default;

        public ValueTask DisposeAsync()
        {
            Disposed = true;
            return ValueTask.CompletedTask;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            Invocations.Add((identifier, args));
            return Result<TValue>(Behavior(identifier, args));
        }

        private static async ValueTask<TValue> Result<TValue>(ValueTask call)
        {
            await call;
            return default!;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
            => InvokeAsync<TValue>(identifier, args);
    }
}
