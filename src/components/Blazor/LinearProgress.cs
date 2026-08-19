using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A linear progress indicator used to express unspecified wait time or display
    /// the length of a process.
    /// </summary>
    public partial class IgbLinearProgress : IgbProgressBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebLinearProgress"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbLinearProgressModule.IsLoadRequested(IgBlazor))
            {
                IgbLinearProgressModule.Register(IgBlazor);
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
                return "igc-linear-progress";
            }
        }

        private bool _striped = false;

        /// <summary>
        /// Sets the striped look of the control.
        /// </summary>
        [Parameter]
        public bool Striped
        {
            get { return this._striped; }
            set
            {
                if (this._striped != value || !IsPropDirty("Striped"))
                {
                    MarkPropDirty("Striped");
                }
                this._striped = value;

            }
        }
        private LinearProgressLabelAlign _labelAlign = LinearProgressLabelAlign.TopStart;

        /// <summary>
        /// The position for the default label of the control.
        /// </summary>
        [Parameter]
        public LinearProgressLabelAlign LabelAlign
        {
            get { return this._labelAlign; }
            set
            {
                if (this._labelAlign != value || !IsPropDirty("LabelAlign"))
                {
                    MarkPropDirty("LabelAlign");
                }
                this._labelAlign = value;

            }
        }

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Striped"))
            { ser.AddBooleanProp("striped", this._striped); }
            if (IsPropDirty("LabelAlign"))
            { ser.AddEnumProp("labelAlign", this._labelAlign); }

        }

    }
}
