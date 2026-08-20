namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for <see cref="IgbCheckboxBase"/>. Registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbCheckboxBaseModule is no longer required, has no effect and can be safely removed.")]
    public partial class IgbCheckboxBaseModule
    {
        /// <summary>
        /// No-op.
        /// </summary>
        public static void Register(IIgniteUIBlazor runtime)
        {
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return true;
        }
    }
}
