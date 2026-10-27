namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Which part of a tile starts a drag-and-drop reorder in an <see cref="IgbTileManager"/>.
    /// </summary>
    public enum TileManagerDragMode
    {
        /// <summary>Tiles cannot be dragged.</summary>
        None,
        /// <summary>Only the tile header starts a drag.</summary>
        [WCEnumName("tile-header")]
        TileHeader,
        /// <summary>The whole tile starts a drag.</summary>
        Tile

    }
}
