using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options controlling how a component is focused, passed to the <c>FocusComponent</c> methods.
    /// Mirrors the browser focus options.
    /// </summary>
    public partial class IgbFocusOptions : BaseRendererElement
    {
        public override string Type { get { return "FocusOptions"; } }

        private bool _preventScroll = false;

        /// <summary>
        /// Whether the browser should keep the current scroll position instead of scrolling the newly
        /// focused component into view. Defaults to <see langword="false"/>.
        /// </summary>
        [Parameter]
        public bool PreventScroll
        {
            get { return this._preventScroll; }
            set
            {
                if (this._preventScroll != value || !IsPropDirty("PreventScroll"))
                {
                    MarkPropDirty("PreventScroll");
                }
                this._preventScroll = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("PreventScroll"))
            { ser.AddBooleanProp("preventScroll", this._preventScroll); }

        }

    }
}
