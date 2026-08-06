namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A navigation bar component is used to facilitate navigation through
    /// a series of hierarchical screens within an app.
    /// </summary>
    public partial class IgbNavbar : BaseRendererControl
    {
        public override string Type { get { return "WebNavbar"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavbarModule.IsLoadRequested(IgBlazor))
            {
                IgbNavbarModule.Register(IgBlazor);
            }
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
                return "igc-navbar";
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
