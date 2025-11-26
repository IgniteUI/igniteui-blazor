
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbFocusOptions: BaseRendererElement {
                                public override string Type { get { return "FocusOptions"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbFocusOptions(): base() {
	        OnCreatedIgbFocusOptions();
	
	        
	    }
	
	    partial void OnCreatedIgbFocusOptions();
	    
	private bool _preventScroll = false;
	
	partial void OnPreventScrollChanging(ref bool newValue);
	[Parameter]
	public bool PreventScroll 
	{
	get { return this._preventScroll; }
	set { 
	                if (this._preventScroll != value || !IsPropDirty("PreventScroll")) {
	                        MarkPropDirty("PreventScroll");
	                } 
	                this._preventScroll = value;
	                 
	                }
	}
	
	    partial void FindByNameFocusOptions(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameFocusOptions(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbFocusOptions(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbFocusOptions(ser);
	
	if (IsPropDirty("PreventScroll")) { ser.AddBooleanProp("preventScroll", this._preventScroll); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("PreventScroll")) { args["preventScroll"] = (this._preventScroll).ToString().ToLower(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("preventScroll")) { this.PreventScroll = ReturnToBoolean(args["preventScroll"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
