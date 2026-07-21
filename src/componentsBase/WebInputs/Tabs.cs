using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTabs : BaseRendererControl
    {

        partial void OnCreatedIgbTabs()
        {
            EnsureChangeHandled();
        }

        partial void OnHandlingChange(IgbTabComponentEventArgs args)
        {
            var selectedTab = args.Detail;
            // add check in case something triggers event without args.
            if (selectedTab == null)
            {
                return;
            }

            foreach (var item in this.ActualTabsCollection.ToArray())
            {
                if (item == args.Detail)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;

                }

                if (!EventCallback<string>.Empty.Equals(item.SelectedChanged))
                {
                    var task = item.SelectedChanged.InvokeAsync(item.Selected);
                    if (task.Exception != null)
                    {
                        throw task.Exception;
                    }
                }
            }

        }

        internal void EnsureChangeHandled()
        {
            if (EventCallback<IgbTabComponentEventArgs>.Empty.Equals(this.Change))
            {

                this.Change = new EventCallback<IgbTabComponentEventArgs>(null, (Action<IgbTabComponentEventArgs>)((e) => { }));
                this._change = null;
            }
        }
    }
}
