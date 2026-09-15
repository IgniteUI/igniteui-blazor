namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Client resource module for <see cref="IgbCombo{T}"/>.
    /// </summary>
    /// <remarks>
    /// Register explicitly on application startup by passing this type to <c>AddIgniteUIBlazor</c>.
    /// </remarks>
    [IgbModule<IgbComboModule>]
    public partial class IgbComboModule : IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebComboModule");

            IgbIconModule.MarkIsLoadRequested(runtime);
            IgbInputModule.MarkIsLoadRequested(runtime);

        }

        internal static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebComboModule");
        }

        internal static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebComboModule");
        }
    }
}
