namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbVirtualScroll.DataRequest"/> event.
    /// </summary>
    public partial class IgbVirtualScrollDataRequestEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebVirtualScrollDataRequestEventArgs"; } }

        private IgbVirtualScrollDataRequestEventArgsDetail _detail = new IgbVirtualScrollDataRequestEventArgsDetail();

        /// <summary>
        /// The requested range of items to append to the data source.
        /// </summary>
        public IgbVirtualScrollDataRequestEventArgsDetail Detail
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

            if (args != null && args.TryGetValue("detail", out var detailValue) && ConvertReturnValue(detailValue, "VirtualScrollDataRequestEventArgsDetail", true) is IgbVirtualScrollDataRequestEventArgsDetail detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
