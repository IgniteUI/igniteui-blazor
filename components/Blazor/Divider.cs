
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The igc-divider allows the content author to easily create a horizontal/vertical rule as a break between content to better organize information on a page.
/// @cssproperty --color - Sets the color of the divider.
/// @cssproperty --inset - Shrinks the divider by the given amount from the start. If `middle` is set it will shrink from both sides.
/// </summary>
public partial class IgbDivider: BaseRendererControl {
                                public override string Type { get { return "WebDivider"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDividerModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDividerModule.Register(IgBlazor);
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
                            return "igc-divider";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbDivider(): base() {
	        OnCreatedIgbDivider();
	
	        
	    }
	
	    partial void OnCreatedIgbDivider();
	    
	private bool _vertical = false;
	
	partial void OnVerticalChanging(ref bool newValue);
	/// <summary>
	/// Whether to render a vertical divider line.
	/// @default false
	/// </summary>
	[Parameter]
	public bool Vertical 
	{
	get { return this._vertical; }
	set { 
	                if (this._vertical != value || !IsPropDirty("Vertical")) {
	                        MarkPropDirty("Vertical");
	                } 
	                this._vertical = value;
	                 
	                }
	}
	private bool _middle = false;
	
	partial void OnMiddleChanging(ref bool newValue);
	/// <summary>
	/// When set and inset is provided, it will shrink the divider line from both sides.
	/// @default false
	/// </summary>
	[Parameter]
	public bool Middle 
	{
	get { return this._middle; }
	set { 
	                if (this._middle != value || !IsPropDirty("Middle")) {
	                        MarkPropDirty("Middle");
	                } 
	                this._middle = value;
	                 
	                }
	}
	private DividerType _lineType = DividerType.Solid;
	
	partial void OnLineTypeChanging(ref DividerType newValue);
	/// <summary>
	/// Whether to render a solid or a dashed divider line.
	/// @default 'solid'
	/// </summary>
	[Parameter]
	[WCWidgetMemberName("Type")]
	public DividerType LineType 
	{
	get { return this._lineType; }
	set { 
	                if (this._lineType != value || !IsPropDirty("LineType")) {
	                        MarkPropDirty("LineType");
	                } 
	                this._lineType = value;
	                 
	                }
	}
	
	    partial void FindByNameDivider(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDivider(name, ref item);
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
	
	    partial void SerializeCoreIgbDivider(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDivider(ser);
	
	if (IsPropDirty("Vertical")) { ser.AddBooleanProp("vertical", this._vertical); }
	if (IsPropDirty("Middle")) { ser.AddBooleanProp("middle", this._middle); }
	if (IsPropDirty("LineType")) { ser.AddEnumProp("lineType", this._lineType); }
	
	    }
	
}
}
