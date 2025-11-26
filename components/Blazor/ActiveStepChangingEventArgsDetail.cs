
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbActiveStepChangingEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebActiveStepChangingEventArgsDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbActiveStepChangingEventArgsDetail(): base() {
	        OnCreatedIgbActiveStepChangingEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbActiveStepChangingEventArgsDetail();
	    
	private double _oldIndex = 0;
	
	partial void OnOldIndexChanging(ref double newValue);
	[Parameter]
	public double OldIndex 
	{
	get { return this._oldIndex; }
	set { 
	                if (this._oldIndex != value || !IsPropDirty("OldIndex")) {
	                        MarkPropDirty("OldIndex");
	                } 
	                this._oldIndex = value;
	                 
	                }
	}
	private double _newIndex = 0;
	
	partial void OnNewIndexChanging(ref double newValue);
	[Parameter]
	public double NewIndex 
	{
	get { return this._newIndex; }
	set { 
	                if (this._newIndex != value || !IsPropDirty("NewIndex")) {
	                        MarkPropDirty("NewIndex");
	                } 
	                this._newIndex = value;
	                 
	                }
	}
	
	    partial void FindByNameActiveStepChangingEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameActiveStepChangingEventArgsDetail(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbActiveStepChangingEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbActiveStepChangingEventArgsDetail(ser);
	
	if (IsPropDirty("OldIndex")) { ser.AddNumberProp("oldIndex", this._oldIndex); }
	if (IsPropDirty("NewIndex")) { ser.AddNumberProp("newIndex", this._newIndex); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("OldIndex")) { args["oldIndex"] = (this._oldIndex).ToString(); }
	if (IsPropDirty("NewIndex")) { args["newIndex"] = (this._newIndex).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("oldIndex")) { this.OldIndex = ReturnToDouble(args["oldIndex"]); }
	if (args.ContainsKey("newIndex")) { this.NewIndex = ReturnToDouble(args["newIndex"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
