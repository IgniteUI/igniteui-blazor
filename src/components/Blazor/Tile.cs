using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The tile component is used within the <see cref="IgbTileManager"/> as a container
    /// for displaying various types of information.
    /// </summary>
    public partial class IgbTile : BaseRendererControl
    {
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

        private double _colSpan = 1;

        /// <summary>
        /// The number of columns the tile will span.
        /// </summary>
        [Parameter]
        public double ColSpan
        {
            get { return this._colSpan; }
            set
            {
                if (this._colSpan != value || !IsPropDirty("ColSpan"))
                {
                    MarkPropDirty("ColSpan");
                }
                this._colSpan = value;

            }
        }
        private double _rowSpan = 1;

        /// <summary>
        /// The number of rows the tile will span.
        /// </summary>
        [Parameter]
        public double RowSpan
        {
            get { return this._rowSpan; }
            set
            {
                if (this._rowSpan != value || !IsPropDirty("RowSpan"))
                {
                    MarkPropDirty("RowSpan");
                }
                this._rowSpan = value;

            }
        }
        private double? _colStart = 0;

        /// <summary>
        /// The starting column for the tile.
        /// </summary>
        [Parameter]
        public double? ColStart
        {
            get { return this._colStart; }
            set
            {
                if (this._colStart != value || !IsPropDirty("ColStart"))
                {
                    MarkPropDirty("ColStart");
                }
                this._colStart = value;

            }
        }
        private double? _rowStart = 0;

        /// <summary>
        /// The starting row for the tile.
        /// </summary>
        [Parameter]
        public double? RowStart
        {
            get { return this._rowStart; }
            set
            {
                if (this._rowStart != value || !IsPropDirty("RowStart"))
                {
                    MarkPropDirty("RowStart");
                }
                this._rowStart = value;

            }
        }

        /// <summary>
        /// Indicates whether the tile occupies the whole screen.
        /// </summary>
        public async Task<bool> GetFullscreenAsync()
        {
            var iv = await InvokeMethod("p:Fullscreen", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Indicates whether the tile occupies the whole screen.
        /// </summary>
        public bool GetFullscreen()
        {
            var iv = InvokeMethodSync("p:Fullscreen", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        private bool _maximized = false;

        /// <summary>
        /// Indicates whether the tile occupies all available space within the layout.
        /// </summary>
        [Parameter]
        public bool Maximized
        {
            get { return this._maximized; }
            set
            {
                if (this._maximized != value || !IsPropDirty("Maximized"))
                {
                    MarkPropDirty("Maximized");
                }
                this._maximized = value;

            }
        }
        private bool _disableResize = false;

        /// <summary>
        /// Indicates whether to disable tile resize behavior regardless
        /// of its tile manager parent settings.
        /// </summary>
        [Parameter]
        public bool DisableResize
        {
            get { return this._disableResize; }
            set
            {
                if (this._disableResize != value || !IsPropDirty("DisableResize"))
                {
                    MarkPropDirty("DisableResize");
                }
                this._disableResize = value;

            }
        }
        private bool _disableFullscreen = false;

        /// <summary>
        /// Whether to disable the rendering of the tile <c>fullscreen-action</c> slot and its
        /// default fullscreen action button.
        /// </summary>
        [Parameter]
        public bool DisableFullscreen
        {
            get { return this._disableFullscreen; }
            set
            {
                if (this._disableFullscreen != value || !IsPropDirty("DisableFullscreen"))
                {
                    MarkPropDirty("DisableFullscreen");
                }
                this._disableFullscreen = value;

            }
        }
        private bool _disableMaximize = false;

        /// <summary>
        /// Whether to disable the rendering of the tile <c>maximize-action</c> slot and its
        /// default maximize action button.
        /// </summary>
        [Parameter]
        public bool DisableMaximize
        {
            get { return this._disableMaximize; }
            set
            {
                if (this._disableMaximize != value || !IsPropDirty("DisableMaximize"))
                {
                    MarkPropDirty("DisableMaximize");
                }
                this._disableMaximize = value;

            }
        }
        private double _position = -1;

        /// <summary>
        /// Gets/sets the tile's visual position in the layout.
        /// Corresponds to the CSS <c>order</c> property.
        /// </summary>
        [Parameter]
        public double Position
        {
            get { return this._position; }
            set
            {
                if (this._position != value || !IsPropDirty("Position"))
                {
                    MarkPropDirty("Position");
                }
                this._position = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        private string _tileFullscreenRef = null;
        private string _tileFullscreenScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileFullscreen"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileFullscreenScript
        {

            set
            {
                if (value != this._tileFullscreenScript)
                {
                    this._tileFullscreenScript = value;
                    this.OnRefChanged("TileFullscreen", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileFullscreenRef = refName;
                        this.MarkPropDirty("TileFullscreenRef");
                    });
                }
            }
            get
            {
                return this._tileFullscreenScript;
            }
        }

        private EventCallback<IgbTileChangeStateEventArgs>? _tileFullscreen = null;

        /// <summary>
        /// Fired when the tile's fullscreen state changes.
        /// </summary>
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
                        this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileFullscreen", value);
                        this.OnRefChanged("TileFullscreen", null, "event:::TileFullscreen", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileFullscreenRef = refName;
                            this.MarkPropDirty("TileFullscreenRef");
                        });
                    }
                }
                else
                {
                    _tileFullscreen = null;
                    this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileFullscreen", null);
                    this.OnRefChanged("TileFullscreen", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileFullscreenRef = null;
                        this.MarkPropDirty("TileFullscreenRef");
                    });
                }
            }
        }

        private string _tileMaximizeRef = null;
        private string _tileMaximizeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileMaximize"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileMaximizeScript
        {

            set
            {
                if (value != this._tileMaximizeScript)
                {
                    this._tileMaximizeScript = value;
                    this.OnRefChanged("TileMaximize", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileMaximizeRef = refName;
                        this.MarkPropDirty("TileMaximizeRef");
                    });
                }
            }
            get
            {
                return this._tileMaximizeScript;
            }
        }

        private EventCallback<IgbTileChangeStateEventArgs>? _tileMaximize = null;

        /// <summary>
        /// Fired when the tile's maximize state changes.
        /// </summary>
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
                        this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileMaximize", value);
                        this.OnRefChanged("TileMaximize", null, "event:::TileMaximize", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileMaximizeRef = refName;
                            this.MarkPropDirty("TileMaximizeRef");
                        });
                    }
                }
                else
                {
                    _tileMaximize = null;
                    this.SetHandler<IgbTileChangeStateEventArgs>(this.Name, "TileMaximize", null);
                    this.OnRefChanged("TileMaximize", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileMaximizeRef = null;
                        this.MarkPropDirty("TileMaximizeRef");
                    });
                }
            }
        }

        private string _tileDragStartRef = null;
        private string _tileDragStartScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileDragStart"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileDragStartScript
        {

            set
            {
                if (value != this._tileDragStartScript)
                {
                    this._tileDragStartScript = value;
                    this.OnRefChanged("TileDragStart", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileDragStartRef = refName;
                        this.MarkPropDirty("TileDragStartRef");
                    });
                }
            }
            get
            {
                return this._tileDragStartScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileDragStart = null;

        /// <summary>
        /// Fired when a drag operation on a tile is about to begin.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragStart", value);
                        this.OnRefChanged("TileDragStart", null, "event:::TileDragStart", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileDragStartRef = refName;
                            this.MarkPropDirty("TileDragStartRef");
                        });
                    }
                }
                else
                {
                    _tileDragStart = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragStart", null);
                    this.OnRefChanged("TileDragStart", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileDragStartRef = null;
                        this.MarkPropDirty("TileDragStartRef");
                    });
                }
            }
        }

        private string _tileDragEndRef = null;
        private string _tileDragEndScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileDragEnd"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileDragEndScript
        {

            set
            {
                if (value != this._tileDragEndScript)
                {
                    this._tileDragEndScript = value;
                    this.OnRefChanged("TileDragEnd", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileDragEndRef = refName;
                        this.MarkPropDirty("TileDragEndRef");
                    });
                }
            }
            get
            {
                return this._tileDragEndScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileDragEnd = null;

        /// <summary>
        /// Fired when a drag operation with a tile is successfully completed.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragEnd", value);
                        this.OnRefChanged("TileDragEnd", null, "event:::TileDragEnd", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileDragEndRef = refName;
                            this.MarkPropDirty("TileDragEndRef");
                        });
                    }
                }
                else
                {
                    _tileDragEnd = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragEnd", null);
                    this.OnRefChanged("TileDragEnd", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileDragEndRef = null;
                        this.MarkPropDirty("TileDragEndRef");
                    });
                }
            }
        }

        private string _tileDragCancelRef = null;
        private string _tileDragCancelScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileDragCancel"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileDragCancelScript
        {

            set
            {
                if (value != this._tileDragCancelScript)
                {
                    this._tileDragCancelScript = value;
                    this.OnRefChanged("TileDragCancel", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileDragCancelRef = refName;
                        this.MarkPropDirty("TileDragCancelRef");
                    });
                }
            }
            get
            {
                return this._tileDragCancelScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileDragCancel = null;

        /// <summary>
        /// Fired when a tile drag operation is canceled by the user.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragCancel", value);
                        this.OnRefChanged("TileDragCancel", null, "event:::TileDragCancel", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileDragCancelRef = refName;
                            this.MarkPropDirty("TileDragCancelRef");
                        });
                    }
                }
                else
                {
                    _tileDragCancel = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileDragCancel", null);
                    this.OnRefChanged("TileDragCancel", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileDragCancelRef = null;
                        this.MarkPropDirty("TileDragCancelRef");
                    });
                }
            }
        }

        private string _tileResizeStartRef = null;
        private string _tileResizeStartScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileResizeStart"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileResizeStartScript
        {

            set
            {
                if (value != this._tileResizeStartScript)
                {
                    this._tileResizeStartScript = value;
                    this.OnRefChanged("TileResizeStart", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileResizeStartRef = refName;
                        this.MarkPropDirty("TileResizeStartRef");
                    });
                }
            }
            get
            {
                return this._tileResizeStartScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileResizeStart = null;

        /// <summary>
        /// Fired when a resize operation on a tile is about to begin.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeStart", value);
                        this.OnRefChanged("TileResizeStart", null, "event:::TileResizeStart", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileResizeStartRef = refName;
                            this.MarkPropDirty("TileResizeStartRef");
                        });
                    }
                }
                else
                {
                    _tileResizeStart = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeStart", null);
                    this.OnRefChanged("TileResizeStart", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileResizeStartRef = null;
                        this.MarkPropDirty("TileResizeStartRef");
                    });
                }
            }
        }

        private string _tileResizeEndRef = null;
        private string _tileResizeEndScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileResizeEnd"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileResizeEndScript
        {

            set
            {
                if (value != this._tileResizeEndScript)
                {
                    this._tileResizeEndScript = value;
                    this.OnRefChanged("TileResizeEnd", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileResizeEndRef = refName;
                        this.MarkPropDirty("TileResizeEndRef");
                    });
                }
            }
            get
            {
                return this._tileResizeEndScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileResizeEnd = null;

        /// <summary>
        /// Fired when a resize operation on a tile is successfully completed.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeEnd", value);
                        this.OnRefChanged("TileResizeEnd", null, "event:::TileResizeEnd", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileResizeEndRef = refName;
                            this.MarkPropDirty("TileResizeEndRef");
                        });
                    }
                }
                else
                {
                    _tileResizeEnd = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeEnd", null);
                    this.OnRefChanged("TileResizeEnd", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileResizeEndRef = null;
                        this.MarkPropDirty("TileResizeEndRef");
                    });
                }
            }
        }

        private string _tileResizeCancelRef = null;
        private string _tileResizeCancelScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="TileResizeCancel"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string TileResizeCancelScript
        {

            set
            {
                if (value != this._tileResizeCancelScript)
                {
                    this._tileResizeCancelScript = value;
                    this.OnRefChanged("TileResizeCancel", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._tileResizeCancelRef = refName;
                        this.MarkPropDirty("TileResizeCancelRef");
                    });
                }
            }
            get
            {
                return this._tileResizeCancelScript;
            }
        }

        private EventCallback<IgbTileComponentEventArgs>? _tileResizeCancel = null;

        /// <summary>
        /// Fired when a resize operation on a tile is canceled by the user.
        /// </summary>
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
                        this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeCancel", value);
                        this.OnRefChanged("TileResizeCancel", null, "event:::TileResizeCancel", true, false, (refName, oldValue, newValue) =>
                        {
                            this._tileResizeCancelRef = refName;
                            this.MarkPropDirty("TileResizeCancelRef");
                        });
                    }
                }
                else
                {
                    _tileResizeCancel = null;
                    this.SetHandler<IgbTileComponentEventArgs>(this.Name, "TileResizeCancel", null);
                    this.OnRefChanged("TileResizeCancel", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._tileResizeCancelRef = null;
                        this.MarkPropDirty("TileResizeCancelRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("ColSpan"))
            { ser.AddNumberProp("colSpan", this._colSpan); }
            if (IsPropDirty("RowSpan"))
            { ser.AddNumberProp("rowSpan", this._rowSpan); }
            if (IsPropDirty("ColStart"))
            { ser.AddNumberProp("colStart", this._colStart); }
            if (IsPropDirty("RowStart"))
            { ser.AddNumberProp("rowStart", this._rowStart); }
            if (IsPropDirty("Maximized"))
            { ser.AddBooleanProp("maximized", this._maximized); }
            if (IsPropDirty("DisableResize"))
            { ser.AddBooleanProp("disableResize", this._disableResize); }
            if (IsPropDirty("DisableFullscreen"))
            { ser.AddBooleanProp("disableFullscreen", this._disableFullscreen); }
            if (IsPropDirty("DisableMaximize"))
            { ser.AddBooleanProp("disableMaximize", this._disableMaximize); }
            if (IsPropDirty("Position"))
            { ser.AddNumberProp("position", this._position); }
            if (IsPropDirty("TileFullscreenRef"))
            { ser.AddStringProp("tileFullscreenRef", this._tileFullscreenRef); }
            if (IsPropDirty("TileMaximizeRef"))
            { ser.AddStringProp("tileMaximizeRef", this._tileMaximizeRef); }
            if (IsPropDirty("TileDragStartRef"))
            { ser.AddStringProp("tileDragStartRef", this._tileDragStartRef); }
            if (IsPropDirty("TileDragEndRef"))
            { ser.AddStringProp("tileDragEndRef", this._tileDragEndRef); }
            if (IsPropDirty("TileDragCancelRef"))
            { ser.AddStringProp("tileDragCancelRef", this._tileDragCancelRef); }
            if (IsPropDirty("TileResizeStartRef"))
            { ser.AddStringProp("tileResizeStartRef", this._tileResizeStartRef); }
            if (IsPropDirty("TileResizeEndRef"))
            { ser.AddStringProp("tileResizeEndRef", this._tileResizeEndRef); }
            if (IsPropDirty("TileResizeCancelRef"))
            { ser.AddStringProp("tileResizeCancelRef", this._tileResizeCancelRef); }

        }

    }
}
