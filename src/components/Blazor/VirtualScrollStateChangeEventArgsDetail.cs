namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbVirtualScroll.StateChange"/> event:
    /// a snapshot of the currently rendered virtual window.
    /// </summary>
    public partial class IgbVirtualScrollStateChangeEventArgsDetail : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebVirtualScrollStateChangeEventArgsDetail"; } }

        private double _startIndex = 0;

        /// <summary>
        /// The index of the first item currently rendered in the viewport.
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
        private double _endIndex = 0;

        /// <summary>
        /// The index of the last item currently rendered in the viewport (inclusive).
        /// </summary>
        public double EndIndex
        {
            get { return this._endIndex; }
            set
            {
                if (this._endIndex != value || !IsPropDirty("EndIndex"))
                {
                    MarkPropDirty("EndIndex");
                }
                this._endIndex = value;

            }
        }
        private double _viewportSize = 0;

        /// <summary>
        /// The size of the viewport in pixels.
        /// </summary>
        public double ViewportSize
        {
            get { return this._viewportSize; }
            set
            {
                if (this._viewportSize != value || !IsPropDirty("ViewportSize"))
                {
                    MarkPropDirty("ViewportSize");
                }
                this._viewportSize = value;

            }
        }
        private double _totalSize = 0;

        /// <summary>
        /// The total size of the virtual scroll content in pixels.
        /// </summary>
        public double TotalSize
        {
            get { return this._totalSize; }
            set
            {
                if (this._totalSize != value || !IsPropDirty("TotalSize"))
                {
                    MarkPropDirty("TotalSize");
                }
                this._totalSize = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("StartIndex"))
            { ser.AddNumberProp("startIndex", this._startIndex); }
            if (IsPropDirty("EndIndex"))
            { ser.AddNumberProp("endIndex", this._endIndex); }
            if (IsPropDirty("ViewportSize"))
            { ser.AddNumberProp("viewportSize", this._viewportSize); }
            if (IsPropDirty("TotalSize"))
            { ser.AddNumberProp("totalSize", this._totalSize); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("StartIndex"))
            { args["startIndex"] = (this._startIndex).ToString(); }
            if (IsPropDirty("EndIndex"))
            { args["endIndex"] = (this._endIndex).ToString(); }
            if (IsPropDirty("ViewportSize"))
            { args["viewportSize"] = (this._viewportSize).ToString(); }
            if (IsPropDirty("TotalSize"))
            { args["totalSize"] = (this._totalSize).ToString(); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("startIndex", out var startIndex))
            { this.StartIndex = ReturnToDouble(startIndex); }
            if (args != null && args.TryGetValue("endIndex", out var endIndex))
            { this.EndIndex = ReturnToDouble(endIndex); }
            if (args != null && args.TryGetValue("viewportSize", out var viewportSize))
            { this.ViewportSize = ReturnToDouble(viewportSize); }
            if (args != null && args.TryGetValue("totalSize", out var totalSize))
            { this.TotalSize = ReturnToDouble(totalSize); }

            this.SuppressParentNotify = false;
        }

    }
}
