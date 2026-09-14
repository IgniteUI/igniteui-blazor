namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// How an <see cref="IgbDateRangeDescriptor"/> matches dates against its <see cref="IgbDateRangeDescriptor.DateRange"/>.
    /// </summary>
    public enum DateRangeType
    {
        /// <summary>Dates after the first date in the range.</summary>
        After,
        /// <summary>Dates before the first date in the range.</summary>
        Before,
        /// <summary>Dates between the first and last date in the range, inclusive.</summary>
        Between,
        /// <summary>Only the dates listed in the range.</summary>
        Specific,
        /// <summary>Monday through Friday. The range is ignored.</summary>
        Weekdays,
        /// <summary>Saturday and Sunday. The range is ignored.</summary>
        Weekends

    }
}
