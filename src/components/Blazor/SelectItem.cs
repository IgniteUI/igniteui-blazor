using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents an item in a select list.
    /// </summary>
    /// <typeparam name="TValue"><c>string</c>, <c>char</c>, an enum, or a numeric type such as <c>int</c> or <c>double</c>, or their nullable forms.</typeparam>
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
