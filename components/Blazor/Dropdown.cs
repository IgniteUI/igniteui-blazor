
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a DropDown component.
/// </summary>
public partial class IgbDropdown: IgbBaseComboBoxLike {
                                public override string Type { get { return "WebDropdown"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDropdownModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDropdownModule.Register(IgBlazor);
                                    }
                                }

                            protected override string ResolveDisplay()
                        {
	                        return "inline-block";
                        }

                            protected override bool SupportsVisualChildren
                        {
                                get 
                                {
	                            return true;
                                }
                        }

                            protected override bool UseDirectRender
                        {
                                get 
                                {
	                            return true;
                                }
                        }

                            protected override string DirectRenderElementName
                        {
                                get 
                                {
	                            return "igc-dropdown";
                                }
                        }
	
	    public IgbDropdown(): base() {
	        OnCreatedIgbDropdown();
	
	        
	    }
	
	    partial void OnCreatedIgbDropdown();
	    
	private PopoverPlacement _placement = PopoverPlacement.Top;
	
	partial void OnPlacementChanging(ref PopoverPlacement newValue);
	/// <summary>
	/// The preferred placement of the component around the target element.
	/// </summary>
	[Parameter]
	public PopoverPlacement Placement 
	{
	get { return this._placement; }
	set { 
	                if (this._placement != value || !IsPropDirty("Placement")) {
	                        MarkPropDirty("Placement");
	                } 
	                this._placement = value;
	                 
	                }
	}
	private PopoverScrollStrategy _scrollStrategy = PopoverScrollStrategy.Scroll;
	
	partial void OnScrollStrategyChanging(ref PopoverScrollStrategy newValue);
	/// <summary>
	/// Determines the behavior of the component during scrolling of the parent container.
	/// </summary>
	[Parameter]
	public PopoverScrollStrategy ScrollStrategy 
	{
	get { return this._scrollStrategy; }
	set { 
	                if (this._scrollStrategy != value || !IsPropDirty("ScrollStrategy")) {
	                        MarkPropDirty("ScrollStrategy");
	                } 
	                this._scrollStrategy = value;
	                 
	                }
	}
	private bool _flip = false;
	
	partial void OnFlipChanging(ref bool newValue);
	/// <summary>
	/// Whether the component should be flipped to the opposite side of the target once it's about to overflow the visible area.
	/// When true, once enough space is detected on its preferred side, it will flip back.
	/// </summary>
	[Parameter]
	public bool Flip 
	{
	get { return this._flip; }
	set { 
	                if (this._flip != value || !IsPropDirty("Flip")) {
	                        MarkPropDirty("Flip");
	                } 
	                this._flip = value;
	                 
	                }
	}
	private double _distance = 0;
	
	partial void OnDistanceChanging(ref double newValue);
	/// <summary>
	/// The distance from the target element.
	/// </summary>
	[Parameter]
	public double Distance 
	{
	get { return this._distance; }
	set { 
	                if (this._distance != value || !IsPropDirty("Distance")) {
	                        MarkPropDirty("Distance");
	                } 
	                this._distance = value;
	                 
	                }
	}
	private bool _sameWidth = false;
	
	partial void OnSameWidthChanging(ref bool newValue);
	/// <summary>
	/// Whether the dropdown's width should be the same as the target's one.
	/// </summary>
	[Parameter]
	public bool SameWidth 
	{
	get { return this._sameWidth; }
	set { 
	                if (this._sameWidth != value || !IsPropDirty("SameWidth")) {
	                        MarkPropDirty("SameWidth");
	                } 
	                this._sameWidth = value;
	                 
	                }
	}
	public async Task<IgbDropdownItem[]> GetItemsAsync()
	                    {
		var iv = await InvokeMethod("p:Items", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbDropdownItem[]);
	    }
	    var retVal = ReturnToObjectArray<IgbDropdownItem>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem[]);
	    }
	    return retVal;
	
	}
	                    public IgbDropdownItem[] GetItems()
	                    {
		var iv = InvokeMethodSync("p:Items", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbDropdownItem[]);
	    }
	    var retVal = ReturnToObjectArray<IgbDropdownItem>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem[]);
	    }
	    return retVal;
	
	}
	public async Task<IgbDropdownGroup[]> GetGroupsAsync()
	                    {
		var iv = await InvokeMethod("p:Groups", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbDropdownGroup[]);
	    }
	    var retVal = ReturnToObjectArray<IgbDropdownGroup>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownGroup[]);
	    }
	    return retVal;
	
	}
	                    public IgbDropdownGroup[] GetGroups()
	                    {
		var iv = InvokeMethodSync("p:Groups", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbDropdownGroup[]);
	    }
	    var retVal = ReturnToObjectArray<IgbDropdownGroup>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownGroup[]);
	    }
	    return retVal;
	
	}
	public async Task<IgbDropdownItem?> GetSelectedItemAsync()
	                    {
		var iv = await InvokeMethod("p:SelectedItem", new object[] { }, new string[] { });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	                    public IgbDropdownItem? GetSelectedItem()
	                    {
		var iv = InvokeMethodSync("p:SelectedItem", new object[] { }, new string[] { });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	
	    partial void FindByNameDropdown(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDropdown(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	/// <summary>
	/// Navigates to the item at the specified index. If it exists, returns the found item, otherwise - null.
	/// </summary>
	public async Task<IgbDropdownItem> NavigateToAsync(Object index)
	                    {
		var iv = await InvokeMethod("navigateTo", new object[] { ObjectToParam(index) }, new string[] { "Json" });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	                    public IgbDropdownItem NavigateTo(Object index)
	                    {
		var iv = InvokeMethodSync("navigateTo", new object[] { ObjectToParam(index) }, new string[] { "Json" });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	/// <summary>
	/// Selects the item with the specified value. If it exists, returns the found item, otherwise - null.
	/// </summary>
	public async Task<IgbDropdownItem> SelectAsync(Object value)
	                    {
		var iv = await InvokeMethod("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	                    public IgbDropdownItem Select(Object value)
	                    {
		var iv = InvokeMethodSync("select", new object[] { ObjectToParam(value) }, new string[] { "Json" });
		
	    if (iv == null)
	    {
	        return default(IgbDropdownItem);
	    }
	    var retVal = (IgbDropdownItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbDropdownItem);
	    }
	    return retVal;
	
	}
	public async  Task DisconnectedCallbackAsync() 
	                    {
		await InvokeMethod("disconnectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void DisconnectedCallback() 
	                    {
		InvokeMethodSync("disconnectedCallback", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Clears the current selection of the dropdown.
	/// </summary>
	public async  Task ClearSelectionAsync() 
	                    {
		await InvokeMethod("clearSelection", new object[] {  }, new string[] {  });
	}
	                    public  void ClearSelection() 
	                    {
		InvokeMethodSync("clearSelection", new object[] {  }, new string[] {  });
	}
	
	    private string _openingRef = null;
	    private string _openingScript = null;
	    [Parameter]
	    public string OpeningScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opening", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openingRef = refName;
	                this.MarkPropDirty("OpeningRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openingScript;
	        }
	    }
	
	    partial void OnHandlingOpening(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opening = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Opening
	    {
	        get 
	        {
	            return this._opening != null ? this._opening.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opening, ref eventCallbacksCache))
	                {
	                    _opening = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", value, (args) => {
	                        OnHandlingOpening(args);
	                        
	                    });
	        this.OnRefChanged("Opening", null, "event:::Opening", true, false, (refName, oldValue, newValue) => {
	                        this._openingRef = refName;
	                        this.MarkPropDirty("OpeningRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opening = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", null);
	    this.OnRefChanged("Opening", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openingRef = null;
	                    this.MarkPropDirty("OpeningRef");	
	            });
	    }
	    }
	    }
	
	    private string _openedRef = null;
	    private string _openedScript = null;
	    [Parameter]
	    public string OpenedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opened", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openedRef = refName;
	                this.MarkPropDirty("OpenedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openedScript;
	        }
	    }
	
	    partial void OnHandlingOpened(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opened = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Opened
	    {
	        get 
	        {
	            return this._opened != null ? this._opened.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opened, ref eventCallbacksCache))
	                {
	                    _opened = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", value, (args) => {
	                        OnHandlingOpened(args);
	                        
	                    });
	        this.OnRefChanged("Opened", null, "event:::Opened", true, false, (refName, oldValue, newValue) => {
	                        this._openedRef = refName;
	                        this.MarkPropDirty("OpenedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opened = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", null);
	    this.OnRefChanged("Opened", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openedRef = null;
	                    this.MarkPropDirty("OpenedRef");	
	            });
	    }
	    }
	    }
	
	    private string _closingRef = null;
	    private string _closingScript = null;
	    [Parameter]
	    public string ClosingScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closingRef = refName;
	                this.MarkPropDirty("ClosingRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closingScript;
	        }
	    }
	
	    partial void OnHandlingClosing(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closing = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Closing
	    {
	        get 
	        {
	            return this._closing != null ? this._closing.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closing, ref eventCallbacksCache))
	                {
	                    _closing = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value, (args) => {
	                        OnHandlingClosing(args);
	                        
	                    });
	        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) => {
	                        this._closingRef = refName;
	                        this.MarkPropDirty("ClosingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closing = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
	    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closingRef = null;
	                    this.MarkPropDirty("ClosingRef");	
	            });
	    }
	    }
	    }
	
	    private string _closedRef = null;
	    private string _closedScript = null;
	    [Parameter]
	    public string ClosedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closedRef = refName;
	                this.MarkPropDirty("ClosedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closedScript;
	        }
	    }
	
	    partial void OnHandlingClosed(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closed = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Closed
	    {
	        get 
	        {
	            return this._closed != null ? this._closed.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closed, ref eventCallbacksCache))
	                {
	                    _closed = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value, (args) => {
	                        OnHandlingClosed(args);
	                        
	                    });
	        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) => {
	                        this._closedRef = refName;
	                        this.MarkPropDirty("ClosedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closed = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
	    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closedRef = null;
	                    this.MarkPropDirty("ClosedRef");	
	            });
	    }
	    }
	    }
	
	    private string _changeRef = null;
	    private string _changeScript = null;
	    [Parameter]
	    public string ChangeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._changeRef = refName;
	                this.MarkPropDirty("ChangeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._changeScript;
	        }
	    }
	
	    partial void OnHandlingChange(IgbDropdownItemComponentEventArgs args);
	    private EventCallback<IgbDropdownItemComponentEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbDropdownItemComponentEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbDropdownItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbDropdownItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbDropdownItemComponentEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	                    });
	        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) => {
	                        this._changeRef = refName;
	                        this.MarkPropDirty("ChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _change = null;
	                this.SetHandler<IgbDropdownItemComponentEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbDropdown(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDropdown(ser);
	
	if (IsPropDirty("Placement")) { ser.AddEnumProp("placement", this._placement); }
	if (IsPropDirty("ScrollStrategy")) { ser.AddEnumProp("scrollStrategy", this._scrollStrategy); }
	if (IsPropDirty("Flip")) { ser.AddBooleanProp("flip", this._flip); }
	if (IsPropDirty("Distance")) { ser.AddNumberProp("distance", this._distance); }
	if (IsPropDirty("SameWidth")) { ser.AddBooleanProp("sameWidth", this._sameWidth); }
	if (IsPropDirty("OpeningRef")) { ser.AddStringProp("openingRef", this._openingRef); }
	if (IsPropDirty("OpenedRef")) { ser.AddStringProp("openedRef", this._openedRef); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
