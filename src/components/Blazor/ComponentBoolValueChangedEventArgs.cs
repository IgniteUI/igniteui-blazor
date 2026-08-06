using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry a Boolean payload.
    /// The meaning of <see cref="Detail"/> depends on the event that raises it.
    /// </summary>
    public partial class IgbComponentBoolValueChangedEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebComponentBoolValueChangedEventArgs"; } }

        private static bool _marshalByValue = true;

        private bool _detail = false;

        partial void OnDetailChanging(ref bool newValue);

        /// <summary>
        /// The Boolean value carried by the event.
        /// </summary>
        [Parameter]
        public bool Detail
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

        partial void FindByNameComponentBoolValueChangedEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameComponentBoolValueChangedEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbComponentBoolValueChangedEventArgs(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbComponentBoolValueChangedEventArgs(ser);

            if (IsPropDirty("Detail"))
            { ser.AddBooleanProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = (this._detail).ToString().ToLower(); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = ReturnToBoolean(args["detail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
