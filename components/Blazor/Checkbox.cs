
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A check box allowing single values to be selected/deselected.
/// </summary>
public partial class IgbCheckbox: IgbCheckboxBase {
                                public override string Type { get { return "WebCheckbox"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbCheckboxModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbCheckboxModule.Register(IgBlazor);
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
	                            return "igc-checkbox";
                                }
                        }
	
	    public IgbCheckbox(): base() {
	        OnCreatedIgbCheckbox();
	
	        
	    }
	
	    partial void OnCreatedIgbCheckbox();
	    
	private bool _indeterminate = false;
	
	partial void OnIndeterminateChanging(ref bool newValue);
	/// <summary>
	/// Draws the checkbox in indeterminate state.
	/// </summary>
	[Parameter]
	public bool Indeterminate 
	{
	get { return this._indeterminate; }
	set { 
	                if (this._indeterminate != value || !IsPropDirty("Indeterminate")) {
	                        MarkPropDirty("Indeterminate");
	                } 
	                this._indeterminate = value;
	                 
	                }
	}
	
	    partial void FindByNameCheckbox(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCheckbox(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCheckbox(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCheckbox(ser);
	
	if (IsPropDirty("Indeterminate")) { ser.AddBooleanProp("indeterminate", this._indeterminate); }
	
	    }
	
}
}
