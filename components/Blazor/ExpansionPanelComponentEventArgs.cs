
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbExpansionPanelComponentEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebExpansionPanelComponentEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbExpansionPanelComponentEventArgs(): base() {
	        OnCreatedIgbExpansionPanelComponentEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbExpansionPanelComponentEventArgs();
	    
	private IgbExpansionPanel _detail;
	
	partial void OnDetailChanging(ref IgbExpansionPanel newValue);
	[Parameter]
	public IgbExpansionPanel Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameExpansionPanelComponentEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameExpansionPanelComponentEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbExpansionPanelComponentEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbExpansionPanelComponentEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddSerializableProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = ObjectToParam(this._detail); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbExpansionPanel)ConvertReturnValue(args["detail"], "ExpansionPanel", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
