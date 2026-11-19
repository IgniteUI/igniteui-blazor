namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// When the user's selection changes of an <see cref="IgbCombo{TValue, TItem}"/> apply in multiple selection mode.
    /// </summary>
    public enum ComboSelectionCommit
    {
        /// <summary>Each change applies at once and emits <see cref="IgbCombo{TValue, TItem}.Change"/>.</summary>
        Immediate,
        /// <summary>
        /// The changes build a pending selection while the list is open. Enter or the Apply button applies it with
        /// one <see cref="IgbCombo{TValue, TItem}.Change"/>; any other close drops it.
        /// </summary>
        Confirm

    }
}
