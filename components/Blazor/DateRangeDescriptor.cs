
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbDateRangeDescriptor: BaseRendererElement {
                                public override string Type { get { return "DateRangeDescriptor"; } }
	
	    public IgbDateRangeDescriptor(): base() {
	        OnCreatedIgbDateRangeDescriptor();
	
	        
	    }
	
	    partial void OnCreatedIgbDateRangeDescriptor();
	    
	private DateRangeType _rangeType = DateRangeType.After;
	
	partial void OnRangeTypeChanging(ref DateRangeType newValue);
	[Parameter]
	[WCWidgetMemberName("Type")]
	public DateRangeType RangeType 
	{
	get { return this._rangeType; }
	set { 
	                if (this._rangeType != value || !IsPropDirty("RangeType")) {
	                        MarkPropDirty("RangeType");
	                } 
	                this._rangeType = value;
	                 
	                }
	}
	private object _dateRange;
	
	partial void OnDateRangeChanging(ref object newValue);
	[Parameter]
	public object DateRange 
	{
	get { return this._dateRange; }
	set { 
	                if (this._dateRange != value || !IsPropDirty("DateRange")) {
	                        MarkPropDirty("DateRange");
	                } 
	                this._dateRange = value;
	                 
	                }
	}
	
	    partial void FindByNameDateRangeDescriptor(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDateRangeDescriptor(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbDateRangeDescriptor(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateRangeDescriptor(ser);
	
	if (IsPropDirty("RangeType")) { ser.AddEnumProp("rangeType", this._rangeType); }
	if (IsPropDirty("DateRange")) { ser.AddPrimitiveProp("dateRange", this._dateRange); }
	
	    }
	
}
}
