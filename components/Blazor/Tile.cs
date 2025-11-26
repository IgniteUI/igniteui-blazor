
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The tile component is used within the `igc-tile-manager` as a container
/// for displaying various types of information.
/// </summary>
public partial class IgbTile: BaseRendererControl {
                                public override string Type { get { return "WebTile"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbTileModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbTileModule.Register(IgBlazor);
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
                            return "igc-tile";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbTile(): base() {
	        OnCreatedIgbTile();
	
	        
	    }
	
	    partial void OnCreatedIgbTile();
	    
	private double _colSpan = 0;
	
	partial void OnColSpanChanging(ref double newValue);
	/// <summary>
	/// The number of columns the tile will span.
	/// @remarks
	/// When setting a value that is less than 1, it will be
	/// coerced to 1.
	/// @default 1
	/// </summary>
	[Parameter]
	public double ColSpan 
	{
	get { return this._colSpan; }
	set { 
	                if (this._colSpan != value || !IsPropDirty("ColSpan")) {
	                        MarkPropDirty("ColSpan");
	                } 
	                this._colSpan = value;
	                 
	                }
	}
	private double _rowSpan = 0;
	
	partial void OnRowSpanChanging(ref double newValue);
	/// <summary>
	/// The number of rows the tile will span.
	/// @remarks
	/// When setting a value that is less than 1, it will be
	/// coerced to 1.
	/// @default 1
	/// </summary>
	[Parameter]
	public double RowSpan 
	{
	get { return this._rowSpan; }
	set { 
	                if (this._rowSpan != value || !IsPropDirty("RowSpan")) {
	                        MarkPropDirty("RowSpan");
	                } 
	                this._rowSpan = value;
	                 
	                }
	}
	private double? _colStart = 0;
	
	partial void OnColStartChanging(ref double? newValue);
	/// <summary>
	/// The starting column for the tile.
	/// </summary>
	[Parameter]
	public double? ColStart 
	{
	get { return this._colStart; }
	set { 
	                if (this._colStart != value || !IsPropDirty("ColStart")) {
	                        MarkPropDirty("ColStart");
	                } 
	                this._colStart = value;
	                 
	                }
	}
	private double? _rowStart = 0;
	
	partial void OnRowStartChanging(ref double? newValue);
	/// <summary>
	/// The starting row for the tile.
	/// </summary>
	[Parameter]
	public double? RowStart 
	{
	get { return this._rowStart; }
	set { 
	                if (this._rowStart != value || !IsPropDirty("RowStart")) {
	                        MarkPropDirty("RowStart");
	                } 
	                this._rowStart = value;
	                 
	                }
	}
	public async Task<bool> GetFullscreenAsync()
	                    {
		var iv = await InvokeMethod("p:Fullscreen", new object[] { }, new string[] { });
		return ReturnToBoolean(iv);
	}
	                    public bool GetFullscreen()
	                    {
		var iv = InvokeMethodSync("p:Fullscreen", new object[] { }, new string[] { });
		return ReturnToBoolean(iv);
	}
	private bool _maximized = false;
	
	partial void OnMaximizedChanging(ref bool newValue);
	/// <summary>
	/// Indicates whether the tile occupies all available space within the layout.
	/// </summary>
	[Parameter]
	public bool Maximized 
	{
	get { return this._maximized; }
	set { 
	                if (this._maximized != value || !IsPropDirty("Maximized")) {
	                        MarkPropDirty("Maximized");
	                } 
	                this._maximized = value;
	                 
	                }
	}
	private bool _disableResize = false;
	
	partial void OnDisableResizeChanging(ref bool newValue);
	/// <summary>
	/// Indicates whether to disable tile resize behavior regardless
	/// ot its tile manager parent settings.
	/// @default false
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
	private bool _disableFullscreen = false;
	
	partial void OnDisableFullscreenChanging(ref bool newValue);
	/// <summary>
	/// Whether to disable the rendering of the tile `fullscreen-action` slot and its
	/// default fullscreen action button.
	/// @default false
	/// </summary>
	[Parameter]
	public bool DisableFullscreen 
	{
	get { return this._disableFullscreen; }
	set { 
	                if (this._disableFullscreen != value || !IsPropDirty("DisableFullscreen")) {
	                        MarkPropDirty("DisableFullscreen");
	                } 
	                this._disableFullscreen = value;
	                 
	                }
	}
	private bool _disableMaximize = false;
	
	partial void OnDisableMaximizeChanging(ref bool newValue);
	/// <summary>
	/// Whether to disable the rendering of the tile `maximize-action` slot and its
	/// default maximize action button.
	/// @default false
	/// </summary>
	[Parameter]
	public bool DisableMaximize 
	{
	get { return this._disableMaximize; }
	set { 
	                if (this._disableMaximize != value || !IsPropDirty("DisableMaximize")) {
	                        MarkPropDirty("DisableMaximize");
	                } 
	                this._disableMaximize = value;
	                 
	                }
	}
	private double _position = 0;
	
	partial void OnPositionChanging(ref double newValue);
	/// <summary>
	/// Gets/sets the tile's visual position in the layout.
	/// Corresponds to the CSS `order` property.
	/// </summary>
	[Parameter]
	public double Position 
	{
	get { return this._position; }
	set { 
	                if (this._position != value || !IsPropDirty("Position")) {
	                        MarkPropDirty("Position");
	                } 
	                this._position = value;
	                 
	                }
	}
	
	    partial void FindByNameTile(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameTile(name, ref item);
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
	
	    private string _tileFullscreenRef = null;
	    private string _tileFullscreenScript = null;
	    [Parameter]
	    public string TileFullscreenScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileFullscreen", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileFullscreenRef = refName;
	                this.MarkPropDirty("TileFullscreenRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileFullscreenScript;
	        }
	    }
	
	    partial void OnHandlingTileFullscreen(IgbTileChangeStateEventArgs args);
	    private EventCallback<IgbTileChangeStateEventArgs>? _tileFullscreen = null;
	    [Parameter]
	    public EventCallback<IgbTileChangeStateEventArgs> TileFullscreen
	    {
	        get 
	        {
	            return this._tileFullscreen != null ? this._tileFullscreen.Value : EventCallback<IgbTileChangeStateEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileChangeStateEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileFullscreen, ref eventCallbacksCache))
	                {
	                    _tileFullscreen = value;
	                    this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileFullscreen", value, (args) => {
	                        OnHandlingTileFullscreen(args);
	                        
	                    });
	        this.OnRefChanged("TileFullscreen", null, "event:::TileFullscreen", true, false, (refName, oldValue, newValue) => {
	                        this._tileFullscreenRef = refName;
	                        this.MarkPropDirty("TileFullscreenRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileFullscreen = null;
	                this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileFullscreen", null);
	    this.OnRefChanged("TileFullscreen", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileFullscreenRef = null;
	                    this.MarkPropDirty("TileFullscreenRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileMaximizeRef = null;
	    private string _tileMaximizeScript = null;
	    [Parameter]
	    public string TileMaximizeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileMaximize", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileMaximizeRef = refName;
	                this.MarkPropDirty("TileMaximizeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileMaximizeScript;
	        }
	    }
	
	    partial void OnHandlingTileMaximize(IgbTileChangeStateEventArgs args);
	    private EventCallback<IgbTileChangeStateEventArgs>? _tileMaximize = null;
	    [Parameter]
	    public EventCallback<IgbTileChangeStateEventArgs> TileMaximize
	    {
	        get 
	        {
	            return this._tileMaximize != null ? this._tileMaximize.Value : EventCallback<IgbTileChangeStateEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileChangeStateEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileMaximize, ref eventCallbacksCache))
	                {
	                    _tileMaximize = value;
	                    this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileMaximize", value, (args) => {
	                        OnHandlingTileMaximize(args);
	                        
	                    });
	        this.OnRefChanged("TileMaximize", null, "event:::TileMaximize", true, false, (refName, oldValue, newValue) => {
	                        this._tileMaximizeRef = refName;
	                        this.MarkPropDirty("TileMaximizeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileMaximize = null;
	                this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileMaximize", null);
	    this.OnRefChanged("TileMaximize", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileMaximizeRef = null;
	                    this.MarkPropDirty("TileMaximizeRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileDragStartRef = null;
	    private string _tileDragStartScript = null;
	    [Parameter]
	    public string TileDragStartScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileDragStart", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileDragStartRef = refName;
	                this.MarkPropDirty("TileDragStartRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileDragStartScript;
	        }
	    }
	
	    partial void OnHandlingTileDragStart(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileDragStart = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileDragStart
	    {
	        get 
	        {
	            return this._tileDragStart != null ? this._tileDragStart.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileDragStart, ref eventCallbacksCache))
	                {
	                    _tileDragStart = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragStart", value, (args) => {
	                        OnHandlingTileDragStart(args);
	                        
	                    });
	        this.OnRefChanged("TileDragStart", null, "event:::TileDragStart", true, false, (refName, oldValue, newValue) => {
	                        this._tileDragStartRef = refName;
	                        this.MarkPropDirty("TileDragStartRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileDragStart = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragStart", null);
	    this.OnRefChanged("TileDragStart", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileDragStartRef = null;
	                    this.MarkPropDirty("TileDragStartRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileDragEndRef = null;
	    private string _tileDragEndScript = null;
	    [Parameter]
	    public string TileDragEndScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileDragEnd", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileDragEndRef = refName;
	                this.MarkPropDirty("TileDragEndRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileDragEndScript;
	        }
	    }
	
	    partial void OnHandlingTileDragEnd(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileDragEnd = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileDragEnd
	    {
	        get 
	        {
	            return this._tileDragEnd != null ? this._tileDragEnd.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileDragEnd, ref eventCallbacksCache))
	                {
	                    _tileDragEnd = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragEnd", value, (args) => {
	                        OnHandlingTileDragEnd(args);
	                        
	                    });
	        this.OnRefChanged("TileDragEnd", null, "event:::TileDragEnd", true, false, (refName, oldValue, newValue) => {
	                        this._tileDragEndRef = refName;
	                        this.MarkPropDirty("TileDragEndRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileDragEnd = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragEnd", null);
	    this.OnRefChanged("TileDragEnd", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileDragEndRef = null;
	                    this.MarkPropDirty("TileDragEndRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileDragCancelRef = null;
	    private string _tileDragCancelScript = null;
	    [Parameter]
	    public string TileDragCancelScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileDragCancel", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileDragCancelRef = refName;
	                this.MarkPropDirty("TileDragCancelRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileDragCancelScript;
	        }
	    }
	
	    partial void OnHandlingTileDragCancel(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileDragCancel = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileDragCancel
	    {
	        get 
	        {
	            return this._tileDragCancel != null ? this._tileDragCancel.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileDragCancel, ref eventCallbacksCache))
	                {
	                    _tileDragCancel = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragCancel", value, (args) => {
	                        OnHandlingTileDragCancel(args);
	                        
	                    });
	        this.OnRefChanged("TileDragCancel", null, "event:::TileDragCancel", true, false, (refName, oldValue, newValue) => {
	                        this._tileDragCancelRef = refName;
	                        this.MarkPropDirty("TileDragCancelRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileDragCancel = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragCancel", null);
	    this.OnRefChanged("TileDragCancel", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileDragCancelRef = null;
	                    this.MarkPropDirty("TileDragCancelRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileResizeStartRef = null;
	    private string _tileResizeStartScript = null;
	    [Parameter]
	    public string TileResizeStartScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileResizeStart", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileResizeStartRef = refName;
	                this.MarkPropDirty("TileResizeStartRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileResizeStartScript;
	        }
	    }
	
	    partial void OnHandlingTileResizeStart(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileResizeStart = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileResizeStart
	    {
	        get 
	        {
	            return this._tileResizeStart != null ? this._tileResizeStart.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileResizeStart, ref eventCallbacksCache))
	                {
	                    _tileResizeStart = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeStart", value, (args) => {
	                        OnHandlingTileResizeStart(args);
	                        
	                    });
	        this.OnRefChanged("TileResizeStart", null, "event:::TileResizeStart", true, false, (refName, oldValue, newValue) => {
	                        this._tileResizeStartRef = refName;
	                        this.MarkPropDirty("TileResizeStartRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileResizeStart = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeStart", null);
	    this.OnRefChanged("TileResizeStart", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileResizeStartRef = null;
	                    this.MarkPropDirty("TileResizeStartRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileResizeEndRef = null;
	    private string _tileResizeEndScript = null;
	    [Parameter]
	    public string TileResizeEndScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileResizeEnd", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileResizeEndRef = refName;
	                this.MarkPropDirty("TileResizeEndRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileResizeEndScript;
	        }
	    }
	
	    partial void OnHandlingTileResizeEnd(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileResizeEnd = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileResizeEnd
	    {
	        get 
	        {
	            return this._tileResizeEnd != null ? this._tileResizeEnd.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileResizeEnd, ref eventCallbacksCache))
	                {
	                    _tileResizeEnd = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeEnd", value, (args) => {
	                        OnHandlingTileResizeEnd(args);
	                        
	                    });
	        this.OnRefChanged("TileResizeEnd", null, "event:::TileResizeEnd", true, false, (refName, oldValue, newValue) => {
	                        this._tileResizeEndRef = refName;
	                        this.MarkPropDirty("TileResizeEndRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileResizeEnd = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeEnd", null);
	    this.OnRefChanged("TileResizeEnd", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileResizeEndRef = null;
	                    this.MarkPropDirty("TileResizeEndRef");	
	            });
	    }
	    }
	    }
	
	    private string _tileResizeCancelRef = null;
	    private string _tileResizeCancelScript = null;
	    [Parameter]
	    public string TileResizeCancelScript { 
	    
	        set 
	        {
	            this.OnRefChanged("TileResizeCancel", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._tileResizeCancelRef = refName;
	                this.MarkPropDirty("TileResizeCancelRef");	
	        }); 
	        }
	        get 
	        {
	            return this._tileResizeCancelScript;
	        }
	    }
	
	    partial void OnHandlingTileResizeCancel(IgbTileComponentEventArgs args);
	    private EventCallback<IgbTileComponentEventArgs>? _tileResizeCancel = null;
	    [Parameter]
	    public EventCallback<IgbTileComponentEventArgs> TileResizeCancel
	    {
	        get 
	        {
	            return this._tileResizeCancel != null ? this._tileResizeCancel.Value : EventCallback<IgbTileComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbTileComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _tileResizeCancel, ref eventCallbacksCache))
	                {
	                    _tileResizeCancel = value;
	                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeCancel", value, (args) => {
	                        OnHandlingTileResizeCancel(args);
	                        
	                    });
	        this.OnRefChanged("TileResizeCancel", null, "event:::TileResizeCancel", true, false, (refName, oldValue, newValue) => {
	                        this._tileResizeCancelRef = refName;
	                        this.MarkPropDirty("TileResizeCancelRef");	
	                });
	                }
	    }
	        else 
	            {
	                _tileResizeCancel = null;
	                this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeCancel", null);
	    this.OnRefChanged("TileResizeCancel", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._tileResizeCancelRef = null;
	                    this.MarkPropDirty("TileResizeCancelRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbTile(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbTile(ser);
	
	if (IsPropDirty("ColSpan")) { ser.AddNumberProp("colSpan", this._colSpan); }
	if (IsPropDirty("RowSpan")) { ser.AddNumberProp("rowSpan", this._rowSpan); }
	if (IsPropDirty("ColStart")) { ser.AddNumberProp("colStart", this._colStart); }
	if (IsPropDirty("RowStart")) { ser.AddNumberProp("rowStart", this._rowStart); }
	if (IsPropDirty("Maximized")) { ser.AddBooleanProp("maximized", this._maximized); }
	if (IsPropDirty("DisableResize")) { ser.AddBooleanProp("disableResize", this._disableResize); }
	if (IsPropDirty("DisableFullscreen")) { ser.AddBooleanProp("disableFullscreen", this._disableFullscreen); }
	if (IsPropDirty("DisableMaximize")) { ser.AddBooleanProp("disableMaximize", this._disableMaximize); }
	if (IsPropDirty("Position")) { ser.AddNumberProp("position", this._position); }
	if (IsPropDirty("TileFullscreenRef")) { ser.AddStringProp("tileFullscreenRef", this._tileFullscreenRef); }
	if (IsPropDirty("TileMaximizeRef")) { ser.AddStringProp("tileMaximizeRef", this._tileMaximizeRef); }
	if (IsPropDirty("TileDragStartRef")) { ser.AddStringProp("tileDragStartRef", this._tileDragStartRef); }
	if (IsPropDirty("TileDragEndRef")) { ser.AddStringProp("tileDragEndRef", this._tileDragEndRef); }
	if (IsPropDirty("TileDragCancelRef")) { ser.AddStringProp("tileDragCancelRef", this._tileDragCancelRef); }
	if (IsPropDirty("TileResizeStartRef")) { ser.AddStringProp("tileResizeStartRef", this._tileResizeStartRef); }
	if (IsPropDirty("TileResizeEndRef")) { ser.AddStringProp("tileResizeEndRef", this._tileResizeEndRef); }
	if (IsPropDirty("TileResizeCancelRef")) { ser.AddStringProp("tileResizeCancelRef", this._tileResizeCancelRef); }
	
	    }
	
}
}
