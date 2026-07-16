using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDropdownItem: IDisposable
    {
        [CascadingParameter(Name="DropdownParent")]
        protected BaseRendererControl DropdownParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (DropdownParent != null)
            {
                var sv = (IgbDropdown)DropdownParent;
                sv.ContentItems.Remove(this);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (DropdownParent != null)
            {
                var sv = (IgbDropdown)DropdownParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}