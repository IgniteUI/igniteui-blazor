
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbDragService: BaseRendererElement {
                                public override string Type { get { return "WebDragService"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDragServiceModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDragServiceModule.Register(IgBlazor);
                                    }
                                }
	
	    public IgbDragService(): base() {
	        OnCreatedIgbDragService();
	
	        
	    }
	
	    partial void OnCreatedIgbDragService();
	    
	
	    partial void FindByNameDragService(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDragService(name, ref item);
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
	
	    partial void SerializeCoreIgbDragService(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDragService(ser);
	
	
	    }
	
}

public class IgbDragServiceModule 
{
    public static void Register(IIgniteUIBlazor runtime) {
        ModuleLoader.Load(runtime, "WebDragServiceModule");
    }

    public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
        ModuleLoader.MarkIsLoadRequested(runtime, "WebDragServiceModule");
    }


    public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
        return ModuleLoader.IsLoadRequested(runtime, "WebDragServiceModule");
    }
}

}
