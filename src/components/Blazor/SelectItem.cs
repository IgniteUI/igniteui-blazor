namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a select list.
    /// </summary>
    public partial class IgbSelectItem : IgbBaseOptionLike
    {
        public override string Type { get { return "WebSelectItem"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbSelectItemModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectItemModule.Register(IgBlazor);
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
                return "igc-select-item";
            }
        }

    }
}
