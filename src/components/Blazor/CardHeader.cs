namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for the card's header section.
    /// Displays header content including an optional thumbnail, title, subtitle, and additional content.
    /// </summary>
    public partial class IgbCardHeader : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCardHeader"; } }

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
                return "igc-card-header";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
