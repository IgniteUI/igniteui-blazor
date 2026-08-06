using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A modal dialog component built on the native <c>&lt;dialog&gt;</c> element.
    /// The dialog traps focus while open and blocks interaction with the rest
    /// of the page (modal semantics). It supports animated open/close
    /// transitions, an optional backdrop overlay, and multiple content areas
    /// through named slots.
    /// The component integrates with the
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Invoker_Commands_API">Invoker Commands API</see>:
    /// an <see cref="IgbButton"/> or a native <c>&lt;button&gt;</c> with <c>command="--show"</c> / <c>"--hide"</c> /
    /// <c>"--toggle"</c> and <c>commandfor</c> pointing to this element will call the corresponding
    /// method declaratively.
    /// </summary>
    public partial class IgbDialog : BaseRendererControl
    {
        public override string Type { get { return "WebDialog"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbDialogModule.IsLoadRequested(IgBlazor))
            {
                IgbDialogModule.Register(IgBlazor);
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
                return "igc-dialog";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _keepOpenOnEscape = false;

        partial void OnKeepOpenOnEscapeChanging(ref bool newValue);
        /// <summary>
        /// When set, pressing the <c>Escape</c> key will not close the dialog.
        /// By default the browser closes a modal dialog on <c>Escape</c>. Enable this
        /// option when the dialog guards unsaved work and should require an explicit
        /// user action to dismiss.
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
        private bool _closeOnOutsideClick = false;

        partial void OnCloseOnOutsideClickChanging(ref bool newValue);
        /// <summary>
        /// When set, clicking on the backdrop area outside the dialog surface
        /// will close it (emitting close events).
        /// Has no effect when the dialog is not yet open.
        /// </summary>
        [Parameter]
        public bool CloseOnOutsideClick
        {
            get { return this._closeOnOutsideClick; }
            set
            {
                if (this._closeOnOutsideClick != value || !IsPropDirty("CloseOnOutsideClick"))
                {
                    MarkPropDirty("CloseOnOutsideClick");
                }
                this._closeOnOutsideClick = value;

            }
        }
        private bool _hideDefaultAction = false;

        partial void OnHideDefaultActionChanging(ref bool newValue);
        /// <summary>
        /// When set, the built-in "OK" close button in the footer is not rendered.
        /// Has no effect when content is projected into the <c>footer</c> slot, since
        /// the slot content replaces the default button entirely.
        /// </summary>
        [Parameter]
        public bool HideDefaultAction
        {
            get { return this._hideDefaultAction; }
            set
            {
                if (this._hideDefaultAction != value || !IsPropDirty("HideDefaultAction"))
                {
                    MarkPropDirty("HideDefaultAction");
                }
                this._hideDefaultAction = value;

            }
        }
        private bool _open = false;

        partial void OnOpenChanging(ref bool newValue);
        /// <summary>
        /// Whether the dialog is open.
        /// Setting this property programmatically will open or close the dialog
        /// without animation and without emitting close events.
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
        private string _title;

        partial void OnTitleChanging(ref string newValue);
        /// <summary>
        /// The title displayed in the dialog header.
        /// Overridden by any content projected into the <c>title</c> slot.
        /// </summary>
        [Parameter]
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title != value || !IsPropDirty("Title"))
                {
                    MarkPropDirty("Title");
                }
                this._title = value;

            }
        }
        private string _returnValue;

        partial void OnReturnValueChanging(ref string newValue);

        /// <summary>
        /// The return value of the dialog.
        /// Automatically set to the <c>value</c> of the submitter element when a
        /// <c>&lt;form method="dialog"&gt;</c> inside the dialog is submitted. Can also
        /// be set programmatically before calling <see cref="Hide"/>.
        /// </summary>
        [Parameter]
        public string ReturnValue
        {
            get { return this._returnValue; }
            set
            {
                if (this._returnValue != value || !IsPropDirty("ReturnValue"))
                {
                    MarkPropDirty("ReturnValue");
                }
                this._returnValue = value;

            }
        }

        partial void FindByNameDialog(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameDialog(name, ref item);
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
        /// Opens the dialog with an animated fade-in transition.
        /// </summary>
        /// <returns><see langword="true"/> when the dialog was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Opens the dialog with an animated fade-in transition.
        /// </summary>
        /// <returns><see langword="true"/> when the dialog was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Closes the dialog with an animated fade-out transition.
        /// </summary>
        /// <returns><see langword="true"/> when the dialog was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Closes the dialog with an animated fade-out transition.
        /// </summary>
        /// <returns><see langword="true"/> when the dialog was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the dialog open or closed depending on its current state.
        /// Equivalent to calling <see cref="Show"/> when closed and <see cref="Hide"/> when open.
        /// </summary>
        /// <returns><see langword="true"/> when the transition completed successfully.</returns>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Toggles the dialog open or closed depending on its current state.
        /// Equivalent to calling <see cref="Show"/> when closed and <see cref="Hide"/> when open.
        /// </summary>
        /// <returns><see langword="true"/> when the transition completed successfully.</returns>
        public bool Toggle()
        {
            var iv = InvokeMethodSync("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        private string _closingRef = null;
        private string _closingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
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

        /// <summary>
        /// Emitted just before the dialog closes.
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

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closed"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
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

        /// <summary>
        /// Emitted after the dialog has fully closed and its exit animation has completed.
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("KeepOpenOnEscape"))
            { ser.AddBooleanProp("keepOpenOnEscape", this._keepOpenOnEscape); }
            if (IsPropDirty("CloseOnOutsideClick"))
            { ser.AddBooleanProp("closeOnOutsideClick", this._closeOnOutsideClick); }
            if (IsPropDirty("HideDefaultAction"))
            { ser.AddBooleanProp("hideDefaultAction", this._hideDefaultAction); }
            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }
            if (IsPropDirty("Title"))
            { ser.AddStringProp("title", this._title); }
            if (IsPropDirty("ReturnValue"))
            { ser.AddStringProp("returnValue", this._returnValue); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }

        }

    }
}
