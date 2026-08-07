namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for the card's header section.
    /// Displays header content including an optional thumbnail, title, subtitle, and additional content.
    /// </summary>
    public partial class IgbCardHeader : BaseRendererControl
    {
        public override string Type { get { return "WebCardHeader"; } }

        protected override void EnsureModulesLoaded()
        {
            ModuleLoader.Load(IgBlazor, "WebCardModule");
        }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-card-header";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

    }
}
