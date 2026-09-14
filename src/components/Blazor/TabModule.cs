namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for the <see cref="IgbTab"/> child component of <see cref="IgbTabs"/>. The parent handles its resources, so registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbTabModule is no longer required, has no effect and can be safely removed.")]
    [IgbModule<IgbTabModule>]
    public partial class IgbTabModule : IIgbModule
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
