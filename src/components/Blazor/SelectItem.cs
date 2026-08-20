namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a select list.
    /// </summary>
    public partial class IgbSelectItem : IgbBaseOptionLike
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSelectItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSelectItemModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectItemModule.Register(IgBlazor);
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
                return "igc-select-item";
            }
        }

    }
}
