using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class for <see cref="IgbDateTimeInput"/>.
    /// </summary>
    public partial class IgbDateTimeInputBase : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDateTimeInputBase"; } }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Queued; }
        }

        private bool _outlined = false;

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

        /// <summary>
        /// The placeholder text of the control.
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

        /// <summary>
        /// The date format to apply on the input.
        /// </summary>
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

        /// <summary>
        /// Delta values used to increment or decrement each date part on step actions.
        /// All values default to <c>1</c>.
        /// </summary>
        [Parameter]
        public IgbDatePartDeltas SpinDelta
        {
            get { return this._spinDelta; }
            set
            {
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
        private bool _spinLoop = true;

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

        /// <summary>
        /// Makes the control a readonly field.
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

        /// <summary>
        /// The prompt symbol to use for unfilled parts of the mask pattern.
        /// Defaults to <c>_</c>.
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

        /// <summary>
        /// The disabled state of the component.
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

        /// <summary>
        /// Sets the control into invalid state (visual state only).
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

        /// <summary>
        /// Selects all the text inside the input.
        /// </summary>
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

        /// <summary>
        /// Sets focus on the control.
        /// </summary>
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

        /// <summary>
        /// Removes focus from the control.
        /// </summary>
        [WCWidgetMemberName("Blur")]
        public void BlurComponent()
        {
            InvokeMethodSync("blur", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Clears the component of any user input.
        /// </summary>
        public async Task ClearAsync()
        {
            await InvokeMethod("clear", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Clears the component of any user input.
        /// </summary>
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

        /// <summary>
        /// Sets the text selection range of the control.
        /// </summary>
        public async Task SetSelectionRangeAsync(double start = -1, double end = -1, String direction = null)
        {
            await InvokeMethod("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
        }

        /// <summary>
        /// Sets the text selection range of the control.
        /// </summary>
        public void SetSelectionRange(double start = -1, double end = -1, String direction = null)
        {
            InvokeMethodSync("setSelectionRange", new object[] { start, end, StringToString(direction) }, new string[] { "Number", "Number", "String" });
        }

        /// <summary>
        /// Replaces the selected text in the control and re-applies the mask.
        /// </summary>
        public async Task SetRangeTextAsync(String replacement, double start = -1, double end = -1, String selectMode = null)
        {
            await InvokeMethod("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
        }

        /// <summary>
        /// Replaces the selected text in the control and re-applies the mask.
        /// </summary>
        public void SetRangeText(String replacement, double start = -1, double end = -1, String selectMode = null)
        {
            InvokeMethodSync("setRangeText", new object[] { StringToString(replacement), start, end, StringToString(selectMode) }, new string[] { "String", "Number", "Number", "String" });
        }
        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public async Task<bool> ReportValidityAsync()
        {
            var iv = await InvokeMethod("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public bool ReportValidity()
        {
            var iv = InvokeMethodSync("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public async Task<bool> CheckValidityAsync()
        {
            var iv = await InvokeMethod("checkValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public bool CheckValidity()
        {
            var iv = InvokeMethodSync("checkValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public async Task SetCustomValidityAsync(String message)
        {
            await InvokeMethod("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }

        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public void SetCustomValidity(String message)
        {
            InvokeMethodSync("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

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
