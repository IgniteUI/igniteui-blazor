
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a clickable button, used to submit forms or anywhere in a
/// document for accessible, standard button functionality.
/// </summary>
public partial class IgbButton: IgbButtonBase {
                                public override string Type { get { return "WebButton"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbButtonModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbButtonModule.Register(IgBlazor);
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
	                            return "igc-button";
                                }
                        }
	
	    public IgbButton(): base() {
	        OnCreatedIgbButton();
	
	        
	    }
	
	    partial void OnCreatedIgbButton();
	    
	private ButtonVariant _variant = ButtonVariant.Contained;
	
	partial void OnVariantChanging(ref ButtonVariant newValue);
	/// <summary>
	/// Sets the variant of the button.
	/// </summary>
	[Parameter]
	public ButtonVariant Variant 
	{
	get { return this._variant; }
	set { 
	                if (this._variant != value || !IsPropDirty("Variant")) {
	                        MarkPropDirty("Variant");
	                } 
	                this._variant = value;
	                 
	                }
	}
	
	    partial void FindByNameButton(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameButton(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbButton(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbButton(ser);
	
	if (IsPropDirty("Variant")) { ser.AddEnumProp("variant", this._variant); }
	
	    }
	
}
}
