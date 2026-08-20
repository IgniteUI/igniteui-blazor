using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The divider allows the content author to easily create a horizontal or vertical rule as a break between
    /// content, to better organize information on a page.
    /// </summary>
    public partial class IgbDivider : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDivider"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbDividerModule.IsLoadRequested(IgBlazor))
            {
                IgbDividerModule.Register(IgBlazor);
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
                return "igc-divider";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _vertical = false;

        /// <summary>
        /// Whether to render a vertical divider line.
        /// </summary>
        [Parameter]
        public bool Vertical
        {
            get { return this._vertical; }
            set
            {
                if (this._vertical != value || !IsPropDirty("Vertical"))
                {
                    MarkPropDirty("Vertical");
                }
                this._vertical = value;

            }
        }
        private bool _middle = false;

        /// <summary>
        /// When set and inset is provided, it will shrink the divider line from both sides.
        /// </summary>
        [Parameter]
        public bool Middle
        {
            get { return this._middle; }
            set
            {
                if (this._middle != value || !IsPropDirty("Middle"))
                {
                    MarkPropDirty("Middle");
                }
                this._middle = value;

            }
        }
        private DividerType _lineType = DividerType.Solid;

        /// <summary>
        /// Whether to render a solid or a dashed divider line.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("Type")]
        public DividerType LineType
        {
            get { return this._lineType; }
            set
            {
                if (this._lineType != value || !IsPropDirty("LineType"))
                {
                    MarkPropDirty("LineType");
                }
                this._lineType = value;

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

            if (IsPropDirty("Vertical"))
            { ser.AddBooleanProp("vertical", this._vertical); }
            if (IsPropDirty("Middle"))
            { ser.AddBooleanProp("middle", this._middle); }
            if (IsPropDirty("LineType"))
            { ser.AddEnumProp("lineType", this._lineType); }

        }

    }
}
