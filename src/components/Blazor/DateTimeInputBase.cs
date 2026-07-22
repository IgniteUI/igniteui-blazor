using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDateTimeInputBase : BaseRendererControl
    {
        public override string Type { get { return "WebDateTimeInputBase"; } }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Queued; }
        }

        public IgbDateTimeInputBase() : base()
        {
            OnCreatedIgbDateTimeInputBase();

        }

        partial void OnCreatedIgbDateTimeInputBase();

        private bool _outlined = false;

        partial void OnOutlinedChanging(ref bool newValue);
        /// <summary>
        /// Whether the control will have outlined appearance.
        /// </summary>
        [Parameter]
        public bool Outlined
        {
            get { return this._outlined; }
            set
            {
                if (this._outlined != value || !IsPropDirty("Outlined"))
                {
                    MarkPropDirty("Outlined");
                }
                this._outlined = value;

            }
        }
        private string _placeholder;

        partial void OnPlaceholderChanging(ref string newValue);
        /// <summary>
        /// The placeholder attribute of the control.
        /// </summary>
        [Parameter]
        public string Placeholder
        {
            get { return this._placeholder; }
            set
            {
                if (this._placeholder != value || !IsPropDirty("Placeholder"))
                {
                    MarkPropDirty("Placeholder");
                }
                this._placeholder = value;

            }
        }
        private string _label;

        partial void OnLabelChanging(ref string newValue);
        /// <summary>
        /// The label for the control.
        /// </summary>
        [Parameter]
        public string Label
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
        private string _inputFormat;

        partial void OnInputFormatChanging(ref string newValue);
        [Parameter]
        public string InputFormat
        {
            get { return this._inputFormat; }
            set
            {
                if (this._inputFormat != value || !IsPropDirty("InputFormat"))
                {
                    MarkPropDirty("InputFormat");
                }
                this._inputFormat = value;

            }
        }
        private DateTime? _min = DateTime.MinValue;

        partial void OnMinChanging(ref DateTime? newValue);
        /// <summary>
        /// The minimum value required for the input to remain valid.
        /// </summary>
        [Parameter]
        public DateTime? Min
        {
            get { return this._min; }
            set
            {
                if (this._min != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._min = value;

            }
        }
        private DateTime? _max = DateTime.MinValue;

        partial void OnMaxChanging(ref DateTime? newValue);
        /// <summary>
        /// The maximum value required for the input to remain valid.
        /// </summary>
        [Parameter]
        public DateTime? Max
        {
            get { return this._max; }
            set
            {
                if (this._max != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._max = value;

            }
        }
        private string _displayFormat;

        partial void OnDisplayFormatChanging(ref string newValue);
        /// <summary>
        /// Format to display the value in when not editing.
        /// Defaults to the locale format if not set.
        /// </summary>
        [Parameter]
        public string DisplayFormat
        {
            get { return this._displayFormat; }
            set
            {
                if (this._displayFormat != value || !IsPropDirty("DisplayFormat"))
                {
                    MarkPropDirty("DisplayFormat");
                }
                this._displayFormat = value;

            }
        }
        private IgbDatePartDeltas _spinDelta;

        partial void OnSpinDeltaChanging(ref IgbDatePartDeltas newValue);
        /// <summary>
        /// Delta values used to increment or decrement each date part on step actions.
        /// All values default to `1`.
        /// </summary>
        [Parameter]
        public IgbDatePartDeltas SpinDelta
        {
            get { return this._spinDelta; }
            set
            {
                OnSpinDeltaChanging(ref value);
                MarkPropDirty("SpinDelta");
                if (this._spinDelta != null)
                {
                    this.DetachChild(this._spinDelta);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._spinDelta = value;
            }

        }
        private bool _spinLoop = false;

        partial void OnSpinLoopChanging(ref bool newValue);
        /// <summary>
        /// Sets whether to loop over the currently spun segment.
        /// </summary>
        [Parameter]
        public bool SpinLoop
        {
            get { return this._spinLoop; }
            set
            {
                if (this._spinLoop != value || !IsPropDirty("SpinLoop"))
                {
                    MarkPropDirty("SpinLoop");
                }
                this._spinLoop = value;

            }
        }
        private string _locale;

        partial void OnLocaleChanging(ref string newValue);
        /// <summary>
        /// Gets/Sets the locale used for formatting the display value.
        /// </summary>
        [Parameter]
        public string Locale
        {
            get { return this._locale; }
            set
            {
                if (this._locale != value || !IsPropDirty("Locale"))
                {
                    MarkPropDirty("Locale");
                }
                this._locale = value;

            }
        }
        private bool _readOnly = false;

        partial void OnReadOnlyChanging(ref bool newValue);
        /// <summary>
        /// Makes the control a readonly field.
        /// @default false
        /// </summary>
        [Parameter]
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set
            {
                if (this._readOnly != value || !IsPropDirty("ReadOnly"))
                {
                    MarkPropDirty("ReadOnly");
                }
                this._readOnly = value;

            }
        }
        private string _mask;

        partial void OnMaskChanging(ref string newValue);
        /// <summary>
        /// The mask pattern of the component.
        /// </summary>
        [Parameter]
        public string Mask
        {
            get { return this._mask; }
            set
            {
                if (this._mask != value || !IsPropDirty("Mask"))
                {
                    MarkPropDirty("Mask");
                }
                this._mask = value;

            }
        }
        private string _prompt;

        partial void OnPromptChanging(ref string newValue);
        /// <summary>
        /// The prompt symbol to use for unfilled parts of the mask pattern.
        /// @default '_'
        /// </summary>
        [Parameter]
        public string Prompt
        {
            get { return this._prompt; }
            set
            {
                if (this._prompt != value || !IsPropDirty("Prompt"))
                {
                    MarkPropDirty("Prompt");
                }
                this._prompt = value;

            }
        }
        private bool _disabled = false;

        partial void OnDisabledChanging(ref bool newValue);
        /// <summary>
        /// The disabled state of the component
        /// </summary>
        [Parameter]
        public bool Disabled
        {
            get { return this._disabled; }
            set
            {
                if (this._disabled != value || !IsPropDirty("Disabled"))
                {
                    MarkPropDirty("Disabled");
                }
                this._disabled = value;

            }
        }
        private bool _required = false;

        partial void OnRequiredChanging(ref bool newValue);
        /// <summary>
        /// Makes the control a required field in a form context.
        /// </summary>
        [Parameter]
        public bool Required
        {
            get { return this._required; }
            set
            {
                if (this._required != value || !IsPropDirty("Required"))
                {
                    MarkPropDirty("Required");
                }
                this._required = value;

            }
        }
        private bool _invalid = false;

        partial void OnInvalidChanging(ref bool newValue);
        /// <summary>
        /// Control the validity of the control.
        /// </summary>
        [Parameter]
        public bool Invalid
        {
            get { return this._invalid; }
            set
            {
                if (this._invalid != value || !IsPropDirty("Invalid"))
                {
                    MarkPropDirty("Invalid");
                }
                this._invalid = value;

            }
        }

        partial void FindByNameDateTimeInputBase(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameDateTimeInputBase(name, ref item);
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
        /// <summary>
        /// Selects all the text inside the input.
        /// </summary>
        public async Task SelectAsync()
        {
            await InvokeMethod("select", new object[] { }, new string[] { });
        }
        public void Select()
        {
            InvokeMethodSync("select", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Sets focus on the control.
        /// </summary>

        [WCWidgetMemberName("Focus")]
        public async Task FocusComponentAsync(IgbFocusOptions options)
        {
            await InvokeMethod("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        [WCWidgetMemberName("Focus")]
        public void FocusComponent(IgbFocusOptions options)
        {
            InvokeMethodSync("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Removes focus from the control.
        /// </summary>

        [WCWidgetMemberName("Blur")]
        public async Task BlurComponentAsync()
        {
            await InvokeMethod("blur", new object[] { }, new string[] { });
        }

        [WCWidgetMemberName("Blur")]
        public void BlurComponent()
        {
            InvokeMethodSync("blur", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Clears the input element of user input.
        /// </summary>
        public async Task ClearAsync()
        {
            await InvokeMethod("clear", new object[] { }, new string[] { });
        }
        public void Clear()
        {
            InvokeMethodSync("clear", new object[] { }, new string[] { });
        }
        public async Task<bool> HasDatePartsAsync()
        {
            var iv = await InvokeMethod("hasDateParts", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public bool HasDateParts()
        {
            var iv = InvokeMethodSync("hasDateParts", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public async Task<bool> HasTimePartsAsync()
        {
            var iv = await InvokeMethod("hasTimeParts", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public bool HasTimeParts()
        {
            var iv = InvokeMethodSync("hasTimeParts", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        public async Task SetSelectionRangeAsync(double start = -1, double end = -1, String direction = null)
        {
            await InvokeMethod("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
        }
        public void SetSelectionRange(double start = -1, double end = -1, String direction = null)
        {
            InvokeMethodSync("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
        }
        public async Task SetRangeTextAsync(String replacement, double start = -1, double end = -1, String selectMode = null)
        {
            await InvokeMethod("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
        }
        public void SetRangeText(String replacement, double start = -1, double end = -1, String selectMode = null)
        {
            InvokeMethodSync("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
        }
        /// <summary>
        /// Checks for validity of the control and shows the browser message if it invalid.
        /// </summary>
        public async Task ReportValidityAsync()
        {
            await InvokeMethod("reportValidity", new object[] { }, new string[] { });
        }
        public void ReportValidity()
        {
            InvokeMethodSync("reportValidity", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it invalid.
        /// </summary>
        public async Task CheckValidityAsync()
        {
            await InvokeMethod("checkValidity", new object[] { }, new string[] { });
        }
        public void CheckValidity()
        {
            InvokeMethodSync("checkValidity", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as `message` is not empty, the control is considered invalid.
        /// </summary>
        public async Task SetCustomValidityAsync(String message)
        {
            await InvokeMethod("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }
        public void SetCustomValidity(String message)
        {
            InvokeMethodSync("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }

        partial void SerializeCoreIgbDateTimeInputBase(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbDateTimeInputBase(ser);

            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("Placeholder"))
            { ser.AddStringProp("placeholder", this._placeholder); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("InputFormat"))
            { ser.AddStringProp("inputFormat", this._inputFormat); }
            if (IsPropDirty("Min"))
            { ser.AddDateTimeProp("min", this._min); }
            if (IsPropDirty("Max"))
            { ser.AddDateTimeProp("max", this._max); }
            if (IsPropDirty("DisplayFormat"))
            { ser.AddStringProp("displayFormat", this._displayFormat); }
            if (IsPropDirty("SpinDelta"))
            { ser.AddSerializableProp("spinDelta", this._spinDelta); }
            if (IsPropDirty("SpinLoop"))
            { ser.AddBooleanProp("spinLoop", this._spinLoop); }
            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("ReadOnly"))
            { ser.AddBooleanProp("readOnly", this._readOnly); }
            if (IsPropDirty("Mask"))
            { ser.AddStringProp("mask", this._mask); }
            if (IsPropDirty("Prompt"))
            { ser.AddStringProp("prompt", this._prompt); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }

        }

    }
}
