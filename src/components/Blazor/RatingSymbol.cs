namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Used when a custom icon/symbol/element needs to be passed to an <see cref="IgbRating"/> component.
    /// </summary>
    public partial class IgbRatingSymbol : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRatingSymbol"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbRatingModule.IsLoadRequested(IgBlazor))
            {
                IgbRatingModule.Register(IgBlazor);
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
                return "igc-rating-symbol";
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
        public async Task ConnectedCallbackAsync()
        {
            await InvokeMethod("connectedCallback", new object[] { }, new string[] { });
        }
        public void ConnectedCallback()
        {
            InvokeMethodSync("connectedCallback", new object[] { }, new string[] { });
        }

    }
}
