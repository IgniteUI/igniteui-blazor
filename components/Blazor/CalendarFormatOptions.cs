
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCalendarFormatOptions: BaseRendererElement {
                                public override string Type { get { return "CalendarFormatOptions"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbCalendarFormatOptions(): base() {
	        OnCreatedIgbCalendarFormatOptions();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendarFormatOptions();
	    
	private string _weekday;
	
	partial void OnWeekdayChanging(ref string newValue);
	[Parameter]
	public string Weekday 
	{
	get { return this._weekday; }
	set { 
	                if (this._weekday != value || !IsPropDirty("Weekday")) {
	                        MarkPropDirty("Weekday");
	                } 
	                this._weekday = value;
	                 
	                }
	}
	private string _month;
	
	partial void OnMonthChanging(ref string newValue);
	[Parameter]
	public string Month 
	{
	get { return this._month; }
	set { 
	                if (this._month != value || !IsPropDirty("Month")) {
	                        MarkPropDirty("Month");
	                } 
	                this._month = value;
	                 
	                }
	}
	
	    partial void FindByNameCalendarFormatOptions(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendarFormatOptions(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCalendarFormatOptions(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendarFormatOptions(ser);
	
	if (IsPropDirty("Weekday")) { ser.AddStringProp("weekday", this._weekday); }
	if (IsPropDirty("Month")) { ser.AddStringProp("month", this._month); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Weekday")) { args["weekday"] = this._weekday; }
	if (IsPropDirty("Month")) { args["month"] = this._month; }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("weekday")) { this.Weekday = ReturnToString(args["weekday"]); }
	if (args.ContainsKey("month")) { this.Month = ReturnToString(args["month"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
