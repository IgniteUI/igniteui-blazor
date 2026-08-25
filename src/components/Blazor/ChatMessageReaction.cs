using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a user's reaction to a specific chat message.
    /// </summary>
    public partial class IgbChatMessageReaction : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatMessageReaction"; } }

        private static bool _marshalByValue = true;

        private IgbChatMessage? _message;

        /// <summary>
        /// The chat message that the reaction is associated with.
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
        private string? _reaction;

        /// <summary>
        /// The string representation of the reaction, such as an emoji or a string;
        /// </summary>
        [Parameter]
        public string? Reaction
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
            if (IsPropDirty("Reaction"))
            { ser.AddStringProp("reaction", this._reaction); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Message"))
            { args["message"] = ObjectToParam(this._message); }
            if (IsPropDirty("Reaction"))
            { args["reaction"] = this._reaction; }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("message"))
            { this.Message = (IgbChatMessage?)ConvertReturnValue(args["message"], "ChatMessage", true); }
            if (args.ContainsKey("reaction"))
            { this.Reaction = ReturnToString(args["reaction"]); }

            this.SuppressParentNotify = false;
        }

    }
}
