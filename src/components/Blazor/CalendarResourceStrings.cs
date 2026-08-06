using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The localized strings used by the calendar views, exposed through the <c>ResourceStrings</c>
    /// property of <see cref="IgbCalendar"/> and <see cref="IgbDatePicker"/>.
    /// </summary>
    public partial class IgbCalendarResourceStrings : BaseRendererElement
    {
        public override string Type { get { return "WebCalendarResourceStrings"; } }

        public IgbCalendarResourceStrings() : base()
        {
            OnCreatedIgbCalendarResourceStrings();

        }

        partial void OnCreatedIgbCalendarResourceStrings();

        private string _selectMonth;

        partial void OnSelectMonthChanging(ref string newValue);

        /// <summary>
        /// Text for the accessible label of the header button that switches the calendar to the months view.
        /// </summary>
        [Parameter]
        public string SelectMonth
        {
            get { return this._selectMonth; }
            set
            {
                if (this._selectMonth != value || !IsPropDirty("SelectMonth"))
                {
                    MarkPropDirty("SelectMonth");
                }
                this._selectMonth = value;

            }
        }
        private string _selectYear;

        partial void OnSelectYearChanging(ref string newValue);

        /// <summary>
        /// Text for the accessible label of the header button that switches the calendar to the years view.
        /// </summary>
        [Parameter]
        public string SelectYear
        {
            get { return this._selectYear; }
            set
            {
                if (this._selectYear != value || !IsPropDirty("SelectYear"))
                {
                    MarkPropDirty("SelectYear");
                }
                this._selectYear = value;

            }
        }
        private string _selectDate;

        partial void OnSelectDateChanging(ref string newValue);

        /// <summary>
        /// Title shown in the calendar header until a date is selected, in single selection mode.
        /// Defaults to <c>Select Date</c>.
        /// </summary>
        [Parameter]
        public string SelectDate
        {
            get { return this._selectDate; }
            set
            {
                if (this._selectDate != value || !IsPropDirty("SelectDate"))
                {
                    MarkPropDirty("SelectDate");
                }
                this._selectDate = value;

            }
        }
        private string _selectRange;

        partial void OnSelectRangeChanging(ref string newValue);

        /// <summary>
        /// Title shown in the calendar header until a range is selected, in range selection mode.
        /// Defaults to <c>Select Range</c>.
        /// </summary>
        [Parameter]
        public string SelectRange
        {
            get { return this._selectRange; }
            set
            {
                if (this._selectRange != value || !IsPropDirty("SelectRange"))
                {
                    MarkPropDirty("SelectRange");
                }
                this._selectRange = value;

            }
        }
        private string _selectedDate;

        partial void OnSelectedDateChanging(ref string newValue);

        /// <summary>
        /// The label for the currently selected date.
        /// </summary>
        /// <remarks>
        /// Not mapped to any string in the current localization pipeline and has no effect.
        /// </remarks>
        [Parameter]
        public string SelectedDate
        {
            get { return this._selectedDate; }
            set
            {
                if (this._selectedDate != value || !IsPropDirty("SelectedDate"))
                {
                    MarkPropDirty("SelectedDate");
                }
                this._selectedDate = value;

            }
        }
        private string _startDate;

        partial void OnStartDateChanging(ref string newValue);

        /// <summary>
        /// Placeholder shown in the calendar header in place of the range start date until one is
        /// selected. Defaults to <c>Start</c>.
        /// </summary>
        [Parameter]
        public string StartDate
        {
            get { return this._startDate; }
            set
            {
                if (this._startDate != value || !IsPropDirty("StartDate"))
                {
                    MarkPropDirty("StartDate");
                }
                this._startDate = value;

            }
        }
        private string _endDate;

        partial void OnEndDateChanging(ref string newValue);

        /// <summary>
        /// Placeholder shown in the calendar header in place of the range end date until one is
        /// selected. Defaults to <c>End</c>.
        /// </summary>
        [Parameter]
        public string EndDate
        {
            get { return this._endDate; }
            set
            {
                if (this._endDate != value || !IsPropDirty("EndDate"))
                {
                    MarkPropDirty("EndDate");
                }
                this._endDate = value;

            }
        }
        private string _previousMonth;

        partial void OnPreviousMonthChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the days view one month back.
        /// Defaults to <c>Previous Month</c>.
        /// </summary>
        [Parameter]
        public string PreviousMonth
        {
            get { return this._previousMonth; }
            set
            {
                if (this._previousMonth != value || !IsPropDirty("PreviousMonth"))
                {
                    MarkPropDirty("PreviousMonth");
                }
                this._previousMonth = value;

            }
        }
        private string _nextMonth;

        partial void OnNextMonthChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the days view one month forward.
        /// Defaults to <c>Next Month</c>.
        /// </summary>
        [Parameter]
        public string NextMonth
        {
            get { return this._nextMonth; }
            set
            {
                if (this._nextMonth != value || !IsPropDirty("NextMonth"))
                {
                    MarkPropDirty("NextMonth");
                }
                this._nextMonth = value;

            }
        }
        private string _previousYear;

        partial void OnPreviousYearChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the months view one year back.
        /// Defaults to <c>Previous Year</c>.
        /// </summary>
        [Parameter]
        public string PreviousYear
        {
            get { return this._previousYear; }
            set
            {
                if (this._previousYear != value || !IsPropDirty("PreviousYear"))
                {
                    MarkPropDirty("PreviousYear");
                }
                this._previousYear = value;

            }
        }
        private string _nextYear;

        partial void OnNextYearChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the months view one year forward.
        /// Defaults to <c>Next Year</c>.
        /// </summary>
        [Parameter]
        public string NextYear
        {
            get { return this._nextYear; }
            set
            {
                if (this._nextYear != value || !IsPropDirty("NextYear"))
                {
                    MarkPropDirty("NextYear");
                }
                this._nextYear = value;

            }
        }
        private string _previousYears;

        partial void OnPreviousYearsChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the years view one page back.
        /// Defaults to <c>Previous {0} Years</c>, where <c>{0}</c> is the number of years on a page.
        /// </summary>
        [Parameter]
        public string PreviousYears
        {
            get { return this._previousYears; }
            set
            {
                if (this._previousYears != value || !IsPropDirty("PreviousYears"))
                {
                    MarkPropDirty("PreviousYears");
                }
                this._previousYears = value;

            }
        }
        private string _nextYears;

        partial void OnNextYearsChanging(ref string newValue);

        /// <summary>
        /// The label of the navigation button that moves the years view one page forward.
        /// Defaults to <c>Next {0} Years</c>, where <c>{0}</c> is the number of years on a page.
        /// </summary>
        [Parameter]
        public string NextYears
        {
            get { return this._nextYears; }
            set
            {
                if (this._nextYears != value || !IsPropDirty("NextYears"))
                {
                    MarkPropDirty("NextYears");
                }
                this._nextYears = value;

            }
        }
        private string _weekLabel;

        partial void OnWeekLabelChanging(ref string newValue);

        /// <summary>
        /// The header of the week numbers column in the days view. Defaults to <c>Wk</c>.
        /// </summary>
        [Parameter]
        public string WeekLabel
        {
            get { return this._weekLabel; }
            set
            {
                if (this._weekLabel != value || !IsPropDirty("WeekLabel"))
                {
                    MarkPropDirty("WeekLabel");
                }
                this._weekLabel = value;

            }
        }

        partial void FindByNameCalendarResourceStrings(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCalendarResourceStrings(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbCalendarResourceStrings(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbCalendarResourceStrings(ser);

            if (IsPropDirty("SelectMonth"))
            { ser.AddStringProp("selectMonth", this._selectMonth); }
            if (IsPropDirty("SelectYear"))
            { ser.AddStringProp("selectYear", this._selectYear); }
            if (IsPropDirty("SelectDate"))
            { ser.AddStringProp("selectDate", this._selectDate); }
            if (IsPropDirty("SelectRange"))
            { ser.AddStringProp("selectRange", this._selectRange); }
            if (IsPropDirty("SelectedDate"))
            { ser.AddStringProp("selectedDate", this._selectedDate); }
            if (IsPropDirty("StartDate"))
            { ser.AddStringProp("startDate", this._startDate); }
            if (IsPropDirty("EndDate"))
            { ser.AddStringProp("endDate", this._endDate); }
            if (IsPropDirty("PreviousMonth"))
            { ser.AddStringProp("previousMonth", this._previousMonth); }
            if (IsPropDirty("NextMonth"))
            { ser.AddStringProp("nextMonth", this._nextMonth); }
            if (IsPropDirty("PreviousYear"))
            { ser.AddStringProp("previousYear", this._previousYear); }
            if (IsPropDirty("NextYear"))
            { ser.AddStringProp("nextYear", this._nextYear); }
            if (IsPropDirty("PreviousYears"))
            { ser.AddStringProp("previousYears", this._previousYears); }
            if (IsPropDirty("NextYears"))
            { ser.AddStringProp("nextYears", this._nextYears); }
            if (IsPropDirty("WeekLabel"))
            { ser.AddStringProp("weekLabel", this._weekLabel); }

        }

    }
}
