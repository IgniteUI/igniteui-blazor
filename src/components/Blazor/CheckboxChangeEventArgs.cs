using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <c>Change</c> event of <see cref="IgbCheckbox"/>
    /// and <see cref="IgbSwitch"/>, raised when the checked state of the control changes.
    /// </summary>
    public partial class IgbCheckboxChangeEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCheckboxChangeEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbCheckboxChangeEventArgsDetail? _detail;

        /// <summary>
        /// The payload of the event, carrying the new checked state and the value of the control.
        /// </summary>
        [Parameter]
        public IgbCheckboxChangeEventArgsDetail? Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._detail = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbCheckboxChangeEventArgsDetail)ConvertReturnValue(args["detail"], "CheckboxChangeEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
