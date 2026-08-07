namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Obsolete. Registering this module has no effect and is no longer required.
    /// </summary>
    /// <remarks>
    /// <see cref="IgbCheckboxBase"/> is a shared base class, not a component you place in markup - it has no client resources of its own. Everything <see cref="IgbCheckbox"/> or <see cref="IgbSwitch"/> needs is loaded by its own module.
    /// The type and its members are kept so existing registrations keep compiling; they do nothing.
    /// </remarks>
    [Obsolete("Registering IgbCheckboxBaseModule is not required and has no effect. Remove it from your AddIgniteUIBlazor registration.")]
    public partial class IgbCheckboxBaseModule
    {
        /// <summary>
        /// No-op. Kept for source compatibility.
        /// </summary>
        /// <param name="runtime">Unused.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
        }

        /// <summary>
        /// No-op. Kept for source compatibility.
        /// </summary>
        /// <param name="runtime">Unused.</param>
        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
        }

        /// <summary>
        /// Always returns <c>true</c> - there is nothing for this module to load.
        /// </summary>
        /// <param name="runtime">Unused.</param>
        /// <returns><c>true</c>.</returns>
        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return true;
        }
    }
}
