namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A circular progress indicator used to express unspecified wait time or display
    /// the length of a process.
    /// </summary>
    public partial class IgbCircularProgress : IgbProgressBase
    {
        public override string Type { get { return "WebCircularProgress"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbCircularProgressModule.IsLoadRequested(IgBlazor))
            {
                IgbCircularProgressModule.Register(IgBlazor);
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
                return "igc-circular-progress";
            }
        }

    }
}
