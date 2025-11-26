namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTileManager
    {
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

        partial void FindByNameTileManager(string name, ref object item)
        {
            foreach (var it in ContentItems)
            {
                if (it.Name == name || it.ContainerId == name)
                {
                    item = it;
                    return;
                }
            }
        }
    }
}
