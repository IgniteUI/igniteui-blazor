namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for the card's main text content.
    /// This component should be used within an igc-card element to display the primary content.
    /// </summary>
    public partial class IgbCardContent : BaseRendererControl
    {
        public override string Type { get { return "WebCardContent"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbCardContentModule.IsLoadRequested(IgBlazor))
            {
                IgbCardContentModule.Register(IgBlazor);
            }
        }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-card-content";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbCardContent() : base()
        {
            OnCreatedIgbCardContent();

        }

        partial void OnCreatedIgbCardContent();

        partial void FindByNameCardContent(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCardContent(name, ref item);
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

        partial void SerializeCoreIgbCardContent(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbCardContent(ser);

        }

    }
}
