namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// How an <see cref="IgbTextarea"/> wraps long lines. Mirrors the HTML textarea <c>wrap</c> attribute.
    /// </summary>
    public enum TextareaWrap
    {
        /// <summary>Lines wrap visually, but the submitted value has no added line breaks.</summary>
        Soft,
        /// <summary>Lines wrap visually and line breaks are inserted into the submitted value at the wrap points. Requires <c>Cols</c> to be set.</summary>
        Hard,
        /// <summary>Lines do not wrap and the control scrolls horizontally.</summary>
        Off

    }
}
