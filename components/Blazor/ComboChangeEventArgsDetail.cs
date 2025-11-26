
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbComboChangeEventArgsDetail: BaseRendererElement {
                                public override string Type { get { return "WebComboChangeEventArgsDetail"; } }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbComboChangeEventArgsDetail(): base() {
	        OnCreatedIgbComboChangeEventArgsDetail();
	
	        
	    }
	
	    partial void OnCreatedIgbComboChangeEventArgsDetail();
	    
	private string _newValueRef;
	private object[] _newValue;
	
	partial void OnNewValueChanging(ref object[] newValue);
	[Parameter]
	public object[] NewValue 
	{
	get { return this._newValue; }
	
	    set { 
	        var oldValue = this._newValue; 
	        OnNewValueChanging(ref value);
	        
	                    
	        if (oldValue != value || !IsPropDirty("NewValue"))
	        {
	            MarkPropDirty("NewValue"); 
	            this._newValue = value; 
	            this.OnRefChanged("NewValue", oldValue, value, false, false, (string refName, object old, object newValue) => {
	        	    this._newValueRef = refName;
	                this.MarkPropDirty("NewValueRef");
	        }); 
	        }
	    }
	}
	
	
	private string _newValueScript;
	
	
	///<summary>Provides a means of setting NewValue in the JavaScript environment.</summary>
	[Parameter]
	public string NewValueScript 
	{
	get { return _newValueScript; }
	
	
	    set { 
	        var oldValue = this._newValueScript; 
	        if (oldValue != value || !IsPropDirty("NewValue"))
	        {
	            MarkPropDirty("NewValue"); 
	            this.OnRefChanged("NewValue", oldValue, value, true, false, (string refName, object old, object newValue) => {
	        	    this._newValueRef = refName;
	                this.MarkPropDirty("NewValueRef");
	        }); 
	        }
	    }
	}
	private string _itemsRef;
	private object[] _items;
	
	partial void OnItemsChanging(ref object[] newValue);
	[Parameter]
	public object[] Items 
	{
	get { return this._items; }
	
	    set { 
	        var oldValue = this._items; 
	        OnItemsChanging(ref value);
	        
	                    
	        if (oldValue != value || !IsPropDirty("Items"))
	        {
	            MarkPropDirty("Items"); 
	            this._items = value; 
	            this.OnRefChanged("Items", oldValue, value, false, false, (string refName, object old, object newValue) => {
	        	    this._itemsRef = refName;
	                this.MarkPropDirty("ItemsRef");
	        }); 
	        }
	    }
	}
	
	
	private string _itemsScript;
	
	
	///<summary>Provides a means of setting Items in the JavaScript environment.</summary>
	[Parameter]
	public string ItemsScript 
	{
	get { return _itemsScript; }
	
	
	    set { 
	        var oldValue = this._itemsScript; 
	        if (oldValue != value || !IsPropDirty("Items"))
	        {
	            MarkPropDirty("Items"); 
	            this.OnRefChanged("Items", oldValue, value, true, false, (string refName, object old, object newValue) => {
	        	    this._itemsRef = refName;
	                this.MarkPropDirty("ItemsRef");
	        }); 
	        }
	    }
	}
	private ComboChangeType _changeType = ComboChangeType.Selection;
	
	partial void OnChangeTypeChanging(ref ComboChangeType newValue);
	[Parameter]
	[WCWidgetMemberName("Type")]
	public ComboChangeType ChangeType 
	{
	get { return this._changeType; }
	set { 
	                if (this._changeType != value || !IsPropDirty("ChangeType")) {
	                        MarkPropDirty("ChangeType");
	                } 
	                this._changeType = value;
	                 
	                }
	}
	
	    partial void FindByNameComboChangeEventArgsDetail(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameComboChangeEventArgsDetail(name, ref item);
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
	
	    partial void SerializeCoreIgbComboChangeEventArgsDetail(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbComboChangeEventArgsDetail(ser);
	
	if (IsPropDirty("NewValue")) { ser.AddArrayProp("newValue", this._newValue); }
	if (IsPropDirty("Items")) { ser.AddArrayProp("items", this._items); }
	if (IsPropDirty("ChangeType")) { ser.AddEnumProp("changeType", this._changeType); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("NewValue")) { args["newValue"] = ObjectArrayToParam(this._newValue); }
	if (IsPropDirty("Items")) { args["items"] = ObjectArrayToParam(this._items); }
	if (IsPropDirty("ChangeType")) { args["changeType"] = EnumToString(this._changeType); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("newValue")) { this.NewValue = ReturnToObjectArray(args["newValue"]); }
	if (args.ContainsKey("items")) { this.Items = ReturnToObjectArray(args["items"]); }
	if (args.ContainsKey("changeType")) { this.ChangeType = StringToEnum<ComboChangeType>(args["changeType"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}
}
