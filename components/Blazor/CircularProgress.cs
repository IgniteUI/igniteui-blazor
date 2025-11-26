
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A circular progress indicator used to express unspecified wait time or display
/// the length of a process.
/// </summary>
public partial class IgbCircularProgress: IgbProgressBase {
                                public override string Type { get { return "WebCircularProgress"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbCircularProgressModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbCircularProgressModule.Register(IgBlazor);
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
	                            return "igc-circular-progress";
                                }
                        }
	
	    public IgbCircularProgress(): base() {
	        OnCreatedIgbCircularProgress();
	
	        
	    }
	
	    partial void OnCreatedIgbCircularProgress();
	    
	
	    partial void FindByNameCircularProgress(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCircularProgress(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCircularProgress(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCircularProgress(ser);
	
	
	    }
	
}
}
