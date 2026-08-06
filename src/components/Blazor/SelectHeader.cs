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
            if (!IgbSelectHeaderModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectHeaderModule.Register(IgBlazor);
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
                return "igc-select-header";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        partial void FindByNameSelectHeader(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameSelectHeader(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
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
