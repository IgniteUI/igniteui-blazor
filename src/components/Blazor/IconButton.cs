
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A button that displays a single icon, designed for compact, icon-only
/// interactions such as toolbar actions, floating action buttons, or inline
/// controls.
/// The icon is sourced from the icon registry via the `name` and `collection`
/// attributes. Like the normal button, it can render as an anchor element when
/// `href` is set and is fully form-associated.
/// </summary>
public partial class IgbIconButton: IgbButtonBase {
                                public override string Type { get { return "WebIconButton"; } }
							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbIconButtonModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbIconButtonModule.Register(IgBlazor);
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
	                            return "igc-icon-button";
                                }
                        }
	
	    public IgbIconButton(): base() {
	        OnCreatedIgbIconButton();
	
	        
	    }
	
	    partial void OnCreatedIgbIconButton();
	    
	private string _iconName;
	
	partial void OnIconNameChanging(ref string newValue);
	/// <summary>
	/// The name of the icon to display.
	/// </summary>
	[Parameter]
	[WCWidgetMemberName("Name")]
	public string IconName 
	{
	get { return this._iconName; }
	set { 
	                if (this._iconName != value || !IsPropDirty("IconName")) {
	                        MarkPropDirty("IconName");
	                } 
	                this._iconName = value;
	                 
	                }
	}
	private string _collection;
	
	partial void OnCollectionChanging(ref string newValue);
	/// <summary>
	/// The collection the icon belongs to.
	/// </summary>
	[Parameter]
	public string Collection 
	{
	get { return this._collection; }
	set { 
	                if (this._collection != value || !IsPropDirty("Collection")) {
	                        MarkPropDirty("Collection");
	                } 
	                this._collection = value;
	                 
	                }
	}
	private bool _mirrored = false;
	
	partial void OnMirroredChanging(ref bool newValue);
	/// <summary>
	/// Determines whether the icon should be mirrored in right-to-left contexts.
	/// </summary>
	[Parameter]
	public bool Mirrored 
	{
	get { return this._mirrored; }
	set { 
	                if (this._mirrored != value || !IsPropDirty("Mirrored")) {
	                        MarkPropDirty("Mirrored");
	                } 
	                this._mirrored = value;
	                 
	                }
	}
	private IconButtonVariant _variant = IconButtonVariant.Contained;
	
	partial void OnVariantChanging(ref IconButtonVariant newValue);
	/// <summary>
	/// The variant of the button which determines its visual appearance.
	/// - `contained` – filled background; highest visual emphasis (default).
	/// - `outlined` – transparent background with a visible border.
	/// - `flat` – no background or border; lowest visual emphasis.
	/// </summary>
	[Parameter]
	public IconButtonVariant Variant 
	{
	get { return this._variant; }
	set { 
	                if (this._variant != value || !IsPropDirty("Variant")) {
	                        MarkPropDirty("Variant");
	                } 
	                this._variant = value;
	                 
	                }
	}
	
	    partial void FindByNameIconButton(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameIconButton(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	public async  Task RegisterIconAsync(String name, String url, String collection = null) 
	                    {
		await InvokeMethod("registerIcon", new object[] { StringToString(name), StringToString(url), StringToString(collection) }, new string[] { "String", "String", "String" });
	}
	                    public  void RegisterIcon(String name, String url, String collection = null) 
	                    {
		InvokeMethodSync("registerIcon", new object[] { StringToString(name), StringToString(url), StringToString(collection) }, new string[] { "String", "String", "String" });
	}
	public async  Task RegisterIconFromTextAsync(String name, String iconText, String collection = null) 
	                    {
		await InvokeMethod("registerIconFromText", new object[] { StringToString(name), StringToString(iconText), StringToString(collection) }, new string[] { "String", "String", "String" });
	}
	                    public  void RegisterIconFromText(String name, String iconText, String collection = null) 
	                    {
		InvokeMethodSync("registerIconFromText", new object[] { StringToString(name), StringToString(iconText), StringToString(collection) }, new string[] { "String", "String", "String" });
	}
	
	    partial void SerializeCoreIgbIconButton(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbIconButton(ser);
	
	if (IsPropDirty("IconName")) { ser.AddStringProp("iconName", this._iconName); }
	if (IsPropDirty("Collection")) { ser.AddStringProp("collection", this._collection); }
	if (IsPropDirty("Mirrored")) { ser.AddBooleanProp("mirrored", this._mirrored); }
	if (IsPropDirty("Variant")) { ser.AddEnumProp("variant", this._variant); }
	
	    }
	
}
}
