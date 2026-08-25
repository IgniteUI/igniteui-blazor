using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A masked input is an input field where a developer can control user input and format the visible value,
    /// based on configurable rules.
    /// </summary>
    public partial class IgbMaskInput : IgbInputBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebMaskInput"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbMaskInputModule.IsLoadRequested(IgBlazor))
            {
                IgbMaskInputModule.Register(IgBlazor);
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

        private MaskInputValueMode _valueMode = MaskInputValueMode.Raw;

        /// <summary>
        /// Dictates the behavior when retrieving the value of the control.
        /// <list type="bullet">
        ///   <item><description>
        ///   <see cref="MaskInputValueMode.Raw"/> returns the clean input, for example <c>5551234567</c>.
        ///   </description></item>
        ///   <item><description>
        ///   <see cref="MaskInputValueMode.WithFormatting"/> returns the value with the mask formatting
        ///   applied, for example <c>(555) 123-4567</c>.
        ///   </description></item>
        /// </list>
        /// Empty values always return an empty string, regardless of the value mode.
        /// </summary>
        [Parameter]
        public MaskInputValueMode ValueMode
        {
            get { return this._valueMode; }
            set
            {
                if (this._valueMode != value || !IsPropDirty("ValueMode"))
                {
                    MarkPropDirty("ValueMode");
                }
                this._valueMode = value;

            }
        }
        private string? _value;

        /// <summary>
        /// The value of the input.
        /// Regardless of the current <see cref="ValueMode"/>, an empty value returns an empty string.
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
        /// Returns the current value of the input.
        /// Regardless of the current <see cref="ValueMode"/>, an empty value returns an empty string.
        /// </summary>
        public async Task<string?> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Returns the current value of the input.
        /// Regardless of the current <see cref="ValueMode"/>, an empty value returns an empty string.
        /// </summary>
        public string? GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }
        private string? _mask;

        /// <summary>
        /// The masked pattern of the component.
        /// </summary>
        [Parameter]
        public string? Mask
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
        private string? _prompt;

        /// <summary>
        /// The prompt symbol to use for unfilled parts of the mask pattern.
        /// </summary>
        [Parameter]
        public string? Prompt
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

        /// <summary>
        /// Sets the text selection range of the control.
        /// </summary>
        public async Task SetSelectionRangeAsync(double start = -1, double end = -1, String? direction = null)
        {
            await InvokeMethod("setSelectionRange", new object?[] { start, end, StringToString(direction)! }, new string[] { "Number", "Number", "String" });
        }

        /// <summary>
        /// Sets the text selection range of the control.
        /// </summary>
        public void SetSelectionRange(double start = -1, double end = -1, String? direction = null)
        {
            InvokeMethodSync("setSelectionRange", new object?[] { start, end, StringToString(direction)! }, new string[] { "Number", "Number", "String" });
        }

        /// <summary>
        /// Replaces the selected text in the control and re-applies the mask.
        /// </summary>
        public async Task SetRangeTextAsync(String replacement, double start = -1, double end = -1, String? selectMode = null)
        {
            await InvokeMethod("setRangeText", new object?[] { StringToString(replacement)!, start, end, StringToString(selectMode)! }, new string[] { "String", "Number", "Number", "String" });
        }

        /// <summary>
        /// Replaces the selected text in the control and re-applies the mask.
        /// </summary>
        public void SetRangeText(String replacement, double start = -1, double end = -1, String? selectMode = null)
        {
            InvokeMethodSync("setRangeText", new object?[] { StringToString(replacement)!, start, end, StringToString(selectMode)! }, new string[] { "String", "Number", "Number", "String" });
        }

        private EventCallback<string>? _valueChanged = null;

        /// <summary>
        /// Emitted when the <see cref="Value"/> property changes.
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

        private EventCallback<IgbComponentValueChangedEventArgs>? _change = null;

        /// <summary>
        /// Emitted when an alteration of the control's value is committed by the user.
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

            if (IsPropDirty("ValueMode"))
            { ser.AddEnumProp("valueMode", this._valueMode); }
            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Mask"))
            { ser.AddStringProp("mask", this._mask); }
            if (IsPropDirty("Prompt"))
            { ser.AddStringProp("prompt", this._prompt); }
            if (IsPropDirty("ReadOnly"))
            { ser.AddBooleanProp("readOnly", this._readOnly); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
