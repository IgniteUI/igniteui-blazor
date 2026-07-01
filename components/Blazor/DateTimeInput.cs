
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
public partial class IgbDateTimeInput: IgbDateTimeInputBase {
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
	
	    public IgbDateTimeInput(): base() {
	        OnCreatedIgbDateTimeInput();
	
	        
	    }
	
	    partial void OnCreatedIgbDateTimeInput();
	    
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
	public async  Task StepUpAsync(DatePart? datePart = null, double delta = -1) 
	                    {
		await InvokeMethod("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	                    public  void StepUp(DatePart? datePart = null, double delta = -1) 
	                    {
		InvokeMethodSync("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	public async  Task StepDownAsync(DatePart? datePart = null, double delta = -1) 
	                    {
		await InvokeMethod("stepDown", new object[] { ObjectToParam(datePart, typeof(DatePart)), delta }, new string[] { "Json", "Number" });
	}
	                    public  void StepDown(DatePart? datePart = null, double delta = -1) 
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
	
	    private string _inputOcurredRef = null;
	    private string _inputOcurredScript = null;
	    [Parameter]
	    public string InputOcurredScript { 
	    
	        set 
	        {
	            if (value != this._inputOcurredScript)
	            {
	                this._inputOcurredScript = value;
	                this.OnRefChanged("InputOcurred", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._inputOcurredRef = refName;
	                    this.MarkPropDirty("InputOcurredRef");	
	                });
	            }
	        }
	        get 
	        {
	            return this._inputOcurredScript;
	        }
	    }
	
	    partial void OnHandlingInputOcurred(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _inputOcurred = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> InputOcurred
	    {
	        get 
	        {
	            return this._inputOcurred != null ? this._inputOcurred.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _inputOcurred, ref eventCallbacksCache))
	                {
	                    _inputOcurred = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputOcurred", value, (args) => {
	                        OnHandlingInputOcurred(args);
	                        
	                    });
	        this.OnRefChanged("InputOcurred", null, "event:::InputOcurred", true, false, (refName, oldValue, newValue) => {
	                        this._inputOcurredRef = refName;
	                        this.MarkPropDirty("InputOcurredRef");	
	                });
	                }
	    }
	        else 
	            {
	                _inputOcurred = null;
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputOcurred", null);
	    this.OnRefChanged("InputOcurred", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputOcurredRef = null;
	                    this.MarkPropDirty("InputOcurredRef");	
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
	            if (value != this._changeScript)
	            {
	                this._changeScript = value;
	                this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
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
	
	
	    private string _focusRef = null;
	    private string _focusScript = null;
	    [Parameter]
	    public string FocusScript { 
	    
	        set 
	        {
	            if (value != this._focusScript)
	            {
	                this._focusScript = value;
	                this.OnRefChanged("Focus", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._focusRef = refName;
	                    this.MarkPropDirty("FocusRef");	
	                });
	            }
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
	            if (value != this._blurScript)
	            {
	                this._blurScript = value;
	                this.OnRefChanged("Blur", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._blurRef = refName;
	                    this.MarkPropDirty("BlurRef");	
	                });
	            }
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
	
	                            partial void OnEventUpdatingValue(DateTime? oldValue, ref DateTime? newValue);
	
	    partial void SerializeCoreIgbDateTimeInput(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateTimeInput(ser);
	
	if (IsPropDirty("Value")) { ser.AddDateTimeProp("value", this._value); }
	if (IsPropDirty("InputOcurredRef")) { ser.AddStringProp("inputOcurredRef", this._inputOcurredRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	
	    }
	
}
}
