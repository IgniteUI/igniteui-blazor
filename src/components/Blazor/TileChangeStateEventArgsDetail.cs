using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload carried by the tile state events, identifying the tile and the state it is changing to.
    /// </summary>
    public partial class IgbTileChangeStateEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTileChangeStateEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private IgbTile _tile;

        /// <summary>
        /// The tile whose state is changing.
        /// </summary>
        [Parameter]
        public IgbTile Tile
        {
            get { return this._tile; }
            set
            {
                if (this._tile != value || !IsPropDirty("Tile"))
                {
                    MarkPropDirty("Tile");
                }
                this._tile = value;

            }
        }
        private bool _state = false;

        /// <summary>
        /// The state the tile is changing to; <see langword="true"/> when it is being maximized or
        /// put in fullscreen, and <see langword="false"/> when it is being restored.
        /// </summary>
        [Parameter]
        public bool State
        {
            get { return this._state; }
            set
            {
                if (this._state != value || !IsPropDirty("State"))
                {
                    MarkPropDirty("State");
                }
                this._state = value;

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

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Tile"))
            { ser.AddSerializableProp("tile", this._tile); }
            if (IsPropDirty("State"))
            { ser.AddBooleanProp("state", this._state); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Tile"))
            { args["tile"] = ObjectToParam(this._tile); }
            if (IsPropDirty("State"))
            { args["state"] = (this._state).ToString().ToLower(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("tile"))
            { this.Tile = (IgbTile)ConvertReturnValue(args["tile"], "Tile", true); }
            if (args.ContainsKey("state"))
            { this.State = ReturnToBoolean(args["state"]); }

            this.SuppressParentNotify = false;
        }

    }
}
