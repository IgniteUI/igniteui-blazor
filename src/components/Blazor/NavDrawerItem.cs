using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a navigation drawer item.
    /// </summary>
    public partial class IgbNavDrawerItem : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebNavDrawerItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbNavDrawerItemModule.IsLoadRequested(IgBlazor))
            {
                IgbNavDrawerItemModule.Register(IgBlazor);
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
                return "igc-nav-drawer-item";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _disabled = false;

        /// <summary>
        /// Determines whether the drawer item is disabled.
        /// </summary>
        [Parameter]
        public bool Disabled
        {
            get { return this._disabled; }
            set
            {
                if (this._disabled != value || !IsPropDirty("Disabled"))
                {
                    MarkPropDirty("Disabled");
                }
                this._disabled = value;

            }
        }
        private bool _active = false;

        /// <summary>
        /// Determines whether the drawer item is active.
        /// </summary>
        [Parameter]
        public bool Active
        {
            get { return this._active; }
            set
            {
                if (this._active != value || !IsPropDirty("Active"))
                {
                    MarkPropDirty("Active");
                }
                this._active = value;

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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Active"))
            { ser.AddBooleanProp("active", this._active); }

        }

    }
}
