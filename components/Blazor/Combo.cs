
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// The Combo component is similar to the Select component in that it provides a list of options from which the user can make a selection.
/// In contrast to the Select component, the Combo component displays all options in a virtualized list of items,
/// meaning the combo box can simultaneously show thousands of options, where one or more options can be selected.
/// Additionally, users can create custom item templates, allowing for robust data visualization.
/// The Combo component features case-sensitive filtering, grouping, complex data binding, dynamic addition of values and more.
/// </summary>
public partial class IgbCombo<T>: BaseRendererControl {
                                public override string Type { get { return "WebCombo"; } }

                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbComboModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbComboModule.Register(IgBlazor);
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

                            protected override ControlEventBehavior DefaultEventBehavior
                            {
                                get { return ControlEventBehavior.Queued; }
                            }
	
	    public IgbCombo(): base() {
	        OnCreatedIgbCombo();
	
	        
	    }
	
	    partial void OnCreatedIgbCombo();
	    
	private string _dataRef;
	private Object _data;
	
	partial void OnDataChanging(ref Object newValue);
	/// <summary>
	/// The data source used to generate the list of options.
	/// </summary>
	[Parameter]
	public Object Data 
	{
	get { return this._data; }
	
	    set { 
	        var oldValue = this._data; 
	        OnDataChanging(ref value);
	        
	                    
	        if (oldValue != value || !IsPropDirty("Data"))
	        {
	            MarkPropDirty("Data"); 
	            this._data = value; 
	            this.OnRefChanged("Data", oldValue, value, false, false, (string refName, object old, object newValue) => {
	        	    this._dataRef = refName;
	                this.MarkPropDirty("DataRef");
	        }); 
	        }
	    }
	}
	
	
	private string _dataScript;
	
	
	///<summary>Provides a means of setting Data in the JavaScript environment.</summary>
	[Parameter]
	public string DataScript 
	{
	get { return _dataScript; }
	
	
	    set { 
	        var oldValue = this._dataScript; 
	        if (oldValue != value || !IsPropDirty("Data"))
	        {
	            MarkPropDirty("Data"); 
	            this.OnRefChanged("Data", oldValue, value, true, false, (string refName, object old, object newValue) => {
	        	    this._dataRef = refName;
	                this.MarkPropDirty("DataRef");
	        }); 
	        }
	    }
	}
	private bool _outlined = false;
	
	partial void OnOutlinedChanging(ref bool newValue);
	/// <summary>
	/// The outlined attribute of the control.
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
	private bool _singleSelect = false;
	
	partial void OnSingleSelectChanging(ref bool newValue);
	/// <summary>
	/// Enables single selection mode and moves item filtering to the main input.
	/// @default false
	/// </summary>
	[Parameter]
	public bool SingleSelect 
	{
	get { return this._singleSelect; }
	set { 
	                if (this._singleSelect != value || !IsPropDirty("SingleSelect")) {
	                        MarkPropDirty("SingleSelect");
	                } 
	                this._singleSelect = value;
	                 
	                }
	}
	private bool _autofocus = false;
	
	partial void OnAutofocusChanging(ref bool newValue);
	/// <summary>
	/// The autofocus attribute of the control.
	/// </summary>
	[Parameter]
	public bool Autofocus 
	{
	get { return this._autofocus; }
	set { 
	                if (this._autofocus != value || !IsPropDirty("Autofocus")) {
	                        MarkPropDirty("Autofocus");
	                } 
	                this._autofocus = value;
	                 
	                }
	}
	private bool _autofocusList = false;
	
	partial void OnAutofocusListChanging(ref bool newValue);
	/// <summary>
	/// Focuses the list of options when the menu opens.
	/// </summary>
	[Parameter]
	public bool AutofocusList 
	{
	get { return this._autofocusList; }
	set { 
	                if (this._autofocusList != value || !IsPropDirty("AutofocusList")) {
	                        MarkPropDirty("AutofocusList");
	                } 
	                this._autofocusList = value;
	                 
	                }
	}
	private string _label;
	
	partial void OnLabelChanging(ref string newValue);
	/// <summary>
	/// The label attribute of the control.
	/// </summary>
	[Parameter]
	public string Label 
	{
	get { return this._label; }
	set { 
	                if (this._label != value || !IsPropDirty("Label")) {
	                        MarkPropDirty("Label");
	                } 
	                this._label = value;
	                 
	                }
	}
	private string _placeholder;
	
	partial void OnPlaceholderChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the control.
	/// </summary>
	[Parameter]
	public string Placeholder 
	{
	get { return this._placeholder; }
	set { 
	                if (this._placeholder != value || !IsPropDirty("Placeholder")) {
	                        MarkPropDirty("Placeholder");
	                } 
	                this._placeholder = value;
	                 
	                }
	}
	private string _placeholderSearch;
	
	partial void OnPlaceholderSearchChanging(ref string newValue);
	/// <summary>
	/// The placeholder attribute of the search input.
	/// </summary>
	[Parameter]
	public string PlaceholderSearch 
	{
	get { return this._placeholderSearch; }
	set { 
	                if (this._placeholderSearch != value || !IsPropDirty("PlaceholderSearch")) {
	                        MarkPropDirty("PlaceholderSearch");
	                } 
	                this._placeholderSearch = value;
	                 
	                }
	}
	private bool _open = false;
	
	partial void OnOpenChanging(ref bool newValue);
	/// <summary>
	/// Sets the open state of the component.
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
	private string? _valueKey;
	
	partial void OnValueKeyChanging(ref string? newValue);
	/// <summary>
	/// The key in the data source used when selecting items.
	/// </summary>
	[Parameter]
	public string? ValueKey 
	{
	get { return this._valueKey; }
	set { 
	                if (this._valueKey != value || !IsPropDirty("ValueKey")) {
	                        MarkPropDirty("ValueKey");
	                } 
	                this._valueKey = value;
	                 
	                }
	}
	private string? _displayKey;
	
	partial void OnDisplayKeyChanging(ref string? newValue);
	/// <summary>
	/// The key in the data source used to display items in the list.
	/// </summary>
	[Parameter]
	public string? DisplayKey 
	{
	get { return this._displayKey; }
	set { 
	                if (this._displayKey != value || !IsPropDirty("DisplayKey")) {
	                        MarkPropDirty("DisplayKey");
	                } 
	                this._displayKey = value;
	                 
	                }
	}
	private string? _groupKey;
	
	partial void OnGroupKeyChanging(ref string? newValue);
	/// <summary>
	/// The key in the data source used to group items in the list.
	/// </summary>
	[Parameter]
	public string? GroupKey 
	{
	get { return this._groupKey; }
	set { 
	                if (this._groupKey != value || !IsPropDirty("GroupKey")) {
	                        MarkPropDirty("GroupKey");
	                } 
	                this._groupKey = value;
	                 
	                }
	}
	private GroupingDirection _groupSorting = GroupingDirection.Asc;
	
	partial void OnGroupSortingChanging(ref GroupingDirection newValue);
	/// <summary>
	/// Sorts the items in each group by ascending or descending order.
	/// @default asc
	/// @type {"asc" | "desc" | "none"}
	/// </summary>
	[Parameter]
	public GroupingDirection GroupSorting 
	{
	get { return this._groupSorting; }
	set { 
	                if (this._groupSorting != value || !IsPropDirty("GroupSorting")) {
	                        MarkPropDirty("GroupSorting");
	                } 
	                this._groupSorting = value;
	                 
	                }
	}
	private IgbFilteringOptions _filteringOptions;
	
	partial void OnFilteringOptionsChanging(ref IgbFilteringOptions newValue);
	[Parameter]
	public IgbFilteringOptions FilteringOptions 
	{
	get { return this._filteringOptions; }
	set { 
	                        OnFilteringOptionsChanging(ref value);
	                        MarkPropDirty("FilteringOptions"); 
	                        if (this._filteringOptions != null) {
	                            this.DetachChild(this._filteringOptions);
	                        }
	                        if (value != null) {
	                            this.AttachChild(value);
	                        }
	                        this._filteringOptions = value; 
	                    }
	                    
	}
	private bool _caseSensitiveIcon = false;
	
	partial void OnCaseSensitiveIconChanging(ref bool newValue);
	/// <summary>
	/// Enables the case sensitive search icon in the filtering input.
	/// </summary>
	[Parameter]
	public bool CaseSensitiveIcon 
	{
	get { return this._caseSensitiveIcon; }
	set { 
	                if (this._caseSensitiveIcon != value || !IsPropDirty("CaseSensitiveIcon")) {
	                        MarkPropDirty("CaseSensitiveIcon");
	                } 
	                this._caseSensitiveIcon = value;
	                 
	                }
	}
	private bool _disableFiltering = false;
	
	partial void OnDisableFilteringChanging(ref bool newValue);
	/// <summary>
	/// Disables the filtering of the list of options.
	/// @default false
	/// </summary>
	[Parameter]
	public bool DisableFiltering 
	{
	get { return this._disableFiltering; }
	set { 
	                if (this._disableFiltering != value || !IsPropDirty("DisableFiltering")) {
	                        MarkPropDirty("DisableFiltering");
	                } 
	                this._disableFiltering = value;
	                 
	                }
	}
	private T[] _value;
	
	partial void OnValueChanging(ref T[] newValue);
	[Parameter]
	public T[] Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<T[]> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToObjectArray(iv).Cast<T>().ToArray();
	}
	                    public T[] GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToObjectArray(iv).Cast<T>().ToArray();
	}
	private string _selectionRef;
	public async Task<object[]> GetSelectionAsync()
	                    {
		var iv = await InvokeMethod("p:Selection", new object[] { }, new string[] { });
		return ReturnToObjectArray(iv);
	}
	                    public object[] GetSelection()
	                    {
		var iv = InvokeMethodSync("p:Selection", new object[] { }, new string[] { });
		return ReturnToObjectArray(iv);
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
	private bool _required = false;
	
	partial void OnRequiredChanging(ref bool newValue);
	/// <summary>
	/// Makes the control a required field in a form context.
	/// </summary>
	[Parameter]
	public bool Required 
	{
	get { return this._required; }
	set { 
	                if (this._required != value || !IsPropDirty("Required")) {
	                        MarkPropDirty("Required");
	                } 
	                this._required = value;
	                 
	                }
	}
	private bool _invalid = false;
	
	partial void OnInvalidChanging(ref bool newValue);
	/// <summary>
	/// Control the validity of the control.
	/// </summary>
	[Parameter]
	public bool Invalid 
	{
	get { return this._invalid; }
	set { 
	                if (this._invalid != value || !IsPropDirty("Invalid")) {
	                        MarkPropDirty("Invalid");
	                } 
	                this._invalid = value;
	                 
	                }
	}
	private string _itemTemplateRef;
	private RenderFragment<object> _itemTemplate;
	
	partial void OnItemTemplateChanging(ref RenderFragment<object> newValue);
	[Parameter]
	public RenderFragment<object> ItemTemplate 
	{
	get { return this._itemTemplate; }
	
	    set { 
	        var oldValue = this._itemTemplate; 
	        OnItemTemplateChanging(ref value);
	        if (oldValue != value || !IsPropDirty("ItemTemplate"))
	        {
	            MarkPropDirty("ItemTemplate"); 
	            this._itemTemplate = value; 
	            this._itemTemplateTemplateId = Guid.NewGuid().ToString(); 
	            this.UpdateTemplate(this._itemTemplateTemplateId, this._itemTemplate, typeof(object));
	            this.OnRefChanged("ItemTemplate", null, "template:::" + this._itemTemplateTemplateId, true, false, (string refName, object old, object newValue) => {
	        	    this._itemTemplateRef = refName;
	                this.MarkPropDirty("ItemTemplateRef");
	        }); 
	        }
	    }
	}
	
	
	private string _itemTemplateTemplateId;
	private string _itemTemplateScript;
	
	
	///<summary>Provides a means of setting ItemTemplate in the JavaScript environment.</summary>
	[Parameter]
	public string ItemTemplateScript 
	{
	get { return _itemTemplateScript; }
	
	
	    set { 
	        var oldValue = this._itemTemplateScript; 
	        if (oldValue != value || !IsPropDirty("ItemTemplate"))
	        {
	            MarkPropDirty("ItemTemplate"); 
	            this.OnRefChanged("ItemTemplate", oldValue, value, true, false, (string refName, object old, object newValue) => {
	        	    this._itemTemplateRef = refName;
	                this.MarkPropDirty("ItemTemplateRef");
	        }); 
	        }
	    }
	}
	private string _groupHeaderTemplateRef;
	private RenderFragment<object> _groupHeaderTemplate;
	
	partial void OnGroupHeaderTemplateChanging(ref RenderFragment<object> newValue);
	[Parameter]
	public RenderFragment<object> GroupHeaderTemplate 
	{
	get { return this._groupHeaderTemplate; }
	
	    set { 
	        var oldValue = this._groupHeaderTemplate; 
	        OnGroupHeaderTemplateChanging(ref value);
	        if (oldValue != value || !IsPropDirty("GroupHeaderTemplate"))
	        {
	            MarkPropDirty("GroupHeaderTemplate"); 
	            this._groupHeaderTemplate = value; 
	            this._groupHeaderTemplateTemplateId = Guid.NewGuid().ToString(); 
	            this.UpdateTemplate(this._groupHeaderTemplateTemplateId, this._groupHeaderTemplate, typeof(object));
	            this.OnRefChanged("GroupHeaderTemplate", null, "template:::" + this._groupHeaderTemplateTemplateId, true, false, (string refName, object old, object newValue) => {
	        	    this._groupHeaderTemplateRef = refName;
	                this.MarkPropDirty("GroupHeaderTemplateRef");
	        }); 
	        }
	    }
	}
	
	
	private string _groupHeaderTemplateTemplateId;
	private string _groupHeaderTemplateScript;
	
	
	///<summary>Provides a means of setting GroupHeaderTemplate in the JavaScript environment.</summary>
	[Parameter]
	public string GroupHeaderTemplateScript 
	{
	get { return _groupHeaderTemplateScript; }
	
	
	    set { 
	        var oldValue = this._groupHeaderTemplateScript; 
	        if (oldValue != value || !IsPropDirty("GroupHeaderTemplate"))
	        {
	            MarkPropDirty("GroupHeaderTemplate"); 
	            this.OnRefChanged("GroupHeaderTemplate", oldValue, value, true, false, (string refName, object old, object newValue) => {
	        	    this._groupHeaderTemplateRef = refName;
	                this.MarkPropDirty("GroupHeaderTemplateRef");
	        }); 
	        }
	    }
	}
	
	    partial void FindByNameCombo(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameCombo(name, ref item);
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
	/// Sets focus on the component.
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
	/// Removes focus from the component.
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
	public async  Task SelectAsync(object[] items) 
	                    {
		await InvokeMethod("select", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
	}
	                    public  void Select(object[] items) 
	                    {
		InvokeMethodSync("select", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
	}
	public async  Task DeselectAsync(object[] items) 
	                    {
		await InvokeMethod("deselect", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
	}
	                    public  void Deselect(object[] items) 
	                    {
		InvokeMethodSync("deselect", new object[] { ObjectArrayToParam(items) }, new string[] { "" });
	}
	/// <summary>
	/// Shows the list of options.
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
	/// Hides the list of options.
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
	/// Toggles the list of options.
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
	/// <summary>
	/// Checks for validity of the control and shows the browser message if it invalid.
	/// </summary>
	public async  Task ReportValidityAsync() 
	                    {
		await InvokeMethod("reportValidity", new object[] {  }, new string[] {  });
	}
	                    public  void ReportValidity() 
	                    {
		InvokeMethodSync("reportValidity", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Checks for validity of the control and emits the invalid event if it invalid.
	/// </summary>
	public async  Task CheckValidityAsync() 
	                    {
		await InvokeMethod("checkValidity", new object[] {  }, new string[] {  });
	}
	                    public  void CheckValidity() 
	                    {
		InvokeMethodSync("checkValidity", new object[] {  }, new string[] {  });
	}
	/// <summary>
	/// Sets a custom validation message for the control.
	/// As long as `message` is not empty, the control is considered invalid.
	/// </summary>
	public async  Task SetCustomValidityAsync(String message) 
	                    {
		await InvokeMethod("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
	}
	                    public  void SetCustomValidity(String message) 
	                    {
		InvokeMethodSync("setCustomValidity", new object[] { StringToString(message) }, new string[] { "String" });
	}
	
	    private EventCallback<T[]>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<T[]> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<T[]>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<T[]>.Empty)) 
	            {
	                 if (!CompareEventCallbacks(value, _valueChanged, ref eventCallbacksCache))
	                {
	                    this.EnsureChangeHandled();
	
	                    _valueChanged = value;
	                }
	    }
	        else 
	            {
	                _valueChanged = null;
	    }
	    }
	    }
	
	    private string _changeRef = null;
	    private string _changeScript = null;
	    [Parameter]
	    public string ChangeScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._changeRef = refName;
	                this.MarkPropDirty("ChangeRef");	
	        }); 
	        }
	        get 
	        {
	            return this._changeScript;
	        }
	    }
	
	    partial void OnHandlingChange(IgbComboChangeEventArgs args);
	    private EventCallback<IgbComboChangeEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbComboChangeEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbComboChangeEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbComboChangeEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbComboChangeEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(T[]);
	
	    
	    {
	        newValueValue = (T[])(DowncastArray<T>(args.Detail.NewValue));
	        ;
	        OnEventUpdatingValue(this._value, ref newValueValue);
	        if (UseDirectRender) {
	            //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
	            this.Value = newValueValue;
	        } else {
	            this._value = newValueValue;
	        }
	        OnPropertyPropagatedOut(Name, "Value");
	    }
	
	    if (!EventCallback<T[]>.Empty.Equals(ValueChanged))
	    {
	        var task = ValueChanged.InvokeAsync(newValueValue);
	        if (task.Exception != null)
	        {
	            throw task.Exception;
	        }
	    }
	
	                    });
	        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) => {
	                        this._changeRef = refName;
	                        this.MarkPropDirty("ChangeRef");	
	                });
	                }
	    }
	        else 
	            {
	                _change = null;
	                this.SetHandler<IgbComboChangeEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbComboChangeEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbComboChangeEventArgs>(null, (Action<IgbComboChangeEventArgs>)((e) => { })); this._change = null;        
	        }
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
	
	    private string _openingRef = null;
	    private string _openingScript = null;
	    [Parameter]
	    public string OpeningScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opening", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openingRef = refName;
	                this.MarkPropDirty("OpeningRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openingScript;
	        }
	    }
	
	    partial void OnHandlingOpening(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opening = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Opening
	    {
	        get 
	        {
	            return this._opening != null ? this._opening.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opening, ref eventCallbacksCache))
	                {
	                    _opening = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", value, (args) => {
	                        OnHandlingOpening(args);
	                        
	                    });
	        this.OnRefChanged("Opening", null, "event:::Opening", true, false, (refName, oldValue, newValue) => {
	                        this._openingRef = refName;
	                        this.MarkPropDirty("OpeningRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opening = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opening", null);
	    this.OnRefChanged("Opening", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openingRef = null;
	                    this.MarkPropDirty("OpeningRef");	
	            });
	    }
	    }
	    }
	
	    private string _openedRef = null;
	    private string _openedScript = null;
	    [Parameter]
	    public string OpenedScript { 
	    
	        set 
	        {
	            this.OnRefChanged("Opened", null, value, true, false, (string refName, object oldValue, object newValue) => {
	                this._openedRef = refName;
	                this.MarkPropDirty("OpenedRef");	
	        }); 
	        }
	        get 
	        {
	            return this._openedScript;
	        }
	    }
	
	    partial void OnHandlingOpened(IgbVoidEventArgs args);
	    private EventCallback<IgbVoidEventArgs>? _opened = null;
	    [Parameter]
	    public EventCallback<IgbVoidEventArgs> Opened
	    {
	        get 
	        {
	            return this._opened != null ? this._opened.Value : EventCallback<IgbVoidEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbVoidEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _opened, ref eventCallbacksCache))
	                {
	                    _opened = value;
	                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", value, (args) => {
	                        OnHandlingOpened(args);
	                        
	                    });
	        this.OnRefChanged("Opened", null, "event:::Opened", true, false, (refName, oldValue, newValue) => {
	                        this._openedRef = refName;
	                        this.MarkPropDirty("OpenedRef");	
	                });
	                }
	    }
	        else 
	            {
	                _opened = null;
	                this.SetHandler<IgbVoidEventArgs>(this.Name, "Opened", null);
	    this.OnRefChanged("Opened", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._openedRef = null;
	                    this.MarkPropDirty("OpenedRef");	
	            });
	    }
	    }
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
	
	                            partial void OnEventUpdatingValue(T[] oldValue, ref T[] newValue);
	
	    partial void SerializeCoreIgbCombo(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbCombo(ser);
	
	if (IsPropDirty("DataRef")) { ser.AddStringProp("dataRef", this._dataRef); }
	if (IsPropDirty("Outlined")) { ser.AddBooleanProp("outlined", this._outlined); }
	if (IsPropDirty("SingleSelect")) { ser.AddBooleanProp("singleSelect", this._singleSelect); }
	if (IsPropDirty("Autofocus")) { ser.AddBooleanProp("autofocus", this._autofocus); }
	if (IsPropDirty("AutofocusList")) { ser.AddBooleanProp("autofocusList", this._autofocusList); }
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("Placeholder")) { ser.AddStringProp("placeholder", this._placeholder); }
	if (IsPropDirty("PlaceholderSearch")) { ser.AddStringProp("placeholderSearch", this._placeholderSearch); }
	if (IsPropDirty("Open")) { ser.AddBooleanProp("open", this._open); }
	if (IsPropDirty("ValueKey")) { ser.AddStringProp("valueKey", this._valueKey); }
	if (IsPropDirty("DisplayKey")) { ser.AddStringProp("displayKey", this._displayKey); }
	if (IsPropDirty("GroupKey")) { ser.AddStringProp("groupKey", this._groupKey); }
	if (IsPropDirty("GroupSorting")) { ser.AddEnumProp("groupSorting", this._groupSorting); }
	if (IsPropDirty("FilteringOptions")) { ser.AddSerializableProp("filteringOptions", this._filteringOptions); }
	if (IsPropDirty("CaseSensitiveIcon")) { ser.AddBooleanProp("caseSensitiveIcon", this._caseSensitiveIcon); }
	if (IsPropDirty("DisableFiltering")) { ser.AddBooleanProp("disableFiltering", this._disableFiltering); }
	if (IsPropDirty("Value")) { ser.AddArrayProp("value", this._value); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Required")) { ser.AddBooleanProp("required", this._required); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
	if (IsPropDirty("ItemTemplateRef")) { ser.AddStringProp("itemTemplateRef", this._itemTemplateRef); }
	if (IsPropDirty("GroupHeaderTemplateRef")) { ser.AddStringProp("groupHeaderTemplateRef", this._groupHeaderTemplateRef); }
	if (IsPropDirty("ChangeRef")) { ser.AddStringProp("changeRef", this._changeRef); }
	if (IsPropDirty("FocusRef")) { ser.AddStringProp("focusRef", this._focusRef); }
	if (IsPropDirty("BlurRef")) { ser.AddStringProp("blurRef", this._blurRef); }
	if (IsPropDirty("OpeningRef")) { ser.AddStringProp("openingRef", this._openingRef); }
	if (IsPropDirty("OpenedRef")) { ser.AddStringProp("openedRef", this._openedRef); }
	if (IsPropDirty("ClosingRef")) { ser.AddStringProp("closingRef", this._closingRef); }
	if (IsPropDirty("ClosedRef")) { ser.AddStringProp("closedRef", this._closedRef); }
	
	    }
	
}
}
