using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Unifies one or more <see cref="IgbRadio{TValue}"/> components into a single group.
    /// </summary>
    [CascadingTypeParameter(nameof(TValue))]
    public partial class IgbRadioGroup<TValue> : BaseRendererControl
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebRadioGroup"; } }

        internal override Type GenericType => Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbRadioGroupModule.IsLoadRequested(IgBlazor))
            {
                IgbRadioGroupModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        private protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        private protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override string DirectRenderElementName
        {
            get
            {
                return "igc-radio-group";
            }
        }

        /// <inheritdoc />
        private protected override ControlEventBehavior DefaultEventBehavior
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
        private TValue? _value;

        /// <summary>
        /// The value of the group, reflecting the value of the currently checked <see cref="IgbRadio{TValue}"/> button.
        /// Setting it checks the <see cref="IgbRadio{TValue}"/> button in the group with a matching value.
        /// </summary>
        [Parameter]
        public TValue? Value
        {
            get { return this._value; }
            set
            {
                if (!EqualityComparer<TValue?>.Default.Equals(this._value, value) || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }
        private string? _name;

        /// <summary>
        /// The name applied to all radio buttons in the group.
        /// </summary>
        [Parameter]
        public string? Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value || !IsPropDirty("Name"))
                {
                    MarkPropDirty("Name");
                }
                this._name = value;

            }
        }

        /// <summary>
        /// Gets the current value of the group.
        /// </summary>
        /// <returns>The value of the checked <see cref="IgbRadio{TValue}"/>.</returns>
        public async Task<TValue> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ConvertToGenericValue<TValue>(iv);
        }

        /// <summary>
        /// Gets the current value of the group.
        /// </summary>
        /// <returns>The value of the checked <see cref="IgbRadio{TValue}"/>.</returns>
        public TValue GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object?[] { }, new string[] { });
            return ConvertToGenericValue<TValue>(iv);
        }

        private EventCallback<TValue>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<TValue>.Empty;
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
        /// Register the function on the client like<br/>
        /// <c>import { registerScript } from './_content/IgniteUI.Blazor/api.js';</c><br/>
        /// <c>registerScript("MyHandler", (args) => { })</c>.
        /// </remarks>
        [Parameter]
        public string? ChangeScript
        {

            set
            {
                if (value != this._changeScript)
                {
                    this._changeScript = value;
                    this.OnRefChanged("Change", null, value, true, false, (string refName, object? oldValue, object? newValue) =>
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

        private EventCallback<IgbRadioChangeEventArgs<TValue>>? _change = null;

        /// <summary>
        /// Emitted when the checked state of a radio button in the group changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbRadioChangeEventArgs<TValue>> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbRadioChangeEventArgs<TValue>>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbRadioChangeEventArgs<TValue>>(this.RendererName, "Change", value, (args) =>
                        {
                            var newValueValue = default(TValue);

                            {
                                newValueValue = args.Detail.Value ?? default(TValue);
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Value = newValueValue;
                                }
                                else
                                {
                                    this._value = newValueValue;
                                }
                                OnPropertyPropagatedOut(RendererName, "Value");
                            }

                            if (!EventCallback<string>.Empty.Equals(ValueChanged))
                            {
                                var task = ValueChanged.InvokeAsync(newValueValue);
                                ObserveHandlerTask(task);
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
                    this.SetHandler<IgbRadioChangeEventArgs<TValue>>(this.RendererName, "Change", null);
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
            if (EventCallback<IgbRadioChangeEventArgs<TValue>>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbRadioChangeEventArgs<TValue>>(null, (Action<IgbRadioChangeEventArgs<TValue>>)((e) => { }));
                this._change = null;
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Alignment"))
            { ser.AddEnumProp("alignment", this._alignment); }
            if (IsPropDirty("Value"))
            { AddGenericValue(ser, "value", this._value); }
            if (IsPropDirty("Name"))
            { ser.AddStringProp("formName", this._name); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
