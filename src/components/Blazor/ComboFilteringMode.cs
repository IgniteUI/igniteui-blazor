namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Where the filtering of an <see cref="IgbCombo{TValue, TItem}"/> runs.
    /// </summary>
    public enum ComboFilteringMode
    {
        /// <summary>The combo filters its data as the user types.</summary>
        Local,
        /// <summary>
        /// The combo does not filter its data. Handle <see cref="IgbCombo{TValue, TItem}.SearchInput"/> to search
        /// on the server and set <see cref="IgbCombo{TValue, TItem}.Data"/> to the results, and handle
        /// <see cref="IgbCombo{TValue, TItem}.DataRequest"/> to append more items when the user scrolls near the end.
        /// </summary>
        Remote

    }
}
