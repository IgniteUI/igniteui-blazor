
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
public partial class IgbSelectGroup: BaseRendererControl {
                                public override string Type { get { return "WebSelectGroup"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSelectGroupModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSelectGroupModule.Register(IgBlazor);
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
                            return "igc-select-group";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbSelectGroup(): base() {
	        OnCreatedIgbSelectGroup();
	
	        
	    }
	
	    partial void OnCreatedIgbSelectGroup();
	    
	private IgbSelectItem[] _items;
	
	partial void OnItemsChanging(ref IgbSelectItem[] newValue);
	/// <summary>
	/// All child `igc-select-item`s.
	/// </summary>
	[Parameter]
	public IgbSelectItem[] Items 
	{
	get { return this._items; }
	set { 
	                if (this._items != value || !IsPropDirty("Items")) {
	                        MarkPropDirty("Items");
	                } 
	                this._items = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Whether the group item and all its children are disabled.
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
	
	    partial void FindByNameSelectGroup(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSelectGroup(name, ref item);
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
	
	    partial void SerializeCoreIgbSelectGroup(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSelectGroup(ser);
	
	if (IsPropDirty("Items")) { ser.AddSerializableArrayProp("items", this._items); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	
	    }
	
}
}
