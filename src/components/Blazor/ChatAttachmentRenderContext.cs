
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatAttachmentRenderContext : BaseRendererElement
    {
        public override string Type { get { return "WebChatAttachmentRenderContext"; } }

        public IgbChatAttachmentRenderContext() : base()
        {
            OnCreatedIgbChatAttachmentRenderContext();

        }

        partial void OnCreatedIgbChatAttachmentRenderContext();

        private IgbChatMessageAttachment _attachment;

        partial void OnAttachmentChanging(ref IgbChatMessageAttachment newValue);
        /// <summary>
        /// The specific attachment being rendered.
        /// </summary>
        [Parameter]
        public IgbChatMessageAttachment Attachment
        {
            get { return this._attachment; }
            set
            {
                OnAttachmentChanging(ref value);
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

        partial void FindByNameChatAttachmentRenderContext(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatAttachmentRenderContext(name, ref item);
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

        partial void SerializeCoreIgbChatAttachmentRenderContext(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatAttachmentRenderContext(ser);

            if (IsPropDirty("Attachment"))
            { ser.AddSerializableProp("attachment", this._attachment); }

        }

    }
}
