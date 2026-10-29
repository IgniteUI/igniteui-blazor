namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with the chat input area.
    /// </summary>
    public partial class IgbChatInputRenderContext : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebChatInputRenderContext"; } }

        private string _value = string.Empty;

        /// <summary>
        /// The current value of the input field.
        /// </summary>
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("value", out var valueObj))
            { this.Value = ReturnToString(valueObj); }

            this.SuppressParentNotify = false;
        }

    }
}
