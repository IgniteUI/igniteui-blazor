using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTreeItem: IDisposable
    {
        [CascadingParameter(Name="TreeParent")]
        protected BaseRendererControl TreeParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (TreeParent != null)
            {
                var sv = (IgbTree)TreeParent;
                sv.ContentItems.Remove(this);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (TreeParent != null)
            {
                var sv = (IgbTree)TreeParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}