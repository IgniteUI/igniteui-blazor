using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A date range defined by a start and an end date.
    /// </summary>
    public partial class IgbDateRangeValue : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDateRangeValue"; } }

        private DateTime _start = DateTime.MinValue;

        /// <summary>
        /// The first date of the range.
        /// </summary>
        [Parameter]
        public DateTime Start
        {
            get { return this._start; }
            set
            {
                if (this._start != value || !IsPropDirty("Start"))
                {
                    MarkPropDirty("Start");
                }
                this._start = value;

            }
        }
        private DateTime _end = DateTime.MinValue;

        /// <summary>
        /// The last date of the range.
        /// </summary>
        [Parameter]
        public DateTime End
        {
            get { return this._end; }
            set
            {
                if (this._end != value || !IsPropDirty("End"))
                {
                    MarkPropDirty("End");
                }
                this._end = value;

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

            if (IsPropDirty("Start"))
            { ser.AddDateTimeProp("start", this._start); }
            if (IsPropDirty("End"))
            { ser.AddDateTimeProp("end", this._end); }

        }

    }
}
