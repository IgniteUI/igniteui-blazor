
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
public partial class IgbRadio: BaseRendererControl {
                                public override string Type { get { return "WebRadio"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbRadioModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbRadioModule.Register(IgBlazor);
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
                            return "igc-radio";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbRadio(): base() {
	        OnCreatedIgbRadio();
	
	        
	    }
	
	    partial void OnCreatedIgbRadio();
	    
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// The value attribute of the control.
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
	private bool _checked = false;
	
	partial void OnCheckedChanging(ref bool newValue);
	/// <summary>
	/// The checked state of the control.
	/// </summary>
	[Parameter]
	public bool Checked 
	{
	get { return this._checked; }
	set { 
	                if (this._checked != value || !IsPropDirty("Checked")) {
	                        MarkPropDirty("Checked");
	                } 
	                this._checked = value;
	                 
	                }
	}
	public async Task<bool> GetCurrentCheckedAsync()
	                    {
		var iv = await InvokeMethod("p:Checked", new object[] { }, new string[] { });
		return ReturnToBoolean(iv);
	}
	                    public bool GetCurrentChecked()
	                    {
		var iv = InvokeMethodSync("p:Checked", new object[] { }, new string[] { });
		return ReturnToBoolean(iv);
	}
	private ToggleLabelPosition _labelPosition = ToggleLabelPosition.After;
	
	partial void OnLabelPositionChanging(ref ToggleLabelPosition newValue);
	/// <summary>
	/// The label position of the radio control.
	/// </summary>
	[Parameter]
	public ToggleLabelPosition LabelPosition 
	{
	get { return this._labelPosition; }
	set { 
	                if (this._labelPosition != value || !IsPropDirty("LabelPosition")) {
	                        MarkPropDirty("LabelPosition");
	                } 
	                this._labelPosition = value;
	                 
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
	
	    partial void FindByNameRadio(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRadio(name, ref item);
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
	/// Simulates a click on the radio control.
	/// </summary>
	public async  Task ClickAsync() 
	                    {
		await InvokeMethod("click", new object[] {  }, new string[] {  });
	}
	                    public  void Click() 
	                    {
		InvokeMethodSync("click", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Sets focus on the radio control.
	/// </summary>
	
	[WCWidgetMemberName("Focus")]
	public async  Task FocusComponentAsync(IgbFocusOptions options) 
	                    {
		await InvokeMethod("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
	}
	                    
	[WCWidgetMemberName("Focus")]
	public  void FocusComponent(IgbFocusOptions options) 
	                    {
		InvokeMethodSync("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
	}
	/// <summary>
	/// Removes focus from the radio control.
	/// </summary>
	
	[WCWidgetMemberName("Blur")]
	public async  Task BlurComponentAsync() 
	                    {
		await InvokeMethod("blur", new object[] {  }, new string[] {  });
	}
	                    
	[WCWidgetMemberName("Blur")]
	public  void BlurComponent() 
	                    {
		InvokeMethodSync("blur", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Checks for validity of the control and shows the browser message if it invalid.
	/// </summary>
	public async Task<bool> ReportValidityAsync() 
	                    {
		var iv = await InvokeMethod("reportValidity", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool ReportValidity() 
	                    {
		var iv = InvokeMethodSync("reportValidity", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
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
	
	    private EventCallback<bool>? _checkedChanged = null;
	    [Parameter]
	    public EventCallback<bool> CheckedChanged
	    {
	        get 
	        {
	            return this._checkedChanged != null ? this._checkedChanged.Value : EventCallback<bool>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<bool>.Empty)) 
	            {
	                 if (!CompareEventCallbacks(value, _checkedChanged, ref eventCallbacksCache))
	                {
	                    this.EnsureChangeHandled();
	
	                    _checkedChanged = value;
	                }
	    }
	        else 
	            {
	                _checkedChanged = null;
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
	
	    partial void OnHandlingChange(IgbRadioChangeEventArgs args);
	    private EventCallback<IgbRadioChangeEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbRadioChangeEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbRadioChangeEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbRadioChangeEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbRadioChangeEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueChecked = default(bool);
	
	    
	    {
	        newValueChecked = (bool)(args.Detail.Checked);
	        ;
	        OnEventUpdatingChecked(this._checked, ref newValueChecked);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Checked = newValueChecked;
	        } else {
	            this._checked = newValueChecked;
	        }
	        OnPropertyPropagatedOut(Name, "Checked");
	    }
	
	    if (!EventCallback<bool>.Empty.Equals(CheckedChanged))
	    {
	        var task = CheckedChanged.InvokeAsync(newValueChecked);
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
	                this.SetHandler<IgbRadioChangeEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbRadioChangeEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbRadioChangeEventArgs>(null, (Action<IgbRadioChangeEventArgs>)((e) => { })); this._change = null;        
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
	
	                            partial void OnEventUpdatingChecked(bool oldValue, ref bool newValue);
	
	    partial void SerializeCoreIgbRadio(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRadio(ser);
	
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Checked")) { ser.AddBooleanProp("checked", this._checked); }
	if (IsPropDirty("LabelPosition")) { ser.AddEnumProp("labelPosition", this._labelPosition); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Required")) { ser.AddBooleanProp("required", this._required); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	
	    }
	
}
}
