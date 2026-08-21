namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for the <see cref="IgbListHeader"/> child component of <see cref="IgbList"/>. The parent handles its resources, so registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbListHeaderModule is no longer required, has no effect and can be safely removed.")]
    public partial class IgbListHeaderModule : IIgbModule
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
