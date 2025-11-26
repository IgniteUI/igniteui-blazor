
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents an item in a select list.
/// </summary>
public partial class IgbSelectItem: IgbBaseOptionLike {
                                public override string Type { get { return "WebSelectItem"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSelectItemModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSelectItemModule.Register(IgBlazor);
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
	                            return "igc-select-item";
                                }
                        }
	
	    public IgbSelectItem(): base() {
	        OnCreatedIgbSelectItem();
	
	        
	    }
	
	    partial void OnCreatedIgbSelectItem();
	    
	
	    partial void FindByNameSelectItem(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSelectItem(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbSelectItem(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSelectItem(ser);
	
	
	    }
	
}
}
