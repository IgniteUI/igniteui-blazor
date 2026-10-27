namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Selection mode of an <see cref="IgbTree"/>.
    /// </summary>
    public enum TreeSelection
    {
        /// <summary>Items cannot be selected.</summary>
        None,
        /// <summary>Any number of items can be selected, independently of each other.</summary>
        Multiple,
        /// <summary>Selecting an item selects all of its descendants. A parent with a partially selected subtree shows an indeterminate state.</summary>
        Cascade

    }
}
