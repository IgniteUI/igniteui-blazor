using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace IgniteUI.Blazor.Tests;

internal static class ThreadPoolSetup
{
    /// <summary>
    /// Components flush their interop queue from a thread-pool work item, while the tests observing
    /// that traffic block a thread-pool thread waiting for it - xUnit runs test cases on the pool.
    /// With the default floor of one thread per core, the collections running at once can hold every
    /// readily available thread, leaving the flush waiting on the pool's slow injection of new ones,
    /// and CI compounds it by running one process per target framework at the same time. Raising the
    /// floor keeps threads available for the flush, so a wait resolves in milliseconds instead of
    /// running out its budget and reporting traffic that was queued but never sent.
    /// </summary>
    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255:The 'ModuleInitializer' attribute should not be used in libraries",
        Justification = "Not a library: this is the test assembly configuring its own run, and the " +
            "floor has to be raised before the first test takes a pool thread.")]
    internal static void RaiseMinimumThreads()
    {
        ThreadPool.GetMinThreads(out var workers, out var completionPorts);
        ThreadPool.SetMinThreads(Math.Max(workers, Environment.ProcessorCount * 8), completionPorts);
    }
}
