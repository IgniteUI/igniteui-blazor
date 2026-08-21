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
        /// Client resource modules to preload, such as <c>typeof(IgbTreeModule)</c> (module types
        /// convert implicitly to <see cref="IgbModuleRef"/>); the types must implement
        /// <see cref="IIgbModule"/>. The typed reference keeps the registration trim-safe.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            params IgbModuleRef[] modulesToLoad)
        {
            // Registrations are resolved immediately, so any later mutation of the caller's array has no effect.
            var modules = new IgbModuleCollection();
            foreach (var moduleRef in modulesToLoad ?? [])
            {
                modules.Add(moduleRef);
            }

            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings();
                    bs = bs.WithModuleRegistrations(modules.Registrations.Count > 0 ? modules.Registrations : null);
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
        /// Modules and registrations the settings already carry are kept, with
        /// <paramref name="modulesToLoad"/> added to them.
        /// </param>
        /// <param name="modulesToLoad">
        /// Client resource modules to preload, such as <c>typeof(IgbTreeModule)</c> (module types
        /// convert implicitly to <see cref="IgbModuleRef"/>); the types must implement
        /// <see cref="IIgbModule"/>. The typed reference keeps the registration trim-safe.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            IIgniteUIBlazorSettings settings,
            params IgbModuleRef[] modulesToLoad)
        {
            var modules = new IgbModuleCollection();
            foreach (var moduleRef in modulesToLoad ?? [])
            {
                modules.Add(moduleRef);
            }

            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings(settings);
                    // The params add to whatever registrations the settings already carry rather than replacing them
                    var merged = (settings?.ModuleRegistrations ?? Enumerable.Empty<Action<IIgniteUIBlazor>>())
                        .Concat(modules.Registrations)
                        .ToList();
                    bs = bs.WithModuleRegistrations(merged.Count > 0 ? merged : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor),
                typeof(IgniteUIBlazor));
        }

        /// <summary>
        /// Registers the Ignite UI Blazor runtime, preloading client resource modules through a
        /// trim-safe module collection.
        /// </summary>
        /// <remarks>
        /// Unlike the <see cref="Type"/>-based overloads, modules added here register through their
        /// static <c>Register</c> method without reflection, so registration keeps working in
        /// applications published with assembly trimming.
        /// </remarks>
        /// <param name="collection">The service collection to add the runtime to.</param>
        /// <param name="configureModules">
        /// Configures the modules to preload, e.g. <c>m => m.Add&lt;IgbTreeModule&gt;()</c>.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            Action<IgbModuleCollection> configureModules)
        {
            var modules = new IgbModuleCollection();
            configureModules?.Invoke(modules);

            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings();
                    bs = bs.WithModuleRegistrations(modules.Registrations.Count > 0 ? modules.Registrations : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor),
                typeof(IgniteUIBlazor));
        }

        /// <summary>
        /// Registers the Ignite UI Blazor runtime with the given settings, preloading client
        /// resource modules through a trim-safe module collection.
        /// </summary>
        /// <remarks>
        /// Unlike the <see cref="Type"/>-based overloads, modules added here register through their
        /// static <c>Register</c> method without reflection, so registration keeps working in
        /// applications published with assembly trimming. Module registrations the settings already
        /// carry are kept, with the configured modules added to them.
        /// </remarks>
        /// <param name="collection">The service collection to add the runtime to.</param>
        /// <param name="settings">
        /// Runtime settings controlling how data is marshalled and serialized to the client.
        /// </param>
        /// <param name="configureModules">
        /// Configures the modules to preload, e.g. <c>m => m.Add&lt;IgbTreeModule&gt;()</c>.
        /// </param>
        /// <returns>The same service collection, so further calls can be chained.</returns>
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddIgniteUIBlazor(this Microsoft.Extensions.DependencyInjection.IServiceCollection collection,
            IIgniteUIBlazorSettings settings,
            Action<IgbModuleCollection> configureModules)
        {
            var modules = new IgbModuleCollection();
            configureModules?.Invoke(modules);

            var s = collection.AddScoped(
                typeof(IIgniteUIBlazorSettings),
                (sp) =>
                {
                    var bs = new IgniteUIBlazorSettings(settings);
                    // The configured modules add to whatever the settings already carry rather than replacing them
                    var merged = (settings?.ModuleRegistrations ?? Enumerable.Empty<Action<IIgniteUIBlazor>>())
                        .Concat(modules.Registrations)
                        .ToList();
                    bs = bs.WithModuleRegistrations(merged.Count > 0 ? merged : null);
                    return bs;
                });

            return s.AddScoped(
                typeof(IIgniteUIBlazor),
                typeof(IgniteUIBlazor));
        }
    }

}
