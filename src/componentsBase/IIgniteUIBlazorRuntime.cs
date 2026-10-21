using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The runtime behind <see cref="IIgniteUIBlazor"/>; the components and
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
        // Temporary, for apps that register an IIgniteUIBlazor implementation themselves instead of calling
        // AddIgniteUIBlazor, such as the full IgniteUI.Blazor package's deprecated IgniteUIBlazor class.
        private static readonly ConditionalWeakTable<IIgniteUIBlazor, IgniteUIBlazor> _adopted = new();
        private static int _adoptionReported;

        /// <summary>
        /// The single conversion from the public service to the runtime it is implemented by.
        /// Another implementation gets a runtime of its own, one per instance and so one per scope.
        /// </summary>
        internal static IIgniteUIBlazorRuntime AsRuntime(this IIgniteUIBlazor service)
        {
            if (service is IIgniteUIBlazorRuntime runtime)
            {
                return runtime;
            }
            return _adopted.GetValue(service, Adopt);
        }

        private static IgniteUIBlazor Adopt(IIgniteUIBlazor service)
        {
            if (Interlocked.Exchange(ref _adoptionReported, 1) == 0)
            {
                Console.WriteLine(
                    $"Warning: {nameof(IIgniteUIBlazor)} is registered as {service.GetType().FullName} instead of through AddIgniteUIBlazor. " +
                    "This is deprecated and will stop working in a future release; call AddIgniteUIBlazor instead.");
            }
            return new IgniteUIBlazor(service.JsRuntime);
        }
    }
}
