using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events whose payload is a single number.
    /// </summary>
    public partial class IgbNumberEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebNumberEventArgs"; } }

        private static bool _marshalByValue = true;

        private double _detail = 0;

        /// <summary>
        /// The numeric payload of the event. Its meaning depends on the event that carries it, for
        /// example the new value of the control or the index of the affected item.
        /// </summary>
        [Parameter]
        public double Detail
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
            { ser.AddNumberProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = (this._detail).ToString(); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = ReturnToDouble(args["detail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
