
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCalendarDateEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebCalendarDateEventArgs"; } }
	
	    public IgbCalendarDateEventArgs(): base() {
	        OnCreatedIgbCalendarDateEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendarDateEventArgs();
	    
	private IgbCalendarDate _detail;
	
	partial void OnDetailChanging(ref IgbCalendarDate newValue);
	[Parameter]
	public IgbCalendarDate Detail 
	{
	get { return this._detail; }
	set { 
	                        OnDetailChanging(ref value);
	                        MarkPropDirty("Detail"); 
	                        if (this._detail != null) {
	                            this.DetachChild(this._detail);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._detail = value; 
	                    }
	                    
	}
	
	    partial void FindByNameCalendarDateEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendarDateEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCalendarDateEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendarDateEventArgs(ser);
	
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
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbCalendarDate)ConvertReturnValue(args["detail"], "CalendarDate", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
