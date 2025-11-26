
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The step component is used within the `igc-stepper` element and it holds the content of each step.
/// It also supports custom indicators, title and subtitle.
/// </summary>
public partial class IgbStep: BaseRendererControl {
                                public override string Type { get { return "WebStep"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbStepModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbStepModule.Register(IgBlazor);
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
                            return "igc-step";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbStep(): base() {
	        OnCreatedIgbStep();
	
	        
	    }
	
	    partial void OnCreatedIgbStep();
	    
	private bool _invalid = false;
	
	partial void OnInvalidChanging(ref bool newValue);
	/// <summary>
	/// Gets/sets whether the step is invalid.
	/// </summary>
	[Parameter]
	public bool Invalid 
	{
	get { return this._invalid; }
	set { 
	                if (this._invalid != value || !IsPropDirty("Invalid")) {
	                        MarkPropDirty("Invalid");
	                } 
	                this._invalid = value;
	                 
	                }
	}
	private bool _active = false;
	
	partial void OnActiveChanging(ref bool newValue);
	/// <summary>
	/// Gets/sets whether the step is activе.
	/// </summary>
	[Parameter]
	public bool Active 
	{
	get { return this._active; }
	set { 
	                if (this._active != value || !IsPropDirty("Active")) {
	                        MarkPropDirty("Active");
	                } 
	                this._active = value;
	                 
	                }
	}
	private bool _optional = false;
	
	partial void OnOptionalChanging(ref bool newValue);
	/// <summary>
	/// Gets/sets whether the step is optional.
	/// @remarks
	/// Optional steps validity does not affect the default behavior when the stepper is in linear mode i.e.
	/// if optional step is invalid the user could still move to the next step.
	/// </summary>
	[Parameter]
	public bool Optional 
	{
	get { return this._optional; }
	set { 
	                if (this._optional != value || !IsPropDirty("Optional")) {
	                        MarkPropDirty("Optional");
	                } 
	                this._optional = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Gets/sets whether the step is interactable.
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
	private bool _complete = false;
	
	partial void OnCompleteChanging(ref bool newValue);
	/// <summary>
	/// Gets/sets whether the step is completed.
	/// @remarks
	/// When set to `true` the following separator is styled `solid`.
	/// </summary>
	[Parameter]
	public bool Complete 
	{
	get { return this._complete; }
	set { 
	                if (this._complete != value || !IsPropDirty("Complete")) {
	                        MarkPropDirty("Complete");
	                } 
	                this._complete = value;
	                 
	                }
	}
	
	    partial void FindByNameStep(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameStep(name, ref item);
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
	
	    partial void SerializeCoreIgbStep(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbStep(ser);
	
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("Active")) { ser.AddBooleanProp("active", this._active); }
	if (IsPropDirty("Optional")) { ser.AddBooleanProp("optional", this._optional); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Complete")) { ser.AddBooleanProp("complete", this._complete); }
	
	    }
	
}
}
