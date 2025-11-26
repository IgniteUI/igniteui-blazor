
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbKeyBindingOptions: BaseRendererElement {
                                public override string Type { get { return "WebKeyBindingOptions"; } }
	
	    public IgbKeyBindingOptions(): base() {
	        OnCreatedIgbKeyBindingOptions();
	
	        
	    }
	
	    partial void OnCreatedIgbKeyBindingOptions();
	    
	
	    partial void FindByNameKeyBindingOptions(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameKeyBindingOptions(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbKeyBindingOptions(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbKeyBindingOptions(ser);
	
	
	    }
	
}
}
