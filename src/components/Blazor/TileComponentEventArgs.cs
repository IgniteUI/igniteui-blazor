using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the tile drag and resize events raised by <see cref="IgbTile"/> and
    /// <see cref="IgbTileManager"/>, such as <see cref="IgbTile.TileDragStart"/> and
    /// <see cref="IgbTile.TileResizeEnd"/>.
    /// </summary>
    public partial class IgbTileComponentEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTileComponentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbTile? _detail;

        /// <summary>
        /// The tile the operation applies to.
        /// </summary>
        [Parameter]
        public IgbTile? Detail
        {
            get { return this._detail; }
            set
            {
                if (this._detail != value || !IsPropDirty("Detail"))
                {
                    MarkPropDirty("Detail");
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

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbTile?)ConvertReturnValue(args["detail"], "Tile", true); }

            this.SuppressParentNotify = false;
        }

    }
}
