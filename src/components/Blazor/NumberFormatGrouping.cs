namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// When a formatted number uses grouping separators, such as thousands separators.
    /// Mirrors the <c>useGrouping</c> option of <c>Intl.NumberFormat</c>.
    /// </summary>
    public enum NumberFormatGrouping
    {
        /// <summary>As the locale prefers.</summary>
        Auto,
        /// <summary>Always, even where the locale prefers otherwise.</summary>
        Always,
        /// <summary>Only when there are at least two digits in a group.</summary>
        Min2,
        /// <summary>Never.</summary>
        Never

    }
}
