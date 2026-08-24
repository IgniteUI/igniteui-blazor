using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A non-modal notification banner that displays important, concise messages
    /// requiring user acknowledgement.
    /// The banner slides into view with an animated grow transition and renders
    /// inline, pushing the surrounding page content rather than overlaying it.
    /// </summary>
    public partial class IgbBanner : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebBanner"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbBannerModule.IsLoadRequested(IgBlazor))
            {
                IgbBannerModule.Register(IgBlazor);
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
                return "igc-banner";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _open = false;

        /// <summary>
        /// Whether the banner is open.
        /// Setting this property programmatically will immediately show or hide the
        /// banner without animation and without emitting close events.
        /// Prefer the <see cref="Show"/>, <see cref="Hide"/>, and <see cref="Toggle"/> methods for
        /// animated transitions.
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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Opens the banner with an animated grow-in transition.
        /// </summary>
        /// <returns><see langword="true"/> when the banner was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Opens the banner with an animated grow-in transition.
        /// </summary>
        /// <returns><see langword="true"/> when the banner was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Closes the banner with an animated grow-out transition.
        /// </summary>
        /// <returns><see langword="true"/> when the banner was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Closes the banner with an animated grow-out transition.
        /// </summary>
        /// <returns><see langword="true"/> when the banner was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the banner open or closed depending on its current state.
        /// Equivalent to calling <see cref="Show"/> when closed and <see cref="Hide"/> when open.
        /// </summary>
        /// <returns><see langword="true"/> when the transition completed successfully.</returns>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Toggles the banner open or closed depending on its current state.
        /// Equivalent to calling <see cref="Show"/> when closed and <see cref="Hide"/> when open.
        /// </summary>
        /// <returns><see langword="true"/> when the transition completed successfully.</returns>
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
                    this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) =>
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
        /// Emitted just before the banner closes in response to the default action button being clicked.
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
                    this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) =>
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
        /// Emitted after the banner has fully closed and its exit animation has completed.
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

            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }

        }

    }
}
