namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// What an open popup (dropdown, select or combo list, picker, tooltip) does when a container scrolls.
    /// </summary>
    public enum PopoverScrollStrategy
    {
        /// <summary>Stays visible and anchored, also while the anchor is out of view.</summary>
        Scroll,
        /// <summary>Hides while the anchor is fully out of view, and shows again when it comes back.</summary>
        Hide,
        /// <summary>Closes.</summary>
        Close

    }
}
