using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDropdownItem
    {
        /// <summary>
        /// The owning <see cref="IgbDropdown"/>, supplied as a cascading parameter.
        /// </summary>
        [CascadingParameter(Name = "DropdownParent")]
        private protected BaseRendererControl? DropdownParent
        {
            get; set;
        }

        /// <inheritdoc/>
        public override async ValueTask DisposeAsync()
        {
            if (DropdownParent != null)
            {
                var sv = (IgbDropdown)DropdownParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
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
