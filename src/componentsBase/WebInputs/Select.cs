namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelect
    {
        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "SelectParent";
            }
        }

        private BaseCollection<IgbSelectItem>? _contentItems = null;

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

    }
}
