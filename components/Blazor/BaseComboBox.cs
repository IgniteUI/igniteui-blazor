
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbBaseComboBox: BaseRendererControl {
                                public override string Type { get { return "WebBaseComboBox"; } }

                            protected override string ResolveDisplay()
                        {
                        return "inline-block";
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Queued; }
                            }
	
	    public IgbBaseComboBox(): base() {
	        OnCreatedIgbBaseComboBox();
	
	        
	    }
	
	    partial void OnCreatedIgbBaseComboBox();
	    
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
	
	    partial void FindByNameBaseComboBox(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameBaseComboBox(name, ref item);
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
	
	    partial void SerializeCoreIgbBaseComboBox(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbBaseComboBox(ser);
	
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	
	    }
	
}
}
