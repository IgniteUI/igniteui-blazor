namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A toast component is used to show a brief, non-interactive notification.
    /// </summary>
    public partial class IgbToast : IgbBaseAlertLike
    {
        /// <inheritdoc />
        public override string Type { get { return "WebToast"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbToastModule.IsLoadRequested(IgBlazor))
            {
                IgbToastModule.Register(IgBlazor);
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
                return "igc-toast";
            }
        }

    }
}
