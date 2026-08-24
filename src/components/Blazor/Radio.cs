using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The radio component allows the user to select a single option from an available set of options that are listed side by side.
    /// </summary>
    public partial class IgbRadio : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebRadio"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbRadioModule.IsLoadRequested(IgBlazor))
            {
                IgbRadioModule.Register(IgBlazor);
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
                return "igc-radio";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _required = false;

        /// <summary>
        /// When set, makes the component a required field for validation.
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
        private bool _checked = false;

        /// <summary>
        /// The checked state of the control.
        /// </summary>
        [Parameter]
        public bool Checked
        {
            get { return this._checked; }
            set
            {
                if (this._checked != value || !IsPropDirty("Checked"))
                {
                    MarkPropDirty("Checked");
                }
                this._checked = value;

            }
        }

        /// <summary>
        /// The checked state of the control.
        /// </summary>
        public async Task<bool> GetCurrentCheckedAsync()
        {
            var iv = await InvokeMethod("p:Checked", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// The checked state of the control.
        /// </summary>
        public bool GetCurrentChecked()
        {
            var iv = InvokeMethodSync("p:Checked", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        private ToggleLabelPosition _labelPosition = ToggleLabelPosition.After;

        /// <summary>
        /// The label position of the radio control.
        /// </summary>
        [Parameter]
        public ToggleLabelPosition LabelPosition
        {
            get { return this._labelPosition; }
            set
            {
                if (this._labelPosition != value || !IsPropDirty("LabelPosition"))
                {
                    MarkPropDirty("LabelPosition");
                }
                this._labelPosition = value;

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
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Simulates a click on the radio control.
        /// </summary>
        public async Task ClickAsync()
        {
            await InvokeMethod("click", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Simulates a click on the radio control.
        /// </summary>
        public void Click()
        {
            InvokeMethodSync("click", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Sets focus on the radio control.
        /// </summary>

        [WCWidgetMemberName("Focus")]
        public async Task FocusComponentAsync(IgbFocusOptions options)
        {
            await InvokeMethod("focus", new object?[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Sets focus on the radio control.
        /// </summary>
        [WCWidgetMemberName("Focus")]
        public void FocusComponent(IgbFocusOptions options)
        {
            InvokeMethodSync("focus", new object?[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Removes focus from the radio control.
        /// </summary>

        [WCWidgetMemberName("Blur")]
        public async Task BlurComponentAsync()
        {
            await InvokeMethod("blur", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Removes focus from the radio control.
        /// </summary>
        [WCWidgetMemberName("Blur")]
        public void BlurComponent()
        {
            InvokeMethodSync("blur", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public async Task<bool> CheckValidityAsync()
        {
            var iv = await InvokeMethod("checkValidity", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and emits the invalid event if it's invalid.
        /// </summary>
        public bool CheckValidity()
        {
            var iv = InvokeMethodSync("checkValidity", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public async Task<bool> ReportValidityAsync()
        {
            var iv = await InvokeMethod("reportValidity", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks for validity of the control and shows the browser message if it's invalid.
        /// </summary>
        public bool ReportValidity()
        {
            var iv = InvokeMethodSync("reportValidity", new object?[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public async Task SetCustomValidityAsync(String message)
        {
            await InvokeMethod("setCustomValidity", new object?[] { StringToString(message) }, new string[] { "String" });
        }

        /// <summary>
        /// Sets a custom validation message for the control.
        /// As long as <paramref name="message"/> is not empty, the control is considered invalid.
        /// </summary>
        public void SetCustomValidity(String message)
        {
            InvokeMethodSync("setCustomValidity", new object?[] { StringToString(message) }, new string[] { "String" });
        }

        private EventCallback<bool>? _checkedChanged = null;

        /// <summary>
        /// Emitted when the <see cref="Checked"/> property changes.
        /// Enables two-way binding through <c>@bind-Checked</c>.
        /// </summary>
        [Parameter]
        public EventCallback<bool> CheckedChanged
        {
            get
            {
                return this._checkedChanged != null ? this._checkedChanged.Value : EventCallback<bool>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_checkedChanged))
                    {
                        this.EnsureChangeHandled();

                        _checkedChanged = value;
                    }
                }
                else
                {
                    _checkedChanged = null;
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

        private EventCallback<IgbRadioChangeEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the control's checked state changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbRadioChangeEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbRadioChangeEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbRadioChangeEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueChecked = default(bool);

                            {
                                newValueChecked = (bool)(args!.Detail!.Checked);
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Checked = newValueChecked;
                                }
                                else
                                {
                                    this._checked = newValueChecked;
                                }
                                OnPropertyPropagatedOut(Name, "Checked");
                            }

                            if (!EventCallback<bool>.Empty.Equals(CheckedChanged))
                            {
                                var task = CheckedChanged.InvokeAsync(newValueChecked);
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
                    this.SetHandler<IgbRadioChangeEventArgs>(this.Name, "Change", null);
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
            if (EventCallback<IgbRadioChangeEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbRadioChangeEventArgs>(null, (Action<IgbRadioChangeEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        private string? _focusRef = null;
        private string? _focusScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Focus"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string FocusScript
        {

            set
            {
                if (value != this._focusScript)
                {
                    this._focusScript = value;
                    this.OnRefChanged("Focus", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._focusRef = refName;
                        this.MarkPropDirty("FocusRef");
                    });
                }
            }
            get
            {
                return this._focusScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _focus = null;

        /// <summary>
        /// Emitted when the component gains focus.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Focus
        {
            get
            {
                return this._focus != null ? this._focus.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_focus))
                    {
                        _focus = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", value);
                        this.OnRefChanged("Focus", null, "nativeEvent:::Focus", true, false, (refName, oldValue, newValue) =>
                        {
                            this._focusRef = refName;
                            this.MarkPropDirty("FocusRef");
                        });
                    }
                }
                else
                {
                    _focus = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", null);
                    this.OnRefChanged("Focus", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._focusRef = null;
                        this.MarkPropDirty("FocusRef");
                    });
                }
            }
        }

        private string? _blurRef = null;
        private string? _blurScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Blur"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string BlurScript
        {

            set
            {
                if (value != this._blurScript)
                {
                    this._blurScript = value;
                    this.OnRefChanged("Blur", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._blurRef = refName;
                        this.MarkPropDirty("BlurRef");
                    });
                }
            }
            get
            {
                return this._blurScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _blur = null;

        /// <summary>
        /// Emitted when the component loses focus.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Blur
        {
            get
            {
                return this._blur != null ? this._blur.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_blur))
                    {
                        _blur = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", value);
                        this.OnRefChanged("Blur", null, "nativeEvent:::Blur", true, false, (refName, oldValue, newValue) =>
                        {
                            this._blurRef = refName;
                            this.MarkPropDirty("BlurRef");
                        });
                    }
                }
                else
                {
                    _blur = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", null);
                    this.OnRefChanged("Blur", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._blurRef = null;
                        this.MarkPropDirty("BlurRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Checked"))
            { ser.AddBooleanProp("checked", this._checked); }
            if (IsPropDirty("LabelPosition"))
            { ser.AddEnumProp("labelPosition", this._labelPosition); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }
            if (IsPropDirty("FocusRef"))
            { ser.AddStringProp("focusRef", this._focusRef); }
            if (IsPropDirty("BlurRef"))
            { ser.AddStringProp("blurRef", this._blurRef); }

        }

    }
}
