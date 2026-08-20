namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Allows formatting the values of the slider as string values.
    /// The text content of the slider labels is used for thumb and tick labels.
    /// </summary>
    public partial class IgbSliderLabel : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSliderLabel"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            // Labels belong to either slider flavour; the range slider already brings them in,
            // so only fall back to the plain slider module when it has not been requested.
            if (!ModuleLoader.IsLoadRequested(IgBlazor, "WebRangeSliderModule"))
            {
                ModuleLoader.Load(IgBlazor, "WebSliderModule");
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
                return "igc-slider-label";
            }
        }

        /// <inheritdoc />
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
