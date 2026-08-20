using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A tab nested in an <see cref="IgbTabs"/> component.
    /// </summary>
    public partial class IgbTab : BaseRendererControl, IDisposable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTab"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbTabModule.IsLoadRequested(IgBlazor))
            {
                IgbTabModule.Register(IgBlazor);
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
                return "igc-tab";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        [CascadingParameter(Name = "TabsParent")]
        protected BaseRendererControl TabsParent
        {
            get; set;
        }

        public void Dispose()
        {
            if (TabsParent != null)
            {
                var sv = (IgbTabs)TabsParent;
                sv.ContentTabsCollection.Remove(this);
            }

        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (TabsParent != null)
            {
                var sv = (IgbTabs)TabsParent;
                sv.ContentTabsCollection.Add(this);
            }

        }

        private string _label;

        /// <summary>
        /// The tab item label.
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
        private bool _selected = false;

        /// <summary>
        /// Determines whether the tab is selected.
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
        private bool _disabled = false;

        /// <summary>
        /// Determines whether the tab is disabled.
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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Label"))
            { ser.AddStringProp("label", this._label); }
            if (IsPropDirty("Selected"))
            { ser.AddBooleanProp("selected", this._selected); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }

        }

    }
}
