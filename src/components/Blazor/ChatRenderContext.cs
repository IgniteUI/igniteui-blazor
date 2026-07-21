
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatRenderContext : BaseRendererElement
    {
        public override string Type { get { return "WebChatRenderContext"; } }

        public IgbChatRenderContext() : base()
        {
            OnCreatedIgbChatRenderContext();

        }

        partial void OnCreatedIgbChatRenderContext();

        private IgbChat _instance;

        partial void OnInstanceChanging(ref IgbChat newValue);
        /// <summary>
        /// The instance of the IgcChatComponent.
        /// </summary>
        [Parameter]
        public IgbChat Instance
        {
            get { return this._instance; }
            set
            {
                if (this._instance != value || !IsPropDirty("Instance"))
                {
                    MarkPropDirty("Instance");
                }
                this._instance = value;

            }
        }

        partial void FindByNameChatRenderContext(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatRenderContext(name, ref item);
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

        partial void SerializeCoreIgbChatRenderContext(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatRenderContext(ser);

            if (IsPropDirty("Instance"))
            { ser.AddSerializableProp("instance", this._instance); }

        }

    }
}
