namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Client resource module for <see cref="IgbToast"/>.
    /// </summary>
    /// <remarks>
    /// Register explicitly on application startup by passing this type to <c>AddIgniteUIBlazor</c>.
    /// </remarks>
    [IgbModule<IgbToastModule>]
    public partial class IgbToastModule : IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebToastModule");

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebToastModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebToastModule");
        }
    }
}
