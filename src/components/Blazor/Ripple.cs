namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A ripple can be applied to an element to represent
    /// an interactive surface.
    /// </summary>
    public partial class IgbRipple : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRipple"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbRippleModule.IsLoadRequested(IgBlazor))
            {
                IgbRippleModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-ripple";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

    }
}
