
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// An avatar component is used as a representation of a user identity
/// typically in a user profile.
/// </summary>
public partial class IgbAvatar: BaseRendererControl {
                                public override string Type { get { return "WebAvatar"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbAvatarModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbAvatarModule.Register(IgBlazor);
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
                            return "igc-avatar";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbAvatar(): base() {
	        OnCreatedIgbAvatar();
	
	        
	    }
	
	    partial void OnCreatedIgbAvatar();
	    
	private string _src;
	
	partial void OnSrcChanging(ref string newValue);
	/// <summary>
	/// The image source to use.
	/// </summary>
	[Parameter]
	public string Src 
	{
	get { return this._src; }
	set { 
	                if (this._src != value || !IsPropDirty("Src")) {
	                        MarkPropDirty("Src");
	                } 
	                this._src = value;
	                 
	                }
	}
	private string _alt;
	
	partial void OnAltChanging(ref string newValue);
	/// <summary>
	/// Alternative text for the image.
	/// </summary>
	[Parameter]
	public string Alt 
	{
	get { return this._alt; }
	set { 
	                if (this._alt != value || !IsPropDirty("Alt")) {
	                        MarkPropDirty("Alt");
	                } 
	                this._alt = value;
	                 
	                }
	}
	private string _initials;
	
	partial void OnInitialsChanging(ref string newValue);
	/// <summary>
	/// Initials to use as a fallback when no image is available.
	/// </summary>
	[Parameter]
	public string Initials 
	{
	get { return this._initials; }
	set { 
	                if (this._initials != value || !IsPropDirty("Initials")) {
	                        MarkPropDirty("Initials");
	                } 
	                this._initials = value;
	                 
	                }
	}
	private AvatarShape _shape = AvatarShape.Square;
	
	partial void OnShapeChanging(ref AvatarShape newValue);
	/// <summary>
	/// The shape of the avatar.
	/// </summary>
	[Parameter]
	public AvatarShape Shape 
	{
	get { return this._shape; }
	set { 
	                if (this._shape != value || !IsPropDirty("Shape")) {
	                        MarkPropDirty("Shape");
	                } 
	                this._shape = value;
	                 
	                }
	}
	
	    partial void FindByNameAvatar(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameAvatar(name, ref item);
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
	
	    partial void SerializeCoreIgbAvatar(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbAvatar(ser);
	
	if (IsPropDirty("Src")) { ser.AddStringProp("src", this._src); }
	if (IsPropDirty("Alt")) { ser.AddStringProp("alt", this._alt); }
	if (IsPropDirty("Initials")) { ser.AddStringProp("initials", this._initials); }
	if (IsPropDirty("Shape")) { ser.AddEnumProp("shape", this._shape); }
	
	    }
	
}
}
