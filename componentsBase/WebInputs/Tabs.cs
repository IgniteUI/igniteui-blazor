using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTabs : BaseRendererControl
    {

        partial void OnCreatedIgbTabs() {
            EnsureChangeHandled();
        }

        partial void OnHandlingChange(IgbTabComponentEventArgs args) {
            var selectedTab = args.Detail;
            foreach (var item in this.ActualTabsCollection.ToArray())
            {
                if (item == args.Detail)
                {
                    item.Selected = true;
                }
                else {
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
