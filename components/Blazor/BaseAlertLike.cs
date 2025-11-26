
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbBaseAlertLike: BaseRendererControl {
                                public override string Type { get { return "WebBaseAlertLike"; } }

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
                            return "igc-base-alert-like";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbBaseAlertLike(): base() {
	        OnCreatedIgbBaseAlertLike();
	
	        
	    }
	
	    partial void OnCreatedIgbBaseAlertLike();
	    
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Whether the component is in shown state.
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
	private double _displayTime = 0;
	
	partial void OnDisplayTimeChanging(ref double newValue);
	/// <summary>
	/// Determines the duration in ms in which the component will be visible.
	/// </summary>
	[Parameter]
	public double DisplayTime 
	{
	get { return this._displayTime; }
	set { 
	                if (this._displayTime != value || !IsPropDirty("DisplayTime")) {
	                        MarkPropDirty("DisplayTime");
	                } 
	                this._displayTime = value;
	                 
	                }
	}
	private bool _keepOpen = false;
	
	partial void OnKeepOpenChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the component should close after the `displayTime` is over.
	/// </summary>
	[Parameter]
	public bool KeepOpen 
	{
	get { return this._keepOpen; }
	set { 
	                if (this._keepOpen != value || !IsPropDirty("KeepOpen")) {
	                        MarkPropDirty("KeepOpen");
	                } 
	                this._keepOpen = value;
	                 
	                }
	}
	private AbsolutePosition _position = AbsolutePosition.Bottom;
	
	partial void OnPositionChanging(ref AbsolutePosition newValue);
	/// <summary>
	/// Sets the position of the component in the viewport.
	/// </summary>
	[Parameter]
	public AbsolutePosition Position 
	{
	get { return this._position; }
	set { 
	                if (this._position != value || !IsPropDirty("Position")) {
	                        MarkPropDirty("Position");
	                } 
	                this._position = value;
	                 
	                }
	}
	
	    partial void FindByNameBaseAlertLike(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameBaseAlertLike(name, ref item);
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
	/// Opens the component.
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
	/// Closes the component.
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
	
	    partial void SerializeCoreIgbBaseAlertLike(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbBaseAlertLike(ser);
	
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	if (IsPropDirty("DisplayTime")) { ser.AddNumberProp("displayTime", this._displayTime); }
	if (IsPropDirty("KeepOpen")) { ser.AddBooleanProp("keepOpen", this._keepOpen); }
	if (IsPropDirty("Position")) { ser.AddEnumProp("position", this._position); }
	
	    }
	
}
}
