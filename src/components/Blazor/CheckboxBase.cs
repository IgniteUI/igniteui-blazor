using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbCheckbox"/> and <see cref="IgbSwitch"/>.
    /// </summary>
    public partial class IgbCheckboxBase : BaseRendererControl
    {
        public override string Type { get { return "WebCheckboxBase"; } }

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

        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-checkbox-base";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbCheckboxBase() : base()
        {
            OnCreatedIgbCheckboxBase();

        }

        partial void OnCreatedIgbCheckboxBase();

        private string _value;

        partial void OnValueChanging(ref string newValue);
        /// <summary>
        /// The value of the control.
        /// </summary>
        [Parameter]
        public string Value
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

        partial void OnCheckedChanging(ref bool newValue);
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
        /// Gets the checked state of the control.
        /// </summary>
        public async Task<bool> GetCurrentCheckedAsync()
        {
            var iv = await InvokeMethod("p:Checked", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Gets the checked state of the control.
        /// </summary>
        public bool GetCurrentChecked()
        {
            var iv = InvokeMethodSync("p:Checked", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        private ToggleLabelPosition _labelPosition = ToggleLabelPosition.After;

        partial void OnLabelPositionChanging(ref ToggleLabelPosition newValue);
        /// <summary>
        /// The label position of the control.
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

        partial void OnDisabledChanging(ref bool newValue);
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

        partial void FindByNameCheckboxBase(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCheckboxBase(name, ref item);
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
        /// Simulates a click on the control.
        /// </summary>
        public async Task ClickAsync()
        {
            await InvokeMethod("click", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Simulates a click on the control.
        /// </summary>
        public void Click()
        {
            InvokeMethodSync("click", new object[] { }, new string[] { });
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

        private EventCallback<bool>? _checkedChanged = null;

        /// <summary>
        /// Emitted when the Checked property changes.
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
                if (!value.Equals(EventCallback<bool>.Empty))
                {
                    if (!CompareEventCallbacks(value, _checkedChanged, ref eventCallbacksCache))
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

        partial void OnHandlingChange(IgbCheckboxChangeEventArgs args);
        private EventCallback<IgbCheckboxChangeEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the control's checked state changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbCheckboxChangeEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbCheckboxChangeEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbCheckboxChangeEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
                    {
                        _change = value;
                        this.SetHandler<IgbCheckboxChangeEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            OnHandlingChange(args);

                            var newValueChecked = default(bool);

                            {
                                newValueChecked = (bool)(args.Detail.Checked);
                                ;
                                OnEventUpdatingChecked(this._checked, ref newValueChecked);
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
                    this.SetHandler<IgbCheckboxChangeEventArgs>(this.Name, "Change", null);
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
            if (EventCallback<IgbCheckboxChangeEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbCheckboxChangeEventArgs>(null, (Action<IgbCheckboxChangeEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        private string _focusRef = null;
        private string _focusScript = null;

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

        partial void OnHandlingFocus(IgbVoidEventArgs args);
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _focus, ref eventCallbacksCache))
                    {
                        _focus = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", value, (args) =>
                        {
                            OnHandlingFocus(args);

                        });
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

        private string _blurRef = null;
        private string _blurScript = null;

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

        partial void OnHandlingBlur(IgbVoidEventArgs args);
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _blur, ref eventCallbacksCache))
                    {
                        _blur = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", value, (args) =>
                        {
                            OnHandlingBlur(args);

                        });
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

        partial void OnEventUpdatingChecked(bool oldValue, ref bool newValue);

        partial void SerializeCoreIgbCheckboxBase(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbCheckboxBase(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Checked"))
            { ser.AddBooleanProp("checked", this._checked); }
            if (IsPropDirty("LabelPosition"))
            { ser.AddEnumProp("labelPosition", this._labelPosition); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
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
