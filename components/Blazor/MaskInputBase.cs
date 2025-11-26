
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbMaskInputBase: IgbInputBase {
                                public override string Type { get { return "WebMaskInputBase"; } }

							
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
	                            return "igc-mask-input-base";
                                }
                        }
	
	    public IgbMaskInputBase(): base() {
	        OnCreatedIgbMaskInputBase();
	
	        
	    }
	
	    partial void OnCreatedIgbMaskInputBase();
	    
	private string _prompt;
	
	partial void OnPromptChanging(ref string newValue);
	/// <summary>
	/// The prompt symbol to use for unfilled parts of the mask.
	/// </summary>
	[Parameter]
	public string Prompt 
	{
	get { return this._prompt; }
	set { 
	                if (this._prompt != value || !IsPropDirty("Prompt")) {
	                        MarkPropDirty("Prompt");
	                } 
	                this._prompt = value;
	                 
	                }
	}
	
	    partial void FindByNameMaskInputBase(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameMaskInputBase(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Selects all text within the input.
	/// </summary>
	public async  Task SelectAsync() 
	                    {
		await InvokeMethod("select", new object[] {  }, new string[] {  });
	}
	                    public  void Select() 
	                    {
		InvokeMethodSync("select", new object[] {  }, new string[] {  });
	}
	
	    partial void SerializeCoreIgbMaskInputBase(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbMaskInputBase(ser);
	
	if (IsPropDirty("Prompt")) { ser.AddStringProp("prompt", this._prompt); }
	
	    }
	
}
}
