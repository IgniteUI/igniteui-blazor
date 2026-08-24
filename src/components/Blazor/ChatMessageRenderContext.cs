using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with a specific chat message.
    /// </summary>
    public partial class IgbChatMessageRenderContext : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageRenderContext"; } }

        private IgbChatMessage? _message;

        /// <summary>
        /// The specific chat message being rendered.
        /// </summary>
        [Parameter]
        public IgbChatMessage? Message
        {
            get { return this._message; }
            set
            {
                MarkPropDirty("Message");
                if (this._message != null)
                {
                    this.DetachChild(this._message);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._message = value;
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

            if (IsPropDirty("Message"))
            { ser.AddSerializableProp("message", this._message); }

        }

    }
}
