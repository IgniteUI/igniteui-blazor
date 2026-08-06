namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Similar to a checkbox, a switch controls the state of a single setting on or off.
    /// </summary>
    public partial class IgbSwitch : IgbCheckboxBase
    {
        public override string Type { get { return "WebSwitch"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbSwitchModule.IsLoadRequested(IgBlazor))
            {
                IgbSwitchModule.Register(IgBlazor);
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
                return "igc-switch";
            }
        }

        partial void FindByNameSwitch(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameSwitch(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

    }
}
