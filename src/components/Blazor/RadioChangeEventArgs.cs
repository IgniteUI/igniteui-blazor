using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbRadio{TValue}.Change"/> and <see cref="IgbRadioGroup{TValue}.Change"/>
    /// events, raised when the checked state of a radio button changes.
    /// </summary>
    public partial class IgbRadioChangeEventArgs<TValue> : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebRadioChangeEventArgs"; } }

        private IgbRadioChangeEventArgsDetail<TValue> _detail = new IgbRadioChangeEventArgsDetail<TValue>();

        /// <summary>
        /// The payload of the event, carrying the new checked state and the value of the radio button.
        /// </summary>
        [Parameter]
        public IgbRadioChangeEventArgsDetail<TValue> Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                this._detail = value;
                if (value != null)
                {
                    this.AttachChild(value);
                }
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<TValue>(detailObj, "RadioChangeEventArgsDetail", true) is IgbRadioChangeEventArgsDetail<TValue> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
