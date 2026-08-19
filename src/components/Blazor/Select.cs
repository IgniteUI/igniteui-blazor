using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a control that provides a menu of options.
    /// </summary>
    public partial class IgbSelect : IgbComboBoxBaseLike
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSelect"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSelectModule.IsLoadRequested(IgBlazor))
            {
                IgbSelectModule.Register(IgBlazor);
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
                return "igc-select";
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
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Returns the current value of the control.
        /// </summary>
        public string? GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }
        private bool _outlined = false;

        /// <summary>
        /// Whether the control has an outlined appearance.
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
        private bool _autofocus = false;

        /// <summary>
        /// Whether the control should receive focus automatically.
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
        private double _distance = 0;

        /// <summary>
        /// The distance of the select dropdown from its input.
        /// </summary>
        [Parameter]
        public double Distance
        {
            get { return this._distance; }
            set
            {
                if (this._distance != value || !IsPropDirty("Distance"))
                {
                    MarkPropDirty("Distance");
                }
                this._distance = value;

            }
        }
        private string _label;

        /// <summary>
        /// The label of the control.
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
        private PopoverPlacement _placement = PopoverPlacement.BottomStart;

        /// <summary>
        /// The preferred placement of the select dropdown around its input.
        /// </summary>
        [Parameter]
        public PopoverPlacement Placement
        {
            get { return this._placement; }
            set
            {
                if (this._placement != value || !IsPropDirty("Placement"))
                {
                    MarkPropDirty("Placement");
                }
                this._placement = value;

            }
        }
        private PopoverScrollStrategy _scrollStrategy = PopoverScrollStrategy.Scroll;

        /// <summary>
        /// Determines the behavior of the component during scrolling of the parent container.
        /// </summary>
        [Parameter]
        public PopoverScrollStrategy ScrollStrategy
        {
            get { return this._scrollStrategy; }
            set
            {
                if (this._scrollStrategy != value || !IsPropDirty("ScrollStrategy"))
                {
                    MarkPropDirty("ScrollStrategy");
                }
                this._scrollStrategy = value;

            }
        }

        /// <summary>
        /// Returns the items of the component.
        /// </summary>
        public async Task<IgbSelectItem[]> GetItemsAsync()
        {
            var iv = await InvokeMethod("p:Items", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectItem[]);
            }
            var retVal = ReturnToObjectArray<IgbSelectItem>(iv);
            if (retVal == null)
            {
                return default(IgbSelectItem[]);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the items of the component.
        /// </summary>
        public IgbSelectItem[] GetItems()
        {
            var iv = InvokeMethodSync("p:Items", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectItem[]);
            }
            var retVal = ReturnToObjectArray<IgbSelectItem>(iv);
            if (retVal == null)
            {
                return default(IgbSelectItem[]);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the groups of the component.
        /// </summary>
        public async Task<IgbSelectGroup[]> GetGroupsAsync()
        {
            var iv = await InvokeMethod("p:Groups", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectGroup[]);
            }
            var retVal = ReturnToObjectArray<IgbSelectGroup>(iv);
            if (retVal == null)
            {
                return default(IgbSelectGroup[]);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the groups of the component.
        /// </summary>
        public IgbSelectGroup[] GetGroups()
        {
            var iv = InvokeMethodSync("p:Groups", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectGroup[]);
            }
            var retVal = ReturnToObjectArray<IgbSelectGroup>(iv);
            if (retVal == null)
            {
                return default(IgbSelectGroup[]);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the selected item from the dropdown, or <see langword="null"/> when nothing is selected.
        /// </summary>
        public async Task<IgbSelectItem?> GetSelectedItemAsync()
        {
            var iv = await InvokeMethod("p:SelectedItem", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectItem);
            }
            var retVal = (IgbSelectItem)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbSelectItem);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the selected item from the dropdown, or <see langword="null"/> when nothing is selected.
        /// </summary>
        public IgbSelectItem? GetSelectedItem()
        {
            var iv = InvokeMethodSync("p:SelectedItem", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbSelectItem);
            }
            var retVal = (IgbSelectItem)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbSelectItem);
            }
            return retVal;

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

        /// <inheritdoc />
        public override object FindByName(string name)
        {
            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            foreach (var item in ContentItems)
            {
                if (item.Name == name || item.ContainerId == name)
                {
                    return item;
                }
            }

            return null;
        }
        /// <summary>
        /// Sets focus on the component.
        /// </summary>

        [WCWidgetMemberName("Focus")]
        public async Task FocusComponentAsync(IgbFocusOptions options)
        {
            await InvokeMethod("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Sets focus on the component.
        /// </summary>
        [WCWidgetMemberName("Focus")]
        public void FocusComponent(IgbFocusOptions options)
        {
            InvokeMethodSync("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Removes focus from the component.
        /// </summary>

        [WCWidgetMemberName("Blur")]
        public async Task BlurComponentAsync()
        {
            await InvokeMethod("blur", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Removes focus from the component.
        /// </summary>
        [WCWidgetMemberName("Blur")]
        public void BlurComponent()
        {
            InvokeMethodSync("blur", new object[] { }, new string[] { });
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
        /// Resets the current value and selection of the component.
        /// </summary>
        public async Task ClearSelectionAsync()
        {
            await InvokeMethod("clearSelection", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Resets the current value and selection of the component.
        /// </summary>
        public void ClearSelection()
        {
            InvokeMethodSync("clearSelection", new object[] { }, new string[] { });
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
                if (!value.Equals(EventCallback<string?>.Empty))
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

        private EventCallback<IgbSelectItemComponentEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the selected item changes through user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbSelectItemComponentEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbSelectItemComponentEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbSelectItemComponentEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
                    {
                        _change = value;
                        this.SetHandler<IgbSelectItemComponentEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueValue = default(string?);

                            {
                                newValueValue = (string?)(args.Detail.Value);
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
                    this.SetHandler<IgbSelectItemComponentEventArgs>(this.Name, "Change", null);
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
            if (EventCallback<IgbSelectItemComponentEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbSelectItemComponentEventArgs>(null, (Action<IgbSelectItemComponentEventArgs>)((e) => { }));
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
        /// Emitted just before the list of options is opened.
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _opening, ref eventCallbacksCache))
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
        /// Emitted after the list of options is opened.
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _opened, ref eventCallbacksCache))
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
        /// Emitted just before the list of options is closed.
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _closing, ref eventCallbacksCache))
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
        /// Emitted after the list of options is closed.
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
                if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _closed, ref eventCallbacksCache))
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

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("Autofocus"))
            { ser.AddBooleanProp("autofocus", this._autofocus); }
            if (IsPropDirty("Distance"))
            { ser.AddNumberProp("distance", this._distance); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Placeholder"))
            { ser.AddStringProp("placeholder", this._placeholder); }
            if (IsPropDirty("Placement"))
            { ser.AddEnumProp("placement", this._placement); }
            if (IsPropDirty("ScrollStrategy"))
            { ser.AddEnumProp("scrollStrategy", this._scrollStrategy); }
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
