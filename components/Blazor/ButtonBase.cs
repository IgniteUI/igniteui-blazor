
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbButtonBase: BaseRendererControl {
                                public override string Type { get { return "WebButtonBase"; } }

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
                            return "igc-button-base";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbButtonBase(): base() {
	        OnCreatedIgbButtonBase();
	
	        
	    }
	
	    partial void OnCreatedIgbButtonBase();
	    
	private ButtonBaseType _displayType = ButtonBaseType.Button;
	
	partial void OnDisplayTypeChanging(ref ButtonBaseType newValue);
	/// <summary>
	/// The type of the button. Defaults to `button`.
	/// </summary>
	[Parameter]
	[WCWidgetMemberName("Type")]
	public ButtonBaseType DisplayType 
	{
	get { return this._displayType; }
	set { 
	                if (this._displayType != value || !IsPropDirty("DisplayType")) {
	                        MarkPropDirty("DisplayType");
	                } 
	                this._displayType = value;
	                 
	                }
	}
	private string _href;
	
	partial void OnHrefChanging(ref string newValue);
	/// <summary>
	/// The URL the button points to.
	/// </summary>
	[Parameter]
	public string Href 
	{
	get { return this._href; }
	set { 
	                if (this._href != value || !IsPropDirty("Href")) {
	                        MarkPropDirty("Href");
	                } 
	                this._href = value;
	                 
	                }
	}
	private string _download;
	
	partial void OnDownloadChanging(ref string newValue);
	/// <summary>
	/// Prompts to save the linked URL instead of navigating to it.
	/// </summary>
	[Parameter]
	public string Download 
	{
	get { return this._download; }
	set { 
	                if (this._download != value || !IsPropDirty("Download")) {
	                        MarkPropDirty("Download");
	                } 
	                this._download = value;
	                 
	                }
	}
	private ButtonBaseTarget _target = ButtonBaseTarget._blank;
	
	partial void OnTargetChanging(ref ButtonBaseTarget newValue);
	/// <summary>
	/// Where to display the linked URL, as the name for a browsing context.
	/// </summary>
	[Parameter]
	public ButtonBaseTarget Target 
	{
	get { return this._target; }
	set { 
	                if (this._target != value || !IsPropDirty("Target")) {
	                        MarkPropDirty("Target");
	                } 
	                this._target = value;
	                 
	                }
	}
	private string _rel;
	
	partial void OnRelChanging(ref string newValue);
	/// <summary>
	/// The relationship of the linked URL.
	/// See https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types
	/// </summary>
	[Parameter]
	public string Rel 
	{
	get { return this._rel; }
	set { 
	                if (this._rel != value || !IsPropDirty("Rel")) {
	                        MarkPropDirty("Rel");
	                } 
	                this._rel = value;
	                 
	                }
	}
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// The disabled state of the component
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
	
	    partial void FindByNameButtonBase(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameButtonBase(name, ref item);
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
	/// Sets focus in the button.
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
	/// Simulates a mouse click on the element
	/// </summary>
	public async  Task ClickAsync() 
	                    {
		await InvokeMethod("click", new object[] {  }, new string[] {  });
	}
	                    public  void Click() 
	                    {
		InvokeMethodSync("click", new object[] {  }, new string[] {  });
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
	
	    private string _focusRef = null;
	    private string _focusScript = null;
	    [Parameter]
	    public string FocusScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Focus", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._focusRef = refName;
	                this.MarkPropDirty("FocusRef");	
	        }); 
	        }
	        get 
	        {
	            return this._focusScript;
	        }
	    }
	
	    partial void OnHandlingFocus(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _focus = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Focus
	    {
	        get 
	        {
	            return this._focus != null ? this._focus.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _focus, ref eventCallbacksCache))
	                {
	                    _focus = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", value, (args) => {
	                        OnHandlingFocus(args);
	                        
	                    });
	        this.OnRefChanged("Focus", null, "nativeEvent:::Focus", true, false, (refName, oldValue, newValue) => {
	                        this._focusRef = refName;
	                        this.MarkPropDirty("FocusRef");	
	                });
	                }
	    }
	        else 
	            {
	                _focus = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Focus", null);
	    this.OnRefChanged("Focus", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._focusRef = null;
	                    this.MarkPropDirty("FocusRef");	
	            });
	    }
	    }
	    }
	
	    private string _blurRef = null;
	    private string _blurScript = null;
	    [Parameter]
	    public string BlurScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Blur", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._blurRef = refName;
	                this.MarkPropDirty("BlurRef");	
	        }); 
	        }
	        get 
	        {
	            return this._blurScript;
	        }
	    }
	
	    partial void OnHandlingBlur(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _blur = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Blur
	    {
	        get 
	        {
	            return this._blur != null ? this._blur.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _blur, ref eventCallbacksCache))
	                {
	                    _blur = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", value, (args) => {
	                        OnHandlingBlur(args);
	                        
	                    });
	        this.OnRefChanged("Blur", null, "nativeEvent:::Blur", true, false, (refName, oldValue, newValue) => {
	                        this._blurRef = refName;
	                        this.MarkPropDirty("BlurRef");	
	                });
	                }
	    }
	        else 
	            {
	                _blur = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Blur", null);
	    this.OnRefChanged("Blur", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._blurRef = null;
	                    this.MarkPropDirty("BlurRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbButtonBase(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbButtonBase(ser);
	
	if (IsPropDirty("DisplayType")) { ser.AddEnumProp("displayType", this._displayType); }
	if (IsPropDirty("Href")) { ser.AddStringProp("href", this._href); }
	if (IsPropDirty("Download")) { ser.AddStringProp("download", this._download); }
	if (IsPropDirty("Target")) { ser.AddEnumProp("target", this._target); }
	if (IsPropDirty("Rel")) { ser.AddStringProp("rel", this._rel); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	
	    }
	
}
}
