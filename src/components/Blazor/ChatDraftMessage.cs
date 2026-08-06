using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatDraftMessage : BaseRendererElement
    {
        public override string Type { get { return "WebChatDraftMessage"; } }

        private static bool _marshalByValue = true;

        private string _text;

        /// <summary>
        /// The textual content of the draft message.
        /// </summary>
        [Parameter]
        public string Text
        {
            get { return this._text; }
            set
            {
                if (this._text != value || !IsPropDirty("Text"))
                {
                    MarkPropDirty("Text");
                }
                this._text = value;

            }
        }
        private IgbChatMessageAttachment[] _attachments;

        /// <summary>
        /// An array of attachments associated with the draft message.
        /// </summary>
        [Parameter]
        public IgbChatMessageAttachment[] Attachments
        {
            get { return this._attachments; }
            set
            {
                if (this._attachments != value || !IsPropDirty("Attachments"))
                {
                    MarkPropDirty("Attachments");
                }
                this._attachments = value;

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

            if (IsPropDirty("Text"))
            { ser.AddStringProp("text", this._text); }
            if (IsPropDirty("Attachments"))
            { ser.AddSerializableArrayProp("attachments", this._attachments); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Text"))
            { args["text"] = this._text; }
            if (IsPropDirty("Attachments"))
            { args["attachments"] = ObjectArrayToParam(this._attachments); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("text"))
            { this.Text = ReturnToString(args["text"]); }
            if (args.ContainsKey("attachments"))
            { this.Attachments = ReturnToObjectArray<IgbChatMessageAttachment>(args["attachments"]); }

            this.SuppressParentNotify = false;
        }

    }
}
