using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class shared by <see cref="IgbRangeSlider"/> and <see cref="IgbSlider"/>.
    /// </summary>
    public partial class IgbSliderBase : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebSliderBase"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbSliderBaseModule.IsLoadRequested(IgBlazor))
            {
                IgbSliderBaseModule.Register(IgBlazor);
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
                return "igc-slider-base";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private double _min = 0;

        /// <summary>
        /// The minimum value of the slider scale. Defaults to 0.
        /// If <see cref="Min"/> is greater than <see cref="Max"/> the assignment is a no-op.
        /// If <see cref="IgbSliderLabel"/> children are provided, then <see cref="Min"/> is always set to 0.
        /// If <see cref="LowerBound"/> ends up being less than the current <see cref="Min"/> value,
        /// it is automatically assigned the new <see cref="Min"/> value.
        /// </summary>
        [Parameter]
        public double Min
        {
            get { return this._min; }
            set
            {
                if (this._min != value || !IsPropDirty("Min"))
                {
                    MarkPropDirty("Min");
                }
                this._min = value;

            }
        }
        private double _max = 100;

        /// <summary>
        /// The maximum value of the slider scale. Defaults to 100.
        /// If <see cref="Max"/> is less than <see cref="Min"/> the assignment is a no-op.
        /// If <see cref="IgbSliderLabel"/> children are provided, then <see cref="Max"/> is always set to
        /// the number of labels.
        /// If <see cref="UpperBound"/> ends up being greater than the current <see cref="Max"/> value,
        /// it is automatically assigned the new <see cref="Max"/> value.
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
        private double _lowerBound = 0;

        /// <summary>
        /// The lower bound of the slider value. If not set, the <see cref="Min"/> value is applied.
        /// </summary>
        [Parameter]
        public double LowerBound
        {
            get { return this._lowerBound; }
            set
            {
                if (this._lowerBound != value || !IsPropDirty("LowerBound"))
                {
                    MarkPropDirty("LowerBound");
                }
                this._lowerBound = value;

            }
        }
        private double _upperBound = 0;

        /// <summary>
        /// The upper bound of the slider value. If not set, the <see cref="Max"/> value is applied.
        /// </summary>
        [Parameter]
        public double UpperBound
        {
            get { return this._upperBound; }
            set
            {
                if (this._upperBound != value || !IsPropDirty("UpperBound"))
                {
                    MarkPropDirty("UpperBound");
                }
                this._upperBound = value;

            }
        }
        private bool _disabled = false;

        /// <summary>
        /// Disables the UI interactions of the slider.
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
        private bool _discreteTrack = false;

        /// <summary>
        /// Marks the slider track as discrete so it displays the steps.
        /// If <see cref="Step"/> is 0, the slider remains continuous even if <see cref="DiscreteTrack"/>
        /// is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool DiscreteTrack
        {
            get { return this._discreteTrack; }
            set
            {
                if (this._discreteTrack != value || !IsPropDirty("DiscreteTrack"))
                {
                    MarkPropDirty("DiscreteTrack");
                }
                this._discreteTrack = value;

            }
        }
        private bool _hideTooltip = false;

        /// <summary>
        /// Hides the thumb tooltip.
        /// </summary>
        [Parameter]
        public bool HideTooltip
        {
            get { return this._hideTooltip; }
            set
            {
                if (this._hideTooltip != value || !IsPropDirty("HideTooltip"))
                {
                    MarkPropDirty("HideTooltip");
                }
                this._hideTooltip = value;

            }
        }
        private double _step = 1;

        /// <summary>
        /// Specifies the granularity that the value must adhere to.
        /// If set to 0 no stepping is implied and any value in the range is allowed.
        /// If <see cref="IgbSliderLabel"/> children are provided then the step is always assumed to be 1,
        /// since it is a discrete slider.
        /// </summary>
        [Parameter]
        public double Step
        {
            get { return this._step; }
            set
            {
                if (this._step != value || !IsPropDirty("Step"))
                {
                    MarkPropDirty("Step");
                }
                this._step = value;

            }
        }
        private double _primaryTicks = 0;

        /// <summary>
        /// The number of primary ticks. It defaults to 0 which means no primary ticks are displayed.
        /// </summary>
        [Parameter]
        public double PrimaryTicks
        {
            get { return this._primaryTicks; }
            set
            {
                if (this._primaryTicks != value || !IsPropDirty("PrimaryTicks"))
                {
                    MarkPropDirty("PrimaryTicks");
                }
                this._primaryTicks = value;

            }
        }
        private double _secondaryTicks = 0;

        /// <summary>
        /// The number of secondary ticks. It defaults to 0 which means no secondary ticks are displayed.
        /// </summary>
        [Parameter]
        public double SecondaryTicks
        {
            get { return this._secondaryTicks; }
            set
            {
                if (this._secondaryTicks != value || !IsPropDirty("SecondaryTicks"))
                {
                    MarkPropDirty("SecondaryTicks");
                }
                this._secondaryTicks = value;

            }
        }
        private SliderTickOrientation _tickOrientation = SliderTickOrientation.End;

        /// <summary>
        /// Changes the orientation of the ticks.
        /// </summary>
        [Parameter]
        public SliderTickOrientation TickOrientation
        {
            get { return this._tickOrientation; }
            set
            {
                if (this._tickOrientation != value || !IsPropDirty("TickOrientation"))
                {
                    MarkPropDirty("TickOrientation");
                }
                this._tickOrientation = value;

            }
        }
        private bool _hidePrimaryLabels = false;

        /// <summary>
        /// Hides the primary tick labels.
        /// </summary>
        [Parameter]
        public bool HidePrimaryLabels
        {
            get { return this._hidePrimaryLabels; }
            set
            {
                if (this._hidePrimaryLabels != value || !IsPropDirty("HidePrimaryLabels"))
                {
                    MarkPropDirty("HidePrimaryLabels");
                }
                this._hidePrimaryLabels = value;

            }
        }
        private bool _hideSecondaryLabels = false;

        /// <summary>
        /// Hides the secondary tick labels.
        /// </summary>
        [Parameter]
        public bool HideSecondaryLabels
        {
            get { return this._hideSecondaryLabels; }
            set
            {
                if (this._hideSecondaryLabels != value || !IsPropDirty("HideSecondaryLabels"))
                {
                    MarkPropDirty("HideSecondaryLabels");
                }
                this._hideSecondaryLabels = value;

            }
        }
        private string? _locale;

        /// <summary>
        /// The locale used to format the thumb and tick label values in the slider.
        /// </summary>
        [Parameter]
        public string? Locale
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
        private string? _valueFormat;

        /// <summary>
        /// String format used for the thumb and tick label values in the slider.
        /// </summary>
        [Parameter]
        public string? ValueFormat
        {
            get { return this._valueFormat; }
            set
            {
                if (this._valueFormat != value || !IsPropDirty("ValueFormat"))
                {
                    MarkPropDirty("ValueFormat");
                }
                this._valueFormat = value;

            }
        }
        private SliderTickLabelRotation _tickLabelRotation = SliderTickLabelRotation.Zero;

        /// <summary>
        /// The degrees for the rotation of the tick labels. Defaults to 0.
        /// </summary>
        [Parameter]
        public SliderTickLabelRotation TickLabelRotation
        {
            get { return this._tickLabelRotation; }
            set
            {
                if (this._tickLabelRotation != value || !IsPropDirty("TickLabelRotation"))
                {
                    MarkPropDirty("TickLabelRotation");
                }
                this._tickLabelRotation = value;

            }
        }
        private IgbNumberFormatSpecifier? _valueFormatOptions;

        /// <summary>
        /// Number format options used for the thumb and tick label values in the slider.
        /// </summary>
        [Parameter]
        public IgbNumberFormatSpecifier? ValueFormatOptions
        {
            get { return this._valueFormatOptions; }
            set
            {
                if (this._valueFormatOptions != value || !IsPropDirty("ValueFormatOptions"))
                {
                    MarkPropDirty("ValueFormatOptions");
                }
                this._valueFormatOptions = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Min"))
            { ser.AddNumberProp("min", this._min); }
            if (IsPropDirty("Max"))
            { ser.AddNumberProp("max", this._max); }
            if (IsPropDirty("LowerBound"))
            { ser.AddNumberProp("lowerBound", this._lowerBound); }
            if (IsPropDirty("UpperBound"))
            { ser.AddNumberProp("upperBound", this._upperBound); }
            if (IsPropDirty("Disabled"))
            { ser.AddBooleanProp("disabled", this._disabled); }
            if (IsPropDirty("DiscreteTrack"))
            { ser.AddBooleanProp("discreteTrack", this._discreteTrack); }
            if (IsPropDirty("HideTooltip"))
            { ser.AddBooleanProp("hideTooltip", this._hideTooltip); }
            if (IsPropDirty("Step"))
            { ser.AddNumberProp("step", this._step); }
            if (IsPropDirty("PrimaryTicks"))
            { ser.AddNumberProp("primaryTicks", this._primaryTicks); }
            if (IsPropDirty("SecondaryTicks"))
            { ser.AddNumberProp("secondaryTicks", this._secondaryTicks); }
            if (IsPropDirty("TickOrientation"))
            { ser.AddEnumProp("tickOrientation", this._tickOrientation); }
            if (IsPropDirty("HidePrimaryLabels"))
            { ser.AddBooleanProp("hidePrimaryLabels", this._hidePrimaryLabels); }
            if (IsPropDirty("HideSecondaryLabels"))
            { ser.AddBooleanProp("hideSecondaryLabels", this._hideSecondaryLabels); }
            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("ValueFormat"))
            { ser.AddStringProp("valueFormat", this._valueFormat); }
            if (IsPropDirty("TickLabelRotation"))
            { ser.AddEnumProp("tickLabelRotation", this._tickLabelRotation); }
            if (IsPropDirty("ValueFormatOptions"))
            { ser.AddSerializableProp("valueFormatOptions", (JsonSerializable)this._valueFormatOptions!); }

        }

    }
}
