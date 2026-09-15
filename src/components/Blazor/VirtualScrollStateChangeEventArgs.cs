using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbVirtualScroll.StateChange"/> event.
    /// </summary>
    public partial class IgbVirtualScrollStateChangeEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebVirtualScrollStateChangeEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbVirtualScrollStateChangeEventArgsDetail _detail;

        /// <summary>
        /// A snapshot of the currently rendered virtual window.
        /// </summary>
        [Parameter]
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
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._detail = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbVirtualScrollStateChangeEventArgsDetail)ConvertReturnValue(args["detail"], "VirtualScrollStateChangeEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
