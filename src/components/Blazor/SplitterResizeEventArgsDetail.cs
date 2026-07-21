
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbSplitterResizeEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebSplitterResizeEventArgsDetail"; } }

	
	                    private static bool _marshalByValue = true;
	
	    public IgbSplitterResizeEventArgsDetail(): base() {
	        OnCreatedIgbSplitterResizeEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbSplitterResizeEventArgsDetail();
	    
	private double _startPanelSize = 0;
	
	partial void OnStartPanelSizeChanging(ref double newValue);
	/// <summary>
	/// The current size of the start panel in pixels
	/// </summary>
	[Parameter]
	public double StartPanelSize 
	{
	get { return this._startPanelSize; }
	set { 
	                if (this._startPanelSize != value || !IsPropDirty("StartPanelSize")) {
	                        MarkPropDirty("StartPanelSize");
	                } 
	                this._startPanelSize = value;
	                 
	                }
	}
	private double _endPanelSize = 0;
	
	partial void OnEndPanelSizeChanging(ref double newValue);
	/// <summary>
	/// The current size of the end panel in pixels
	/// </summary>
	[Parameter]
	public double EndPanelSize 
	{
	get { return this._endPanelSize; }
	set { 
	                if (this._endPanelSize != value || !IsPropDirty("EndPanelSize")) {
	                        MarkPropDirty("EndPanelSize");
	                } 
	                this._endPanelSize = value;
	                 
	                }
	}
	private double _delta = 0;
	
	partial void OnDeltaChanging(ref double newValue);
	/// <summary>
	/// The change in size since the resize operation started (only for igcResizing and igcResizeEnd)
	/// </summary>
	[Parameter]
	public double Delta 
	{
	get { return this._delta; }
	set { 
	                if (this._delta != value || !IsPropDirty("Delta")) {
	                        MarkPropDirty("Delta");
	                } 
	                this._delta = value;
	                 
	                }
	}
	
	    partial void FindByNameSplitterResizeEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSplitterResizeEventArgsDetail(name, ref item);
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
	
	    partial void SerializeCoreIgbSplitterResizeEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSplitterResizeEventArgsDetail(ser);
	
	if (IsPropDirty("StartPanelSize")) { ser.AddNumberProp("startPanelSize", this._startPanelSize); }
	if (IsPropDirty("EndPanelSize")) { ser.AddNumberProp("endPanelSize", this._endPanelSize); }
	if (IsPropDirty("Delta")) { ser.AddNumberProp("delta", this._delta); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("StartPanelSize")) { args["startPanelSize"] = (this._startPanelSize).ToString(); }
	if (IsPropDirty("EndPanelSize")) { args["endPanelSize"] = (this._endPanelSize).ToString(); }
	if (IsPropDirty("Delta")) { args["delta"] = (this._delta).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("startPanelSize")) { this.StartPanelSize = ReturnToDouble(args["startPanelSize"]); }
	if (args.ContainsKey("endPanelSize")) { this.EndPanelSize = ReturnToDouble(args["endPanelSize"]); }
	if (args.ContainsKey("delta")) { this.Delta = ReturnToDouble(args["delta"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
