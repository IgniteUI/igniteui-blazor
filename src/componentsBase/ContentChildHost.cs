using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Implemented by components of other Ignite UI packages (the full IgniteUI.Blazor package) that accept elements
    /// of this package as content children, e.g. an <see cref="IgbNumberFormatSpecifier"/> declared inside a chart axis.
    /// The host cascades itself to its child content; supported elements attach on initialization and detach on disposal.
    /// </summary>
    internal interface IContentChildHost
    {
        /// <summary>Adds <paramref name="child"/> to the matching content collection of the host.</summary>
        /// <returns><see langword="true"/> when the host accepted the child.</returns>
        bool AttachContentChild(BaseRendererElement child);

        /// <summary>Removes <paramref name="child"/> from the content collection it was added to.</summary>
        void DetachContentChild(BaseRendererElement child);
    }

    internal static class ContentChildHost
    {
        /// <summary>The name the host is cascaded under.</summary>
        internal const string CascadingName = "IgbContentChildHost";

        /// <summary>
        /// Adds <paramref name="content"/> to <paramref name="builder"/>, wrapped in a cascade of <paramref name="owner"/>
        /// when it is a <see cref="IContentChildHost"/>. Components that are not hosts render exactly as before.
        /// </summary>
        internal static void AddChildContent(RenderTreeBuilder builder, int sequence, object owner, RenderFragment? content)
        {
            if (owner is not IContentChildHost host)
            {
                builder.AddContent(sequence, content);
                return;
            }

            builder.OpenRegion(sequence);
            builder.OpenComponent<CascadingValue<IContentChildHost>>(0);
            builder.AddComponentParameter(1, nameof(CascadingValue<IContentChildHost>.Value), host);
            builder.AddComponentParameter(2, nameof(CascadingValue<IContentChildHost>.Name), CascadingName);
            builder.AddComponentParameter(3, nameof(CascadingValue<IContentChildHost>.IsFixed), true);
            builder.AddComponentParameter(4, nameof(CascadingValue<IContentChildHost>.ChildContent), content);
            builder.CloseComponent();
            builder.CloseRegion();
        }
    }

    public partial class IgbFormatSpecifier : IDisposable
    {
        private IContentChildHost? _attachedHost;

        [CascadingParameter(Name = ContentChildHost.CascadingName)]
        private IContentChildHost? ContentChildHostParent { get; set; }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (ContentChildHostParent != null && ContentChildHostParent.AttachContentChild(this))
            {
                _attachedHost = ContentChildHostParent;
            }
        }

        void IDisposable.Dispose()
        {
            _attachedHost?.DetachContentChild(this);
            _attachedHost = null;
            GC.SuppressFinalize(this);
        }
    }
}
