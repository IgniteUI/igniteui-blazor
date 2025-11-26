
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
public partial class IgbMaskInput: IgbMaskInputBase {
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
	                            return "igc-mask-input";
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
	/// - `raw` will return the clean user input.
	/// - `withFormatting` will return the value with all literals and prompts.
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
	/// The mask pattern to apply on the input.
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
	
	    partial void SerializeCoreIgbMaskInput(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbMaskInput(ser);
	
	if (IsPropDirty("ValueMode")) { ser.AddEnumProp("valueMode", this._valueMode); }
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Mask")) { ser.AddStringProp("mask", this._mask); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
