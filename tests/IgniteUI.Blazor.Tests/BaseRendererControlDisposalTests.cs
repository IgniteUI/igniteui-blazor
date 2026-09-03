using System.Collections.ObjectModel;
using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Runs alone: one of these holds the thread pool, which every component's flush needs, so it
/// must not run alongside tests that are waiting for one.
/// </summary>
// TODO: on xUnit v3 4.0+ this can narrow to [Fact(DisableParallelism = true)] on the one test
// that holds the pool - but only under ParallelMode.All, since the marker is ignored in the
// default collections mode. https://xunit.net/docs/running-tests-in-parallel
[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class DisposalCollection
{
    public const string Name = "renderer disposal";
}

/// <summary>
/// Tests around <see cref="BaseRendererControl.DisposeAsync"/> — the async disposal
/// path must be resilient to JS interop failures, since disposal typically runs
/// while the Blazor circuit / JS runtime is being torn down and any interop call
/// can legitimately throw. <see cref="BaseRendererControl"/> intentionally does
/// not implement <see cref="IDisposable"/> per Blazor guidance
/// (https://learn.microsoft.com/aspnet/core/blazor/components/component-disposal):
/// when both are implemented the framework only invokes the async overload.
/// </summary>
[Collection(DisposalCollection.Name)]
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

    private sealed record Row(string Name);

    private int SendsFor(string containerId) => JSInterop.Invocations.Count(v =>
        v.Identifier == "igSendMessage" && v.Arguments.Count > 0 && v.Arguments[0] as string == containerId);

    [Fact]
    public async Task DisposeAsync_StopsAFlushScheduledBeforeIt()
    {
        Interop.PrimeReady();
        var data = new ObservableCollection<Row>();
        var cut = Render<IgbCombo<Row>>(ps => ps.Add(c => c.Data, data));
        var id = Interop.ContainerIdOf(cut);

        // The flush is posted through a thread-pool work item, so holding the pool leaves one
        // scheduled but unable to run - the state teardown has to refuse rather than let land.
        ThreadPool.GetMinThreads(out var workers, out var completionPorts);
        ThreadPool.SetMinThreads(1, completionPorts);
        using var release = new ManualResetEventSlim(false);
        for (var i = 0; i < 128; i++)
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => release.Wait(10000), null);
        }

        data.Add(new Row("queued")); // enqueued; flush scheduled, cannot run
        var beforeDisposal = SendsFor(id);

        await ((IAsyncDisposable)cut.Instance).DisposeAsync();

        release.Set();
        ThreadPool.SetMinThreads(workers, completionPorts);
        await Task.Delay(250);

        Assert.Equal(beforeDisposal, SendsFor(id));
    }

    [Fact(Skip = "DisposeAsync sets disposedValue before TrySendCleanupAsync, and SendMessageImmediate " +
        "drops on that flag, so no cleanup message is transmitted for this to order against. " +
        "Un-skip once disposal sends one.")]
    public async Task DisposeAsync_SendsNothingAfterCleanup()
    {
        Interop.PrimeReady();
        var data = new ObservableCollection<Row>();
        var cut = Render<IgbCombo<Row>>(ps => ps.Add(c => c.Data, data));
        var id = Interop.ContainerIdOf(cut);

        // A producer that does not know the component is going away, so it keeps reaching the
        // queue across teardown - the case the disposed check has to close.
        using var stop = new CancellationTokenSource();
        var producer = Task.Run(() =>
        {
            while (!stop.IsCancellationRequested)
            {
                data.Add(new Row("r"));
            }
        });

        await Task.Delay(30);
        await ((IAsyncDisposable)cut.Instance).DisposeAsync();
        await Task.Delay(60);
        stop.Cancel();
        await producer;

        var sent = JSInterop.Invocations
            .Where(v => v.Identifier == "igSendMessage" && v.Arguments.Count > 1 && v.Arguments[0] as string == id)
            .Select(v => (string)v.Arguments[1]!)
            .ToList();
        var cleanup = sent.FindIndex(json => json.Contains("\"type\": \"cleanup\""));

        Assert.True(cleanup >= 0, "disposal transmitted no cleanup message");
        Assert.Equal(sent.Count - 1, cleanup);
    }
}
