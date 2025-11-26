
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// 
/// </summary>
public partial class IgbInput: IgbInputBase {
                                public override string Type { get { return "WebInput"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbInputModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbInputModule.Register(IgBlazor);
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
	                            return "igc-input";
                                }
                        }
	
	    public IgbInput(): base() {
	        OnCreatedIgbInput();
	
	        
	    }
	
	    partial void OnCreatedIgbInput();
	    
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// The value of the control.
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
	private InputType _displayType = InputType.Text;
	
	partial void OnDisplayTypeChanging(ref InputType newValue);
	/// <summary>
	/// The type attribute of the control.
	/// </summary>
	[Parameter]
	[WCWidgetMemberName("Type")]
	public InputType DisplayType 
	{
	get { return this._displayType; }
	set { 
	                if (this._displayType != value || !IsPropDirty("DisplayType")) {
	                        MarkPropDirty("DisplayType");
	                } 
	                this._displayType = value;
	                 
	                }
	}
	private string _inputMode;
	
	partial void OnInputModeChanging(ref string newValue);
	/// <summary>
	/// The input mode attribute of the control.
	/// See [relevant MDN article](https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/inputmode)
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
	private string? _pattern;
	
	partial void OnPatternChanging(ref string? newValue);
	/// <summary>
	/// The pattern attribute of the control.
	/// </summary>
	[Parameter]
	public string? Pattern 
	{
	get { return this._pattern; }
	set { 
	                if (this._pattern != value || !IsPropDirty("Pattern")) {
	                        MarkPropDirty("Pattern");
	                } 
	                this._pattern = value;
	                 
	                }
	}
	private double? _minLength = 0;
	
	partial void OnMinLengthChanging(ref double? newValue);
	/// <summary>
	/// The minimum string length required by the control.
	/// </summary>
	[Parameter]
	[WCAttributeName("minlength")]
	public double? MinLength 
	{
	get { return this._minLength; }
	set { 
	                if (this._minLength != value || !IsPropDirty("MinLength")) {
	                        MarkPropDirty("MinLength");
	                } 
	                this._minLength = value;
	                 
	                }
	}
	private double? _maxLength = 0;
	
	partial void OnMaxLengthChanging(ref double? newValue);
	/// <summary>
	/// The maximum string length of the control.
	/// </summary>
	[Parameter]
	[WCAttributeName("maxlength")]
	public double? MaxLength 
	{
	get { return this._maxLength; }
	set { 
	                if (this._maxLength != value || !IsPropDirty("MaxLength")) {
	                        MarkPropDirty("MaxLength");
	                } 
	                this._maxLength = value;
	                 
	                }
	}
	private double? _min = 0;
	
	partial void OnMinChanging(ref double? newValue);
	/// <summary>
	/// The min attribute of the control.
	/// </summary>
	[Parameter]
	public double? Min 
	{
	get { return this._min; }
	set { 
	                if (this._min != value || !IsPropDirty("Min")) {
	                        MarkPropDirty("Min");
	                } 
	                this._min = value;
	                 
	                }
	}
	private double? _max = 0;
	
	partial void OnMaxChanging(ref double? newValue);
	/// <summary>
	/// The max attribute of the control.
	/// </summary>
	[Parameter]
	public double? Max 
	{
	get { return this._max; }
	set { 
	                if (this._max != value || !IsPropDirty("Max")) {
	                        MarkPropDirty("Max");
	                } 
	                this._max = value;
	                 
	                }
	}
	private double? _step = 0;
	
	partial void OnStepChanging(ref double? newValue);
	/// <summary>
	/// The step attribute of the control.
	/// </summary>
	[Parameter]
	public double? Step 
	{
	get { return this._step; }
	set { 
	                if (this._step != value || !IsPropDirty("Step")) {
	                        MarkPropDirty("Step");
	                } 
	                this._step = value;
	                 
	                }
	}
	private bool _autofocus = false;
	
	partial void OnAutofocusChanging(ref bool newValue);
	/// <summary>
	/// The autofocus attribute of the control.
	/// </summary>
	[Parameter]
	public bool Autofocus 
	{
	get { return this._autofocus; }
	set { 
	                if (this._autofocus != value || !IsPropDirty("Autofocus")) {
	                        MarkPropDirty("Autofocus");
	                } 
	                this._autofocus = value;
	                 
	                }
	}
	private string _autocomplete;
	
	partial void OnAutocompleteChanging(ref string newValue);
	/// <summary>
	/// The autocomplete attribute of the control.
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
	private bool _validateOnly = false;
	
	partial void OnValidateOnlyChanging(ref bool newValue);
	/// <summary>
	/// Enables validation rules to be evaluated without restricting user input. This applies to the `maxLength` property for
	/// string-type inputs or allows spin buttons to exceed the predefined `min/max` limits for number-type inputs.
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
	
	    partial void FindByNameInput(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameInput(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	/// <summary>
	/// Selects all text within the input.
	/// </summary>
	public async  Task SelectAsync() 
	                    {
		await InvokeMethod("select", new object[] {  }, new string[] {  });
	}
	                    public  void Select() 
	                    {
		InvokeMethodSync("select", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Increments the numeric value of the input by one or more steps.
	/// </summary>
	public async  Task StepUpAsync(double n = -1) 
	                    {
		await InvokeMethod("stepUp", new object[] { n }, new string[] { "Number" });
	}
	                    public  void StepUp(double n = -1) 
	                    {
		InvokeMethodSync("stepUp", new object[] { n }, new string[] { "Number" });
	}
	/// <summary>
	/// Decrements the numeric value of the input by one or more steps.
	/// </summary>
	public async  Task StepDownAsync(double n = -1) 
	                    {
		await InvokeMethod("stepDown", new object[] { n }, new string[] { "Number" });
	}
	                    public  void StepDown(double n = -1) 
	                    {
		InvokeMethodSync("stepDown", new object[] { n }, new string[] { "Number" });
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
	
	
	                            partial void OnEventUpdatingValue(string oldValue, ref string newValue);
	
	    partial void SerializeCoreIgbInput(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbInput(ser);
	
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("DisplayType")) { ser.AddEnumProp("displayType", this._displayType); }
	if (IsPropDirty("InputMode")) { ser.AddStringProp("inputMode", this._inputMode); }
	if (IsPropDirty("Pattern")) { ser.AddStringProp("pattern", this._pattern); }
	if (IsPropDirty("MinLength")) { ser.AddNumberProp("minLength", this._minLength); }
	if (IsPropDirty("MaxLength")) { ser.AddNumberProp("maxLength", this._maxLength); }
	if (IsPropDirty("Min")) { ser.AddNumberProp("min", this._min); }
	if (IsPropDirty("Max")) { ser.AddNumberProp("max", this._max); }
	if (IsPropDirty("Step")) { ser.AddNumberProp("step", this._step); }
	if (IsPropDirty("Autofocus")) { ser.AddBooleanProp("autofocus", this._autofocus); }
	if (IsPropDirty("Autocomplete")) { ser.AddStringProp("autocomplete", this._autocomplete); }
	if (IsPropDirty("ValidateOnly")) { ser.AddBooleanProp("validateOnly", this._validateOnly); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
