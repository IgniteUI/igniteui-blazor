namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for card media content such as images, GIFs, or videos.
    /// This component should be nested inside an <see cref="IgbCard"/> to display visual content.
    /// </summary>
    public partial class IgbCardMedia : BaseRendererControl
    {
        public override string Type { get { return "WebCardMedia"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbCardMediaModule.IsLoadRequested(IgBlazor))
            {
                IgbCardMediaModule.Register(IgBlazor);
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
                return "igc-card-media";
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
