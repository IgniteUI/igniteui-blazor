namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with a specific chat message.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbChatMessageRenderContext : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageRenderContext"; } }

        private IgbChatMessage _message = new IgbChatMessage();

        /// <summary>
        /// The specific chat message being rendered.
        /// </summary>
        public IgbChatMessage Message
        {
            get { return this._message; }
            set
            {
                MarkPropDirty("Message");
                this._message = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Message"))
            { ser.AddSerializableProp("message", this._message); }

        }

    }
}
