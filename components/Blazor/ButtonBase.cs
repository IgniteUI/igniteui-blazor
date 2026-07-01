
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
	/// The type of the button, which determines its behavior and semantics.
	/// - `'button'` – no default action; useful for custom JavaScript handlers.
	/// - `'submit'` – submits the associated form when clicked.
	/// - `'reset'` – resets the associated form fields to their initial values.
	/// Ignored when the button is rendered as a link (i.e. `href` is set).
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
	/// Prompts the browser to download the linked resource rather than navigating
	/// to it. The optional value is used as the suggested file name.
	/// Only effective when `href` is set.
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
	/// Where to open the linked document. Only effective when `href` is set.
	/// - `'_self'` – current browsing context (default browser behavior).
	/// - `'_blank'` – new tab or window.
	/// - `'_parent'` – parent browsing context; falls back to `_self` if none.
	/// - `'_top'` – top-level browsing context; falls back to `_self` if none.
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
	/// The relationship between the current document and the linked URL.
	/// Accepts a space-separated list of link types (e.g. `'noopener noreferrer'`).
	/// Only effective when `href` is set. When `target="_blank"` is used,
	/// setting `rel="noopener noreferrer"` is strongly recommended for security.
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
	/// When set, the button will be disabled and non-interactive.
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
	private string _command;
	
	partial void OnCommandChanging(ref string newValue);
	/// <summary>
	/// The command to invoke on the target element specified by `commandfor`.
	/// Part of the [Invoker Commands](https://developer.mozilla.org/en-US/docs/Web/API/Invoker_Commands_API) API.
	/// Custom commands must start with two dashes (e.g. `'--my-command'`).
	/// </summary>
	[Parameter]
	public string Command 
	{
	get { return this._command; }
	set { 
	                if (this._command != value || !IsPropDirty("Command")) {
	                        MarkPropDirty("Command");
	                } 
	                this._command = value;
	                 
	                }
	}
	private string? _commandfor;
	
	partial void OnCommandforChanging(ref string? newValue);
	/// <summary>
	/// The ID of the target element for the invoker command.
	/// Part of the [Invoker Commands API](https://developer.mozilla.org/en-US/docs/Web/API/Invoker_Commands_API).
	/// </summary>
	[Parameter]
	public string? Commandfor 
	{
	get { return this._commandfor; }
	set { 
	                if (this._commandfor != value || !IsPropDirty("Commandfor")) {
	                        MarkPropDirty("Commandfor");
	                } 
	                this._commandfor = value;
	                 
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
	public async  Task SetNativeElementAsync(Object element) 
	                    {
		await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	                    public  void SetNativeElement(Object element) 
	                    {
		InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
	}
	/// <summary>
	/// Simulates a mouse click on the button, triggering its click handler and any associated form action.
	/// </summary>
	public async  Task ClickAsync() 
	                    {
		await InvokeMethod("click", new object[] {  }, new string[] {  });
	}
	                    public  void Click() 
	                    {
		InvokeMethodSync("click", new object[] {  }, new string[] {  });
	}
	
	    private string _focusRef = null;
	    private string _focusScript = null;
	    [Parameter]
	    public string FocusScript { 
	    
	        set 
	        {
	            if (value != this._focusScript)
	            {
	                this._focusScript = value;
	                this.OnRefChanged("Focus", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._focusRef = refName;
	                    this.MarkPropDirty("FocusRef");	
	                });
	            }
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
	            if (value != this._blurScript)
	            {
	                this._blurScript = value;
	                this.OnRefChanged("Blur", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                    this._blurRef = refName;
	                    this.MarkPropDirty("BlurRef");	
	                });
	            }
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
	if (IsPropDirty("Command")) { ser.AddStringProp("command", this._command); }
	if (IsPropDirty("Commandfor")) { ser.AddStringProp("commandfor", this._commandfor); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	
	    }
	
}
}
