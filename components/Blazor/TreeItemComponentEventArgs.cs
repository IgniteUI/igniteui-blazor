
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTreeItemComponentEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebTreeItemComponentEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTreeItemComponentEventArgs(): base() {
	        OnCreatedIgbTreeItemComponentEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbTreeItemComponentEventArgs();
	    
	private IgbTreeItem _detail;
	
	partial void OnDetailChanging(ref IgbTreeItem newValue);
	[Parameter]
	public IgbTreeItem Detail 
	{
	get { return this._detail; }
	set { 
	                if (this._detail != value || !IsPropDirty("Detail")) {
	                        MarkPropDirty("Detail");
	                } 
	                this._detail = value;
	                 
	                }
	}
	
	    partial void FindByNameTreeItemComponentEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTreeItemComponentEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbTreeItemComponentEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTreeItemComponentEventArgs(ser);
	
	if (IsPropDirty("Detail")) { ser.AddSerializableProp("detail", this._detail); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Detail")) { args["detail"] = ObjectToParam(this._detail); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbTreeItem)ConvertReturnValue(args["detail"], "TreeItem", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
