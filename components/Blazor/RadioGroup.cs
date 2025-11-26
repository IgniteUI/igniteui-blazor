
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The igc-radio-group component unifies one or more igc-radio buttons.
/// </summary>
public partial class IgbRadioGroup: BaseRendererControl {
                                public override string Type { get { return "WebRadioGroup"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbRadioGroupModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbRadioGroupModule.Register(IgBlazor);
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
                            return "igc-radio-group";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbRadioGroup(): base() {
	        OnCreatedIgbRadioGroup();
	
	        
	    }
	
	    partial void OnCreatedIgbRadioGroup();
	    
	private ContentOrientation _alignment = ContentOrientation.Horizontal;
	
	partial void OnAlignmentChanging(ref ContentOrientation newValue);
	/// <summary>
	/// Alignment of the radio controls inside this group.
	/// </summary>
	[Parameter]
	public ContentOrientation Alignment 
	{
	get { return this._alignment; }
	set { 
	                if (this._alignment != value || !IsPropDirty("Alignment")) {
	                        MarkPropDirty("Alignment");
	                } 
	                this._alignment = value;
	                 
	                }
	}
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// Gets/Sets the checked igc-radio element that matches `value`
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
	
	    partial void FindByNameRadioGroup(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRadioGroup(name, ref item);
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
	                        
	var newValueValue = default(string);
	
	    
	    {
	        newValueValue = (string)(args.Detail.Value);
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
	
	
	                            partial void OnEventUpdatingValue(string oldValue, ref string newValue);
	
	    partial void SerializeCoreIgbRadioGroup(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRadioGroup(ser);
	
	if (IsPropDirty("Alignment")) { ser.AddEnumProp("alignment", this._alignment); }
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
