
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options for controlling navigation behavior when moving the active highlight.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbHighlightNavigation : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebHighlightNavigation"; } }

        private bool _preventScroll = false;

        /// <summary>
        /// When <see langword="true"/>, prevents the component from scrolling the new active match into view.
        /// </summary>
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
