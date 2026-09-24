using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A tab nested in an <see cref="IgbTabs"/> component.
    /// </summary>
    public partial class IgbTab : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTab"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbTabsModule.IsLoadRequested(IgBlazor))
            {
                IgbTabsModule.Register(IgBlazor);
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
                return "igc-tab";
            }
        }

        /// <inheritdoc />
        private protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        /// <summary>
        /// The owning <see cref="IgbTabs"/>, supplied as a cascading parameter.
        /// </summary>
        [CascadingParameter(Name = "TabsParent")]
        private protected BaseRendererControl? TabsParent
        {
            get; set;
        }

        /// <inheritdoc />
        public override async ValueTask DisposeAsync()
        {
            if (TabsParent != null)
            {
                var sv = (IgbTabs)TabsParent;
                sv.ContentTabsCollection.Remove(this);
            }
            await base.DisposeAsync().ConfigureAwait(false);
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

        private string? _label;

        /// <summary>
        /// The tab item label.
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

        internal void Select()
        {
            this.Selected = true;
        }

        internal void Deselect()
        {
            this.Selected = false;
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
