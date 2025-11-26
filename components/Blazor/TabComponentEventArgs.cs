
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTabComponentEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebTabComponentEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTabComponentEventArgs(): base() {
	        OnCreatedIgbTabComponentEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbTabComponentEventArgs();
	    
	private IgbTab _detail;
	
	partial void OnDetailChanging(ref IgbTab newValue);
	[Parameter]
	public IgbTab Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameTabComponentEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTabComponentEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbTabComponentEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTabComponentEventArgs(ser);
	
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
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbTab)ConvertReturnValue(args["detail"], "Tab", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
