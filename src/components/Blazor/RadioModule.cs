namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Client resource module for <see cref="IgbRadio"/>.
    /// </summary>
    /// <remarks>
    /// Register explicitly on application startup by passing this type to <c>AddIgniteUIBlazor</c>.
    /// </remarks>
    [IgbModule<IgbRadioModule>]
    public partial class IgbRadioModule : IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebRadioModule");

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebRadioModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebRadioModule");
        }
    }
}
