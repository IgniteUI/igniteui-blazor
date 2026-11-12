namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The context of <see cref="IgbCombo{TValue, TItem}.GroupHeaderTemplate"/>: the group a header stands for.
    /// </summary>
    public class IgbComboGroupHeaderContext
    {
        /// <summary>
        /// The value the items in this group share for <see cref="IgbCombo{TValue, TItem}.GroupKey"/>.
        /// </summary>
        public object? Key { get; internal set; }
    }
}
