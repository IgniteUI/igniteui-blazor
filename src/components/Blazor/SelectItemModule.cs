namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for the <see cref="IgbSelectItem{TValue}"/> child component of <see cref="IgbSelect{TValue}"/> or <see cref="IgbSelectGroup{TValue}"/>. The parent handles its resources, so registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbSelectItemModule is no longer required, has no effect and can be safely removed.")]
    [IgbModule<IgbSelectItemModule>]
    public partial class IgbSelectItemModule : IIgbModule
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
