namespace IgniteUI.Blazor.Controls
{
    public partial class IgbNavDrawerHeaderItemModule
    {
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "WebNavDrawerHeaderItemModule");

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebNavDrawerHeaderItemModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "WebNavDrawerHeaderItemModule");
        }
    }
}
