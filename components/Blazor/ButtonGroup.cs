
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The `igc-button-group` groups a series of `igc-toggle-button`s together, exposing features such as layout and selection.
/// </summary>
public partial class IgbButtonGroup: BaseRendererControl {
                                public override string Type { get { return "WebButtonGroup"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbButtonGroupModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbButtonGroupModule.Register(IgBlazor);
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
                            return "igc-button-group";
                                }
                        }

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Immediate; }
                            }
	
	    public IgbButtonGroup(): base() {
	        OnCreatedIgbButtonGroup();
	
	        
	    }
	
	    partial void OnCreatedIgbButtonGroup();
	    
	private bool _disabled = false;
	
	partial void OnDisabledChanging(ref bool newValue);
	/// <summary>
	/// Disables all buttons inside the group.
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
	private ContentOrientation _alignment = ContentOrientation.Horizontal;
	
	partial void OnAlignmentChanging(ref ContentOrientation newValue);
	/// <summary>
	/// Sets the orientation of the buttons in the group.
	/// </summary>
	[Parameter]
	public ContentOrientation Alignment 
	{
	get { return this._alignment; }
	set { 
	                if (this._alignment != value || !IsPropDirty("Alignment")) {
	                        MarkPropDirty("Alignment");
	                } 
	                this._alignment = value;
	                 
	                }
	}
	private ButtonGroupSelection _selection = ButtonGroupSelection.Single;
	
	partial void OnSelectionChanging(ref ButtonGroupSelection newValue);
	/// <summary>
	/// Controls the mode of selection for the button group.
	/// </summary>
	[Parameter]
	public ButtonGroupSelection Selection 
	{
	get { return this._selection; }
	set { 
	                if (this._selection != value || !IsPropDirty("Selection")) {
	                        MarkPropDirty("Selection");
	                } 
	                this._selection = value;
	                 
	                }
	}
	private string[] _selectedItems;
	
	partial void OnSelectedItemsChanging(ref string[] newValue);
	[Parameter]
	public string[] SelectedItems 
	{
	get { return this._selectedItems; }
	set { 
	                if (this._selectedItems != value || !IsPropDirty("SelectedItems")) {
	                        MarkPropDirty("SelectedItems");
	                } 
	                this._selectedItems = value;
	                 
	                }
	}
	
	    partial void FindByNameButtonGroup(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameButtonGroup(name, ref item);
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
	
	    private string _selectRef = null;
	    private string _selectScript = null;
	    [Parameter]
	    public string SelectScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Select", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._selectRef = refName;
	                this.MarkPropDirty("SelectRef");	
	        }); 
	        }
	        get 
	        {
	            return this._selectScript;
	        }
	    }
	
	    partial void OnHandlingSelect(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _select = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> Select
	    {
	        get 
	        {
	            return this._select != null ? this._select.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _select, ref eventCallbacksCache))
	                {
	                    _select = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Select", value, (args) => {
	                        OnHandlingSelect(args);
	                        
	                    });
	        this.OnRefChanged("Select", null, "event:::Select", true, false, (refName, oldValue, newValue) => {
	                        this._selectRef = refName;
	                        this.MarkPropDirty("SelectRef");	
	                });
	                }
	    }
	        else 
	            {
	                _select = null;
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Select", null);
	    this.OnRefChanged("Select", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._selectRef = null;
	                    this.MarkPropDirty("SelectRef");	
	            });
	    }
	    }
	    }
	
	    private string _deselectRef = null;
	    private string _deselectScript = null;
	    [Parameter]
	    public string DeselectScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Deselect", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._deselectRef = refName;
	                this.MarkPropDirty("DeselectRef");	
	        }); 
	        }
	        get 
	        {
	            return this._deselectScript;
	        }
	    }
	
	    partial void OnHandlingDeselect(IgbComponentValueChangedEventArgs args);
	    private EventCallback<IgbComponentValueChangedEventArgs>? _deselect = null;
	    [Parameter]
	    public EventCallback<IgbComponentValueChangedEventArgs> Deselect
	    {
	        get 
	        {
	            return this._deselect != null ? this._deselect.Value : EventCallback<IgbComponentValueChangedEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComponentValueChangedEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _deselect, ref eventCallbacksCache))
	                {
	                    _deselect = value;
	                    this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Deselect", value, (args) => {
	                        OnHandlingDeselect(args);
	                        
	                    });
	        this.OnRefChanged("Deselect", null, "event:::Deselect", true, false, (refName, oldValue, newValue) => {
	                        this._deselectRef = refName;
	                        this.MarkPropDirty("DeselectRef");	
	                });
	                }
	    }
	        else 
	            {
	                _deselect = null;
	                this.SetHandler<IgbComponentValueChangedEventArgs>(this.Name, "Deselect", null);
	    this.OnRefChanged("Deselect", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._deselectRef = null;
	                    this.MarkPropDirty("DeselectRef");	
	            });
	    }
	    }
	    }
	
	    partial void SerializeCoreIgbButtonGroup(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbButtonGroup(ser);
	
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Alignment")) { ser.AddEnumProp("alignment", this._alignment); }
	if (IsPropDirty("Selection")) { ser.AddEnumProp("selection", this._selection); }
	if (IsPropDirty("SelectedItems")) { ser.AddArrayProp("selectedItems", this._selectedItems); }
	if (IsPropDirty("SelectRef")) { ser.AddStringProp("selectRef", this._selectRef); }
	if (IsPropDirty("DeselectRef")) { ser.AddStringProp("deselectRef", this._deselectRef); }
	
	    }
	
}
}
