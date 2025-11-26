
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a calendar that lets users
/// to select a date value in a variety of different ways.
/// </summary>
public partial class IgbCalendar: IgbCalendarBase {
                                public override string Type { get { return "WebCalendar"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbCalendarModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbCalendarModule.Register(IgBlazor);
                                    }
                                }

                            protected override string ResolveDisplay()
                        {
	                        return "inline-block";
                        }

                            protected override bool SupportsVisualChildren
                        {
                                get 
                                {
	                            return true;
                                }
                        }
	
	    public IgbCalendar(): base() {
	        OnCreatedIgbCalendar();
	
	        
	    }
	
	    partial void OnCreatedIgbCalendar();
	    
	private DateTime _value = DateTime.MinValue;
	
	partial void OnValueChanging(ref DateTime newValue);
	[Parameter]
	public DateTime Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<DateTime> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToDate(iv);
	}
	                    public DateTime GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToDate(iv);
	}
	private DateTime[] _values;
	
	partial void OnValuesChanging(ref DateTime[] newValue);
	[Parameter]
	public DateTime[] Values 
	{
	get { return this._values; }
	set { 
	                if (this._values != value || !IsPropDirty("Values")) {
	                        MarkPropDirty("Values");
	                } 
	                this._values = value;
	                 
	                }
	}
	public async Task<DateTime[]> GetCurrentValuesAsync()
	                    {
		var iv = await InvokeMethod("p:Values", new object[] { }, new string[] { });
		return ReturnToDateArray(iv);
	}
	                    public DateTime[] GetCurrentValues()
	                    {
		var iv = InvokeMethodSync("p:Values", new object[] { }, new string[] { });
		return ReturnToDateArray(iv);
	}
	private DateTime _activeDate = DateTime.MinValue;
	
	partial void OnActiveDateChanging(ref DateTime newValue);
	/// <summary>
	/// Sets the date which is shown in view and is highlighted. By default it is the current date.
	/// </summary>
	[Parameter]
	public DateTime ActiveDate 
	{
	get { return this._activeDate; }
	set { 
	                if (this._activeDate != value || !IsPropDirty("ActiveDate")) {
	                        MarkPropDirty("ActiveDate");
	                } 
	                this._activeDate = value;
	                 
	                }
	}
	private bool _hideOutsideDays = false;
	
	partial void OnHideOutsideDaysChanging(ref bool newValue);
	/// <summary>
	/// Whether to show the dates that do not belong to the current active month.
	/// </summary>
	[Parameter]
	public bool HideOutsideDays 
	{
	get { return this._hideOutsideDays; }
	set { 
	                if (this._hideOutsideDays != value || !IsPropDirty("HideOutsideDays")) {
	                        MarkPropDirty("HideOutsideDays");
	                } 
	                this._hideOutsideDays = value;
	                 
	                }
	}
	private bool _hideHeader = false;
	
	partial void OnHideHeaderChanging(ref bool newValue);
	/// <summary>
	/// Whether to render the calendar header part.
	/// When the calendar selection is set to `multiple` the header is always hidden.
	/// </summary>
	[Parameter]
	public bool HideHeader 
	{
	get { return this._hideHeader; }
	set { 
	                if (this._hideHeader != value || !IsPropDirty("HideHeader")) {
	                        MarkPropDirty("HideHeader");
	                } 
	                this._hideHeader = value;
	                 
	                }
	}
	private CalendarHeaderOrientation _headerOrientation = CalendarHeaderOrientation.Horizontal;
	
	partial void OnHeaderOrientationChanging(ref CalendarHeaderOrientation newValue);
	/// <summary>
	/// The orientation of the calendar header.
	/// </summary>
	[Parameter]
	public CalendarHeaderOrientation HeaderOrientation 
	{
	get { return this._headerOrientation; }
	set { 
	                if (this._headerOrientation != value || !IsPropDirty("HeaderOrientation")) {
	                        MarkPropDirty("HeaderOrientation");
	                } 
	                this._headerOrientation = value;
	                 
	                }
	}
	private ContentOrientation _orientation = ContentOrientation.Horizontal;
	
	partial void OnOrientationChanging(ref ContentOrientation newValue);
	/// <summary>
	/// The orientation of the calendar months when more than one month
	/// is being shown.
	/// </summary>
	[Parameter]
	public ContentOrientation Orientation 
	{
	get { return this._orientation; }
	set { 
	                if (this._orientation != value || !IsPropDirty("Orientation")) {
	                        MarkPropDirty("Orientation");
	                } 
	                this._orientation = value;
	                 
	                }
	}
	private double _visibleMonths = 0;
	
	partial void OnVisibleMonthsChanging(ref double newValue);
	/// <summary>
	/// The number of months displayed in the days view.
	/// </summary>
	[Parameter]
	public double VisibleMonths 
	{
	get { return this._visibleMonths; }
	set { 
	                if (this._visibleMonths != value || !IsPropDirty("VisibleMonths")) {
	                        MarkPropDirty("VisibleMonths");
	                } 
	                this._visibleMonths = value;
	                 
	                }
	}
	private CalendarActiveView _activeView = CalendarActiveView.Days;
	
	partial void OnActiveViewChanging(ref CalendarActiveView newValue);
	/// <summary>
	/// The current active view of the component.
	/// </summary>
	[Parameter]
	public CalendarActiveView ActiveView 
	{
	get { return this._activeView; }
	set { 
	                if (this._activeView != value || !IsPropDirty("ActiveView")) {
	                        MarkPropDirty("ActiveView");
	                } 
	                this._activeView = value;
	                 
	                }
	}
	private IgbCalendarFormatOptions _formatOptions;
	
	partial void OnFormatOptionsChanging(ref IgbCalendarFormatOptions newValue);
	/// <summary>
	/// The options used to format the months and the weekdays in the calendar views.
	/// </summary>
	[Parameter]
	public IgbCalendarFormatOptions FormatOptions 
	{
	get { return this._formatOptions; }
	set { 
	                        OnFormatOptionsChanging(ref value);
	                        MarkPropDirty("FormatOptions"); 
	                        if (this._formatOptions != null) {
	                            this.DetachChild(this._formatOptions);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._formatOptions = value; 
	                    }
	                    
	}
	private IgbCalendarResourceStrings _resourceStrings;
	
	partial void OnResourceStringsChanging(ref IgbCalendarResourceStrings newValue);
	/// <summary>
	/// The resource strings for localization.
	/// </summary>
	[Parameter]
	public IgbCalendarResourceStrings ResourceStrings 
	{
	get { return this._resourceStrings; }
	set { 
	                        OnResourceStringsChanging(ref value);
	                        MarkPropDirty("ResourceStrings"); 
	                        if (this._resourceStrings != null) {
	                            this.DetachChild(this._resourceStrings);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._resourceStrings = value; 
	                    }
	                    
	}
	
	    partial void FindByNameCalendar(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCalendar(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    private EventCallback<DateTime>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<DateTime> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<DateTime>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<DateTime>.Empty)) 
	            {
	                 if (!CompareEventCallbacks(value, _valueChanged, ref eventCallbacksCache))
	                {
	                    this.EnsureChangeHandled();
	
	                    _valueChanged = value;
	                }
	    }
	        else 
	            {
	                _valueChanged = null;
	    }
	    }
	    }
	
	    private EventCallback<DateTime[]>? _valuesChanged = null;
	    [Parameter]
	    public EventCallback<DateTime[]> ValuesChanged
	    {
	        get 
	        {
	            return this._valuesChanged != null ? this._valuesChanged.Value : EventCallback<DateTime[]>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<DateTime[]>.Empty)) 
	            {
	                 if (!CompareEventCallbacks(value, _valuesChanged, ref eventCallbacksCache))
	                {
	                    this.EnsureChangeHandled();
	
	                    _valuesChanged = value;
	                }
	    }
	        else 
	            {
	                _valuesChanged = null;
	    }
	    }
	    }
	
	    private string _changeRef = null;
	    private string _changeScript = null;
	    [Parameter]
	    public string ChangeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._changeRef = refName;
	                this.MarkPropDirty("ChangeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._changeScript;
	        }
	    }
	
	    partial void OnHandlingChange(IgbComponentDataValueChangedEventArgs args);
	    private EventCallback<IgbComponentDataValueChangedEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbComponentDataValueChangedEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbComponentDataValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentDataValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbComponentDataValueChangedEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(DateTime);
	
	    if (this.Selection == CalendarSelection.Single)
	    {
	        newValueValue = (DateTime)(args.Detail);
	        ;
	        OnEventUpdatingValue(this._value, ref newValueValue);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Value = newValueValue;
	        } else {
	            this._value = newValueValue;
	        }
	        OnPropertyPropagatedOut(Name, "Value");
	    }
	
	var newValueValues = default(DateTime[]);
	
	    if (this.Selection != CalendarSelection.Single)
	    {
	        newValueValues = (DateTime[])(DowncastArray<DateTime>(args.Detail));
	        ;
	        OnEventUpdatingValues(this._values, ref newValueValues);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Values = newValueValues;
	        } else {
	            this._values = newValueValues;
	        }
	        OnPropertyPropagatedOut(Name, "Values");
	    }
	
	    if (!EventCallback<DateTime>.Empty.Equals(ValueChanged))
	    {
	        var task = ValueChanged.InvokeAsync(newValueValue);
	        if (task.Exception != null)
	        {
	            throw task.Exception;
	        }
	    }
	
	    if (!EventCallback<DateTime[]>.Empty.Equals(ValuesChanged))
	    {
	        var task = ValuesChanged.InvokeAsync(newValueValues);
	        if (task.Exception != null)
	        {
	            throw task.Exception;
	        }
	    }
	
	                    });
	        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) => {
	                        this._changeRef = refName;
	                        this.MarkPropDirty("ChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _change = null;
	                this.SetHandler<IgbComponentDataValueChangedEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbComponentDataValueChangedEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbComponentDataValueChangedEventArgs>(null, (Action<IgbComponentDataValueChangedEventArgs>)((e) => { })); this._change = null;        
	        }
	    }
	
	
	                            partial void OnEventUpdatingValue(DateTime oldValue, ref DateTime newValue);
	
	                            partial void OnEventUpdatingValues(DateTime[] oldValue, ref DateTime[] newValue);
	
	    partial void SerializeCoreIgbCalendar(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCalendar(ser);
	
	if (IsPropDirty("Value")) { ser.AddDateTimeProp("value", this._value); }
	if (IsPropDirty("Values")) { ser.AddDateArrayProp("values", this._values); }
	if (IsPropDirty("ActiveDate")) { ser.AddDateTimeProp("activeDate", this._activeDate); }
	if (IsPropDirty("HideOutsideDays")) { ser.AddBooleanProp("hideOutsideDays", this._hideOutsideDays); }
	if (IsPropDirty("HideHeader")) { ser.AddBooleanProp("hideHeader", this._hideHeader); }
	if (IsPropDirty("HeaderOrientation")) { ser.AddEnumProp("headerOrientation", this._headerOrientation); }
	if (IsPropDirty("Orientation")) { ser.AddEnumProp("orientation", this._orientation); }
	if (IsPropDirty("VisibleMonths")) { ser.AddNumberProp("visibleMonths", this._visibleMonths); }
	if (IsPropDirty("ActiveView")) { ser.AddEnumProp("activeView", this._activeView); }
	if (IsPropDirty("FormatOptions")) { ser.AddSerializableProp("formatOptions", this._formatOptions); }
	if (IsPropDirty("ResourceStrings")) { ser.AddSerializableProp("resourceStrings", this._resourceStrings); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
