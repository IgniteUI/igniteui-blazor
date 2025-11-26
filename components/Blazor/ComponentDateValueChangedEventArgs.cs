
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComponentDateValueChangedEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebComponentDateValueChangedEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbComponentDateValueChangedEventArgs(): base() {
	        OnCreatedIgbComponentDateValueChangedEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbComponentDateValueChangedEventArgs();
	    
	private DateTime _detail = DateTime.MinValue;
	
	partial void OnDetailChanging(ref DateTime newValue);
	[Parameter]
	public DateTime Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameComponentDateValueChangedEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComponentDateValueChangedEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbComponentDateValueChangedEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComponentDateValueChangedEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddDateTimeProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = DateToString(this._detail); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = ReturnToDate(args["detail"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
