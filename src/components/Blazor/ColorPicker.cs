using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Color input component.
    /// The user picks a color with the HSV saturation/value canvas, the hue slider
    /// and the optional alpha slider, or types a color string: hex, rgb(a), hsl(a)
    /// or a named CSS color. Supports pre-defined swatches and the native EyeDropper
    /// API, where the browser provides one.
    /// </summary>
    public partial class IgbColorPicker : IgbBaseComboBox
    {
        /// <inheritdoc />
        public override string Type { get { return "WebColorPicker"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbColorPickerModule.IsLoadRequested(IgBlazor))
            {
                IgbColorPickerModule.Register(IgBlazor);
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

        private string? _value;

        /// <summary>
        /// The value of the component as a CSS color string. Accepts hex, rgb(a),
        /// hsl(a) and named colors. An empty, whitespace-only or invalid string clears the value.
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
        /// Returns the current value of the component.
        /// </summary>
        public async Task<string?> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Returns the current value of the component.
        /// </summary>
        public string? GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }
        private string _label;

        /// <summary>
        /// The label of the component.
        /// In input mode the component forwards the label to the anchor input;
        /// in default mode it renders the label as a separate element.
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
        private ColorFormat _format = ColorFormat.Hex;

        /// <summary>
        /// Sets the color format of the string value.
        /// A format change renders the value in the new notation. The color does not
        /// change, so the component emits no input or change event.
        /// </summary>
        [Parameter]
        public ColorFormat Format
        {
            get { return this._format; }
            set
            {
                if (this._format != value || !IsPropDirty("Format"))
                {
                    MarkPropDirty("Format");
                }
                this._format = value;

            }
        }
        private bool _hideFormats = false;

        /// <summary>
        /// Whether to hide the format picker buttons.
        /// </summary>
        [Parameter]
        public bool HideFormats
        {
            get { return this._hideFormats; }
            set
            {
                if (this._hideFormats != value || !IsPropDirty("HideFormats"))
                {
                    MarkPropDirty("HideFormats");
                }
                this._hideFormats = value;

            }
        }
        private bool _showAlpha = false;

        /// <summary>
        /// Whether to show the alpha slider and input.
        /// </summary>
        [Parameter]
        public bool ShowAlpha
        {
            get { return this._showAlpha; }
            set
            {
                if (this._showAlpha != value || !IsPropDirty("ShowAlpha"))
                {
                    MarkPropDirty("ShowAlpha");
                }
                this._showAlpha = value;

            }
        }
        private ColorPickerMode _mode = ColorPickerMode.Default;

        /// <summary>
        /// The mode of the color picker.
        /// In default mode the anchor is a trigger button; in input mode the anchor is
        /// an editable text field with a color swatch prefix that also opens the picker.
        /// </summary>
        [Parameter]
        public ColorPickerMode Mode
        {
            get { return this._mode; }
            set
            {
                if (this._mode != value || !IsPropDirty("Mode"))
                {
                    MarkPropDirty("Mode");
                }
                this._mode = value;

            }
        }
        private string[] _swatches;

        /// <summary>
        /// Pre-defined color strings. The component renders them as clickable
        /// swatches below the picker controls. A click on a swatch commits its color
        /// as the value.
        /// </summary>
        [Parameter]
        public string[] Swatches
        {
            get { return this._swatches; }
            set
            {
                if (this._swatches != value || !IsPropDirty("Swatches"))
                {
                    MarkPropDirty("Swatches");
                }
                this._swatches = value;

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
        /// <summary>
        /// Checks the validity of the control and moves the focus to it if it is not valid.
        /// </summary>
        public async Task<bool> ReportValidityAsync()
        {
            var iv = await InvokeMethod("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks the validity of the control and moves the focus to it if it is not valid.
        /// </summary>
        public bool ReportValidity()
        {
            var iv = InvokeMethodSync("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Checks the validity of the control and emits the invalid event if it is invalid.
        /// </summary>
        public async Task<bool> CheckValidityAsync()
        {
            var iv = await InvokeMethod("checkValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks the validity of the control and emits the invalid event if it is invalid.
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

        private EventCallback<string?>? _valueChanged = null;

        /// <summary>
        /// Emitted when the <see cref="Value"/> property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<string?>.Empty;
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

        private string _changeRef = null;
        private string _changeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Change"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ChangeScript
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
        /// Emitted when the value of the component is committed.
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
                            var newValueValue = default(string?);

                            {
                                newValueValue = (string?)(args.Detail);
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

                            if (!EventCallback<string?>.Empty.Equals(ValueChanged))
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

        private string _inputRef = null;
        private string _inputScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Input"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string InputScript
        {

            set
            {
                if (value != this._inputScript)
                {
                    this._inputScript = value;
                    this.OnRefChanged("Input", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputRef = refName;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
            get
            {
                return this._inputScript;
            }
        }

        private EventCallback<IgbComponentValueChangedEventArgs>? _input = null;

        /// <summary>
        /// Emitted when the value of the component is changed.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> Input
        {
            get
            {
                return this._input != null ? this._input.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_input))
                    {
                        _input = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Input", value);
                        this.OnRefChanged("Input", null, "event:::Input", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputRef = refName;
                            this.MarkPropDirty("InputRef");
                        });
                    }
                }
                else
                {
                    _input = null;
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Input", null);
                    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputRef = null;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
        }

        private string _openingRef = null;
        private string _openingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Opening"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string OpeningScript
        {

            set
            {
                if (value != this._openingScript)
                {
                    this._openingScript = value;
                    this.OnRefChanged("Opening", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._openingRef = refName;
                        this.MarkPropDirty("OpeningRef");
                    });
                }
            }
            get
            {
                return this._openingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _opening = null;

        /// <summary>
        /// Emitted just before the picker dropdown is open.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Opening
        {
            get
            {
                return this._opening != null ? this._opening.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_opening))
                    {
                        _opening = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", value);
                        this.OnRefChanged("Opening", null, "event:::Opening", true, false, (refName, oldValue, newValue) =>
                        {
                            this._openingRef = refName;
                            this.MarkPropDirty("OpeningRef");
                        });
                    }
                }
                else
                {
                    _opening = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", null);
                    this.OnRefChanged("Opening", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._openingRef = null;
                        this.MarkPropDirty("OpeningRef");
                    });
                }
            }
        }

        private string _openedRef = null;
        private string _openedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Opened"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string OpenedScript
        {

            set
            {
                if (value != this._openedScript)
                {
                    this._openedScript = value;
                    this.OnRefChanged("Opened", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._openedRef = refName;
                        this.MarkPropDirty("OpenedRef");
                    });
                }
            }
            get
            {
                return this._openedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _opened = null;

        /// <summary>
        /// Emitted after the picker dropdown is open.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Opened
        {
            get
            {
                return this._opened != null ? this._opened.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_opened))
                    {
                        _opened = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", value);
                        this.OnRefChanged("Opened", null, "event:::Opened", true, false, (refName, oldValue, newValue) =>
                        {
                            this._openedRef = refName;
                            this.MarkPropDirty("OpenedRef");
                        });
                    }
                }
                else
                {
                    _opened = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", null);
                    this.OnRefChanged("Opened", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._openedRef = null;
                        this.MarkPropDirty("OpenedRef");
                    });
                }
            }
        }

        private string _closingRef = null;
        private string _closingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ClosingScript
        {

            set
            {
                if (value != this._closingScript)
                {
                    this._closingScript = value;
                    this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._closingRef = refName;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
            get
            {
                return this._closingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closing = null;

        /// <summary>
        /// Emitted just before the picker dropdown is closed.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closing
        {
            get
            {
                return this._closing != null ? this._closing.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_closing))
                    {
                        _closing = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value);
                        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closingRef = refName;
                            this.MarkPropDirty("ClosingRef");
                        });
                    }
                }
                else
                {
                    _closing = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
                    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closingRef = null;
                        this.MarkPropDirty("ClosingRef");
                    });
                }
            }
        }

        private string _closedRef = null;
        private string _closedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Closed"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ClosedScript
        {

            set
            {
                if (value != this._closedScript)
                {
                    this._closedScript = value;
                    this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._closedRef = refName;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
            get
            {
                return this._closedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _closed = null;

        /// <summary>
        /// Emitted after closing the picker dropdown.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Closed
        {
            get
            {
                return this._closed != null ? this._closed.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_closed))
                    {
                        _closed = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value);
                        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) =>
                        {
                            this._closedRef = refName;
                            this.MarkPropDirty("ClosedRef");
                        });
                    }
                }
                else
                {
                    _closed = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
                    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._closedRef = null;
                        this.MarkPropDirty("ClosedRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Format"))
            { ser.AddEnumProp("format", this._format); }
            if (IsPropDirty("HideFormats"))
            { ser.AddBooleanProp("hideFormats", this._hideFormats); }
            if (IsPropDirty("ShowAlpha"))
            { ser.AddBooleanProp("showAlpha", this._showAlpha); }
            if (IsPropDirty("Mode"))
            { ser.AddEnumProp("mode", this._mode); }
            if (IsPropDirty("Swatches"))
            { ser.AddArrayProp("swatches", this._swatches); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }
            if (IsPropDirty("InputRef"))
            { ser.AddStringProp("inputRef", this._inputRef); }
            if (IsPropDirty("OpeningRef"))
            { ser.AddStringProp("openingRef", this._openingRef); }
            if (IsPropDirty("OpenedRef"))
            { ser.AddStringProp("openedRef", this._openedRef); }
            if (IsPropDirty("ClosingRef"))
            { ser.AddStringProp("closingRef", this._closingRef); }
            if (IsPropDirty("ClosedRef"))
            { ser.AddStringProp("closedRef", this._closedRef); }

        }

    }
}
