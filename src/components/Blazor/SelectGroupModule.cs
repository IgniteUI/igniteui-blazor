namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for the <see cref="IgbSelectGroup"/> child component of <see cref="IgbSelect"/>. The parent handles its resources, so registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbSelectGroupModule is no longer required, has no effect and can be safely removed.")]
    public partial class IgbSelectGroupModule : IIgbModule
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
