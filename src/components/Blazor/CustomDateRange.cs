
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A predefined date range with label for <see cref="IgbDateRangePicker.CustomRanges"/>.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbCustomDateRange : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCustomDateRange"; } }

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
        private IgbDateRangeValue _dateRange = new IgbDateRangeValue();

        /// <summary>
        /// The date range applied when the chip is selected.
        /// </summary>
        public required IgbDateRangeValue DateRange
        {
            get { return this._dateRange; }
            set
            {
                MarkPropDirty("DateRange");
                this._dateRange = value;
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
