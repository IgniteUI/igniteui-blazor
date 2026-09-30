using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    // Sends unmarshalled data (WebAssembly) to the component's client interop module: a synchronous in-process call
    // that passes the address of the local holding the columns array, which the client reads off the heap for the
    // duration of the call. The same path on every target framework (the net8-only InvokeUnmarshalled fast path
    // needed a window global).
    internal class RuntimeHelper
    {
        private readonly IIgniteUIBlazorRuntime? _igBlazor;
        private readonly Func<InteropModule>? _interop;

        public RuntimeHelper(IJSRuntime? runtime, IIgniteUIBlazorRuntime igBlazor, Func<InteropModule>? interop)
        {
            _igBlazor = igBlazor;
            _interop = interop;
            IsInproc = runtime is IJSInProcessRuntime;
        }

        public void SendUnmarshalledColumnMessage(string methodName, string refName, int index, UnmarshalledColumn[]? columns)
        {
            _interop?.Invoke().Post(module => SendUnmarshalledColumnMessageNow(module, methodName, refName, index, columns));
        }

        public void SendUnmarshalledColumnDataIntentsMessage(string methodName, string refName, string dataIntents)
        {
            _interop?.Invoke().Post(module =>
            {
                if (module is IUnmarshalledColumnSink sink)
                {
                    sink.SendDataIntents(methodName, refName, dataIntents);
                }
                else
                {
                    (module as IJSInProcessObjectReference)?.InvokeVoid(methodName, refName, dataIntents);
                }
            });
        }

        private static unsafe void SendUnmarshalledColumnMessageNow(IJSObjectReference module, string methodName, string refName, int index, UnmarshalledColumn[]? columns)
        {
            if (module is IUnmarshalledColumnSink sink)
            {
                sink.SendColumns(methodName, refName, index, columns);
            }
            else if (module is IJSInProcessObjectReference inproc)
            {
                var intptr = Unsafe.AsPointer(ref columns);
                inproc.InvokeVoid(methodName, refName, index, (int)intptr);
            }
        }

        public bool IsInproc { get; private set; }
        public bool IsForcedJsonDataMarshalling { get { return _igBlazor?.Settings?.ForceJsonDataMarshalling ?? false; } }
    }

    /// <summary>
    /// A module that takes the column arrays themselves instead of their address, which only a WebAssembly heap
    /// can resolve. Implemented by test doubles of the interop module.
    /// </summary>
    internal interface IUnmarshalledColumnSink
    {
        void SendColumns(string methodName, string refName, int index, UnmarshalledColumn[]? columns);
        void SendDataIntents(string methodName, string refName, string dataIntents);
    }
}
