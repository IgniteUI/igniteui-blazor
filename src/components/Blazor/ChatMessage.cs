namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a single chat message in an <see cref="IgbChat"/> conversation.
    /// </summary>
    public partial class IgbChatMessage : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessage"; } }

        private string _id = string.Empty;

        /// <summary>
        /// A unique identifier for the message.
        /// </summary>
        public string Id
        {
            get { return this._id; }
            set
            {
                if (this._id != value || !IsPropDirty("Id"))
                {
                    MarkPropDirty("Id");
                }
                this._id = value;

            }
        }
        private string _text = string.Empty;

        /// <summary>
        /// The textual content of the message.
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set
            {
                if (this._text != value || !IsPropDirty("Text"))
                {
                    MarkPropDirty("Text");
                }
                this._text = value;

            }
        }
        private string _sender = string.Empty;

        /// <summary>
        /// The identifier or name of the sender of the message.
        /// </summary>
        public string Sender
        {
            get { return this._sender; }
            set
            {
                if (this._sender != value || !IsPropDirty("Sender"))
                {
                    MarkPropDirty("Sender");
                }
                this._sender = value;

            }
        }
        private string? _timestamp;

        /// <summary>
        /// The timestamp indicating when the message was sent.
        /// </summary>
        public string? Timestamp
        {
            get { return this._timestamp; }
            set
            {
                if (this._timestamp != value || !IsPropDirty("Timestamp"))
                {
                    MarkPropDirty("Timestamp");
                }
                this._timestamp = value;

            }
        }
        private IgbChatMessageAttachment[] _attachments = Array.Empty<IgbChatMessageAttachment>();

        /// <summary>
        /// Optional list of attachments associated with the message,
        /// such as images, files, or links.
        /// </summary>
        public IgbChatMessageAttachment[] Attachments
        {
            get { return this._attachments; }
            set
            {
                if (this._attachments != value || !IsPropDirty("Attachments"))
                {
                    MarkPropDirty("Attachments");
                }
                this._attachments = value;

            }
        }
        private string[] _reactions = Array.Empty<string>();

        /// <summary>
        /// Optional list of reactions associated with the message.
        /// </summary>
        public string[] Reactions
        {
            get { return this._reactions; }
            set
            {
                if (this._reactions != value || !IsPropDirty("Reactions"))
                {
                    MarkPropDirty("Reactions");
                }
                this._reactions = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Id"))
            { ser.AddStringProp("id", this._id); }
            if (IsPropDirty("Text"))
            { ser.AddStringProp("text", this._text); }
            if (IsPropDirty("Sender"))
            { ser.AddStringProp("sender", this._sender); }
            if (IsPropDirty("Timestamp"))
            { ser.AddStringProp("timestamp", this._timestamp); }
            if (IsPropDirty("Attachments"))
            { ser.AddSerializableArrayProp("attachments", this._attachments); }
            if (IsPropDirty("Reactions"))
            { ser.AddArrayProp("reactions", this._reactions); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Id"))
            { args["id"] = this._id; }
            if (IsPropDirty("Text"))
            { args["text"] = this._text; }
            if (IsPropDirty("Sender"))
            { args["sender"] = this._sender; }
            if (IsPropDirty("Timestamp"))
            { args["timestamp"] = this._timestamp; }
            if (IsPropDirty("Attachments"))
            { args["attachments"] = ObjectArrayToParam(this._attachments); }
            if (IsPropDirty("Reactions"))
            { args["reactions"] = StringArrayToString(this._reactions); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("id", out var idObj))
            { this.Id = ReturnToString(idObj); }
            if (args != null && args.TryGetValue("text", out var textObj))
            { this.Text = ReturnToString(textObj); }
            if (args != null && args.TryGetValue("sender", out var senderObj))
            { this.Sender = ReturnToString(senderObj); }
            if (args != null && args.TryGetValue("timestamp", out var timestampObj))
            { this.Timestamp = ReturnToString(timestampObj); }
            if (args != null && args.TryGetValue("attachments", out var attachmentsObj))
            { this.Attachments = ReturnToObjectArray<IgbChatMessageAttachment>(attachmentsObj) ?? Array.Empty<IgbChatMessageAttachment>(); }
            if (args != null && args.TryGetValue("reactions", out var reactionsObj))
            { this.Reactions = ReturnToStringArray(reactionsObj) ?? Array.Empty<string>(); }

            this.SuppressParentNotify = false;
        }

    }
}
