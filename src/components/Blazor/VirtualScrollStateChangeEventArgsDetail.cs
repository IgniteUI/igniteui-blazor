using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbVirtualScroll.StateChange"/> event:
    /// a snapshot of the currently rendered virtual window.
    /// </summary>
    public partial class IgbVirtualScrollStateChangeEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebVirtualScrollStateChangeEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private double _startIndex = 0;

        /// <summary>
        /// The index of the first item currently rendered in the viewport.
        /// </summary>
        [Parameter]
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
        [Parameter]
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
        [Parameter]
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
        [Parameter]
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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
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
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
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
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("startIndex"))
            { this.StartIndex = ReturnToDouble(args["startIndex"]); }
            if (args.ContainsKey("endIndex"))
            { this.EndIndex = ReturnToDouble(args["endIndex"]); }
            if (args.ContainsKey("viewportSize"))
            { this.ViewportSize = ReturnToDouble(args["viewportSize"]); }
            if (args.ContainsKey("totalSize"))
            { this.TotalSize = ReturnToDouble(args["totalSize"]); }

            this.SuppressParentNotify = false;
        }

    }
}
