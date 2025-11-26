using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbAccordion
    {
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

        partial void FindByNameAccordion(string name, ref object item)
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

    public partial class IgbExpansionPanel: IDisposable
    {
        [CascadingParameter(Name = "AccordionParent")]
        protected BaseRendererControl AccordionParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (AccordionParent != null)
            {
                var sv = (IgbAccordion)AccordionParent;
                sv.ContentItems.Remove(this);
            }
        }

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
