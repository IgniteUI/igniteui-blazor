
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The Expansion Panel Component provides a way to display information in a toggleable way -
/// compact summary view containing title and description and expanded detail view containing
/// additional content to the summary header.
/// </summary>
public partial class IgbExpansionPanel: BaseRendererControl {
                                public override string Type { get { return "WebExpansionPanel"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbExpansionPanelModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbExpansionPanelModule.Register(IgBlazor);
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
                            return "igc-expansion-panel";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbExpansionPanel(): base() {
	        OnCreatedIgbExpansionPanel();
	
	        
	    }
	
	    partial void OnCreatedIgbExpansionPanel();
	    
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Indicates whether the contents of the control should be visible.
	/// </summary>
	[Parameter]
	public bool Open 
	{
	get { return this._open; }
	set { 
	                if (this._open != value || !IsPropDirty("Open")) {
	                        MarkPropDirty("Open");
	                } 
	                this._open = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Get/Set whether the expansion panel is disabled. Disabled panels are ignored for user interactions.
	/// </summary>
	[Parameter]
	public bool Disabled 
	{
	get { return this._disabled; }
	set { 
	                if (this._disabled != value || !IsPropDirty("Disabled")) {
	                        MarkPropDirty("Disabled");
	                } 
	                this._disabled = value;
	                 
	                }
	}
	private ExpansionPanelIndicatorPosition _indicatorPosition = ExpansionPanelIndicatorPosition.Start;
	
	partial void OnIndicatorPositionChanging(ref ExpansionPanelIndicatorPosition newValue);
	/// <summary>
	/// The indicator position of the expansion panel.
	/// </summary>
	[Parameter]
	public ExpansionPanelIndicatorPosition IndicatorPosition 
	{
	get { return this._indicatorPosition; }
	set { 
	                if (this._indicatorPosition != value || !IsPropDirty("IndicatorPosition")) {
	                        MarkPropDirty("IndicatorPosition");
	                } 
	                this._indicatorPosition = value;
	                 
	                }
	}
	
	    partial void FindByNameExpansionPanel(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameExpansionPanel(name, ref item);
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
	/// Toggles the panel open/close state.
	/// </summary>
	public async Task<bool> ToggleAsync() 
	                    {
		var iv = await InvokeMethod("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Toggle() 
	                    {
		var iv = InvokeMethodSync("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Hides the panel content.
	/// </summary>
	public async Task<bool> HideAsync() 
	                    {
		var iv = await InvokeMethod("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Hide() 
	                    {
		var iv = InvokeMethodSync("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Shows the panel content.
	/// </summary>
	public async Task<bool> ShowAsync() 
	                    {
		var iv = await InvokeMethod("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Show() 
	                    {
		var iv = InvokeMethodSync("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
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
	
	    partial void OnHandlingOpening(IgbExpansionPanelComponentEventArgs args);
	    private EventCallback<IgbExpansionPanelComponentEventArgs>? _opening = null;
	    [Parameter]
	    public EventCallback<IgbExpansionPanelComponentEventArgs> Opening
	    {
	        get 
	        {
	            return this._opening != null ? this._opening.Value : EventCallback<IgbExpansionPanelComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbExpansionPanelComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opening, ref eventCallbacksCache))
	                {
	                    _opening = value;
	                    this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Opening", value, (args) => {
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
	                this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Opening", null);
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
	
	    partial void OnHandlingOpened(IgbExpansionPanelComponentEventArgs args);
	    private EventCallback<IgbExpansionPanelComponentEventArgs>? _opened = null;
	    [Parameter]
	    public EventCallback<IgbExpansionPanelComponentEventArgs> Opened
	    {
	        get 
	        {
	            return this._opened != null ? this._opened.Value : EventCallback<IgbExpansionPanelComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbExpansionPanelComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opened, ref eventCallbacksCache))
	                {
	                    _opened = value;
	                    this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Opened", value, (args) => {
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
	                this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Opened", null);
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
	
	    partial void OnHandlingClosing(IgbExpansionPanelComponentEventArgs args);
	    private EventCallback<IgbExpansionPanelComponentEventArgs>? _closing = null;
	    [Parameter]
	    public EventCallback<IgbExpansionPanelComponentEventArgs> Closing
	    {
	        get 
	        {
	            return this._closing != null ? this._closing.Value : EventCallback<IgbExpansionPanelComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbExpansionPanelComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closing, ref eventCallbacksCache))
	                {
	                    _closing = value;
	                    this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Closing", value, (args) => {
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
	                this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Closing", null);
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
	
	    partial void OnHandlingClosed(IgbExpansionPanelComponentEventArgs args);
	    private EventCallback<IgbExpansionPanelComponentEventArgs>? _closed = null;
	    [Parameter]
	    public EventCallback<IgbExpansionPanelComponentEventArgs> Closed
	    {
	        get 
	        {
	            return this._closed != null ? this._closed.Value : EventCallback<IgbExpansionPanelComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbExpansionPanelComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closed, ref eventCallbacksCache))
	                {
	                    _closed = value;
	                    this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Closed", value, (args) => {
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
	                this.SetHandler<IgbExpansionPanelComponentEventArgs>(this.Name, "Closed", null);
	    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closedRef = null;
	                    this.MarkPropDirty("ClosedRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbExpansionPanel(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbExpansionPanel(ser);
	
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("IndicatorPosition")) { ser.AddEnumProp("indicatorPosition", this._indicatorPosition); }
	if (IsPropDirty("OpeningRef")) { ser.AddStringProp("openingRef", this._openingRef); }
	if (IsPropDirty("OpenedRef")) { ser.AddStringProp("openedRef", this._openedRef); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	
	    }
	
}
}
