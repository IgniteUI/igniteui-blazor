using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A single item in an <see cref="IgbBreadcrumbs"/> navigation trail.
    /// </summary>
    /// <remarks>
    /// The component has the ARIA <c>listitem</c> role. Put the item content, usually an anchor, in the default slot;
    /// the <c>prefix</c> and <c>suffix</c> slots add content before and after it, and the <c>separator</c> slot
    /// replaces the separator icon of this item. Assistive technology does not read the separator.
    /// </remarks>
    public partial class IgbBreadcrumb : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebBreadcrumb"; } }

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
                return "igc-breadcrumb";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _current = false;

        /// <summary>
        /// Marks the item as the current page and sets <c>aria-current="page"</c> on it.
        /// </summary>
        [Parameter]
        public bool Current
        {
            get { return this._current; }
            set
            {
                if (this._current != value || !IsPropDirty("Current"))
                {
                    MarkPropDirty("Current");
                }
                this._current = value;

            }
        }
        private bool _disabled = false;

        /// <summary>
        /// Disables the item. Sets <c>aria-disabled="true"</c> on it and removes the slotted content from the tab sequence.
        /// </summary>
        [Parameter]
        public bool Disabled
        {
            get { return this._disabled; }
            set
            {
                if (this._disabled != value || !IsPropDirty("Disabled"))
                {
                    MarkPropDirty("Disabled");
                }
                this._disabled = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Current"))
            { ser.AddBooleanProp("current", this._current); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }

        }

    }
}
