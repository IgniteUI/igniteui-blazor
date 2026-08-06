using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbChat.MessageReact"/> event, carrying the reaction and
    /// the chat message it applies to.
    /// </summary>
    public partial class IgbChatMessageReactionEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebChatMessageReactionEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbChatMessageReaction _detail;

        partial void OnDetailChanging(ref IgbChatMessageReaction newValue);
        /// <summary>
        /// The reaction the event was raised for, together with the chat message it is associated with.
        /// </summary>
        [Parameter]
        public IgbChatMessageReaction Detail
        {
            get { return this._detail; }
            set
            {
                OnDetailChanging(ref value);
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

        partial void FindByNameChatMessageReactionEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatMessageReactionEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbChatMessageReaction)ConvertReturnValue(args["detail"], "ChatMessageReaction", true); }

            this.SuppressParentNotify = false;
        }

    }
}
