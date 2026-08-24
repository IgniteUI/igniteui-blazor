using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbInputBase : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebInputBase"; } }

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
        private string? _placeholder;

        /// <summary>
        /// The placeholder text of the control.
        /// </summary>
        [Parameter]
        public string? Placeholder
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
        private string? _label;

        /// <summary>
        /// The label for the control.
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
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Selects all the text inside the input.
        /// </summary>
        public async Task SelectAsync()
        {
            await InvokeMethod("select", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Selects all the text inside the input.
        /// </summary>
        public void Select()
        {
            InvokeMethodSync("select", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Sets focus on the control.
        /// </summary>

        [WCWidgetMemberName("Focus")]
        public async Task FocusComponentAsync(IgbFocusOptions options)
        {
            await InvokeMethod("focus", new object?[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Sets focus on the control.
        /// </summary>
        [WCWidgetMemberName("Focus")]
        public void FocusComponent(IgbFocusOptions options)
        {
            InvokeMethodSync("focus", new object?[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Removes focus from the control.
        /// </summary>

        [WCWidgetMemberName("Blur")]
        public async Task BlurComponentAsync()
        {
            await InvokeMethod("blur", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Removes focus from the control.
        /// </summary>
        [WCWidgetMemberName("Blur")]
        public void BlurComponent()
        {
            InvokeMethodSync("blur", new object?[] { }, new string[] { });
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

        private string? _inputOcurredRef = null;
        private string? _inputOcurredScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="InputOcurred"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string InputOcurredScript
        {

            set
            {
                if (value != this._inputOcurredScript)
                {
                    this._inputOcurredScript = value;
                    this.OnRefChanged("InputOcurred", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._inputOcurredRef = refName;
                        this.MarkPropDirty("InputOcurredRef");
                    });
                }
            }
            get
            {
                return this._inputOcurredScript;
            }
        }

        private EventCallback<IgbComponentValueChangedEventArgs>? _inputOcurred = null;

        /// <summary>
        /// Emitted when the control input receives user input.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> InputOcurred
        {
            get
            {
                return this._inputOcurred != null ? this._inputOcurred.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_inputOcurred))
                    {
                        _inputOcurred = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputOcurred", value, (args) =>
                        {
                            RaiseValueChanging(args);
                        });
                        this.OnRefChanged("InputOcurred", null, "event:::InputOcurred", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputOcurredRef = refName;
                            this.MarkPropDirty("InputOcurredRef");
                        });
                    }
                }
                else
                {
                    _inputOcurred = null;
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "InputOcurred", null);
                    this.OnRefChanged("InputOcurred", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputOcurredRef = null;
                        this.MarkPropDirty("InputOcurredRef");
                    });
                }
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

            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("Placeholder"))
            { ser.AddStringProp("placeholder", this._placeholder); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("InputOcurredRef"))
            { ser.AddStringProp("inputOcurredRef", this._inputOcurredRef); }
            if (IsPropDirty("FocusRef"))
            { ser.AddStringProp("focusRef", this._focusRef); }
            if (IsPropDirty("BlurRef"))
            { ser.AddStringProp("blurRef", this._blurRef); }

        }

    }
}
