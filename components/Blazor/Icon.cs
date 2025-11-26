
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The icon component allows visualizing collections of pre-registered SVG icons.
/// </summary>
public partial class IgbIcon: BaseRendererControl {
                                public override string Type { get { return "WebIcon"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbIconModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbIconModule.Register(IgBlazor);
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
                            return "igc-icon";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbIcon(): base() {
	        OnCreatedIgbIcon();
	
	        
	    }
	
	    partial void OnCreatedIgbIcon();
	    
	private string _iconName;
	
	partial void OnIconNameChanging(ref string newValue);
	/// <summary>
	/// The name of the icon glyph to draw.
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
	/// The name of the registered collection for look up of icons.
	/// Defaults to `default`.
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
	/// Whether to flip the icon. Useful for RTL layouts.
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
	
	    partial void FindByNameIcon(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameIcon(name, ref item);
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
	public async  Task ConnectedCallbackAsync() 
	                    {
		await InvokeMethod("connectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void ConnectedCallback() 
	                    {
		InvokeMethodSync("connectedCallback", new object[] {  }, new string[] {  });
	}
	public async  Task DisconnectedCallbackAsync() 
	                    {
		await InvokeMethod("disconnectedCallback", new object[] {  }, new string[] {  });
	}
	                    public  void DisconnectedCallback() 
	                    {
		InvokeMethodSync("disconnectedCallback", new object[] {  }, new string[] {  });
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
	public async  Task SetIconRefAsync(String name, String collection, IgbIconMeta icon) 
	                    {
		await InvokeMethod("setIconRef", new object[] { StringToString(name), StringToString(collection), ObjectToParam(icon) }, new string[] { "String", "String", "Json" });
	}
	                    public  void SetIconRef(String name, String collection, IgbIconMeta icon) 
	                    {
		InvokeMethodSync("setIconRef", new object[] { StringToString(name), StringToString(collection), ObjectToParam(icon) }, new string[] { "String", "String", "Json" });
	}
	
	    partial void SerializeCoreIgbIcon(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbIcon(ser);
	
	if (IsPropDirty("IconName")) { ser.AddStringProp("iconName", this._iconName); }
	if (IsPropDirty("Collection")) { ser.AddStringProp("collection", this._collection); }
	if (IsPropDirty("Mirrored")) { ser.AddBooleanProp("mirrored", this._mirrored); }
	
	    }
	
}
}
