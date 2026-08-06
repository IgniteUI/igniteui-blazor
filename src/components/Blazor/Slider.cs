using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A slider component used to select numeric value within a range.
    /// </summary>
    public partial class IgbSlider : IgbSliderBase
    {
        public override string Type { get { return "WebSlider"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbSliderModule.IsLoadRequested(IgBlazor))
            {
                IgbSliderModule.Register(IgBlazor);
            }
        }

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
                return "igc-slider";
            }
        }

        private double _value = 0;

        partial void OnValueChanging(ref double newValue);
        /// <summary>
        /// The current value of the component.
        /// </summary>
        [Parameter]
        public double Value
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
        /// Gets the current value of the component.
        /// </summary>
        public async Task<double> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Gets the current value of the component.
        /// </summary>
        public double GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
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

        partial void FindByNameSlider(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameSlider(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        /// <summary>
        /// Increments the value of the slider by <c>stepIncrement * step</c>, where
        /// <paramref name="stepIncrement"/> defaults to 1.
        /// </summary>
        /// <param name="stepIncrement">Optional step increment. If no parameter is passed, it defaults to 1.</param>
        public async Task StepUpAsync(double stepIncrement = 1)
        {
            await InvokeMethod("stepUp", new object[] { stepIncrement }, new string[] { "Number" });
        }

        /// <summary>
        /// Increments the value of the slider by <c>stepIncrement * step</c>, where
        /// <paramref name="stepIncrement"/> defaults to 1.
        /// </summary>
        /// <param name="stepIncrement">Optional step increment. If no parameter is passed, it defaults to 1.</param>
        public void StepUp(double stepIncrement = 1)
        {
            InvokeMethodSync("stepUp", new object[] { stepIncrement }, new string[] { "Number" });
        }
        /// <summary>
        /// Decrements the value of the slider by <c>stepDecrement * step</c>, where
        /// <paramref name="stepDecrement"/> defaults to 1.
        /// </summary>
        /// <param name="stepDecrement">Optional step decrement. If no parameter is passed, it defaults to 1.</param>
        public async Task StepDownAsync(double stepDecrement = 1)
        {
            await InvokeMethod("stepDown", new object[] { stepDecrement }, new string[] { "Number" });
        }

        /// <summary>
        /// Decrements the value of the slider by <c>stepDecrement * step</c>, where
        /// <paramref name="stepDecrement"/> defaults to 1.
        /// </summary>
        /// <param name="stepDecrement">Optional step decrement. If no parameter is passed, it defaults to 1.</param>
        public void StepDown(double stepDecrement = 1)
        {
            InvokeMethodSync("stepDown", new object[] { stepDecrement }, new string[] { "Number" });
        }
        /// <summary>
        /// Checks the validity of the control and shows the browser message if it is invalid.
        /// </summary>
        public async Task<bool> ReportValidityAsync()
        {
            var iv = await InvokeMethod("reportValidity", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Checks the validity of the control and shows the browser message if it is invalid.
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

        private EventCallback<double>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<double> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<double>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<double>.Empty))
                {
                    if (!CompareEventCallbacks(value, _valueChanged, ref eventCallbacksCache))
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

        partial void OnHandlingInput(IgbNumberEventArgs args);
        private EventCallback<IgbNumberEventArgs>? _input = null;

        /// <summary>
        /// Emitted when a value is changed via thumb drag or keyboard interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbNumberEventArgs> Input
        {
            get
            {
                return this._input != null ? this._input.Value : EventCallback<IgbNumberEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbNumberEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _input, ref eventCallbacksCache))
                    {
                        _input = value;
                        this.SetHandler<IgbNumberEventArgs>(this.Name, "Input", value, (args) =>
                        {
                            OnHandlingInput(args);

                        });
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
                    this.SetHandler<IgbNumberEventArgs>(this.Name, "Input", null);
                    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputRef = null;
                        this.MarkPropDirty("InputRef");
                    });
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

        partial void OnHandlingChange(IgbNumberEventArgs args);
        private EventCallback<IgbNumberEventArgs>? _change = null;

        /// <summary>
        /// Emitted when a value change is committed on a thumb drag end or keyboard interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbNumberEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbNumberEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbNumberEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
                    {
                        _change = value;
                        this.SetHandler<IgbNumberEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            OnHandlingChange(args);

                            var newValueValue = default(double);

                            {
                                newValueValue = (double)(args.Detail);
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

                            if (!EventCallback<double>.Empty.Equals(ValueChanged))
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
                    this.SetHandler<IgbNumberEventArgs>(this.Name, "Change", null);
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
            if (EventCallback<IgbNumberEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbNumberEventArgs>(null, (Action<IgbNumberEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddNumberProp("value", this._value); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("InputRef"))
            { ser.AddStringProp("inputRef", this._inputRef); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
