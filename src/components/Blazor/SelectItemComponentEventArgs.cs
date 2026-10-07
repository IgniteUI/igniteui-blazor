using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbSelect{TValue}.Change"/> event, carrying the
    /// <see cref="IgbSelectItem{TValue}"/> instance the event applies to.
    /// </summary>
    public partial class IgbSelectItemComponentEventArgs<TValue> : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebSelectItemComponentEventArgs"; } }

        private IgbSelectItem<TValue> _detail = new IgbSelectItem<TValue>();

        /// <summary>
        /// The select item that became selected.
        /// </summary>
        [Parameter]
        public IgbSelectItem<TValue> Detail
        {
            get { return this._detail; }
            set
            {
                if (this._detail != value || !IsPropDirty("Detail"))
                {
                    MarkPropDirty("Detail");
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

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue(detailObj, "SelectItem", false, typeof(TValue)) is IgbSelectItem<TValue> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
