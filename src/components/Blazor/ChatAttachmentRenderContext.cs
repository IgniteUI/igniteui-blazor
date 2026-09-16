
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with a specific attachment within a chat message.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbChatAttachmentRenderContext : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatAttachmentRenderContext"; } }

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
                this._attachment = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Attachment"))
            { ser.AddSerializableProp("attachment", this._attachment); }

        }

    }
}
