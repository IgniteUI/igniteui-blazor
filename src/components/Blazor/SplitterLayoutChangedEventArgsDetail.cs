using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbSplitter.LayoutChanged"/> event:
    /// a full snapshot of the current layout (pane sizes and collapsed states).
    /// </summary>
    public partial class IgbSplitterLayoutChangedEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSplitterLayoutChangedEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private string _startSize;

        /// <summary>
        /// The current size of the start pane.
        /// </summary>
        [Parameter]
        public string StartSize
        {
            get { return this._startSize; }
            set
            {
                if (this._startSize != value || !IsPropDirty("StartSize"))
                {
                    MarkPropDirty("StartSize");
                }
                this._startSize = value;

            }
        }
        private string _endSize;

        /// <summary>
        /// The current size of the end pane.
        /// </summary>
        [Parameter]
        public string EndSize
        {
            get { return this._endSize; }
            set
            {
                if (this._endSize != value || !IsPropDirty("EndSize"))
                {
                    MarkPropDirty("EndSize");
                }
                this._endSize = value;

            }
        }
        private bool _startCollapsed = false;

        /// <summary>
        /// Whether the start pane is currently collapsed.
        /// </summary>
        [Parameter]
        public bool StartCollapsed
        {
            get { return this._startCollapsed; }
            set
            {
                if (this._startCollapsed != value || !IsPropDirty("StartCollapsed"))
                {
                    MarkPropDirty("StartCollapsed");
                }
                this._startCollapsed = value;

            }
        }
        private bool _endCollapsed = false;

        /// <summary>
        /// Whether the end pane is currently collapsed.
        /// </summary>
        [Parameter]
        public bool EndCollapsed
        {
            get { return this._endCollapsed; }
            set
            {
                if (this._endCollapsed != value || !IsPropDirty("EndCollapsed"))
                {
                    MarkPropDirty("EndCollapsed");
                }
                this._endCollapsed = value;

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

            if (IsPropDirty("StartSize"))
            { ser.AddStringProp("startSize", this._startSize); }
            if (IsPropDirty("EndSize"))
            { ser.AddStringProp("endSize", this._endSize); }
            if (IsPropDirty("StartCollapsed"))
            { ser.AddBooleanProp("startCollapsed", this._startCollapsed); }
            if (IsPropDirty("EndCollapsed"))
            { ser.AddBooleanProp("endCollapsed", this._endCollapsed); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("StartSize"))
            { args["startSize"] = this._startSize; }
            if (IsPropDirty("EndSize"))
            { args["endSize"] = this._endSize; }
            if (IsPropDirty("StartCollapsed"))
            { args["startCollapsed"] = (this._startCollapsed).ToString().ToLower(); }
            if (IsPropDirty("EndCollapsed"))
            { args["endCollapsed"] = (this._endCollapsed).ToString().ToLower(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("startSize"))
            { this.StartSize = ReturnToString(args["startSize"]); }
            if (args.ContainsKey("endSize"))
            { this.EndSize = ReturnToString(args["endSize"]); }
            if (args.ContainsKey("startCollapsed"))
            { this.StartCollapsed = ReturnToBoolean(args["startCollapsed"]); }
            if (args.ContainsKey("endCollapsed"))
            { this.EndCollapsed = ReturnToBoolean(args["endCollapsed"]); }

            this.SuppressParentNotify = false;
        }

    }
}
