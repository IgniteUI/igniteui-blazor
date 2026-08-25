namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTileManager
    {
        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "TileManagerParent";
            }
        }

        private BaseCollection<IgbTile> _contentItems = null;

        internal BaseCollection<IgbTile> ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new BaseCollection<IgbTile>(this, "Items");
                }
                return this._contentItems;
            }
        }

    }
}
