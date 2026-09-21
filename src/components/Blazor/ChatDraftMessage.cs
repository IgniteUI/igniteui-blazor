namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A message being composed by the user but not yet sent, including its text and attachments.
    /// </summary>
    public partial class IgbChatDraftMessage : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatDraftMessage"; } }

        private string _text = string.Empty;

        /// <summary>
        /// The textual content of the draft message.
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
        private IgbChatMessageAttachment[] _attachments = Array.Empty<IgbChatMessageAttachment>();

        /// <summary>
        /// An array of attachments associated with the draft message.
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Text"))
            { ser.AddStringProp("text", this._text); }
            if (IsPropDirty("Attachments"))
            { ser.AddSerializableArrayProp("attachments", this._attachments); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Text"))
            { args["text"] = this._text; }
            if (IsPropDirty("Attachments"))
            { args["attachments"] = ObjectArrayToParam(this._attachments); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("text", out var textObj))
            { this.Text = ReturnToString(textObj); }
            if (args != null && args.TryGetValue("attachments", out var attachmentsObj))
            { this.Attachments = ReturnToObjectArray<IgbChatMessageAttachment>(attachmentsObj) ?? Array.Empty<IgbChatMessageAttachment>(); }

            this.SuppressParentNotify = false;
        }

    }
}
