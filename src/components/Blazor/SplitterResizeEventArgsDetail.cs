using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbSplitter.ResizeStart"/>, <see cref="IgbSplitter.Resizing"/>
    /// and <see cref="IgbSplitter.ResizeEnd"/> events.
    /// </summary>
    public partial class IgbSplitterResizeEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSplitterResizeEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private double _startPanelSize = 0;

        /// <summary>
        /// The current size of the start panel in pixels.
        /// </summary>
        [Parameter]
        public double StartPanelSize
        {
            get { return this._startPanelSize; }
            set
            {
                if (this._startPanelSize != value || !IsPropDirty("StartPanelSize"))
                {
                    MarkPropDirty("StartPanelSize");
                }
                this._startPanelSize = value;

            }
        }
        private double _endPanelSize = 0;

        /// <summary>
        /// The current size of the end panel in pixels.
        /// </summary>
        [Parameter]
        public double EndPanelSize
        {
            get { return this._endPanelSize; }
            set
            {
                if (this._endPanelSize != value || !IsPropDirty("EndPanelSize"))
                {
                    MarkPropDirty("EndPanelSize");
                }
                this._endPanelSize = value;

            }
        }
        private double _delta = 0;

        /// <summary>
        /// The change in size since the resize operation started. Only set for
        /// <see cref="IgbSplitter.Resizing"/> and <see cref="IgbSplitter.ResizeEnd"/>.
        /// </summary>
        [Parameter]
        public double Delta
        {
            get { return this._delta; }
            set
            {
                if (this._delta != value || !IsPropDirty("Delta"))
                {
                    MarkPropDirty("Delta");
                }
                this._delta = value;

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

            if (IsPropDirty("StartPanelSize"))
            { ser.AddNumberProp("startPanelSize", this._startPanelSize); }
            if (IsPropDirty("EndPanelSize"))
            { ser.AddNumberProp("endPanelSize", this._endPanelSize); }
            if (IsPropDirty("Delta"))
            { ser.AddNumberProp("delta", this._delta); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("StartPanelSize"))
            { args["startPanelSize"] = (this._startPanelSize).ToString(); }
            if (IsPropDirty("EndPanelSize"))
            { args["endPanelSize"] = (this._endPanelSize).ToString(); }
            if (IsPropDirty("Delta"))
            { args["delta"] = (this._delta).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.ContainsKey("startPanelSize"))
            { this.StartPanelSize = ReturnToDouble(args["startPanelSize"]); }
            if (args != null && args.ContainsKey("endPanelSize"))
            { this.EndPanelSize = ReturnToDouble(args["endPanelSize"]); }
            if (args != null && args.ContainsKey("delta"))
            { this.Delta = ReturnToDouble(args["delta"]); }

            this.SuppressParentNotify = false;
        }

    }
}
