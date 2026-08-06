namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a navigation drawer header item.
    /// </summary>
    public partial class IgbNavDrawerHeaderItem : BaseRendererControl
    {
        public override string Type { get { return "WebNavDrawerHeaderItem"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavDrawerHeaderItemModule.IsLoadRequested(IgBlazor))
            {
                IgbNavDrawerHeaderItemModule.Register(IgBlazor);
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
                return "igc-nav-drawer-header-item";
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
