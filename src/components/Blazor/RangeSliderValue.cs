
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The pair of thumb values carried by the <see cref="IgbRangeSlider"/> value events.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbRangeSliderValue : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRangeSliderValue"; } }

        private double _lower = 0;

        /// <summary>
        /// The value of the lower thumb.
        /// </summary>
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

    }
}
