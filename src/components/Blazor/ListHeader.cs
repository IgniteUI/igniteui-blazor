namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Header list item.
    /// </summary>
    public partial class IgbListHeader : BaseRendererControl
    {
        public override string Type { get { return "WebListHeader"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbListHeaderModule.IsLoadRequested(IgBlazor))
            {
                IgbListHeaderModule.Register(IgBlazor);
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
                return "igc-list-header";
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
