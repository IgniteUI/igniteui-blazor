using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <c>Change</c> event of <see cref="IgbCheckbox"/> and <see cref="IgbSwitch"/>.
    /// </summary>
    public partial class IgbCheckboxChangeEventArgsDetail : BaseRendererElement
    {
        public override string Type { get { return "WebCheckboxChangeEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private bool _checked = false;

        partial void OnCheckedChanging(ref bool newValue);
        /// <summary>
        /// The checked state of the control after the change.
        /// </summary>
        [Parameter]
        public bool Checked
        {
            get { return this._checked; }
            set
            {
                if (this._checked != value || !IsPropDirty("Checked"))
                {
                    MarkPropDirty("Checked");
                }
                this._checked = value;

            }
        }
        private string _value;

        partial void OnValueChanging(ref string newValue);
        /// <summary>
        /// The value of the control.
        /// </summary>
        [Parameter]
        public string Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }

        partial void FindByNameCheckboxChangeEventArgsDetail(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCheckboxChangeEventArgsDetail(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Checked"))
            { ser.AddBooleanProp("checked", this._checked); }
            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Checked"))
            { args["checked"] = (this._checked).ToString().ToLower(); }
            if (IsPropDirty("Value"))
            { args["value"] = this._value; }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("checked"))
            { this.Checked = ReturnToBoolean(args["checked"]); }
            if (args.ContainsKey("value"))
            { this.Value = ReturnToString(args["value"]); }

            this.SuppressParentNotify = false;
        }

    }
}
