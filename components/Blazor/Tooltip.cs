
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Provides a way to display supplementary information related to an element when a user interacts with it (e.g., hover, focus).
/// It offers features such as placement customization, delays, sticky mode, and animations.
/// </summary>
public partial class IgbTooltip: BaseRendererControl {
                                public override string Type { get { return "WebTooltip"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTooltipModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTooltipModule.Register(IgBlazor);
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
                            return "igc-tooltip";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbTooltip(): base() {
	        OnCreatedIgbTooltip();
	
	        
	    }
	
	    partial void OnCreatedIgbTooltip();
	    
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Whether the tooltip is showing.
	/// @default false
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
	private bool _disableArrow = false;
	
	partial void OnDisableArrowChanging(ref bool newValue);
	/// <summary>
	/// Whether to disable the rendering of the arrow indicator for the tooltip.
	/// </summary>
	[Parameter]
	[Obsolete("Use `with-arrow` to control the behavior of the tooltip arrow.")]
	public bool DisableArrow 
	{
	get { return this._disableArrow; }
	set { 
	                if (this._disableArrow != value || !IsPropDirty("DisableArrow")) {
	                        MarkPropDirty("DisableArrow");
	                } 
	                this._disableArrow = value;
	                 
	                }
	}
	private bool _withArrow = false;
	
	partial void OnWithArrowChanging(ref bool newValue);
	/// <summary>
	/// Whether to render an arrow indicator for the tooltip.
	/// @default false
	/// </summary>
	[Parameter]
	public bool WithArrow 
	{
	get { return this._withArrow; }
	set { 
	                if (this._withArrow != value || !IsPropDirty("WithArrow")) {
	                        MarkPropDirty("WithArrow");
	                } 
	                this._withArrow = value;
	                 
	                }
	}
	private double _offset = 0;
	
	partial void OnOffsetChanging(ref double newValue);
	/// <summary>
	/// The offset of the tooltip from the anchor in pixels.
	/// @default 6
	/// </summary>
	[Parameter]
	public double Offset 
	{
	get { return this._offset; }
	set { 
	                if (this._offset != value || !IsPropDirty("Offset")) {
	                        MarkPropDirty("Offset");
	                } 
	                this._offset = value;
	                 
	                }
	}
	private PopoverPlacement _placement = PopoverPlacement.Top;
	
	partial void OnPlacementChanging(ref PopoverPlacement newValue);
	/// <summary>
	/// Where to place the floating element relative to the parent anchor element.
	/// @default bottom
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
	private string _anchor;
	
	partial void OnAnchorChanging(ref string newValue);
	/// <summary>
	/// An element instance or an IDREF to use as the anchor for the tooltip.
	/// @remarks
	/// Trying to bind to an IDREF that does not exist in the current DOM root at will not work.
	/// In such scenarios, it is better to get a DOM reference and pass it to the tooltip instance.
	/// </summary>
	[Parameter]
	public string Anchor 
	{
	get { return this._anchor; }
	set { 
	                if (this._anchor != value || !IsPropDirty("Anchor")) {
	                        MarkPropDirty("Anchor");
	                } 
	                this._anchor = value;
	                 
	                }
	}
	private string _showTriggers;
	
	partial void OnShowTriggersChanging(ref string newValue);
	/// <summary>
	/// Which event triggers will show the tooltip.
	/// Expects a comma separate string of different event triggers.
	/// @default pointerenter
	/// </summary>
	[Parameter]
	public string ShowTriggers 
	{
	get { return this._showTriggers; }
	set { 
	                if (this._showTriggers != value || !IsPropDirty("ShowTriggers")) {
	                        MarkPropDirty("ShowTriggers");
	                } 
	                this._showTriggers = value;
	                 
	                }
	}
	private string _hideTriggers;
	
	partial void OnHideTriggersChanging(ref string newValue);
	/// <summary>
	/// Which event triggers will hide the tooltip.
	/// Expects a comma separate string of different event triggers.
	/// @default pointerleave, click
	/// </summary>
	[Parameter]
	public string HideTriggers 
	{
	get { return this._hideTriggers; }
	set { 
	                if (this._hideTriggers != value || !IsPropDirty("HideTriggers")) {
	                        MarkPropDirty("HideTriggers");
	                } 
	                this._hideTriggers = value;
	                 
	                }
	}
	private double _showDelay = 0;
	
	partial void OnShowDelayChanging(ref double newValue);
	/// <summary>
	/// Specifies the number of milliseconds that should pass before showing the tooltip.
	/// @default 200
	/// </summary>
	[Parameter]
	public double ShowDelay 
	{
	get { return this._showDelay; }
	set { 
	                if (this._showDelay != value || !IsPropDirty("ShowDelay")) {
	                        MarkPropDirty("ShowDelay");
	                } 
	                this._showDelay = value;
	                 
	                }
	}
	private double _hideDelay = 0;
	
	partial void OnHideDelayChanging(ref double newValue);
	/// <summary>
	/// Specifies the number of milliseconds that should pass before hiding the tooltip.
	/// @default 300
	/// </summary>
	[Parameter]
	public double HideDelay 
	{
	get { return this._hideDelay; }
	set { 
	                if (this._hideDelay != value || !IsPropDirty("HideDelay")) {
	                        MarkPropDirty("HideDelay");
	                } 
	                this._hideDelay = value;
	                 
	                }
	}
	private string _message;
	
	partial void OnMessageChanging(ref string newValue);
	/// <summary>
	/// Specifies a plain text as tooltip content.
	/// </summary>
	[Parameter]
	public string Message 
	{
	get { return this._message; }
	set { 
	                if (this._message != value || !IsPropDirty("Message")) {
	                        MarkPropDirty("Message");
	                } 
	                this._message = value;
	                 
	                }
	}
	private bool _sticky = false;
	
	partial void OnStickyChanging(ref bool newValue);
	/// <summary>
	/// Specifies if the tooltip remains visible until the user closes it via the close button or Esc key.
	/// @default false
	/// </summary>
	[Parameter]
	public bool Sticky 
	{
	get { return this._sticky; }
	set { 
	                if (this._sticky != value || !IsPropDirty("Sticky")) {
	                        MarkPropDirty("Sticky");
	                } 
	                this._sticky = value;
	                 
	                }
	}
	
	    partial void FindByNameTooltip(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTooltip(name, ref item);
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
	/// <summary>
	/// Shows the tooltip if not already showing.
	/// If a target is provided, sets it as a transient anchor.
	/// </summary>
	public async Task<bool> ShowAsync(String target = null) 
	                    {
		var iv = await InvokeMethod("show", new object[] { StringToString(target) }, new string[] { "String" });
		return ReturnToBoolean(iv);
	}
	                    public bool Show(String target = null) 
	                    {
		var iv = InvokeMethodSync("show", new object[] { StringToString(target) }, new string[] { "String" });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Hides the tooltip if not already hidden.
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
	/// Toggles the tooltip between shown/hidden state
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
	
	    partial void SerializeCoreIgbTooltip(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTooltip(ser);
	
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	if (IsPropDirty("DisableArrow")) { ser.AddBooleanProp("disableArrow", this._disableArrow); }
	if (IsPropDirty("WithArrow")) { ser.AddBooleanProp("withArrow", this._withArrow); }
	if (IsPropDirty("Offset")) { ser.AddNumberProp("offset", this._offset); }
	if (IsPropDirty("Placement")) { ser.AddEnumProp("placement", this._placement); }
	if (IsPropDirty("Anchor")) { ser.AddStringProp("anchor", this._anchor); }
	if (IsPropDirty("ShowTriggers")) { ser.AddStringProp("showTriggers", this._showTriggers); }
	if (IsPropDirty("HideTriggers")) { ser.AddStringProp("hideTriggers", this._hideTriggers); }
	if (IsPropDirty("ShowDelay")) { ser.AddNumberProp("showDelay", this._showDelay); }
	if (IsPropDirty("HideDelay")) { ser.AddNumberProp("hideDelay", this._hideDelay); }
	if (IsPropDirty("Message")) { ser.AddStringProp("message", this._message); }
	if (IsPropDirty("Sticky")) { ser.AddBooleanProp("sticky", this._sticky); }
	if (IsPropDirty("OpeningRef")) { ser.AddStringProp("openingRef", this._openingRef); }
	if (IsPropDirty("OpenedRef")) { ser.AddStringProp("openedRef", this._openedRef); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	
	    }
	
}
}
