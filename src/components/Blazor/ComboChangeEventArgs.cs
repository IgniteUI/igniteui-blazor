using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.Change"/> event.
    /// </summary>
    public partial class IgbComboChangeEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboChangeEventArgs"; } }

        private IgbComboChangeEventArgsDetail<object, object> _detail = new();

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        [Parameter]
        public IgbComboChangeEventArgsDetail<object, object> Detail
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

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<object>(detailObj, "ComboChangeEventArgsDetail", true) is IgbComboChangeEventArgsDetail<object, object> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }
    }

    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.Change"/> event.
    /// </summary>
    public partial class IgbComboChangeEventArgs<TValue, TItem> : IgbComboChangeEventArgs
    {
        private IgbComboChangeEventArgsDetail<TValue, TItem> _detail = new();

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        [Parameter]
        public new IgbComboChangeEventArgsDetail<TValue, TItem> Detail
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

        /// <inheritdoc />
        internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<TValue, TItem>(detailObj, "ComboChangeEventArgsDetail", true) is IgbComboChangeEventArgsDetail<TValue, TItem> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
