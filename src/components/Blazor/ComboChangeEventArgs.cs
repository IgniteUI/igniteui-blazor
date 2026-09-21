using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{T}.Change"/> event.
    /// </summary>
    public partial class IgbComboChangeEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebComboChangeEventArgs"; } }

        private IgbComboChangeEventArgsDetail<object> _detail = new();

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        [Parameter]
        public IgbComboChangeEventArgsDetail<object> Detail
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

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<object>(detailObj, "ComboChangeEventArgsDetail", true) is IgbComboChangeEventArgsDetail<object> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }
    }

    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{T}.Change"/> event.
    /// </summary>
    public partial class IgbComboChangeEventArgs<T> : IgbComboChangeEventArgs
    {
        private IgbComboChangeEventArgsDetail<T> _detail = new();

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        [Parameter]
        public new IgbComboChangeEventArgsDetail<T> Detail
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
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<T>(detailObj, "ComboChangeEventArgsDetail", true) is IgbComboChangeEventArgsDetail<T> detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
