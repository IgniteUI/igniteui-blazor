using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a select list.
    /// </summary>
    [CascadingTypeParameter(nameof(TValue))]
    public partial class IgbSelectItem<TValue> : IgbBaseOptionLike<TValue>
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebSelectItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSelectModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectModule.Register(IgBlazor);
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
                return "igc-select-item";
            }
        }

    }
}
