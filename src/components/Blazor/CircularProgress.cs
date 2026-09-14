namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A circular progress indicator used to express unspecified wait time or display
    /// the length of a process.
    /// </summary>
    public partial class IgbCircularProgress : IgbProgressBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCircularProgress"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCircularProgressModule.IsLoadRequested(IgBlazor))
            {
                IgbCircularProgressModule.Register(IgBlazor);
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
                return "igc-circular-progress";
            }
        }

    }
}
