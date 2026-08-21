using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A splitter component that provides a resizable split-pane layout, dividing the view
    /// into two panels — start and end — separated by a draggable bar.
    /// Panels can be resized by dragging the bar, using keyboard shortcuts, or collapsed/expanded
    /// using the built-in collapse buttons or the <see cref="Toggle"/> method.
    /// Nested splitters are supported for more complex layouts.
    /// </summary>
    public partial class IgbSplitter : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSplitter"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSplitterModule.IsLoadRequested(IgBlazor))
            {
                IgbSplitterModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-splitter";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private SplitterOrientation _orientation = SplitterOrientation.Horizontal;

        /// <summary>
        /// The orientation of the splitter, which determines the direction of resizing and collapsing.
        /// </summary>
        [Parameter]
        public SplitterOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                if (this._orientation != value || !IsPropDirty("Orientation"))
                {
                    MarkPropDirty("Orientation");
                }
                this._orientation = value;

            }
        }
        private bool _disableCollapse = false;

        /// <summary>
        /// When <see langword="true"/>, prevents the user from collapsing either pane.
        /// This also hides the expand/collapse buttons on the splitter bar.
        /// </summary>
        [Parameter]
        public bool DisableCollapse
        {
            get { return this._disableCollapse; }
            set
            {
                if (this._disableCollapse != value || !IsPropDirty("DisableCollapse"))
                {
                    MarkPropDirty("DisableCollapse");
                }
                this._disableCollapse = value;

            }
        }
        private bool _disableResize = false;

        /// <summary>
        /// When <see langword="true"/>, prevents the user from resizing the panes by dragging the splitter bar
        /// or using keyboard shortcuts.
        /// This also hides the drag handle on the splitter bar.
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
        private bool _hideCollapseButtons = false;

        /// <summary>
        /// When <see langword="true"/>, hides the expand/collapse buttons on the splitter bar.
        /// Note that the buttons will also be hidden if <see cref="DisableCollapse"/> is
        /// <see langword="true"/> or if a pane is currently collapsed.
        /// </summary>
        [Parameter]
        public bool HideCollapseButtons
        {
            get { return this._hideCollapseButtons; }
            set
            {
                if (this._hideCollapseButtons != value || !IsPropDirty("HideCollapseButtons"))
                {
                    MarkPropDirty("HideCollapseButtons");
                }
                this._hideCollapseButtons = value;

            }
        }
        private bool _hideDragHandle = false;

        /// <summary>
        /// When <see langword="true"/>, hides the drag handle on the splitter bar.
        /// Note that the drag handle will also be hidden if <see cref="DisableResize"/>
        /// is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool HideDragHandle
        {
            get { return this._hideDragHandle; }
            set
            {
                if (this._hideDragHandle != value || !IsPropDirty("HideDragHandle"))
                {
                    MarkPropDirty("HideDragHandle");
                }
                this._hideDragHandle = value;

            }
        }
        private string? _startMinSize;

        /// <summary>
        /// The minimum size of the start pane.
        /// </summary>
        [Parameter]
        public string? StartMinSize
        {
            get { return this._startMinSize; }
            set
            {
                if (this._startMinSize != value || !IsPropDirty("StartMinSize"))
                {
                    MarkPropDirty("StartMinSize");
                }
                this._startMinSize = value;

            }
        }
        private string? _endMinSize;

        /// <summary>
        /// The minimum size of the end pane.
        /// </summary>
        [Parameter]
        public string? EndMinSize
        {
            get { return this._endMinSize; }
            set
            {
                if (this._endMinSize != value || !IsPropDirty("EndMinSize"))
                {
                    MarkPropDirty("EndMinSize");
                }
                this._endMinSize = value;

            }
        }
        private string? _startMaxSize;

        /// <summary>
        /// The maximum size of the start pane.
        /// </summary>
        [Parameter]
        public string? StartMaxSize
        {
            get { return this._startMaxSize; }
            set
            {
                if (this._startMaxSize != value || !IsPropDirty("StartMaxSize"))
                {
                    MarkPropDirty("StartMaxSize");
                }
                this._startMaxSize = value;

            }
        }
        private string? _endMaxSize;

        /// <summary>
        /// The maximum size of the end pane.
        /// </summary>
        [Parameter]
        public string? EndMaxSize
        {
            get { return this._endMaxSize; }
            set
            {
                if (this._endMaxSize != value || !IsPropDirty("EndMaxSize"))
                {
                    MarkPropDirty("EndMaxSize");
                }
                this._endMaxSize = value;

            }
        }
        private string? _startSize;

        /// <summary>
        /// The size of the start pane.
        /// </summary>
        [Parameter]
        public string? StartSize
        {
            get { return this._startSize; }
            set
            {
                if (this._startSize != value || !IsPropDirty("StartSize"))
                {
                    MarkPropDirty("StartSize");
                }
                this._startSize = value;

            }
        }
        private string? _endSize;

        /// <summary>
        /// The size of the end pane.
        /// </summary>
        [Parameter]
        public string? EndSize
        {
            get { return this._endSize; }
            set
            {
                if (this._endSize != value || !IsPropDirty("EndSize"))
                {
                    MarkPropDirty("EndSize");
                }
                this._endSize = value;

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
        /// <summary>
        /// Toggles the collapsed state of the specified pane.
        /// </summary>
        public async Task ToggleAsync(PanePosition position)
        {
            await InvokeMethod("toggle", new object[] { ObjectToParam(position, typeof(PanePosition)) }, new string[] { "Json" });
        }

        /// <summary>
        /// Toggles the collapsed state of the specified pane.
        /// </summary>
        public void Toggle(PanePosition position)
        {
            InvokeMethodSync("toggle", new object[] { ObjectToParam(position, typeof(PanePosition)) }, new string[] { "Json" });
        }

        private string _resizeStartRef = null;
        private string _resizeStartScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="ResizeStart"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ResizeStartScript
        {

            set
            {
                if (value != this._resizeStartScript)
                {
                    this._resizeStartScript = value;
                    this.OnRefChanged("ResizeStart", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
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

        private EventCallback<IgbSplitterResizeEventArgs>? _resizeStart = null;

        /// <summary>
        /// Emitted once when a resize operation begins (pointer drag or keyboard).
        /// </summary>
        [Parameter]
        public EventCallback<IgbSplitterResizeEventArgs> ResizeStart
        {
            get
            {
                return this._resizeStart != null ? this._resizeStart.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_resizeStart))
                    {
                        _resizeStart = value;
                        this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeStart", value);
                        this.OnRefChanged("ResizeStart", null, "event:::ResizeStart", true, false, (refName, oldValue, newValue) =>
                        {
                            this._resizeStartRef = refName;
                            this.MarkPropDirty("ResizeStartRef");
                        });
                    }
                }
                else
                {
                    _resizeStart = null;
                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeStart", null);
                    this.OnRefChanged("ResizeStart", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._resizeStartRef = null;
                        this.MarkPropDirty("ResizeStartRef");
                    });
                }
            }
        }

        private string _resizingRef = null;
        private string _resizingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Resizing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ResizingScript
        {

            set
            {
                if (value != this._resizingScript)
                {
                    this._resizingScript = value;
                    this.OnRefChanged("Resizing", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
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

        private EventCallback<IgbSplitterResizeEventArgs>? _resizing = null;

        /// <summary>
        /// Emitted continuously while a pane is being resized.
        /// </summary>
        [Parameter]
        public EventCallback<IgbSplitterResizeEventArgs> Resizing
        {
            get
            {
                return this._resizing != null ? this._resizing.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_resizing))
                    {
                        _resizing = value;
                        this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "Resizing", value);
                        this.OnRefChanged("Resizing", null, "event:::Resizing", true, false, (refName, oldValue, newValue) =>
                        {
                            this._resizingRef = refName;
                            this.MarkPropDirty("ResizingRef");
                        });
                    }
                }
                else
                {
                    _resizing = null;
                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "Resizing", null);
                    this.OnRefChanged("Resizing", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._resizingRef = null;
                        this.MarkPropDirty("ResizingRef");
                    });
                }
            }
        }

        private string _resizeEndRef = null;
        private string _resizeEndScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="ResizeEnd"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ResizeEndScript
        {

            set
            {
                if (value != this._resizeEndScript)
                {
                    this._resizeEndScript = value;
                    this.OnRefChanged("ResizeEnd", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
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

        private EventCallback<IgbSplitterResizeEventArgs>? _resizeEnd = null;

        /// <summary>
        /// Emitted once when a resize operation completes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbSplitterResizeEventArgs> ResizeEnd
        {
            get
            {
                return this._resizeEnd != null ? this._resizeEnd.Value : EventCallback<IgbSplitterResizeEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_resizeEnd))
                    {
                        _resizeEnd = value;
                        this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeEnd", value);
                        this.OnRefChanged("ResizeEnd", null, "event:::ResizeEnd", true, false, (refName, oldValue, newValue) =>
                        {
                            this._resizeEndRef = refName;
                            this.MarkPropDirty("ResizeEndRef");
                        });
                    }
                }
                else
                {
                    _resizeEnd = null;
                    this.SetHandler<IgbSplitterResizeEventArgs>(this.Name, "ResizeEnd", null);
                    this.OnRefChanged("ResizeEnd", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._resizeEndRef = null;
                        this.MarkPropDirty("ResizeEndRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }
            if (IsPropDirty("DisableCollapse"))
            { ser.AddBooleanProp("disableCollapse", this._disableCollapse); }
            if (IsPropDirty("DisableResize"))
            { ser.AddBooleanProp("disableResize", this._disableResize); }
            if (IsPropDirty("HideCollapseButtons"))
            { ser.AddBooleanProp("hideCollapseButtons", this._hideCollapseButtons); }
            if (IsPropDirty("HideDragHandle"))
            { ser.AddBooleanProp("hideDragHandle", this._hideDragHandle); }
            if (IsPropDirty("StartMinSize"))
            { ser.AddStringProp("startMinSize", this._startMinSize); }
            if (IsPropDirty("EndMinSize"))
            { ser.AddStringProp("endMinSize", this._endMinSize); }
            if (IsPropDirty("StartMaxSize"))
            { ser.AddStringProp("startMaxSize", this._startMaxSize); }
            if (IsPropDirty("EndMaxSize"))
            { ser.AddStringProp("endMaxSize", this._endMaxSize); }
            if (IsPropDirty("StartSize"))
            { ser.AddStringProp("startSize", this._startSize); }
            if (IsPropDirty("EndSize"))
            { ser.AddStringProp("endSize", this._endSize); }
            if (IsPropDirty("ResizeStartRef"))
            { ser.AddStringProp("resizeStartRef", this._resizeStartRef); }
            if (IsPropDirty("ResizingRef"))
            { ser.AddStringProp("resizingRef", this._resizingRef); }
            if (IsPropDirty("ResizeEndRef"))
            { ser.AddStringProp("resizeEndRef", this._resizeEndRef); }

        }

    }
}
