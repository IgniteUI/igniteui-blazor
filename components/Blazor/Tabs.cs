
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbTabs: BaseRendererControl {
                                public override string Type { get { return "WebTabs"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTabsModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTabsModule.Register(IgBlazor);
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
                            return "igc-tabs";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }

                            protected override string ParentTypeName
                            {
                                get
                                {
                                    return "TabsParent";
                                }
                            }

                            private CollectionAdapter<IgbTab, IgbTab> _tabsCollectionAdapter;
                            private IgbTabs_TabCollection _allTabsCollection;
                            private IgbTabs_TabCollection _contentTabsCollection = null;

                            public IgbTabs_TabCollection ContentTabsCollection
                            {

                                get 
                                {
                                    if (this._contentTabsCollection == null) {
                                        this._contentTabsCollection  = new IgbTabs_TabCollection(this, "TabsCollection");
                                    }
                                    return this._contentTabsCollection;
                                }
                            }
                            partial void GetSerializableTabsCollection(ref IgbTabs_TabCollection value)
                            {
                                value = ActualTabsCollection;
                            }
                            private IgbTabs_TabCollection _actualTabsCollection = null;

                            public IgbTabs_TabCollection ActualTabsCollection
                            {

                                get 
                                {
                                    if (this._actualTabsCollection == null) {
                                        this._actualTabsCollection  = new IgbTabs_TabCollection(this, "TabsCollection");
                                    }
                                    return this._actualTabsCollection;
                                }
                            }
	
	    public IgbTabs(): base() {
	        OnCreatedIgbTabs();
	
	        
	            _allTabsCollection = new IgbTabs_TabCollection(this, "TabsCollection");
	            _tabsCollectionAdapter = new CollectionAdapter<IgbTab, IgbTab>(
	                ContentTabsCollection,
	                ActualTabsCollection,
	                _allTabsCollection,
	                (s) => s,
	                (s) => {
	                },
	                (s) => { }
	            );
	            _tabsCollectionAdapter.SubcribeToManual(TabsCollection);
	
	    }
	
	    partial void OnCreatedIgbTabs();
	    
	private IgbTabs_TabCollection _tabsCollection = null;
	
	partial void GetSerializableTabsCollection(ref IgbTabs_TabCollection value);
	
	partial void OnTabsCollectionChanging(ref IgbTabs_TabCollection newValue);
	
	public IgbTabs_TabCollection TabsCollection 
	{
	
	    get 
	    {
	                if (this._tabsCollection == null) {
	                    this._tabsCollection  = new IgbTabs_TabCollection(this, "TabsCollection");
	                }
	                return this._tabsCollection;
	    }
	protected set { 
	                if (this._tabsCollection != value || !IsPropDirty("TabsCollection")) {
	                        MarkPropDirty("TabsCollection");
	                } 
	                this._tabsCollection = value;
	                 
	                }
	}
	private TabsAlignment _alignment = TabsAlignment.Start;
	
	partial void OnAlignmentChanging(ref TabsAlignment newValue);
	/// <summary>
	/// Sets the alignment for the tab headers
	/// </summary>
	[Parameter]
	public TabsAlignment Alignment 
	{
	get { return this._alignment; }
	set { 
	                if (this._alignment != value || !IsPropDirty("Alignment")) {
	                        MarkPropDirty("Alignment");
	                } 
	                this._alignment = value;
	                 
	                }
	}
	private TabsActivation _activation = TabsActivation.Auto;
	
	partial void OnActivationChanging(ref TabsActivation newValue);
	/// <summary>
	/// Determines the tab activation. When set to auto,
	/// the tab is instantly selected while navigating with the Left/Right Arrows, Home or End keys
	/// and the corresponding panel is displayed.
	/// When set to manual, the tab is only focused. The selection happens after pressing Space or Enter.
	/// </summary>
	[Parameter]
	public TabsActivation Activation 
	{
	get { return this._activation; }
	set { 
	                if (this._activation != value || !IsPropDirty("Activation")) {
	                        MarkPropDirty("Activation");
	                } 
	                this._activation = value;
	                 
	                }
	}
	public async Task<string> GetSelectedAsync()
	                    {
		var iv = await InvokeMethod("p:Selected", new object[] { }, new string[] { });
		return ReturnToString(iv);
	}
	                    public string GetSelected()
	                    {
		var iv = InvokeMethodSync("p:Selected", new object[] { }, new string[] { });
		return ReturnToString(iv);
	}
	
	    partial void FindByNameTabs(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTabs(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	if (_actualTabsCollection != null && _actualTabsCollection.HasName(name)) { return _actualTabsCollection.FindByName(name); }
	
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
	/// <summary>
	/// Selects the specified tab and displays the corresponding panel.
	/// </summary>
	public async  Task SelectAsync(String id) 
	                    {
		await InvokeMethod("select", new object[] { StringToString(id) }, new string[] { "String" });
	}
	                    public  void Select(String id) 
	                    {
		InvokeMethodSync("select", new object[] { StringToString(id) }, new string[] { "String" });
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
	
	    partial void OnHandlingChange(IgbTabComponentEventArgs args);
	    private EventCallback<IgbTabComponentEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbTabComponentEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbTabComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTabComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbTabComponentEventArgs>(this.Name, "Change", value, (args) => {
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
	                this.SetHandler<IgbTabComponentEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbTabs(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTabs(ser);
	
	if (IsPropDirty("TabsCollection")) { var coll = this._tabsCollection; GetSerializableTabsCollection(ref coll); ser.AddCollectionProp("tabsCollection", coll); }
	if (IsPropDirty("Alignment")) { ser.AddEnumProp("alignment", this._alignment); }
	if (IsPropDirty("Activation")) { ser.AddEnumProp("activation", this._activation); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
