using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTile
    {
        [CascadingParameter(Name = "TileManagerParent")]
        protected BaseRendererControl TileManagerParent
        {
            get; set;
        }

        public override async ValueTask DisposeAsync()
        {
            if (TileManagerParent != null)
            {
                var sv = (IgbTileManager)TileManagerParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        protected override async Task OnInitializedAsync()
        {
            if (TileManagerParent != null)
            {
                var sv = (IgbTileManager)TileManagerParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}
