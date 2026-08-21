using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbAccordion
    {
        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "AccordionParent";
            }
        }

        private BaseCollection<IgbExpansionPanel> _contentItems = null;

        internal BaseCollection<IgbExpansionPanel> ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new BaseCollection<IgbExpansionPanel>(this, "Items");
                }
                return this._contentItems;
            }
        }

    }

    public partial class IgbExpansionPanel
    {
        [CascadingParameter(Name = "AccordionParent")]
        protected BaseRendererControl AccordionParent
        {
            get; set;
        }

        public override async ValueTask DisposeAsync()
        {
            if (AccordionParent != null)
            {
                var sv = (IgbAccordion)AccordionParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (AccordionParent != null)
            {
                var sv = (IgbAccordion)AccordionParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}
