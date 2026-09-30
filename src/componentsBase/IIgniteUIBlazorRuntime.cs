using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The runtime behind the <see cref="IIgniteUIBlazor"/> marker; the components and
    /// <see cref="ModuleLoader"/> reach it through <see cref="IgniteUIBlazorRuntimeExtensions.AsRuntime"/>.
    /// </summary>
    internal interface IIgniteUIBlazorRuntime
    {
        IJSRuntime JsRuntime { get; }
        IIgniteUIBlazorSettings? Settings { get; }
        WebCallback WebCallback { get; }
        // Load requests are tracked per interop module: each Ignite UI package loads its client modules through its own.
        void RequestLoad(string moduleName, string interopModulePath = InteropModule.LitePath);
        bool IsLoadRequested(string moduleName, string interopModulePath = InteropModule.LitePath);
        void MarkIsLoadRequested(string moduleName, string interopModulePath = InteropModule.LitePath);
        bool IsRuntimeValid(bool reevaluate = false);

        /// <summary>The client interop module at <paramref name="path"/>, shared by the components of the runtime.</summary>
        InteropModule GetInteropModule(string path);
    }

    internal static class IgniteUIBlazorRuntimeExtensions
    {
        /// <summary>
        /// The single conversion from the public marker to the runtime it is implemented by.
        /// </summary>
        internal static IIgniteUIBlazorRuntime AsRuntime(this IIgniteUIBlazor service)
        {
            return service as IIgniteUIBlazorRuntime
                ?? throw new InvalidOperationException(
                    $"The registered {nameof(IIgniteUIBlazor)} instance is of type {service?.GetType().Name ?? "null"}, which is not the runtime registered by AddIgniteUIBlazor.");
        }
    }
}
