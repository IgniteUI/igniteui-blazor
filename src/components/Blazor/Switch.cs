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
                return "igc-switch";
            }
        }

    }
}
