using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbSnackbar"/> and <see cref="IgbToast"/>.
    /// </summary>
    public partial class IgbBaseAlertLike : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebBaseAlertLike"; } }

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
                return "igc-base-alert-like";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _open = false;

        /// <summary>
        /// Whether the component is in shown state.
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
        private double _displayTime = 4000;

        /// <summary>
        /// Determines the duration in milliseconds in which the component will be visible.
        /// </summary>
        [Parameter]
        public double DisplayTime
        {
            get { return this._displayTime; }
            set
            {
                if (this._displayTime != value || !IsPropDirty("DisplayTime"))
                {
                    MarkPropDirty("DisplayTime");
                }
                this._displayTime = value;

            }
        }
        private bool _keepOpen = false;

        /// <summary>
        /// Determines whether the component should close after the <see cref="DisplayTime"/> is over.
        /// </summary>
        [Parameter]
        public bool KeepOpen
        {
            get { return this._keepOpen; }
            set
            {
                if (this._keepOpen != value || !IsPropDirty("KeepOpen"))
                {
                    MarkPropDirty("KeepOpen");
                }
                this._keepOpen = value;

            }
        }
        private AbsolutePosition _position = AbsolutePosition.Bottom;

        /// <summary>
        /// Sets the position of the component in the viewport.
        /// <list type="bullet">
        ///   <item><description>
        ///     <see cref="AbsolutePosition.Bottom"/> - positions the component at the bottom.
        ///     This is the default.
        ///   </description></item>
        ///   <item><description>
        ///     <see cref="AbsolutePosition.Middle"/> - positions the component at the center.
        ///   </description></item>
        ///   <item><description>
        ///     <see cref="AbsolutePosition.Top"/> - positions the component at the top.
        ///   </description></item>
        /// </list>
        /// </summary>
        [Parameter]
        public AbsolutePosition Position
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
        private NotificationPositioning _positioning = NotificationPositioning.Viewport;

        /// <summary>
        /// Sets the positioning strategy of the component.
        /// <list type="bullet">
        ///   <item><description>
        ///     <see cref="NotificationPositioning.Viewport"/> - positions the component relative to the
        ///     viewport, ignoring any ancestor elements. This is the default behavior.
        ///   </description></item>
        ///   <item><description>
        ///     <see cref="NotificationPositioning.Container"/> - positions the component relative to the
        ///     nearest visible ancestor. In this mode, the component will be constrained within the bounding
        ///     box of the ancestor and will be positioned according to the <see cref="Position"/> property.
        ///   </description></item>
        /// </list>
        /// </summary>
        [Parameter]
        public NotificationPositioning Positioning
        {
            get { return this._positioning; }
            set
            {
                if (this._positioning != value || !IsPropDirty("Positioning"))
                {
                    MarkPropDirty("Positioning");
                }
                this._positioning = value;

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
        public async Task ConnectedCallbackAsync()
        {
            await InvokeMethod("connectedCallback", new object?[] { }, new string[] { });
        }
        public void ConnectedCallback()
        {
            InvokeMethodSync("connectedCallback", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Opens the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the component was successfully opened, or <see langword="false"/>
        /// if it was already open or could not be shown (for example, in
        /// <see cref="NotificationPositioning.Container"/> positioning mode with no visible ancestors).
        /// </returns>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Opens the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the component was successfully opened, or <see langword="false"/>
        /// if it was already open or could not be shown (for example, in
        /// <see cref="NotificationPositioning.Container"/> positioning mode with no visible ancestors).
        /// </returns>
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Closes the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the component was successfully closed, or <see langword="false"/>
        /// if it was already closed.
        /// </returns>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Closes the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the component was successfully closed, or <see langword="false"/>
        /// if it was already closed.
        /// </returns>
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the open state of the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the operation completed successfully, or <see langword="false"/>
        /// if it was already in the desired state.
        /// </returns>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Toggles the open state of the component.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when the operation completed successfully, or <see langword="false"/>
        /// if it was already in the desired state.
        /// </returns>
        public bool Toggle()
        {
            var iv = InvokeMethodSync("toggle", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }
            if (IsPropDirty("DisplayTime"))
            { ser.AddNumberProp("displayTime", this._displayTime); }
            if (IsPropDirty("KeepOpen"))
            { ser.AddBooleanProp("keepOpen", this._keepOpen); }
            if (IsPropDirty("Position"))
            { ser.AddEnumProp("position", this._position); }
            if (IsPropDirty("Positioning"))
            { ser.AddEnumProp("positioning", this._positioning); }

        }

    }
}
