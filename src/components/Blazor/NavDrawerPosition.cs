namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Edge of the viewport an <see cref="IgbNavDrawer"/> is anchored to.
    /// </summary>
    public enum NavDrawerPosition
    {
        /// <summary>The inline-start edge (left in left-to-right layouts).</summary>
        Start,
        /// <summary>The inline-end edge (right in left-to-right layouts).</summary>
        End,
        /// <summary>The top edge.</summary>
        Top,
        /// <summary>The bottom edge.</summary>
        Bottom,
        /// <summary>Not anchored. The drawer is rendered inline in the page flow without a modal backdrop.</summary>
        Relative

    }
}
