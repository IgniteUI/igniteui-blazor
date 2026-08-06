namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Displays a collection of data items in a templatable list format.
    /// </summary>
    public partial class IgbList : BaseRendererControl
    {
        public override string Type { get { return "WebList"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbListModule.IsLoadRequested(IgBlazor))
            {
                IgbListModule.Register(IgBlazor);
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
                return "igc-list";
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
