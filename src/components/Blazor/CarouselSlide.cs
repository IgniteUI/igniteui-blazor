using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A single content container within a set of containers nested in an <see cref="IgbCarousel"/>.
    /// </summary>
    public partial class IgbCarouselSlide : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCarouselSlide"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCarouselModule.IsLoadRequested(IgBlazor))
            {
                IgbCarouselModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-carousel-slide";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _active = false;

        /// <summary>
        /// The current active slide for the carousel component.
        /// </summary>
        [Parameter]
        public bool Active
        {
            get { return this._active; }
            set
            {
                if (this._active != value || !IsPropDirty("Active"))
                {
                    MarkPropDirty("Active");
                }
                this._active = value;

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

            if (IsPropDirty("Active"))
            { ser.AddBooleanProp("active", this._active); }

        }

    }
}
