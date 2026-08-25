using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The pair of thumb values carried by the <see cref="IgbRangeSlider"/> value events.
    /// </summary>
    public partial class IgbRangeSliderValue : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRangeSliderValue"; } }

        private static bool _marshalByValue = true;

        private double _lower = 0;

        /// <summary>
        /// The value of the lower thumb.
        /// </summary>
        [Parameter]
        public double Lower
        {
            get { return this._lower; }
            set
            {
                if (this._lower != value || !IsPropDirty("Lower"))
                {
                    MarkPropDirty("Lower");
                }
                this._lower = value;

            }
        }
        private double _upper = 0;

        /// <summary>
        /// The value of the upper thumb.
        /// </summary>
        [Parameter]
        public double Upper
        {
            get { return this._upper; }
            set
            {
                if (this._upper != value || !IsPropDirty("Upper"))
                {
                    MarkPropDirty("Upper");
                }
                this._upper = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Lower"))
            { ser.AddNumberProp("lower", this._lower); }
            if (IsPropDirty("Upper"))
            { ser.AddNumberProp("upper", this._upper); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Lower"))
            { args["lower"] = (this._lower).ToString(); }
            if (IsPropDirty("Upper"))
            { args["upper"] = (this._upper).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.ContainsKey("lower"))
            { this.Lower = ReturnToDouble(args["lower"]); }
            if (args != null && args.ContainsKey("upper"))
            { this.Upper = ReturnToDouble(args["upper"]); }

            this.SuppressParentNotify = false;
        }

    }
}
