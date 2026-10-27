namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Type of the native input rendered by an <see cref="IgbInput"/>.
    /// Mirrors the HTML input <c>type</c> attribute and affects validation and the virtual keyboard shown on touch devices.
    /// </summary>
    public enum InputType
    {
        /// <summary>Plain single-line text.</summary>
        Text,
        /// <summary>An email address.</summary>
        Email,
        /// <summary>A number.</summary>
        Number,
        /// <summary>Masked text.</summary>
        Password,
        /// <summary>A search string.</summary>
        Search,
        /// <summary>A telephone number.</summary>
        Tel,
        /// <summary>A URL.</summary>
        Url

    }
}
