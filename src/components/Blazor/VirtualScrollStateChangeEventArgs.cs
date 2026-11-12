namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbVirtualScroll.StateChange"/> event.
    /// </summary>
    public partial class IgbVirtualScrollStateChangeEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebVirtualScrollStateChangeEventArgs"; } }

        private IgbVirtualScrollStateChangeEventArgsDetail _detail = new IgbVirtualScrollStateChangeEventArgsDetail();

        /// <summary>
        /// A snapshot of the currently rendered virtual window.
        /// </summary>
        public IgbVirtualScrollStateChangeEventArgsDetail Detail
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

            if (args != null && args.TryGetValue("detail", out var detailValue) && ConvertReturnValue(detailValue, "VirtualScrollStateChangeEventArgsDetail", true) is IgbVirtualScrollStateChangeEventArgsDetail detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
