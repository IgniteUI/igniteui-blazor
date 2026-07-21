using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTab : BaseRendererControl, IDisposable
    {
        private EventCallback<bool>? _selectedChanged = null;
        [Parameter]
        public EventCallback<bool> SelectedChanged
        {
            get
            {
                return this._selectedChanged != null ? this._selectedChanged.Value : EventCallback<bool>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<string>.Empty))
                {
                    if (!CompareEventCallbacks(value, _selectedChanged, ref eventCallbacksCache))
                    {
                        _selectedChanged = value;
                    }
                }
                else
                {
                    _selectedChanged = null;
                }
            }
        }
    }
}
