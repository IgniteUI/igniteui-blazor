using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTile
    {
        /// <summary>
        /// The owning <see cref="IgbTileManager"/>, supplied as a cascading parameter.
        /// </summary>
        [CascadingParameter(Name = "TileManagerParent")]
        private protected BaseRendererControl? TileManagerParent
        {
            get; set;
        }

        /// <inheritdoc />
        public override async ValueTask DisposeAsync()
        {
            if (TileManagerParent != null)
            {
                var sv = (IgbTileManager)TileManagerParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
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
