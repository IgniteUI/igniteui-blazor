using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A date range defined by a start and an end date, carried as the payload of
    /// <see cref="IgbDateRangeValueEventArgs"/>.
    /// </summary>
    public partial class IgbDateRangeValueDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDateRangeValueDetail"; } }

        private static bool _marshalByValue = true;

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

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Start"))
            { args["start"] = DateToString(this._start); }
            if (IsPropDirty("End"))
            { args["end"] = DateToString(this._end); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("start"))
            { this.Start = ReturnToDate(args["start"]); }
            if (args.ContainsKey("end"))
            { this.End = ReturnToDate(args["end"]); }

            this.SuppressParentNotify = false;
        }

    }
}
