namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Client resource module for <see cref="IgbExpansionPanel"/>.
    /// </summary>
    /// <remarks>
    /// Register explicitly on application startup by passing this type to <c>AddIgniteUIBlazor</c>.
    /// </remarks>
    [IgbModule<IgbExpansionPanelModule>]
    public partial class IgbExpansionPanelModule : IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebExpansionPanelModule");

        }

        internal static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebExpansionPanelModule");
        }

        internal static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebExpansionPanelModule");
        }
    }
}
