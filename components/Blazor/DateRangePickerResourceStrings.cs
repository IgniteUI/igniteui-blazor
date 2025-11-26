
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbDateRangePickerResourceStrings: BaseRendererElement {
                                public override string Type { get { return "WebDateRangePickerResourceStrings"; } }
	
	    public IgbDateRangePickerResourceStrings(): base() {
	        OnCreatedIgbDateRangePickerResourceStrings();
	
	        
	    }
	
	    partial void OnCreatedIgbDateRangePickerResourceStrings();
	    
	
	    partial void FindByNameDateRangePickerResourceStrings(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDateRangePickerResourceStrings(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbDateRangePickerResourceStrings(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDateRangePickerResourceStrings(ser);
	
	
	    }
	
}
}
