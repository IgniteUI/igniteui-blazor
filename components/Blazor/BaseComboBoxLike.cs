
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbBaseComboBoxLike: BaseRendererControl {
                                public override string Type { get { return "WebBaseComboBoxLike"; } }

                            protected override string ResolveDisplay()
                        {
                        return "inline-block";
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Queued; }
                            }
	
	    public IgbBaseComboBoxLike(): base() {
	        OnCreatedIgbBaseComboBoxLike();
	
	        
	    }
	
	    partial void OnCreatedIgbBaseComboBoxLike();
	    
	private object _emitEvent;
	
	partial void OnEmitEventChanging(ref object newValue);
	[Parameter]
	public object EmitEvent 
	{
	get { return this._emitEvent; }
	set { 
	                if (this._emitEvent != value || !IsPropDirty("EmitEvent")) {
	                        MarkPropDirty("EmitEvent");
	                } 
	                this._emitEvent = value;
	                 
	                }
	}
	private bool _keepOpenOnSelect = false;
	
	partial void OnKeepOpenOnSelectChanging(ref bool newValue);
	/// <summary>
	/// Whether the component dropdown should be kept open on selection.
	/// </summary>
	[Parameter]
	public bool KeepOpenOnSelect 
	{
	get { return this._keepOpenOnSelect; }
	set { 
	                if (this._keepOpenOnSelect != value || !IsPropDirty("KeepOpenOnSelect")) {
	                        MarkPropDirty("KeepOpenOnSelect");
	                } 
	                this._keepOpenOnSelect = value;
	                 
	                }
	}
	private bool _keepOpenOnOutsideClick = false;
	
	partial void OnKeepOpenOnOutsideClickChanging(ref bool newValue);
	/// <summary>
	/// Whether the component dropdown should be kept open on clicking outside of it.
	/// </summary>
	[Parameter]
	public bool KeepOpenOnOutsideClick 
	{
	get { return this._keepOpenOnOutsideClick; }
	set { 
	                if (this._keepOpenOnOutsideClick != value || !IsPropDirty("KeepOpenOnOutsideClick")) {
	                        MarkPropDirty("KeepOpenOnOutsideClick");
	                } 
	                this._keepOpenOnOutsideClick = value;
	                 
	                }
	}
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Sets the open state of the component.
	/// </summary>
	[Parameter]
	public bool Open 
	{
	get { return this._open; }
	set { 
	                if (this._open != value || !IsPropDirty("Open")) {
	                        MarkPropDirty("Open");
	                } 
	                this._open = value;
	                 
	                }
	}
	
	    partial void FindByNameBaseComboBoxLike(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameBaseComboBoxLike(name, ref item);
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
	/// Shows the component.
	/// </summary>
	public async Task<bool> ShowAsync() 
	                    {
		var iv = await InvokeMethod("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Show() 
	                    {
		var iv = InvokeMethodSync("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Hides the component.
	/// </summary>
	public async Task<bool> HideAsync() 
	                    {
		var iv = await InvokeMethod("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Hide() 
	                    {
		var iv = InvokeMethodSync("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Toggles the open state of the component.
	/// </summary>
	public async Task<bool> ToggleAsync() 
	                    {
		var iv = await InvokeMethod("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Toggle() 
	                    {
		var iv = InvokeMethodSync("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	
	    partial void SerializeCoreIgbBaseComboBoxLike(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbBaseComboBoxLike(ser);
	
	if (IsPropDirty("EmitEvent")) { ser.AddPrimitiveProp("emitEvent", this._emitEvent); }
	if (IsPropDirty("KeepOpenOnSelect")) { ser.AddBooleanProp("keepOpenOnSelect", this._keepOpenOnSelect); }
	if (IsPropDirty("KeepOpenOnOutsideClick")) { ser.AddBooleanProp("keepOpenOnOutsideClick", this._keepOpenOnOutsideClick); }
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	
	    }
	
}
}
