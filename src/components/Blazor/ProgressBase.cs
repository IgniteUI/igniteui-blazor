
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbProgressBase: BaseRendererControl {
                                public override string Type { get { return "WebProgressBase"; } }

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
                            return "igc-progress-base";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbProgressBase(): base() {
	        OnCreatedIgbProgressBase();
	
	        
	    }
	
	    partial void OnCreatedIgbProgressBase();
	    
	private double _max = 0;
	
	partial void OnMaxChanging(ref double newValue);
	/// <summary>
	/// Maximum value of the control.
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
	private double _value = 0;
	
	partial void OnValueChanging(ref double newValue);
	/// <summary>
	/// The value of the control.
	/// </summary>
	[Parameter]
	public double Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	private StyleVariant _variant = StyleVariant.Primary;
	
	partial void OnVariantChanging(ref StyleVariant newValue);
	/// <summary>
	/// The variant of the control.
	/// </summary>
	[Parameter]
	public StyleVariant Variant 
	{
	get { return this._variant; }
	set { 
	                if (this._variant != value || !IsPropDirty("Variant")) {
	                        MarkPropDirty("Variant");
	                } 
	                this._variant = value;
	                 
	                }
	}
	private double _animationDuration = 0;
	
	partial void OnAnimationDurationChanging(ref double newValue);
	/// <summary>
	/// Animation duration in milliseconds.
	/// </summary>
	[Parameter]
	public double AnimationDuration 
	{
	get { return this._animationDuration; }
	set { 
	                if (this._animationDuration != value || !IsPropDirty("AnimationDuration")) {
	                        MarkPropDirty("AnimationDuration");
	                } 
	                this._animationDuration = value;
	                 
	                }
	}
	private bool _indeterminate = false;
	
	partial void OnIndeterminateChanging(ref bool newValue);
	/// <summary>
	/// The indeterminate state of the control.
	/// </summary>
	[Parameter]
	public bool Indeterminate 
	{
	get { return this._indeterminate; }
	set { 
	                if (this._indeterminate != value || !IsPropDirty("Indeterminate")) {
	                        MarkPropDirty("Indeterminate");
	                } 
	                this._indeterminate = value;
	                 
	                }
	}
	private bool _hideLabel = false;
	
	partial void OnHideLabelChanging(ref bool newValue);
	/// <summary>
	/// Shows/hides the label of the control.
	/// </summary>
	[Parameter]
	public bool HideLabel 
	{
	get { return this._hideLabel; }
	set { 
	                if (this._hideLabel != value || !IsPropDirty("HideLabel")) {
	                        MarkPropDirty("HideLabel");
	                } 
	                this._hideLabel = value;
	                 
	                }
	}
	private string _labelFormat;
	
	partial void OnLabelFormatChanging(ref string newValue);
	/// <summary>
	/// Format string for the default label of the control.
	/// Placeholders:
	/// {0} - current value of the control.
	/// {1} - max value of the control.
	/// </summary>
	[Parameter]
	public string LabelFormat 
	{
	get { return this._labelFormat; }
	set { 
	                if (this._labelFormat != value || !IsPropDirty("LabelFormat")) {
	                        MarkPropDirty("LabelFormat");
	                } 
	                this._labelFormat = value;
	                 
	                }
	}
	
	    partial void FindByNameProgressBase(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameProgressBase(name, ref item);
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
	
	    partial void SerializeCoreIgbProgressBase(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbProgressBase(ser);
	
	if (IsPropDirty("Max")) { ser.AddNumberProp("max", this._max); }
	if (IsPropDirty("Value")) { ser.AddNumberProp("value", this._value); }
	if (IsPropDirty("Variant")) { ser.AddEnumProp("variant", this._variant); }
	if (IsPropDirty("AnimationDuration")) { ser.AddNumberProp("animationDuration", this._animationDuration); }
	if (IsPropDirty("Indeterminate")) { ser.AddBooleanProp("indeterminate", this._indeterminate); }
	if (IsPropDirty("HideLabel")) { ser.AddBooleanProp("hideLabel", this._hideLabel); }
	if (IsPropDirty("LabelFormat")) { ser.AddStringProp("labelFormat", this._labelFormat); }
	
	    }
	
}
}
