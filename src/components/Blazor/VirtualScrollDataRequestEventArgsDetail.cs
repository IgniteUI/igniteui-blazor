using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbVirtualScroll.DataRequest"/> event:
    /// a request for more data, emitted when the rendered window comes near the
    /// end of the loaded items.
    /// </summary>
    public partial class IgbVirtualScrollDataRequestEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebVirtualScrollDataRequestEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private double _startIndex = 0;

        /// <summary>
        /// The first index that does not yet have data.
        /// Append at least <see cref="Count"/> more items starting here.
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
        private double _count = 0;

        /// <summary>
        /// Number of items being requested.
        /// </summary>
        [Parameter]
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
            if (IsPropDirty("Count"))
            { ser.AddNumberProp("count", this._count); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("StartIndex"))
            { args["startIndex"] = (this._startIndex).ToString(); }
            if (IsPropDirty("Count"))
            { args["count"] = (this._count).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("startIndex"))
            { this.StartIndex = ReturnToDouble(args["startIndex"]); }
            if (args.ContainsKey("count"))
            { this.Count = ReturnToDouble(args["count"]); }

            this.SuppressParentNotify = false;
        }

    }
}
