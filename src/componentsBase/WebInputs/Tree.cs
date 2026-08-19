namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTree
    {
        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "TreeParent";
            }
        }

        private IgbTreeItemCollection _contentItems = null;

        public IgbTreeItemCollection ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new IgbTreeItemCollection(this, "Items");
                }
                return this._contentItems;
            }
        }

    }
}
