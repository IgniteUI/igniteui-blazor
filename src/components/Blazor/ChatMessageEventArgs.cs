using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbChat.MessageCreated"/> event, carrying the chat message
    /// that was created.
    /// </summary>
    public partial class IgbChatMessageEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbChatMessage? _detail;

        /// <summary>
        /// The chat message the event was raised for.
        /// </summary>
        [Parameter]
        public IgbChatMessage? Detail
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
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbChatMessage)ConvertReturnValue(args["detail"], "ChatMessage", true); }

            this.SuppressParentNotify = false;
        }

    }
}
