namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Client resource module for <see cref="IgbDateRangePicker"/>.
    /// </summary>
    /// <remarks>
    /// Register explicitly on application startup by passing this type to <c>AddIgniteUIBlazor</c>.
    /// </remarks>
    public partial class IgbDateRangePickerModule : IIgbModule
    {
        /// <summary>
        /// Requests this module's client resources to be loaded into the runtime.
        /// </summary>
        /// <param name="runtime">The Ignite UI Blazor runtime to load the resources into.</param>
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebDateRangePickerModule");

            IgbCalendarModule.MarkIsLoadRequested(runtime);
            IgbDateTimeInputModule.MarkIsLoadRequested(runtime);
            IgbDialogModule.MarkIsLoadRequested(runtime);
            IgbIconModule.MarkIsLoadRequested(runtime);
            IgbChipModule.MarkIsLoadRequested(runtime);
            IgbInputModule.MarkIsLoadRequested(runtime);

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebDateRangePickerModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebDateRangePickerModule");
        }
    }
}
