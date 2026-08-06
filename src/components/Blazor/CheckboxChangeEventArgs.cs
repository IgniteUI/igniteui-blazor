using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <c>Change</c> event of <see cref="IgbCheckbox"/>
    /// and <see cref="IgbSwitch"/>, raised when the checked state of the control changes.
    /// </summary>
    public partial class IgbCheckboxChangeEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebCheckboxChangeEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbCheckboxChangeEventArgsDetail _detail;

        partial void OnDetailChanging(ref IgbCheckboxChangeEventArgsDetail newValue);
        /// <summary>
        /// The payload of the event, carrying the new checked state and the value of the control.
        /// </summary>
        [Parameter]
        public IgbCheckboxChangeEventArgsDetail Detail
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

        partial void FindByNameCheckboxChangeEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCheckboxChangeEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbCheckboxChangeEventArgs(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbCheckboxChangeEventArgs(ser);

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
            { this.Detail = (IgbCheckboxChangeEventArgsDetail)ConvertReturnValue(args["detail"], "CheckboxChangeEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
