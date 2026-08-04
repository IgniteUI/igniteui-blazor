using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The localized strings used by the date range picker, exposed through
    /// <see cref="IgbDateRangePicker.ResourceStrings"/>.
    /// </summary>
    public partial class IgbDateRangePickerResourceStrings : IgbCalendarResourceStrings
    {
        public override string Type { get { return "WebDateRangePickerResourceStrings"; } }

        public IgbDateRangePickerResourceStrings() : base()
        {
            OnCreatedIgbDateRangePickerResourceStrings();

        }

        partial void OnCreatedIgbDateRangePickerResourceStrings();

        private string _separator;
        /// <summary>
        /// The text shown between the start and end inputs when the date range picker is configured with separate inputs.
        /// </summary>
        [Parameter]
        public string Separator
        {
            get { return this._separator; }
            set
            {
                if (this._separator != value || !IsPropDirty("Separator"))
                {
                    MarkPropDirty("Separator");
                }
                this._separator = value;

            }
        }

        private string _doneButton;
        /// <summary>
        /// Text for the picker button that commits the range selection.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("done")]
        public string DoneButton
        {
            get { return this._doneButton; }
            set
            {
                if (this._doneButton != value || !IsPropDirty("DoneButton"))
                {
                    MarkPropDirty("DoneButton");
                }
                this._doneButton = value;

            }
        }

        private string _cancelButton;
        /// <summary>
        /// Text for the picker button that cancels the range selection.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("cancel")]
        public string CancelButton
        {
            get { return this._cancelButton; }
            set
            {
                if (this._cancelButton != value || !IsPropDirty("CancelButton"))
                {
                    MarkPropDirty("CancelButton");
                }
                this._cancelButton = value;

            }
        }

        partial void FindByNameDateRangePickerResourceStrings(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameDateRangePickerResourceStrings(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbDateRangePickerResourceStrings(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbDateRangePickerResourceStrings(ser);

            if (IsPropDirty("Separator"))
            { ser.AddStringProp("separator", this._separator); }
            if (IsPropDirty("DoneButton"))
            { ser.AddStringProp("doneButton", this._doneButton); }
            if (IsPropDirty("CancelButton"))
            { ser.AddStringProp("cancelButton", this._cancelButton); }
        }

    }
}
