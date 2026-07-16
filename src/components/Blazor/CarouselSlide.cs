
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A single content container within a set of containers used in the context of an `igc-carousel`.
/// </summary>
public partial class IgbCarouselSlide: BaseRendererControl {
                                public override string Type { get { return "WebCarouselSlide"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbCarouselSlideModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbCarouselSlideModule.Register(IgBlazor);
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
                            return "igc-carousel-slide";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbCarouselSlide(): base() {
	        OnCreatedIgbCarouselSlide();
	
	        
	    }
	
	    partial void OnCreatedIgbCarouselSlide();
	    
	private bool _active = false;
	
	partial void OnActiveChanging(ref bool newValue);
	/// <summary>
	/// The current active slide for the carousel component.
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
	
	    partial void FindByNameCarouselSlide(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCarouselSlide(name, ref item);
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
	
	    partial void SerializeCoreIgbCarouselSlide(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCarouselSlide(ser);
	
	if (IsPropDirty("Active")) { ser.AddBooleanProp("active", this._active); }
	
	    }
	
}
}
