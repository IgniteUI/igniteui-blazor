
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The `igc-toggle-button` wraps a native button element and exposes additional `value` and `selected` properties.
/// It is used in the context of an `igc-button-group` to facilitate the creation of group/toolbar like UX behaviors.
/// </summary>
public partial class IgbToggleButton: BaseRendererControl {
                                public override string Type { get { return "WebToggleButton"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbToggleButtonModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbToggleButtonModule.Register(IgBlazor);
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
                            return "igc-toggle-button";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbToggleButton(): base() {
	        OnCreatedIgbToggleButton();
	
	        
	    }
	
	    partial void OnCreatedIgbToggleButton();
	    
	private string _value;
	
	partial void OnValueChanging(ref string newValue);
	/// <summary>
	/// The value attribute of the control.
	/// </summary>
	[Parameter]
	public string Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	private bool _selected = false;
	
	partial void OnSelectedChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the button is selected.
	/// </summary>
	[Parameter]
	public bool Selected 
	{
	get { return this._selected; }
	set { 
	                if (this._selected != value || !IsPropDirty("Selected")) {
	                        MarkPropDirty("Selected");
	                } 
	                this._selected = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the button is disabled.
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
	
	    partial void FindByNameToggleButton(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameToggleButton(name, ref item);
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
	/// <summary>
	/// Sets focus on the button.
	/// </summary>
	
	[WCWidgetMemberName("Focus")]
	public async  Task FocusComponentAsync(IgbFocusOptions options) 
	                    {
		await InvokeMethod("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
	}
	                    
	[WCWidgetMemberName("Focus")]
	public  void FocusComponent(IgbFocusOptions options) 
	                    {
		InvokeMethodSync("focus", new object[] { ObjectToParam(options) }, new string[] { "Json" });
	}
	/// <summary>
	/// Removes focus from the button.
	/// </summary>
	
	[WCWidgetMemberName("Blur")]
	public async  Task BlurComponentAsync() 
	                    {
		await InvokeMethod("blur", new object[] {  }, new string[] {  });
	}
	                    
	[WCWidgetMemberName("Blur")]
	public  void BlurComponent() 
	                    {
		InvokeMethodSync("blur", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Simulates a mouse click on the element.
	/// </summary>
	public async  Task ClickAsync() 
	                    {
		await InvokeMethod("click", new object[] {  }, new string[] {  });
	}
	                    public  void Click() 
	                    {
		InvokeMethodSync("click", new object[] {  }, new string[] {  });
	}
	
	    partial void SerializeCoreIgbToggleButton(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbToggleButton(ser);
	
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Selected")) { ser.AddBooleanProp("selected", this._selected); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	
	    }
	
}
}
