using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbRadio.Change"/> and <see cref="IgbRadioGroup.Change"/> events.
    /// </summary>
    public partial class IgbRadioChangeEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRadioChangeEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private bool _checked = false;

        /// <summary>
        /// The checked state of the radio button after the change.
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
        private string? _value;

        /// <summary>
        /// The value of the radio button.
        /// </summary>
        [Parameter]
        public string? Value
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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Checked"))
            { ser.AddBooleanProp("checked", this._checked); }
            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Checked"))
            { args["checked"] = (this._checked).ToString().ToLower(); }
            if (IsPropDirty("Value"))
            { args["value"] = this._value; }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
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
