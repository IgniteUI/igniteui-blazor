namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A date range defined by a start and an end date.
    /// </summary>
    public partial class IgbDateRangeValue<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebDateRangeValue"; } }

        private TValue? _start = default!;

        /// <summary>
        /// The first date of the range.
        /// </summary>
        public TValue? Start
        {
            get { return this._start; }
            set
            {
                if (!EqualityComparer<TValue?>.Default.Equals(this._start, value) || !IsPropDirty("Start"))
                {
                    MarkPropDirty("Start");
                }
                this._start = value;

            }
        }
        private TValue? _end = default!;

        /// <summary>
        /// The last date of the range.
        /// </summary>
        public TValue? End
        {
            get { return this._end; }
            set
            {
                if (!EqualityComparer<TValue?>.Default.Equals(this._end, value) || !IsPropDirty("End"))
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
            { AddGenericValue(ser, "start", this._start); }
            if (IsPropDirty("End"))
            { AddGenericValue(ser, "end", this._end); }

        }
    }
}
