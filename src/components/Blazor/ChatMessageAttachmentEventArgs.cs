using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbChat.AttachmentClick"/> event, carrying the message
    /// attachment that was clicked.
    /// </summary>
    public partial class IgbChatMessageAttachmentEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageAttachmentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbChatMessageAttachment? _detail;

        /// <summary>
        /// The chat message attachment the event was raised for.
        /// </summary>
        [Parameter]
        public IgbChatMessageAttachment? Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._detail = value;
            }

        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbChatMessageAttachment)ConvertReturnValue(args["detail"], "ChatMessageAttachment", true); }

            this.SuppressParentNotify = false;
        }

    }
}
