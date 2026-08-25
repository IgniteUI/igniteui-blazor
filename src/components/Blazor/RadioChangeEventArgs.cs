using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbRadio.Change"/> and <see cref="IgbRadioGroup.Change"/>
    /// events, raised when the checked state of a radio button changes.
    /// </summary>
    public partial class IgbRadioChangeEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRadioChangeEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbRadioChangeEventArgsDetail _detail;

        /// <summary>
        /// The payload of the event, carrying the new checked state and the value of the radio button.
        /// </summary>
        [Parameter]
        public IgbRadioChangeEventArgsDetail Detail
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
            { this.Detail = (IgbRadioChangeEventArgsDetail)ConvertReturnValue(args["detail"], "RadioChangeEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
