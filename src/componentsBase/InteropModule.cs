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
                return _loading ??= LoadAsync();
            }
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
                    _loading ??= LoadAsync();
                    return;
                }
            }
            call(module);
        }

        internal async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, params object?[]? args)
        {
            var module = _module ?? await GetAsync().ConfigureAwait(false);
            return await module.InvokeAsync<TValue>(identifier, args).ConfigureAwait(false);
        }

        internal async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
        {
            var module = _module ?? await GetAsync().ConfigureAwait(false);
            await module.InvokeVoidAsync(identifier, args).ConfigureAwait(false);
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
                    // Allow a later call to retry (e.g. the circuit was not connected yet); queued calls are dropped,
                    // like calls made on a runtime that could not reach the client.
                    _loading = null;
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
                    if (_pending.Count == 0)
                    {
                        _module = module;
                        break;
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
                    }
                }
            }
            return module;
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
            if (module == null)
            {
                return;
            }
            try
            {
                await module.DisposeAsync().ConfigureAwait(false);
            }
            catch (JSDisconnectedException)
            {
            }
            catch (JSException)
            {
            }
            catch (OperationCanceledException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}
