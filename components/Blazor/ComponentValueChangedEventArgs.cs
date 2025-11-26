
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComponentValueChangedEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebComponentValueChangedEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbComponentValueChangedEventArgs(): base() {
	        OnCreatedIgbComponentValueChangedEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbComponentValueChangedEventArgs();
	    
	private string _detail;
	
	partial void OnDetailChanging(ref string newValue);
	[Parameter]
	public string Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameComponentValueChangedEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComponentValueChangedEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbComponentValueChangedEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComponentValueChangedEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddStringProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = this._detail; }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = ReturnToString(args["detail"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
