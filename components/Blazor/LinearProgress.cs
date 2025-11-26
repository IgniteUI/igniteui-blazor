
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A linear progress indicator used to express unspecified wait time or display
/// the length of a process.
/// </summary>
public partial class IgbLinearProgress: IgbProgressBase {
                                public override string Type { get { return "WebLinearProgress"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbLinearProgressModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbLinearProgressModule.Register(IgBlazor);
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
	                            return "igc-linear-progress";
                                }
                        }
	
	    public IgbLinearProgress(): base() {
	        OnCreatedIgbLinearProgress();
	
	        
	    }
	
	    partial void OnCreatedIgbLinearProgress();
	    
	private bool _striped = false;
	
	partial void OnStripedChanging(ref bool newValue);
	/// <summary>
	/// Sets the striped look of the control.
	/// </summary>
	[Parameter]
	public bool Striped 
	{
	get { return this._striped; }
	set { 
	                if (this._striped != value || !IsPropDirty("Striped")) {
	                        MarkPropDirty("Striped");
	                } 
	                this._striped = value;
	                 
	                }
	}
	private LinearProgressLabelAlign _labelAlign = LinearProgressLabelAlign.TopStart;
	
	partial void OnLabelAlignChanging(ref LinearProgressLabelAlign newValue);
	/// <summary>
	/// The position for the default label of the control.
	/// </summary>
	[Parameter]
	public LinearProgressLabelAlign LabelAlign 
	{
	get { return this._labelAlign; }
	set { 
	                if (this._labelAlign != value || !IsPropDirty("LabelAlign")) {
	                        MarkPropDirty("LabelAlign");
	                } 
	                this._labelAlign = value;
	                 
	                }
	}
	
	    partial void FindByNameLinearProgress(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameLinearProgress(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbLinearProgress(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbLinearProgress(ser);
	
	if (IsPropDirty("Striped")) { ser.AddBooleanProp("striped", this._striped); }
	if (IsPropDirty("LabelAlign")) { ser.AddEnumProp("labelAlign", this._labelAlign); }
	
	    }
	
}
}
