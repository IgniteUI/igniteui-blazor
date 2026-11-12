namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbVirtualScroll.DataRequest"/> event:
    /// a request for more data, emitted when the rendered window comes near the
    /// end of the loaded items.
    /// </summary>
    public partial class IgbVirtualScrollDataRequestEventArgsDetail : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebVirtualScrollDataRequestEventArgsDetail"; } }

        private double _startIndex = 0;

        /// <summary>
        /// The first index that does not yet have data.
        /// Append at least <see cref="Count"/> more items starting here.
        /// </summary>
        public double StartIndex
        {
            get { return this._startIndex; }
            set
            {
                if (this._startIndex != value || !IsPropDirty("StartIndex"))
                {
                    MarkPropDirty("StartIndex");
                }
                this._startIndex = value;

            }
        }
        private double _count = 0;

        /// <summary>
        /// Number of items being requested.
        /// </summary>
        public double Count
        {
            get { return this._count; }
            set
            {
                if (this._count != value || !IsPropDirty("Count"))
                {
                    MarkPropDirty("Count");
                }
                this._count = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("StartIndex"))
            { ser.AddNumberProp("startIndex", this._startIndex); }
            if (IsPropDirty("Count"))
            { ser.AddNumberProp("count", this._count); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("StartIndex"))
            { args["startIndex"] = (this._startIndex).ToString(); }
            if (IsPropDirty("Count"))
            { args["count"] = (this._count).ToString(); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("startIndex", out var startIndex))
            { this.StartIndex = ReturnToDouble(startIndex); }
            if (args != null && args.TryGetValue("count", out var count))
            { this.Count = ReturnToDouble(count); }

            this.SuppressParentNotify = false;
        }

    }
}
