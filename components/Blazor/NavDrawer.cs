
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a side navigation container that provides
/// quick access between views.
/// </summary>
public partial class IgbNavDrawer: BaseRendererControl {
                                public override string Type { get { return "WebNavDrawer"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbNavDrawerModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbNavDrawerModule.Register(IgBlazor);
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
                            return "igc-nav-drawer";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbNavDrawer(): base() {
	        OnCreatedIgbNavDrawer();
	
	        
	    }
	
	    partial void OnCreatedIgbNavDrawer();
	    
	private NavDrawerPosition _position = NavDrawerPosition.Start;
	
	partial void OnPositionChanging(ref NavDrawerPosition newValue);
	/// <summary>
	/// The position of the drawer.
	/// </summary>
	[Parameter]
	public NavDrawerPosition Position 
	{
	get { return this._position; }
	set { 
	                if (this._position != value || !IsPropDirty("Position")) {
	                        MarkPropDirty("Position");
	                } 
	                this._position = value;
	                 
	                }
	}
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the drawer is opened.
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
	
	    partial void FindByNameNavDrawer(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameNavDrawer(name, ref item);
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
	/// Opens the drawer.
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
	/// Closes the drawer.
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
	/// Toggles the open state of the drawer.
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
	
	    partial void SerializeCoreIgbNavDrawer(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbNavDrawer(ser);
	
	if (IsPropDirty("Position")) { ser.AddEnumProp("position", this._position); }
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	
	    }
	
}
}
