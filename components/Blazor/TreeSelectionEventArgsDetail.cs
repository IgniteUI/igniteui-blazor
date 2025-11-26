
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTreeSelectionEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebTreeSelectionEventArgsDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTreeSelectionEventArgsDetail(): base() {
	        OnCreatedIgbTreeSelectionEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbTreeSelectionEventArgsDetail();
	    
	private IgbTreeItem[] _newSelection;
	
	partial void OnNewSelectionChanging(ref IgbTreeItem[] newValue);
	[Parameter]
	public IgbTreeItem[] NewSelection 
	{
	get { return this._newSelection; }
	set { 
	                if (this._newSelection != value || !IsPropDirty("NewSelection")) {
	                        MarkPropDirty("NewSelection");
	                } 
	                this._newSelection = value;
	                 
	                }
	}
	
	    partial void FindByNameTreeSelectionEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTreeSelectionEventArgsDetail(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbTreeSelectionEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTreeSelectionEventArgsDetail(ser);
	
	if (IsPropDirty("NewSelection")) { ser.AddSerializableArrayProp("newSelection", this._newSelection); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("NewSelection")) { args["newSelection"] = ObjectArrayToParam(this._newSelection); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("newSelection")) { this.NewSelection = ReturnToObjectArray<IgbTreeItem>(args["newSelection"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
