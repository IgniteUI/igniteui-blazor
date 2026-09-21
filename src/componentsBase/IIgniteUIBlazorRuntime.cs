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
        void RequestLoad(string moduleName);
        bool IsLoadRequested(string moduleName);
        void MarkIsLoadRequested(string moduleName);
        bool IsRuntimeValid(bool reevaluate = false);
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
