namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Format in which an <see cref="IgbMaskInput"/> exposes its value.
    /// </summary>
    public enum MaskInputValueMode
    {
        /// <summary>Returns clean input, e.g. <c>5551234567</c>.</summary>
        Raw,
        /// <summary>Returns with mask formatting, e.g. <c>(555) 123-4567</c>.</summary>
        WithFormatting

    }
}
