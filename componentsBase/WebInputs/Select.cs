namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelect
    {
        protected override string ParentTypeName
        {
            get
            {
                return "SelectParent";
            }
        }

        private BaseCollection<IgbSelectItem> _contentItems = null;

        internal BaseCollection<IgbSelectItem> ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new BaseCollection<IgbSelectItem>(this, "Items");
                }
                return this._contentItems;
            }
        }

        partial void FindByNameSelect(string name, ref object item)
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
