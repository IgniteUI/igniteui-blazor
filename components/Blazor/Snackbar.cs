
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// A snackbar component is used to provide feedback about an operation
/// by showing a brief message at the bottom of the screen.
/// </summary>
public partial class IgbSnackbar: IgbBaseAlertLike {
                                public override string Type { get { return "WebSnackbar"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSnackbarModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSnackbarModule.Register(IgBlazor);
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
	                            return "igc-snackbar";
                                }
                        }
	
	    public IgbSnackbar(): base() {
	        OnCreatedIgbSnackbar();
	
	        
	    }
	
	    partial void OnCreatedIgbSnackbar();
	    
	private string _actionText;
	
	partial void OnActionTextChanging(ref string newValue);
	/// <summary>
	/// The snackbar action button.
	/// </summary>
	[Parameter]
	public string ActionText 
	{
	get { return this._actionText; }
	set { 
	                if (this._actionText != value || !IsPropDirty("ActionText")) {
	                        MarkPropDirty("ActionText");
	                } 
	                this._actionText = value;
	                 
	                }
	}
	
	    partial void FindByNameSnackbar(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSnackbar(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    private string _actionRef = null;
	    private string _actionScript = null;
	    [Parameter]
	    public string ActionScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Action", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._actionRef = refName;
	                this.MarkPropDirty("ActionRef");	
	        }); 
	        }
	        get 
	        {
	            return this._actionScript;
	        }
	    }
	
	    partial void OnHandlingAction(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _action = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Action
	    {
	        get 
	        {
	            return this._action != null ? this._action.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _action, ref eventCallbacksCache))
	                {
	                    _action = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Action", value, (args) => {
	                        OnHandlingAction(args);
	                        
	                    });
	        this.OnRefChanged("Action", null, "event:::Action", true, false, (refName, oldValue, newValue) => {
	                        this._actionRef = refName;
	                        this.MarkPropDirty("ActionRef");	
	                });
	                }
	    }
	        else 
	            {
	                _action = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Action", null);
	    this.OnRefChanged("Action", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._actionRef = null;
	                    this.MarkPropDirty("ActionRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbSnackbar(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSnackbar(ser);
	
	if (IsPropDirty("ActionText")) { ser.AddStringProp("actionText", this._actionText); }
	if (IsPropDirty("ActionRef")) { ser.AddStringProp("actionRef", this._actionRef); }
	
	    }
	
}
}
