namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Format in which an <see cref="IgbMaskInput"/> exposes its value.
    /// </summary>
    public enum MaskInputValueMode
    {
        /// <summary>Only the characters typed by the user, for example <c>5551234567</c>.</summary>
        Raw,
        /// <summary>The value with the mask literals applied, for example <c>(555) 123-4567</c>.</summary>
        WithFormatting

    }
}
