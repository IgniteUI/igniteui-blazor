
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbKeyBindingHandler: BaseRendererElement {
                                public override string Type { get { return "WebKeyBindingHandler"; } }
	
	    public IgbKeyBindingHandler(): base() {
	        OnCreatedIgbKeyBindingHandler();
	
	        
	    }
	
	    partial void OnCreatedIgbKeyBindingHandler();
	    
	
	    partial void FindByNameKeyBindingHandler(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameKeyBindingHandler(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbKeyBindingHandler(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbKeyBindingHandler(ser);
	
	
	    }
	
}
}
