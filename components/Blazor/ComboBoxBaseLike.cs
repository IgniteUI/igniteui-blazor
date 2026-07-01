
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComboBoxBaseLike: IgbBaseComboBox {
                                public override string Type { get { return "WebComboBoxBaseLike"; } }
							
                            protected override string ResolveDisplay()
                        {
	                        return "inline-block";
                        }
	
	    public IgbComboBoxBaseLike(): base() {
	        OnCreatedIgbComboBoxBaseLike();
	
	        
	    }
	
	    partial void OnCreatedIgbComboBoxBaseLike();
	    
	private bool _keepOpenOnSelect = false;
	
	partial void OnKeepOpenOnSelectChanging(ref bool newValue);
	/// <summary>
	/// Whether the component dropdown should be kept open on selection.
	/// </summary>
	[Parameter]
	public bool KeepOpenOnSelect 
	{
	get { return this._keepOpenOnSelect; }
	set { 
	                if (this._keepOpenOnSelect != value || !IsPropDirty("KeepOpenOnSelect")) {
	                        MarkPropDirty("KeepOpenOnSelect");
	                } 
	                this._keepOpenOnSelect = value;
	                 
	                }
	}
	private bool _keepOpenOnOutsideClick = false;
	
	partial void OnKeepOpenOnOutsideClickChanging(ref bool newValue);
	/// <summary>
	/// Whether the component dropdown should be kept open on clicking outside of it.
	/// </summary>
	[Parameter]
	public bool KeepOpenOnOutsideClick 
	{
	get { return this._keepOpenOnOutsideClick; }
	set { 
	                if (this._keepOpenOnOutsideClick != value || !IsPropDirty("KeepOpenOnOutsideClick")) {
	                        MarkPropDirty("KeepOpenOnOutsideClick");
	                } 
	                this._keepOpenOnOutsideClick = value;
	                 
	                }
	}
	
	    partial void FindByNameComboBoxBaseLike(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComboBoxBaseLike(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbComboBoxBaseLike(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComboBoxBaseLike(ser);
	
	if (IsPropDirty("KeepOpenOnSelect")) { ser.AddBooleanProp("keepOpenOnSelect", this._keepOpenOnSelect); }
	if (IsPropDirty("KeepOpenOnOutsideClick")) { ser.AddBooleanProp("keepOpenOnOutsideClick", this._keepOpenOnOutsideClick); }
	
	    }
	
}
}
