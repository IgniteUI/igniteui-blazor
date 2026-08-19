using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Chips help people enter information, make selections, filter content, or trigger actions.
    /// </summary>
    public partial class IgbChip : BaseRendererControl
    {
        public override string Type { get { return "WebChip"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbChipModule.IsLoadRequested(IgBlazor))
            {
                IgbChipModule.Register(IgBlazor);
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
                return "igc-chip";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _disabled = false;

        /// <summary>
        /// Sets the disabled state for the chip.
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
        private bool _removable = false;

        /// <summary>
        /// Defines if the chip is removable or not.
        /// </summary>
        [Parameter]
        public bool Removable
        {
            get { return this._removable; }
            set
            {
                if (this._removable != value || !IsPropDirty("Removable"))
                {
                    MarkPropDirty("Removable");
                }
                this._removable = value;

            }
        }
        private bool _selectable = false;

        /// <summary>
        /// Defines if the chip is selectable or not.
        /// </summary>
        [Parameter]
        public bool Selectable
        {
            get { return this._selectable; }
            set
            {
                if (this._selectable != value || !IsPropDirty("Selectable"))
                {
                    MarkPropDirty("Selectable");
                }
                this._selectable = value;

            }
        }
        private bool _selected = false;

        /// <summary>
        /// Defines if the chip is selected or not.
        /// </summary>
        [Parameter]
        public bool Selected
        {
            get { return this._selected; }
            set
            {
                if (this._selected != value || !IsPropDirty("Selected"))
                {
                    MarkPropDirty("Selected");
                }
                this._selected = value;

            }
        }

        /// <summary>
        /// Returns the current selected state of the component.
        /// </summary>
        public async Task<bool> GetCurrentSelectedAsync()
        {
            var iv = await InvokeMethod("p:Selected", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Returns the current selected state of the component.
        /// </summary>
        public bool GetCurrentSelected()
        {
            var iv = InvokeMethodSync("p:Selected", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        private StyleVariant _variant = StyleVariant.Primary;

        /// <summary>
        /// A property that sets the color variant of the chip component.
        /// </summary>
        [Parameter]
        public StyleVariant Variant
        {
            get { return this._variant; }
            set
            {
                if (this._variant != value || !IsPropDirty("Variant"))
                {
                    MarkPropDirty("Variant");
                }
                this._variant = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        private EventCallback<bool>? _selectedChanged = null;

        /// <summary>
        /// Emitted when the Selected state changes.
        /// Enables two-way binding through <c>@bind-Selected</c>.
        /// </summary>
        [Parameter]
        public EventCallback<bool> SelectedChanged
        {
            get
            {
                return this._selectedChanged != null ? this._selectedChanged.Value : EventCallback<bool>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_selectedChanged))
                    {
                        this.EnsureSelectHandled();

                        _selectedChanged = value;
                    }
                }
                else
                {
                    _selectedChanged = null;
                }
            }
        }

        private string _removeRef = null;
        private string _removeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Remove"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string RemoveScript
        {

            set
            {
                if (value != this._removeScript)
                {
                    this._removeScript = value;
                    this.OnRefChanged("Remove", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._removeRef = refName;
                        this.MarkPropDirty("RemoveRef");
                    });
                }
            }
            get
            {
                return this._removeScript;
            }
        }

        private EventCallback<IgbComponentBoolValueChangedEventArgs>? _remove = null;

        /// <summary>
        /// Emitted when the chip is removed.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentBoolValueChangedEventArgs> Remove
        {
            get
            {
                return this._remove != null ? this._remove.Value : EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_remove))
                    {
                        _remove = value;
                        this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "Remove", value);
                        this.OnRefChanged("Remove", null, "event:::Remove", true, false, (refName, oldValue, newValue) =>
                        {
                            this._removeRef = refName;
                            this.MarkPropDirty("RemoveRef");
                        });
                    }
                }
                else
                {
                    _remove = null;
                    this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "Remove", null);
                    this.OnRefChanged("Remove", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._removeRef = null;
                        this.MarkPropDirty("RemoveRef");
                    });
                }
            }
        }

        private string _selectRef = null;
        private string _selectScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Select"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string SelectScript
        {

            set
            {
                if (value != this._selectScript)
                {
                    this._selectScript = value;
                    this.OnRefChanged("Select", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._selectRef = refName;
                        this.MarkPropDirty("SelectRef");
                    });
                }
            }
            get
            {
                return this._selectScript;
            }
        }

        private EventCallback<IgbComponentBoolValueChangedEventArgs>? _select = null;

        /// <summary>
        /// Emitted when the chip is selected or deselected, after any related animations
        /// and transitions have completed.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentBoolValueChangedEventArgs> Select
        {
            get
            {
                return this._select != null ? this._select.Value : EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_select))
                    {
                        _select = value;
                        this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "Select", value, (args) =>
                        {
                            var newValueSelected = default(bool);

                            {
                                newValueSelected = (bool)(args.Detail);
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Selected = newValueSelected;
                                }
                                else
                                {
                                    this._selected = newValueSelected;
                                }
                                OnPropertyPropagatedOut(Name, "Selected");
                            }

                            if (!EventCallback<bool>.Empty.Equals(SelectedChanged))
                            {
                                var task = SelectedChanged.InvokeAsync(newValueSelected);
                                if (task.Exception != null)
                                {
                                    throw task.Exception;
                                }
                            }

                        });
                        this.OnRefChanged("Select", null, "event:::Select", true, false, (refName, oldValue, newValue) =>
                        {
                            this._selectRef = refName;
                            this.MarkPropDirty("SelectRef");
                        });
                    }
                }
                else
                {
                    _select = null;
                    this.SetHandler<IgbComponentBoolValueChangedEventArgs>(this.Name, "Select", null);
                    this.OnRefChanged("Select", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._selectRef = null;
                        this.MarkPropDirty("SelectRef");
                    });
                }
            }
        }
        internal void EnsureSelectHandled()
        {
            if (EventCallback<IgbComponentBoolValueChangedEventArgs>.Empty.Equals(this.Select))
            {
                this.Select = new EventCallback<IgbComponentBoolValueChangedEventArgs>(null, (Action<IgbComponentBoolValueChangedEventArgs>)((e) => { }));
                this._select = null;
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Removable"))
            { ser.AddBooleanProp("removable", this._removable); }
            if (IsPropDirty("Selectable"))
            { ser.AddBooleanProp("selectable", this._selectable); }
            if (IsPropDirty("Selected"))
            { ser.AddBooleanProp("selected", this._selected); }
            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }
            if (IsPropDirty("RemoveRef"))
            { ser.AddStringProp("removeRef", this._removeRef); }
            if (IsPropDirty("SelectRef"))
            { ser.AddStringProp("selectRef", this._selectRef); }

        }

    }
}
