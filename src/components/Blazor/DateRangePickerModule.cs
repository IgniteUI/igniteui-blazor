namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDateRangePickerModule
    {
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
