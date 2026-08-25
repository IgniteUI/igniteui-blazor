using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class for <see cref="IgbCalendar"/>.
    /// </summary>
    public partial class IgbCalendarBase : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCalendarBase"; } }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Queued; }
        }

        private CalendarSelection _selection = CalendarSelection.Single;

        /// <summary>
        /// Sets the type of selection in the component.
        /// </summary>
        [Parameter]
        public CalendarSelection Selection
        {
            get { return this._selection; }
            set
            {
                if (this._selection != value || !IsPropDirty("Selection"))
                {
                    MarkPropDirty("Selection");
                }
                this._selection = value;

            }
        }
        private bool _showWeekNumbers = false;

        /// <summary>
        /// Whether to show the week numbers.
        /// </summary>
        [Parameter]
        public bool ShowWeekNumbers
        {
            get { return this._showWeekNumbers; }
            set
            {
                if (this._showWeekNumbers != value || !IsPropDirty("ShowWeekNumbers"))
                {
                    MarkPropDirty("ShowWeekNumbers");
                }
                this._showWeekNumbers = value;

            }
        }
        private WeekDays _weekStart = WeekDays.Sunday;

        /// <summary>
        /// Gets/Sets the first day of the week.
        /// </summary>
        [Parameter]
        public WeekDays WeekStart
        {
            get { return this._weekStart; }
            set
            {
                if (this._weekStart != value || !IsPropDirty("WeekStart"))
                {
                    MarkPropDirty("WeekStart");
                }
                this._weekStart = value;

            }
        }
        private string? _locale;

        /// <summary>
        /// Gets/Sets the locale used for formatting and displaying the dates in the component.
        /// </summary>
        [Parameter]
        public string? Locale
        {
            get { return this._locale; }
            set
            {
                if (this._locale != value || !IsPropDirty("Locale"))
                {
                    MarkPropDirty("Locale");
                }
                this._locale = value;

            }
        }
        private IgbCalendarResourceStrings? _resourceStrings;

        /// <summary>
        /// The resource strings for localization.
        /// </summary>
        [Parameter]
        public IgbCalendarResourceStrings? ResourceStrings
        {
            get { return this._resourceStrings; }
            set
            {
                MarkPropDirty("ResourceStrings");
                if (this._resourceStrings != null)
                {
                    this.DetachChild(this._resourceStrings);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._resourceStrings = value;
            }

        }
        private IgbDateRangeDescriptor[]? _specialDates;

        /// <summary>
        /// Gets/Sets the special dates for the component.
        /// </summary>
        [Parameter]
        public IgbDateRangeDescriptor[]? SpecialDates
        {
            get { return this._specialDates; }
            set
            {
                if (this._specialDates != value || !IsPropDirty("SpecialDates"))
                {
                    MarkPropDirty("SpecialDates");
                }
                this._specialDates = value;

            }
        }
        private IgbDateRangeDescriptor[]? _disabledDates;

        /// <summary>
        /// Gets/Sets the disabled dates for the component.
        /// </summary>
        [Parameter]
        public IgbDateRangeDescriptor[]? DisabledDates
        {
            get { return this._disabledDates; }
            set
            {
                if (this._disabledDates != value || !IsPropDirty("DisabledDates"))
                {
                    MarkPropDirty("DisabledDates");
                }
                this._disabledDates = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Selection"))
            { ser.AddEnumProp("selection", this._selection); }
            if (IsPropDirty("ShowWeekNumbers"))
            { ser.AddBooleanProp("showWeekNumbers", this._showWeekNumbers); }
            if (IsPropDirty("WeekStart"))
            { ser.AddEnumProp("weekStart", this._weekStart); }
            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("ResourceStrings"))
            { ser.AddSerializableProp("resourceStrings", this._resourceStrings); }
            if (IsPropDirty("SpecialDates"))
            { ser.AddSerializableArrayProp("specialDates", this._specialDates); }
            if (IsPropDirty("DisabledDates"))
            { ser.AddSerializableArrayProp("disabledDates", this._disabledDates); }

        }

    }
}
