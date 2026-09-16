
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a user's reaction to a specific chat message.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbChatMessageReaction : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageReaction"; } }

        private IgbChatMessage _message = new IgbChatMessage();

        /// <summary>
        /// The chat message that the reaction is associated with.
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
        private string _reaction = string.Empty;

        /// <summary>
        /// The string representation of the reaction, such as an emoji or a string;
        /// </summary>
        public string Reaction
        {
            get { return this._reaction; }
            set
            {
                if (this._reaction != value || !IsPropDirty("Reaction"))
                {
                    MarkPropDirty("Reaction");
                }
                this._reaction = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Message"))
            { ser.AddSerializableProp("message", this._message); }
            if (IsPropDirty("Reaction"))
            { ser.AddStringProp("reaction", this._reaction); }

        }

    }
}
