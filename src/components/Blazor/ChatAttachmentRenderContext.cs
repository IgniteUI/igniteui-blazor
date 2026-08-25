using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with a specific attachment within a chat message.
    /// </summary>
    public partial class IgbChatAttachmentRenderContext : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatAttachmentRenderContext"; } }

        private IgbChatMessageAttachment? _attachment;

        /// <summary>
        /// The specific attachment being rendered.
        /// </summary>
        [Parameter]
        public IgbChatMessageAttachment? Attachment
        {
            get { return this._attachment; }
            set
            {
                MarkPropDirty("Attachment");
                if (this._attachment != null)
                {
                    this.DetachChild(this._attachment);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._attachment = value;
            }

        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Attachment"))
            { ser.AddSerializableProp("attachment", this._attachment); }

        }

    }
}
