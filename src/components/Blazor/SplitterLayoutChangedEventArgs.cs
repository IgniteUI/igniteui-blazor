using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbSplitter.LayoutChanged"/> event.
    /// </summary>
    public partial class IgbSplitterLayoutChangedEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSplitterLayoutChangedEventArgs"; } }

        private IgbSplitterLayoutChangedEventArgsDetail _detail = new IgbSplitterLayoutChangedEventArgsDetail();

        /// <summary>
        /// A full snapshot of the current layout (pane sizes and collapsed states).
        /// </summary>
        [Parameter]
        public IgbSplitterLayoutChangedEventArgsDetail Detail
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
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailValue) && ConvertReturnValue(detailValue, "SplitterLayoutChangedEventArgsDetail", true) is IgbSplitterLayoutChangedEventArgsDetail detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
