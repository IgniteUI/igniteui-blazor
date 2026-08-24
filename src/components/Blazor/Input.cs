using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A highly customizable single-line text field for entering and editing data,
    /// with support for prefix/suffix content, helper text, form integration, and built-in validation.
    /// </summary>
    public partial class IgbInput : IgbInputBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebInput"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbInputModule.IsLoadRequested(IgBlazor))
            {
                IgbInputModule.Register(IgBlazor);
            }
        }

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
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-input";
            }
        }

        private string? _value;

        /// <summary>
        /// The value of the control.
        /// </summary>
        [Parameter]
        public string? Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }

        /// <summary>
        /// Returns the current value of the control.
        /// </summary>
        public async Task<string?> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Returns the current value of the control.
        /// </summary>
        public string? GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }
        private InputType _displayType = InputType.Text;

        /// <summary>
        /// The type attribute of the control.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("Type")]
        public InputType DisplayType
        {
            get { return this._displayType; }
            set
            {
                if (this._displayType != value || !IsPropDirty("DisplayType"))
                {
                    MarkPropDirty("DisplayType");
                }
                this._displayType = value;

            }
        }
        private bool _readOnly = false;

        /// <summary>
        /// Makes the control a readonly field.
        /// </summary>
        [Parameter]
        [WCAttributeName("readonly")]
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
        private string? _inputMode;

        /// <summary>
        /// The input mode attribute of the control.
        /// See the relevant MDN article on
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/inputmode">inputmode</see>.
        /// </summary>
        [Parameter]
        [WCAttributeName("inputmode")]
        public string? InputMode
        {
            get { return this._inputMode; }
            set
            {
                if (this._inputMode != value || !IsPropDirty("InputMode"))
                {
                    MarkPropDirty("InputMode");
                }
                this._inputMode = value;

            }
        }
        private string? _pattern;

        /// <summary>
        /// The pattern attribute of the control.
        /// </summary>
        [Parameter]
        public string? Pattern
        {
            get { return this._pattern; }
            set
            {
                if (this._pattern != value || !IsPropDirty("Pattern"))
                {
                    MarkPropDirty("Pattern");
                }
                this._pattern = value;

            }
        }
        private double? _minLength = 0;

        /// <summary>
        /// The minimum string length required by the control.
        /// </summary>
        [Parameter]
        [WCAttributeName("minlength")]
        public double? MinLength
        {
            get { return this._minLength; }
            set
            {
                if (this._minLength != value || !IsPropDirty("MinLength"))
                {
                    MarkPropDirty("MinLength");
                }
                this._minLength = value;

            }
        }
        private double? _maxLength = 0;

        /// <summary>
        /// The maximum string length of the control.
        /// </summary>
        [Parameter]
        [WCAttributeName("maxlength")]
        public double? MaxLength
        {
            get { return this._maxLength; }
            set
            {
                if (this._maxLength != value || !IsPropDirty("MaxLength"))
                {
                    MarkPropDirty("MaxLength");
                }
                this._maxLength = value;

            }
        }
        private double? _min = 0;

        /// <summary>
        /// The min attribute of the control.
        /// </summary>
        [Parameter]
        public double? Min
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
        private double? _max = 0;

        /// <summary>
        /// The max attribute of the control.
        /// </summary>
        [Parameter]
        public double? Max
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
        private double? _step = 0;

        /// <summary>
        /// The step attribute of the control.
        /// </summary>
        [Parameter]
        public double? Step
        {
            get { return this._step; }
            set
            {
                if (this._step != value || !IsPropDirty("Step"))
                {
                    MarkPropDirty("Step");
                }
                this._step = value;

            }
        }
        private bool _autofocus = false;

        /// <summary>
        /// The autofocus attribute of the control.
        /// </summary>
        [Parameter]
        public bool Autofocus
        {
            get { return this._autofocus; }
            set
            {
                if (this._autofocus != value || !IsPropDirty("Autofocus"))
                {
                    MarkPropDirty("Autofocus");
                }
                this._autofocus = value;

            }
        }
        private string? _autocomplete;

        /// <summary>
        /// The autocomplete attribute of the control.
        /// </summary>
        [Parameter]
        public string? Autocomplete
        {
            get { return this._autocomplete; }
            set
            {
                if (this._autocomplete != value || !IsPropDirty("Autocomplete"))
                {
                    MarkPropDirty("Autocomplete");
                }
                this._autocomplete = value;

            }
        }
        private bool _validateOnly = false;

        /// <summary>
        /// Enables validation rules to be evaluated without restricting user input.
        /// This applies to the <see cref="MaxLength"/> property for string-type inputs, or allows spin buttons
        /// to exceed the predefined <see cref="Min"/> and <see cref="Max"/> limits for number-type inputs.
        /// </summary>
        [Parameter]
        public bool ValidateOnly
        {
            get { return this._validateOnly; }
            set
            {
                if (this._validateOnly != value || !IsPropDirty("ValidateOnly"))
                {
                    MarkPropDirty("ValidateOnly");
                }
                this._validateOnly = value;

            }
        }

        /// <summary>
        /// Increments the numeric value of the input by one or more steps.
        /// </summary>
        public async Task StepUpAsync(double n = -1)
        {
            await InvokeMethod("stepUp", new object?[] { n }, new string[] { "Number" });
        }

        /// <summary>
        /// Increments the numeric value of the input by one or more steps.
        /// </summary>
        public void StepUp(double n = -1)
        {
            InvokeMethodSync("stepUp", new object?[] { n }, new string[] { "Number" });
        }
        /// <summary>
        /// Decrements the numeric value of the input by one or more steps.
        /// </summary>
        public async Task StepDownAsync(double n = -1)
        {
            await InvokeMethod("stepDown", new object?[] { n }, new string[] { "Number" });
        }

        /// <summary>
        /// Decrements the numeric value of the input by one or more steps.
        /// </summary>
        public void StepDown(double n = -1)
        {
            InvokeMethodSync("stepDown", new object?[] { n }, new string[] { "Number" });
        }

        private EventCallback<string>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<string>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_valueChanged))
                    {
                        this.EnsureChangeHandled();

                        _valueChanged = value;
                    }
                }
                else
                {
                    _valueChanged = null;
                }
            }
        }

        private string? _changeRef = null;
        private string? _changeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Change"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string? ChangeScript
        {

            set
            {
                if (value != this._changeScript)
                {
                    this._changeScript = value;
                    this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._changeRef = refName;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
            get
            {
                return this._changeScript;
            }
        }

        private EventCallback<IgbComponentValueChangedEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the control's value changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueValue = default(string);

                            {
                                newValueValue = (string)args.Detail!;
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Value = newValueValue;
                                }
                                else
                                {
                                    this._value = newValueValue;
                                }
                                OnPropertyPropagatedOut(Name, "Value");
                            }

                            if (!EventCallback<string>.Empty.Equals(ValueChanged))
                            {
                                var task = ValueChanged.InvokeAsync(newValueValue);
                                if (task.Exception != null)
                                {
                                    throw task.Exception;
                                }
                            }

                        });
                        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) =>
                        {
                            this._changeRef = refName;
                            this.MarkPropDirty("ChangeRef");
                        });
                    }
                }
                else
                {
                    _change = null;
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Change", null);
                    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._changeRef = null;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
        }
        internal void EnsureChangeHandled()
        {
            if (EventCallback<IgbComponentValueChangedEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbComponentValueChangedEventArgs>(null, (Action<IgbComponentValueChangedEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("DisplayType"))
            { ser.AddEnumProp("displayType", this._displayType); }
            if (IsPropDirty("ReadOnly"))
            { ser.AddBooleanProp("readOnly", this._readOnly); }
            if (IsPropDirty("InputMode"))
            { ser.AddStringProp("inputMode", this._inputMode); }
            if (IsPropDirty("Pattern"))
            { ser.AddStringProp("pattern", this._pattern); }
            if (IsPropDirty("MinLength"))
            { ser.AddNumberProp("minLength", this._minLength); }
            if (IsPropDirty("MaxLength"))
            { ser.AddNumberProp("maxLength", this._maxLength); }
            if (IsPropDirty("Min"))
            { ser.AddNumberProp("min", this._min); }
            if (IsPropDirty("Max"))
            { ser.AddNumberProp("max", this._max); }
            if (IsPropDirty("Step"))
            { ser.AddNumberProp("step", this._step); }
            if (IsPropDirty("Autofocus"))
            { ser.AddBooleanProp("autofocus", this._autofocus); }
            if (IsPropDirty("Autocomplete"))
            { ser.AddStringProp("autocomplete", this._autocomplete); }
            if (IsPropDirty("ValidateOnly"))
            { ser.AddBooleanProp("validateOnly", this._validateOnly); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
