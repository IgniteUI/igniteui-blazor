namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.Change"/> event, without the combo's type arguments.
    /// </summary>
    [Obsolete("Use IgbComboChangeEventArgs<TValue, TItem>, with the TValue and TItem of the combo.")]
    public abstract partial class IgbComboChangeEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboChangeEventArgs"; } }

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        public abstract IgbComboChangeEventArgsDetail Detail { get; }
    }

#pragma warning disable CS0618 // The obsolete base keeps handlers written against IgbComboChangeEventArgs binding to Change.
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.Change"/> event.
    /// </summary>
    public partial class IgbComboChangeEventArgs<TValue, TItem> : IgbComboChangeEventArgs
#pragma warning restore CS0618
    {
        private IgbComboChangeEventArgsDetail<TValue, TItem> _detail = new();

        /// <summary>
        /// Describes the selection change: the new value, the items it affected and the kind of change.
        /// </summary>
        public override IgbComboChangeEventArgsDetail<TValue, TItem> Detail
        {
            get { return this._detail; }
        }

        private void SetDetail(IgbComboChangeEventArgsDetail<TValue, TItem> value)
        {
            MarkPropDirty("Detail");
            this.DetachChild(this._detail);
            this._detail = value;
            this.AttachChild(value);
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue<TValue, TItem>(detailObj, "ComboChangeEventArgsDetail", true) is IgbComboChangeEventArgsDetail<TValue, TItem> detail)
            { this.SetDetail(detail); }

            this.SuppressParentNotify = false;
        }

    }
}
