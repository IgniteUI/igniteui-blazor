namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for card media content such as images, GIFs, or videos.
    /// This component should be nested inside an <see cref="IgbCard"/> to display visual content.
    /// </summary>
    public partial class IgbCardMedia : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCardMedia"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCardModule.IsLoadRequested(IgBlazor))
            {
                IgbCardModule.Register(IgBlazor);
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
                return "igc-card-media";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
