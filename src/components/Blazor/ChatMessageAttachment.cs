
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an attachment associated with a chat message.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbChatMessageAttachment : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageAttachment"; } }

        private string _id = string.Empty;

        /// <summary>
        /// A unique identifier for the attachment.
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
        private string? _url;

        /// <summary>
        /// The URL from which the attachment can be downloaded or viewed.
        /// Typically used for attachments stored on a server or CDN.
        /// </summary>
        public string? Url
        {
            get { return this._url; }
            set
            {
                if (this._url != value || !IsPropDirty("Url"))
                {
                    MarkPropDirty("Url");
                }
                this._url = value;

            }
        }
        private string? _attachmentType;

        /// <summary>
        /// The MIME type or a custom type identifier for the attachment (e.g. "image/png", "pdf", "audio").
        /// </summary>
        [WCWidgetMemberName("Type")]
        public string? AttachmentType
        {
            get { return this._attachmentType; }
            set
            {
                if (this._attachmentType != value || !IsPropDirty("AttachmentType"))
                {
                    MarkPropDirty("AttachmentType");
                }
                this._attachmentType = value;

            }
        }
        private string? _thumbnail;

        /// <summary>
        /// Optional URL to a thumbnail preview of the attachment (e.g. for images or videos).
        /// </summary>
        public string? Thumbnail
        {
            get { return this._thumbnail; }
            set
            {
                if (this._thumbnail != value || !IsPropDirty("Thumbnail"))
                {
                    MarkPropDirty("Thumbnail");
                }
                this._thumbnail = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Id"))
            { ser.AddStringProp("id", this._id); }
            if (IsPropDirty("Url"))
            { ser.AddStringProp("url", this._url); }
            if (IsPropDirty("AttachmentType"))
            { ser.AddStringProp("attachmentType", this._attachmentType); }
            if (IsPropDirty("Thumbnail"))
            { ser.AddStringProp("thumbnail", this._thumbnail); }

        }

    }
}
