
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTileChangeStateEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebTileChangeStateEventArgsDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbTileChangeStateEventArgsDetail(): base() {
	        OnCreatedIgbTileChangeStateEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbTileChangeStateEventArgsDetail();
	    
	private IgbTile _tile;
	
	partial void OnTileChanging(ref IgbTile newValue);
	[Parameter]
	public IgbTile Tile 
	{
	get { return this._tile; }
	set { 
	                if (this._tile != value || !IsPropDirty("Tile")) {
	                        MarkPropDirty("Tile");
	                } 
	                this._tile = value;
	                 
	                }
	}
	private bool _state = false;
	
	partial void OnStateChanging(ref bool newValue);
	[Parameter]
	public bool State 
	{
	get { return this._state; }
	set { 
	                if (this._state != value || !IsPropDirty("State")) {
	                        MarkPropDirty("State");
	                } 
	                this._state = value;
	                 
	                }
	}
	
	    partial void FindByNameTileChangeStateEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTileChangeStateEventArgsDetail(name, ref item);
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
	
	    partial void SerializeCoreIgbTileChangeStateEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTileChangeStateEventArgsDetail(ser);
	
	if (IsPropDirty("Tile")) { ser.AddSerializableProp("tile", this._tile); }
	if (IsPropDirty("State")) { ser.AddBooleanProp("state", this._state); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Tile")) { args["tile"] = ObjectToParam(this._tile); }
	if (IsPropDirty("State")) { args["state"] = (this._state).ToString().ToLower(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("tile")) { this.Tile = (IgbTile)ConvertReturnValue(args["tile"], "Tile", true); }
	if (args.ContainsKey("state")) { this.State = ReturnToBoolean(args["state"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
