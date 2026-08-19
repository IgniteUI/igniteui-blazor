using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Groups a series of <see cref="IgbToggleButton"/> components together, exposing features
    /// such as layout and selection.
    /// </summary>
    public partial class IgbButtonGroup : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebButtonGroup"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbButtonGroupModule.IsLoadRequested(IgBlazor))
            {
                IgbButtonGroupModule.Register(IgBlazor);
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
                return "igc-button-group";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _disabled = false;

        /// <summary>
        /// Disables all buttons inside the group.
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
        private ContentOrientation _alignment = ContentOrientation.Horizontal;

        /// <summary>
        /// Sets the orientation of the buttons in the group.
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
        private ButtonGroupSelection _selection = ButtonGroupSelection.Single;

        /// <summary>
        /// Controls the mode of selection for the button group.
        /// </summary>
        [Parameter]
        public ButtonGroupSelection Selection
        {
            get { return this._selection; }
            set
            {
                if (this._selection != value || !IsPropDirty("Selection"))
                {
                    MarkPropDirty("Selection");
                }
                this._selection = value;

            }
        }
        private string[] _selectedItems;

        /// <summary>
        /// Gets or sets the values of the currently selected buttons.
        /// </summary>
        [Parameter]
        public string[] SelectedItems
        {
            get { return this._selectedItems; }
            set
            {
                if (this._selectedItems != value || !IsPropDirty("SelectedItems"))
                {
                    MarkPropDirty("SelectedItems");
                }
                this._selectedItems = value;

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

        private EventCallback<IgbComponentValueChangedEventArgs>? _select = null;

        /// <summary>
        /// Emitted when a button is selected through user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> Select
        {
            get
            {
                return this._select != null ? this._select.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _select, ref eventCallbacksCache))
                    {
                        _select = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Select", value);
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
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Select", null);
                    this.OnRefChanged("Select", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._selectRef = null;
                        this.MarkPropDirty("SelectRef");
                    });
                }
            }
        }

        private string _deselectRef = null;
        private string _deselectScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Deselect"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string DeselectScript
        {

            set
            {
                if (value != this._deselectScript)
                {
                    this._deselectScript = value;
                    this.OnRefChanged("Deselect", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._deselectRef = refName;
                        this.MarkPropDirty("DeselectRef");
                    });
                }
            }
            get
            {
                return this._deselectScript;
            }
        }

        private EventCallback<IgbComponentValueChangedEventArgs>? _deselect = null;

        /// <summary>
        /// Emitted when a button is deselected through user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentValueChangedEventArgs> Deselect
        {
            get
            {
                return this._deselect != null ? this._deselect.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _deselect, ref eventCallbacksCache))
                    {
                        _deselect = value;
                        this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Deselect", value);
                        this.OnRefChanged("Deselect", null, "event:::Deselect", true, false, (refName, oldValue, newValue) =>
                        {
                            this._deselectRef = refName;
                            this.MarkPropDirty("DeselectRef");
                        });
                    }
                }
                else
                {
                    _deselect = null;
                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Deselect", null);
                    this.OnRefChanged("Deselect", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._deselectRef = null;
                        this.MarkPropDirty("DeselectRef");
                    });
                }
            }
        }

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Alignment"))
            { ser.AddEnumProp("alignment", this._alignment); }
            if (IsPropDirty("Selection"))
            { ser.AddEnumProp("selection", this._selection); }
            if (IsPropDirty("SelectedItems"))
            { ser.AddArrayProp("selectedItems", this._selectedItems); }
            if (IsPropDirty("SelectRef"))
            { ser.AddStringProp("selectRef", this._selectRef); }
            if (IsPropDirty("DeselectRef"))
            { ser.AddStringProp("deselectRef", this._deselectRef); }

        }

    }
}
