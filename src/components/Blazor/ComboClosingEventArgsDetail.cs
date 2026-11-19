namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbCombo{TValue, TItem}.Closing"/> and <see cref="IgbCombo{TValue, TItem}.Closed"/> events.
    /// </summary>
    public partial class IgbComboClosingEventArgsDetail : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboClosingEventArgsDetail"; } }

        private ComboCloseTrigger? _trigger;

        /// <summary>
        /// The user action that closes the list, or <see langword="null"/> when the web component reports
        /// none or one that this version does not know.
        /// </summary>
        public ComboCloseTrigger? Trigger
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

            if (IsPropDirty("Trigger") && this._trigger is { } trigger)
            { args["trigger"] = EnumToString(trigger); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            this.Trigger = args != null && args.TryGetValue("trigger", out var trigger) ? ToTrigger(ReturnToString(trigger)) : null;

            this.SuppressParentNotify = false;
        }

        private static ComboCloseTrigger? ToTrigger(string? value)
        {
            if (Utils.TryGetEnumMemberName(typeof(ComboCloseTrigger), value, out var member) &&
                Enum.TryParse<ComboCloseTrigger>(member, out var named))
            {
                return named;
            }
            // Only names: a number would parse as an undefined member.
            if (value != null && !value.Any(char.IsDigit) &&
                Enum.TryParse<ComboCloseTrigger>(value, true, out var parsed) && Enum.IsDefined(parsed))
            {
                return parsed;
            }
            return null;
        }
    }
}
