using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A client resource module that can register itself with the Ignite UI Blazor runtime.
    /// Implemented by the component <c>*Module</c> classes.
    /// </summary>
    public interface IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        static abstract void Register(IIgniteUIBlazor runtime);
    }

    /// <summary>
    /// A reference to an <see cref="IIgbModule"/> type for <c>AddIgniteUIBlazor</c>. Converts
    /// implicitly from <see cref="Type"/>, so call sites keep the familiar
    /// <c>AddIgniteUIBlazor(typeof(IgbTreeModule))</c> shape, while the conversion's annotated
    /// parameter lets the trimmer preserve the module's registration surface per call site.
    /// </summary>
    public readonly struct IgbModuleRef
    {
        /// <summary>
        /// The module type; always implements <see cref="IIgbModule"/>.
        /// </summary>
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
        public Type ModuleType { get; }

        /// <summary>
        /// Creates a reference to the given module type.
        /// </summary>
        /// <exception cref="ArgumentException">The type does not implement <see cref="IIgbModule"/>.</exception>
        public IgbModuleRef([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type moduleType)
        {
            if (moduleType == null)
            {
                throw new ArgumentNullException(nameof(moduleType));
            }
            if (!typeof(IIgbModule).IsAssignableFrom(moduleType))
            {
                throw new ArgumentException($"Module type '{moduleType}' must implement {nameof(IIgbModule)}.", nameof(moduleType));
            }
            ModuleType = moduleType;
        }

        /// <summary>
        /// Converts a module <see cref="Type"/> (e.g. <c>typeof(IgbTreeModule)</c>) to a module reference.
        /// </summary>
        public static implicit operator IgbModuleRef([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type moduleType)
        {
            return new IgbModuleRef(moduleType);
        }
    }

    /// <summary>
    /// Collects module registrations for <c>AddIgniteUIBlazor</c>. Modules added here register
    /// through their static <see cref="IIgbModule.Register"/> method without reflection, so the
    /// registration survives assembly trimming.
    /// </summary>
    public sealed class IgbModuleCollection
    {
        private readonly List<Action<IIgniteUIBlazor>> _registrations = new List<Action<IIgniteUIBlazor>>();

        internal IReadOnlyList<Action<IIgniteUIBlazor>> Registrations => _registrations;

        /// <summary>
        /// Adds a client resource module to preload, such as <c>IgbTreeModule</c>.
        /// </summary>
        /// <returns>The same collection, so further calls can be chained.</returns>
        public IgbModuleCollection Add<T>() where T : IIgbModule
        {
            _registrations.Add(static runtime => T.Register(runtime));
            return this;
        }

        /// <summary>
        /// Adds a client resource module to preload by reference, e.g. <c>m.Add(typeof(IgbTreeModule))</c>.
        /// The module's static <c>Register</c> is resolved once, when the module is added.
        /// </summary>
        /// <returns>The same collection, so further calls can be chained.</returns>
        public IgbModuleCollection Add(IgbModuleRef module)
        {
            var register = module.ModuleType.GetMethod(nameof(IIgbModule.Register), BindingFlags.Public | BindingFlags.Static);
            if (register == null)
            {
                // Unreachable for implicitly-implemented IIgbModule types (enforced by IgbModuleRef);
                // guards against explicit interface implementations.
                throw new ArgumentException($"Module type '{module.ModuleType}' has no public static {nameof(IIgbModule.Register)} method.", nameof(module));
            }
            _registrations.Add(runtime => register.Invoke(null, new object[] { runtime }));
            return this;
        }
    }
}
