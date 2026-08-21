using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelectItem
    {
        [CascadingParameter(Name = "SelectParent")]
        protected BaseRendererControl SelectParent
        {
            get; set;
        }

        public override async ValueTask DisposeAsync()
        {
            if (SelectParent != null)
            {
                var sv = (IgbSelect)SelectParent;
                sv.ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (SelectParent != null)
            {
                var sv = (IgbSelect)SelectParent;
                sv.ContentItems.Add(this);
            }
        }
    }
}
