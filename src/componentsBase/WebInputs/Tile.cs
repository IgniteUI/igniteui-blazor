using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTile: IDisposable
    {
        [CascadingParameter(Name = "TileManagerParent")]
        protected BaseRendererControl TileManagerParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (TileManagerParent != null)
            {
                var sv = (IgbTileManager)TileManagerParent;
                sv.ContentItems.Remove(this);
            }
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
