using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelectItem<TValue>
    {
        /// <summary>
        /// The owning <see cref="IgbSelect{TValue}"/>, supplied as a cascading parameter.
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
                ((IgbSelect<TValue>)SelectParent).ContentItems.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (SelectParent != null)
            {
                ((IgbSelect<TValue>)SelectParent).ContentItems.Add(this);
            }
        }
    }
}
