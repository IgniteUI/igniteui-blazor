using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A container component that wraps different elements related to a single subject.
    /// The card component provides a flexible container for organizing content such as headers,
    /// media, text content, and actions.
    /// </summary>
    public partial class IgbCard : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCard"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCardModule.IsLoadRequested(IgBlazor))
            {
                IgbCardModule.Register(IgBlazor);
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
                return "igc-card";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _elevated = false;

        /// <summary>
        /// Sets the card to have an elevated appearance with shadow.
        /// When false, the card uses an outlined style with a border.
        /// </summary>
        [Parameter]
        public bool Elevated
        {
            get { return this._elevated; }
            set
            {
                if (this._elevated != value || !IsPropDirty("Elevated"))
                {
                    MarkPropDirty("Elevated");
                }
                this._elevated = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Elevated"))
            { ser.AddBooleanProp("elevated", this._elevated); }

        }

    }
}
