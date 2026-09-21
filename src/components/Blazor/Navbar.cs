namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A navigation bar component is used to facilitate navigation through
    /// a series of hierarchical screens within an app.
    /// </summary>
    public partial class IgbNavbar : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebNavbar"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavbarModule.IsLoadRequested(IgBlazor))
            {
                IgbNavbarModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        private protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        private protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override string DirectRenderElementName
        {
            get
            {
                return "igc-navbar";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

    }
}
