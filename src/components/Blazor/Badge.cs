using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The badge is a component indicating a status on a related item or an area
    /// where some active indication is required.
    /// </summary>
    public partial class IgbBadge : BaseRendererControl
    {
        public override string Type { get { return "WebBadge"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbBadgeModule.IsLoadRequested(IgBlazor))
            {
                IgbBadgeModule.Register(IgBlazor);
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
                return "igc-badge";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private StyleVariant _variant = StyleVariant.Primary;

        /// <summary>
        /// The type (style variant) of the badge.
        /// </summary>
        [Parameter]
        public StyleVariant Variant
        {
            get { return this._variant; }
            set
            {
                if (this._variant != value || !IsPropDirty("Variant"))
                {
                    MarkPropDirty("Variant");
                }
                this._variant = value;

            }
        }
        private bool _outlined = false;

        /// <summary>
        /// Sets whether to draw an outlined version of the badge.
        /// </summary>
        [Parameter]
        public bool Outlined
        {
            get { return this._outlined; }
            set
            {
                if (this._outlined != value || !IsPropDirty("Outlined"))
                {
                    MarkPropDirty("Outlined");
                }
                this._outlined = value;

            }
        }
        private BadgeShape _shape = BadgeShape.Rounded;

        /// <summary>
        /// The shape of the badge.
        /// </summary>
        [Parameter]
        public BadgeShape Shape
        {
            get { return this._shape; }
            set
            {
                if (this._shape != value || !IsPropDirty("Shape"))
                {
                    MarkPropDirty("Shape");
                }
                this._shape = value;

            }
        }
        private bool _dot = false;

        /// <summary>
        /// Sets whether to render a dot type badge.
        /// When enabled, the badge appears as a small dot without any content.
        /// </summary>
        [Parameter]
        public bool Dot
        {
            get { return this._dot; }
            set
            {
                if (this._dot != value || !IsPropDirty("Dot"))
                {
                    MarkPropDirty("Dot");
                }
                this._dot = value;

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

            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }
            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("Shape"))
            { ser.AddEnumProp("shape", this._shape); }
            if (IsPropDirty("Dot"))
            { ser.AddBooleanProp("dot", this._dot); }

        }

    }
}
