using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The client interop module of one Ignite UI package, imported through JS isolation (<c>import()</c>)
    /// instead of calling functions on <c>window</c>. One instance per module path and runtime (circuit or app).
    /// </summary>
    /// <remarks>
    /// Calls made while the module is still being imported are queued and dispatched in call order once it
    /// arrives, so fire-and-forget messages keep their relative order, as they did with the window functions.
    /// </remarks>
    internal sealed class InteropModule : IAsyncDisposable
    {
        /// <summary>The interop module of this package.</summary>
        internal const string LitePath = "./_content/IgniteUI.Blazor/interop.js";

        private readonly IJSRuntime _jsRuntime;
        private readonly object _lock = new object();
        private readonly List<Action<IJSObjectReference>> _pending = new List<Action<IJSObjectReference>>();
        private Task<IJSObjectReference>? _loading;
        private volatile IJSObjectReference? _module;
        private bool _disposed;

        internal InteropModule(IJSRuntime jsRuntime, string path)
        {
            _jsRuntime = jsRuntime;
            Path = path;
        }

        internal string Path { get; }

        /// <summary>The module, once imported; <see langword="null"/> while it is still loading.</summary>
        internal IJSObjectReference? Loaded => _module;

        /// <summary>The module for synchronous calls, where .NET runs in the browser and the module is imported.</summary>
        internal IJSInProcessObjectReference? LoadedInProcess => _module as IJSInProcessObjectReference;

        /// <summary>Imports the module, once.</summary>
        internal Task<IJSObjectReference> GetAsync()
        {
            lock (_lock)
            {
                return StartLoading();
            }
        }

        // Under _lock. A load that ended without the module (faulted, or canceled as a JS interop timeout is, e.g.
        // during a reconnect) is replaced, so a later call retries the import; checking the stored task (instead
        // of resetting it from LoadAsync) also covers an import that fails before LoadAsync yields.
        private Task<IJSObjectReference> StartLoading()
        {
            if (_loading == null || (_loading.IsCompleted && !_loading.IsCompletedSuccessfully))
            {
                _loading = LoadAsync();
            }
            return _loading;
        }

        /// <summary>Runs <paramref name="call"/> with the module: now if it is imported, otherwise after the calls queued before it.</summary>
        internal void Post(Action<IJSObjectReference> call)
        {
            IJSObjectReference? module;
            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }
                module = _module;
                if (module == null)
                {
                    _pending.Add(call);
                    StartLoading();
                    return;
                }
            }
            call(module);
        }

        /// <summary>
        /// Posts a call that nothing awaits, such as a message to the client. Its failure when the circuit goes
        /// away is expected and dropped; any other failure stays unobserved, as a discarded task's would.
        /// </summary>
        internal void PostVoid(string identifier, params object?[]? args)
        {
            Post(module => _ = InvokeAndForgetAsync(module, identifier, args));
        }

        // Internal so tests can await the task that PostVoid discards and check which failures are dropped.
        internal static async Task InvokeAndForgetAsync(IJSObjectReference module, string identifier, object?[]? args)
        {
            try
            {
                await module.InvokeVoidAsync(identifier, args).ConfigureAwait(false);
            }
            catch (JSDisconnectedException)
            {
                // The circuit is gone; there is no client left to update.
            }
            catch (OperationCanceledException)
            {
                // The call was canceled with the circuit or runtime.
            }
        }

        // Awaited calls: the caller gets the failure, so it can handle it as its own. A client error is reported
        // with the call that raised it; the circuit going away or a canceled call reaches the caller unchanged.
        internal async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, params object?[]? args)
        {
            var module = _module ?? await GetAsync().ConfigureAwait(false);
            try
            {
                return await module.InvokeAsync<TValue>(identifier, args).ConfigureAwait(false);
            }
            catch (JSException ex)
            {
                throw CallFailed(identifier, ex);
            }
        }

        internal async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
        {
            var module = _module ?? await GetAsync().ConfigureAwait(false);
            try
            {
                await module.InvokeVoidAsync(identifier, args).ConfigureAwait(false);
            }
            catch (JSException ex)
            {
                throw CallFailed(identifier, ex);
            }
        }

        /// <summary>
        /// The failure of client call <paramref name="identifier"/>, naming the call. It is still a
        /// <see cref="JSException"/>, so code that handles client errors keeps handling it, and it keeps the
        /// client's error as its inner exception.
        /// </summary>
        internal static JSException CallFailed(string identifier, JSException error)
        {
            return new JSException($"The Ignite UI client call '{identifier}' failed: {error.Message}", error);
        }

        private async Task<IJSObjectReference> LoadAsync()
        {
            IJSObjectReference module;
            try
            {
                module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", Path).ConfigureAwait(false);
            }
            catch
            {
                lock (_lock)
                {
                    // A later call retries (e.g. the circuit was not connected yet); queued calls are dropped,
                    // like calls made on a runtime that could not reach the client.
                    _pending.Clear();
                }
                throw;
            }

            // Drain in order; publish the module only when nothing is left queued, so a call arriving meanwhile
            // cannot overtake the queued ones.
            while (true)
            {
                Action<IJSObjectReference>[] batch;
                lock (_lock)
                {
                    if (_disposed)
                    {
                        break;
                    }
                    if (_pending.Count == 0)
                    {
                        _module = module;
                        return module;
                    }
                    batch = _pending.ToArray();
                    _pending.Clear();
                }
                foreach (var call in batch)
                {
                    try
                    {
                        call(module);
                    }
                    catch (Exception)
                    {
                        // Fire-and-forget calls: one failing must not keep the rest from being dispatched.
                        // Deliberately broad: an exception escaping here would fault the load before the module
                        // is published, leaving every later call queued forever.
                    }
                }
            }

            // Disposed while importing: the module was never published, so nothing else will release it.
            await ReleaseAsync(module).ConfigureAwait(false);
            throw new ObjectDisposedException(nameof(InteropModule));
        }

        public async ValueTask DisposeAsync()
        {
            IJSObjectReference? module;
            lock (_lock)
            {
                _disposed = true;
                _pending.Clear();
                module = _module;
                _module = null;
            }
            if (module != null)
            {
                await ReleaseAsync(module).ConfigureAwait(false);
            }
        }

        private static async ValueTask ReleaseAsync(IJSObjectReference module)
        {
            try
            {
                await module.DisposeAsync().ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is JSDisconnectedException or JSException
                                          or OperationCanceledException or ObjectDisposedException)
            {
                // Best-effort disposal during teardown; the client or runtime may already be gone.
            }
        }
    }
}
