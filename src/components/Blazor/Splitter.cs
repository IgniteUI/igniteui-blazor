
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The `igc-splitter` component provides a resizable split-pane layout that divides the view
/// into two panels — *start* and *end* — separated by a draggable bar.
/// Panels can be resized by dragging the bar, using keyboard shortcuts, or collapsed/expanded
/// using the built-in collapse buttons or the programmatic `toggle()` API.
/// Nested splitters are supported for more complex layouts.
/// </summary>
public partial class IgbSplitter: BaseRendererControl {
                                public override string Type { get { return "WebSplitter"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSplitterModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSplitterModule.Register(IgBlazor);
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
                            return "igc-splitter";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbSplitter(): base() {
	        OnCreatedIgbSplitter();
	
	        
	    }
	
	    partial void OnCreatedIgbSplitter();
	    
	private SplitterOrientation _orientation = SplitterOrientation.Horizontal;
	
	partial void OnOrientationChanging(ref SplitterOrientation newValue);
	/// <summary>
	/// The orientation of the splitter, which determines the direction of resizing and collapsing.
	/// </summary>
	[Parameter]
	public SplitterOrientation Orientation 
	{
	get { return this._orientation; }
	set { 
	                if (this._orientation != value || !IsPropDirty("Orientation")) {
	                        MarkPropDirty("Orientation");
	                } 
	                this._orientation = value;
	                 
	                }
	}
	private bool _disableCollapse = false;
	
	partial void OnDisableCollapseChanging(ref bool newValue);
	/// <summary>
	/// When true, prevents the user from collapsing either pane.
	/// This also hides the expand/collapse buttons on the splitter bar.
	/// </summary>
	[Parameter]
	public bool DisableCollapse 
	{
	get { return this._disableCollapse; }
	set { 
	                if (this._disableCollapse != value || !IsPropDirty("DisableCollapse")) {
	                        MarkPropDirty("DisableCollapse");
	                } 
	                this._disableCollapse = value;
	                 
	                }
	}
	private bool _disableResize = false;
	
	partial void OnDisableResizeChanging(ref bool newValue);
	/// <summary>
	/// When true, prevents the user from resizing the panes by dragging the splitter bar or using keyboard shortcuts.
	/// This also hides the drag handle on the splitter bar.
	/// </summary>
	[Parameter]
	public bool DisableResize 
	{
	get { return this._disableResize; }
	set { 
	                if (this._disableResize != value || !IsPropDirty("DisableResize")) {
	                        MarkPropDirty("DisableResize");
	                } 
	                this._disableResize = value;
	                 
	                }
	}
	private bool _hideCollapseButtons = false;
	
	partial void OnHideCollapseButtonsChanging(ref bool newValue);
	/// <summary>
	/// When true, hides the expand/collapse buttons on the splitter bar.
	/// Note that the buttons will also be hidden if `disable-collapse` is true or
	/// if a pane is currently collapsed.
	/// </summary>
	[Parameter]
	public bool HideCollapseButtons 
	{
	get { return this._hideCollapseButtons; }
	set { 
	                if (this._hideCollapseButtons != value || !IsPropDirty("HideCollapseButtons")) {
	                        MarkPropDirty("HideCollapseButtons");
	                } 
	                this._hideCollapseButtons = value;
	                 
	                }
	}
	private bool _hideDragHandle = false;
	
	partial void OnHideDragHandleChanging(ref bool newValue);
	/// <summary>
	/// When true, hides the drag handle on the splitter bar.
	/// Note that the drag handle will also be hidden if `disable-resize` is true.
	/// </summary>
	[Parameter]
	public bool HideDragHandle 
	{
	get { return this._hideDragHandle; }
	set { 
	                if (this._hideDragHandle != value || !IsPropDirty("HideDragHandle")) {
	                        MarkPropDirty("HideDragHandle");
	                } 
	                this._hideDragHandle = value;
	                 
	                }
	}
	private string? _startMinSize;
	
	partial void OnStartMinSizeChanging(ref string? newValue);
	/// <summary>
	/// The minimum size of the start pane.
	/// </summary>
	[Parameter]
	public string? StartMinSize 
	{
	get { return this._startMinSize; }
	set { 
	                if (this._startMinSize != value || !IsPropDirty("StartMinSize")) {
	                        MarkPropDirty("StartMinSize");
	                } 
	                this._startMinSize = value;
	                 
	                }
	}
	private string? _endMinSize;
	
	partial void OnEndMinSizeChanging(ref string? newValue);
	/// <summary>
	/// The minimum size of the end pane.
	/// </summary>
	[Parameter]
	public string? EndMinSize 
	{
	get { return this._endMinSize; }
	set { 
	                if (this._endMinSize != value || !IsPropDirty("EndMinSize")) {
	                        MarkPropDirty("EndMinSize");
	                } 
	                this._endMinSize = value;
	                 
	                }
	}
	private string? _startMaxSize;
	
	partial void OnStartMaxSizeChanging(ref string? newValue);
	/// <summary>
	/// The maximum size of the start pane.
	/// </summary>
	[Parameter]
	public string? StartMaxSize 
	{
	get { return this._startMaxSize; }
	set { 
	                if (this._startMaxSize != value || !IsPropDirty("StartMaxSize")) {
	                        MarkPropDirty("StartMaxSize");
	                } 
	                this._startMaxSize = value;
	                 
	                }
	}
	private string? _endMaxSize;
	
	partial void OnEndMaxSizeChanging(ref string? newValue);
	/// <summary>
	/// The maximum size of the end pane.
	/// </summary>
	[Parameter]
	public string? EndMaxSize 
	{
	get { return this._endMaxSize; }
	set { 
	                if (this._endMaxSize != value || !IsPropDirty("EndMaxSize")) {
	                        MarkPropDirty("EndMaxSize");
	                } 
	                this._endMaxSize = value;
	                 
	                }
	}
	private string? _startSize;
	
	partial void OnStartSizeChanging(ref string? newValue);
	/// <summary>
	/// The size of the start pane.
	/// </summary>
	[Parameter]
	public string? StartSize 
	{
	get { return this._startSize; }
	set { 
	                if (this._startSize != value || !IsPropDirty("StartSize")) {
	                        MarkPropDirty("StartSize");
	                } 
	                this._startSize = value;
	                 
	                }
	}
	private string? _endSize;
	
	partial void OnEndSizeChanging(ref string? newValue);
	/// <summary>
	/// The size of the end pane.
	/// </summary>
	[Parameter]
	public string? EndSize 
	{
	get { return this._endSize; }
	set { 
	                if (this._endSize != value || !IsPropDirty("EndSize")) {
	                        MarkPropDirty("EndSize");
	                } 
	                this._endSize = value;
	                 
	                }
	}
	
	    partial void FindByNameSplitter(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSplitter(name, ref item);
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
	/// Toggles the collapsed state of the specified pane.
	/// </summary>
	public async  Task ToggleAsync(PanePosition position) 
	                    {
		await InvokeMethod("toggle", new object[] { ObjectToParam(position, typeof(PanePosition)) }, new string[] { "Json" });
	}
	                    public  void Toggle(PanePosition position) 
	                    {
		InvokeMethodSync("toggle", new object[] { ObjectToParam(position, typeof(PanePosition)) }, new string[] { "Json" });
	}
	
	    private string _resizeStartRef = null;
	    private string _resizeStartScript = null;
	    [Parameter]
	    public string ResizeStartScript { 
	    
	        set 
	        {
	            if (value != this._resizeStartScript)
	            {
	                this._resizeStartScript = value;
	                this.OnRefChanged("ResizeStart", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._resizeStartRef = refName;
	                    this.MarkPropDirty("ResizeStartRef");	
	                });
	            }
	        }
	        get 
	        {
	            return this._resizeStartScript;
	        }
	    }
	
	    partial void OnHandlingResizeStart(IgbSplitterResizeEventArgs args);
	    private EventCallback<IgbSplitterResizeEventArgs>? _resizeStart = null;
	    [Parameter]
	    public EventCallback<IgbSplitterResizeEventArgs> ResizeStart
	    {
	        get 
	        {
	            return this._resizeStart != null ? this._resizeStart.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbSplitterResizeEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _resizeStart, ref eventCallbacksCache))
	                {
	                    _resizeStart = value;
	                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeStart", value, (args) => {
	                        OnHandlingResizeStart(args);
	                        
	                    });
	        this.OnRefChanged("ResizeStart", null, "event:::ResizeStart", true, false, (refName, oldValue, newValue) => {
	                        this._resizeStartRef = refName;
	                        this.MarkPropDirty("ResizeStartRef");	
	                });
	                }
	    }
	        else 
	            {
	                _resizeStart = null;
	                this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeStart", null);
	    this.OnRefChanged("ResizeStart", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._resizeStartRef = null;
	                    this.MarkPropDirty("ResizeStartRef");	
	            });
	    }
	    }
	    }
	
	    private string _resizingRef = null;
	    private string _resizingScript = null;
	    [Parameter]
	    public string ResizingScript { 
	    
	        set 
	        {
	            if (value != this._resizingScript)
	            {
	                this._resizingScript = value;
	                this.OnRefChanged("Resizing", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._resizingRef = refName;
	                    this.MarkPropDirty("ResizingRef");	
	                });
	            }
	        }
	        get 
	        {
	            return this._resizingScript;
	        }
	    }
	
	    partial void OnHandlingResizing(IgbSplitterResizeEventArgs args);
	    private EventCallback<IgbSplitterResizeEventArgs>? _resizing = null;
	    [Parameter]
	    public EventCallback<IgbSplitterResizeEventArgs> Resizing
	    {
	        get 
	        {
	            return this._resizing != null ? this._resizing.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbSplitterResizeEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _resizing, ref eventCallbacksCache))
	                {
	                    _resizing = value;
	                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "Resizing", value, (args) => {
	                        OnHandlingResizing(args);
	                        
	                    });
	        this.OnRefChanged("Resizing", null, "event:::Resizing", true, false, (refName, oldValue, newValue) => {
	                        this._resizingRef = refName;
	                        this.MarkPropDirty("ResizingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _resizing = null;
	                this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "Resizing", null);
	    this.OnRefChanged("Resizing", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._resizingRef = null;
	                    this.MarkPropDirty("ResizingRef");	
	            });
	    }
	    }
	    }
	
	    private string _resizeEndRef = null;
	    private string _resizeEndScript = null;
	    [Parameter]
	    public string ResizeEndScript { 
	    
	        set 
	        {
	            if (value != this._resizeEndScript)
	            {
	                this._resizeEndScript = value;
	                this.OnRefChanged("ResizeEnd", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._resizeEndRef = refName;
	                    this.MarkPropDirty("ResizeEndRef");	
	                });
	            }
	        }
	        get 
	        {
	            return this._resizeEndScript;
	        }
	    }
	
	    partial void OnHandlingResizeEnd(IgbSplitterResizeEventArgs args);
	    private EventCallback<IgbSplitterResizeEventArgs>? _resizeEnd = null;
	    [Parameter]
	    public EventCallback<IgbSplitterResizeEventArgs> ResizeEnd
	    {
	        get 
	        {
	            return this._resizeEnd != null ? this._resizeEnd.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbSplitterResizeEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _resizeEnd, ref eventCallbacksCache))
	                {
	                    _resizeEnd = value;
	                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeEnd", value, (args) => {
	                        OnHandlingResizeEnd(args);
	                        
	                    });
	        this.OnRefChanged("ResizeEnd", null, "event:::ResizeEnd", true, false, (refName, oldValue, newValue) => {
	                        this._resizeEndRef = refName;
	                        this.MarkPropDirty("ResizeEndRef");	
	                });
	                }
	    }
	        else 
	            {
	                _resizeEnd = null;
	                this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeEnd", null);
	    this.OnRefChanged("ResizeEnd", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._resizeEndRef = null;
	                    this.MarkPropDirty("ResizeEndRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbSplitter(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSplitter(ser);
	
	if (IsPropDirty("Orientation")) { ser.AddEnumProp("orientation", this._orientation); }
	if (IsPropDirty("DisableCollapse")) { ser.AddBooleanProp("disableCollapse", this._disableCollapse); }
	if (IsPropDirty("DisableResize")) { ser.AddBooleanProp("disableResize", this._disableResize); }
	if (IsPropDirty("HideCollapseButtons")) { ser.AddBooleanProp("hideCollapseButtons", this._hideCollapseButtons); }
	if (IsPropDirty("HideDragHandle")) { ser.AddBooleanProp("hideDragHandle", this._hideDragHandle); }
	if (IsPropDirty("StartMinSize")) { ser.AddStringProp("startMinSize", this._startMinSize); }
	if (IsPropDirty("EndMinSize")) { ser.AddStringProp("endMinSize", this._endMinSize); }
	if (IsPropDirty("StartMaxSize")) { ser.AddStringProp("startMaxSize", this._startMaxSize); }
	if (IsPropDirty("EndMaxSize")) { ser.AddStringProp("endMaxSize", this._endMaxSize); }
	if (IsPropDirty("StartSize")) { ser.AddStringProp("startSize", this._startSize); }
	if (IsPropDirty("EndSize")) { ser.AddStringProp("endSize", this._endSize); }
	if (IsPropDirty("ResizeStartRef")) { ser.AddStringProp("resizeStartRef", this._resizeStartRef); }
	if (IsPropDirty("ResizingRef")) { ser.AddStringProp("resizingRef", this._resizingRef); }
	if (IsPropDirty("ResizeEndRef")) { ser.AddStringProp("resizeEndRef", this._resizeEndRef); }
	
	    }
	
}
}
