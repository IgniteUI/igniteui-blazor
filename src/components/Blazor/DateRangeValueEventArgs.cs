using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbDateRangePicker{TValue}"/> events that carry a date range payload.
    /// </summary>
    public partial class IgbDateRangeValueEventArgs<TValue> : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebDateRangeValueEventArgs"; } }

        private IgbDateRangeValueDetail<TValue> _detail = new();

        /// <summary>
        /// The date range carried by the event.
        /// </summary>
        [Parameter]
        public IgbDateRangeValueDetail<TValue> Detail
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

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<TValue>(detailObj, "DateRangeValueDetail", true) is IgbDateRangeValueDetail<TValue> detail)
            {
                this.Detail = detail;
            }

            this.SuppressParentNotify = false;
        }

    }
}
