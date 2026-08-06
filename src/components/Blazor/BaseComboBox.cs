using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbCombo{T}"/> and <see cref="IgbComboBoxBaseLike"/>.
    /// </summary>
    public partial class IgbBaseComboBox : BaseRendererControl
    {
        public override string Type { get { return "WebBaseComboBox"; } }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Queued; }
        }

        private bool _open = false;

        /// <summary>
        /// Sets the open state of the component.
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
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Shows the component.
        /// </summary>
        /// <returns><see langword="true"/> when the component was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public async Task<bool> ShowAsync()
        {
            var iv = await InvokeMethod("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Shows the component.
        /// </summary>
        /// <returns><see langword="true"/> when the component was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public bool Show()
        {
            var iv = InvokeMethodSync("show", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Hides the component.
        /// </summary>
        /// <returns><see langword="true"/> when the component was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public async Task<bool> HideAsync()
        {
            var iv = await InvokeMethod("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Hides the component.
        /// </summary>
        /// <returns><see langword="true"/> when the component was successfully closed,
        /// or <see langword="false"/> if it was already closed.</returns>
        public bool Hide()
        {
            var iv = InvokeMethodSync("hide", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the open state of the component.
        /// </summary>
        /// <returns><see langword="true"/> when the open state was changed.</returns>
        public async Task<bool> ToggleAsync()
        {
            var iv = await InvokeMethod("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Toggles the open state of the component.
        /// </summary>
        /// <returns><see langword="true"/> when the open state was changed.</returns>
        public bool Toggle()
        {
            var iv = InvokeMethodSync("toggle", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Open"))
            { ser.AddBooleanProp("open", this._open); }

        }

    }
}
