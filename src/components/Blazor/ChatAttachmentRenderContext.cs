namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with a specific attachment within a chat message.
    /// </summary>
    public partial class IgbChatAttachmentRenderContext : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebChatAttachmentRenderContext"; } }

        private IgbChatMessageAttachment _attachment = new IgbChatMessageAttachment();

        /// <summary>
        /// The specific attachment being rendered.
        /// </summary>
        public IgbChatMessageAttachment Attachment
        {
            get { return this._attachment; }
            set
            {
                MarkPropDirty("Attachment");
                if (this._attachment != null)
                {
                    this.DetachChild(this._attachment);
                }
                this._attachment = value;
                if (value != null)
                {
                    this.AttachChild(value);
                }
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Attachment"))
            { ser.AddSerializableProp("attachment", this._attachment); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Attachment"))
            { args["attachment"] = ObjectToParam(this._attachment); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("attachment", out var attachmentObj) && ConvertReturnValue(attachmentObj, "ChatMessageAttachment", true) is IgbChatMessageAttachment attachment)
            { this.Attachment = attachment; }

            this.SuppressParentNotify = false;
        }

    }
}
