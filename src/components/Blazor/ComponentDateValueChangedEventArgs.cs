using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry a date payload.
    /// The meaning of <see cref="Detail"/> depends on the event that raises it.
    /// </summary>
    public partial class IgbComponentDateValueChangedEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebComponentDateValueChangedEventArgs"; } }

        private static bool _marshalByValue = true;

        private DateTime _detail = DateTime.MinValue;

        /// <summary>
        /// The date value carried by the event.
        /// </summary>
        [Parameter]
        public DateTime Detail
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
            { ser.AddDateTimeProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = DateToString(this._detail); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = ReturnToDate(args["detail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
