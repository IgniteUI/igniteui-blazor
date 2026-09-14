using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A check box allowing single values to be selected/deselected.
    /// </summary>
    public partial class IgbCheckbox : IgbCheckboxBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCheckbox"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCheckboxModule.IsLoadRequested(IgBlazor))
            {
                IgbCheckboxModule.Register(IgBlazor);
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
                return "igc-checkbox";
            }
        }

        private bool _indeterminate = false;

        /// <summary>
        /// Draws the checkbox in indeterminate state.
        /// </summary>
        [Parameter]
        public bool Indeterminate
        {
            get { return this._indeterminate; }
            set
            {
                if (this._indeterminate != value || !IsPropDirty("Indeterminate"))
                {
                    MarkPropDirty("Indeterminate");
                }
                this._indeterminate = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Indeterminate"))
            { ser.AddBooleanProp("indeterminate", this._indeterminate); }

        }

    }
}
