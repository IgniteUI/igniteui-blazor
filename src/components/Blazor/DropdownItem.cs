namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a dropdown list.
    /// </summary>
    public partial class IgbDropdownItem : IgbBaseOptionLike
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDropdownItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbDropdownModule.IsLoadRequested(IgBlazor))
            {
                IgbDropdownModule.Register(IgBlazor);
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
                return "igc-dropdown-item";
            }
        }

    }
}
