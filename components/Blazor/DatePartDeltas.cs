
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbDatePartDeltas: BaseRendererElement {
                                public override string Type { get { return "DatePartDeltas"; } }
	
	    public IgbDatePartDeltas(): base() {
	        OnCreatedIgbDatePartDeltas();
	
	        
	    }
	
	    partial void OnCreatedIgbDatePartDeltas();
	    
	private double _date = 0;
	
	partial void OnDateChanging(ref double newValue);
	[Parameter]
	public double Date 
	{
	get { return this._date; }
	set { 
	                if (this._date != value || !IsPropDirty("Date")) {
	                        MarkPropDirty("Date");
	                } 
	                this._date = value;
	                 
	                }
	}
	private double _month = 0;
	
	partial void OnMonthChanging(ref double newValue);
	[Parameter]
	public double Month 
	{
	get { return this._month; }
	set { 
	                if (this._month != value || !IsPropDirty("Month")) {
	                        MarkPropDirty("Month");
	                } 
	                this._month = value;
	                 
	                }
	}
	private double _year = 0;
	
	partial void OnYearChanging(ref double newValue);
	[Parameter]
	public double Year 
	{
	get { return this._year; }
	set { 
	                if (this._year != value || !IsPropDirty("Year")) {
	                        MarkPropDirty("Year");
	                } 
	                this._year = value;
	                 
	                }
	}
	private double _hours = 0;
	
	partial void OnHoursChanging(ref double newValue);
	[Parameter]
	public double Hours 
	{
	get { return this._hours; }
	set { 
	                if (this._hours != value || !IsPropDirty("Hours")) {
	                        MarkPropDirty("Hours");
	                } 
	                this._hours = value;
	                 
	                }
	}
	private double _minutes = 0;
	
	partial void OnMinutesChanging(ref double newValue);
	[Parameter]
	public double Minutes 
	{
	get { return this._minutes; }
	set { 
	                if (this._minutes != value || !IsPropDirty("Minutes")) {
	                        MarkPropDirty("Minutes");
	                } 
	                this._minutes = value;
	                 
	                }
	}
	private double _seconds = 0;
	
	partial void OnSecondsChanging(ref double newValue);
	[Parameter]
	public double Seconds 
	{
	get { return this._seconds; }
	set { 
	                if (this._seconds != value || !IsPropDirty("Seconds")) {
	                        MarkPropDirty("Seconds");
	                } 
	                this._seconds = value;
	                 
	                }
	}
	
	    partial void FindByNameDatePartDeltas(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDatePartDeltas(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbDatePartDeltas(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDatePartDeltas(ser);
	
	if (IsPropDirty("Date")) { ser.AddNumberProp("date", this._date); }
	if (IsPropDirty("Month")) { ser.AddNumberProp("month", this._month); }
	if (IsPropDirty("Year")) { ser.AddNumberProp("year", this._year); }
	if (IsPropDirty("Hours")) { ser.AddNumberProp("hours", this._hours); }
	if (IsPropDirty("Minutes")) { ser.AddNumberProp("minutes", this._minutes); }
	if (IsPropDirty("Seconds")) { ser.AddNumberProp("seconds", this._seconds); }
	
	    }
	
}
}
