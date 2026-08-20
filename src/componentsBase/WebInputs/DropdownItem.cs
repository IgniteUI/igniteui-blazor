using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDropdownItem
    {
        [CascadingParameter(Name = "DropdownParent")]
        protected BaseRendererControl DropdownParent
        {
            get; set;
        }

        public override async ValueTask DisposeAsync()
        {
            if (DropdownParent != null)
            {
                var sv = (IgbDropdown)DropdownParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
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
