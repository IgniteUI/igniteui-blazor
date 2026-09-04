using System;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbCombo<T>
    {
        // The combo exposes RenderFragment templates (ItemTemplate / GroupHeaderTemplate)
        // that are hosted client-side through an <igc-template-content> element. That
        // hosting routes back into the Blazor renderer via AdjustDynamicContent, which
        // requires the DynamicContentHolder to be present. Like the grid and charts, the
        // combo therefore needs dynamic content enabled so the holder is created.
        protected override bool NeedsDynamicContent
        {
            get
            {
                return true;
            }
        }
    }
}
