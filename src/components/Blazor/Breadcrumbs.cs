using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A navigation trail that renders an ordered list of <see cref="IgbBreadcrumb"/> items.
    /// </summary>
    /// <remarks>
    /// The component has the ARIA <c>list</c> role. Put it in a <c>&lt;nav aria-label="..."&gt;</c> element,
    /// as the ARIA breadcrumb pattern requires the label on the navigation landmark, not on the list.
    /// </remarks>
    public partial class IgbBreadcrumbs : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebBreadcrumbs"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbBreadcrumbsModule.IsLoadRequested(IgBlazor))
            {
                IgbBreadcrumbsModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        private protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        private protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override string DirectRenderElementName
        {
            get
            {
                return "igc-breadcrumbs";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private string? _separator;

        /// <summary>
        /// The name of the icon rendered as the separator between the items. When not set, the
        /// <c>tree_expand</c> icon is used. An item overrides it with content in its <c>separator</c> slot.
        /// </summary>
        [Parameter]
        public string? Separator
        {
            get { return this._separator; }
            set
            {
                if (this._separator != value || !IsPropDirty("Separator"))
                {
                    MarkPropDirty("Separator");
                }
                this._separator = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Separator"))
            { ser.AddStringProp("separator", this._separator); }

        }

    }
}
