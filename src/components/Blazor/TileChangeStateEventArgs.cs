using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the tile state events raised by <see cref="IgbTile"/> and
    /// <see cref="IgbTileManager"/>, such as <see cref="IgbTile.TileFullscreen"/> and
    /// <see cref="IgbTile.TileMaximize"/>.
    /// </summary>
    public partial class IgbTileChangeStateEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTileChangeStateEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbTileChangeStateEventArgsDetail _detail;

        /// <summary>
        /// The affected tile and the state it is changing to.
        /// </summary>
        [Parameter]
        public IgbTileChangeStateEventArgsDetail Detail
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
            { this.Detail = (IgbTileChangeStateEventArgsDetail)ConvertReturnValue(args["detail"], "TileChangeStateEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
