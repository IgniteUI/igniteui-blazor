using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The localized strings used by the date range picker, exposed through
    /// <see cref="IgbDateRangePicker.ResourceStrings"/>.
    /// </summary>
    public partial class IgbDateRangePickerResourceStrings : IgbCalendarResourceStrings
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDateRangePickerResourceStrings"; } }

        public IgbDateRangePickerResourceStrings() : base()
        {
            OnCreatedIgbDateRangePickerResourceStrings();

        }

        partial void OnCreatedIgbDateRangePickerResourceStrings();

        private string _separator;
        /// <summary>
        /// The text shown between the start and end inputs when the date range picker is configured with separate inputs.
        /// </summary>
        [Parameter]
        public string Separator
        {
            get { return this._separator; }
            set
            {
                if (this._separator != value || !IsPropDirty("Separator"))
                {
                    MarkPropDirty("Separator");
                }
                this._separator = value;

            }
        }

        private string _doneButton;
        /// <summary>
        /// Text for the button that commits the range selection when the picker is in dialog mode.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("done")]
        public string DoneButton
        {
            get { return this._doneButton; }
            set
            {
                if (this._doneButton != value || !IsPropDirty("DoneButton"))
                {
                    MarkPropDirty("DoneButton");
                }
                this._doneButton = value;

            }
        }

        private string _cancelButton;
        /// <summary>
        /// Text for the button that cancels the range selection when the picker is in dialog mode.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("cancel")]
        public string CancelButton
        {
            get { return this._cancelButton; }
            set
            {
                if (this._cancelButton != value || !IsPropDirty("CancelButton"))
                {
                    MarkPropDirty("CancelButton");
                }
                this._cancelButton = value;

            }
        }

        private string _last7Days;
        /// <summary>
        /// Text for the preset range button that selects the last 7 days.
        /// </summary>
        [Parameter]
        public string Last7Days
        {
            get { return this._last7Days; }
            set
            {
                if (this._last7Days != value || !IsPropDirty("Last7Days"))
                {
                    MarkPropDirty("Last7Days");
                }
                this._last7Days = value;

            }
        }

        private string _last30Days;
        /// <summary>
        /// Text for the preset range button that selects the last 30 days.
        /// </summary>
        [Parameter]
        public string Last30Days
        {
            get { return this._last30Days; }
            set
            {
                if (this._last30Days != value || !IsPropDirty("Last30Days"))
                {
                    MarkPropDirty("Last30Days");
                }
                this._last30Days = value;

            }
        }

        private string _currentMonth;
        /// <summary>
        /// Text for the preset range button that selects the current month.
        /// </summary>
        [Parameter]
        public string CurrentMonth
        {
            get { return this._currentMonth; }
            set
            {
                if (this._currentMonth != value || !IsPropDirty("CurrentMonth"))
                {
                    MarkPropDirty("CurrentMonth");
                }
                this._currentMonth = value;

            }
        }

        private string _yearToDate;
        /// <summary>
        /// Text for the preset range button that selects from the start of the current year to today.
        /// </summary>
        [Parameter]
        public string YearToDate
        {
            get { return this._yearToDate; }
            set
            {
                if (this._yearToDate != value || !IsPropDirty("YearToDate"))
                {
                    MarkPropDirty("YearToDate");
                }
                this._yearToDate = value;

            }
        }

        partial void FindByNameDateRangePickerResourceStrings(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameDateRangePickerResourceStrings(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbDateRangePickerResourceStrings(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbDateRangePickerResourceStrings(ser);

            if (IsPropDirty("Separator"))
            { ser.AddStringProp("separator", this._separator); }
            if (IsPropDirty("DoneButton"))
            { ser.AddStringProp("doneButton", this._doneButton); }
            if (IsPropDirty("CancelButton"))
            { ser.AddStringProp("cancelButton", this._cancelButton); }
            if (IsPropDirty("Last7Days"))
            { ser.AddStringProp("last7Days", this._last7Days); }
            if (IsPropDirty("Last30Days"))
            { ser.AddStringProp("last30Days", this._last30Days); }
            if (IsPropDirty("CurrentMonth"))
            { ser.AddStringProp("currentMonth", this._currentMonth); }
            if (IsPropDirty("YearToDate"))
            { ser.AddStringProp("yearToDate", this._yearToDate); }
        }

    }
}
