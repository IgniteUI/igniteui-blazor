namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Browsing context in which a link button opens its <c>Href</c>.
    /// Mirrors the HTML anchor <c>target</c> attribute.
    /// </summary>
    public enum ButtonBaseTarget
    {
        /// <summary>A new tab or window.</summary>
        _blank,
        /// <summary>The parent browsing context; falls back to <see cref="_self"/> if there is none.</summary>
        _parent,
        /// <summary>The current browsing context.</summary>
        _self,
        /// <summary>The top-level browsing context; falls back to <see cref="_self"/> if there is none.</summary>
        _top

    }
}
