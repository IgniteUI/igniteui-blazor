using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Unifies one or more <see cref="IgbRadio"/> components into a single group.
    /// </summary>
    public partial class IgbRadioGroup : BaseRendererControl
    {
        public override string Type { get { return "WebRadioGroup"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbRadioGroupModule.IsLoadRequested(IgBlazor))
            {
                IgbRadioGroupModule.Register(IgBlazor);
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
                return "igc-radio-group";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private ContentOrientation _alignment = ContentOrientation.Vertical;

        /// <summary>
        /// Alignment of the radio controls inside this group.
        /// </summary>
        [Parameter]
        public ContentOrientation Alignment
        {
            get { return this._alignment; }
            set
            {
                if (this._alignment != value || !IsPropDirty("Alignment"))
                {
                    MarkPropDirty("Alignment");
                }
                this._alignment = value;

            }
        }
        private string _value;

        /// <summary>
        /// The value of the group, reflecting the value of the currently checked <see cref="IgbRadio"/> button.
        /// Setting it checks the <see cref="IgbRadio"/> button in the group with a matching value.
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

        /// <summary>
        /// Gets the current value of the group.
        /// </summary>
        /// <returns>The value of the checked <see cref="IgbRadio"/>.</returns>
        public async Task<string> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Gets the current value of the group.
        /// </summary>
        /// <returns>The value of the checked <see cref="IgbRadio"/>.</returns>
        public string GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
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

        private EventCallback<IgbRadioChangeEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the checked state of a radio button in the group changes.
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
                            var newValueValue = default(string);

                            {
                                newValueValue = (string)(args.Detail.Value);
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Alignment"))
            { ser.AddEnumProp("alignment", this._alignment); }
            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
