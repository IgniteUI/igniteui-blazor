using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatInputRenderContext : BaseRendererElement
    {
        public override string Type { get { return "WebChatInputRenderContext"; } }

        public IgbChatInputRenderContext() : base()
        {
            OnCreatedIgbChatInputRenderContext();

        }

        partial void OnCreatedIgbChatInputRenderContext();

        private string _value;

        partial void OnValueChanging(ref string newValue);
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

        partial void FindByNameChatInputRenderContext(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatInputRenderContext(name, ref item);
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

        partial void SerializeCoreIgbChatInputRenderContext(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatInputRenderContext(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }

        }

    }
}
