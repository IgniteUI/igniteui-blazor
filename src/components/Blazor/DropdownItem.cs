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
            ModuleLoader.Load(IgBlazor, "WebDropdownModule");
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
                return "igc-dropdown-item";
            }
        }

    }
}
