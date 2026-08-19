using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The Date Range Picker includes a text input and a calendar pop-up, allowing users to easily select start and end dates.
    /// </summary>
    public partial class IgbDateRangePicker : IgbComboBoxBaseLike
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDateRangePicker"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbDateRangePickerModule.IsLoadRequested(IgBlazor))
            {
                IgbDateRangePickerModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        private IgbDateRangeValue? _value;

        /// <summary>
        /// The value of the picker.
        /// </summary>
        [Parameter]
        public IgbDateRangeValue? Value
        {
            get { return this._value; }
            set
            {
                MarkPropDirty("Value");
                if (this._value != null)
                {
                    this.DetachChild(this._value);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._value = value;
            }

        }

        /// <summary>
        /// Returns the current value of the picker.
        /// </summary>
        public async Task<IgbDateRangeValue?> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbDateRangeValue);
            }
            var retVal = (IgbDateRangeValue)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbDateRangeValue);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the current value of the picker.
        /// </summary>
        public IgbDateRangeValue? GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbDateRangeValue);
            }
            var retVal = (IgbDateRangeValue)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbDateRangeValue);
            }
            return retVal;

        }
        private IgbCustomDateRange[] _customRanges;

        /// <summary>
        /// Renders chips with custom ranges based on the elements of the array.
        /// </summary>
        [Parameter]
        public IgbCustomDateRange[] CustomRanges
        {
            get { return this._customRanges; }
            set
            {
                if (this._customRanges != value || !IsPropDirty("CustomRanges"))
                {
                    MarkPropDirty("CustomRanges");
                }
                this._customRanges = value;

            }
        }
        private PickerMode _mode = PickerMode.Dropdown;

        /// <summary>
        /// Determines whether the calendar is opened in a dropdown or a modal dialog.
        /// </summary>
        [Parameter]
        public PickerMode Mode
        {
            get { return this._mode; }
            set
            {
                if (this._mode != value || !IsPropDirty("Mode"))
                {
                    MarkPropDirty("Mode");
                }
                this._mode = value;

            }
        }
        private bool _useTwoInputs = false;

        /// <summary>
        /// Use two inputs to display the date range values. Makes the input editable in dropdown mode.
        /// </summary>
        [Parameter]
        public bool UseTwoInputs
        {
            get { return this._useTwoInputs; }
            set
            {
                if (this._useTwoInputs != value || !IsPropDirty("UseTwoInputs"))
                {
                    MarkPropDirty("UseTwoInputs");
                }
                this._useTwoInputs = value;

            }
        }
        private bool _usePredefinedRanges = false;

        /// <summary>
        /// Whether the control will show chips with predefined ranges.
        /// </summary>
        [Parameter]
        public bool UsePredefinedRanges
        {
            get { return this._usePredefinedRanges; }
            set
            {
                if (this._usePredefinedRanges != value || !IsPropDirty("UsePredefinedRanges"))
                {
                    MarkPropDirty("UsePredefinedRanges");
                }
                this._usePredefinedRanges = value;

            }
        }
        private string _locale;

        /// <summary>
        /// The locale settings used to display the value.
        /// </summary>
        [Parameter]
        public string Locale
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
        private IgbDateRangePickerResourceStrings _resourceStrings;

        /// <summary>
        /// The resource strings of the date range picker.
        /// </summary>
        [Parameter]
        public IgbDateRangePickerResourceStrings ResourceStrings
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
        private bool _readOnly = false;

        /// <summary>
        /// Makes the control a readonly field.
        /// </summary>
        [Parameter]
        [WCAttributeName("readonly")]
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set
            {
                if (this._readOnly != value || !IsPropDirty("ReadOnly"))
                {
                    MarkPropDirty("ReadOnly");
                }
                this._readOnly = value;

            }
        }
        private bool _nonEditable = false;

        /// <summary>
        /// Whether to allow typing in the input.
        /// </summary>
        [Parameter]
        public bool NonEditable
        {
            get { return this._nonEditable; }
            set
            {
                if (this._nonEditable != value || !IsPropDirty("NonEditable"))
                {
                    MarkPropDirty("NonEditable");
                }
                this._nonEditable = value;

            }
        }
        private bool _outlined = false;

        /// <summary>
        /// Whether the control will have outlined appearance.
        /// </summary>
        [Parameter]
        public bool Outlined
        {
            get { return this._outlined; }
            set
            {
                if (this._outlined != value || !IsPropDirty("Outlined"))
                {
                    MarkPropDirty("Outlined");
                }
                this._outlined = value;

            }
        }
        private string _label;

        /// <summary>
        /// The label of the control (single input).
        /// </summary>
        [Parameter]
        public string Label
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
        private string _labelStart;

        /// <summary>
        /// The label of the start input.
        /// </summary>
        [Parameter]
        public string LabelStart
        {
            get { return this._labelStart; }
            set
            {
                if (this._labelStart != value || !IsPropDirty("LabelStart"))
                {
                    MarkPropDirty("LabelStart");
                }
                this._labelStart = value;

            }
        }
        private string _labelEnd;

        /// <summary>
        /// The label of the end input.
        /// </summary>
        [Parameter]
        public string LabelEnd
        {
            get { return this._labelEnd; }
            set
            {
                if (this._labelEnd != value || !IsPropDirty("LabelEnd"))
                {
                    MarkPropDirty("LabelEnd");
                }
                this._labelEnd = value;

            }
        }
        private string _placeholder;

        /// <summary>
        /// The placeholder text of the control (single input).
        /// </summary>
        [Parameter]
        public string Placeholder
        {
            get { return this._placeholder; }
            set
            {
                if (this._placeholder != value || !IsPropDirty("Placeholder"))
                {
                    MarkPropDirty("Placeholder");
                }
                this._placeholder = value;

            }
        }
        private string _placeholderStart;

        /// <summary>
        /// The placeholder text of the start input.
        /// </summary>
        [Parameter]
        public string PlaceholderStart
        {
            get { return this._placeholderStart; }
            set
            {
                if (this._placeholderStart != value || !IsPropDirty("PlaceholderStart"))
                {
                    MarkPropDirty("PlaceholderStart");
                }
                this._placeholderStart = value;

            }
        }
        private string _placeholderEnd;

        /// <summary>
        /// The placeholder text of the end input.
        /// </summary>
        [Parameter]
        public string PlaceholderEnd
        {
            get { return this._placeholderEnd; }
            set
            {
                if (this._placeholderEnd != value || !IsPropDirty("PlaceholderEnd"))
                {
                    MarkPropDirty("PlaceholderEnd");
                }
                this._placeholderEnd = value;

            }
        }
        private string _prompt;

        /// <summary>
        /// The prompt symbol to use for unfilled parts of the mask.
        /// </summary>
        [Parameter]
        public string Prompt
        {
            get { return this._prompt; }
            set
            {
                if (this._prompt != value || !IsPropDirty("Prompt"))
                {
                    MarkPropDirty("Prompt");
                }
                this._prompt = value;

            }
        }
        private string _displayFormat;

        /// <summary>
        /// Format to display the value in when not editing.
        /// Defaults to the locale format if not set.
        /// </summary>
        [Parameter]
        public string DisplayFormat
        {
            get { return this._displayFormat; }
            set
            {
                if (this._displayFormat != value || !IsPropDirty("DisplayFormat"))
                {
                    MarkPropDirty("DisplayFormat");
                }
                this._displayFormat = value;

            }
        }
        private string _inputFormat;

        /// <summary>
        /// The date format to apply on the inputs.
        /// Defaults to the current locale of the client <c>Intl.DateTimeFormat</c>
        /// </summary>
        [Parameter]
        public string InputFormat
        {
            get { return this._inputFormat; }
            set
            {
                if (this._inputFormat != value || !IsPropDirty("InputFormat"))
                {
                    MarkPropDirty("InputFormat");
                }
                this._inputFormat = value;

            }
        }
        private DateTime? _min = DateTime.MinValue;

        /// <summary>
        /// The minimum value required for the date range picker to remain valid.
        /// </summary>
        [Parameter]
        public DateTime? Min
        {
            get { return this._min; }
            set
            {
                if (this._min != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._min = value;

            }
        }
        private DateTime? _max = DateTime.MinValue;

        /// <summary>
        /// The maximum value required for the date range picker to remain valid.
        /// </summary>
        [Parameter]
        public DateTime? Max
        {
            get { return this._max; }
            set
            {
                if (this._max != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._max = value;

            }
        }
        private IgbDateRangeDescriptor[] _disabledDates;

        /// <summary>
        /// Gets/sets disabled dates.
        /// </summary>
        [Parameter]
        public IgbDateRangeDescriptor[] DisabledDates
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
        private double _visibleMonths = 2;

        /// <summary>
        /// The number of months displayed in the calendar.
        /// </summary>
        [Parameter]
        public double VisibleMonths
        {
            get { return this._visibleMonths; }
            set
            {
                if (this._visibleMonths != value || !IsPropDirty("VisibleMonths"))
                {
                    MarkPropDirty("VisibleMonths");
                }
                this._visibleMonths = value;

            }
        }
        private ContentOrientation _headerOrientation = ContentOrientation.Horizontal;

        /// <summary>
        /// The orientation of the calendar header.
        /// </summary>
        [Parameter]
        public ContentOrientation HeaderOrientation
        {
            get { return this._headerOrientation; }
            set
            {
                if (this._headerOrientation != value || !IsPropDirty("HeaderOrientation"))
                {
                    MarkPropDirty("HeaderOrientation");
                }
                this._headerOrientation = value;

            }
        }
        private ContentOrientation _orientation = ContentOrientation.Horizontal;

        /// <summary>
        /// The orientation of the multiple months displayed in the calendar's days view.
        /// </summary>
        [Parameter]
        public ContentOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                if (this._orientation != value || !IsPropDirty("Orientation"))
                {
                    MarkPropDirty("Orientation");
                }
                this._orientation = value;

            }
        }
        private bool _hideHeader = false;

        /// <summary>
        /// Determines whether the calendar hides its header.
        /// </summary>
        [Parameter]
        public bool HideHeader
        {
            get { return this._hideHeader; }
            set
            {
                if (this._hideHeader != value || !IsPropDirty("HideHeader"))
                {
                    MarkPropDirty("HideHeader");
                }
                this._hideHeader = value;

            }
        }
        private DateTime _activeDate = DateTime.MinValue;

        /// <summary>
        /// Gets/Sets the date which is shown in the calendar picker and is highlighted.
        /// By default it is the current date.
        /// </summary>
        [Parameter]
        public DateTime ActiveDate
        {
            get { return this._activeDate; }
            set
            {
                if (this._activeDate != value || !IsPropDirty("ActiveDate"))
                {
                    MarkPropDirty("ActiveDate");
                }
                this._activeDate = value;

            }
        }
        private bool _showWeekNumbers = false;

        /// <summary>
        /// Whether to show the number of the week in the calendar.
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
        private bool _hideOutsideDays = false;

        /// <summary>
        /// Controls the visibility of the dates that do not belong to the current month.
        /// </summary>
        [Parameter]
        public bool HideOutsideDays
        {
            get { return this._hideOutsideDays; }
            set
            {
                if (this._hideOutsideDays != value || !IsPropDirty("HideOutsideDays"))
                {
                    MarkPropDirty("HideOutsideDays");
                }
                this._hideOutsideDays = value;

            }
        }
        private IgbDateRangeDescriptor[] _specialDates;

        /// <summary>
        /// Gets/sets special dates.
        /// </summary>
        [Parameter]
        public IgbDateRangeDescriptor[] SpecialDates
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
        private WeekDays _weekStart = WeekDays.Sunday;

        /// <summary>
        /// Sets the start day of the week for the calendar.
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
        private bool _disabled = false;

        /// <summary>
        /// The disabled state of the component.
        /// </summary>
        [Parameter]
        public bool Disabled
        {
            get { return this._disabled; }
            set
            {
                if (this._disabled != value || !IsPropDirty("Disabled"))
                {
                    MarkPropDirty("Disabled");
                }
                this._disabled = value;

            }
        }
        private bool _required = false;

        /// <summary>
        /// Makes the control a required field in a form context.
        /// </summary>
        [Parameter]
        public bool Required
        {
            get { return this._required; }
            set
            {
                if (this._required != value || !IsPropDirty("Required"))
                {
                    MarkPropDirty("Required");
                }
                this._required = value;

            }
        }
        private bool _invalid = false;

        /// <summary>
        /// Sets the control into invalid state (visual state only).
        /// </summary>
        [Parameter]
        public bool Invalid
        {
            get { return this._invalid; }
            set
            {
                if (this._invalid != value || !IsPropDirty("Invalid"))
                {
                    MarkPropDirty("Invalid");
                }
                this._invalid = value;

            }
        }

        /// <summary>
        /// Clears the input parts of the component of any user input.
        /// </summary>
        public async Task ClearAsync()
        {
            await InvokeMethod("clear", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Clears the input parts of the component of any user input.
        /// </summary>
        public void Clear()
        {
            InvokeMethodSync("clear", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Selects a date range value in the picker.
        /// </summary>
        public async Task SelectAsync(IgbDateRangeValue value)
        {
            await InvokeMethod("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
        }

        /// <summary>
        /// Selects a date range value in the picker.
        /// </summary>
        public void Select(IgbDateRangeValue value)
        {
            InvokeMethodSync("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
        }
        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public async Task<bool> ReportValidityAsync()
        {
            var iv = await InvokeMethod("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public bool ReportValidity()
        {
            var iv = InvokeMethodSync("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public async Task<bool> CheckValidityAsync()
        {
            var iv = await InvokeMethod("checkValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public bool CheckValidity()
        {
            var iv = InvokeMethodSync("checkValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public async Task SetCustomValidityAsync(String message)
        {
            await InvokeMethod("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }

        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public void SetCustomValidity(String message)
        {
            InvokeMethodSync("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }

        private EventCallback<IgbDateRangeValue?>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<IgbDateRangeValue?> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<IgbDateRangeValue?>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbDateRangeValue?>.Empty))
                {
                    if (!CompareEventCallbacks(value, _valueChanged, ref eventCallbacksCache))
                    {
                        this.EnsureChangeHandled();

                        _valueChanged = value;
                    }
                }
                else
                {
                    _valueChanged = null;
                }
            }
        }

        private string _openingRef = null;
        private string _openingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Opening"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string OpeningScript
        {

            set
            {
                if (value != this._openingScript)
                {
                    this._openingScript = value;
                    this.OnRefChanged("Opening", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._openingRef = refName;
                        this.MarkPropDirty("OpeningRef");
                    });
                }
            }
            get
            {
                return this._openingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _opening = null;

        /// <summary>
        /// Emitted just before the calendar popover is shown.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Opening
        {
            get
            {
                return this._opening != null ? this._opening.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _opening, ref eventCallbacksCache))
                    {
                        _opening = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", value);
                        this.OnRefChanged("Opening", null, "event:::Opening", true, false, (refName, oldValue, newValue) =>
                        {
                            this._openingRef = refName;
                            this.MarkPropDirty("OpeningRef");
                        });
                    }
                }
                else
                {
                    _opening = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", null);
                    this.OnRefChanged("Opening", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._openingRef = null;
                        this.MarkPropDirty("OpeningRef");
                    });
                }
            }
        }

        private string _openedRef = null;
        private string _openedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Opened"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string OpenedScript
        {

            set
            {
                if (value != this._openedScript)
                {
                    this._openedScript = value;
                    this.OnRefChanged("Opened", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._openedRef = refName;
                        this.MarkPropDirty("OpenedRef");
                    });
                }
            }
            get
            {
                return this._openedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _opened = null;

        /// <summary>
        /// Emitted after the calendar popover is shown.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Opened
        {
            get
            {
                return this._opened != null ? this._opened.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _opened, ref eventCallbacksCache))
                    {
                        _opened = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", value);
                        this.OnRefChanged("Opened", null, "event:::Opened", true, false, (refName, oldValue, newValue) =>
                        {
                            this._openedRef = refName;
                            this.MarkPropDirty("OpenedRef");
                        });
                    }
                }
                else
                {
                    _opened = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", null);
                    this.OnRefChanged("Opened", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._openedRef = null;
                        this.MarkPropDirty("OpenedRef");
                    });
                }
            }
        }

        private string _closingRef = null;
        private string _closingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ClosingScript
        {

            set
            {
                if (value != this._closingScript)
                {
                    this._closingScript = value;
                    this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._closingRef = refName;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
            get
            {
                return this._closingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closing = null;

        /// <summary>
        /// Emitted just before the calendar popover is hidden.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closing
        {
            get
            {
                return this._closing != null ? this._closing.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _closing, ref eventCallbacksCache))
                    {
                        _closing = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value);
                        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closingRef = refName;
                            this.MarkPropDirty("ClosingRef");
                        });
                    }
                }
                else
                {
                    _closing = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
                    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closingRef = null;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
        }

        private string _closedRef = null;
        private string _closedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closed"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ClosedScript
        {

            set
            {
                if (value != this._closedScript)
                {
                    this._closedScript = value;
                    this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._closedRef = refName;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
            get
            {
                return this._closedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closed = null;

        /// <summary>
        /// Emitted after the calendar popover is hidden.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closed
        {
            get
            {
                return this._closed != null ? this._closed.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _closed, ref eventCallbacksCache))
                    {
                        _closed = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value);
                        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closedRef = refName;
                            this.MarkPropDirty("ClosedRef");
                        });
                    }
                }
                else
                {
                    _closed = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
                    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closedRef = null;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
        }

        private string _changeRef = null;
        private string _changeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Change"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ChangeScript
        {

            set
            {
                if (value != this._changeScript)
                {
                    this._changeScript = value;
                    this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._changeRef = refName;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
            get
            {
                return this._changeScript;
            }
        }

        private EventCallback<IgbDateRangeValueEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the user modifies and commits the value of the component.
        /// </summary>
        [Parameter]
        public EventCallback<IgbDateRangeValueEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbDateRangeValueEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbDateRangeValueEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
                    {
                        _change = value;
                        this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueValue = default(IgbDateRangeValue?);

                            {
                                newValueValue = JsonSerializer.Deserialize<IgbDateRangeValue?>(JsonSerializer.Serialize(args.Detail, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }));

                                if (newValueValue != null)
                                {
                                    this.AttachChild(newValueValue);
                                }
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Value = newValueValue;
                                }
                                else
                                {
                                    this._value = newValueValue;
                                }
                                OnPropertyPropagatedOut(Name, "Value");
                            }

                            if (!EventCallback<IgbDateRangeValue?>.Empty.Equals(ValueChanged))
                            {
                                var task = ValueChanged.InvokeAsync(newValueValue);
                                if (task.Exception != null)
                                {
                                    throw task.Exception;
                                }
                            }

                        });
                        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) =>
                        {
                            this._changeRef = refName;
                            this.MarkPropDirty("ChangeRef");
                        });
                    }
                }
                else
                {
                    _change = null;
                    this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Change", null);
                    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._changeRef = null;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
        }
        internal void EnsureChangeHandled()
        {
            if (EventCallback<IgbDateRangeValueEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbDateRangeValueEventArgs>(null, (Action<IgbDateRangeValueEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        private string _inputRef = null;
        private string _inputScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Input"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string InputScript
        {

            set
            {
                if (value != this._inputScript)
                {
                    this._inputScript = value;
                    this.OnRefChanged("Input", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputRef = refName;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
            get
            {
                return this._inputScript;
            }
        }

        private EventCallback<IgbDateRangeValueEventArgs>? _input = null;

        /// <summary>
        /// Emitted when the user types in the component.
        /// </summary>
        [Parameter]
        public EventCallback<IgbDateRangeValueEventArgs> Input
        {
            get
            {
                return this._input != null ? this._input.Value : EventCallback<IgbDateRangeValueEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbDateRangeValueEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _input, ref eventCallbacksCache))
                    {
                        _input = value;
                        this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Input", value);
                        this.OnRefChanged("Input", null, "event:::Input", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputRef = refName;
                            this.MarkPropDirty("InputRef");
                        });
                    }
                }
                else
                {
                    _input = null;
                    this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Input", null);
                    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputRef = null;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddSerializableProp("value", this._value); }
            if (IsPropDirty("CustomRanges"))
            { ser.AddSerializableArrayProp("customRanges", this._customRanges); }
            if (IsPropDirty("Mode"))
            { ser.AddEnumProp("mode", this._mode); }
            if (IsPropDirty("UseTwoInputs"))
            { ser.AddBooleanProp("useTwoInputs", this._useTwoInputs); }
            if (IsPropDirty("UsePredefinedRanges"))
            { ser.AddBooleanProp("usePredefinedRanges", this._usePredefinedRanges); }
            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("ResourceStrings"))
            { ser.AddSerializableProp("resourceStrings", this._resourceStrings); }
            if (IsPropDirty("ReadOnly"))
            { ser.AddBooleanProp("readOnly", this._readOnly); }
            if (IsPropDirty("NonEditable"))
            { ser.AddBooleanProp("nonEditable", this._nonEditable); }
            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("LabelStart"))
            { ser.AddStringProp("labelStart", this._labelStart); }
            if (IsPropDirty("LabelEnd"))
            { ser.AddStringProp("labelEnd", this._labelEnd); }
            if (IsPropDirty("Placeholder"))
            { ser.AddStringProp("placeholder", this._placeholder); }
            if (IsPropDirty("PlaceholderStart"))
            { ser.AddStringProp("placeholderStart", this._placeholderStart); }
            if (IsPropDirty("PlaceholderEnd"))
            { ser.AddStringProp("placeholderEnd", this._placeholderEnd); }
            if (IsPropDirty("Prompt"))
            { ser.AddStringProp("prompt", this._prompt); }
            if (IsPropDirty("DisplayFormat"))
            { ser.AddStringProp("displayFormat", this._displayFormat); }
            if (IsPropDirty("InputFormat"))
            { ser.AddStringProp("inputFormat", this._inputFormat); }
            if (IsPropDirty("Min"))
            { ser.AddDateTimeProp("min", this._min); }
            if (IsPropDirty("Max"))
            { ser.AddDateTimeProp("max", this._max); }
            if (IsPropDirty("DisabledDates"))
            { ser.AddSerializableArrayProp("disabledDates", this._disabledDates); }
            if (IsPropDirty("VisibleMonths"))
            { ser.AddNumberProp("visibleMonths", this._visibleMonths); }
            if (IsPropDirty("HeaderOrientation"))
            { ser.AddEnumProp("headerOrientation", this._headerOrientation); }
            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }
            if (IsPropDirty("HideHeader"))
            { ser.AddBooleanProp("hideHeader", this._hideHeader); }
            if (IsPropDirty("ActiveDate"))
            { ser.AddDateTimeProp("activeDate", this._activeDate); }
            if (IsPropDirty("ShowWeekNumbers"))
            { ser.AddBooleanProp("showWeekNumbers", this._showWeekNumbers); }
            if (IsPropDirty("HideOutsideDays"))
            { ser.AddBooleanProp("hideOutsideDays", this._hideOutsideDays); }
            if (IsPropDirty("SpecialDates"))
            { ser.AddSerializableArrayProp("specialDates", this._specialDates); }
            if (IsPropDirty("WeekStart"))
            { ser.AddEnumProp("weekStart", this._weekStart); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("OpeningRef"))
            { ser.AddStringProp("openingRef", this._openingRef); }
            if (IsPropDirty("OpenedRef"))
            { ser.AddStringProp("openedRef", this._openedRef); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }
            if (IsPropDirty("InputRef"))
            { ser.AddStringProp("inputRef", this._inputRef); }

        }

    }
}
