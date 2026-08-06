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
        public override string Type { get { return "WebTileChangeStateEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbTileChangeStateEventArgsDetail _detail;

        partial void OnDetailChanging(ref IgbTileChangeStateEventArgsDetail newValue);

        /// <summary>
        /// The affected tile and the state it is changing to.
        /// </summary>
        [Parameter]
        public IgbTileChangeStateEventArgsDetail Detail
        {
            get { return this._detail; }
            set
            {
                OnDetailChanging(ref value);
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

        partial void FindByNameTileChangeStateEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameTileChangeStateEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

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
