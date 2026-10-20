namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A predefined date range with label for <see cref="IgbDateRangePicker{TValue}.CustomRanges"/>.
    /// </summary>
    /// <typeparam name="TValue">The <c>TValue</c> of the <see cref="IgbDateRangePicker{TValue}"/> the range is for.</typeparam>
    public partial class IgbCustomDateRange<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebCustomDateRange"; } }

        private string _label = string.Empty;

        /// <summary>
        /// The text rendered in the chip for this range.
        /// </summary>
        public required string Label
        {
            get { return this._label; }
            set
            {
                if (this._label != value || !IsPropDirty("Label"))
                {
                    MarkPropDirty("Label");
                }
                this._label = value;

            }
        }
        private IgbDateRangeValue<TValue> _dateRange = new();

        /// <summary>
        /// The date range applied when the chip is selected.
        /// </summary>
        public required IgbDateRangeValue<TValue> DateRange
        {
            get { return this._dateRange; }
            set
            {
                MarkPropDirty("DateRange");
                if (this._dateRange != null)
                {
                    this.DetachChild(this._dateRange);
                }
                this._dateRange = value;
                if (value != null)
                {
                    this.AttachChild(value);
                }
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("DateRange"))
            { ser.AddSerializableProp("dateRange", this._dateRange); }

        }

    }
}
