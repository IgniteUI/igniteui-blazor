using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Describes a set of dates by combining a range type with the dates it applies to.
    /// </summary>
    public partial class IgbDateRangeDescriptor : BaseRendererElement
    {
        public override string Type { get { return "DateRangeDescriptor"; } }

        private DateRangeType _rangeType = DateRangeType.After;

        /// <summary>
        /// The kind of range being described, which determines how <see cref="DateRange"/> is matched.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("Type")]
        public DateRangeType RangeType
        {
            get { return this._rangeType; }
            set
            {
                if (this._rangeType != value || !IsPropDirty("RangeType"))
                {
                    MarkPropDirty("RangeType");
                }
                this._rangeType = value;

            }
        }
        private object _dateRange;

        /// <summary>
        /// The date or dates the descriptor applies to, interpreted according to <see cref="RangeType"/>.
        /// <see cref="DateRangeType.After"/> and <see cref="DateRangeType.Before"/> use the first date,
        /// <see cref="DateRangeType.Between"/> uses the first and the last, and
        /// <see cref="DateRangeType.Specific"/> matches every date listed. Not used by
        /// <see cref="DateRangeType.Weekdays"/> and <see cref="DateRangeType.Weekends"/>.
        /// </summary>
        [Parameter]
        public object DateRange
        {
            get { return this._dateRange; }
            set
            {
                if (this._dateRange != value || !IsPropDirty("DateRange"))
                {
                    MarkPropDirty("DateRange");
                }
                this._dateRange = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("RangeType"))
            { ser.AddEnumProp("rangeType", this._rangeType); }
            if (IsPropDirty("DateRange"))
            { ser.AddPrimitiveProp("dateRange", this._dateRange); }

        }

    }
}
