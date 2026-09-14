namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for the card's main text content.
    /// Nest inside an <see cref="IgbCard"/> to display the primary content.
    /// </summary>
    public partial class IgbCardContent : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCardContent"; } }

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
                return "igc-card-content";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
