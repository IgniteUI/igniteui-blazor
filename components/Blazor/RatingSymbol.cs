
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Used when a custom icon/symbol/element needs to be passed to the igc-rating component.
/// </summary>
public partial class IgbRatingSymbol: BaseRendererControl {
                                public override string Type { get { return "WebRatingSymbol"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbRatingSymbolModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbRatingSymbolModule.Register(IgBlazor);
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
                            return "igc-rating-symbol";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbRatingSymbol(): base() {
	        OnCreatedIgbRatingSymbol();
	
	        
	    }
	
	    partial void OnCreatedIgbRatingSymbol();
	    
	
	    partial void FindByNameRatingSymbol(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRatingSymbol(name, ref item);
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
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	
	    partial void SerializeCoreIgbRatingSymbol(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRatingSymbol(ser);
	
	
	    }
	
}
}
