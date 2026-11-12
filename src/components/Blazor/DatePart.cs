namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Segment of a date/time value that a date-time input can step up or down.
    /// </summary>
    public enum DatePart
    {
        /// <summary>The month.</summary>
        [WCEnumName("month")]
        Month,
        /// <summary>The year.</summary>
        [WCEnumName("year")]
        Year,
        /// <summary>The day of the month.</summary>
        [WCEnumName("date")]
        Date,
        /// <summary>The hours.</summary>
        [WCEnumName("hours")]
        Hours,
        /// <summary>The minutes.</summary>
        [WCEnumName("minutes")]
        Minutes,
        /// <summary>The seconds.</summary>
        [WCEnumName("seconds")]
        Seconds,
        /// <summary>The AM/PM designator of a 12-hour clock.</summary>
        [WCEnumName("amPm")]
        AmPm

    }
}
