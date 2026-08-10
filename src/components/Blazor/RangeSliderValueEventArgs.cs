using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbRangeSlider"/> value events, such as
    /// <see cref="IgbRangeSlider.Input"/> and <see cref="IgbRangeSlider.Change"/>.
    /// </summary>
    public partial class IgbRangeSliderValueEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebRangeSliderValueEventArgs"; } }

        private IgbRangeSliderValue _detail;

        /// <summary>
        /// The lower and upper thumb values of the range slider.
        /// </summary>
        [Parameter]
        public IgbRangeSliderValue Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._detail = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbRangeSliderValue)ConvertReturnValue(args["detail"], "RangeSliderValue", true); }

            this.SuppressParentNotify = false;
        }

    }
}
