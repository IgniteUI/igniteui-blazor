using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbDatePicker"/>, <see cref="IgbDateRangePicker"/>, <see cref="IgbDropdown"/>
    /// and <see cref="IgbSelect"/>.
    /// </summary>
    public partial class IgbComboBoxBaseLike : IgbBaseComboBox
    {
        /// <inheritdoc />
        public override string Type { get { return "WebComboBoxBaseLike"; } }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        private bool _keepOpenOnSelect = false;

        /// <summary>
        /// Whether the component dropdown should be kept open on selection.
        /// </summary>
        [Parameter]
        public bool KeepOpenOnSelect
        {
            get { return this._keepOpenOnSelect; }
            set
            {
                if (this._keepOpenOnSelect != value || !IsPropDirty("KeepOpenOnSelect"))
                {
                    MarkPropDirty("KeepOpenOnSelect");
                }
                this._keepOpenOnSelect = value;

            }
        }
        private bool _keepOpenOnOutsideClick = false;

        /// <summary>
        /// Whether the component dropdown should be kept open on clicking outside of it.
        /// </summary>
        [Parameter]
        public bool KeepOpenOnOutsideClick
        {
            get { return this._keepOpenOnOutsideClick; }
            set
            {
                if (this._keepOpenOnOutsideClick != value || !IsPropDirty("KeepOpenOnOutsideClick"))
                {
                    MarkPropDirty("KeepOpenOnOutsideClick");
                }
                this._keepOpenOnOutsideClick = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("KeepOpenOnSelect"))
            { ser.AddBooleanProp("keepOpenOnSelect", this._keepOpenOnSelect); }
            if (IsPropDirty("KeepOpenOnOutsideClick"))
            { ser.AddBooleanProp("keepOpenOnOutsideClick", this._keepOpenOnOutsideClick); }

        }

    }
}
