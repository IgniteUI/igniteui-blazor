
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A slider component used to select numeric value within a range.
/// </summary>
public partial class IgbSlider: IgbSliderBase {
                                public override string Type { get { return "WebSlider"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSliderModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSliderModule.Register(IgBlazor);
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
	                            return "igc-slider";
                                }
                        }
	
	    public IgbSlider(): base() {
	        OnCreatedIgbSlider();
	
	        
	    }
	
	    partial void OnCreatedIgbSlider();
	    
	private double _value = 0;
	
	partial void OnValueChanging(ref double newValue);
	/// <summary>
	/// The current value of the component.
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
	
	    partial void FindByNameSlider(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSlider(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	/// <summary>
	/// Increments the value of the slider by `stepIncrement * step`, where `stepIncrement` defaults to 1.
	/// stepIncrement Optional step increment. If no parameter is passed, it defaults to 1.
	/// </summary>
	/// <param name="stepIncrement">Optional step increment. If no parameter is passed, it defaults to 1.</param>
	public async  Task StepUpAsync(double stepIncrement = -1) 
	                    {
		await InvokeMethod("stepUp", new object[] { stepIncrement }, new string[] { "Number" });
	}
	                    public  void StepUp(double stepIncrement = -1) 
	                    {
		InvokeMethodSync("stepUp", new object[] { stepIncrement }, new string[] { "Number" });
	}
	/// <summary>
	/// Decrements the value of the slider by `stepDecrement * step`, where `stepDecrement` defaults to 1.
	/// stepDecrement Optional step decrement. If no parameter is passed, it defaults to 1.
	/// </summary>
	/// <param name="stepDecrement">Optional step decrement. If no parameter is passed, it defaults to 1.</param>
	public async  Task StepDownAsync(double stepDecrement = -1) 
	                    {
		await InvokeMethod("stepDown", new object[] { stepDecrement }, new string[] { "Number" });
	}
	                    public  void StepDown(double stepDecrement = -1) 
	                    {
		InvokeMethodSync("stepDown", new object[] { stepDecrement }, new string[] { "Number" });
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
	
	    partial void OnHandlingInput(IgbNumberEventArgs args);
	    private EventCallback<IgbNumberEventArgs>? _input = null;
	    [Parameter]
	    public EventCallback<IgbNumberEventArgs> Input
	    {
	        get 
	        {
	            return this._input != null ? this._input.Value : EventCallback<IgbNumberEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbNumberEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _input, ref eventCallbacksCache))
	                {
	                    _input = value;
	                    this.SetHandler<IgbNumberEventArgs>(this.Name, "Input", value, (args) => {
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
	                this.SetHandler<IgbNumberEventArgs>(this.Name, "Input", null);
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
	
	
	                            partial void OnEventUpdatingValue(double oldValue, ref double newValue);
	
	    partial void SerializeCoreIgbSlider(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSlider(ser);
	
	if (IsPropDirty("Value")) { ser.AddNumberProp("value", this._value); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("InputRef")) { ser.AddStringProp("inputRef", this._inputRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
