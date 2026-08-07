namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a header item in an <see cref="IgbSelect"/> component.
    /// </summary>
    public partial class IgbSelectHeader : BaseRendererControl
    {
        public override string Type { get { return "WebSelectHeader"; } }

        protected override void EnsureModulesLoaded()
        {
            ModuleLoader.Load(IgBlazor, "WebSelectModule");
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
                return "igc-select-header";
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
