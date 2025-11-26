
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents an item in a dropdown list.
/// </summary>
public partial class IgbDropdownItem: IgbBaseOptionLike {
                                public override string Type { get { return "WebDropdownItem"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDropdownItemModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDropdownItemModule.Register(IgBlazor);
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
	                            return "igc-dropdown-item";
                                }
                        }
	
	    public IgbDropdownItem(): base() {
	        OnCreatedIgbDropdownItem();
	
	        
	    }
	
	    partial void OnCreatedIgbDropdownItem();
	    
	
	    partial void FindByNameDropdownItem(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDropdownItem(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbDropdownItem(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDropdownItem(ser);
	
	
	    }
	
}
}
