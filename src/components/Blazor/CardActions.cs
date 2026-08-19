using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component for card action items such as buttons or icon buttons.
    /// Actions can be positioned at the start, center, or end of the container.
    /// </summary>
    public partial class IgbCardActions : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCardActions"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCardActionsModule.IsLoadRequested(IgBlazor))
            {
                IgbCardActionsModule.Register(IgBlazor);
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
                return "igc-card-actions";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private ContentOrientation _orientation = ContentOrientation.Horizontal;

        /// <summary>
        /// The orientation of the actions layout.
        /// </summary>
        [Parameter]
        public ContentOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                if (this._orientation != value || !IsPropDirty("Orientation"))
                {
                    MarkPropDirty("Orientation");
                }
                this._orientation = value;

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

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }

        }

    }
}
