namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Header list item.
    /// </summary>
    public partial class IgbListHeader : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebListHeader"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbListModule.IsLoadRequested(IgBlazor))
            {
                IgbListModule.Register(IgBlazor);
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
                return "igc-list-header";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
