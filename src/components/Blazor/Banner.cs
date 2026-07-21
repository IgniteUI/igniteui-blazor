using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbBanner : BaseRendererControl
    {
        public override string Type { get { return "WebBanner"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbBannerModule.IsLoadRequested(IgBlazor))
            {
                IgbBannerModule.Register(IgBlazor);
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
                return "igc-banner";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbBanner() : base()
        {
            OnCreatedIgbBanner();

        }

        partial void OnCreatedIgbBanner();

        private bool _open = false;

        partial void OnOpenChanging(ref bool newValue);
        /// <summary>
        /// Whether the banner is open.
        /// Setting this property programmatically will immediately show or hide the
        /// banner without animation and without emitting close events.
        /// Prefer the `show()`, `hide()`, and `toggle()` methods for animated
        /// transitions.
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

        partial void FindByNameBanner(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameBanner(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
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
        /// Opens the banner with an animated grow-in transition.
        /// Returns `true` when the banner was successfully opened, or `false` if
        /// it was already open.
        /// </summary>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Closes the banner with an animated grow-out transition.
        /// Returns `true` when the banner was successfully closed, or `false` if
        /// it was already closed.
        /// </summary>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the banner open or closed depending on its current state.
        /// Equivalent to calling `show()` when closed and `hide()` when open.
        /// Returns `true` when the transition completed successfully.
        /// </summary>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public bool Toggle()
        {
            var iv = InvokeMethodSync("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        private string _closingRef = null;
        private string _closingScript = null;
        [Parameter]
        public string ClosingScript
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
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value, (args) =>
                        {
                            OnHandlingClosing(args);

                        });
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

        private string _closedRef = null;
        private string _closedScript = null;
        [Parameter]
        public string ClosedScript
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
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value, (args) =>
                        {
                            OnHandlingClosed(args);

                        });
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

        partial void SerializeCoreIgbBanner(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbBanner(ser);

            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }

        }

    }
}
