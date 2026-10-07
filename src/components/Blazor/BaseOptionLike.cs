using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbDropdownItem"/> and <see cref="IgbSelectItem{TValue}"/>.
    /// </summary>
    public partial class IgbBaseOptionLike<TValue> : BaseRendererControl
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebBaseOptionLike"; } }

        /// <inheritdoc />
        private protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        private protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override string DirectRenderElementName
        {
            get
            {
                return "igc-base-option-like";
            }
        }

        /// <inheritdoc />
        private protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _active = false;

        /// <summary>
        /// Whether the item is active.
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
        private bool _disabled = false;

        /// <summary>
        /// Whether the item is disabled.
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
        private bool _selected = false;

        /// <summary>
        /// Whether the item is selected.
        /// </summary>
        [Parameter]
        public bool Selected
        {
            get { return this._selected; }
            set
            {
                if (this._selected != value || !IsPropDirty("Selected"))
                {
                    MarkPropDirty("Selected");
                }
                this._selected = value;

            }
        }
        private TValue? _value;

        /// <summary>
        /// The current value of the item.
        /// If not specified, the text content of the item is used.
        /// </summary>
        [Parameter]
        public TValue? Value
        {
            get { return this._value; }
            set
            {
                if (!EqualityComparer<TValue?>.Default.Equals(this._value, value) || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Active"))
            { ser.AddBooleanProp("active", this._active); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Selected"))
            { ser.AddBooleanProp("selected", this._selected); }
            if (IsPropDirty("Value"))
            { ser.AddPrimitiveProp("value", this._value); }

        }

    }
}
