
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Similar to a checkbox, a switch controls the state of a single setting on or off.
/// </summary>
public partial class IgbSwitch: IgbCheckboxBase {
                                public override string Type { get { return "WebSwitch"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSwitchModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSwitchModule.Register(IgBlazor);
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
	                            return "igc-switch";
                                }
                        }
	
	    public IgbSwitch(): base() {
	        OnCreatedIgbSwitch();
	
	        
	    }
	
	    partial void OnCreatedIgbSwitch();
	    
	
	    partial void FindByNameSwitch(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSwitch(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbSwitch(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSwitch(ser);
	
	
	    }
	
}
}
