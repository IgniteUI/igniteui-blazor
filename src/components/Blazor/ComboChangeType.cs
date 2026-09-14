namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Kind of change reported by an <see cref="IgbCombo{T}"/> change event.
    /// </summary>
    public enum ComboChangeType
    {
        /// <summary>One or more items were selected.</summary>
        Selection,
        /// <summary>One or more items were deselected.</summary>
        Deselection,
        /// <summary>A custom value typed by the user was added as an item.</summary>
        Addition

    }
}
