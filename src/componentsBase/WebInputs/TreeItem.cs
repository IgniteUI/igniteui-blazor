using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTreeItem
    {
        [CascadingParameter(Name = "TreeParent")]
        protected BaseRendererControl TreeParent
        {
            get; set;
        }

        public override async ValueTask DisposeAsync()
        {
            if (TreeParent != null)
            {
                var sv = (IgbTree)TreeParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (TreeParent != null)
            {
                var sv = (IgbTree)TreeParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}
