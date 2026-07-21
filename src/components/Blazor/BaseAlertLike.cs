
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
	/// Determines the duration in milliseconds in which the component will be visible.
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
	/// `bottom` - positions the component at the bottom. This is the default.
	/// `middle` - positions the component at the center.
	/// `top` - positions the component at the top.
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
	private NotificationPositioning _positioning = NotificationPositioning.Viewport;
	
	partial void OnPositioningChanging(ref NotificationPositioning newValue);
	/// <summary>
	/// Sets the positioning strategy of the component.
	/// `viewport` - positions the component relative to the viewport, ignoring any ancestor elements. This is the default behavior.
	/// `container` - positions the component relative to the nearest visible ancestor. In this mode, the component will be constrained within the bounding box of the ancestor and will be positioned according to the `position` attribute.
	/// </summary>
	[Parameter]
	public NotificationPositioning Positioning 
	{
	get { return this._positioning; }
	set { 
	                if (this._positioning != value || !IsPropDirty("Positioning")) {
	                        MarkPropDirty("Positioning");
	                } 
	                this._positioning = value;
	                 
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
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Opens the component.
	/// Returns a promise that resolves to `true` if the component was successfully opened, or `false`
	/// if it was already open or could not be shown (e.g., in `container` positioning mode with no visible ancestors).
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
	/// Returns a promise that resolves to `true` if the component was successfully closed, or `false`
	/// if it was already closed.
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
	/// Returns a promise that resolves to `true` if the operation completed successfully, or `false`
	/// if it was already in the desired state.
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
	if (IsPropDirty("Positioning")) { ser.AddEnumProp("positioning", this._positioning); }
	
	    }
	
}
}
