using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbCircularProgress"/> and <see cref="IgbLinearProgress"/>.
    /// </summary>
    public partial class IgbProgressBase : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebProgressBase"; } }

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
                return "igc-progress-base";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private double _max = 100;

        /// <summary>
        /// Maximum value of the control.
        /// </summary>
        [Parameter]
        public double Max
        {
            get { return this._max; }
            set
            {
                if (this._max != value || !IsPropDirty("Max"))
                {
                    MarkPropDirty("Max");
                }
                this._max = value;

            }
        }
        private double _value = 0;

        /// <summary>
        /// The value of the control.
        /// </summary>
        [Parameter]
        public double Value
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
        private StyleVariant _variant = StyleVariant.Primary;

        /// <summary>
        /// The variant of the control.
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
        private double _animationDuration = 500;

        /// <summary>
        /// Animation duration in milliseconds.
        /// </summary>
        [Parameter]
        public double AnimationDuration
        {
            get { return this._animationDuration; }
            set
            {
                if (this._animationDuration != value || !IsPropDirty("AnimationDuration"))
                {
                    MarkPropDirty("AnimationDuration");
                }
                this._animationDuration = value;

            }
        }
        private bool _indeterminate = false;

        /// <summary>
        /// The indeterminate state of the control.
        /// </summary>
        [Parameter]
        public bool Indeterminate
        {
            get { return this._indeterminate; }
            set
            {
                if (this._indeterminate != value || !IsPropDirty("Indeterminate"))
                {
                    MarkPropDirty("Indeterminate");
                }
                this._indeterminate = value;

            }
        }
        private bool _hideLabel = false;

        /// <summary>
        /// Shows/hides the label of the control.
        /// </summary>
        [Parameter]
        public bool HideLabel
        {
            get { return this._hideLabel; }
            set
            {
                if (this._hideLabel != value || !IsPropDirty("HideLabel"))
                {
                    MarkPropDirty("HideLabel");
                }
                this._hideLabel = value;

            }
        }
        private string _labelFormat;

        /// <summary>
        /// Format string for the default label of the control. Placeholders:
        /// <list type="bullet">
        ///   <item><description><c>{0}</c> - current value of the control.</description></item>
        ///   <item><description><c>{1}</c> - max value of the control.</description></item>
        /// </list>
        /// </summary>
        [Parameter]
        public string LabelFormat
        {
            get { return this._labelFormat; }
            set
            {
                if (this._labelFormat != value || !IsPropDirty("LabelFormat"))
                {
                    MarkPropDirty("LabelFormat");
                }
                this._labelFormat = value;

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

        /// <inheritdoc />
        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Max"))
            { ser.AddNumberProp("max", this._max); }
            if (IsPropDirty("Value"))
            { ser.AddNumberProp("value", this._value); }
            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }
            if (IsPropDirty("AnimationDuration"))
            { ser.AddNumberProp("animationDuration", this._animationDuration); }
            if (IsPropDirty("Indeterminate"))
            { ser.AddBooleanProp("indeterminate", this._indeterminate); }
            if (IsPropDirty("HideLabel"))
            { ser.AddBooleanProp("hideLabel", this._hideLabel); }
            if (IsPropDirty("LabelFormat"))
            { ser.AddStringProp("labelFormat", this._labelFormat); }

        }

    }
}
