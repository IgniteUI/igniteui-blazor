namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbCombo{TValue, TItem}.Closing"/> and <see cref="IgbCombo{TValue, TItem}.Closed"/> events.
    /// </summary>
    /// <remarks>
    /// Derives from <see cref="IgbVoidEventArgs"/>, so handler methods written against <see cref="IgbVoidEventArgs"/> still bind.
    /// </remarks>
    public partial class IgbComboClosingEventArgs : IgbVoidEventArgs
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboClosingEventArgs"; } }

        private IgbComboClosingEventArgsDetail _detail = new IgbComboClosingEventArgsDetail();

        /// <summary>
        /// Describes the close: the user action that closes the list.
        /// </summary>
        public IgbComboClosingEventArgsDetail Detail
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

            if (args != null && args.TryGetValue("detail", out var detailValue) && ConvertReturnValue(detailValue, "ComboClosingEventArgsDetail", true) is IgbComboClosingEventArgsDetail detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
