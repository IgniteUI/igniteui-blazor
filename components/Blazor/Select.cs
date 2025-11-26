
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            /// <summary>
/// Represents a control that provides a menu of options.
/// </summary>
public partial class IgbSelect: IgbBaseComboBoxLike {
                                public override string Type { get { return "WebSelect"; } }

							
                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbSelectModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbSelectModule.Register(IgBlazor);
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
	                            return "igc-select";
                                }
                        }
	
	    public IgbSelect(): base() {
	        OnCreatedIgbSelect();
	
	        
	    }
	
	    partial void OnCreatedIgbSelect();
	    
	private string? _value;
	
	partial void OnValueChanging(ref string? newValue);
	/// <summary>
	/// The value attribute of the control.
	/// </summary>
	[Parameter]
	public string? Value 
	{
	get { return this._value; }
	set { 
	                if (this._value != value || !IsPropDirty("Value")) {
	                        MarkPropDirty("Value");
	                } 
	                this._value = value;
	                 
	                }
	}
	public async Task<string?> GetCurrentValueAsync()
	                    {
		var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
		return ReturnToString(iv);
	}
	                    public string? GetCurrentValue()
	                    {
		var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
		return ReturnToString(iv);
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
	private double _distance = 0;
	
	partial void OnDistanceChanging(ref double newValue);
	/// <summary>
	/// The distance of the select dropdown from its input.
	/// </summary>
	[Parameter]
	public double Distance 
	{
	get { return this._distance; }
	set { 
	                if (this._distance != value || !IsPropDirty("Distance")) {
	                        MarkPropDirty("Distance");
	                } 
	                this._distance = value;
	                 
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
	private PopoverPlacement _placement = PopoverPlacement.Top;
	
	partial void OnPlacementChanging(ref PopoverPlacement newValue);
	/// <summary>
	/// The preferred placement of the select dropdown around its input.
	/// </summary>
	[Parameter]
	public PopoverPlacement Placement 
	{
	get { return this._placement; }
	set { 
	                if (this._placement != value || !IsPropDirty("Placement")) {
	                        MarkPropDirty("Placement");
	                } 
	                this._placement = value;
	                 
	                }
	}
	private PopoverScrollStrategy _scrollStrategy = PopoverScrollStrategy.Scroll;
	
	partial void OnScrollStrategyChanging(ref PopoverScrollStrategy newValue);
	/// <summary>
	/// Determines the behavior of the component during scrolling of the parent container.
	/// </summary>
	[Parameter]
	public PopoverScrollStrategy ScrollStrategy 
	{
	get { return this._scrollStrategy; }
	set { 
	                if (this._scrollStrategy != value || !IsPropDirty("ScrollStrategy")) {
	                        MarkPropDirty("ScrollStrategy");
	                } 
	                this._scrollStrategy = value;
	                 
	                }
	}
	public async Task<IgbSelectItem[]> GetItemsAsync()
	                    {
		var iv = await InvokeMethod("p:Items", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbSelectItem[]);
	    }
	    var retVal = ReturnToObjectArray<IgbSelectItem>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectItem[]);
	    }
	    return retVal;
	
	}
	                    public IgbSelectItem[] GetItems()
	                    {
		var iv = InvokeMethodSync("p:Items", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbSelectItem[]);
	    }
	    var retVal = ReturnToObjectArray<IgbSelectItem>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectItem[]);
	    }
	    return retVal;
	
	}
	public async Task<IgbSelectGroup[]> GetGroupsAsync()
	                    {
		var iv = await InvokeMethod("p:Groups", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbSelectGroup[]);
	    }
	    var retVal = ReturnToObjectArray<IgbSelectGroup>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectGroup[]);
	    }
	    return retVal;
	
	}
	                    public IgbSelectGroup[] GetGroups()
	                    {
		var iv = InvokeMethodSync("p:Groups", new object[] { }, new string[] { });
		
	    if (iv == null) 
	    {
	        return default(IgbSelectGroup[]);
	    }
	    var retVal = ReturnToObjectArray<IgbSelectGroup>(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectGroup[]);
	    }
	    return retVal;
	
	}
	public async Task<IgbSelectItem?> GetSelectedItemAsync()
	                    {
		var iv = await InvokeMethod("p:SelectedItem", new object[] { }, new string[] { });
		
	    if (iv == null)
	    {
	        return default(IgbSelectItem);
	    }
	    var retVal = (IgbSelectItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectItem);
	    }
	    return retVal;
	
	}
	                    public IgbSelectItem? GetSelectedItem()
	                    {
		var iv = InvokeMethodSync("p:SelectedItem", new object[] { }, new string[] { });
		
	    if (iv == null)
	    {
	        return default(IgbSelectItem);
	    }
	    var retVal = (IgbSelectItem)ConvertReturnValue(iv);
	    if (retVal == null) 
	    {
	        return default(IgbSelectItem);
	    }
	    return retVal;
	
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
	
	    partial void FindByNameSelect(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameSelect(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
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
	/// <summary>
	/// Checks the validity of the control and moves the focus to it if it is not valid.
	/// </summary>
	public async Task<bool> ReportValidityAsync() 
	                    {
		var iv = await InvokeMethod("reportValidity", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	                    public bool ReportValidity() 
	                    {
		var iv = InvokeMethodSync("reportValidity", new object[] {  }, new string[] {  });
		return ReturnToBoolean(iv);
	}
	/// <summary>
	/// Resets the current value and selection of the component.
	/// </summary>
	public async  Task ClearSelectionAsync() 
	                    {
		await InvokeMethod("clearSelection", new object[] {  }, new string[] {  });
	}
	                    public  void ClearSelection() 
	                    {
		InvokeMethodSync("clearSelection", new object[] {  }, new string[] {  });
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
	
	    private EventCallback<string?>? _valueChanged = null;
	    [Parameter]
	    public EventCallback<string?> ValueChanged
	    {
	        get 
	        {
	            return this._valueChanged != null ? this._valueChanged.Value : EventCallback<string?>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<string?>.Empty)) 
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
	
	    partial void OnHandlingChange(IgbSelectItemComponentEventArgs args);
	    private EventCallback<IgbSelectItemComponentEventArgs>? _change = null;
	    [Parameter]
	    public EventCallback<IgbSelectItemComponentEventArgs> Change
	    {
	        get 
	        {
	            return this._change != null ? this._change.Value : EventCallback<IgbSelectItemComponentEventArgs>.Empty;
	        }
	        set
	        { 
	            if (!value.Equals(EventCallback<IgbSelectItemComponentEventArgs>.Empty)) 
	            {
	                if (!CompareEventCallbacks(value, _change, ref eventCallbacksCache))
	                {
	                    _change = value;
	                    this.SetHandler<IgbSelectItemComponentEventArgs>(this.Name, "Change", value, (args) => {
	                        OnHandlingChange(args);
	                        
	var newValueValue = default(string?);
	
	    
	    {
	        newValueValue = (string?)(args.Detail.Value);
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
	
	    if (!EventCallback<string?>.Empty.Equals(ValueChanged))
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
	                this.SetHandler<IgbSelectItemComponentEventArgs>(this.Name, "Change", null);
	    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) => {
	                    this._changeRef = null;
	                    this.MarkPropDirty("ChangeRef");	
	            });
	    }
	    }
	    }
	    internal void EnsureChangeHandled()
	    {
	        if (EventCallback<IgbSelectItemComponentEventArgs>.Empty.Equals(this.Change))
	        {
	            this.Change = new EventCallback<IgbSelectItemComponentEventArgs>(null, (Action<IgbSelectItemComponentEventArgs>)((e) => { })); this._change = null;        
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
	
	                            partial void OnEventUpdatingValue(string? oldValue, ref string? newValue);
	
	    partial void SerializeCoreIgbSelect(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbSelect(ser);
	
	if (IsPropDirty("Value")) { ser.AddStringProp("value", this._value); }
	if (IsPropDirty("Outlined")) { ser.AddBooleanProp("outlined", this._outlined); }
	if (IsPropDirty("Autofocus")) { ser.AddBooleanProp("autofocus", this._autofocus); }
	if (IsPropDirty("Distance")) { ser.AddNumberProp("distance", this._distance); }
	if (IsPropDirty("Label")) { ser.AddStringProp("label", this._label); }
	if (IsPropDirty("Placeholder")) { ser.AddStringProp("placeholder", this._placeholder); }
	if (IsPropDirty("Placement")) { ser.AddEnumProp("placement", this._placement); }
	if (IsPropDirty("ScrollStrategy")) { ser.AddEnumProp("scrollStrategy", this._scrollStrategy); }
	if (IsPropDirty("Disabled")) { ser.AddBooleanProp("disabled", this._disabled); }
	if (IsPropDirty("Required")) { ser.AddBooleanProp("required", this._required); }
	if (IsPropDirty("Invalid")) { ser.AddBooleanProp("invalid", this._invalid); }
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
