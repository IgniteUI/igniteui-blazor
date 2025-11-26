
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A date time input is an input field that lets you set and edit the date and time in a chosen input element
/// using customizable display and input formats.
/// </summary>
public partial class IgbDateTimeInput: IgbMaskInputBase {
                                public override string Type { get { return "WebDateTimeInput"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDateTimeInputModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDateTimeInputModule.Register(IgBlazor);
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
	                            return "igc-date-time-input";
                                }
                        }
	
	    public IgbDateTimeInput(): base() {
	        OnCreatedIgbDateTimeInput();
	
	        
	    }
	
	    partial void OnCreatedIgbDateTimeInput();
	    
	private string _inputFormat;
	
	partial void OnInputFormatChanging(ref string newValue);
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
	private DateTime? _value = DateTime.MinValue;
	
	partial void OnValueChanging(ref DateTime? newValue);
	/// <summary>
	/// The value of the input.
	/// </summary>
	[Parameter]
	public DateTime? Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<DateTime?> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToDate(iv);
	}
	                    public DateTime? GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToDate(iv);
	}
	private DateTime? _min = DateTime.MinValue;
	
	partial void OnMinChanging(ref DateTime? newValue);
	/// <summary>
	/// The minimum value required for the input to remain valid.
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
	/// The maximum value required for the input to remain valid.
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
	private IgbDatePartDeltas _spinDelta;
	
	partial void OnSpinDeltaChanging(ref IgbDatePartDeltas newValue);
	/// <summary>
	/// Delta values used to increment or decrement each date part on step actions.
	/// All values default to `1`.
	/// </summary>
	[Parameter]
	public IgbDatePartDeltas SpinDelta 
	{
	get { return this._spinDelta; }
	set { 
	                        OnSpinDeltaChanging(ref value);
	                        MarkPropDirty("SpinDelta"); 
	                        if (this._spinDelta != null) {
	                            this.DetachChild(this._spinDelta);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._spinDelta = value; 
	                    }
	                    
	}
	private bool _spinLoop = false;
	
	partial void OnSpinLoopChanging(ref bool newValue);
	/// <summary>
	/// Sets whether to loop over the currently spun segment.
	/// </summary>
	[Parameter]
	public bool SpinLoop 
	{
	get { return this._spinLoop; }
	set { 
	                if (this._spinLoop != value || !IsPropDirty("SpinLoop")) {
	                        MarkPropDirty("SpinLoop");
	                } 
	                this._spinLoop = value;
	                 
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
	
	    partial void FindByNameDateTimeInput(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDateTimeInput(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	public async  Task StepUpAsync(DatePart datePart, double delta = -1) 
	                    {
		await InvokeMethod("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	                    public  void StepUp(DatePart datePart, double delta = -1) 
	                    {
		InvokeMethodSync("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	public async  Task StepDownAsync(DatePart datePart, double delta = -1) 
	                    {
		await InvokeMethod("stepDown", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	                    public  void StepDown(DatePart datePart, double delta = -1) 
	                    {
		InvokeMethodSync("stepDown", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	/// <summary>
	/// Clears the input element of user input.
	/// </summary>
	public async  Task ClearAsync() 
	                    {
		await InvokeMethod("clear", new object[] {  }, new string[] {  });
	}
	                    public  void Clear() 
	                    {
		InvokeMethodSync("clear", new object[] {  }, new string[] {  });
	}
	
	    private EventCallback<DateTime?>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<DateTime?> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<DateTime?>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<DateTime?>.Empty)) 
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
	
	    partial void OnHandlingChange(IgbComponentDateValueChangedEventArgs args);
	    private EventCallback<IgbComponentDateValueChangedEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbComponentDateValueChangedEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbComponentDateValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentDateValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbComponentDateValueChangedEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(DateTime?);
	
	    
	    {
	        newValueValue = (DateTime?)(args.Detail);
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
	
	    if (!EventCallback<DateTime?>.Empty.Equals(ValueChanged))
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
	                this.SetHandler<IgbComponentDateValueChangedEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbComponentDateValueChangedEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbComponentDateValueChangedEventArgs>(null, (Action<IgbComponentDateValueChangedEventArgs>)((e) => { })); this._change = null;        
	        }
	    }
	
	
	                            partial void OnEventUpdatingValue(DateTime? oldValue, ref DateTime? newValue);
	
	    partial void SerializeCoreIgbDateTimeInput(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateTimeInput(ser);
	
	if (IsPropDirty("InputFormat")) { ser.AddStringProp("inputFormat", this._inputFormat); }
	if (IsPropDirty("Value")) { ser.AddDateTimeProp("value", this._value); }
	if (IsPropDirty("Min")) { ser.AddDateTimeProp("min", this._min); }
	if (IsPropDirty("Max")) { ser.AddDateTimeProp("max", this._max); }
	if (IsPropDirty("DisplayFormat")) { ser.AddStringProp("displayFormat", this._displayFormat); }
	if (IsPropDirty("SpinDelta")) { ser.AddSerializableProp("spinDelta", this._spinDelta); }
	if (IsPropDirty("SpinLoop")) { ser.AddBooleanProp("spinLoop", this._spinLoop); }
	if (IsPropDirty("Locale")) { ser.AddStringProp("locale", this._locale); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
