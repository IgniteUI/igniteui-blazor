
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComboChangeEventArgs: BaseRendererElement {
                                public override string Type { get { return "WebComboChangeEventArgs"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbComboChangeEventArgs(): base() {
	        OnCreatedIgbComboChangeEventArgs();
	
	        
	    }
	
	    partial void OnCreatedIgbComboChangeEventArgs();
	    
	private IgbComboChangeEventArgsDetail _detail;
	
	partial void OnDetailChanging(ref IgbComboChangeEventArgsDetail newValue);
	[Parameter]
	public IgbComboChangeEventArgsDetail Detail 
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
	
	    partial void FindByNameComboChangeEventArgs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComboChangeEventArgs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbComboChangeEventArgs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComboChangeEventArgs(ser);
	
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
	
	if (args.ContainsKey("detail")) { this.Detail = (IgbComboChangeEventArgsDetail)ConvertReturnValue(args["detail"], "ComboChangeEventArgsDetail", true); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
