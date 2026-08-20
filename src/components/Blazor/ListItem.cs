using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The list-item component is a container
    /// intended for row items in the list component.
    /// </summary>
    public partial class IgbListItem : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebListItem"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            ModuleLoader.Load(IgBlazor, "WebListModule");
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
                return "igc-list-item";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _selected = false;

        /// <summary>
        /// Defines if the list item is selected or not.
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

            if (IsPropDirty("Selected"))
            { ser.AddBooleanProp("selected", this._selected); }

        }

    }
}
