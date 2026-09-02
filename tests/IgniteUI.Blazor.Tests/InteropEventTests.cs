using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Shared JS → .NET event-pipeline semantics that per-component contracts don't
/// cover: two-way binding propagation out of a JS-originated event, the delta
/// state-sync it must send back, and dispatch without a bound handler being a
/// no-op. (Per-member dispatch/args/registration coverage lives in each
/// component's contract.)
/// </summary>
public class InteropEventTests : BlazorComponentTestBase
{
    [Fact]
    public void BoolDetailEvent_FromJs_PropagatesTwoWayBinding()
    {
        var selected = false;
        var cut = Render<IgbChip>(parameters => parameters
            .Add(c => c.Select, _ => { })
            .Add(c => c.SelectedChanged, value => selected = value));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", """{"detail": true}""");

        Assert.True(selected);
        Assert.True(cut.Instance.Selected);
    }

    [Fact]
    public void BoolDetailEvent_FromJs_SendsDeltaStateSyncBack()
    {
        var cut = Render<IgbChip>(parameters =>
            parameters.Add(c => c.Select, _ => { }));
        var containerId = Interop.ContainerIdOf(cut);

        Interop.RaiseEvent(containerId, "Select", """{"detail": true}""");

        // Two-way propagation out of the handler must sync the changed state back to JS.
        var delta = Interop.StateSyncs.LastOrDefault(s => s.ContainerId == containerId && s.IsDelta);
        Assert.NotNull(delta);
        Assert.True(delta.State.GetProperty("selected").GetBoolean());
    }

    [Fact]
    public void Event_WithoutBoundHandler_IsIgnored()
    {
        var cut = Render<IgbBanner>();

        // No handler bound — dispatch must be a no-op rather than an error.
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing");
    }

    /// <summary>
    /// Regression coverage for <c>BaseRendererControl.ObserveHandlerTask</c>: an async
    /// <see cref="EventCallback{T}"/> handler that faults *after* an await returns a
    /// task that completes asynchronously. Without observation those exceptions would
    /// be silently dropped by the interop dispatch path (which does not itself await
    /// the returned task). The observer surfaces the fault through the same error
    /// channel as the interop dispatcher — captured here via <see cref="Console.Out"/>.
    /// </summary>
    [Fact]
    public async Task AsyncHandler_FaultingAfterAwait_IsObservedNotSwallowed()
    {
        var gate = new TaskCompletionSource();
        const string faultMessage = "async handler fault after await";

        var cut = Render<IgbChip>(parameters => parameters
            .Add(c => c.Select, _ => { })
            .Add<bool>(c => c.SelectedChanged, async _ =>
            {
                await gate.Task;
                throw new InvalidOperationException(faultMessage);
            }));

        var originalOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            // Dispatch must not throw — the handler is still awaiting the gate,
            // so InvokeAsync returns an incomplete task.
            Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", """{"detail": true}""");

            Assert.DoesNotContain(faultMessage, writer.ToString());

            // Release the async handler; ObserveHandlerTask's continuation runs
            // synchronously (TaskContinuationOptions.ExecuteSynchronously) on the
            // completing thread and must log the fault.
            gate.SetResult();

            // Belt-and-braces: if the continuation is deferred, give it a bounded
            // window so the test stays deterministic without racing forever.
            for (var i = 0; i < 50 && !writer.ToString().Contains(faultMessage); i++)
            {
                await Task.Delay(10);
            }
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        Assert.Contains(faultMessage, writer.ToString());
    }

    /// <summary>
    /// Negative control for <c>ObserveHandlerTask</c>: a successfully-completing
    /// async handler must not write anything to the interop error channel. Guards
    /// against a regression where the observer logs on every completion rather
    /// than only on faults.
    /// </summary>
    [Fact]
    public async Task AsyncHandler_CompletingSuccessfully_IsNotReportedAsError()
    {
        var gate = new TaskCompletionSource();
        var completed = false;

        var cut = Render<IgbChip>(parameters => parameters
            .Add(c => c.Select, _ => { })
            .Add<bool>(c => c.SelectedChanged, async _ =>
            {
                await gate.Task;
                completed = true;
            }));

        var originalOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Select", """{"detail": true}""");
            gate.SetResult();

            // Give any (unwanted) continuation a chance to run.
            for (var i = 0; i < 10 && !completed; i++)
            {
                await Task.Delay(10);
            }
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        Assert.True(completed);
        Assert.Equal(string.Empty, writer.ToString());
    }
}
