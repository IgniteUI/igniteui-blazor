namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Provides the module for the <see cref="IgbSelectHeader"/> child component of <see cref="IgbSelect"/>. The parent handles its resources, so registering this module has no effect and is no longer required.
    /// </summary>
    [Obsolete("Registering IgbSelectHeaderModule is no longer required, has no effect and can be safely removed.")]
    [IgbModule<IgbSelectHeaderModule>]
    public partial class IgbSelectHeaderModule : IIgbModule
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
