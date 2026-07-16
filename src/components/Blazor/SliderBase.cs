
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbSliderBase: BaseRendererControl {
                                public override string Type { get { return "WebSliderBase"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSliderBaseModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSliderBaseModule.Register(IgBlazor);
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
                            return "igc-slider-base";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbSliderBase(): base() {
	        OnCreatedIgbSliderBase();
	
	        
	    }
	
	    partial void OnCreatedIgbSliderBase();
	    
	private double _min = 0;
	
	partial void OnMinChanging(ref double newValue);
	/// <summary>
	/// The minimum value of the slider scale. Defaults to 0.
	/// If `min` is greater than `max` the call is a no-op.
	/// If `labels` are provided (projected), then `min` is always set to 0.
	/// If `lowerBound` ends up being less than than the current `min` value,
	/// it is automatically assigned the new `min` value.
	/// </summary>
	[Parameter]
	public double Min 
	{
	get { return this._min; }
	set { 
	                if (this._min != value || !IsPropDirty("Min")) {
	                        MarkPropDirty("Min");
	                } 
	                this._min = value;
	                 
	                }
	}
	private double _max = 0;
	
	partial void OnMaxChanging(ref double newValue);
	/// <summary>
	/// The maximum value of the slider scale. Defaults to 100.
	/// If `max` is less than `min` the call is a no-op.
	/// If `labels` are provided (projected), then `max` is always set to
	/// the number of labels.
	/// If `upperBound` ends up being greater than than the current `max` value,
	/// it is automatically assigned the new `max` value.
	/// </summary>
	[Parameter]
	public double Max 
	{
	get { return this._max; }
	set { 
	                if (this._max != value || !IsPropDirty("Max")) {
	                        MarkPropDirty("Max");
	                } 
	                this._max = value;
	                 
	                }
	}
	private double _lowerBound = 0;
	
	partial void OnLowerBoundChanging(ref double newValue);
	/// <summary>
	/// The lower bound of the slider value. If not set, the `min` value is applied.
	/// </summary>
	[Parameter]
	public double LowerBound 
	{
	get { return this._lowerBound; }
	set { 
	                if (this._lowerBound != value || !IsPropDirty("LowerBound")) {
	                        MarkPropDirty("LowerBound");
	                } 
	                this._lowerBound = value;
	                 
	                }
	}
	private double _upperBound = 0;
	
	partial void OnUpperBoundChanging(ref double newValue);
	/// <summary>
	/// The upper bound of the slider value. If not set, the `max` value is applied.
	/// </summary>
	[Parameter]
	public double UpperBound 
	{
	get { return this._upperBound; }
	set { 
	                if (this._upperBound != value || !IsPropDirty("UpperBound")) {
	                        MarkPropDirty("UpperBound");
	                } 
	                this._upperBound = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Disables the UI interactions of the slider.
	/// </summary>
	[Parameter]
	public bool Disabled 
	{
	get { return this._disabled; }
	set { 
	                if (this._disabled != value || !IsPropDirty("Disabled")) {
	                        MarkPropDirty("Disabled");
	                } 
	                this._disabled = value;
	                 
	                }
	}
	private bool _discreteTrack = false;
	
	partial void OnDiscreteTrackChanging(ref bool newValue);
	/// <summary>
	/// Marks the slider track as discrete so it displays the steps.
	/// If the `step` is 0, the slider will remain continuos even if `discreteTrack` is `true`.
	/// </summary>
	[Parameter]
	public bool DiscreteTrack 
	{
	get { return this._discreteTrack; }
	set { 
	                if (this._discreteTrack != value || !IsPropDirty("DiscreteTrack")) {
	                        MarkPropDirty("DiscreteTrack");
	                } 
	                this._discreteTrack = value;
	                 
	                }
	}
	private bool _hideTooltip = false;
	
	partial void OnHideTooltipChanging(ref bool newValue);
	/// <summary>
	/// Hides the thumb tooltip.
	/// </summary>
	[Parameter]
	public bool HideTooltip 
	{
	get { return this._hideTooltip; }
	set { 
	                if (this._hideTooltip != value || !IsPropDirty("HideTooltip")) {
	                        MarkPropDirty("HideTooltip");
	                } 
	                this._hideTooltip = value;
	                 
	                }
	}
	private double _step = 0;
	
	partial void OnStepChanging(ref double newValue);
	/// <summary>
	/// Specifies the granularity that the value must adhere to.
	/// If set to 0 no stepping is implied and any value in the range is allowed.
	/// If `labels` are provided (projected) then the step is always assumed to be 1 since it is a discrete slider.
	/// </summary>
	[Parameter]
	public double Step 
	{
	get { return this._step; }
	set { 
	                if (this._step != value || !IsPropDirty("Step")) {
	                        MarkPropDirty("Step");
	                } 
	                this._step = value;
	                 
	                }
	}
	private double _primaryTicks = 0;
	
	partial void OnPrimaryTicksChanging(ref double newValue);
	/// <summary>
	/// The number of primary ticks. It defaults to 0 which means no primary ticks are displayed.
	/// </summary>
	[Parameter]
	public double PrimaryTicks 
	{
	get { return this._primaryTicks; }
	set { 
	                if (this._primaryTicks != value || !IsPropDirty("PrimaryTicks")) {
	                        MarkPropDirty("PrimaryTicks");
	                } 
	                this._primaryTicks = value;
	                 
	                }
	}
	private double _secondaryTicks = 0;
	
	partial void OnSecondaryTicksChanging(ref double newValue);
	/// <summary>
	/// The number of secondary ticks. It defaults to 0 which means no secondary ticks are displayed.
	/// </summary>
	[Parameter]
	public double SecondaryTicks 
	{
	get { return this._secondaryTicks; }
	set { 
	                if (this._secondaryTicks != value || !IsPropDirty("SecondaryTicks")) {
	                        MarkPropDirty("SecondaryTicks");
	                } 
	                this._secondaryTicks = value;
	                 
	                }
	}
	private SliderTickOrientation _tickOrientation = SliderTickOrientation.End;
	
	partial void OnTickOrientationChanging(ref SliderTickOrientation newValue);
	/// <summary>
	/// Changes the orientation of the ticks.
	/// </summary>
	[Parameter]
	public SliderTickOrientation TickOrientation 
	{
	get { return this._tickOrientation; }
	set { 
	                if (this._tickOrientation != value || !IsPropDirty("TickOrientation")) {
	                        MarkPropDirty("TickOrientation");
	                } 
	                this._tickOrientation = value;
	                 
	                }
	}
	private bool _hidePrimaryLabels = false;
	
	partial void OnHidePrimaryLabelsChanging(ref bool newValue);
	/// <summary>
	/// Hides the primary tick labels.
	/// </summary>
	[Parameter]
	public bool HidePrimaryLabels 
	{
	get { return this._hidePrimaryLabels; }
	set { 
	                if (this._hidePrimaryLabels != value || !IsPropDirty("HidePrimaryLabels")) {
	                        MarkPropDirty("HidePrimaryLabels");
	                } 
	                this._hidePrimaryLabels = value;
	                 
	                }
	}
	private bool _hideSecondaryLabels = false;
	
	partial void OnHideSecondaryLabelsChanging(ref bool newValue);
	/// <summary>
	/// Hides the secondary tick labels.
	/// </summary>
	[Parameter]
	public bool HideSecondaryLabels 
	{
	get { return this._hideSecondaryLabels; }
	set { 
	                if (this._hideSecondaryLabels != value || !IsPropDirty("HideSecondaryLabels")) {
	                        MarkPropDirty("HideSecondaryLabels");
	                } 
	                this._hideSecondaryLabels = value;
	                 
	                }
	}
	private string _locale;
	
	partial void OnLocaleChanging(ref string newValue);
	/// <summary>
	/// The locale used to format the thumb and tick label values in the slider.
	/// </summary>
	[Parameter]
	public string Locale 
	{
	get { return this._locale; }
	set { 
	                if (this._locale != value || !IsPropDirty("Locale")) {
	                        MarkPropDirty("Locale");
	                } 
	                this._locale = value;
	                 
	                }
	}
	private string _valueFormat;
	
	partial void OnValueFormatChanging(ref string newValue);
	/// <summary>
	/// String format used for the thumb and tick label values in the slider.
	/// </summary>
	[Parameter]
	public string ValueFormat 
	{
	get { return this._valueFormat; }
	set { 
	                if (this._valueFormat != value || !IsPropDirty("ValueFormat")) {
	                        MarkPropDirty("ValueFormat");
	                } 
	                this._valueFormat = value;
	                 
	                }
	}
	private SliderTickLabelRotation _tickLabelRotation = SliderTickLabelRotation.Zero;
	
	partial void OnTickLabelRotationChanging(ref SliderTickLabelRotation newValue);
	/// <summary>
	/// The degrees for the rotation of the tick labels. Defaults to 0.
	/// </summary>
	[Parameter]
	public SliderTickLabelRotation TickLabelRotation 
	{
	get { return this._tickLabelRotation; }
	set { 
	                if (this._tickLabelRotation != value || !IsPropDirty("TickLabelRotation")) {
	                        MarkPropDirty("TickLabelRotation");
	                } 
	                this._tickLabelRotation = value;
	                 
	                }
	}
	private IgbNumberFormatSpecifier _valueFormatOptions;
	
	partial void OnValueFormatOptionsChanging(ref IgbNumberFormatSpecifier newValue);
	/// <summary>
	/// Number format options used for the thumb and tick label values in the slider.
	/// </summary>
	[Parameter]
	public IgbNumberFormatSpecifier ValueFormatOptions 
	{
	get { return this._valueFormatOptions; }
	set { 
	                if (this._valueFormatOptions != value || !IsPropDirty("ValueFormatOptions")) {
	                        MarkPropDirty("ValueFormatOptions");
	                } 
	                this._valueFormatOptions = value;
	                 
	                }
	}
	
	    partial void FindByNameSliderBase(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSliderBase(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task SetNativeElementAsync(Object element) 
	                    {
		await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	                    public  void SetNativeElement(Object element) 
	                    {
		InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	
	    partial void SerializeCoreIgbSliderBase(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSliderBase(ser);
	
	if (IsPropDirty("Min")) { ser.AddNumberProp("min", this._min); }
	if (IsPropDirty("Max")) { ser.AddNumberProp("max", this._max); }
	if (IsPropDirty("LowerBound")) { ser.AddNumberProp("lowerBound", this._lowerBound); }
	if (IsPropDirty("UpperBound")) { ser.AddNumberProp("upperBound", this._upperBound); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("DiscreteTrack")) { ser.AddBooleanProp("discreteTrack", this._discreteTrack); }
	if (IsPropDirty("HideTooltip")) { ser.AddBooleanProp("hideTooltip", this._hideTooltip); }
	if (IsPropDirty("Step")) { ser.AddNumberProp("step", this._step); }
	if (IsPropDirty("PrimaryTicks")) { ser.AddNumberProp("primaryTicks", this._primaryTicks); }
	if (IsPropDirty("SecondaryTicks")) { ser.AddNumberProp("secondaryTicks", this._secondaryTicks); }
	if (IsPropDirty("TickOrientation")) { ser.AddEnumProp("tickOrientation", this._tickOrientation); }
	if (IsPropDirty("HidePrimaryLabels")) { ser.AddBooleanProp("hidePrimaryLabels", this._hidePrimaryLabels); }
	if (IsPropDirty("HideSecondaryLabels")) { ser.AddBooleanProp("hideSecondaryLabels", this._hideSecondaryLabels); }
	if (IsPropDirty("Locale")) { ser.AddStringProp("locale", this._locale); }
	if (IsPropDirty("ValueFormat")) { ser.AddStringProp("valueFormat", this._valueFormat); }
	if (IsPropDirty("TickLabelRotation")) { ser.AddEnumProp("tickLabelRotation", this._tickLabelRotation); }
	if (IsPropDirty("ValueFormatOptions")) { ser.AddSerializableProp("valueFormatOptions", (JsonSerializable)this._valueFormatOptions); }
	
	    }
	
}
}
