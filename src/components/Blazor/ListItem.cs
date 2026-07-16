
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The list-item component is a container
/// intended for row items in the list component.
/// </summary>
public partial class IgbListItem: BaseRendererControl {
                                public override string Type { get { return "WebListItem"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbListItemModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbListItemModule.Register(IgBlazor);
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
                            return "igc-list-item";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbListItem(): base() {
	        OnCreatedIgbListItem();
	
	        
	    }
	
	    partial void OnCreatedIgbListItem();
	    
	private bool _selected = false;
	
	partial void OnSelectedChanging(ref bool newValue);
	/// <summary>
	/// Defines if the list item is selected or not.
	/// </summary>
	[Parameter]
	public bool Selected 
	{
	get { return this._selected; }
	set { 
	                if (this._selected != value || !IsPropDirty("Selected")) {
	                        MarkPropDirty("Selected");
	                } 
	                this._selected = value;
	                 
	                }
	}
	
	    partial void FindByNameListItem(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameListItem(name, ref item);
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
	
	    partial void SerializeCoreIgbListItem(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbListItem(ser);
	
	if (IsPropDirty("Selected")) { ser.AddBooleanProp("selected", this._selected); }
	
	    }
	
}
}
