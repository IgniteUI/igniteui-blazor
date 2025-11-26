namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTree
    {
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
                if (this._contentItems == null) {
                    this._contentItems  = new IgbTreeItemCollection(this, "Items");
                }
                return this._contentItems;
            }
        }

        partial void FindByNameTree(string name, ref object item)
        {
            foreach (var it in ContentItems) 
            {
                if (it.Name == name || it.ContainerId == name) {
                    item = it;
                    return;
                }
            }
        }
    }
}