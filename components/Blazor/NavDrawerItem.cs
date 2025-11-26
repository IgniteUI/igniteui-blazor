
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a navigation drawer item.
/// </summary>
public partial class IgbNavDrawerItem: BaseRendererControl {
                                public override string Type { get { return "WebNavDrawerItem"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbNavDrawerItemModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbNavDrawerItemModule.Register(IgBlazor);
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
                            return "igc-nav-drawer-item";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbNavDrawerItem(): base() {
	        OnCreatedIgbNavDrawerItem();
	
	        
	    }
	
	    partial void OnCreatedIgbNavDrawerItem();
	    
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the drawer is disabled.
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
	private bool _active = false;
	
	partial void OnActiveChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the drawer is active.
	/// </summary>
	[Parameter]
	public bool Active 
	{
	get { return this._active; }
	set { 
	                if (this._active != value || !IsPropDirty("Active")) {
	                        MarkPropDirty("Active");
	                } 
	                this._active = value;
	                 
	                }
	}
	
	    partial void FindByNameNavDrawerItem(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameNavDrawerItem(name, ref item);
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
	
	    partial void SerializeCoreIgbNavDrawerItem(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbNavDrawerItem(ser);
	
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Active")) { ser.AddBooleanProp("active", this._active); }
	
	    }
	
}
}
