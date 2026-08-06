using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry an arbitrary data payload.
    /// The type and meaning of <see cref="Detail"/> depend on the event that raises it.
    /// </summary>
    public partial class IgbComponentDataValueChangedEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebComponentDataValueChangedEventArgs"; } }

        private object _detail;

        /// <summary>
        /// The value carried by the event.
        /// </summary>
        [Parameter]
        public object Detail
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

        partial void FindByNameComponentDataValueChangedEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameComponentDataValueChangedEventArgs(name, ref item);
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
            { ser.AddPrimitiveProp("detail", this._detail); }

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
            { this.Detail = ReturnToPrimitive(args["detail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
