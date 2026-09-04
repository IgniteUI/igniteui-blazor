using System.Collections.ObjectModel;
using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Thread-safety of the queue a component sends its interop through. A component reaches that
/// queue from wherever it is driven from, and two paths collide without any misuse: change
/// notifications from bound data run on whichever thread mutated it, while the renderer flushes.
/// </summary>
public class InteropThreadingTests : BlazorComponentTestBase
{
    private sealed record Row(string Name);

    private const int Rows = 20000;

    [Fact]
    public async Task BoundCollection_FilledFromABackgroundTask_DoesNotTearTheMessageQueue()
    {
        Interop.PrimeReady();
        var data = new ObservableCollection<Row>();
        var cut = Render<IgbCombo<Row>>(ps => ps.Add(c => c.Data, data));

        // One writer, so the collection itself is never used concurrently: only the component's
        // queue sees two threads - these change notifications and the renderer's flush. Filling
        // bound data from a background load is ordinary, and it used to throw out of the queue.
        await Task.Run(() =>
        {
            for (var i = 0; i < Rows; i++)
            {
                data.Add(new Row($"row-{i}"));
            }
        });

        // Asserted on what the client received, not on the collection we just filled: tearing the
        // queue drops and duplicates notifications as readily as it throws, and the producer's own
        // count shows neither. Only the renderer drains here, so this says nothing about two
        // drains interleaving - that is what holding the lock across the whole drain is for.
        Assert.Equal(Enumerable.Range(0, Rows), Interop.DataItemInsertions(Interop.ContainerIdOf(cut), Rows));
    }

    [Fact]
    public async Task ConcurrentApiCalls_KeepInvocationBookkeepingIntact()
    {
        Interop.PrimeReady();
        Interop.SetupMethodResult("show", InteropReturn.Bool(true));
        var cut = Render<IgbSnackbar>();

        // Unsynchronised, concurrent calls corrupt the maps pairing an invocation with its return.
        // Sending from eight threads is only safe here because the queue's lock also covers the
        // send, which is where bUnit records the invocation - see InteropHarness.OnDispatcher.
        await Task.WhenAll(Enumerable.Range(0, 8).Select(_ => Task.Run(async () =>
        {
            for (var i = 0; i < 2000; i++)
            {
                Assert.True(await cut.Instance.ShowAsync());
            }
        })));

        // Every call also has to have been given an id of its own: two sharing one collide in
        // those maps, and each still completes its own local task, so nothing above would notice.
        var ids = Interop.CallsOf("show", Interop.ContainerIdOf(cut)).Select(c => c.InvokeId).ToList();
        Assert.Equal(8 * 2000, ids.Count);
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }
}
