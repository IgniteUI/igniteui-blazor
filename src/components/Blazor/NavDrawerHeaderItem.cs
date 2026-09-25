namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a navigation drawer header item.
    /// </summary>
    public partial class IgbNavDrawerHeaderItem : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebNavDrawerHeaderItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavDrawerModule.IsLoadRequested(IgBlazor))
            {
                IgbNavDrawerModule.Register(IgBlazor);
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
                return "igc-nav-drawer-header-item";
            }
        }

        /// <inheritdoc />
        private protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

    }
}
