namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.SearchInput"/> event.
    /// </summary>
    public partial class IgbComboSearchInputEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboSearchInputEventArgs"; } }

        private IgbComboSearchInputEventArgsDetail _detail = new IgbComboSearchInputEventArgsDetail();

        /// <summary>
        /// Describes the search: the text of the search input.
        /// </summary>
        public IgbComboSearchInputEventArgsDetail Detail
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

            if (args != null && args.TryGetValue("detail", out var detailValue) && ConvertReturnValue(detailValue, "ComboSearchInputEventArgsDetail", true) is IgbComboSearchInputEventArgsDetail detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
