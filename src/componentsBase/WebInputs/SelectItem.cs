using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelectItem
    {
        /// <summary>
        /// The owning <see cref="IgbSelect"/>, supplied as a cascading parameter.
        /// </summary>
        [CascadingParameter(Name = "SelectParent")]
        private protected BaseRendererControl? SelectParent
        {
            get; set;
        }

        /// <inheritdoc />
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
