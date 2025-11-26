
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbIconMeta: BaseRendererElement {
                                public override string Type { get { return "WebIconMeta"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbIconMeta(): base() {
	        OnCreatedIgbIconMeta();
	
	        
	    }
	
	    partial void OnCreatedIgbIconMeta();
	    
	private string _collection;
	
	partial void OnCollectionChanging(ref string newValue);
	[Parameter]
	public string Collection 
	{
	get { return this._collection; }
	set { 
	                if (this._collection != value || !IsPropDirty("Collection")) {
	                        MarkPropDirty("Collection");
	                } 
	                this._collection = value;
	                 
	                }
	}
	
	    partial void FindByNameIconMeta(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameIconMeta(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbIconMeta(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbIconMeta(ser);
	
	if (IsPropDirty("Collection")) { ser.AddStringProp("collection", this._collection); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Name")) { args["name"] = this._name; }
	if (IsPropDirty("Collection")) { args["collection"] = this._collection; }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("name")) { this.Name = ReturnToString(args["name"]); }
	if (args.ContainsKey("collection")) { this.Collection = ReturnToString(args["collection"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
