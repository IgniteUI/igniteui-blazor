namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Used when a custom indicator needs to be passed to the <see cref="IgbCarousel"/> component.
    /// </summary>
    public partial class IgbCarouselIndicator : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCarouselIndicator"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCarouselModule.IsLoadRequested(IgBlazor))
            {
                IgbCarouselModule.Register(IgBlazor);
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
                return "igc-carousel-indicator";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
