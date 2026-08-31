using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A step component used within an <see cref="IgbStepper"/> to represent an individual step
    /// in a wizard-like workflow.
    /// </summary>
    public partial class IgbStep : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebStep"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbStepperModule.IsLoadRequested(IgBlazor))
            {
                IgbStepperModule.Register(IgBlazor);
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
                return "igc-step";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _invalid = false;

        /// <summary>
        /// Whether the step is invalid.
        /// Invalid steps are styled with an error state and are not
        /// interactive when the stepper is in linear mode.
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
        private bool _active = false;

        /// <summary>
        /// Whether the step is active.
        /// Active steps are styled with an active state and their content is visible.
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
        private bool _optional = false;

        /// <summary>
        /// Whether the step is optional.
        /// Optional steps validity does not affect the default behavior when the stepper is in linear mode i.e.
        /// if optional step is invalid the user could still move to the next step.
        /// </summary>
        [Parameter]
        public bool Optional
        {
            get { return this._optional; }
            set
            {
                if (this._optional != value || !IsPropDirty("Optional"))
                {
                    MarkPropDirty("Optional");
                }
                this._optional = value;

            }
        }
        private bool _disabled = false;

        /// <summary>
        /// Whether the step is disabled.
        /// Disabled steps are styled with a disabled state and are not interactive.
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
        private bool _complete = false;

        /// <summary>
        /// Whether the step is completed.
        /// </summary>
        [Parameter]
        public bool Complete
        {
            get { return this._complete; }
            set
            {
                if (this._complete != value || !IsPropDirty("Complete"))
                {
                    MarkPropDirty("Complete");
                }
                this._complete = value;

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

            if (IsPropDirty("Invalid"))
            { ser.AddBooleanProp("invalid", this._invalid); }
            if (IsPropDirty("Active"))
            { ser.AddBooleanProp("active", this._active); }
            if (IsPropDirty("Optional"))
            { ser.AddBooleanProp("optional", this._optional); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("Complete"))
            { ser.AddBooleanProp("complete", this._complete); }

        }

    }
}
