
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// This element represents a multi-line plain-text editing control,
/// useful when you want to allow users to enter a sizeable amount of free-form text,
/// for example a comment on a review or feedback form.
/// </summary>
public partial class IgbTextarea: BaseRendererControl {
                                public override string Type { get { return "WebTextarea"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTextareaModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTextareaModule.Register(IgBlazor);
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

                            protected override bool UseDirectRender
                        {
                                get 
                                {
                            return true;
                                }
                        }

                            protected override string DirectRenderElementName
                        {
                                get 
                                {
                            return "igc-textarea";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbTextarea(): base() {
	        OnCreatedIgbTextarea();
	
	        
	    }
	
	    partial void OnCreatedIgbTextarea();
	    
	private string _autocomplete;
	
	partial void OnAutocompleteChanging(ref string newValue);
	/// <summary>
	/// Specifies what if any permission the browser has to provide for automated assistance in filling out form field values,
	/// as well as guidance to the browser as to the type of information expected in the field.
	/// Refer to [this page](https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes/autocomplete) for additional information.
	/// </summary>
	[Parameter]
	public string Autocomplete 
	{
	get { return this._autocomplete; }
	set { 
	                if (this._autocomplete != value || !IsPropDirty("Autocomplete")) {
	                        MarkPropDirty("Autocomplete");
	                } 
	                this._autocomplete = value;
	                 
	                }
	}
	private string _autocapitalize;
	
	partial void OnAutocapitalizeChanging(ref string newValue);
	/// <summary>
	/// Controls whether and how text input is automatically capitalized as it is entered/edited by the user.
	/// [MDN documentation](https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/autocapitalize).
	/// </summary>
	[Parameter]
	public string Autocapitalize 
	{
	get { return this._autocapitalize; }
	set { 
	                if (this._autocapitalize != value || !IsPropDirty("Autocapitalize")) {
	                        MarkPropDirty("Autocapitalize");
	                } 
	                this._autocapitalize = value;
	                 
	                }
	}
	private string _inputMode;
	
	partial void OnInputModeChanging(ref string newValue);
	/// <summary>
	/// Hints at the type of data that might be entered by the user while editing the element or its contents.
	/// This allows a browser to display an appropriate virtual keyboard.
	/// [MDN documentation](https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/inputmode)
	/// </summary>
	[Parameter]
	[WCAttributeName("inputmode")]
	public string InputMode 
	{
	get { return this._inputMode; }
	set { 
	                if (this._inputMode != value || !IsPropDirty("InputMode")) {
	                        MarkPropDirty("InputMode");
	                } 
	                this._inputMode = value;
	                 
	                }
	}
	private string _label;
	
	partial void OnLabelChanging(ref string newValue);
	/// <summary>
	/// The label for the control.
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
	private double _maxLength = 0;
	
	partial void OnMaxLengthChanging(ref double newValue);
	/// <summary>
	/// The maximum number of characters (UTF-16 code units) that the user can enter.
	/// If this value isn't specified, the user can enter an unlimited number of characters.
	/// </summary>
	[Parameter]
	[WCAttributeName("maxlength")]
	public double MaxLength 
	{
	get { return this._maxLength; }
	set { 
	                if (this._maxLength != value || !IsPropDirty("MaxLength")) {
	                        MarkPropDirty("MaxLength");
	                } 
	                this._maxLength = value;
	                 
	                }
	}
	private double _minLength = 0;
	
	partial void OnMinLengthChanging(ref double newValue);
	/// <summary>
	/// The minimum number of characters (UTF-16 code units) required that the user should enter.
	/// </summary>
	[Parameter]
	[WCAttributeName("minlength")]
	public double MinLength 
	{
	get { return this._minLength; }
	set { 
	                if (this._minLength != value || !IsPropDirty("MinLength")) {
	                        MarkPropDirty("MinLength");
	                } 
	                this._minLength = value;
	                 
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
	private string _placeholder;
	
	partial void OnPlaceholderChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the control.
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
	private TextareaResize _resize = TextareaResize.Vertical;
	
	partial void OnResizeChanging(ref TextareaResize newValue);
	/// <summary>
	/// Controls whether the control can be resized.
	/// When `auto` is set, the control will try to expand and fit its content.
	/// </summary>
	[Parameter]
	public TextareaResize Resize 
	{
	get { return this._resize; }
	set { 
	                if (this._resize != value || !IsPropDirty("Resize")) {
	                        MarkPropDirty("Resize");
	                } 
	                this._resize = value;
	                 
	                }
	}
	private double _rows = 0;
	
	partial void OnRowsChanging(ref double newValue);
	/// <summary>
	/// The number of visible text lines for the control. If it is specified, it must be a positive integer.
	/// If it is not specified, the default value is 2.
	/// </summary>
	[Parameter]
	public double Rows 
	{
	get { return this._rows; }
	set { 
	                if (this._rows != value || !IsPropDirty("Rows")) {
	                        MarkPropDirty("Rows");
	                } 
	                this._rows = value;
	                 
	                }
	}
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// The value of the component
	/// </summary>
	[Parameter]
	public string Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<string> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToString(iv);
	}
	                    public string GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToString(iv);
	}
	private bool _spellcheck = false;
	
	partial void OnSpellcheckChanging(ref bool newValue);
	/// <summary>
	/// Controls whether the element may be checked for spelling errors.
	/// </summary>
	[Parameter]
	public bool Spellcheck 
	{
	get { return this._spellcheck; }
	set { 
	                if (this._spellcheck != value || !IsPropDirty("Spellcheck")) {
	                        MarkPropDirty("Spellcheck");
	                } 
	                this._spellcheck = value;
	                 
	                }
	}
	private TextareaWrap _wrap = TextareaWrap.Soft;
	
	partial void OnWrapChanging(ref TextareaWrap newValue);
	/// <summary>
	/// Indicates how the control should wrap the value for form submission.
	/// Refer to [this page on MDN](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/textarea#attributes)
	/// for explanation of the available values.
	/// </summary>
	[Parameter]
	public TextareaWrap Wrap 
	{
	get { return this._wrap; }
	set { 
	                if (this._wrap != value || !IsPropDirty("Wrap")) {
	                        MarkPropDirty("Wrap");
	                } 
	                this._wrap = value;
	                 
	                }
	}
	private bool _validateOnly = false;
	
	partial void OnValidateOnlyChanging(ref bool newValue);
	/// <summary>
	/// Enables validation rules to be evaluated without restricting user input. This applies to the `maxLength` property
	/// when it is defined.
	/// </summary>
	[Parameter]
	public bool ValidateOnly 
	{
	get { return this._validateOnly; }
	set { 
	                if (this._validateOnly != value || !IsPropDirty("ValidateOnly")) {
	                        MarkPropDirty("ValidateOnly");
	                } 
	                this._validateOnly = value;
	                 
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
	
	    partial void FindByNameTextarea(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTextarea(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task SetNativeElementAsync(Object element) 
	                    {
		await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	                    public  void SetNativeElement(Object element) 
	                    {
		InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	/// <summary>
	/// Selects all text within the control.
	/// </summary>
	public async  Task SelectAsync() 
	                    {
		await InvokeMethod("select", new object[] {  }, new string[] {  });
	}
	                    public  void Select() 
	                    {
		InvokeMethodSync("select", new object[] {  }, new string[] {  });
	}
	public async  Task SetSelectionRangeAsync(double start, double end, SelectionRangeDirection direction) 
	                    {
		await InvokeMethod("setSelectionRange", new object[] { start, end, ObjectToParam(direction, typeof(SelectionRangeDirection)) }, new string[] { "Number", "Number", "Json" });
	}
	                    public  void SetSelectionRange(double start, double end, SelectionRangeDirection direction) 
	                    {
		InvokeMethodSync("setSelectionRange", new object[] { start, end, ObjectToParam(direction, typeof(SelectionRangeDirection)) }, new string[] { "Number", "Number", "Json" });
	}
	public async  Task SetRangeTextAsync(String replacement, double start, double end, RangeTextSelectMode selectMode) 
	                    {
		await InvokeMethod("setRangeText", new object[] { StringToString(replacement), start, end, ObjectToParam(selectMode, typeof(RangeTextSelectMode)) }, new string[] { "String", "Number", "Number", "Json" });
	}
	                    public  void SetRangeText(String replacement, double start, double end, RangeTextSelectMode selectMode) 
	                    {
		InvokeMethodSync("setRangeText", new object[] { StringToString(replacement), start, end, ObjectToParam(selectMode, typeof(RangeTextSelectMode)) }, new string[] { "String", "Number", "Number", "Json" });
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
	
	    private EventCallback<string>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<string> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<string>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<string>.Empty)) 
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
	
	    partial void OnHandlingInput(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _input = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> Input
	    {
	        get 
	        {
	            return this._input != null ? this._input.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _input, ref eventCallbacksCache))
	                {
	                    _input = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Input", value, (args) => {
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
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Input", null);
	    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputRef = null;
	                    this.MarkPropDirty("InputRef");	
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
	
	    partial void OnHandlingChange(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(string);
	
	    
	    {
	        newValueValue = (string)(args.Detail);
	        ;
	        OnEventUpdatingValue(this._value, ref newValueValue);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Value = newValueValue;
	        } else {
	            this._value = newValueValue;
	        }
	        OnPropertyPropagatedOut(Name, "Value");
	    }
	
	    if (!EventCallback<string>.Empty.Equals(ValueChanged))
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
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbComponentValueChangedEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbComponentValueChangedEventArgs>(null, (Action<IgbComponentValueChangedEventArgs>)((e) => { })); this._change = null;        
	        }
	    }
	
	
	    private string _focusRef = null;
	    private string _focusScript = null;
	    [Parameter]
	    public string FocusScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Focus", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._focusRef = refName;
	                this.MarkPropDirty("FocusRef");	
	        }); 
	        }
	        get 
	        {
	            return this._focusScript;
	        }
	    }
	
	    partial void OnHandlingFocus(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _focus = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Focus
	    {
	        get 
	        {
	            return this._focus != null ? this._focus.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _focus, ref eventCallbacksCache))
	                {
	                    _focus = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", value, (args) => {
	                        OnHandlingFocus(args);
	                        
	                    });
	        this.OnRefChanged("Focus", null, "nativeEvent:::Focus", true, false, (refName, oldValue, newValue) => {
	                        this._focusRef = refName;
	                        this.MarkPropDirty("FocusRef");	
	                });
	                }
	    }
	        else 
	            {
	                _focus = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", null);
	    this.OnRefChanged("Focus", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._focusRef = null;
	                    this.MarkPropDirty("FocusRef");	
	            });
	    }
	    }
	    }
	
	    private string _blurRef = null;
	    private string _blurScript = null;
	    [Parameter]
	    public string BlurScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Blur", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._blurRef = refName;
	                this.MarkPropDirty("BlurRef");	
	        }); 
	        }
	        get 
	        {
	            return this._blurScript;
	        }
	    }
	
	    partial void OnHandlingBlur(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _blur = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Blur
	    {
	        get 
	        {
	            return this._blur != null ? this._blur.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _blur, ref eventCallbacksCache))
	                {
	                    _blur = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", value, (args) => {
	                        OnHandlingBlur(args);
	                        
	                    });
	        this.OnRefChanged("Blur", null, "nativeEvent:::Blur", true, false, (refName, oldValue, newValue) => {
	                        this._blurRef = refName;
	                        this.MarkPropDirty("BlurRef");	
	                });
	                }
	    }
	        else 
	            {
	                _blur = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", null);
	    this.OnRefChanged("Blur", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._blurRef = null;
	                    this.MarkPropDirty("BlurRef");	
	            });
	    }
	    }
	    }
	
	                            partial void OnEventUpdatingValue(string oldValue, ref string newValue);
	
	    partial void SerializeCoreIgbTextarea(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTextarea(ser);
	
	if (IsPropDirty("Autocomplete")) { ser.AddStringProp("autocomplete", this._autocomplete); }
	if (IsPropDirty("Autocapitalize")) { ser.AddStringProp("autocapitalize", this._autocapitalize); }
	if (IsPropDirty("InputMode")) { ser.AddStringProp("inputMode", this._inputMode); }
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("MaxLength")) { ser.AddNumberProp("maxLength", this._maxLength); }
	if (IsPropDirty("MinLength")) { ser.AddNumberProp("minLength", this._minLength); }
	if (IsPropDirty("Outlined")) { ser.AddBooleanProp("outlined", this._outlined); }
	if (IsPropDirty("Placeholder")) { ser.AddStringProp("placeholder", this._placeholder); }
	if (IsPropDirty("ReadOnly")) { ser.AddBooleanProp("readOnly", this._readOnly); }
	if (IsPropDirty("Resize")) { ser.AddEnumProp("resize", this._resize); }
	if (IsPropDirty("Rows")) { ser.AddNumberProp("rows", this._rows); }
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Spellcheck")) { ser.AddBooleanProp("spellcheck", this._spellcheck); }
	if (IsPropDirty("Wrap")) { ser.AddEnumProp("wrap", this._wrap); }
	if (IsPropDirty("ValidateOnly")) { ser.AddBooleanProp("validateOnly", this._validateOnly); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Required")) { ser.AddBooleanProp("required", this._required); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("InputRef")) { ser.AddStringProp("inputRef", this._inputRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	
	    }
	
}
}
