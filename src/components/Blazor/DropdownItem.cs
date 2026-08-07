namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a dropdown list.
    /// </summary>
    public partial class IgbDropdownItem : IgbBaseOptionLike
    {
        public override string Type { get { return "WebDropdownItem"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbDropdownItemModule.IsLoadRequested(IgBlazor))
            {
                IgbDropdownItemModule.Register(IgBlazor);
            }
        }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-dropdown-item";
            }
        }

    }
}
