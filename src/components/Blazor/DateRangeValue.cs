using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDateRangeValue : BaseRendererElement
    {
        public override string Type { get { return "WebDateRangeValue"; } }

        public IgbDateRangeValue() : base()
        {
            OnCreatedIgbDateRangeValue();

        }

        partial void OnCreatedIgbDateRangeValue();

        private DateTime _start = DateTime.MinValue;

        partial void OnStartChanging(ref DateTime newValue);
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

        partial void OnEndChanging(ref DateTime newValue);
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

        partial void FindByNameDateRangeValue(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameDateRangeValue(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        partial void SerializeCoreIgbDateRangeValue(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbDateRangeValue(ser);

            if (IsPropDirty("Start"))
            { ser.AddDateTimeProp("start", this._start); }
            if (IsPropDirty("End"))
            { ser.AddDateTimeProp("end", this._end); }

        }

    }
}
