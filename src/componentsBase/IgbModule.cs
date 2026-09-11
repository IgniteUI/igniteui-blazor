using System.Diagnostics.CodeAnalysis;

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
    /// Marks a module class so the trimmer preserves its public methods whenever the type is kept,
    /// keeping the reflective <c>Register</c> lookup working under trimming; unreferenced modules
    /// still trim away. Applied self-referentially, e.g. <c>[IgbModule&lt;IgbTreeModule&gt;]</c>.
    /// A plain class-level <see cref="DynamicallyAccessedMembersAttribute"/> would not work here:
    /// it only activates for instantiated types (or annotated <c>GetType()</c> flows), and module
    /// classes are only ever referenced via <c>typeof</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class IgbModuleAttribute<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TModule> : Attribute where TModule : IIgbModule
    {
    }
}
