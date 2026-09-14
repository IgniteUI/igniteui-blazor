using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container for a group of select items.
    /// </summary>
    public partial class IgbSelectGroup : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSelectGroup"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSelectModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectModule.Register(IgBlazor);
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
                return "igc-select-group";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private IgbSelectItem[] _items = Array.Empty<IgbSelectItem>();

        /// <summary>
        /// All child <see cref="IgbSelectItem"/> components.
        /// </summary>
        [Parameter]
        public IgbSelectItem[] Items
        {
            get { return this._items; }
            set
            {
                if (this._items != value || !IsPropDirty("Items"))
                {
                    MarkPropDirty("Items");
                }
                this._items = value;

            }
        }
        private bool _disabled = false;

        /// <summary>
        /// Whether the group item and all its children are disabled.
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


        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Items"))
            { ser.AddSerializableArrayProp("items", this._items); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }

        }

    }
}
