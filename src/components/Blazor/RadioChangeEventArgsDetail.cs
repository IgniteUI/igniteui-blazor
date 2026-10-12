namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbRadio{TValue}.Change"/> and <see cref="IgbRadioGroup{TValue}.Change"/> events.
    /// </summary>
    /// <typeparam name="TValue">The <c>TValue</c> of the radio or radio group that raises the event.</typeparam>
    public partial class IgbRadioChangeEventArgsDetail<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebRadioChangeEventArgsDetail"; } }

        private bool _checked = false;

        /// <summary>
        /// The checked state of the radio button after the change.
        /// </summary>
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
        private TValue? _value;

        /// <summary>
        /// The value of the radio button.
        /// </summary>
        public TValue? Value
        {
            get { return this._value; }
            set
            {
                if (!EqualityComparer<TValue?>.Default.Equals(this._value, value) || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Checked"))
            { ser.AddBooleanProp("checked", this._checked); }
            if (IsPropDirty("Value"))
            { AddGenericValue(ser, "value", this._value); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Checked"))
            { args["checked"] = (this._checked).ToString().ToLower(); }
            if (IsPropDirty("Value"))
            { args["value"] = this._value; }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("checked", out var checkedObj))
            { this.Checked = ReturnToBoolean(checkedObj); }
            if (args != null && args.TryGetValue("value", out var valueObj))
            { this.Value = GenericValueFromEventJson<TValue>(valueObj); }

            this.SuppressParentNotify = false;
        }

    }
}
