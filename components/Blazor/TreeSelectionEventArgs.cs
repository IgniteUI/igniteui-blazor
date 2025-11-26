
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTreeSelectionEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebTreeSelectionEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTreeSelectionEventArgs(): base() {
	        OnCreatedIgbTreeSelectionEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbTreeSelectionEventArgs();
	    
	private IgbTreeSelectionEventArgsDetail _detail;
	
	partial void OnDetailChanging(ref IgbTreeSelectionEventArgsDetail newValue);
	[Parameter]
	public IgbTreeSelectionEventArgsDetail Detail 
	{
	get { return this._detail; }
	set { 
	                        OnDetailChanging(ref value);
	                        MarkPropDirty("Detail"); 
	                        if (this._detail != null) {
	                            this.DetachChild(this._detail);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._detail = value; 
	                    }
	                    
	}
	
	    partial void FindByNameTreeSelectionEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTreeSelectionEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbTreeSelectionEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTreeSelectionEventArgs(ser);
	
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
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbTreeSelectionEventArgsDetail)ConvertReturnValue(args["detail"], "TreeSelectionEventArgsDetail", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
