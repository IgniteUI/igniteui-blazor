
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbRangeSliderValue: BaseRendererElement {
                                public override string Type { get { return "WebRangeSliderValue"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbRangeSliderValue(): base() {
	        OnCreatedIgbRangeSliderValue();
	
	        
	    }
	
	    partial void OnCreatedIgbRangeSliderValue();
	    
	private double _lower = 0;
	
	partial void OnLowerChanging(ref double newValue);
	[Parameter]
	public double Lower 
	{
	get { return this._lower; }
	set { 
	                if (this._lower != value || !IsPropDirty("Lower")) {
	                        MarkPropDirty("Lower");
	                } 
	                this._lower = value;
	                 
	                }
	}
	private double _upper = 0;
	
	partial void OnUpperChanging(ref double newValue);
	[Parameter]
	public double Upper 
	{
	get { return this._upper; }
	set { 
	                if (this._upper != value || !IsPropDirty("Upper")) {
	                        MarkPropDirty("Upper");
	                } 
	                this._upper = value;
	                 
	                }
	}
	
	    partial void FindByNameRangeSliderValue(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRangeSliderValue(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbRangeSliderValue(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRangeSliderValue(ser);
	
	if (IsPropDirty("Lower")) { ser.AddNumberProp("lower", this._lower); }
	if (IsPropDirty("Upper")) { ser.AddNumberProp("upper", this._upper); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Lower")) { args["lower"] = (this._lower).ToString(); }
	if (IsPropDirty("Upper")) { args["upper"] = (this._upper).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("lower")) { this.Lower = ReturnToDouble(args["lower"]); }
	if (args.ContainsKey("upper")) { this.Upper = ReturnToDouble(args["upper"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
