namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A toast component is used to show a brief, non-interactive notification.
    /// </summary>
    public partial class IgbToast : IgbBaseAlertLike
    {
        public override string Type { get { return "WebToast"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbToastModule.IsLoadRequested(IgBlazor))
            {
                IgbToastModule.Register(IgBlazor);
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
                return "igc-toast";
            }
        }

    }
}
