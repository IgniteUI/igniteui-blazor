namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelect<TValue>
    {
        /// <inheritdoc />
        private protected override string ParentTypeName
        {
            get
            {
                return "SelectParent";
            }
        }

        private BaseCollection<IgbComponentBase>? _contentItems = null;

        internal BaseCollection<IgbComponentBase> ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new BaseCollection<IgbComponentBase>(this, "Items");
                }
                return this._contentItems;
            }
        }

    }
}
