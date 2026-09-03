using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Tabs organize and allow navigation between groups of content that are related
    /// and at the same level of hierarchy.
    /// The <see cref="IgbTabs"/> component allows the user to navigate between multiple
    /// <see cref="IgbTab"/> children.
    /// It supports keyboard navigation and provides API methods to control the selected tab.
    /// </summary>
    public partial class IgbTabs : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTabs"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbTabsModule.IsLoadRequested(IgBlazor))
            {
                IgbTabsModule.Register(IgBlazor);
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
                return "igc-tabs";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "TabsParent";
            }
        }

        private CollectionAdapter<IgbTab, IgbTab> _tabsCollectionAdapter;
        private IgbTabs_TabCollection _allTabsCollection;
        private IgbTabs_TabCollection _contentTabsCollection = null;

        public IgbTabs_TabCollection ContentTabsCollection
        {

            get
            {
                if (this._contentTabsCollection == null)
                {
                    this._contentTabsCollection = new IgbTabs_TabCollection(this, "TabsCollection");
                }
                return this._contentTabsCollection;
            }
        }
        private IgbTabs_TabCollection _actualTabsCollection = null;

        public IgbTabs_TabCollection ActualTabsCollection
        {

            get
            {
                if (this._actualTabsCollection == null)
                {
                    this._actualTabsCollection = new IgbTabs_TabCollection(this, "TabsCollection");
                }
                return this._actualTabsCollection;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="IgbTabs"/>.
        /// </summary>
        public IgbTabs() : base()
        {
            // Ensure Change handler so selection syncs back to the child IgbTab instances
            EnsureChangeHandled();

            _allTabsCollection = new IgbTabs_TabCollection(this, "TabsCollection");
            _tabsCollectionAdapter = new CollectionAdapter<IgbTab, IgbTab>(
                ContentTabsCollection,
                ActualTabsCollection,
                _allTabsCollection,
                (s) => s,
                (s) =>
                {
                },
                (s) => { }
            );
            _tabsCollectionAdapter.SubcribeToManual(TabsCollection);

        }

        private IgbTabs_TabCollection _tabsCollection = null;

        public IgbTabs_TabCollection TabsCollection
        {

            get
            {
                if (this._tabsCollection == null)
                {
                    this._tabsCollection = new IgbTabs_TabCollection(this, "TabsCollection");
                }
                return this._tabsCollection;
            }
            protected set
            {
                if (this._tabsCollection != value || !IsPropDirty("TabsCollection"))
                {
                    MarkPropDirty("TabsCollection");
                }
                this._tabsCollection = value;

            }
        }
        private TabsAlignment _alignment = TabsAlignment.Start;

        /// <summary>
        /// Sets the alignment for the tab headers.
        /// </summary>
        [Parameter]
        public TabsAlignment Alignment
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
        private TabsActivation _activation = TabsActivation.Auto;

        /// <summary>
        /// Determines the tab activation. When set to <see cref="TabsActivation.Auto"/>,
        /// the tab is instantly selected while navigating with the Left/Right Arrows, Home or End keys
        /// and the corresponding panel is displayed.
        /// When set to <see cref="TabsActivation.Manual"/>, the tab is only focused.
        /// The selection happens after pressing Space or Enter.
        /// </summary>
        [Parameter]
        public TabsActivation Activation
        {
            get { return this._activation; }
            set
            {
                if (this._activation != value || !IsPropDirty("Activation"))
                {
                    MarkPropDirty("Activation");
                }
                this._activation = value;

            }
        }

        /// <summary>
        /// Gets the currently selected tab.
        /// </summary>
        /// <returns>The label of the selected tab, or its ID if no label is set.</returns>
        public async Task<string> GetSelectedAsync()
        {
            var iv = await InvokeMethod("p:Selected", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Gets the currently selected tab.
        /// </summary>
        /// <returns>The label of the selected tab, or its ID if no label is set.</returns>
        public string GetSelected()
        {
            var iv = InvokeMethodSync("p:Selected", new object[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <summary>
        /// Returns the currently selected tab, or <see langword="null"/> when no tab is selected.
        /// </summary>
        public async Task<IgbTab?> GetSelectedTabAsync()
        {
            var iv = await InvokeMethod("p:SelectedTab", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbTab);
            }
            var retVal = (IgbTab)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbTab);
            }
            return retVal;

        }

        /// <summary>
        /// Returns the currently selected tab, or <see langword="null"/> when no tab is selected.
        /// </summary>
        public IgbTab? GetSelectedTab()
        {
            var iv = InvokeMethodSync("p:SelectedTab", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbTab);
            }
            var retVal = (IgbTab)ConvertReturnValue(iv);
            if (retVal == null)
            {
                return default(IgbTab);
            }
            return retVal;

        }

        /// <inheritdoc />
        public override object FindByName(string name)
        {
            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            if (_actualTabsCollection != null && _actualTabsCollection.HasName(name))
            { return _actualTabsCollection.FindByName(name); }

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
        /// Selects the specified tab and displays the corresponding panel.
        /// </summary>
        public async Task SelectAsync(String id)
        {
            await InvokeMethod("select", new object[] { StringToString(id) }, new string[] { "String" });
        }

        /// <summary>
        /// Selects the specified tab and displays the corresponding panel.
        /// </summary>
        public void Select(String id)
        {
            InvokeMethodSync("select", new object[] { StringToString(id) }, new string[] { "String" });
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

        private EventCallback<IgbTabComponentEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the selected tab changes.
        /// </summary>
        [Parameter]
        public EventCallback<IgbTabComponentEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbTabComponentEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbTabComponentEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            SyncSelectedTab(args);
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
                    this.SetHandler<IgbTabComponentEventArgs>(this.Name, "Change", null);
                    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._changeRef = null;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("TabsCollection"))
            { ser.AddCollectionProp("tabsCollection", ActualTabsCollection); }
            if (IsPropDirty("Alignment"))
            { ser.AddEnumProp("alignment", this._alignment); }
            if (IsPropDirty("Activation"))
            { ser.AddEnumProp("activation", this._activation); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
