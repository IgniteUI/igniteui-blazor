
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The badge is a component indicating a status on a related item or an area
/// where some active indication is required.
/// </summary>
public partial class IgbBadge: BaseRendererControl {
                                public override string Type { get { return "WebBadge"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbBadgeModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbBadgeModule.Register(IgBlazor);
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
                            return "igc-badge";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbBadge(): base() {
	        OnCreatedIgbBadge();
	
	        
	    }
	
	    partial void OnCreatedIgbBadge();
	    
	private StyleVariant _variant = StyleVariant.Primary;
	
	partial void OnVariantChanging(ref StyleVariant newValue);
	/// <summary>
	/// The type of badge.
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
	private bool _outlined = false;
	
	partial void OnOutlinedChanging(ref bool newValue);
	/// <summary>
	/// Sets whether to draw an outlined version of the badge.
	/// </summary>
	[Parameter]
	public bool Outlined 
	{
	get { return this._outlined; }
	set { 
	                if (this._outlined != value || !IsPropDirty("Outlined")) {
	                        MarkPropDirty("Outlined");
	                } 
	                this._outlined = value;
	                 
	                }
	}
	private BadgeShape _shape = BadgeShape.Rounded;
	
	partial void OnShapeChanging(ref BadgeShape newValue);
	/// <summary>
	/// The shape of the badge.
	/// </summary>
	[Parameter]
	public BadgeShape Shape 
	{
	get { return this._shape; }
	set { 
	                if (this._shape != value || !IsPropDirty("Shape")) {
	                        MarkPropDirty("Shape");
	                } 
	                this._shape = value;
	                 
	                }
	}
	
	    partial void FindByNameBadge(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameBadge(name, ref item);
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
	
	    partial void SerializeCoreIgbBadge(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbBadge(ser);
	
	if (IsPropDirty("Variant")) { ser.AddEnumProp("variant", this._variant); }
	if (IsPropDirty("Outlined")) { ser.AddBooleanProp("outlined", this._outlined); }
	if (IsPropDirty("Shape")) { ser.AddEnumProp("shape", this._shape); }
	
	    }
	
}
}
