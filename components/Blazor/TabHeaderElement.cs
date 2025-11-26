
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Describes a tab header element.
/// </summary>
public partial class IgbTabHeaderElement: BaseRendererElement {
                                public override string Type { get { return "WebTabHeaderElement"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTabHeaderElementModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTabHeaderElementModule.Register(IgBlazor);
                                    }
                                }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTabHeaderElement(): base() {
	        OnCreatedIgbTabHeaderElement();
	
	        
	    }
	
	    partial void OnCreatedIgbTabHeaderElement();
	    
	private IgbDragService _dragService;
	
	partial void OnDragServiceChanging(ref IgbDragService newValue);
	/// <summary>
	/// Gets/sets the drag service.
	/// </summary>
	[Parameter]
	public IgbDragService DragService 
	{
	get { return this._dragService; }
	set { 
	                        OnDragServiceChanging(ref value);
	                        MarkPropDirty("DragService"); 
	                        if (this._dragService != null) {
	                            this.DetachChild(this._dragService);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._dragService = value; 
	                    }
	                    
	}
	
	    partial void FindByNameTabHeaderElement(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTabHeaderElement(name, ref item);
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
	
	    partial void SerializeCoreIgbTabHeaderElement(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTabHeaderElement(ser);
	
	if (IsPropDirty("DragService")) { ser.AddSerializableProp("dragService", this._dragService); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("DragService")) { args["dragService"] = ObjectToParam(this._dragService); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("dragService")) { this.DragService = (IgbDragService)ConvertReturnValue(args["dragService"], "DragService", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}

public class IgbTabHeaderElementModule 
{
    public static void Register(IIgniteUIBlazor runtime) {
        ModuleLoader.Load(runtime, "WebTabHeaderElementModule");
    }

    public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
        ModuleLoader.MarkIsLoadRequested(runtime, "WebTabHeaderElementModule");
    }


    public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
        return ModuleLoader.IsLoadRequested(runtime, "WebTabHeaderElementModule");
    }
}

}
