namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Obsolete. Registering this module has no effect and is no longer required.
    /// </summary>
    /// <remarks>
    /// <see cref="IgbListHeader"/> is only ever used inside <see cref="IgbList"/>, and the parent's web component registers its children itself, so this module never needed a separate registration.
    /// The type and its members are kept so existing registrations keep compiling; they do nothing.
    /// </remarks>
    [Obsolete("Registering IgbListHeaderModule is not required and has no effect. Remove it from your AddIgniteUIBlazor registration.")]
    public partial class IgbListHeaderModule
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
