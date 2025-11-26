
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbDateRangeValueDetail: BaseRendererElement {
                                public override string Type { get { return "WebDateRangeValueDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbDateRangeValueDetail(): base() {
	        OnCreatedIgbDateRangeValueDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbDateRangeValueDetail();
	    
	private DateTime _start = DateTime.MinValue;
	
	partial void OnStartChanging(ref DateTime newValue);
	[Parameter]
	public DateTime Start 
	{
	get { return this._start; }
	set { 
	                if (this._start != value || !IsPropDirty("Start")) {
	                        MarkPropDirty("Start");
	                } 
	                this._start = value;
	                 
	                }
	}
	private DateTime _end = DateTime.MinValue;
	
	partial void OnEndChanging(ref DateTime newValue);
	[Parameter]
	public DateTime End 
	{
	get { return this._end; }
	set { 
	                if (this._end != value || !IsPropDirty("End")) {
	                        MarkPropDirty("End");
	                } 
	                this._end = value;
	                 
	                }
	}
	
	    partial void FindByNameDateRangeValueDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDateRangeValueDetail(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbDateRangeValueDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateRangeValueDetail(ser);
	
	if (IsPropDirty("Start")) { ser.AddDateTimeProp("start", this._start); }
	if (IsPropDirty("End")) { ser.AddDateTimeProp("end", this._end); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Start")) { args["start"] = DateToString(this._start); }
	if (IsPropDirty("End")) { args["end"] = DateToString(this._end); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("start")) { this.Start = ReturnToDate(args["start"]); }
	if (args.ContainsKey("end")) { this.End = ReturnToDate(args["end"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
