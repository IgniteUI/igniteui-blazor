namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// What an open popover (dropdown, select list) does when the document scrolls.
    /// </summary>
    public enum PopoverScrollStrategy
    {
        /// <summary>Stays open and moves with its anchor.</summary>
        Scroll,
        /// <summary>Stays open and prevents the scroll.</summary>
        Block,
        /// <summary>Closes.</summary>
        Close

    }
}
