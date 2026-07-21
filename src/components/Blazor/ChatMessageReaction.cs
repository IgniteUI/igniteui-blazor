using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatMessageReaction : BaseRendererElement
    {
        public override string Type { get { return "WebChatMessageReaction"; } }

        private static bool _marshalByValue = true;

        public IgbChatMessageReaction() : base()
        {
            OnCreatedIgbChatMessageReaction();

        }

        partial void OnCreatedIgbChatMessageReaction();

        private IgbChatMessage _message;

        partial void OnMessageChanging(ref IgbChatMessage newValue);
        /// <summary>
        /// The chat message that the reaction is associated with.
        /// </summary>
        [Parameter]
        public IgbChatMessage Message
        {
            get { return this._message; }
            set
            {
                OnMessageChanging(ref value);
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
        private string _reaction;

        partial void OnReactionChanging(ref string newValue);
        /// <summary>
        /// The string representation of the reaction, such as an emoji or a string;
        /// </summary>
        [Parameter]
        public string Reaction
        {
            get { return this._reaction; }
            set
            {
                if (this._reaction != value || !IsPropDirty("Reaction"))
                {
                    MarkPropDirty("Reaction");
                }
                this._reaction = value;

            }
        }

        partial void FindByNameChatMessageReaction(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatMessageReaction(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        partial void SerializeCoreIgbChatMessageReaction(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatMessageReaction(ser);

            if (IsPropDirty("Message"))
            { ser.AddSerializableProp("message", this._message); }
            if (IsPropDirty("Reaction"))
            { ser.AddStringProp("reaction", this._reaction); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Message"))
            { args["message"] = ObjectToParam(this._message); }
            if (IsPropDirty("Reaction"))
            { args["reaction"] = this._reaction; }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("message"))
            { this.Message = (IgbChatMessage)ConvertReturnValue(args["message"], "ChatMessage", true); }
            if (args.ContainsKey("reaction"))
            { this.Reaction = ReturnToString(args["reaction"]); }

            this.SuppressParentNotify = false;
        }

    }
}
