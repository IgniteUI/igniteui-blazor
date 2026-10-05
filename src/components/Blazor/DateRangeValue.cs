namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A date range defined by a start and an end date.
    /// </summary>
    public partial class IgbDateRangeValue : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebDateRangeValue"; } }

        private DateTime _start = DateTime.MinValue;

        /// <summary>
        /// The first date of the range.
        /// </summary>
        public DateTime Start
        {
            get { return this._start; }
            set
            {
                if (this._start != value || !IsPropDirty("Start"))
                {
                    MarkPropDirty("Start");
                }
                this._start = value;

            }
        }
        private DateTime _end = DateTime.MinValue;

        /// <summary>
        /// The last date of the range.
        /// </summary>
        public DateTime End
        {
            get { return this._end; }
            set
            {
                if (this._end != value || !IsPropDirty("End"))
                {
                    MarkPropDirty("End");
                }
                this._end = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Start"))
            { BaseRendererControl.AddDateProp(ser, "start", this._start, this._startAsDateOnly, null); }
            if (IsPropDirty("End"))
            { BaseRendererControl.AddDateProp(ser, "end", this._end, this._endAsDateOnly, null); }

        }

    }
}
