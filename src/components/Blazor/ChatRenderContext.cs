using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The base context object passed to custom renderers, containing the
    /// <see cref="IgbChat"/> component instance.
    /// </summary>
    public partial class IgbChatRenderContext : BaseRendererElement
    {
        public override string Type { get { return "WebChatRenderContext"; } }

        private IgbChat _instance;

        /// <summary>
        /// The instance of the <see cref="IgbChat"/> component.
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

            if (IsPropDirty("Instance"))
            { ser.AddSerializableProp("instance", this._instance); }

        }

    }
}
