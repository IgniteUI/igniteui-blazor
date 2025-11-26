
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbFilteringOptions: BaseRendererElement {
                                public override string Type { get { return "WebFilteringOptions"; } }
	
	    public IgbFilteringOptions(): base() {
	        OnCreatedIgbFilteringOptions();
	
	        
	    }
	
	    partial void OnCreatedIgbFilteringOptions();
	    
	
	    partial void FindByNameFilteringOptions(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameFilteringOptions(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbFilteringOptions(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbFilteringOptions(ser);
	
	
	    }
	
}
}
