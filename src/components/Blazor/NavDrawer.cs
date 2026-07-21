
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbNavDrawer : BaseRendererControl
    {
        public override string Type { get { return "WebNavDrawer"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavDrawerModule.IsLoadRequested(IgBlazor))
            {
                IgbNavDrawerModule.Register(IgBlazor);
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
                return "igc-nav-drawer";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbNavDrawer() : base()
        {
            OnCreatedIgbNavDrawer();

        }

        partial void OnCreatedIgbNavDrawer();

        private NavDrawerPosition _position = NavDrawerPosition.Start;

        partial void OnPositionChanging(ref NavDrawerPosition newValue);
        /// <summary>
        /// Sets the position of the drawer.
        /// - `start` — anchored to the inline-start edge (default).
        /// - `end` — anchored to the inline-end edge.
        /// - `top` — anchored to the block-start edge.
        /// - `bottom` — anchored to the block-end edge.
        /// - `relative` — rendered inline within the page flow; no modal backdrop.
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

        partial void OnOpenChanging(ref bool newValue);
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

        partial void OnKeepOpenOnEscapeChanging(ref bool newValue);
        /// <summary>
        /// Determines whether the drawer should remain open when the Escape key is pressed.
        /// This attribute is only applicable when the drawer is in a non-relative position,
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
        private string _label;

        partial void OnLabelChanging(ref string newValue);
        [Parameter]
        public string Label
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

        partial void FindByNameNavDrawer(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameNavDrawer(name, ref item);
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
        /// Opens the drawer. Returns `true` if the operation was successful, `false` if the drawer was already open.
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
        /// Closes the drawer. Returns `true` if the operation was successful, `false` if the drawer was already closed.
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
        /// Toggles the open state of the drawer. Delegates to `show()` or `hide()` depending on the current state.
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

        partial void SerializeCoreIgbNavDrawer(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbNavDrawer(ser);

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
