
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A masked input is an input field where a developer can control user input and format the visible value,
/// based on configurable rules
/// </summary>
public partial class IgbMaskInput: IgbInputBase {
                                public override string Type { get { return "WebMaskInput"; } }
							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbMaskInputModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbMaskInputModule.Register(IgBlazor);
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
	
	    public IgbMaskInput(): base() {
	        OnCreatedIgbMaskInput();
	
	        
	    }
	
	    partial void OnCreatedIgbMaskInput();
	    
	private MaskInputValueMode _valueMode = MaskInputValueMode.Raw;
	
	partial void OnValueModeChanging(ref MaskInputValueMode newValue);
	/// <summary>
	/// Dictates the behavior when retrieving the value of the control:
	/// - `raw`: Returns clean input (e.g. "5551234567")
	/// - `withFormatting`: Returns with mask formatting (e.g. "(555) 123-4567")
	/// Empty values always return an empty string, regardless of the value mode.
	/// </summary>
	[Parameter]
	public MaskInputValueMode ValueMode 
	{
	get { return this._valueMode; }
	set { 
	                if (this._valueMode != value || !IsPropDirty("ValueMode")) {
	                        MarkPropDirty("ValueMode");
	                } 
	                this._valueMode = value;
	                 
	                }
	}
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// The value of the input.
	/// Regardless of the currently set `value-mode`, an empty value will return an empty string.
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
	private string _mask;
	
	partial void OnMaskChanging(ref string newValue);
	/// <summary>
	/// The masked pattern of the component.
	/// </summary>
	[Parameter]
	public string Mask 
	{
	get { return this._mask; }
	set { 
	                if (this._mask != value || !IsPropDirty("Mask")) {
	                        MarkPropDirty("Mask");
	                } 
	                this._mask = value;
	                 
	                }
	}
	private string _prompt;
	
	partial void OnPromptChanging(ref string newValue);
	/// <summary>
	/// The prompt symbol to use for unfilled parts of the mask pattern.
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
	private bool _readOnly = false;
	
	partial void OnReadOnlyChanging(ref bool newValue);
	/// <summary>
	/// Makes the control a readonly field.
	/// @default false
	/// </summary>
	[Parameter]
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
	
	    partial void FindByNameMaskInput(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameMaskInput(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task SetSelectionRangeAsync(double start = -1, double end = -1, String direction = null) 
	                    {
		await InvokeMethod("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
	}
	                    public  void SetSelectionRange(double start = -1, double end = -1, String direction = null) 
	                    {
		InvokeMethodSync("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
	}
	public async  Task SetRangeTextAsync(String replacement, double start = -1, double end = -1, String selectMode = null) 
	                    {
		await InvokeMethod("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
	}
	                    public  void SetRangeText(String replacement, double start = -1, double end = -1, String selectMode = null) 
	                    {
		InvokeMethodSync("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
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
	
	    partial void SerializeCoreIgbMaskInput(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbMaskInput(ser);
	
	if (IsPropDirty("ValueMode")) { ser.AddEnumProp("valueMode", this._valueMode); }
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Mask")) { ser.AddStringProp("mask", this._mask); }
	if (IsPropDirty("Prompt")) { ser.AddStringProp("prompt", this._prompt); }
	if (IsPropDirty("ReadOnly")) { ser.AddBooleanProp("readOnly", this._readOnly); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
