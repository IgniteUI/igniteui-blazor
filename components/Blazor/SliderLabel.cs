
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Allows formatting the values of the slider as string values.
/// The text content of the slider labels is used for thumb and tick labels.
/// @remarks
/// When slider labels are provided, the `min`, `max` and `step` properties are automatically
/// calculated so that they do not allow values that do not map to the provided labels.
/// </summary>
public partial class IgbSliderLabel: BaseRendererControl {
                                public override string Type { get { return "WebSliderLabel"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSliderLabelModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSliderLabelModule.Register(IgBlazor);
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
                            return "igc-slider-label";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbSliderLabel(): base() {
	        OnCreatedIgbSliderLabel();
	
	        
	    }
	
	    partial void OnCreatedIgbSliderLabel();
	    
	
	    partial void FindByNameSliderLabel(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSliderLabel(name, ref item);
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
	
	    partial void SerializeCoreIgbSliderLabel(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSliderLabel(ser);
	
	
	    }
	
}
}
