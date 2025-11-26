
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A toast component is used to show a notification
/// </summary>
public partial class IgbToast: IgbBaseAlertLike {
                                public override string Type { get { return "WebToast"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbToastModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbToastModule.Register(IgBlazor);
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
	                            return "igc-toast";
                                }
                        }
	
	    public IgbToast(): base() {
	        OnCreatedIgbToast();
	
	        
	    }
	
	    partial void OnCreatedIgbToast();
	    
	
	    partial void FindByNameToast(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameToast(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbToast(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbToast(ser);
	
	
	    }
	
}
}
