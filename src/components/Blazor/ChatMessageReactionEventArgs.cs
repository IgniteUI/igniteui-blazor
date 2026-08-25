using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbChat.MessageReact"/> event, carrying the reaction and
    /// the chat message it applies to.
    /// </summary>
    public partial class IgbChatMessageReactionEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageReactionEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbChatMessageReaction? _detail;

        /// <summary>
        /// The reaction the event was raised for, together with the chat message it is associated with.
        /// </summary>
        [Parameter]
        public IgbChatMessageReaction? Detail
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
            { this.Detail = (IgbChatMessageReaction?)ConvertReturnValue(args["detail"], "ChatMessageReaction", true); }

            this.SuppressParentNotify = false;
        }

    }
}
