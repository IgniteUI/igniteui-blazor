using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry an arbitrary data payload.
    /// The type and meaning of <see cref="Detail"/> depend on the event that raises it.
    /// </summary>
    public partial class IgbComponentDataValueChangedEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebComponentDataValueChangedEventArgs"; } }

        private object? _detail;

        /// <summary>
        /// The value carried by the event.
        /// </summary>
        [Parameter]
        public object? Detail
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
            { ser.AddPrimitiveProp("detail", this._detail); }

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

            if (args != null && args.ContainsKey("detail"))
            { this.Detail = ReturnToPrimitive(args["detail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
