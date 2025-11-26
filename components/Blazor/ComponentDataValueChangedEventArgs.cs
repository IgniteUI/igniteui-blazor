
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComponentDataValueChangedEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebComponentDataValueChangedEventArgs"; } }
	
	    public IgbComponentDataValueChangedEventArgs(): base() {
	        OnCreatedIgbComponentDataValueChangedEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbComponentDataValueChangedEventArgs();
	    
	private object _detail;
	
	partial void OnDetailChanging(ref object newValue);
	[Parameter]
	public object Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameComponentDataValueChangedEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComponentDataValueChangedEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbComponentDataValueChangedEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComponentDataValueChangedEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddPrimitiveProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = ObjectToParam(this._detail); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = ReturnToPrimitive(args["detail"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
