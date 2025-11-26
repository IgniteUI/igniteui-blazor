
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCalendarDate: BaseRendererElement {
                                public override string Type { get { return "CalendarDate"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbCalendarDate(): base() {
	        OnCreatedIgbCalendarDate();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendarDate();
	    
	private DateTime _date = DateTime.MinValue;
	
	partial void OnDateChanging(ref DateTime newValue);
	[Parameter]
	public DateTime Date 
	{
	get { return this._date; }
	set { 
	                if (this._date != value || !IsPropDirty("Date")) {
	                        MarkPropDirty("Date");
	                } 
	                this._date = value;
	                 
	                }
	}
	private bool _isCurrentMonth = false;
	
	partial void OnIsCurrentMonthChanging(ref bool newValue);
	[Parameter]
	public bool IsCurrentMonth 
	{
	get { return this._isCurrentMonth; }
	set { 
	                if (this._isCurrentMonth != value || !IsPropDirty("IsCurrentMonth")) {
	                        MarkPropDirty("IsCurrentMonth");
	                } 
	                this._isCurrentMonth = value;
	                 
	                }
	}
	private bool _isPrevMonth = false;
	
	partial void OnIsPrevMonthChanging(ref bool newValue);
	[Parameter]
	public bool IsPrevMonth 
	{
	get { return this._isPrevMonth; }
	set { 
	                if (this._isPrevMonth != value || !IsPropDirty("IsPrevMonth")) {
	                        MarkPropDirty("IsPrevMonth");
	                } 
	                this._isPrevMonth = value;
	                 
	                }
	}
	private bool _isNextMonth = false;
	
	partial void OnIsNextMonthChanging(ref bool newValue);
	[Parameter]
	public bool IsNextMonth 
	{
	get { return this._isNextMonth; }
	set { 
	                if (this._isNextMonth != value || !IsPropDirty("IsNextMonth")) {
	                        MarkPropDirty("IsNextMonth");
	                } 
	                this._isNextMonth = value;
	                 
	                }
	}
	
	    partial void FindByNameCalendarDate(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendarDate(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCalendarDate(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendarDate(ser);
	
	if (IsPropDirty("Date")) { ser.AddDateTimeProp("date", this._date); }
	if (IsPropDirty("IsCurrentMonth")) { ser.AddBooleanProp("isCurrentMonth", this._isCurrentMonth); }
	if (IsPropDirty("IsPrevMonth")) { ser.AddBooleanProp("isPrevMonth", this._isPrevMonth); }
	if (IsPropDirty("IsNextMonth")) { ser.AddBooleanProp("isNextMonth", this._isNextMonth); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Date")) { args["date"] = DateToString(this._date); }
	if (IsPropDirty("IsCurrentMonth")) { args["isCurrentMonth"] = (this._isCurrentMonth).ToString().ToLower(); }
	if (IsPropDirty("IsPrevMonth")) { args["isPrevMonth"] = (this._isPrevMonth).ToString().ToLower(); }
	if (IsPropDirty("IsNextMonth")) { args["isNextMonth"] = (this._isNextMonth).ToString().ToLower(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("date")) { this.Date = ReturnToDate(args["date"]); }
	if (args.ContainsKey("isCurrentMonth")) { this.IsCurrentMonth = ReturnToBoolean(args["isCurrentMonth"]); }
	if (args.ContainsKey("isPrevMonth")) { this.IsPrevMonth = ReturnToBoolean(args["isPrevMonth"]); }
	if (args.ContainsKey("isNextMonth")) { this.IsNextMonth = ReturnToBoolean(args["isNextMonth"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
