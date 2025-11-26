
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCalendarResourceStrings: BaseRendererElement {
                                public override string Type { get { return "WebCalendarResourceStrings"; } }
	
	    public IgbCalendarResourceStrings(): base() {
	        OnCreatedIgbCalendarResourceStrings();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendarResourceStrings();
	    
	private string _selectMonth;
	
	partial void OnSelectMonthChanging(ref string newValue);
	[Parameter]
	public string SelectMonth 
	{
	get { return this._selectMonth; }
	set { 
	                if (this._selectMonth != value || !IsPropDirty("SelectMonth")) {
	                        MarkPropDirty("SelectMonth");
	                } 
	                this._selectMonth = value;
	                 
	                }
	}
	private string _selectYear;
	
	partial void OnSelectYearChanging(ref string newValue);
	[Parameter]
	public string SelectYear 
	{
	get { return this._selectYear; }
	set { 
	                if (this._selectYear != value || !IsPropDirty("SelectYear")) {
	                        MarkPropDirty("SelectYear");
	                } 
	                this._selectYear = value;
	                 
	                }
	}
	private string _selectDate;
	
	partial void OnSelectDateChanging(ref string newValue);
	[Parameter]
	public string SelectDate 
	{
	get { return this._selectDate; }
	set { 
	                if (this._selectDate != value || !IsPropDirty("SelectDate")) {
	                        MarkPropDirty("SelectDate");
	                } 
	                this._selectDate = value;
	                 
	                }
	}
	private string _selectRange;
	
	partial void OnSelectRangeChanging(ref string newValue);
	[Parameter]
	public string SelectRange 
	{
	get { return this._selectRange; }
	set { 
	                if (this._selectRange != value || !IsPropDirty("SelectRange")) {
	                        MarkPropDirty("SelectRange");
	                } 
	                this._selectRange = value;
	                 
	                }
	}
	private string _selectedDate;
	
	partial void OnSelectedDateChanging(ref string newValue);
	[Parameter]
	public string SelectedDate 
	{
	get { return this._selectedDate; }
	set { 
	                if (this._selectedDate != value || !IsPropDirty("SelectedDate")) {
	                        MarkPropDirty("SelectedDate");
	                } 
	                this._selectedDate = value;
	                 
	                }
	}
	private string _startDate;
	
	partial void OnStartDateChanging(ref string newValue);
	[Parameter]
	public string StartDate 
	{
	get { return this._startDate; }
	set { 
	                if (this._startDate != value || !IsPropDirty("StartDate")) {
	                        MarkPropDirty("StartDate");
	                } 
	                this._startDate = value;
	                 
	                }
	}
	private string _endDate;
	
	partial void OnEndDateChanging(ref string newValue);
	[Parameter]
	public string EndDate 
	{
	get { return this._endDate; }
	set { 
	                if (this._endDate != value || !IsPropDirty("EndDate")) {
	                        MarkPropDirty("EndDate");
	                } 
	                this._endDate = value;
	                 
	                }
	}
	private string _previousMonth;
	
	partial void OnPreviousMonthChanging(ref string newValue);
	[Parameter]
	public string PreviousMonth 
	{
	get { return this._previousMonth; }
	set { 
	                if (this._previousMonth != value || !IsPropDirty("PreviousMonth")) {
	                        MarkPropDirty("PreviousMonth");
	                } 
	                this._previousMonth = value;
	                 
	                }
	}
	private string _nextMonth;
	
	partial void OnNextMonthChanging(ref string newValue);
	[Parameter]
	public string NextMonth 
	{
	get { return this._nextMonth; }
	set { 
	                if (this._nextMonth != value || !IsPropDirty("NextMonth")) {
	                        MarkPropDirty("NextMonth");
	                } 
	                this._nextMonth = value;
	                 
	                }
	}
	private string _previousYear;
	
	partial void OnPreviousYearChanging(ref string newValue);
	[Parameter]
	public string PreviousYear 
	{
	get { return this._previousYear; }
	set { 
	                if (this._previousYear != value || !IsPropDirty("PreviousYear")) {
	                        MarkPropDirty("PreviousYear");
	                } 
	                this._previousYear = value;
	                 
	                }
	}
	private string _nextYear;
	
	partial void OnNextYearChanging(ref string newValue);
	[Parameter]
	public string NextYear 
	{
	get { return this._nextYear; }
	set { 
	                if (this._nextYear != value || !IsPropDirty("NextYear")) {
	                        MarkPropDirty("NextYear");
	                } 
	                this._nextYear = value;
	                 
	                }
	}
	private string _previousYears;
	
	partial void OnPreviousYearsChanging(ref string newValue);
	[Parameter]
	public string PreviousYears 
	{
	get { return this._previousYears; }
	set { 
	                if (this._previousYears != value || !IsPropDirty("PreviousYears")) {
	                        MarkPropDirty("PreviousYears");
	                } 
	                this._previousYears = value;
	                 
	                }
	}
	private string _nextYears;
	
	partial void OnNextYearsChanging(ref string newValue);
	[Parameter]
	public string NextYears 
	{
	get { return this._nextYears; }
	set { 
	                if (this._nextYears != value || !IsPropDirty("NextYears")) {
	                        MarkPropDirty("NextYears");
	                } 
	                this._nextYears = value;
	                 
	                }
	}
	private string _weekLabel;
	
	partial void OnWeekLabelChanging(ref string newValue);
	[Parameter]
	public string WeekLabel 
	{
	get { return this._weekLabel; }
	set { 
	                if (this._weekLabel != value || !IsPropDirty("WeekLabel")) {
	                        MarkPropDirty("WeekLabel");
	                } 
	                this._weekLabel = value;
	                 
	                }
	}
	
	    partial void FindByNameCalendarResourceStrings(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendarResourceStrings(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCalendarResourceStrings(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendarResourceStrings(ser);
	
	if (IsPropDirty("SelectMonth")) { ser.AddStringProp("selectMonth", this._selectMonth); }
	if (IsPropDirty("SelectYear")) { ser.AddStringProp("selectYear", this._selectYear); }
	if (IsPropDirty("SelectDate")) { ser.AddStringProp("selectDate", this._selectDate); }
	if (IsPropDirty("SelectRange")) { ser.AddStringProp("selectRange", this._selectRange); }
	if (IsPropDirty("SelectedDate")) { ser.AddStringProp("selectedDate", this._selectedDate); }
	if (IsPropDirty("StartDate")) { ser.AddStringProp("startDate", this._startDate); }
	if (IsPropDirty("EndDate")) { ser.AddStringProp("endDate", this._endDate); }
	if (IsPropDirty("PreviousMonth")) { ser.AddStringProp("previousMonth", this._previousMonth); }
	if (IsPropDirty("NextMonth")) { ser.AddStringProp("nextMonth", this._nextMonth); }
	if (IsPropDirty("PreviousYear")) { ser.AddStringProp("previousYear", this._previousYear); }
	if (IsPropDirty("NextYear")) { ser.AddStringProp("nextYear", this._nextYear); }
	if (IsPropDirty("PreviousYears")) { ser.AddStringProp("previousYears", this._previousYears); }
	if (IsPropDirty("NextYears")) { ser.AddStringProp("nextYears", this._nextYears); }
	if (IsPropDirty("WeekLabel")) { ser.AddStringProp("weekLabel", this._weekLabel); }
	
	    }
	
}
}
