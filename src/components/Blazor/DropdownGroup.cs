namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container for a group of dropdown items.
    /// </summary>
    public partial class IgbDropdownGroup : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDropdownGroup"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbDropdownModule.IsLoadRequested(IgBlazor))
            {
                IgbDropdownModule.Register(IgBlazor);
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
                return "igc-dropdown-group";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }


    }
}
