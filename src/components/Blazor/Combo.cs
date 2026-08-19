using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The Combo component is similar to <see cref="IgbSelect"/> in that it provides a list of options
    /// from which the user can make a selection.
    /// In contrast to the Select component, the Combo component displays all options in a virtualized
    /// list of items, meaning the combo box can simultaneously show thousands of options, where one or
    /// more options can be selected.
    /// Additionally, users can create custom item templates, allowing for robust data visualization.
    /// The Combo component features case-sensitive filtering, grouping, complex data binding,
    /// dynamic addition of values and more.
    /// </summary>
    public partial class IgbCombo<T> : IgbBaseComboBox
    {
        public override string Type { get { return "WebCombo"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbComboModule.IsLoadRequested(IgBlazor))
            {
                IgbComboModule.Register(IgBlazor);
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

        private string _dataRef;
        private Object _data;

        /// <summary>
        /// The data source used to generate the list of options.
        /// </summary>
        [Parameter]
        public Object Data
        {
            get { return this._data; }

            set
            {
                var oldValue = this._data;

                if (oldValue != value || !IsPropDirty("Data"))
                {
                    MarkPropDirty("Data");
                    this._data = value;
                    this.OnRefChanged("Data", oldValue, value, false, false, (string refName, object old, object newValue) =>
                    {
                        this._dataRef = refName;
                        this.MarkPropDirty("DataRef");
                    });
                }
            }
        }

        private string _dataScript;

        ///<summary>Provides a means of setting Data in the JavaScript environment.</summary>
        [Parameter]
        public string DataScript
        {
            get { return _dataScript; }

            set
            {
                var oldValue = this._dataScript;
                if (oldValue != value || !IsPropDirty("Data"))
                {
                    this._dataScript = value;
                    MarkPropDirty("Data");
                    this.OnRefChanged("Data", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._dataRef = refName;
                        this.MarkPropDirty("DataRef");
                    });
                }
            }
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
        private bool _singleSelect = false;

        /// <summary>
        /// Enables single selection mode and moves item filtering to the main input.
        /// </summary>
        [Parameter]
        public bool SingleSelect
        {
            get { return this._singleSelect; }
            set
            {
                if (this._singleSelect != value || !IsPropDirty("SingleSelect"))
                {
                    MarkPropDirty("SingleSelect");
                }
                this._singleSelect = value;

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
        private bool _autofocusList = false;

        /// <summary>
        /// Focuses the list of options when the menu opens.
        /// </summary>
        [Parameter]
        public bool AutofocusList
        {
            get { return this._autofocusList; }
            set
            {
                if (this._autofocusList != value || !IsPropDirty("AutofocusList"))
                {
                    MarkPropDirty("AutofocusList");
                }
                this._autofocusList = value;

            }
        }
        private string _locale;

        /// <summary>
        /// Gets/Sets the locale used for getting language, affecting resource strings.
        /// </summary>
        [Parameter]
        public string Locale
        {
            get { return this._locale; }
            set
            {
                if (this._locale != value || !IsPropDirty("Locale"))
                {
                    MarkPropDirty("Locale");
                }
                this._locale = value;

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
        private string _placeholderSearch;

        /// <summary>
        /// The placeholder text of the search input.
        /// </summary>
        [Parameter]
        public string PlaceholderSearch
        {
            get { return this._placeholderSearch; }
            set
            {
                if (this._placeholderSearch != value || !IsPropDirty("PlaceholderSearch"))
                {
                    MarkPropDirty("PlaceholderSearch");
                }
                this._placeholderSearch = value;

            }
        }
        private string? _valueKey;

        /// <summary>
        /// The key in the data source used when selecting items.
        /// </summary>
        [Parameter]
        public string? ValueKey
        {
            get { return this._valueKey; }
            set
            {
                if (this._valueKey != value || !IsPropDirty("ValueKey"))
                {
                    MarkPropDirty("ValueKey");
                }
                this._valueKey = value;

            }
        }
        private string? _displayKey;

        /// <summary>
        /// The key in the data source used to display items in the list.
        /// </summary>
        [Parameter]
        public string? DisplayKey
        {
            get { return this._displayKey; }
            set
            {
                if (this._displayKey != value || !IsPropDirty("DisplayKey"))
                {
                    MarkPropDirty("DisplayKey");
                }
                this._displayKey = value;

            }
        }
        private string _groupKey;

        /// <summary>
        /// The key in the data source used to group items in the list.
        /// </summary>
        [Parameter]
        public string GroupKey
        {
            get { return this._groupKey; }
            set
            {
                if (this._groupKey != value || !IsPropDirty("GroupKey"))
                {
                    MarkPropDirty("GroupKey");
                }
                this._groupKey = value;

            }
        }
        private GroupingDirection _groupSorting = GroupingDirection.Asc;

        /// <summary>
        /// Sorts the items in each group by ascending or descending order.
        /// </summary>
        [Parameter]
        public GroupingDirection GroupSorting
        {
            get { return this._groupSorting; }
            set
            {
                if (this._groupSorting != value || !IsPropDirty("GroupSorting"))
                {
                    MarkPropDirty("GroupSorting");
                }
                this._groupSorting = value;

            }
        }
        private IgbFilteringOptions _filteringOptions;

        /// <summary>
        /// An object that configures the filtering of the combo.
        /// </summary>
        [Parameter]
        public IgbFilteringOptions FilteringOptions
        {
            get { return this._filteringOptions; }
            set
            {
                MarkPropDirty("FilteringOptions");
                if (this._filteringOptions != null)
                {
                    this.DetachChild(this._filteringOptions);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._filteringOptions = value;
            }

        }
        private bool _caseSensitiveIcon = false;

        /// <summary>
        /// Enables the case sensitive search icon in the filtering input.
        /// </summary>
        [Parameter]
        public bool CaseSensitiveIcon
        {
            get { return this._caseSensitiveIcon; }
            set
            {
                if (this._caseSensitiveIcon != value || !IsPropDirty("CaseSensitiveIcon"))
                {
                    MarkPropDirty("CaseSensitiveIcon");
                }
                this._caseSensitiveIcon = value;

            }
        }
        private bool _disableFiltering = false;

        /// <summary>
        /// Disables the filtering of the list of options.
        /// </summary>
        [Parameter]
        public bool DisableFiltering
        {
            get { return this._disableFiltering; }
            set
            {
                if (this._disableFiltering != value || !IsPropDirty("DisableFiltering"))
                {
                    MarkPropDirty("DisableFiltering");
                }
                this._disableFiltering = value;

            }
        }
        private bool _disableClear = false;

        /// <summary>
        /// Hides the clear button.
        /// </summary>
        [Parameter]
        public bool DisableClear
        {
            get { return this._disableClear; }
            set
            {
                if (this._disableClear != value || !IsPropDirty("DisableClear"))
                {
                    MarkPropDirty("DisableClear");
                }
                this._disableClear = value;

            }
        }
        private T[] _value;

        /// <summary>
        /// The value of the control, that is the currently selected items.
        /// If the data source is an array of complex objects, <see cref="ValueKey"/> must be set.
        /// Note that when <see cref="DisplayKey"/> is not explicitly set, it falls back to the value
        /// of <see cref="ValueKey"/>.
        /// </summary>
        [Parameter]
        public T[] Value
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
        /// Returns the current value of the combo.
        /// </summary>
        /// <returns>The selected values, represented by <see cref="ValueKey"/> when provided.</returns>
        public async Task<T[]> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToObjectArray(iv).Cast<T>().ToArray();
        }

        /// <summary>
        /// Returns the current value of the combo.
        /// </summary>
        /// <returns>The selected values, represented by <see cref="ValueKey"/> when provided.</returns>
        public T[] GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToObjectArray(iv).Cast<T>().ToArray();
        }
        private string _selectionRef;

        /// <summary>
        /// Returns the current selection of the combo.
        /// </summary>
        /// <returns>The selected items as provided in the <see cref="Data"/> source.</returns>
        public async Task<object[]> GetSelectionAsync()
        {
            var iv = await InvokeMethod("p:Selection", new object[] { }, new string[] { });
            return ReturnToObjectArray(iv);
        }

        /// <summary>
        /// Returns the current selection of the combo.
        /// </summary>
        /// <returns>The selected items as provided in the <see cref="Data"/> source.</returns>
        public object[] GetSelection()
        {
            var iv = InvokeMethodSync("p:Selection", new object[] { }, new string[] { });
            return ReturnToObjectArray(iv);
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
        private string _itemTemplateRef;
        private RenderFragment<object> _itemTemplate;

        /// <summary>
        /// The template used for the content of each combo item.
        /// </summary>
        [Parameter]
        public RenderFragment<object> ItemTemplate
        {
            get { return this._itemTemplate; }

            set
            {
                var oldValue = this._itemTemplate;
                if (oldValue != value || !IsPropDirty("ItemTemplate"))
                {
                    MarkPropDirty("ItemTemplate");
                    this._itemTemplate = value;
                    this._itemTemplateTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._itemTemplateTemplateId, this._itemTemplate, typeof(object));
                    this.OnRefChanged("ItemTemplate", null, "template:::" + this._itemTemplateTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._itemTemplateRef = refName;
                        this.MarkPropDirty("ItemTemplateRef");
                    });
                }
            }
        }

        private string _itemTemplateTemplateId;
        private string _itemTemplateScript;

        /// <summary>
        /// Name of a client-side function that renders the template used for the content of each combo item.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyTemplate", function (ctx) { return ...; }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ItemTemplateScript
        {
            get { return _itemTemplateScript; }

            set
            {
                var oldValue = this._itemTemplateScript;
                if (oldValue != value || !IsPropDirty("ItemTemplate"))
                {
                    this._itemTemplateScript = value;
                    MarkPropDirty("ItemTemplate");
                    this.OnRefChanged("ItemTemplate", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._itemTemplateRef = refName;
                        this.MarkPropDirty("ItemTemplateRef");
                    });
                }
            }
        }
        private string _groupHeaderTemplateRef;
        private RenderFragment<object> _groupHeaderTemplate;

        /// <summary>
        /// The template used for the content of each combo group header.
        /// </summary>
        [Parameter]
        public RenderFragment<object> GroupHeaderTemplate
        {
            get { return this._groupHeaderTemplate; }

            set
            {
                var oldValue = this._groupHeaderTemplate;
                if (oldValue != value || !IsPropDirty("GroupHeaderTemplate"))
                {
                    MarkPropDirty("GroupHeaderTemplate");
                    this._groupHeaderTemplate = value;
                    this._groupHeaderTemplateTemplateId = Guid.NewGuid().ToString();
                    this.UpdateTemplate(this._groupHeaderTemplateTemplateId, this._groupHeaderTemplate, typeof(object));
                    this.OnRefChanged("GroupHeaderTemplate", null, "template:::" + this._groupHeaderTemplateTemplateId, true, false, (string refName, object old, object newValue) =>
                    {
                        this._groupHeaderTemplateRef = refName;
                        this.MarkPropDirty("GroupHeaderTemplateRef");
                    });
                }
            }
        }

        private string _groupHeaderTemplateTemplateId;
        private string _groupHeaderTemplateScript;

        /// <summary>
        /// Name of a client-side function that renders the template used for the content of each combo group header.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyTemplate", function (ctx) { return ...; }, false)</c>.
        /// </remarks>
        [Parameter]
        public string GroupHeaderTemplateScript
        {
            get { return _groupHeaderTemplateScript; }

            set
            {
                var oldValue = this._groupHeaderTemplateScript;
                if (oldValue != value || !IsPropDirty("GroupHeaderTemplate"))
                {
                    this._groupHeaderTemplateScript = value;
                    MarkPropDirty("GroupHeaderTemplate");
                    this.OnRefChanged("GroupHeaderTemplate", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._groupHeaderTemplateRef = refName;
                        this.MarkPropDirty("GroupHeaderTemplateRef");
                    });
                }
            }
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
        /// Selects one or more options in the list by either reference or <see cref="ValueKey"/>.
        /// If no items are provided all items are selected.
        /// </summary>
        /// <param name="items">One or more items to be selected. When <see cref="ValueKey"/> is specified,
        /// the corresponding value should be used in place of the item reference.</param>
        public async Task SelectAsync(object[] items)
        {
            await InvokeMethod("select", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
        }

        /// <summary>
        /// Selects one or more options in the list by either reference or <see cref="ValueKey"/>.
        /// If no items are provided all items are selected.
        /// </summary>
        /// <param name="items">One or more items to be selected. When <see cref="ValueKey"/> is specified,
        /// the corresponding value should be used in place of the item reference.</param>
        public void Select(object[] items)
        {
            InvokeMethodSync("select", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
        }

        /// <summary>
        /// Deselects one or more options in the list by either reference or <see cref="ValueKey"/>.
        /// If no items are provided all items are deselected.
        /// </summary>
        /// <param name="items">One or more items to be deselected. When <see cref="ValueKey"/> is specified,
        /// the corresponding value should be used in place of the item reference.</param>
        public async Task DeselectAsync(object[] items)
        {
            await InvokeMethod("deselect", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
        }

        /// <summary>
        /// Deselects one or more options in the list by either reference or <see cref="ValueKey"/>.
        /// If no items are provided all items are deselected.
        /// </summary>
        /// <param name="items">One or more items to be deselected. When <see cref="ValueKey"/> is specified,
        /// the corresponding value should be used in place of the item reference.</param>
        public void Deselect(object[] items)
        {
            InvokeMethodSync("deselect", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
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

        private EventCallback<T[]>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<T[]> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<T[]>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<T[]>.Empty))
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

        private EventCallback<IgbComboChangeEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the control's selection has changed.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComboChangeEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbComboChangeEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbComboChangeEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
                    {
                        _change = value;
                        this.SetHandler<IgbComboChangeEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueValue = default(T[]);

                            {
                                newValueValue = (T[])(DowncastArray<T>(args.Detail.NewValue));
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

                            if (!EventCallback<T[]>.Empty.Equals(ValueChanged))
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
                    this.SetHandler<IgbComboChangeEventArgs>(this.Name, "Change", null);
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
            if (EventCallback<IgbComboChangeEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbComboChangeEventArgs>(null, (Action<IgbComboChangeEventArgs>)((e) => { }));
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("DataRef"))
            { ser.AddStringProp("dataRef", this._dataRef); }
            if (IsPropDirty("Outlined"))
            { ser.AddBooleanProp("outlined", this._outlined); }
            if (IsPropDirty("SingleSelect"))
            { ser.AddBooleanProp("singleSelect", this._singleSelect); }
            if (IsPropDirty("Autofocus"))
            { ser.AddBooleanProp("autofocus", this._autofocus); }
            if (IsPropDirty("AutofocusList"))
            { ser.AddBooleanProp("autofocusList", this._autofocusList); }
            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Placeholder"))
            { ser.AddStringProp("placeholder", this._placeholder); }
            if (IsPropDirty("PlaceholderSearch"))
            { ser.AddStringProp("placeholderSearch", this._placeholderSearch); }
            if (IsPropDirty("ValueKey"))
            { ser.AddStringProp("valueKey", this._valueKey); }
            if (IsPropDirty("DisplayKey"))
            { ser.AddStringProp("displayKey", this._displayKey); }
            if (IsPropDirty("GroupKey"))
            { ser.AddStringProp("groupKey", this._groupKey); }
            if (IsPropDirty("GroupSorting"))
            { ser.AddEnumProp("groupSorting", this._groupSorting); }
            if (IsPropDirty("FilteringOptions"))
            { ser.AddSerializableProp("filteringOptions", this._filteringOptions); }
            if (IsPropDirty("CaseSensitiveIcon"))
            { ser.AddBooleanProp("caseSensitiveIcon", this._caseSensitiveIcon); }
            if (IsPropDirty("DisableFiltering"))
            { ser.AddBooleanProp("disableFiltering", this._disableFiltering); }
            if (IsPropDirty("DisableClear"))
            { ser.AddBooleanProp("disableClear", this._disableClear); }
            if (IsPropDirty("Value"))
            { ser.AddArrayProp("value", this._value); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Required"))
            { ser.AddBooleanProp("required", this._required); }
            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("ItemTemplateRef"))
            { ser.AddStringProp("itemTemplateRef", this._itemTemplateRef); }
            if (IsPropDirty("GroupHeaderTemplateRef"))
            { ser.AddStringProp("groupHeaderTemplateRef", this._groupHeaderTemplateRef); }
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
