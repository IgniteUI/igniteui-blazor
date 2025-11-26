
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbNumberEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebNumberEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbNumberEventArgs(): base() {
	        OnCreatedIgbNumberEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbNumberEventArgs();
	    
	private double _detail = 0;
	
	partial void OnDetailChanging(ref double newValue);
	[Parameter]
	public double Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameNumberEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameNumberEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbNumberEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbNumberEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddNumberProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = (this._detail).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = ReturnToDouble(args["detail"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
