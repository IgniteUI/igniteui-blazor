using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The tree-item component represents a child item of the tree component or another tree item.
    /// </summary>
    public partial class IgbTreeItem : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTreeItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbTreeModule.IsLoadRequested(IgBlazor))
            {
                IgbTreeModule.Register(IgBlazor);
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
                return "igc-tree-item";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private IgbTreeItem? _parent;

        /// <summary>
        /// The parent item of the current tree item (if any)
        /// </summary>
        [Parameter]
        public IgbTreeItem? Parent
        {
            get { return this._parent; }
            set
            {
                if (this._parent != value || !IsPropDirty("Parent"))
                {
                    MarkPropDirty("Parent");
                }
                this._parent = value;

            }
        }
        private double _level = 0;

        /// <summary>
        /// The depth of the item, relative to the root.
        /// </summary>
        [Parameter]
        public double Level
        {
            get { return this._level; }
            set
            {
                if (this._level != value || !IsPropDirty("Level"))
                {
                    MarkPropDirty("Level");
                }
                this._level = value;

            }
        }
        private string? _label;

        /// <summary>
        /// The tree item label.
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
        private bool _expanded = false;

        /// <summary>
        /// The tree item expansion state.
        /// </summary>
        [Parameter]
        public bool Expanded
        {
            get { return this._expanded; }
            set
            {
                if (this._expanded != value || !IsPropDirty("Expanded"))
                {
                    MarkPropDirty("Expanded");
                }
                this._expanded = value;

            }
        }
        private bool _active = false;

        /// <summary>
        /// Marks the item as the tree's active item.
        /// </summary>
        [Parameter]
        public bool Active
        {
            get { return this._active; }
            set
            {
                if (this._active != value || !IsPropDirty("Active"))
                {
                    MarkPropDirty("Active");
                }
                this._active = value;

            }
        }
        private bool _disabled = false;

        /// <summary>
        /// Get/Set whether the tree item is disabled. Disabled items are ignored for user interactions.
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
        private bool _selected = false;

        /// <summary>
        /// The tree item selection state.
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
        private bool _loading = false;

        /// <summary>
        /// To be used for load-on-demand scenarios in order to specify whether the item is loading data.
        /// </summary>
        [Parameter]
        public bool Loading
        {
            get { return this._loading; }
            set
            {
                if (this._loading != value || !IsPropDirty("Loading"))
                {
                    MarkPropDirty("Loading");
                }
                this._loading = value;

            }
        }
        private object? _value;

        /// <summary>
        /// The value entry that the tree item is visualizing. Required for searching through items.
        /// </summary>
        [Parameter]
        public object? Value
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
        /// Returns the full path to the tree item, starting from the top-most ancestor.
        /// </summary>
        public async Task<IgbTreeItem[]> GetPathAsync()
        {
            var iv = await InvokeMethod("p:Path", new object?[] { }, new string[] { });
            var retVal = ReturnToObjectArray<IgbTreeItem>(iv);
            if (retVal == null)
            {
                return Array.Empty<IgbTreeItem>();
            }
            return retVal;

        }

        /// <summary>
        /// Returns the full path to the tree item, starting from the top-most ancestor.
        /// </summary>
        public IgbTreeItem[] GetPath()
        {
            var iv = InvokeMethodSync("p:Path", new object?[] { }, new string[] { });
            var retVal = ReturnToObjectArray<IgbTreeItem>(iv);
            if (retVal == null)
            {
                return Array.Empty<IgbTreeItem>();
            }
            return retVal;

        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public async Task ConnectedCallbackAsync()
        {
            await InvokeMethod("connectedCallback", new object?[] { }, new string[] { });
        }
        public void ConnectedCallback()
        {
            InvokeMethodSync("connectedCallback", new object?[] { }, new string[] { });
        }
        public async Task DisconnectedCallbackAsync()
        {
            await InvokeMethod("disconnectedCallback", new object?[] { }, new string[] { });
        }
        public void DisconnectedCallback()
        {
            InvokeMethodSync("disconnectedCallback", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Toggles tree item expansion state.
        /// </summary>
        public async Task ToggleAsync()
        {
            await InvokeMethod("toggle", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Toggles tree item expansion state.
        /// </summary>
        public void Toggle()
        {
            InvokeMethodSync("toggle", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Expands the tree item.
        /// </summary>
        public async Task ExpandAsync()
        {
            await InvokeMethod("expand", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Expands the tree item.
        /// </summary>
        public void Expand()
        {
            InvokeMethodSync("expand", new object?[] { }, new string[] { });
        }
        /// <summary>
        /// Collapses the tree item.
        /// </summary>
        public async Task CollapseAsync()
        {
            await InvokeMethod("collapse", new object?[] { }, new string[] { });
        }

        /// <summary>
        /// Collapses the tree item.
        /// </summary>
        public void Collapse()
        {
            InvokeMethodSync("collapse", new object?[] { }, new string[] { });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Parent"))
            { ser.AddSerializableProp("parent", this._parent); }
            if (IsPropDirty("Level"))
            { ser.AddNumberProp("level", this._level); }
            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Expanded"))
            { ser.AddBooleanProp("expanded", this._expanded); }
            if (IsPropDirty("Active"))
            { ser.AddBooleanProp("active", this._active); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Selected"))
            { ser.AddBooleanProp("selected", this._selected); }
            if (IsPropDirty("Loading"))
            { ser.AddBooleanProp("loading", this._loading); }
            if (IsPropDirty("Value"))
            { ser.AddPrimitiveProp("value", this._value); }

        }

    }
}
