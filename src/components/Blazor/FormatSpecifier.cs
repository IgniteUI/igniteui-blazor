
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbFormatSpecifier: BaseRendererElement {
                                public override string Type { get { return "FormatSpecifier"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbFormatSpecifierModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbFormatSpecifierModule.Register(IgBlazor);
                                    }
                                }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbFormatSpecifier(): base() {
	        OnCreatedIgbFormatSpecifier();
	
	        
	    }
	
	    partial void OnCreatedIgbFormatSpecifier();
	    
	
	    partial void FindByNameFormatSpecifier(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameFormatSpecifier(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async Task<String> GetLocalCultureAsync() 
	                    {
		var iv = await InvokeMethod("getLocalCulture", new object[] {  }, new string[] {  });
		return ReturnToString(iv);
	}
	                    public String GetLocalCulture() 
	                    {
		var iv = InvokeMethodSync("getLocalCulture", new object[] {  }, new string[] {  });
		return ReturnToString(iv);
	}
	
	    partial void SerializeCoreIgbFormatSpecifier(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbFormatSpecifier(ser);
	
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	
	        this.SuppressParentNotify = false;
	    }
	
}

public class IgbFormatSpecifierModule 
{
    public static void Register(IIgniteUIBlazor runtime) {
        ModuleLoader.Load(runtime, "FormatSpecifierModule");
    }

    public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
        ModuleLoader.MarkIsLoadRequested(runtime, "FormatSpecifierModule");
    }


    public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
        return ModuleLoader.IsLoadRequested(runtime, "FormatSpecifierModule");
    }
}

}
