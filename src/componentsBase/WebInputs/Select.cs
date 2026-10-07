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

        private BaseCollection<BaseRendererControl>? _contentItems = null;

        internal BaseCollection<BaseRendererControl> ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new BaseCollection<BaseRendererControl>(this, "Items");
                }
                return this._contentItems;
            }
        }

    }
}
