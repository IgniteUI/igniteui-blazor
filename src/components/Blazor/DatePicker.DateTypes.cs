using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Date-typed parameters for <see cref="IgbDatePicker"/>.
    /// </summary>
    /// <remarks>
    /// Each date the picker exposes has three sibling parameters, and the one the consumer sets
    /// decides how the value crosses to the client:
    /// <list type="bullet">
    /// <item><description>
    /// <c>DateOnly</c> is a calendar date. It crosses as <c>yyyy-MM-dd</c> and is never converted,
    /// so it cannot drift to the neighbouring day whatever the browser's timezone is.
    /// </description></item>
    /// <item><description>
    /// <c>DateTimeOffset</c> is an instant. It keeps its offset, so the browser shows the right
    /// moment in the user's zone and the instant survives the round trip.
    /// </description></item>
    /// <item><description>
    /// <see cref="DateTime"/> behaves by its <see cref="DateTime.Kind"/>: <c>Unspecified</c> is a
    /// wall clock value and crosses verbatim, <c>Utc</c> and <c>Local</c> are instants.
    /// </description></item>
    /// </list>
    /// Setting one of them clears the others, and all of them stay readable: the
    /// <see cref="DateTime"/> parameter keeps reporting the same moment regardless of which sibling
    /// was assigned.
    /// </remarks>
    public partial class IgbDatePicker
    {
        private DateOnly? _valueAsDateOnly = null;
        private DateTimeOffset? _valueAsDateTimeOffset = null;

        /// <summary>
        /// The value of the picker as a calendar date, which is never converted between timezones.
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
                this._value = value?.ToDateTime(TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// The value of the picker as an instant, which keeps its offset across the round trip.
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
                this._value = value?.DateTime;
            }
        }

        private DateOnly? _minAsDateOnly = null;
        private DateTimeOffset? _minAsDateTimeOffset = null;

        /// <summary>
        /// The minimum value required for the picker to remain valid, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? MinAsDateOnly
        {
            get { return this._minAsDateOnly; }
            set
            {
                if (this._minAsDateOnly != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._minAsDateOnly = value;
                this._minAsDateTimeOffset = null;
                this._min = value?.ToDateTime(TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// The minimum value required for the picker to remain valid, as an instant.
        /// </summary>
        [Parameter]
        public DateTimeOffset? MinAsDateTimeOffset
        {
            get { return this._minAsDateTimeOffset; }
            set
            {
                if (this._minAsDateTimeOffset != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._minAsDateTimeOffset = value;
                this._minAsDateOnly = null;
                this._min = value?.DateTime;
            }
        }

        private DateOnly? _maxAsDateOnly = null;
        private DateTimeOffset? _maxAsDateTimeOffset = null;

        /// <summary>
        /// The maximum value required for the picker to remain valid, as a calendar date.
        /// </summary>
        [Parameter]
        public DateOnly? MaxAsDateOnly
        {
            get { return this._maxAsDateOnly; }
            set
            {
                if (this._maxAsDateOnly != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._maxAsDateOnly = value;
                this._maxAsDateTimeOffset = null;
                this._max = value?.ToDateTime(TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// The maximum value required for the picker to remain valid, as an instant.
        /// </summary>
        [Parameter]
        public DateTimeOffset? MaxAsDateTimeOffset
        {
            get { return this._maxAsDateTimeOffset; }
            set
            {
                if (this._maxAsDateTimeOffset != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._maxAsDateTimeOffset = value;
                this._maxAsDateOnly = null;
                this._max = value?.DateTime;
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
        /// Gets the current value of the picker as a calendar date.
        /// </summary>
        public async Task<DateOnly?> GetCurrentValueAsDateOnlyAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToDateOnly(iv);
        }

        /// <summary>
        /// Gets the current value of the picker as an instant.
        /// </summary>
        public async Task<DateTimeOffset?> GetCurrentValueAsDateTimeOffsetAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToDateTimeOffset(iv);
        }
    }
}
