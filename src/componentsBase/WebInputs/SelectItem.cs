using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelectItem : IDisposable
    {
        [CascadingParameter(Name = "SelectParent")]
        protected BaseRendererControl SelectParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (SelectParent != null)
            {
                var sv = (IgbSelect)SelectParent;
                sv.ContentItems.Remove(this);
            }
        }

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
