using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an attachment associated with a chat message.
    /// </summary>
    public partial class IgbChatMessageAttachment : BaseRendererElement
    {
        public override string Type { get { return "WebChatMessageAttachment"; } }

        private static bool _marshalByValue = true;

        private string _id;

        /// <summary>
        /// A unique identifier for the attachment.
        /// </summary>
        [Parameter]
        public string Id
        {
            get { return this._id; }
            set
            {
                if (this._id != value || !IsPropDirty("Id"))
                {
                    MarkPropDirty("Id");
                }
                this._id = value;

            }
        }
        private string _url;

        /// <summary>
        /// The URL from which the attachment can be downloaded or viewed.
        /// Typically used for attachments stored on a server or CDN.
        /// </summary>
        [Parameter]
        public string Url
        {
            get { return this._url; }
            set
            {
                if (this._url != value || !IsPropDirty("Url"))
                {
                    MarkPropDirty("Url");
                }
                this._url = value;

            }
        }
        private string _attachmentType;

        /// <summary>
        /// The MIME type or a custom type identifier for the attachment (e.g. "image/png", "pdf", "audio").
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("Type")]
        public string AttachmentType
        {
            get { return this._attachmentType; }
            set
            {
                if (this._attachmentType != value || !IsPropDirty("AttachmentType"))
                {
                    MarkPropDirty("AttachmentType");
                }
                this._attachmentType = value;

            }
        }
        private string _thumbnail;

        /// <summary>
        /// Optional URL to a thumbnail preview of the attachment (e.g. for images or videos).
        /// </summary>
        [Parameter]
        public string Thumbnail
        {
            get { return this._thumbnail; }
            set
            {
                if (this._thumbnail != value || !IsPropDirty("Thumbnail"))
                {
                    MarkPropDirty("Thumbnail");
                }
                this._thumbnail = value;

            }
        }

        partial void FindByNameChatMessageAttachment(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatMessageAttachment(name, ref item);
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Id"))
            { ser.AddStringProp("id", this._id); }
            if (IsPropDirty("Url"))
            { ser.AddStringProp("url", this._url); }
            if (IsPropDirty("AttachmentType"))
            { ser.AddStringProp("attachmentType", this._attachmentType); }
            if (IsPropDirty("Thumbnail"))
            { ser.AddStringProp("thumbnail", this._thumbnail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Id"))
            { args["id"] = this._id; }
            if (IsPropDirty("Name"))
            { args["name"] = this._name; }
            if (IsPropDirty("Url"))
            { args["url"] = this._url; }
            if (IsPropDirty("AttachmentType"))
            { args["attachmentType"] = this._attachmentType; }
            if (IsPropDirty("Thumbnail"))
            { args["thumbnail"] = this._thumbnail; }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("id"))
            { this.Id = ReturnToString(args["id"]); }
            if (args.ContainsKey("name"))
            { this.Name = ReturnToString(args["name"]); }
            if (args.ContainsKey("url"))
            { this.Url = ReturnToString(args["url"]); }
            if (args.ContainsKey("attachmentType"))
            { this.AttachmentType = ReturnToString(args["attachmentType"]); }
            if (args.ContainsKey("thumbnail"))
            { this.Thumbnail = ReturnToString(args["thumbnail"]); }

            this.SuppressParentNotify = false;
        }

    }
}
