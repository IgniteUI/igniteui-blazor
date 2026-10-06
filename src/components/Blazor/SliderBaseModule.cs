namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for <see cref="IgbSliderBase"/>. Registering this module has no effect and is no longer required.
    /// </summary>
    /// Provides the module for <see cref="IgbSliderBase"/>. Registering this module has no effect and is no longer required.
    [IgbModule<IgbSliderBaseModule>]
    public partial class IgbSliderBaseModule : IIgbModule
    {
        /// <summary>
        /// No-op.
        /// </summary>
        public static void Register(IIgniteUIBlazor runtime)
        {
        }

        internal static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
        }

        internal static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return true;
        }
    }
}
