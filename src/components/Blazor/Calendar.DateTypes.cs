using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Date-typed parameters for <see cref="IgbCalendar"/>.
    /// </summary>
    /// <remarks>
    /// Each date the calendar exposes has sibling parameters, and the one the consumer sets decides
    /// how the value crosses to the client: a <c>DateOnly</c> is a calendar date that crosses as
    /// <c>yyyy-MM-dd</c> and is never converted, a <c>DateTimeOffset</c> is an instant that keeps its
    /// offset, and a <see cref="DateTime"/> behaves by its <see cref="DateTime.Kind"/>.
    /// Setting one clears the others.
    /// </remarks>
    public partial class IgbCalendar
    {
        private DateOnly? _valueAsDateOnly = null;
        private DateTimeOffset? _valueAsDateTimeOffset = null;

        /// <summary>
        /// The current value of the calendar as a calendar date, which is never converted between
        /// timezones.
        /// </summary>
        [Parameter]
        public DateOnly? ValueAsDateOnly
        {
            get { return this._valueAsDateOnly; }
            set
            {
                if (this._valueAsDateOnly != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._valueAsDateOnly = value;
                this._valueAsDateTimeOffset = null;
                this._value = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue;
            }
        }

        /// <summary>
        /// The current value of the calendar as an instant, which keeps its offset across the
        /// round trip.
        /// </summary>
        [Parameter]
        public DateTimeOffset? ValueAsDateTimeOffset
        {
            get { return this._valueAsDateTimeOffset; }
            set
            {
                if (this._valueAsDateTimeOffset != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._valueAsDateTimeOffset = value;
                this._valueAsDateOnly = null;
                this._value = value?.DateTime ?? DateTime.MinValue;
            }
        }

        private DateOnly[]? _valuesAsDateOnly = null;

        /// <summary>
        /// The current values of the calendar as calendar dates, for multiple and range selection.
        /// </summary>
        [Parameter]
        public DateOnly[]? ValuesAsDateOnly
        {
            get { return this._valuesAsDateOnly; }
            set
            {
                MarkPropDirty("Values");
                this._valuesAsDateOnly = value;
                this._values = value == null
                    ? Array.Empty<DateTime>()
                    : Array.ConvertAll(value, d => d.ToDateTime(TimeOnly.MinValue));
            }
        }

        private DateOnly? _activeDateAsDateOnly = null;

        /// <summary>
        /// The date shown and highlighted in the calendar, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? ActiveDateAsDateOnly
        {
            get { return this._activeDateAsDateOnly; }
            set
            {
                if (this._activeDateAsDateOnly != value || !IsPropDirty("ActiveDate"))
                {
                    MarkPropDirty("ActiveDate");
                }
                this._activeDateAsDateOnly = value;
                this._activeDate = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the current value of the calendar as a calendar date.
        /// </summary>
        public async Task<DateOnly> GetCurrentValueAsDateOnlyAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToDateOnly(iv);
        }

        /// <summary>
        /// Gets the current value of the calendar as an instant.
        /// </summary>
        public async Task<DateTimeOffset> GetCurrentValueAsDateTimeOffsetAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToDateTimeOffset(iv);
        }
    }
}
