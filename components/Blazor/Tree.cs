
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The tree allows users to represent hierarchical data in a tree-view structure,
/// maintaining parent-child relationships, as well as to define static tree-view structure without a corresponding data model.
/// </summary>
public partial class IgbTree: BaseRendererControl {
                                public override string Type { get { return "WebTree"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTreeModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTreeModule.Register(IgBlazor);
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
                            return "igc-tree";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbTree(): base() {
	        OnCreatedIgbTree();
	
	        
	    }
	
	    partial void OnCreatedIgbTree();
	    
	private bool _singleBranchExpand = false;
	
	partial void OnSingleBranchExpandChanging(ref bool newValue);
	/// <summary>
	/// Whether a single or multiple of a parent's child items can be expanded.
	/// </summary>
	[Parameter]
	public bool SingleBranchExpand 
	{
	get { return this._singleBranchExpand; }
	set { 
	                if (this._singleBranchExpand != value || !IsPropDirty("SingleBranchExpand")) {
	                        MarkPropDirty("SingleBranchExpand");
	                } 
	                this._singleBranchExpand = value;
	                 
	                }
	}
	private bool _toggleNodeOnClick = false;
	
	partial void OnToggleNodeOnClickChanging(ref bool newValue);
	/// <summary>
	/// Whether clicking over nodes will change their expanded state or not.
	/// </summary>
	[Parameter]
	public bool ToggleNodeOnClick 
	{
	get { return this._toggleNodeOnClick; }
	set { 
	                if (this._toggleNodeOnClick != value || !IsPropDirty("ToggleNodeOnClick")) {
	                        MarkPropDirty("ToggleNodeOnClick");
	                } 
	                this._toggleNodeOnClick = value;
	                 
	                }
	}
	private TreeSelection _selection = TreeSelection.None;
	
	partial void OnSelectionChanging(ref TreeSelection newValue);
	/// <summary>
	/// The selection state of the tree.
	/// </summary>
	[Parameter]
	public TreeSelection Selection 
	{
	get { return this._selection; }
	set { 
	                if (this._selection != value || !IsPropDirty("Selection")) {
	                        MarkPropDirty("Selection");
	                } 
	                this._selection = value;
	                 
	                }
	}
	
	    partial void FindByNameTree(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTree(name, ref item);
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
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// @private
	/// </summary>
	public async  Task ExpandToItemAsync(IgbTreeItem item) 
	                    {
		await InvokeMethod("expandToItem", new object[] { ObjectToParam(item) }, new string[] { "Json" });
	}
	                    public  void ExpandToItem(IgbTreeItem item) 
	                    {
		InvokeMethodSync("expandToItem", new object[] { ObjectToParam(item) }, new string[] { "Json" });
	}
	
	    private string _selectionChangedRef = null;
	    private string _selectionChangedScript = null;
	    [Parameter]
	    public string SelectionChangedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("SelectionChanged", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._selectionChangedRef = refName;
	                this.MarkPropDirty("SelectionChangedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._selectionChangedScript;
	        }
	    }
	
	    partial void OnHandlingSelectionChanged(IgbTreeSelectionEventArgs args);
	    private EventCallback<IgbTreeSelectionEventArgs>? _selectionChanged = null;
	    [Parameter]
	    public EventCallback<IgbTreeSelectionEventArgs> SelectionChanged
	    {
	        get 
	        {
	            return this._selectionChanged != null ? this._selectionChanged.Value : EventCallback<IgbTreeSelectionEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeSelectionEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _selectionChanged, ref eventCallbacksCache))
	                {
	                    _selectionChanged = value;
	                    this.SetHandler<IgbTreeSelectionEventArgs>(this.Name, "SelectionChanged", value, (args) => {
	                        OnHandlingSelectionChanged(args);
	                        
	                    });
	        this.OnRefChanged("SelectionChanged", null, "event:::SelectionChanged", true, false, (refName, oldValue, newValue) => {
	                        this._selectionChangedRef = refName;
	                        this.MarkPropDirty("SelectionChangedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _selectionChanged = null;
	                this.SetHandler<IgbTreeSelectionEventArgs>(this.Name, "SelectionChanged", null);
	    this.OnRefChanged("SelectionChanged", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._selectionChangedRef = null;
	                    this.MarkPropDirty("SelectionChangedRef");	
	            });
	    }
	    }
	    }
	
	    private string _itemExpandingRef = null;
	    private string _itemExpandingScript = null;
	    [Parameter]
	    public string ItemExpandingScript { 
	    
	        set 
	        {
	            this.OnRefChanged("ItemExpanding", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._itemExpandingRef = refName;
	                this.MarkPropDirty("ItemExpandingRef");	
	        }); 
	        }
	        get 
	        {
	            return this._itemExpandingScript;
	        }
	    }
	
	    partial void OnHandlingItemExpanding(IgbTreeItemComponentEventArgs args);
	    private EventCallback<IgbTreeItemComponentEventArgs>? _itemExpanding = null;
	    [Parameter]
	    public EventCallback<IgbTreeItemComponentEventArgs> ItemExpanding
	    {
	        get 
	        {
	            return this._itemExpanding != null ? this._itemExpanding.Value : EventCallback<IgbTreeItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _itemExpanding, ref eventCallbacksCache))
	                {
	                    _itemExpanding = value;
	                    this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemExpanding", value, (args) => {
	                        OnHandlingItemExpanding(args);
	                        
	                    });
	        this.OnRefChanged("ItemExpanding", null, "event:::ItemExpanding", true, false, (refName, oldValue, newValue) => {
	                        this._itemExpandingRef = refName;
	                        this.MarkPropDirty("ItemExpandingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _itemExpanding = null;
	                this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemExpanding", null);
	    this.OnRefChanged("ItemExpanding", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._itemExpandingRef = null;
	                    this.MarkPropDirty("ItemExpandingRef");	
	            });
	    }
	    }
	    }
	
	    private string _itemExpandedRef = null;
	    private string _itemExpandedScript = null;
	    [Parameter]
	    public string ItemExpandedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("ItemExpanded", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._itemExpandedRef = refName;
	                this.MarkPropDirty("ItemExpandedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._itemExpandedScript;
	        }
	    }
	
	    partial void OnHandlingItemExpanded(IgbTreeItemComponentEventArgs args);
	    private EventCallback<IgbTreeItemComponentEventArgs>? _itemExpanded = null;
	    [Parameter]
	    public EventCallback<IgbTreeItemComponentEventArgs> ItemExpanded
	    {
	        get 
	        {
	            return this._itemExpanded != null ? this._itemExpanded.Value : EventCallback<IgbTreeItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _itemExpanded, ref eventCallbacksCache))
	                {
	                    _itemExpanded = value;
	                    this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemExpanded", value, (args) => {
	                        OnHandlingItemExpanded(args);
	                        
	                    });
	        this.OnRefChanged("ItemExpanded", null, "event:::ItemExpanded", true, false, (refName, oldValue, newValue) => {
	                        this._itemExpandedRef = refName;
	                        this.MarkPropDirty("ItemExpandedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _itemExpanded = null;
	                this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemExpanded", null);
	    this.OnRefChanged("ItemExpanded", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._itemExpandedRef = null;
	                    this.MarkPropDirty("ItemExpandedRef");	
	            });
	    }
	    }
	    }
	
	    private string _itemCollapsingRef = null;
	    private string _itemCollapsingScript = null;
	    [Parameter]
	    public string ItemCollapsingScript { 
	    
	        set 
	        {
	            this.OnRefChanged("ItemCollapsing", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._itemCollapsingRef = refName;
	                this.MarkPropDirty("ItemCollapsingRef");	
	        }); 
	        }
	        get 
	        {
	            return this._itemCollapsingScript;
	        }
	    }
	
	    partial void OnHandlingItemCollapsing(IgbTreeItemComponentEventArgs args);
	    private EventCallback<IgbTreeItemComponentEventArgs>? _itemCollapsing = null;
	    [Parameter]
	    public EventCallback<IgbTreeItemComponentEventArgs> ItemCollapsing
	    {
	        get 
	        {
	            return this._itemCollapsing != null ? this._itemCollapsing.Value : EventCallback<IgbTreeItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _itemCollapsing, ref eventCallbacksCache))
	                {
	                    _itemCollapsing = value;
	                    this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemCollapsing", value, (args) => {
	                        OnHandlingItemCollapsing(args);
	                        
	                    });
	        this.OnRefChanged("ItemCollapsing", null, "event:::ItemCollapsing", true, false, (refName, oldValue, newValue) => {
	                        this._itemCollapsingRef = refName;
	                        this.MarkPropDirty("ItemCollapsingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _itemCollapsing = null;
	                this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemCollapsing", null);
	    this.OnRefChanged("ItemCollapsing", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._itemCollapsingRef = null;
	                    this.MarkPropDirty("ItemCollapsingRef");	
	            });
	    }
	    }
	    }
	
	    private string _itemCollapsedRef = null;
	    private string _itemCollapsedScript = null;
	    [Parameter]
	    public string ItemCollapsedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("ItemCollapsed", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._itemCollapsedRef = refName;
	                this.MarkPropDirty("ItemCollapsedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._itemCollapsedScript;
	        }
	    }
	
	    partial void OnHandlingItemCollapsed(IgbTreeItemComponentEventArgs args);
	    private EventCallback<IgbTreeItemComponentEventArgs>? _itemCollapsed = null;
	    [Parameter]
	    public EventCallback<IgbTreeItemComponentEventArgs> ItemCollapsed
	    {
	        get 
	        {
	            return this._itemCollapsed != null ? this._itemCollapsed.Value : EventCallback<IgbTreeItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _itemCollapsed, ref eventCallbacksCache))
	                {
	                    _itemCollapsed = value;
	                    this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemCollapsed", value, (args) => {
	                        OnHandlingItemCollapsed(args);
	                        
	                    });
	        this.OnRefChanged("ItemCollapsed", null, "event:::ItemCollapsed", true, false, (refName, oldValue, newValue) => {
	                        this._itemCollapsedRef = refName;
	                        this.MarkPropDirty("ItemCollapsedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _itemCollapsed = null;
	                this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ItemCollapsed", null);
	    this.OnRefChanged("ItemCollapsed", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._itemCollapsedRef = null;
	                    this.MarkPropDirty("ItemCollapsedRef");	
	            });
	    }
	    }
	    }
	
	    private string _activeItemRef = null;
	    private string _activeItemScript = null;
	    [Parameter]
	    public string ActiveItemScript { 
	    
	        set 
	        {
	            this.OnRefChanged("ActiveItem", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._activeItemRef = refName;
	                this.MarkPropDirty("ActiveItemRef");	
	        }); 
	        }
	        get 
	        {
	            return this._activeItemScript;
	        }
	    }
	
	    partial void OnHandlingActiveItem(IgbTreeItemComponentEventArgs args);
	    private EventCallback<IgbTreeItemComponentEventArgs>? _activeItem = null;
	    [Parameter]
	    public EventCallback<IgbTreeItemComponentEventArgs> ActiveItem
	    {
	        get 
	        {
	            return this._activeItem != null ? this._activeItem.Value : EventCallback<IgbTreeItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTreeItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _activeItem, ref eventCallbacksCache))
	                {
	                    _activeItem = value;
	                    this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ActiveItem", value, (args) => {
	                        OnHandlingActiveItem(args);
	                        
	                    });
	        this.OnRefChanged("ActiveItem", null, "event:::ActiveItem", true, false, (refName, oldValue, newValue) => {
	                        this._activeItemRef = refName;
	                        this.MarkPropDirty("ActiveItemRef");	
	                });
	                }
	    }
	        else 
	            {
	                _activeItem = null;
	                this.SetHandler<IgbTreeItemComponentEventArgs>(this.Name, "ActiveItem", null);
	    this.OnRefChanged("ActiveItem", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._activeItemRef = null;
	                    this.MarkPropDirty("ActiveItemRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbTree(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTree(ser);
	
	if (IsPropDirty("SingleBranchExpand")) { ser.AddBooleanProp("singleBranchExpand", this._singleBranchExpand); }
	if (IsPropDirty("ToggleNodeOnClick")) { ser.AddBooleanProp("toggleNodeOnClick", this._toggleNodeOnClick); }
	if (IsPropDirty("Selection")) { ser.AddEnumProp("selection", this._selection); }
	if (IsPropDirty("SelectionChangedRef")) { ser.AddStringProp("selectionChangedRef", this._selectionChangedRef); }
	if (IsPropDirty("ItemExpandingRef")) { ser.AddStringProp("itemExpandingRef", this._itemExpandingRef); }
	if (IsPropDirty("ItemExpandedRef")) { ser.AddStringProp("itemExpandedRef", this._itemExpandedRef); }
	if (IsPropDirty("ItemCollapsingRef")) { ser.AddStringProp("itemCollapsingRef", this._itemCollapsingRef); }
	if (IsPropDirty("ItemCollapsedRef")) { ser.AddStringProp("itemCollapsedRef", this._itemCollapsedRef); }
	if (IsPropDirty("ActiveItemRef")) { ser.AddStringProp("activeItemRef", this._activeItemRef); }
	
	    }
	
}
}
