
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCalendarBase: BaseRendererControl {
                                public override string Type { get { return "WebCalendarBase"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbCalendarBaseModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbCalendarBaseModule.Register(IgBlazor);
                                    }
                                }

                            protected override string ResolveDisplay()
                        {
                        return "inline-block";
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Queued; }
                            }
	
	    public IgbCalendarBase(): base() {
	        OnCreatedIgbCalendarBase();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendarBase();
	    
	private CalendarSelection _selection = CalendarSelection.Single;
	
	partial void OnSelectionChanging(ref CalendarSelection newValue);
	/// <summary>
	/// Sets the type of selection in the component.
	/// </summary>
	[Parameter]
	public CalendarSelection Selection 
	{
	get { return this._selection; }
	set { 
	                if (this._selection != value || !IsPropDirty("Selection")) {
	                        MarkPropDirty("Selection");
	                } 
	                this._selection = value;
	                 
	                }
	}
	private bool _showWeekNumbers = false;
	
	partial void OnShowWeekNumbersChanging(ref bool newValue);
	/// <summary>
	/// Whether to show the week numbers.
	/// </summary>
	[Parameter]
	public bool ShowWeekNumbers 
	{
	get { return this._showWeekNumbers; }
	set { 
	                if (this._showWeekNumbers != value || !IsPropDirty("ShowWeekNumbers")) {
	                        MarkPropDirty("ShowWeekNumbers");
	                } 
	                this._showWeekNumbers = value;
	                 
	                }
	}
	private WeekDays _weekStart = WeekDays.Sunday;
	
	partial void OnWeekStartChanging(ref WeekDays newValue);
	/// <summary>
	/// Gets/Sets the first day of the week.
	/// </summary>
	[Parameter]
	public WeekDays WeekStart 
	{
	get { return this._weekStart; }
	set { 
	                if (this._weekStart != value || !IsPropDirty("WeekStart")) {
	                        MarkPropDirty("WeekStart");
	                } 
	                this._weekStart = value;
	                 
	                }
	}
	private string _locale;
	
	partial void OnLocaleChanging(ref string newValue);
	/// <summary>
	/// Gets/Sets the locale used for formatting and displaying the dates in the component.
	/// </summary>
	[Parameter]
	public string Locale 
	{
	get { return this._locale; }
	set { 
	                if (this._locale != value || !IsPropDirty("Locale")) {
	                        MarkPropDirty("Locale");
	                } 
	                this._locale = value;
	                 
	                }
	}
	private IgbDateRangeDescriptor[]? _specialDates;
	
	partial void OnSpecialDatesChanging(ref IgbDateRangeDescriptor[]? newValue);
	/// <summary>
	/// Gets/Sets the special dates for the component.
	/// </summary>
	[Parameter]
	public IgbDateRangeDescriptor[]? SpecialDates 
	{
	get { return this._specialDates; }
	set { 
	                if (this._specialDates != value || !IsPropDirty("SpecialDates")) {
	                        MarkPropDirty("SpecialDates");
	                } 
	                this._specialDates = value;
	                 
	                }
	}
	private IgbDateRangeDescriptor[]? _disabledDates;
	
	partial void OnDisabledDatesChanging(ref IgbDateRangeDescriptor[]? newValue);
	/// <summary>
	/// Gets/Sets the disabled dates for the component.
	/// </summary>
	[Parameter]
	public IgbDateRangeDescriptor[]? DisabledDates 
	{
	get { return this._disabledDates; }
	set { 
	                if (this._disabledDates != value || !IsPropDirty("DisabledDates")) {
	                        MarkPropDirty("DisabledDates");
	                } 
	                this._disabledDates = value;
	                 
	                }
	}
	
	    partial void FindByNameCalendarBase(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendarBase(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task SetNativeElementAsync(Object element) 
	                    {
		await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	                    public  void SetNativeElement(Object element) 
	                    {
		InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	
	    partial void SerializeCoreIgbCalendarBase(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendarBase(ser);
	
	if (IsPropDirty("Selection")) { ser.AddEnumProp("selection", this._selection); }
	if (IsPropDirty("ShowWeekNumbers")) { ser.AddBooleanProp("showWeekNumbers", this._showWeekNumbers); }
	if (IsPropDirty("WeekStart")) { ser.AddEnumProp("weekStart", this._weekStart); }
	if (IsPropDirty("Locale")) { ser.AddStringProp("locale", this._locale); }
	if (IsPropDirty("SpecialDates")) { ser.AddSerializableArrayProp("specialDates", this._specialDates); }
	if (IsPropDirty("DisabledDates")) { ser.AddSerializableArrayProp("disabledDates", this._disabledDates); }
	
	    }
	
}
}
