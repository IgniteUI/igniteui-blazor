using System.Globalization;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Formats date values for the JavaScript interop wire.
    /// </summary>
    /// <remarks>
    /// The shape is chosen per type, because a value alone cannot say what it means: a reading of
    /// midnight is a calendar date in one model and an instant in another.
    /// <list type="bullet">
    /// <item><description>
    /// <see cref="DateOnly"/> crosses as <c>yyyy-MM-dd</c>, with no time and no timezone designator.
    /// The client parses it through the local <c>Date</c> constructor, so it can never shift a day.
    /// </description></item>
    /// <item><description>
    /// <see cref="DateTime"/> with <see cref="DateTimeKind.Unspecified"/> is a wall clock value and
    /// crosses as its literal reading with no designator, so the browser shows what .NET holds.
    /// </description></item>
    /// <item><description>
    /// <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> or <see cref="DateTimeKind.Local"/>,
    /// and <see cref="DateTimeOffset"/>, are instants and carry their designator (<c>Z</c> or
    /// <c>±HH:mm</c>), so the browser renders the right moment in the user's zone. The client returns
    /// its own offset, which lets the value be restored as the same instant and the same kind.
    /// </description></item>
    /// </list>
    /// </remarks>
    internal static class DateTimeWireFormat
    {
        private const string DateOnlyFormat = "yyyy-MM-dd";

        // The round-trip ("o") format already encodes the rule for DateTime: no designator for
        // Unspecified, "Z" for Utc and "±HH:mm" for Local.
        private const string RoundTripFormat = "o";

        /// <summary>Returns the wire representation of a calendar date: no time, no timezone.</summary>
        public static string ToWireString(DateOnly value)
        {
            return value.ToString(DateOnlyFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>Returns the wire representation of a calendar date, or <c>null</c> when it has no value.</summary>
        public static string? ToWireString(DateOnly? value)
        {
            return value.HasValue ? ToWireString(value.Value) : null;
        }

        /// <summary>
        /// Returns the wire representation of <paramref name="value"/>: the literal reading when the
        /// kind is <see cref="DateTimeKind.Unspecified"/>, otherwise the reading plus its designator.
        /// </summary>
        public static string ToWireString(DateTime value)
        {
            return value.ToString(RoundTripFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the wire representation of <paramref name="value"/>, or <c>null</c> when it has no value.
        /// </summary>
        public static string? ToWireString(DateTime? value)
        {
            return value.HasValue ? ToWireString(value.Value) : null;
        }

        /// <summary>Returns the wire representation of an instant, always carrying its offset.</summary>
        public static string ToWireString(DateTimeOffset value)
        {
            return value.ToString(RoundTripFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>Returns the wire representation of an instant, or <c>null</c> when it has no value.</summary>
        public static string? ToWireString(DateTimeOffset? value)
        {
            return value.HasValue ? ToWireString(value.Value) : null;
        }

        /// <summary>
        /// Returns the wire representation of a boxed date value of any supported type, or
        /// <c>null</c> when the value is null or is not a date.
        /// </summary>
        public static string? ToWireString(object? value)
        {
            switch (value)
            {
                case DateTime dateTime:
                    return ToWireString(dateTime);
                case DateOnly dateOnly:
                    return ToWireString(dateOnly);
                case DateTimeOffset dateTimeOffset:
                    return ToWireString(dateTimeOffset);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Tells whether <paramref name="value"/> is a date-only payload, that is a bare
        /// <c>yyyy-MM-dd</c> with no time component.
        /// </summary>
        public static bool IsDateOnlyPayload(string? value)
        {
            return value != null && value.Length == 10 && value.IndexOf('T') < 0;
        }
    }
}
