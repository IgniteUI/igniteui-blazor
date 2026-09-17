namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Similar to a checkbox, a switch controls the state of a single setting on or off.
    /// </summary>
    public partial class IgbSwitch : IgbCheckboxBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSwitch"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSwitchModule.IsLoadRequested(IgBlazor))
            {
                IgbSwitchModule.Register(IgBlazor);
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
                return "igc-switch";
            }
        }

    }
}
