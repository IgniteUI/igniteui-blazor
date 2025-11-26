
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A range slider component used to select two numeric values within a range.
/// </summary>
public partial class IgbRangeSlider: IgbSliderBase {
                                public override string Type { get { return "WebRangeSlider"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbRangeSliderModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbRangeSliderModule.Register(IgBlazor);
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
	                            return "igc-range-slider";
                                }
                        }
	
	    public IgbRangeSlider(): base() {
	        OnCreatedIgbRangeSlider();
	
	        
	    }
	
	    partial void OnCreatedIgbRangeSlider();
	    
	private double _lower = 0;
	
	partial void OnLowerChanging(ref double newValue);
	/// <summary>
	/// The current value of the lower thumb.
	/// </summary>
	[Parameter]
	public double Lower 
	{
	get { return this._lower; }
	set { 
	                if (this._lower != value || !IsPropDirty("Lower")) {
	                        MarkPropDirty("Lower");
	                } 
	                this._lower = value;
	                 
	                }
	}
	private double _upper = 0;
	
	partial void OnUpperChanging(ref double newValue);
	/// <summary>
	/// The current value of the upper thumb.
	/// </summary>
	[Parameter]
	public double Upper 
	{
	get { return this._upper; }
	set { 
	                if (this._upper != value || !IsPropDirty("Upper")) {
	                        MarkPropDirty("Upper");
	                } 
	                this._upper = value;
	                 
	                }
	}
	private string _thumbLabelLower;
	
	partial void OnThumbLabelLowerChanging(ref string newValue);
	/// <summary>
	/// The aria label for the lower thumb.
	/// </summary>
	[Parameter]
	public string ThumbLabelLower 
	{
	get { return this._thumbLabelLower; }
	set { 
	                if (this._thumbLabelLower != value || !IsPropDirty("ThumbLabelLower")) {
	                        MarkPropDirty("ThumbLabelLower");
	                } 
	                this._thumbLabelLower = value;
	                 
	                }
	}
	private string _thumbLabelUpper;
	
	partial void OnThumbLabelUpperChanging(ref string newValue);
	/// <summary>
	/// The aria label for the upper thumb.
	/// </summary>
	[Parameter]
	public string ThumbLabelUpper 
	{
	get { return this._thumbLabelUpper; }
	set { 
	                if (this._thumbLabelUpper != value || !IsPropDirty("ThumbLabelUpper")) {
	                        MarkPropDirty("ThumbLabelUpper");
	                } 
	                this._thumbLabelUpper = value;
	                 
	                }
	}
	
	    partial void FindByNameRangeSlider(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameRangeSlider(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    private string _inputRef = null;
	    private string _inputScript = null;
	    [Parameter]
	    public string InputScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Input", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._inputRef = refName;
	                this.MarkPropDirty("InputRef");	
	        }); 
	        }
	        get 
	        {
	            return this._inputScript;
	        }
	    }
	
	    partial void OnHandlingInput(IgbRangeSliderValueEventArgs args);
	    private EventCallback<IgbRangeSliderValueEventArgs>? _input = null;
	    [Parameter]
	    public EventCallback<IgbRangeSliderValueEventArgs> Input
	    {
	        get 
	        {
	            return this._input != null ? this._input.Value : EventCallback<IgbRangeSliderValueEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbRangeSliderValueEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _input, ref eventCallbacksCache))
	                {
	                    _input = value;
	                    this.SetHandler<IgbRangeSliderValueEventArgs>(this.Name, "Input", value, (args) => {
	                        OnHandlingInput(args);
	                        
	                    });
	        this.OnRefChanged("Input", null, "event:::Input", true, false, (refName, oldValue, newValue) => {
	                        this._inputRef = refName;
	                        this.MarkPropDirty("InputRef");	
	                });
	                }
	    }
	        else 
	            {
	                _input = null;
	                this.SetHandler<IgbRangeSliderValueEventArgs>(this.Name, "Input", null);
	    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._inputRef = null;
	                    this.MarkPropDirty("InputRef");	
	            });
	    }
	    }
	    }
	
	    private string _changeRef = null;
	    private string _changeScript = null;
	    [Parameter]
	    public string ChangeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._changeRef = refName;
	                this.MarkPropDirty("ChangeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._changeScript;
	        }
	    }
	
	    partial void OnHandlingChange(IgbRangeSliderValueEventArgs args);
	    private EventCallback<IgbRangeSliderValueEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbRangeSliderValueEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbRangeSliderValueEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbRangeSliderValueEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbRangeSliderValueEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	                    });
	        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) => {
	                        this._changeRef = refName;
	                        this.MarkPropDirty("ChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _change = null;
	                this.SetHandler<IgbRangeSliderValueEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbRangeSlider(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbRangeSlider(ser);
	
	if (IsPropDirty("Lower")) { ser.AddNumberProp("lower", this._lower); }
	if (IsPropDirty("Upper")) { ser.AddNumberProp("upper", this._upper); }
	if (IsPropDirty("ThumbLabelLower")) { ser.AddStringProp("thumbLabelLower", this._thumbLabelLower); }
	if (IsPropDirty("ThumbLabelUpper")) { ser.AddStringProp("thumbLabelUpper", this._thumbLabelUpper); }
	if (IsPropDirty("InputRef")) { ser.AddStringProp("inputRef", this._inputRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	
	    }
	
}
}
