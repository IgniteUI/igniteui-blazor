using System.Collections.ObjectModel;
using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Thread-safety of the queue a component sends its interop through. A component reaches that
/// queue from wherever it is driven from, and two paths collide without any misuse: change
/// notifications from bound data run on whichever thread mutated it, while the renderer flushes.
/// </summary>
public class InteropThreadingTests : BlazorComponentTestBase
{
    private sealed record Row(string Name);

    [Fact]
    public async Task BoundCollection_FilledFromABackgroundTask_DoesNotTearTheMessageQueue()
    {
        Interop.PrimeReady();
        var data = new ObservableCollection<Row>();
        Render<IgbCombo<Row>>(ps => ps.Add(c => c.Data, data));

        // One writer, so the collection itself is never used concurrently: only the component's
        // queue sees two threads - these change notifications and the renderer's flush. Filling
        // bound data from a background load is ordinary, and it used to throw out of the queue.
        await Task.Run(() =>
        {
            for (var i = 0; i < 20000; i++)
            {
                data.Add(new Row($"row-{i}"));
            }
        });

        Assert.Equal(20000, data.Count);
    }
}
