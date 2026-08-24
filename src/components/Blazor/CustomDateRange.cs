using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A predefined date range with label for <see cref="IgbDateRangePicker.CustomRanges"/>.
    /// </summary>
    public partial class IgbCustomDateRange : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCustomDateRange"; } }

        private string? _label;

        /// <summary>
        /// The text rendered in the chip for this range.
        /// </summary>
        [Parameter]
        public string? Label
        {
            get { return this._label; }
            set
            {
                if (this._label != value || !IsPropDirty("Label"))
                {
                    MarkPropDirty("Label");
                }
                this._label = value;

            }
        }
        private IgbDateRangeValue? _dateRange;

        /// <summary>
        /// The date range applied when the chip is selected.
        /// </summary>
        [Parameter]
        public IgbDateRangeValue? DateRange
        {
            get { return this._dateRange; }
            set
            {
                MarkPropDirty("DateRange");
                if (this._dateRange != null)
                {
                    this.DetachChild(this._dateRange);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._dateRange = value;
            }

        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("DateRange"))
            { ser.AddSerializableProp("dateRange", this._dateRange); }

        }

    }
}
