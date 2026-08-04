using System.Collections.ObjectModel;
using IgniteUI.Blazor.Controls;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods that add the Ignite UI Blazor runtime to a service collection.
    /// </summary>
    public static class InfragisticsBlazorExtensions
    {

        /// <summary>
        /// Registers the Ignite UI Blazor runtime, optionally preloading client resource modules.
        /// </summary>
        /// <remarks>
        /// For a component's own resources, listing is optional: the component requests its module
        /// the first time it renders, and a module can also be requested later by calling its own
        /// <c>Register</c> method with the runtime. Preload them here to have the client resources
        /// ready during application startup, or for components that are only created dynamically
        /// on the client.
        /// <br />
        /// Modules that are not a single component's own resources — optional features layered onto
        /// a component, or bundles pulling in additional ones — load only when listed or registered
        /// explicitly, since no component requests them on its own.
        /// </remarks>
        /// <param name="collection">The service collection to add the runtime to.</param>
        /// <param name="modulesToLoad">
        /// Client resource module types to preload, such as <see cref="IgbTreeModule"/>.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            params Type[] modulesToLoad)
        {
            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings();
                    bs = bs.WithModulesToLoad(modulesToLoad != null && modulesToLoad.Length > 0 ? new ReadOnlyCollection<Type>(modulesToLoad) : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor),
                typeof(IgniteUIBlazor));
        }

        /// <summary>
        /// Registers the Ignite UI Blazor runtime with the given settings, optionally preloading
        /// client resource modules.
        /// </summary>
        /// <remarks>
        /// For a component's own resources, listing is optional: the component requests its module
        /// the first time it renders, and a module can also be requested later by calling its own
        /// <c>Register</c> method with the runtime. Preload them here to have the client resources
        /// ready during application startup, or for components that are only created dynamically
        /// on the client.
        /// <br />
        /// Modules that are not a single component's own resources — optional features layered onto
        /// a component, or bundles pulling in additional ones — load only when listed or registered
        /// explicitly, since no component requests them on its own.
        /// </remarks>
        /// <param name="collection">The service collection to add the runtime to.</param>
        /// <param name="settings">
        /// Runtime settings controlling how data is marshalled and serialized to the client.
        /// Modules the settings already carry are kept, with <paramref name="modulesToLoad"/> added
        /// to them.
        /// </param>
        /// <param name="modulesToLoad">
        /// Client resource module types to preload, such as <see cref="IgbTreeModule"/>.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            IIgniteUIBlazorSettings settings,
            params Type[] modulesToLoad)
        {
            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings(settings);
                    // The params add to whatever the settings already carry rather than replacing them
                    Type[] modules = [.. (settings?.ModulesToLoad ?? Enumerable.Empty<Type>())
                        .Concat(modulesToLoad ?? Enumerable.Empty<Type>())
                        .Distinct()];
                    bs = bs.WithModulesToLoad(modules.Length > 0 ? new ReadOnlyCollection<Type>(modules) : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor),
                typeof(IgniteUIBlazor));
        }
    }

}
