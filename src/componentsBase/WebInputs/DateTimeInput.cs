namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDateTimeInput
    {
        /// <summary>
        /// Increments the date/time portion at the caret.
        /// </summary>
        public async Task StepUpAsync()
        {
            await InvokeMethod("stepUp", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Increments the date/time portion at the caret.
        /// </summary>
        public void StepUp()
        {
            InvokeMethodSync("stepUp", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Increments a date/time portion.
        /// </summary>
        /// <param name="datePart">The portion to increment.</param>
        public async Task StepUpAsync(DatePart datePart)
        {
            await InvokeMethod("stepUp", new object?[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
        /// <summary>
        /// Increments a date/time portion.
        /// </summary>
        /// <param name="datePart">The portion to increment.</param>
        public void StepUp(DatePart datePart)
        {
            InvokeMethodSync("stepUp", new object?[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }

        /// <summary>
        /// Decrements the date/time portion at the caret.
        /// </summary>
        public async Task StepDownAsync()
        {
            await InvokeMethod("stepDown", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Decrements the date/time portion at the caret.
        /// </summary>
        public void StepDown()
        {
            InvokeMethodSync("stepDown", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Decrements a date/time portion.
        /// </summary>
        /// <param name="datePart">The portion to decrement.</param>
        public async Task StepDownAsync(DatePart datePart)
        {
            await InvokeMethod("stepDown", new object?[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
        /// <summary>
        /// Decrements a date/time portion.
        /// </summary>
        /// <param name="datePart">The portion to decrement.</param>
        public void StepDown(DatePart datePart)
        {
            InvokeMethodSync("stepDown", new object?[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
    }
}
