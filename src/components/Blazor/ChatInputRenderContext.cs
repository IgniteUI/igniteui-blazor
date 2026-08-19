using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context object for renderers that deal with the chat input area.
    /// </summary>
    public partial class IgbChatInputRenderContext : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebChatInputRenderContext"; } }

        private string _value;

        /// <summary>
        /// The current value of the input field.
        /// </summary>
        [Parameter]
        public string Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

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

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }

        }

    }
}
