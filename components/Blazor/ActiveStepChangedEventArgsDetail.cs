
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbActiveStepChangedEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebActiveStepChangedEventArgsDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbActiveStepChangedEventArgsDetail(): base() {
	        OnCreatedIgbActiveStepChangedEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbActiveStepChangedEventArgsDetail();
	    
	private double _index = 0;
	
	partial void OnIndexChanging(ref double newValue);
	[Parameter]
	public double Index 
	{
	get { return this._index; }
	set { 
	                if (this._index != value || !IsPropDirty("Index")) {
	                        MarkPropDirty("Index");
	                } 
	                this._index = value;
	                 
	                }
	}
	
	    partial void FindByNameActiveStepChangedEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameActiveStepChangedEventArgsDetail(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbActiveStepChangedEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbActiveStepChangedEventArgsDetail(ser);
	
	if (IsPropDirty("Index")) { ser.AddNumberProp("index", this._index); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Index")) { args["index"] = (this._index).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("index")) { this.Index = ReturnToDouble(args["index"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
