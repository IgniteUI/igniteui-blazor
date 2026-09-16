
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The options used to format the months and the weekdays in the calendar views.
    /// Set through <see cref="IgbCalendar.FormatOptions"/>.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbCalendarFormatOptions : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "CalendarFormatOptions"; } }

        private string? _weekday;

        /// <summary>
        /// The representation of the weekday names, one of <c>long</c>, <c>short</c> or <c>narrow</c>.
        /// Defaults to <c>narrow</c>.
        /// </summary>
        public string? Weekday
        {
            get { return this._weekday; }
            set
            {
                if (this._weekday != value || !IsPropDirty("Weekday"))
                {
                    MarkPropDirty("Weekday");
                }
                this._weekday = value;

            }
        }
        private string? _month;

        /// <summary>
        /// The representation of the month names, one of <c>numeric</c>, <c>2-digit</c>, <c>long</c>,
        /// <c>short</c> or <c>narrow</c>. Defaults to <c>long</c>.
        /// </summary>
        public string? Month
        {
            get { return this._month; }
            set
            {
                if (this._month != value || !IsPropDirty("Month"))
                {
                    MarkPropDirty("Month");
                }
                this._month = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Weekday"))
            { ser.AddStringProp("weekday", this._weekday); }
            if (IsPropDirty("Month"))
            { ser.AddStringProp("month", this._month); }

        }

    }
}
