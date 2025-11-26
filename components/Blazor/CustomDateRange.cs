
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbCustomDateRange: BaseRendererElement {
                                public override string Type { get { return "WebCustomDateRange"; } }
	
	    public IgbCustomDateRange(): base() {
	        OnCreatedIgbCustomDateRange();
	
	        
	    }
	
	    partial void OnCreatedIgbCustomDateRange();
	    
	private string _label;
	
	partial void OnLabelChanging(ref string newValue);
	[Parameter]
	public string Label 
	{
	get { return this._label; }
	set { 
	                if (this._label != value || !IsPropDirty("Label")) {
	                        MarkPropDirty("Label");
	                } 
	                this._label = value;
	                 
	                }
	}
	private IgbDateRangeValue _dateRange;
	
	partial void OnDateRangeChanging(ref IgbDateRangeValue newValue);
	[Parameter]
	public IgbDateRangeValue DateRange 
	{
	get { return this._dateRange; }
	set { 
	                        OnDateRangeChanging(ref value);
	                        MarkPropDirty("DateRange"); 
	                        if (this._dateRange != null) {
	                            this.DetachChild(this._dateRange);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._dateRange = value; 
	                    }
	                    
	}
	
	    partial void FindByNameCustomDateRange(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCustomDateRange(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbCustomDateRange(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCustomDateRange(ser);
	
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("DateRange")) { ser.AddSerializableProp("dateRange", this._dateRange); }
	
	    }
	
}
}
