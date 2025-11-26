
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Rating provides insight regarding others' opinions and experiences,
/// and can allow the user to submit a rating of their own
/// @cssproperty --symbol-size - The size of the symbols.
/// @cssproperty --symbol-full-color - The color of the filled symbol.
/// @cssproperty --symbol-empty-color - The color of the empty symbol.
/// @cssproperty --symbol-full-filter - The filter(s) used for the filled symbol.
/// @cssproperty --symbol-empty-filter - The filter(s) used for the empty symbol.
/// </summary>
public partial class IgbRating: BaseRendererControl {
                                public override string Type { get { return "WebRating"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbRatingModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbRatingModule.Register(IgBlazor);
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
                            return "igc-rating";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbRating(): base() {
	        OnCreatedIgbRating();
	
	        
	    }
	
	    partial void OnCreatedIgbRating();
	    
	private double _max = 0;
	
	partial void OnMaxChanging(ref double newValue);
	/// <summary>
	/// The maximum value for the rating.
	/// If there are projected symbols, the maximum value will be resolved
	/// based on the number of symbols.
	/// @default 5
	/// </summary>
	[Parameter]
	public double Max 
	{
	get { return this._max; }
	set { 
	                if (this._max != value || !IsPropDirty("Max")) {
	                        MarkPropDirty("Max");
	                } 
	                this._max = value;
	                 
	                }
	}
	private double _step = 0;
	
	partial void OnStepChanging(ref double newValue);
	/// <summary>
	/// The minimum value change allowed.
	/// Valid values are in the interval between 0 and 1 inclusive.
	/// @default 1
	/// </summary>
	[Parameter]
	public double Step 
	{
	get { return this._step; }
	set { 
	                if (this._step != value || !IsPropDirty("Step")) {
	                        MarkPropDirty("Step");
	                } 
	                this._step = value;
	                 
	                }
	}
	private string _label;
	
	partial void OnLabelChanging(ref string newValue);
	/// <summary>
	/// The label of the control.
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
	private string _valueFormat;
	
	partial void OnValueFormatChanging(ref string newValue);
	/// <summary>
	/// A format string which sets aria-valuetext. Instances of '{0}' will be replaced
	/// with the current value of the control and instances of '{1}' with the maximum value for the control.
	/// Important for screen-readers and useful for localization.
	/// </summary>
	[Parameter]
	public string ValueFormat 
	{
	get { return this._valueFormat; }
	set { 
	                if (this._valueFormat != value || !IsPropDirty("ValueFormat")) {
	                        MarkPropDirty("ValueFormat");
	                } 
	                this._valueFormat = value;
	                 
	                }
	}
	private double _value = 0;
	
	partial void OnValueChanging(ref double newValue);
	/// <summary>
	/// The current value of the component
	/// @default 0
	/// </summary>
	[Parameter]
	public double Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<double> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToDouble(iv);
	}
	                    public double GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToDouble(iv);
	}
	private bool _hoverPreview = false;
	
	partial void OnHoverPreviewChanging(ref bool newValue);
	/// <summary>
	/// Sets hover preview behavior for the component
	/// </summary>
	[Parameter]
	public bool HoverPreview 
	{
	get { return this._hoverPreview; }
	set { 
	                if (this._hoverPreview != value || !IsPropDirty("HoverPreview")) {
	                        MarkPropDirty("HoverPreview");
	                } 
	                this._hoverPreview = value;
	                 
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
	private bool _single = false;
	
	partial void OnSingleChanging(ref bool newValue);
	/// <summary>
	/// Toggles single selection visual mode.
	/// @default false
	/// </summary>
	[Parameter]
	public bool Single 
	{
	get { return this._single; }
	set { 
	                if (this._single != value || !IsPropDirty("Single")) {
	                        MarkPropDirty("Single");
	                } 
	                this._single = value;
	                 
	                }
	}
	private bool _allowReset = false;
	
	partial void OnAllowResetChanging(ref bool newValue);
	/// <summary>
	/// Whether to reset the rating when the user selects the same value.
	/// </summary>
	[Parameter]
	public bool AllowReset 
	{
	get { return this._allowReset; }
	set { 
	                if (this._allowReset != value || !IsPropDirty("AllowReset")) {
	                        MarkPropDirty("AllowReset");
	                } 
	                this._allowReset = value;
	                 
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
	
	    partial void FindByNameRating(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRating(name, ref item);
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
	/// Increments the value of the control by `n` steps multiplied by the
	/// step factor.
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
	/// Decrements the value of the control by `n` steps multiplied by
	/// the step factor.
	/// </summary>
	public async  Task StepDownAsync(double n = -1) 
	                    {
		await InvokeMethod("stepDown", new object[] { n }, new string[] { "Number" });
	}
	                    public  void StepDown(double n = -1) 
	                    {
		InvokeMethodSync("stepDown", new object[] { n }, new string[] { "Number" });
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
	
	    private EventCallback<double>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<double> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<double>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<double>.Empty)) 
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
	
	    partial void OnHandlingChange(IgbNumberEventArgs args);
	    private EventCallback<IgbNumberEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbNumberEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbNumberEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbNumberEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbNumberEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(double);
	
	    
	    {
	        newValueValue = (double)(args.Detail);
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
	
	    if (!EventCallback<double>.Empty.Equals(ValueChanged))
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
	                this.SetHandler<IgbNumberEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbNumberEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbNumberEventArgs>(null, (Action<IgbNumberEventArgs>)((e) => { })); this._change = null;        
	        }
	    }
	
	
	    private string _hoverRef = null;
	    private string _hoverScript = null;
	    [Parameter]
	    public string HoverScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Hover", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._hoverRef = refName;
	                this.MarkPropDirty("HoverRef");	
	        }); 
	        }
	        get 
	        {
	            return this._hoverScript;
	        }
	    }
	
	    partial void OnHandlingHover(IgbNumberEventArgs args);
	    private EventCallback<IgbNumberEventArgs>? _hover = null;
	    [Parameter]
	    public EventCallback<IgbNumberEventArgs> Hover
	    {
	        get 
	        {
	            return this._hover != null ? this._hover.Value : EventCallback<IgbNumberEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbNumberEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _hover, ref eventCallbacksCache))
	                {
	                    _hover = value;
	                    this.SetHandler<IgbNumberEventArgs>(this.Name, "Hover", value, (args) => {
	                        OnHandlingHover(args);
	                        
	                    });
	        this.OnRefChanged("Hover", null, "event:::Hover", true, false, (refName, oldValue, newValue) => {
	                        this._hoverRef = refName;
	                        this.MarkPropDirty("HoverRef");	
	                });
	                }
	    }
	        else 
	            {
	                _hover = null;
	                this.SetHandler<IgbNumberEventArgs>(this.Name, "Hover", null);
	    this.OnRefChanged("Hover", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._hoverRef = null;
	                    this.MarkPropDirty("HoverRef");	
	            });
	    }
	    }
	    }
	
	                            partial void OnEventUpdatingValue(double oldValue, ref double newValue);
	
	    partial void SerializeCoreIgbRating(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRating(ser);
	
	if (IsPropDirty("Max")) { ser.AddNumberProp("max", this._max); }
	if (IsPropDirty("Step")) { ser.AddNumberProp("step", this._step); }
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("ValueFormat")) { ser.AddStringProp("valueFormat", this._valueFormat); }
	if (IsPropDirty("Value")) { ser.AddNumberProp("value", this._value); }
	if (IsPropDirty("HoverPreview")) { ser.AddBooleanProp("hoverPreview", this._hoverPreview); }
	if (IsPropDirty("ReadOnly")) { ser.AddBooleanProp("readOnly", this._readOnly); }
	if (IsPropDirty("Single")) { ser.AddBooleanProp("single", this._single); }
	if (IsPropDirty("AllowReset")) { ser.AddBooleanProp("allowReset", this._allowReset); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("HoverRef")) { ser.AddStringProp("hoverRef", this._hoverRef); }
	
	    }
	
}
}
