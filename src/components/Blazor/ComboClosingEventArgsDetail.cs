namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbCombo{TValue, TItem}.Closing"/> and <see cref="IgbCombo{TValue, TItem}.Closed"/> events.
    /// </summary>
    public partial class IgbComboClosingEventArgsDetail : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboClosingEventArgsDetail"; } }

        private ComboCloseTrigger _trigger = ComboCloseTrigger.Enter;

        /// <summary>
        /// The user action that closes the list.
        /// </summary>
        public ComboCloseTrigger Trigger
        {
            get { return this._trigger; }
            set
            {
                if (this._trigger != value || !IsPropDirty("Trigger"))
                {
                    MarkPropDirty("Trigger");
                }
                this._trigger = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Trigger"))
            { ser.AddEnumProp("trigger", this._trigger); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Trigger"))
            { args["trigger"] = EnumToString(this._trigger); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("trigger", out var trigger))
            { this.Trigger = StringToEnum<ComboCloseTrigger>(trigger); }

            this.SuppressParentNotify = false;
        }

    }
}
