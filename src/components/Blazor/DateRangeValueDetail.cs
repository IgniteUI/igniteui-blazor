namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A date range defined by a start and an end date, carried as the payload of
    /// <see cref="IgbDateRangeValueEventArgs{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">The <c>TValue</c> of the <see cref="IgbDateRangePicker{TValue}"/> that raises the event.</typeparam>
    public partial class IgbDateRangeValueDetail<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebDateRangeValueDetail"; } }

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

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Start"))
            { args["start"] = GenericValueString(this._start); }
            if (IsPropDirty("End"))
            { args["end"] = GenericValueString(this._end); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("start", out var startObj))
            {
                this.Start = GenericValueFromEventJson<TValue>(startObj);
            }
            if (args != null && args.TryGetValue("end", out var endObj))
            {
                this.End = GenericValueFromEventJson<TValue>(endObj);
            }

            this.SuppressParentNotify = false;
        }
    }
}
