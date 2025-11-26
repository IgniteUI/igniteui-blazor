
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a Dialog component.
/// </summary>
public partial class IgbDialog: BaseRendererControl {
                                public override string Type { get { return "WebDialog"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbDialogModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbDialogModule.Register(IgBlazor);
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
                            return "igc-dialog";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbDialog(): base() {
	        OnCreatedIgbDialog();
	
	        
	    }
	
	    partial void OnCreatedIgbDialog();
	    
	private bool _keepOpenOnEscape = false;
	
	partial void OnKeepOpenOnEscapeChanging(ref bool newValue);
	/// <summary>
	/// Whether the dialog should be kept open when pressing the 'Escape' button.
	/// </summary>
	[Parameter]
	public bool KeepOpenOnEscape 
	{
	get { return this._keepOpenOnEscape; }
	set { 
	                if (this._keepOpenOnEscape != value || !IsPropDirty("KeepOpenOnEscape")) {
	                        MarkPropDirty("KeepOpenOnEscape");
	                } 
	                this._keepOpenOnEscape = value;
	                 
	                }
	}
	private bool _closeOnOutsideClick = false;
	
	partial void OnCloseOnOutsideClickChanging(ref bool newValue);
	/// <summary>
	/// Whether the dialog should be closed when clicking outside of it.
	/// </summary>
	[Parameter]
	public bool CloseOnOutsideClick 
	{
	get { return this._closeOnOutsideClick; }
	set { 
	                if (this._closeOnOutsideClick != value || !IsPropDirty("CloseOnOutsideClick")) {
	                        MarkPropDirty("CloseOnOutsideClick");
	                } 
	                this._closeOnOutsideClick = value;
	                 
	                }
	}
	private bool _hideDefaultAction = false;
	
	partial void OnHideDefaultActionChanging(ref bool newValue);
	/// <summary>
	/// Whether to hide the default action button for the dialog.
	/// When there is projected content in the `footer` slot this property
	/// has no effect.
	/// </summary>
	[Parameter]
	public bool HideDefaultAction 
	{
	get { return this._hideDefaultAction; }
	set { 
	                if (this._hideDefaultAction != value || !IsPropDirty("HideDefaultAction")) {
	                        MarkPropDirty("HideDefaultAction");
	                } 
	                this._hideDefaultAction = value;
	                 
	                }
	}
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Whether the dialog is opened.
	/// </summary>
	[Parameter]
	public bool Open 
	{
	get { return this._open; }
	set { 
	                if (this._open != value || !IsPropDirty("Open")) {
	                        MarkPropDirty("Open");
	                } 
	                this._open = value;
	                 
	                }
	}
	private string _title;
	
	partial void OnTitleChanging(ref string newValue);
	/// <summary>
	/// Sets the title of the dialog.
	/// </summary>
	[Parameter]
	public string Title 
	{
	get { return this._title; }
	set { 
	                if (this._title != value || !IsPropDirty("Title")) {
	                        MarkPropDirty("Title");
	                } 
	                this._title = value;
	                 
	                }
	}
	private string _returnValue;
	
	partial void OnReturnValueChanging(ref string newValue);
	/// <summary>
	/// Sets the return value for the dialog.
	/// </summary>
	[Parameter]
	public string ReturnValue 
	{
	get { return this._returnValue; }
	set { 
	                if (this._returnValue != value || !IsPropDirty("ReturnValue")) {
	                        MarkPropDirty("ReturnValue");
	                } 
	                this._returnValue = value;
	                 
	                }
	}
	
	    partial void FindByNameDialog(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameDialog(name, ref item);
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
	/// Opens the dialog.
	/// </summary>
	public async Task<bool> ShowAsync() 
	                    {
		var iv = await InvokeMethod("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Show() 
	                    {
		var iv = InvokeMethodSync("show", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Closes the dialog.
	/// </summary>
	public async Task<bool> HideAsync() 
	                    {
		var iv = await InvokeMethod("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Hide() 
	                    {
		var iv = InvokeMethodSync("hide", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Toggles the open state of the dialog.
	/// </summary>
	public async Task<bool> ToggleAsync() 
	                    {
		var iv = await InvokeMethod("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool Toggle() 
	                    {
		var iv = InvokeMethodSync("toggle", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	
	    private string _closingRef = null;
	    private string _closingScript = null;
	    [Parameter]
	    public string ClosingScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closing", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closingRef = refName;
	                this.MarkPropDirty("ClosingRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closingScript;
	        }
	    }
	
	    partial void OnHandlingClosing(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closing = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Closing
	    {
	        get 
	        {
	            return this._closing != null ? this._closing.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closing, ref eventCallbacksCache))
	                {
	                    _closing = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", value, (args) => {
	                        OnHandlingClosing(args);
	                        
	                    });
	        this.OnRefChanged("Closing", null, "event:::Closing", true, false, (refName, oldValue, newValue) => {
	                        this._closingRef = refName;
	                        this.MarkPropDirty("ClosingRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closing = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closing", null);
	    this.OnRefChanged("Closing", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closingRef = null;
	                    this.MarkPropDirty("ClosingRef");	
	            });
	    }
	    }
	    }
	
	    private string _closedRef = null;
	    private string _closedScript = null;
	    [Parameter]
	    public string ClosedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Closed", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._closedRef = refName;
	                this.MarkPropDirty("ClosedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._closedScript;
	        }
	    }
	
	    partial void OnHandlingClosed(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _closed = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Closed
	    {
	        get 
	        {
	            return this._closed != null ? this._closed.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _closed, ref eventCallbacksCache))
	                {
	                    _closed = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", value, (args) => {
	                        OnHandlingClosed(args);
	                        
	                    });
	        this.OnRefChanged("Closed", null, "event:::Closed", true, false, (refName, oldValue, newValue) => {
	                        this._closedRef = refName;
	                        this.MarkPropDirty("ClosedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _closed = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Closed", null);
	    this.OnRefChanged("Closed", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._closedRef = null;
	                    this.MarkPropDirty("ClosedRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbDialog(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbDialog(ser);
	
	if (IsPropDirty("KeepOpenOnEscape")) { ser.AddBooleanProp("keepOpenOnEscape", this._keepOpenOnEscape); }
	if (IsPropDirty("CloseOnOutsideClick")) { ser.AddBooleanProp("closeOnOutsideClick", this._closeOnOutsideClick); }
	if (IsPropDirty("HideDefaultAction")) { ser.AddBooleanProp("hideDefaultAction", this._hideDefaultAction); }
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	if (IsPropDirty("Title")) { ser.AddStringProp("title", this._title); }
	if (IsPropDirty("ReturnValue")) { ser.AddStringProp("returnValue", this._returnValue); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	
	    }
	
}
}
