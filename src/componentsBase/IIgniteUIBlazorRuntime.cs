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

        /// <summary>The client interop module at <paramref name="path"/>, shared by the components of the runtime.</summary>
        InteropModule GetInteropModule(string path);
    }

    internal static class IgniteUIBlazorRuntimeExtensions
    {
        private static readonly ConditionalWeakTable<IIgniteUIBlazor, IgniteUIBlazor> _adopted = new();

        /// <summary>
        /// The single conversion from the public service to the runtime it is implemented by.
        /// </summary>
        internal static IIgniteUIBlazorRuntime AsRuntime(this IIgniteUIBlazor service)
        {
            if (service is IIgniteUIBlazorRuntime runtime)
            {
                return runtime;
            }
            if (service != null && IsFullPackageRuntime(service.GetType()))
            {
                return _adopted.GetValue(service, s => new IgniteUIBlazor(s.JsRuntime));
            }
            throw new InvalidOperationException(
                $"The registered {nameof(IIgniteUIBlazor)} instance is of type {service?.GetType().Name ?? "null"}, which is not the runtime registered by AddIgniteUIBlazor.");
        }

        // Temporary, while the full IgniteUI.Blazor package deprecates its IgniteUIBlazor class: an app that registers
        // that class instead of calling AddIgniteUIBlazor gets a runtime of its own, one per instance and so one per scope.
        // Matched by name because that package depends on this one.
        private static bool IsFullPackageRuntime(Type type) =>
            type.FullName == "IgniteUI.Blazor.Controls.IgniteUIBlazor" && type.Assembly.GetName().Name == "IgniteUI.Blazor";
    }
}
