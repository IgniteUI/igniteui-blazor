using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Date-typed parameters for <see cref="IgbDateTimeInput"/>.
    /// </summary>
    /// <remarks>
    /// The input edits a date and a time together, so its natural type is a <see cref="DateTime"/>
    /// with <see cref="DateTimeKind.Unspecified"/>, which crosses as a wall clock reading and returns
    /// verbatim. <c>ValueAsDateTimeOffset</c> is for an absolute instant: it keeps its offset, so the
    /// browser shows the right moment in the user's zone and the instant survives the round trip.
    /// </remarks>
    public partial class IgbDateTimeInput
    {
        private DateTimeOffset? _valueAsDateTimeOffset = null;

        /// <summary>
        /// The value of the input as an instant, which keeps its offset across the round trip.
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
                this._value = value?.DateTime;
            }
        }

        /// <summary>
        /// Gets the current value of the input as an instant.
        /// </summary>
        public async Task<DateTimeOffset?> GetCurrentValueAsDateTimeOffsetAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToDateTimeOffset(iv);
        }
    }
}
