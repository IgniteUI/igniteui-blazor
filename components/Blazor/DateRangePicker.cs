
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The igc-date-range-picker allows the user to select a range of dates.
/// </summary>
public partial class IgbDateRangePicker: IgbBaseComboBoxLike {
                                public override string Type { get { return "WebDateRangePicker"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDateRangePickerModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDateRangePickerModule.Register(IgBlazor);
                                    }
                                }

                            protected override string ResolveDisplay()
                        {
	                        return "inline-block";
                        }

                            protected override bool SupportsVisualChildren
                        {
                                get 
                                {
	                            return true;
                                }
                        }
	
	    public IgbDateRangePicker(): base() {
	        OnCreatedIgbDateRangePicker();
	
	        
	    }
	
	    partial void OnCreatedIgbDateRangePicker();
	    
	private IgbDateRangeValue? _value;
	
	partial void OnValueChanging(ref IgbDateRangeValue? newValue);
	/// <summary>
	/// The value of the picker
	/// </summary>
	[Parameter]
	public IgbDateRangeValue? Value 
	{
	get { return this._value; }
	set { 
	                        OnValueChanging(ref value);
	                        MarkPropDirty("Value"); 
	                        if (this._value != null) {
	                            this.DetachChild(this._value);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._value = value; 
	                    }
	                    
	}
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
	
	partial void OnCustomRangesChanging(ref IgbCustomDateRange[] newValue);
	/// <summary>
	/// Renders chips with custom ranges based on the elements of the array.
	/// </summary>
	[Parameter]
	public IgbCustomDateRange[] CustomRanges 
	{
	get { return this._customRanges; }
	set { 
	                if (this._customRanges != value || !IsPropDirty("CustomRanges")) {
	                        MarkPropDirty("CustomRanges");
	                } 
	                this._customRanges = value;
	                 
	                }
	}
	private PickerMode _mode = PickerMode.Dropdown;
	
	partial void OnModeChanging(ref PickerMode newValue);
	/// <summary>
	/// Determines whether the calendar is opened in a dropdown or a modal dialog
	/// </summary>
	[Parameter]
	public PickerMode Mode 
	{
	get { return this._mode; }
	set { 
	                if (this._mode != value || !IsPropDirty("Mode")) {
	                        MarkPropDirty("Mode");
	                } 
	                this._mode = value;
	                 
	                }
	}
	private bool _useTwoInputs = false;
	
	partial void OnUseTwoInputsChanging(ref bool newValue);
	/// <summary>
	/// Use two inputs to display the date range values. Makes the input editable in dropdown mode.
	/// </summary>
	[Parameter]
	public bool UseTwoInputs 
	{
	get { return this._useTwoInputs; }
	set { 
	                if (this._useTwoInputs != value || !IsPropDirty("UseTwoInputs")) {
	                        MarkPropDirty("UseTwoInputs");
	                } 
	                this._useTwoInputs = value;
	                 
	                }
	}
	private bool _usePredefinedRanges = false;
	
	partial void OnUsePredefinedRangesChanging(ref bool newValue);
	/// <summary>
	/// Whether the control will show chips with predefined ranges.
	/// </summary>
	[Parameter]
	public bool UsePredefinedRanges 
	{
	get { return this._usePredefinedRanges; }
	set { 
	                if (this._usePredefinedRanges != value || !IsPropDirty("UsePredefinedRanges")) {
	                        MarkPropDirty("UsePredefinedRanges");
	                } 
	                this._usePredefinedRanges = value;
	                 
	                }
	}
	private string _locale;
	
	partial void OnLocaleChanging(ref string newValue);
	/// <summary>
	/// The locale settings used to display the value.
	/// </summary>
	[Parameter]
	public string Locale 
	{
	get { return this._locale; }
	set { 
	                if (this._locale != value || !IsPropDirty("Locale")) {
	                        MarkPropDirty("Locale");
	                } 
	                this._locale = value;
	                 
	                }
	}
	private IgbDateRangePickerResourceStrings _resourceStrings;
	
	partial void OnResourceStringsChanging(ref IgbDateRangePickerResourceStrings newValue);
	/// <summary>
	/// The resource strings of the date range picker.
	/// </summary>
	[Parameter]
	public IgbDateRangePickerResourceStrings ResourceStrings 
	{
	get { return this._resourceStrings; }
	set { 
	                        OnResourceStringsChanging(ref value);
	                        MarkPropDirty("ResourceStrings"); 
	                        if (this._resourceStrings != null) {
	                            this.DetachChild(this._resourceStrings);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._resourceStrings = value; 
	                    }
	                    
	}
	private bool _readOnly = false;
	
	partial void OnReadOnlyChanging(ref bool newValue);
	/// <summary>
	/// Makes the control a readonly field.
	/// </summary>
	[Parameter]
	[WCAttributeName("readonly")]
	public bool ReadOnly 
	{
	get { return this._readOnly; }
	set { 
	                if (this._readOnly != value || !IsPropDirty("ReadOnly")) {
	                        MarkPropDirty("ReadOnly");
	                } 
	                this._readOnly = value;
	                 
	                }
	}
	private bool _nonEditable = false;
	
	partial void OnNonEditableChanging(ref bool newValue);
	/// <summary>
	/// Whether to allow typing in the input.
	/// </summary>
	[Parameter]
	public bool NonEditable 
	{
	get { return this._nonEditable; }
	set { 
	                if (this._nonEditable != value || !IsPropDirty("NonEditable")) {
	                        MarkPropDirty("NonEditable");
	                } 
	                this._nonEditable = value;
	                 
	                }
	}
	private bool _outlined = false;
	
	partial void OnOutlinedChanging(ref bool newValue);
	/// <summary>
	/// Whether the control will have outlined appearance.
	/// </summary>
	[Parameter]
	public bool Outlined 
	{
	get { return this._outlined; }
	set { 
	                if (this._outlined != value || !IsPropDirty("Outlined")) {
	                        MarkPropDirty("Outlined");
	                } 
	                this._outlined = value;
	                 
	                }
	}
	private string _label;
	
	partial void OnLabelChanging(ref string newValue);
	/// <summary>
	/// The label of the control (single input).
	/// </summary>
	[Parameter]
	public string Label 
	{
	get { return this._label; }
	set { 
	                if (this._label != value || !IsPropDirty("Label")) {
	                        MarkPropDirty("Label");
	                } 
	                this._label = value;
	                 
	                }
	}
	private string _labelStart;
	
	partial void OnLabelStartChanging(ref string newValue);
	/// <summary>
	/// The label attribute of the start input.
	/// </summary>
	[Parameter]
	public string LabelStart 
	{
	get { return this._labelStart; }
	set { 
	                if (this._labelStart != value || !IsPropDirty("LabelStart")) {
	                        MarkPropDirty("LabelStart");
	                } 
	                this._labelStart = value;
	                 
	                }
	}
	private string _labelEnd;
	
	partial void OnLabelEndChanging(ref string newValue);
	/// <summary>
	/// The label attribute of the end input.
	/// </summary>
	[Parameter]
	public string LabelEnd 
	{
	get { return this._labelEnd; }
	set { 
	                if (this._labelEnd != value || !IsPropDirty("LabelEnd")) {
	                        MarkPropDirty("LabelEnd");
	                } 
	                this._labelEnd = value;
	                 
	                }
	}
	private string _placeholder;
	
	partial void OnPlaceholderChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the control (single input).
	/// </summary>
	[Parameter]
	public string Placeholder 
	{
	get { return this._placeholder; }
	set { 
	                if (this._placeholder != value || !IsPropDirty("Placeholder")) {
	                        MarkPropDirty("Placeholder");
	                } 
	                this._placeholder = value;
	                 
	                }
	}
	private string _placeholderStart;
	
	partial void OnPlaceholderStartChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the start input.
	/// </summary>
	[Parameter]
	public string PlaceholderStart 
	{
	get { return this._placeholderStart; }
	set { 
	                if (this._placeholderStart != value || !IsPropDirty("PlaceholderStart")) {
	                        MarkPropDirty("PlaceholderStart");
	                } 
	                this._placeholderStart = value;
	                 
	                }
	}
	private string _placeholderEnd;
	
	partial void OnPlaceholderEndChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the end input.
	/// </summary>
	[Parameter]
	public string PlaceholderEnd 
	{
	get { return this._placeholderEnd; }
	set { 
	                if (this._placeholderEnd != value || !IsPropDirty("PlaceholderEnd")) {
	                        MarkPropDirty("PlaceholderEnd");
	                } 
	                this._placeholderEnd = value;
	                 
	                }
	}
	private string _prompt;
	
	partial void OnPromptChanging(ref string newValue);
	/// <summary>
	/// The prompt symbol to use for unfilled parts of the mask.
	/// </summary>
	[Parameter]
	public string Prompt 
	{
	get { return this._prompt; }
	set { 
	                if (this._prompt != value || !IsPropDirty("Prompt")) {
	                        MarkPropDirty("Prompt");
	                } 
	                this._prompt = value;
	                 
	                }
	}
	private string _displayFormat;
	
	partial void OnDisplayFormatChanging(ref string newValue);
	/// <summary>
	/// Format to display the value in when not editing.
	/// Defaults to the input format if not set.
	/// </summary>
	[Parameter]
	public string DisplayFormat 
	{
	get { return this._displayFormat; }
	set { 
	                if (this._displayFormat != value || !IsPropDirty("DisplayFormat")) {
	                        MarkPropDirty("DisplayFormat");
	                } 
	                this._displayFormat = value;
	                 
	                }
	}
	private string _inputFormat;
	
	partial void OnInputFormatChanging(ref string newValue);
	/// <summary>
	/// The date format to apply on the inputs.
	/// Defaults to the current locale Intl.DateTimeFormat
	/// </summary>
	[Parameter]
	public string InputFormat 
	{
	get { return this._inputFormat; }
	set { 
	                if (this._inputFormat != value || !IsPropDirty("InputFormat")) {
	                        MarkPropDirty("InputFormat");
	                } 
	                this._inputFormat = value;
	                 
	                }
	}
	private DateTime? _min = DateTime.MinValue;
	
	partial void OnMinChanging(ref DateTime? newValue);
	/// <summary>
	/// The minimum value required for the date range picker to remain valid.
	/// </summary>
	[Parameter]
	public DateTime? Min 
	{
	get { return this._min; }
	set { 
	                if (this._min != value || !IsPropDirty("Min")) {
	                        MarkPropDirty("Min");
	                } 
	                this._min = value;
	                 
	                }
	}
	private DateTime? _max = DateTime.MinValue;
	
	partial void OnMaxChanging(ref DateTime? newValue);
	/// <summary>
	/// The maximum value required for the date range picker to remain valid.
	/// </summary>
	[Parameter]
	public DateTime? Max 
	{
	get { return this._max; }
	set { 
	                if (this._max != value || !IsPropDirty("Max")) {
	                        MarkPropDirty("Max");
	                } 
	                this._max = value;
	                 
	                }
	}
	private IgbDateRangeDescriptor[] _disabledDates;
	
	partial void OnDisabledDatesChanging(ref IgbDateRangeDescriptor[] newValue);
	/// <summary>
	/// Gets/sets disabled dates.
	/// </summary>
	[Parameter]
	public IgbDateRangeDescriptor[] DisabledDates 
	{
	get { return this._disabledDates; }
	set { 
	                if (this._disabledDates != value || !IsPropDirty("DisabledDates")) {
	                        MarkPropDirty("DisabledDates");
	                } 
	                this._disabledDates = value;
	                 
	                }
	}
	private double _visibleMonths = 0;
	
	partial void OnVisibleMonthsChanging(ref double newValue);
	[Parameter]
	public double VisibleMonths 
	{
	get { return this._visibleMonths; }
	set { 
	                if (this._visibleMonths != value || !IsPropDirty("VisibleMonths")) {
	                        MarkPropDirty("VisibleMonths");
	                } 
	                this._visibleMonths = value;
	                 
	                }
	}
	private ContentOrientation _headerOrientation = ContentOrientation.Horizontal;
	
	partial void OnHeaderOrientationChanging(ref ContentOrientation newValue);
	/// <summary>
	/// The orientation of the calendar header.
	/// </summary>
	[Parameter]
	public ContentOrientation HeaderOrientation 
	{
	get { return this._headerOrientation; }
	set { 
	                if (this._headerOrientation != value || !IsPropDirty("HeaderOrientation")) {
	                        MarkPropDirty("HeaderOrientation");
	                } 
	                this._headerOrientation = value;
	                 
	                }
	}
	private ContentOrientation _orientation = ContentOrientation.Horizontal;
	
	partial void OnOrientationChanging(ref ContentOrientation newValue);
	/// <summary>
	/// The orientation of the multiple months displayed in the calendar's days view.
	/// </summary>
	[Parameter]
	public ContentOrientation Orientation 
	{
	get { return this._orientation; }
	set { 
	                if (this._orientation != value || !IsPropDirty("Orientation")) {
	                        MarkPropDirty("Orientation");
	                } 
	                this._orientation = value;
	                 
	                }
	}
	private bool _hideHeader = false;
	
	partial void OnHideHeaderChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the calendar hides its header.
	/// </summary>
	[Parameter]
	public bool HideHeader 
	{
	get { return this._hideHeader; }
	set { 
	                if (this._hideHeader != value || !IsPropDirty("HideHeader")) {
	                        MarkPropDirty("HideHeader");
	                } 
	                this._hideHeader = value;
	                 
	                }
	}
	private DateTime _activeDate = DateTime.MinValue;
	
	partial void OnActiveDateChanging(ref DateTime newValue);
	/// <summary>
	/// Gets/Sets the date which is shown in the calendar picker and is highlighted.
	/// By default it is the current date.
	/// </summary>
	[Parameter]
	public DateTime ActiveDate 
	{
	get { return this._activeDate; }
	set { 
	                if (this._activeDate != value || !IsPropDirty("ActiveDate")) {
	                        MarkPropDirty("ActiveDate");
	                } 
	                this._activeDate = value;
	                 
	                }
	}
	private bool _showWeekNumbers = false;
	
	partial void OnShowWeekNumbersChanging(ref bool newValue);
	/// <summary>
	/// Whether to show the number of the week in the calendar.
	/// </summary>
	[Parameter]
	public bool ShowWeekNumbers 
	{
	get { return this._showWeekNumbers; }
	set { 
	                if (this._showWeekNumbers != value || !IsPropDirty("ShowWeekNumbers")) {
	                        MarkPropDirty("ShowWeekNumbers");
	                } 
	                this._showWeekNumbers = value;
	                 
	                }
	}
	private bool _hideOutsideDays = false;
	
	partial void OnHideOutsideDaysChanging(ref bool newValue);
	/// <summary>
	/// Controls the visibility of the dates that do not belong to the current month.
	/// </summary>
	[Parameter]
	public bool HideOutsideDays 
	{
	get { return this._hideOutsideDays; }
	set { 
	                if (this._hideOutsideDays != value || !IsPropDirty("HideOutsideDays")) {
	                        MarkPropDirty("HideOutsideDays");
	                } 
	                this._hideOutsideDays = value;
	                 
	                }
	}
	private IgbDateRangeDescriptor[] _specialDates;
	
	partial void OnSpecialDatesChanging(ref IgbDateRangeDescriptor[] newValue);
	/// <summary>
	/// Gets/sets special dates.
	/// </summary>
	[Parameter]
	public IgbDateRangeDescriptor[] SpecialDates 
	{
	get { return this._specialDates; }
	set { 
	                if (this._specialDates != value || !IsPropDirty("SpecialDates")) {
	                        MarkPropDirty("SpecialDates");
	                } 
	                this._specialDates = value;
	                 
	                }
	}
	private WeekDays _weekStart = WeekDays.Sunday;
	
	partial void OnWeekStartChanging(ref WeekDays newValue);
	/// <summary>
	/// Sets the start day of the week for the calendar.
	/// </summary>
	[Parameter]
	public WeekDays WeekStart 
	{
	get { return this._weekStart; }
	set { 
	                if (this._weekStart != value || !IsPropDirty("WeekStart")) {
	                        MarkPropDirty("WeekStart");
	                } 
	                this._weekStart = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// The disabled state of the component
	/// </summary>
	[Parameter]
	public bool Disabled 
	{
	get { return this._disabled; }
	set { 
	                if (this._disabled != value || !IsPropDirty("Disabled")) {
	                        MarkPropDirty("Disabled");
	                } 
	                this._disabled = value;
	                 
	                }
	}
	private bool _required = false;
	
	partial void OnRequiredChanging(ref bool newValue);
	/// <summary>
	/// Makes the control a required field in a form context.
	/// </summary>
	[Parameter]
	public bool Required 
	{
	get { return this._required; }
	set { 
	                if (this._required != value || !IsPropDirty("Required")) {
	                        MarkPropDirty("Required");
	                } 
	                this._required = value;
	                 
	                }
	}
	private bool _invalid = false;
	
	partial void OnInvalidChanging(ref bool newValue);
	/// <summary>
	/// Control the validity of the control.
	/// </summary>
	[Parameter]
	public bool Invalid 
	{
	get { return this._invalid; }
	set { 
	                if (this._invalid != value || !IsPropDirty("Invalid")) {
	                        MarkPropDirty("Invalid");
	                } 
	                this._invalid = value;
	                 
	                }
	}
	
	    partial void FindByNameDateRangePicker(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDateRangePicker(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	/// <summary>
	/// Clears the input parts of the component of any user input
	/// </summary>
	public async  Task ClearAsync() 
	                    {
		await InvokeMethod("clear", new object[] {  }, new string[] {  });
	}
	                    public  void Clear() 
	                    {
		InvokeMethodSync("clear", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Selects a date range value in the picker
	/// </summary>
	public async  Task SelectAsync(IgbDateRangeValue value) 
	                    {
		await InvokeMethod("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
	}
	                    public  void Select(IgbDateRangeValue value) 
	                    {
		InvokeMethodSync("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
	}
	/// <summary>
	/// Checks for validity of the control and shows the browser message if it invalid.
	/// </summary>
	public async  Task ReportValidityAsync() 
	                    {
		await InvokeMethod("reportValidity", new object[] {  }, new string[] {  });
	}
	                    public  void ReportValidity() 
	                    {
		InvokeMethodSync("reportValidity", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Checks for validity of the control and emits the invalid event if it invalid.
	/// </summary>
	public async  Task CheckValidityAsync() 
	                    {
		await InvokeMethod("checkValidity", new object[] {  }, new string[] {  });
	}
	                    public  void CheckValidity() 
	                    {
		InvokeMethodSync("checkValidity", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Sets a custom validation message for the control.
	/// As long as `message` is not empty, the control is considered invalid.
	/// </summary>
	public async  Task SetCustomValidityAsync(String message) 
	                    {
		await InvokeMethod("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
	}
	                    public  void SetCustomValidity(String message) 
	                    {
		InvokeMethodSync("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
	}
	
	    private EventCallback<IgbDateRangeValue?>? _valueChanged = null;
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
	    [Parameter]
	    public string OpeningScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opening", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openingRef = refName;
	                this.MarkPropDirty("OpeningRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openingScript;
	        }
	    }
	
	    partial void OnHandlingOpening(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opening = null;
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
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", value, (args) => {
	                        OnHandlingOpening(args);
	                        
	                    });
	        this.OnRefChanged("Opening", null, "event:::Opening", true, false, (refName, oldValue, newValue) => {
	                        this._openingRef = refName;
	                        this.MarkPropDirty("OpeningRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opening = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", null);
	    this.OnRefChanged("Opening", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openingRef = null;
	                    this.MarkPropDirty("OpeningRef");	
	            });
	    }
	    }
	    }
	
	    private string _openedRef = null;
	    private string _openedScript = null;
	    [Parameter]
	    public string OpenedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opened", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openedRef = refName;
	                this.MarkPropDirty("OpenedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openedScript;
	        }
	    }
	
	    partial void OnHandlingOpened(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opened = null;
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
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", value, (args) => {
	                        OnHandlingOpened(args);
	                        
	                    });
	        this.OnRefChanged("Opened", null, "event:::Opened", true, false, (refName, oldValue, newValue) => {
	                        this._openedRef = refName;
	                        this.MarkPropDirty("OpenedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opened = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", null);
	    this.OnRefChanged("Opened", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openedRef = null;
	                    this.MarkPropDirty("OpenedRef");	
	            });
	    }
	    }
	    }
	
	    private string _closingRef = null;
	    private string _closingScript = null;
	    [Parameter]
	    public string ClosingScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closingRef = refName;
	                this.MarkPropDirty("ClosingRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closingScript;
	        }
	    }
	
	    partial void OnHandlingClosing(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closing = null;
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
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value, (args) => {
	                        OnHandlingClosing(args);
	                        
	                    });
	        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) => {
	                        this._closingRef = refName;
	                        this.MarkPropDirty("ClosingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closing = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
	    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closingRef = null;
	                    this.MarkPropDirty("ClosingRef");	
	            });
	    }
	    }
	    }
	
	    private string _closedRef = null;
	    private string _closedScript = null;
	    [Parameter]
	    public string ClosedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closedRef = refName;
	                this.MarkPropDirty("ClosedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closedScript;
	        }
	    }
	
	    partial void OnHandlingClosed(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closed = null;
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
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value, (args) => {
	                        OnHandlingClosed(args);
	                        
	                    });
	        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) => {
	                        this._closedRef = refName;
	                        this.MarkPropDirty("ClosedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closed = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
	    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closedRef = null;
	                    this.MarkPropDirty("ClosedRef");	
	            });
	    }
	    }
	    }
	
	    private string _changeRef = null;
	    private string _changeScript = null;
	    [Parameter]
	    public string ChangeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._changeRef = refName;
	                this.MarkPropDirty("ChangeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._changeScript;
	        }
	    }
	
	    partial void OnHandlingChange(IgbDateRangeValueEventArgs args);
	    private EventCallback<IgbDateRangeValueEventArgs>? _change = null;
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
	                    this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(IgbDateRangeValue?);
	
	    
	    {
	        newValueValue = JsonSerializer.Deserialize<IgbDateRangeValue?>(JsonSerializer.Serialize(args.Detail, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }));
	        
	                            if (newValueValue != null)
	                            {
	                                this.AttachChild(newValueValue);
	                            };
	        OnEventUpdatingValue(this._value, ref newValueValue);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Value = newValueValue;
	        } else {
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
	        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) => {
	                        this._changeRef = refName;
	                        this.MarkPropDirty("ChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _change = null;
	                this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
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
	            this.Change = new EventCallback<IgbDateRangeValueEventArgs>(null, (Action<IgbDateRangeValueEventArgs>)((e) => { })); this._change = null;        
	        }
	    }
	
	
	    private string _inputRef = null;
	    private string _inputScript = null;
	    [Parameter]
	    public string InputScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Input", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._inputRef = refName;
	                this.MarkPropDirty("InputRef");	
	        }); 
	        }
	        get 
	        {
	            return this._inputScript;
	        }
	    }
	
	    partial void OnHandlingInput(IgbDateRangeValueEventArgs args);
	    private EventCallback<IgbDateRangeValueEventArgs>? _input = null;
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
	                    this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Input", value, (args) => {
	                        OnHandlingInput(args);
	                        
	                    });
	        this.OnRefChanged("Input", null, "event:::Input", true, false, (refName, oldValue, newValue) => {
	                        this._inputRef = refName;
	                        this.MarkPropDirty("InputRef");	
	                });
	                }
	    }
	        else 
	            {
	                _input = null;
	                this.SetHandler<IgbDateRangeValueEventArgs>(this.Name, "Input", null);
	    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputRef = null;
	                    this.MarkPropDirty("InputRef");	
	            });
	    }
	    }
	    }
	
	                            partial void OnEventUpdatingValue(IgbDateRangeValue? oldValue, ref IgbDateRangeValue? newValue);
	
	    partial void SerializeCoreIgbDateRangePicker(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateRangePicker(ser);
	
	if (IsPropDirty("Value")) { ser.AddSerializableProp("value", this._value); }
	if (IsPropDirty("CustomRanges")) { ser.AddSerializableArrayProp("customRanges", this._customRanges); }
	if (IsPropDirty("Mode")) { ser.AddEnumProp("mode", this._mode); }
	if (IsPropDirty("UseTwoInputs")) { ser.AddBooleanProp("useTwoInputs", this._useTwoInputs); }
	if (IsPropDirty("UsePredefinedRanges")) { ser.AddBooleanProp("usePredefinedRanges", this._usePredefinedRanges); }
	if (IsPropDirty("Locale")) { ser.AddStringProp("locale", this._locale); }
	if (IsPropDirty("ResourceStrings")) { ser.AddSerializableProp("resourceStrings", this._resourceStrings); }
	if (IsPropDirty("ReadOnly")) { ser.AddBooleanProp("readOnly", this._readOnly); }
	if (IsPropDirty("NonEditable")) { ser.AddBooleanProp("nonEditable", this._nonEditable); }
	if (IsPropDirty("Outlined")) { ser.AddBooleanProp("outlined", this._outlined); }
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("LabelStart")) { ser.AddStringProp("labelStart", this._labelStart); }
	if (IsPropDirty("LabelEnd")) { ser.AddStringProp("labelEnd", this._labelEnd); }
	if (IsPropDirty("Placeholder")) { ser.AddStringProp("placeholder", this._placeholder); }
	if (IsPropDirty("PlaceholderStart")) { ser.AddStringProp("placeholderStart", this._placeholderStart); }
	if (IsPropDirty("PlaceholderEnd")) { ser.AddStringProp("placeholderEnd", this._placeholderEnd); }
	if (IsPropDirty("Prompt")) { ser.AddStringProp("prompt", this._prompt); }
	if (IsPropDirty("DisplayFormat")) { ser.AddStringProp("displayFormat", this._displayFormat); }
	if (IsPropDirty("InputFormat")) { ser.AddStringProp("inputFormat", this._inputFormat); }
	if (IsPropDirty("Min")) { ser.AddDateTimeProp("min", this._min); }
	if (IsPropDirty("Max")) { ser.AddDateTimeProp("max", this._max); }
	if (IsPropDirty("DisabledDates")) { ser.AddSerializableArrayProp("disabledDates", this._disabledDates); }
	if (IsPropDirty("VisibleMonths")) { ser.AddNumberProp("visibleMonths", this._visibleMonths); }
	if (IsPropDirty("HeaderOrientation")) { ser.AddEnumProp("headerOrientation", this._headerOrientation); }
	if (IsPropDirty("Orientation")) { ser.AddEnumProp("orientation", this._orientation); }
	if (IsPropDirty("HideHeader")) { ser.AddBooleanProp("hideHeader", this._hideHeader); }
	if (IsPropDirty("ActiveDate")) { ser.AddDateTimeProp("activeDate", this._activeDate); }
	if (IsPropDirty("ShowWeekNumbers")) { ser.AddBooleanProp("showWeekNumbers", this._showWeekNumbers); }
	if (IsPropDirty("HideOutsideDays")) { ser.AddBooleanProp("hideOutsideDays", this._hideOutsideDays); }
	if (IsPropDirty("SpecialDates")) { ser.AddSerializableArrayProp("specialDates", this._specialDates); }
	if (IsPropDirty("WeekStart")) { ser.AddEnumProp("weekStart", this._weekStart); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Required")) { ser.AddBooleanProp("required", this._required); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("OpeningRef")) { ser.AddStringProp("openingRef", this._openingRef); }
	if (IsPropDirty("OpenedRef")) { ser.AddStringProp("openedRef", this._openedRef); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("InputRef")) { ser.AddStringProp("inputRef", this._inputRef); }
	
	    }
	
}
}
