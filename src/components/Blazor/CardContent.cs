namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for the card's main text content.
    /// Nest inside an <see cref="IgbCard"/> to display the primary content.
    /// </summary>
    public partial class IgbCardContent : BaseRendererControl
    {
        public override string Type { get { return "WebCardContent"; } }

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
                return "igc-card-content";
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
