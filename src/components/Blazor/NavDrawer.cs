using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A side navigation container that provides
    /// quick access between views within an application.
    /// For non-relative positions (<see cref="NavDrawerPosition.Start"/>,
    /// <see cref="NavDrawerPosition.End"/>, <see cref="NavDrawerPosition.Top"/>,
    /// <see cref="NavDrawerPosition.Bottom"/>) the drawer is rendered as a native
    /// <c>&lt;dialog&gt;</c> element, providing modal semantics, automatic focus trapping,
    /// and a backdrop. For the <see cref="NavDrawerPosition.Relative"/> position it is
    /// rendered inline as a <c>&lt;nav&gt;</c> landmark.
    /// When content is provided in the <c>mini</c> slot, a compact icon-only variant is
    /// always displayed alongside the main drawer (hidden only while the full drawer
    /// is open).
    /// The component integrates with the
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Invoker_Commands_API">Invoker Commands API</see>:
    /// an Ignite UI button or a native <c>&lt;button&gt;</c> with <c>command="--show"</c> / <c>"--hide"</c> /
    /// <c>"--toggle"</c> and <c>commandfor</c> pointing to this component will call the
    /// corresponding method declaratively.
    /// </summary>
    public partial class IgbNavDrawer : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebNavDrawer"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavDrawerModule.IsLoadRequested(IgBlazor))
            {
                IgbNavDrawerModule.Register(IgBlazor);
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
                return "igc-nav-drawer";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private NavDrawerPosition _position = NavDrawerPosition.Start;

        /// <summary>
        /// Sets the position of the drawer.
        /// <list type="bullet">
        ///   <item><description>
        ///   <see cref="NavDrawerPosition.Start"/> � anchored to the inline-start edge (default).
        ///   </description></item>
        ///   <item><description>
        ///   <see cref="NavDrawerPosition.End"/> � anchored to the inline-end edge.
        ///   </description></item>
        ///   <item><description>
        ///   <see cref="NavDrawerPosition.Top"/> � anchored to the block-start edge.
        ///   </description></item>
        ///   <item><description>
        ///   <see cref="NavDrawerPosition.Bottom"/> � anchored to the block-end edge.
        ///   </description></item>
        ///   <item><description>
        ///   <see cref="NavDrawerPosition.Relative"/> � rendered inline within the page flow; no modal backdrop.
        ///   </description></item>
        /// </list>
        /// </summary>
        [Parameter]
        public NavDrawerPosition Position
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
        private bool _open = false;

        /// <summary>
        /// Whether the drawer is open.
        /// </summary>
        [Parameter]
        public bool Open
        {
            get { return this._open; }
            set
            {
                if (this._open != value || !IsPropDirty("Open"))
                {
                    MarkPropDirty("Open");
                }
                this._open = value;

            }
        }
        private bool _keepOpenOnEscape = false;

        /// <summary>
        /// Determines whether the drawer should remain open when the Escape key is pressed.
        /// This is only applicable when the drawer is in a non-relative position,
        /// as the Escape key does not trigger the closing of relative drawers.
        /// </summary>
        [Parameter]
        public bool KeepOpenOnEscape
        {
            get { return this._keepOpenOnEscape; }
            set
            {
                if (this._keepOpenOnEscape != value || !IsPropDirty("KeepOpenOnEscape"))
                {
                    MarkPropDirty("KeepOpenOnEscape");
                }
                this._keepOpenOnEscape = value;

            }
        }
        private string? _label;

        /// <summary>
        /// Sets an accessible label for the drawer.
        /// In non-relative positions this label is applied to the modal <c>&lt;dialog&gt;</c> element.
        /// In <see cref="NavDrawerPosition.Relative"/> position it labels the <c>&lt;nav&gt;</c> landmark.
        /// When multiple navigation landmarks exist on the page each should receive a
        /// distinct label so screen-reader users can differentiate between them.
        /// </summary>
        [Parameter]
        public string? Label
        {
            get { return this._label; }
            set
            {
                if (this._label != value || !IsPropDirty("Label"))
                {
                    MarkPropDirty("Label");
                }
                this._label = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Opens the drawer.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the drawer was successfully opened, or <see langword="false"/>
        /// if it was already open.
        /// </returns>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Opens the drawer.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the drawer was successfully opened, or <see langword="false"/>
        /// if it was already open.
        /// </returns>
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Closes the drawer.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the drawer was successfully closed, or <see langword="false"/>
        /// if it was already closed.
        /// </returns>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Closes the drawer.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the drawer was successfully closed, or <see langword="false"/>
        /// if it was already closed.
        /// </returns>
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the open state of the drawer. Delegates to <see cref="Show"/> or <see cref="Hide"/> depending
        /// on the current state.
        /// </summary>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Toggles the open state of the drawer. Delegates to <see cref="Show"/> or <see cref="Hide"/> depending
        /// on the current state.
        /// </summary>
        public bool Toggle()
        {
            var iv = InvokeMethodSync("toggle", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        private string? _closingRef = null;
        private string? _closingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? ClosingScript
        {

            set
            {
                if (value != this._closingScript)
                {
                    this._closingScript = value;
                    this.OnRefChanged("Closing", null, value, true, false, (string refName, object? oldValue, object? newValue) =>
                    {
                        this._closingRef = refName;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
            get
            {
                return this._closingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closing = null;

        /// <summary>
        /// Emitted just before the drawer is closed by a user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closing
        {
            get
            {
                return this._closing != null ? this._closing.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_closing))
                    {
                        _closing = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value);
                        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closingRef = refName;
                            this.MarkPropDirty("ClosingRef");
                        });
                    }
                }
                else
                {
                    _closing = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
                    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closingRef = null;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
        }

        private string? _closedRef = null;
        private string? _closedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closed"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? ClosedScript
        {

            set
            {
                if (value != this._closedScript)
                {
                    this._closedScript = value;
                    this.OnRefChanged("Closed", null, value, true, false, (string refName, object? oldValue, object? newValue) =>
                    {
                        this._closedRef = refName;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
            get
            {
                return this._closedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closed = null;

        /// <summary>
        /// Emitted just after the drawer is closed by a user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closed
        {
            get
            {
                return this._closed != null ? this._closed.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_closed))
                    {
                        _closed = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value);
                        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closedRef = refName;
                            this.MarkPropDirty("ClosedRef");
                        });
                    }
                }
                else
                {
                    _closed = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
                    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closedRef = null;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Position"))
            { ser.AddEnumProp("position", this._position); }
            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }
            if (IsPropDirty("KeepOpenOnEscape"))
            { ser.AddBooleanProp("keepOpenOnEscape", this._keepOpenOnEscape); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }

        }

    }
}
