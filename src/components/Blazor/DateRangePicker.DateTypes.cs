using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Date-typed parameters for <see cref="IgbDateRangePicker"/>.
    /// </summary>
    /// <remarks>
    /// A range is almost always a pair of calendar dates, so the <c>DateOnly</c> parameters are the
    /// safe default: they cross as <c>yyyy-MM-dd</c> and are never converted between timezones.
    /// Setting one of the sibling parameters clears the others.
    /// </remarks>
    public partial class IgbDateRangePicker
    {
        private DateOnly? _minAsDateOnly = null;
        private DateTimeOffset? _minAsDateTimeOffset = null;

        /// <summary>
        /// The minimum value required for the picker to remain valid, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? MinAsDateOnly
        {
            get { return this._minAsDateOnly; }
            set
            {
                if (this._minAsDateOnly != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._minAsDateOnly = value;
                this._minAsDateTimeOffset = null;
                this._min = value?.ToDateTime(TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// The minimum value required for the picker to remain valid, as an instant.
        /// </summary>
        [Parameter]
        public DateTimeOffset? MinAsDateTimeOffset
        {
            get { return this._minAsDateTimeOffset; }
            set
            {
                if (this._minAsDateTimeOffset != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._minAsDateTimeOffset = value;
                this._minAsDateOnly = null;
                this._min = value?.DateTime;
            }
        }

        private DateOnly? _maxAsDateOnly = null;
        private DateTimeOffset? _maxAsDateTimeOffset = null;

        /// <summary>
        /// The maximum value required for the picker to remain valid, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? MaxAsDateOnly
        {
            get { return this._maxAsDateOnly; }
            set
            {
                if (this._maxAsDateOnly != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._maxAsDateOnly = value;
                this._maxAsDateTimeOffset = null;
                this._max = value?.ToDateTime(TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// The maximum value required for the picker to remain valid, as an instant.
        /// </summary>
        [Parameter]
        public DateTimeOffset? MaxAsDateTimeOffset
        {
            get { return this._maxAsDateTimeOffset; }
            set
            {
                if (this._maxAsDateTimeOffset != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._maxAsDateTimeOffset = value;
                this._maxAsDateOnly = null;
                this._max = value?.DateTime;
            }
        }

        private DateOnly? _activeDateAsDateOnly = null;

        /// <summary>
        /// The date shown and highlighted in the calendar, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? ActiveDateAsDateOnly
        {
            get { return this._activeDateAsDateOnly; }
            set
            {
                if (this._activeDateAsDateOnly != value || !IsPropDirty("ActiveDate"))
                {
                    MarkPropDirty("ActiveDate");
                }
                this._activeDateAsDateOnly = value;
                this._activeDate = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue;
            }
        }
    }

    /// <summary>
    /// Date-typed members for <see cref="IgbDateRangeValue"/>.
    /// </summary>
    /// <remarks>
    /// The range endpoints follow the same rule as the picker's other dates: a <c>DateOnly</c> is a
    /// calendar date that never shifts, while <see cref="DateTime"/> behaves by its
    /// <see cref="DateTime.Kind"/>. Setting the <c>DateOnly</c> member keeps the
    /// <see cref="DateTime"/> one readable at midnight of the same day.
    /// </remarks>
    public partial class IgbDateRangeValue
    {
        private DateOnly? _startAsDateOnly = null;

        /// <summary>
        /// The start of the range as a calendar date, which is never converted between timezones.
        /// </summary>
        public DateOnly? StartAsDateOnly
        {
            get { return this._startAsDateOnly; }
            set
            {
                this._startAsDateOnly = value;
                this.Start = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue;
            }
        }

        private DateOnly? _endAsDateOnly = null;

        /// <summary>
        /// The end of the range as a calendar date, which is never converted between timezones.
        /// </summary>
        public DateOnly? EndAsDateOnly
        {
            get { return this._endAsDateOnly; }
            set
            {
                this._endAsDateOnly = value;
                this.End = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue;
            }
        }
    }
}
