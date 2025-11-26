

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Rendering;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;
using System.Reflection;

namespace IgniteUI.Blazor.Controls
{

/// <summary>
/// Determines the behavior of events as they are fired at the JavaScript level and bubbled up to the Blazor level.
/// </summary>
public enum ControlEventBehavior
{
    /// <summary>
    /// The behavior is automatically determined by the component.
    /// </summary>
    Auto,
    /// <summary>
    /// The behavior is to immediately fire event handlers.
    /// </summary>
    Immediate,
    /// <summary>
    /// The behavior is to queue the event handlers to the next available cycle.
    /// </summary>
    Queued
}

public partial class BaseRendererControl: ComponentBase, RefSink, JsonSerializable, IDisposable 
{
    private IIgniteUIBlazor _igBlazor;
    [Inject]
	protected IIgniteUIBlazor IgBlazor
	{
		get
        {
            return _igBlazor;
        }
		set
        {
            _igBlazor = value;
                // if (_igBlazor is IJSInProcessRuntime)
                // {
                //     this.JsInProcessRuntime = (IJSInProcessRuntime)_igBlazor;
                // }            

            _igBlazor.WebCallback.Register(this);
            _dataSourceManager = new DataSourceManager(this, new RuntimeHelper(JsRuntime, _igBlazor));
            EnsureModulesLoaded();
        }
	}

    protected virtual void EnsureModulesLoaded()
    {

    }

    private IJSRuntime JsRuntime
    {
        get
        {
            return IgBlazor != null ? IgBlazor.JsRuntime : null;
        }
    }

    private IJSInProcessRuntime _inproc = null;
    private bool _checkedInproc = false;
    private IJSInProcessRuntime JsInProcessRuntime
    {
        get 
        {
            if (!_checkedInproc)
            {
                _checkedInproc = true;
                _inproc = JsRuntime as IJSInProcessRuntime;
                
            }
            return _inproc;
        }
    }

    [Parameter]
    public string Height
    {
        get; set;
    }

    [Parameter]
    public string Width
    {
        get; set;
    }

    [Parameter]
    public string Class
    {
        get; set;
    }

    [Parameter(CaptureUnmatchedValues = true)]    
    public Dictionary<string, object> AdditionalAttributes { get; set; }

    protected virtual string ParentTypeName
    {
        get
        {
            return "BaseRenderControlParent";
        }
    }

    /// <summary>
    /// Gets or sets how events are bubbled up from JavaScript to Blazor.
    /// </summary>
    [Parameter]
    public ControlEventBehavior EventBehavior { get; set; } = ControlEventBehavior.Auto;

    /// <summary>
    /// Gets the components default event behavior.
    /// </summary>
    protected virtual ControlEventBehavior DefaultEventBehavior
    {
        get { return ControlEventBehavior.Queued; }    
    }

    /// <summary>
    /// Resolves the components event behavior if Auto is selected.
    /// </summary>
    protected ControlEventBehavior ResolveEventBehavior()
    {
        if (EventBehavior == ControlEventBehavior.Auto)
        {
            return DefaultEventBehavior;
        }
        return EventBehavior;
    }

    [Parameter] public RenderFragment ChildContent { get; set; }

    private ElementReference contEle;
    private Dictionary<string, bool> _isDirty = new Dictionary<string, bool>();
    private Dictionary<string, bool> _isDirtyRef = new Dictionary<string, bool>();
    private bool _hasDirty = false;
    private bool _serializeDirty = true;
    private DataSourceManager _dataSourceManager;
    internal DataSourceManager DataSourceManager
    {
        get {return _dataSourceManager; }
    }
    private LinkedList<RendererMessage> _messageQueue = new LinkedList<RendererMessage>();

    private string _containerId = Guid.NewGuid().ToString();

    internal string ContainerId
    {
        get
        {
            return _containerId;
        }
    }

    private bool _ready = false;
    private Dictionary<string, Action<object, object>> _handlers = new Dictionary<string, Action<object, object>>();
    private bool _updateQueued = false;

    /**
    * Cache the delegate and receiver field of each EventCallback type for increased performance when comparing.
    */
    protected Dictionary<Type, Dictionary<string, FieldInfo>> eventCallbacksCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();

    /// <summary>
    /// Gets or sets what type of date conversion to make when round tripping dates.
    /// </summary>
    [Parameter]
    public RoundTripDateConversion RoundTripDateConversion { get; set; } = RoundTripDateConversion.Auto;

    private DotNetObjectReference<WebCallback> _objRef;

    private DotNetObjectReference<WebCallback> GetObjectRef()
    {
        if (_objRef == null)
        {
            _objRef = DotNetObjectReference.Create(IgBlazor.WebCallback);
        }        

        return _objRef;
    }

    public BaseRendererControl(): base()
    {
        //Console.WriteLine("constructed: " + this.GetType().Name);
        //_dataSourceManager = new DataSourceManager(this, new RuntimeHelper(JsRuntime));
        //WebCallback.Instance.Register(this);
        //this._objRef = DotNetObjectReference.Create(IgBlazor.WebCallback);
        //_webCallbackHelper.WebCallback = WebCallback.Instance;
    }

    protected virtual string ResolveDisplay()
    {
        return "block";
    }

    protected string ToSpinal(string value)
    {
        if (value == null)
            {
                return null;
            }

            List<char> output = new List<char>();
            int upperRun = 0;

            for (var i = 0; i < value.Length; i++)
            {
                var curr = value[i];

                var upperChar = Char.ToUpper(curr);
                var lowerChar = Char.ToLower(curr);
                //if (i == 0)
                //{
                //	curr = upperChar;
                //}
                bool charIsNewWord = (upperRun == 0 && output.Count > 0);
                bool previousCharWasNewWord = (upperRun > 1);

                if (upperChar == curr)
                {
                    if (charIsNewWord)
                    {
                        output.Add('-');
                    }
                    upperRun++;
                }
                else if (lowerChar == curr)
                {
                    if (previousCharWasNewWord)
                    {
                        output.Insert(output.Count - 1, '-');
                    }
                    upperRun = 0;
                }
                else
                {
                    upperRun = 0;
                }

                output.Add(lowerChar);
            }

            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < output.Count; i++)
            {
                sb.Append(output[i]);
            }
            return sb.ToString();
    }

    protected virtual bool SupportsVisualChildren
    {
        get
        {
            return false;
        }
    }

    protected virtual bool UseDirectRender
    {
        get
        {
            return false;
        }
    }

    protected virtual string DirectRenderElementName
    {
        get
        {
            return "";
        }
    }

    private void EnsureSequenceInfo()
    {
        if (_sequenceInfo == null)
        {
            _sequenceInfo = BuildSequenceInfo(3);
        }
    }

    private Dictionary<string, object> GatherSimpleAttributes()
    {
        EnsureSequenceInfo();

        var ser = Serialize();
        var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(ser);
        Dictionary<string, object> ret = new Dictionary<string, object>();
        foreach (var key in data.Keys)
        {
            var currKey = key;
            var currValue = data[key];
            if (currValue is JsonElement) 
            {
                switch (((JsonElement)currValue).ValueKind)
                {
                    case JsonValueKind.False:
                        currValue = false;
                        break;
                    case JsonValueKind.True:
                        currValue = true;
                        break; 
                    case JsonValueKind.Array:
                        currValue = ArrayToSimpleAttributeValue((JsonElement)currValue);
                        break; 
                }
            }
            //currKey = TransformSimpleKey(currKey);
            ret[currKey] = currValue;
        }

        return ret;
    }

    private object ArrayToSimpleAttributeValue(JsonElement currValue)
    {
        string ret = "[";
        for (var i = 0; i < currValue.GetArrayLength(); i++) 
        {
            if (i > 0) 
            {
                ret += ", ";
            }
            var currEle = currValue[i];
            ret += "\"";
            switch (currEle.ValueKind)
            {
                case JsonValueKind.Undefined:
                 continue;
                case JsonValueKind.Object:
                 ret += currEle.ToString();
                 break;
                case JsonValueKind.Array:
                 ret += ArrayToSimpleAttributeValue(currEle);
                 break;
                case JsonValueKind.False:
                 ret += "false";
                 break;
                case JsonValueKind.Null:
                 ret += "null";
                 break;
                case JsonValueKind.String:
                 ret += currEle.GetString();
                 break;
                case JsonValueKind.True:
                 ret += "true";
                 break;
                case JsonValueKind.Number:
                 ret += currEle.ToString();
                 break;
            }
            ret += "\"";
    }
            ret += "]";
        return ret;
    }

    protected virtual string TransformSimpleKey(string key)
    {
        key = Camelize(key);
        return _sequenceInfo.TransformKey(key);
    }

    protected virtual bool IsTransformedEnumValue(string key)
    {
        key = Camelize(key);
        if (_sequenceInfo.IsTransformedEnum(key))
        {
            return true;
        }
        return false;
    }

    protected virtual object TransformPotentialEnumValue(string key, object value)
    {
        key = Camelize(key);
        //Console.WriteLine("transforming enum value...." + (value.GetType().Name));
        if (_sequenceInfo.IsTransformedEnum(key))
        {
            //Console.WriteLine("transforming enum value....");
        
            key = Camelize(key);
            return _sequenceInfo.TransformEnumValue(key, value.ToString());
        }
        return value;
    }

    private SequenceInfo _sequenceInfo = null;

    protected virtual SequenceInfo BuildSequenceInfo(int startSequence)
    {
        SequenceInfo info = new SequenceInfo(startSequence);
        var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public |
        System.Reflection.BindingFlags.Instance | 
        System.Reflection.BindingFlags.FlattenHierarchy);
        foreach (var prop in props)
        {
            bool isParam = false;
            string wcName = null;
            foreach (var attr in prop.GetCustomAttributes(true))
            {
                if (attr.GetType().Name == "ParameterAttribute")
                {
                    isParam = true;
                }
                if (attr.GetType().Name == "WCWidgetMemberNameAttribute")
                {
                    var wc = (WCWidgetMemberNameAttribute)attr;
                    wcName = Camelize(wc.Name);
                }
                if (attr.GetType().Name == "WCAttributeNameAttribute")
                {
                    var wc = (WCAttributeNameAttribute)attr;
                    wcName = wc.Name;
                }
                }

            var pType = prop.PropertyType;
            Dictionary<string, string> wcEnumTransform = null;
            if (pType != null)
            {
                if (pType.IsEnum)
                {
                    
                    foreach (var f in pType.GetFields()) 
                    {
                        if (f.IsPublic && !f.IsSpecialName)
                        {
                            foreach (var attr in f.GetCustomAttributes(true))
                            {
                                if (attr.GetType().Name == "WCEnumNameAttribute")
                                {
                                    if (wcEnumTransform == null)
                                    {
                                        wcEnumTransform = new Dictionary<string, string>();
                                    }
                                    var wc = (WCEnumNameAttribute)attr;
                                    var wcEnumName = Camelize(wc.Name);
                                    wcEnumTransform.Add(f.Name.ToLower(), wcEnumName);
                                }
                            }
                        }
                    }
                }
            }

            if (isParam)
            {
                info.AddSequence(Camelize(prop.Name), wcName, wcEnumTransform);
            }
        }

        return info;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        string spinalName = ToSpinal(this.Type);
        string className = "igb-" + spinalName;
        if (Class != null)
        {
            className = Class;
        }

        if (UseDirectRender)
        {
            builder.OpenElement(0, DirectRenderElementName);
            builder.AddAttribute(1, "class", className);
            var attributes = GatherSimpleAttributes();
            builder.AddAttribute(2, "data-ig-id", _containerId);
            
            EnsureSequenceInfo();
            foreach (var key in _sequenceInfo.AttributeKeys)
            {
                if (attributes.ContainsKey(key))
                {
                    var sequence = _sequenceInfo.GetSequence(key);
                    var tKey = TransformSimpleKey(key);

                    var val = attributes[key];
                    if (val is bool) 
                    {
                        if ((bool)val == false)
                        {
                            continue;
                        }
                    }

                    var attributeVal = attributes[key];
                    if (IsTransformedEnumValue(key))
                    {
                        attributeVal =  TransformPotentialEnumValue(key, attributeVal);
                    }
                    //Console.WriteLine("adding attribute: " + tKey + ", " + attributes[key]);
                    builder.AddAttribute(sequence, ToSpinal(ToPascal(tKey)), attributeVal);
                }
            }

            builder.AddMultipleAttributes(4 + _sequenceInfo.MaxSequence, AdditionalAttributes);

            
            builder.AddElementReferenceCapture(5 + 15 + _sequenceInfo.MaxSequence, delegate(ElementReference value)
            {
                contEle = value;
            });

            
            //builder.AddMarkupContent(6 + 15 + _sequenceInfo.MaxSequence, "\r\n    ");
            builder.OpenComponent<CascadingValue<BaseRendererControl>>(6); // TODO: This '6' here might be a bug because it doesn't seem to match the other line sequence numbers
            builder.AddAttribute(7 + 15 + _sequenceInfo.MaxSequence, "Value", this);
            builder.AddAttribute(8 + 15 + _sequenceInfo.MaxSequence, "Name", ParentTypeName);
            builder.AddAttribute(9 + 15 + _sequenceInfo.MaxSequence, "ChildContent", (RenderFragment)delegate(RenderTreeBuilder builder2)
            {
                //builder2.AddMarkupContent(10 + 15 + _sequenceInfo.MaxSequence, "\r\n        ");
                builder2.AddContent(11 + 15 + _sequenceInfo.MaxSequence, ChildContent);
                //builder2.AddMarkupContent(12 + 15 + _sequenceInfo.MaxSequence, "\r\n    ");
            });
            builder.CloseComponent();
            //builder.AddMarkupContent(13 + 15 + _sequenceInfo.MaxSequence, "\r\n");

            builder.CloseElement();
            return;
        }

        //Console.WriteLine("rendering");
        var width = Width;
        var height = Height;
        var display = "block";
        bool hasSize = false;
        if ((width != "" && width != null) ||
            (height != "" && height != null)) 
        {
            hasSize = true;
        }        

        display = ResolveDisplay();

        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", className);
        if (hasSize)
        {
            builder.AddAttribute(2, "style", "width: " + Width + "; height: " + Height + "; display: " + display + "; padding: 0px;");
        }
        else
        {
            builder.AddAttribute(2, "style", "display: " + display + "; padding: 0px;");    
        }
        builder.AddMultipleAttributes(3, AdditionalAttributes);

        builder.OpenElement(4, "igc-component-renderer-container");
        builder.AddAttribute(5, "data-ig-id", _containerId);
        //if (hasSize)
        {
            builder.AddAttribute(6, "style", "width: 100%; height: 100%; display: " + display + ";");
        }

        // if (SupportsVisualChildren) {
        //     builder.AddAttribute(7, "shadow-dom-mode", true);
        //     builder.AddAttribute(8, "child-content-mode", true);
        // }

        if (SupportsVisualChildren) {
            builder.AddAttribute(7, "portal-mode", true);
        }

        // else
        // {
        //     builder.AddAttribute(4, "style", "display: " + display + ";");
        // }

        builder.AddElementReferenceCapture(9, delegate(ElementReference value)
        {
            contEle = value;
        });
        
        builder.CloseElement();

        builder.AddMarkupContent(10, "\r\n    ");
        if (!SupportsVisualChildren)
        {
            builder.OpenElement(11, "div");
            builder.AddAttribute(12, "class", "ig-hidden-content");
            builder.AddAttribute(13, "style", "display: none");
            builder.AddMarkupContent(14, "\r\n");
            builder.OpenComponent<CascadingValue<BaseRendererControl>>(10);
            builder.AddAttribute(15, "Value", this);
            builder.AddAttribute(16, "Name", ParentTypeName);
            builder.AddAttribute(17, "ChildContent", (RenderFragment)delegate(RenderTreeBuilder builder2)
            {
                builder2.AddMarkupContent(18, "\r\n        ");
                builder2.AddContent(19, ChildContent);
                builder2.AddMarkupContent(20, "\r\n    ");
            });
            builder.CloseComponent();
            builder.CloseElement();
        }
        builder.AddMarkupContent(21, "\r\n");
        

        if (SupportsVisualChildren)
        {
            builder.OpenComponent<CascadingValue<BaseRendererControl>>(10);
            builder.AddAttribute(22, "Value", this);
            builder.AddAttribute(23, "Name", ParentTypeName);
            builder.AddAttribute(24, "ChildContent", (RenderFragment)delegate(RenderTreeBuilder builder2)
            {
                builder2.AddMarkupContent(25, "\r\n        ");
                builder2.OpenElement(26, "igc-portal-entrance");
                builder2.AddAttribute(27, "portal-id", "portal-" + _containerId);
                builder2.AddAttribute(28, "move-once-mode", "true");
                builder2.AddContent(29, ChildContent);
                builder2.CloseElement();
                builder2.AddMarkupContent(30, "\r\n    ");
            });
            builder.CloseComponent();
            builder.AddMarkupContent(31, "\r\n");
        }

        if (NeedsDynamicContent)
		{
			builder.OpenComponent<DynamicContentHolder>(32);
			builder.AddComponentReferenceCapture(33, delegate(object __value)
			{
				Holder = (DynamicContentHolder)__value;
			});
			builder.CloseComponent();
			builder.AddMarkupContent(34, "\r\n");
		}
        builder.CloseElement();
    }

        

        internal Dictionary<string,object>[] DeserializeDictionaryArray(string batch)
        {
            return JsonSerializer.Deserialize<Dictionary<string,object>[]>(batch, SerializerOptions);
        }

        private Dictionary<string, DynamicContentInfo> _dynamicContentInfos = new Dictionary<string, DynamicContentInfo>();

    private Dictionary<string, object> _contentTemplates = new Dictionary<string, object>();
    private Dictionary<string, Type> _contentTemplateTypes = new Dictionary<string, Type>();
    internal void UpdateTemplate(string templateId, object template, Type type)
    {
        _contentTemplates[templateId] = template;
        _contentTemplateTypes[templateId] = type;
    }

    internal object FindTemplate(string templateId)
    {
        if (_contentTemplates.ContainsKey(templateId))
        {
            return _contentTemplates[templateId];
        }
        return null;
    }

    internal void AdjustDynamicContent(string containerId, string contentType, string templateId, string contentId, string actionType, string args)
    {
        switch (actionType)
        {
            case "Add":
            {
                DynamicContentInfo dynamicContent = BuildDynamicContentInfo(contentType, templateId);
                if (dynamicContent == null)
                {
                    return;
                }
                dynamicContent.Owner = this;
                dynamicContent.RefName = contentId;
                _dynamicContentInfos[contentId] = dynamicContent;
                
                var template = FindTemplate(templateId);
                if (template == null)
                {
                    return;
                }
                else
                {
                    dynamicContent.UpdateTemplate(template);
                }

                Holder.AddDynamicContent(dynamicContent);
                break;
            }
            case "Remove":
            {
                if (_dynamicContentInfos.ContainsKey(contentId))
                {
                    DynamicContentInfo dynamicContent = _dynamicContentInfos[contentId];
                    _dynamicContentInfos.Remove(contentId);
                    Holder.RemoveDynamicContent(dynamicContent);
                }
                break;
            }  
            case "Update":
            {
                if (_dynamicContentInfos.ContainsKey(contentId))
                {
                    DynamicContentInfo dynamicContent = _dynamicContentInfos[contentId];
                    
                    object context = null;
                    if (args != null)
                    {
                        var argsDic = JsonSerializer.Deserialize<Dictionary<string, object>>(args, SerializerOptions);
                        context = ConvertReturnValue(argsDic);
                    }
                    dynamicContent.UpdateContext(context);
                }
                break;
            }  
        }
    }

    protected Type TemplateContentType(string templateId)
    {
        if (!_contentTemplateTypes.ContainsKey(templateId))
        {
            return null;
        }

        return _contentTemplateTypes[templateId];
    }

   
    private Dictionary<Type, Func<DynamicContentInfo>>  _dynamicContentBuilders = new Dictionary<Type, Func<DynamicContentInfo>>();
    private DynamicContentInfo BuildDynamicContentInfo(string contentType, string templateId)
    {
        var templateContentType = TemplateContentType(templateId);
        if (templateContentType != null)
        {
            if (!_dynamicContentBuilders.ContainsKey(templateContentType))
            {
                if (contentType == "TemplateContent" && templateContentType != null)
                {
                    var createType = typeof(DynamicContentInfo<>).MakeGenericType(templateContentType);
                    var nonGen = typeof(DynamicContentInfo);
                    System.Linq.Expressions.NewExpression newExp = System.Linq.Expressions.Expression.New(createType);
                    System.Linq.Expressions.UnaryExpression conversion = System.Linq.Expressions.Expression.Convert(newExp, nonGen);

                    var getNew = (Func<DynamicContentInfo>)System.Linq.Expressions.Expression.Lambda(conversion).Compile();
                    _dynamicContentBuilders[templateContentType] = getNew;
                }
                else
                {
                    //TODO: other types
                    _dynamicContentBuilders[templateContentType] = () => null;
                }
            }
        }
        else
        {
            //TODO: other types
            _dynamicContentBuilders[templateContentType] = () => null;
        }

        return _dynamicContentBuilders[templateContentType]();
    }

    protected virtual bool NeedsDynamicContent
    {
        get
        {
            return false;
        }
    }

    private DynamicContentHolder Holder { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender)
        {
            MarkContentDirty();
            EnsureReady();
        }
    }

    //protected override void OnParametersSet()
    //{
    //    Console.WriteLine("on parameters set");
    //}
    // protected override bool ShouldRender()
    // {
    //     return false;
    // }

    public async Task EnsureReady() {
        if (!IgBlazor.IsRuntimeValid(_shouldReevaluateRuntime)) {
            return;
        }

        //Console.WriteLine("ensuring ready: " + this.GetType().Name);
        while (!this._ready) {
            bool ready = await JsRuntime.InvokeAsync<bool>("igCheckReady", new object[] { _containerId });
            //Console.WriteLine(ready + " -> " + this.GetType().Name);
            if (ready)
            {
                await JsRuntime.InvokeVoidAsync("igWaitForLoaded");
                OnReady();
                break;
            }
            await Task.Delay(100);
        }
    }

    internal void OnReady() {
        this._ready = true;

        QueueUpdate();
    }

    protected internal void MarkPropDirty(string propertyName) {
        _isDirty[propertyName] = true;
        _hasDirty = true;
        _serializeDirty = true;
        //Console.WriteLine("dirty: " + propertyName);
        MarkContentDirty();
    }

    private bool _isContentDirty = false;
    private void MarkContentDirty() {
        if (!_isContentDirty) {
            //Console.WriteLine("sending message");
            _isContentDirty = true;
            SendDescriptionMessage();
        }
    }

    private void SendDescriptionMessage() {
        if (disposedValue) 
        {
            return;
        }
        RendererMessage m = new RendererMessage();
        m.Type = ("description");
        _messageQueue.AddLast(m);
        QueueUpdate();
    }

    internal void OnPropertyPropagatedOut(string name, string propertyName)
    {
        var camelName = propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        SendImmediateDeltaDescription((n, p) => n == name && p == camelName);
    }

    private void SendImmediateDeltaDescription(SerializationFilter filter)
    {
        using(var stream = new MemoryStream())
        {
            using (Utf8JsonWriter uw = new Utf8JsonWriter(stream))
            {
                SerializationContext c = new SerializationContext(uw, filter);
                Serialize(c);

                RendererMessage m = new RendererMessage();
                m.Type = "descriptionDelta";
                m.SetData("skipApply", "true");
               
                uw.Flush();
                var desc = Encoding.UTF8.GetString(stream.ToArray());
                m.SetData("description", desc);

                SendMessageImmediate(m);
            }
        }
    }

    protected bool IsPropDirty(String propertyName) {
        if (_isDirty.ContainsKey(propertyName)) {
            return _isDirty[propertyName];
        }

        return false;
    }

    internal void ChildDirty(object child) {
        _serializeDirty = true;
        MarkContentDirty();
    }

    internal virtual void SerializeCore(RendererSerializer ser) {
        ser.AddStringProp("name", "mainControl");
    }

    protected String _cachedSerializedContent = "";

    public virtual string Type
    {
        get
        {
            var typeName = (this.GetType().Name.Replace("View", "View"));
            if (typeName.StartsWith("Igb"))
            {
                typeName = typeName.Substring(3);
            }
            return typeName;
        }
    }

    public void Serialize(SerializationContext context, string propertyName = null) {
        RendererSerializer ser = new RendererSerializer(context, this, Name);
        ser.Type = Type;
        ser.Start(propertyName);
        SerializeCore(ser);
        ser.End();
    }

    public string Serialize()
    {
        if (_serializeDirty) {
            using(var stream = new MemoryStream())
            {
                using (Utf8JsonWriter uw = new Utf8JsonWriter(stream))
                {
                    SerializationContext c = new SerializationContext(uw, null);
                //RendererSerializer ser = new RendererSerializer(uw);
                
                    Serialize(c);
                    uw.Flush();
                    _cachedSerializedContent = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            _serializeDirty = false;
        }
        return _cachedSerializedContent;
    }

    //private Dictionary<Long, Semaphore> _methodSemaphores = new Dictionary<Long, Semaphore>();
    private Dictionary<long, Object> _methodReturns = new Dictionary<long, Object>();
    private Object _semLock = new Object();

    static long _invokeId = 0;
    protected async Task<object> InvokeMethod(string methodName, object[] arguments, string[] types, ElementReference[] nativeElements = null) {
        return await InvokeMethodHelper(null, methodName, arguments, types, nativeElements);
    }

    protected object InvokeMethodSync(string methodName, object[] arguments, string[] types, ElementReference[] nativeElements = null) {
        return InvokeMethodHelperSync(null, methodName, arguments, types, nativeElements);
    }

    private JsonSerializerOptions _serializerOptions = null;
    private JsonSerializerOptions SerializerOptions
    {
        get
        {
            if (_serializerOptions == null)
            {
                var def = IgBlazor.Settings.JsonSerializerOptions;
                var options = new JsonSerializerOptions();
                options.MaxDepth = def.MaxDepth;
                _serializerOptions = options;
            }
            return _serializerOptions;
        }
    }

    internal object InvokeMethodHelperSync(string target, string methodName, object[] arguments, string[] types, ElementReference[] nativeElements) {
        if (!_ready) {
            throw new InvalidOperationException("cannot invoke method until main component is ready.");
        }
        if (JsInProcessRuntime == null) 
        {
            throw new InvalidOperationException("cannot synchronously invoke a method without IJSInProcessRuntime");
        }
        RendererMessage m = new RendererMessage();
        m.Type = ("invokeMethod");
        string[] args = new string[arguments.Length];
        string[] typeStrings = new string[arguments.Length];
        long invokeId = _invokeId++;
        for (int i = 0; i < arguments.Length; i++) {
            args[i] = GetStringArg(arguments[i], types[i]);
            typeStrings[i] = "\"" + types[i] + "\"";
        }
        m.SetData("isSync", "true");
        m.SetData("methodName", "\"" + methodName + "\"");
        if (target != null) {
            m.SetData("target", "\"" + target + "\"");
        }
        m.SetData("invokeId", ((long)invokeId).ToString());
        m.SetData("arguments", "[" + String.Join(", ", args) + "]");
        m.SetData("types", "[" + String.Join(", ", typeStrings) + "]");
        m.NativeElements = nativeElements;
        var ret = SendMessageSyncImmediate(m);

        if (ret is JsonElement && ((JsonElement)ret).ValueKind == JsonValueKind.String)
        {
            var str = ((JsonElement)ret).GetString();
            var retDict = JsonSerializer.Deserialize<Dictionary<string, object>>(str, SerializerOptions);

            if (retDict.ContainsKey("retType") &&
                retDict["retType"] is JsonElement &&
                ((JsonElement)retDict["retType"]).GetString() == "promise")
            {
                throw new Exception($"Invocation of method \"{methodName}\" returned a promise. Please use the async version of the method to get the value.");
            }

            ret = retDict;
        }
        //Console.WriteLine("got return");
        //Console.WriteLine(ret);
        return ret;
        
    }


    Dictionary<long, TaskCompletionSource<object>> _methodTasks = new Dictionary<long, TaskCompletionSource<object>>();

    internal async Task<object> InvokeMethodHelper(string target, string methodName, object[] arguments, string[] types, ElementReference[] nativeElements) {
        if (!_ready) {
            throw new InvalidOperationException("cannot invoke method until main component is ready.");
        }
        RendererMessage m = new RendererMessage();
        m.Type = ("invokeMethod");
        string[] args = new string[arguments.Length];
        string[] typeStrings = new string[arguments.Length];
        long invokeId = _invokeId++;
        for (int i = 0; i < arguments.Length; i++) {
            args[i] = GetStringArg(arguments[i], types[i]);
            typeStrings[i] = "\"" + types[i] + "\"";
        }
        m.SetData("methodName", "\"" + methodName + "\"");
        if (target != null) {
            m.SetData("target", "\"" + target + "\"");
        }
        m.SetData("invokeId", ((long)invokeId).ToString());
        m.SetData("arguments", "[" + String.Join(", ", args) + "]");
        m.SetData("types", "[" + String.Join(", ", typeStrings) + "]");
        m.NativeElements = nativeElements;
        var ret = await SendMessageImmediate(m);

        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
        _methodTasks.Add(invokeId, tcs);

        if (ret is JsonElement && ((JsonElement)ret).ValueKind == JsonValueKind.String)
        {
            var str = ((JsonElement)ret).GetString();
            var retDict = JsonSerializer.Deserialize<Dictionary<string, object>>(str, SerializerOptions);
            ret = retDict;

            if (retDict.ContainsKey("retType") &&
                retDict["retType"] is JsonElement &&
                ((JsonElement)retDict["retType"]).GetString() == "promise")
            {
                // did we already get a value returned for this method invoke before we could start up a task?
                if (_methodReturns.ContainsKey(invokeId))
                {
                    tcs.SetResult(_methodReturns[invokeId]);
                }
            }
            else
            {
                tcs.SetResult(ret);
            }
        }
        var result = await tcs.Task;

        _methodTasks.Remove(invokeId);
        _methodReturns.Remove(invokeId);

        return result;
    }

    private string GetStringArg(object argument, string type) {
        if (argument == null) {
            return null;
        }
        if (type == "Number" && argument is double &&
        double.IsNaN((double)argument))
        {
            return null;
        }
        if (type == "Date")
        {
            if (argument is DateTime)
            {
                argument = DateToString((DateTime)argument);
            }
            return "\"" + argument.ToString() + "\"";
        }
        if (type == "NumberArray")
        {
            var str = "[";
            if (argument is int[])
            {
                var values = (int[])argument;
                for (int i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                    {
                        str += ",";
                    }
                    str += values[i].ToString();
                }
            }
            if (argument is double[])
            {
                var values = (double[])argument;
                for (int i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                    {
                        str += ",";
                    }
                    str += values[i].ToString();
                }
            }
            str += "]";
            return str;
        }
        if (type == "StringArray")
        {
            var str = "[";
            var values = (string[])argument;
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    str += ",";
                }
                str += "\"" + values[i].ToString() + "\"";
            }
            str += "]";
            return str;
        }
        if (type == "DateArray")
        {
            var str = "[";
            var values = (DateTime[])argument;
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    str += ",";
                }
                str += "\"" + DateToString((DateTime)values[i]) + "\"";
            }
            str += "]";
            return str;
        }
        if (type == "BoolArray")
        {
            var str = "[";
            var values = (bool[])argument;
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    str += ",";
                }
                str += values[i].ToString().ToLower();
            }
            str += "]";
            return str;
        }
        
        return argument.ToString();
    }

    internal void OnRefChanged(string propertyName, object oldValue, object newValue, bool isScript, bool isElement, Action<string, object, object> refChanged) {
        _isDirtyRef[propertyName] = true;
        _isDirty[propertyName] = true;
        _hasDirty = true;
        _serializeDirty = true;
        string refId = _containerId + "/" + propertyName;

        if (newValue is LocalJson)
        {
            newValue = ((LocalJson)newValue).ToRef();
        }
        if (newValue is RemoteJson)
        {
            newValue = ((RemoteJson)newValue).ToRef();
        }

       
        if (!isElement && !isScript)
        {
            if (newValue is BaseRendererControl || newValue is BaseRendererElement)
            {
                isElement = true;
            }
        }

        if (newValue is string && ((string)newValue).StartsWith("json:::")) {
            isScript = true;
        }
        if (newValue is string && ((string)newValue).StartsWith("localJson:::")) {
            isScript = true;
        }

        if (!isScript) {
            if (isElement)
            {
                if (newValue == null)
                {
                    refId = null;
                }
                else
                {
                    if (oldValue is BaseRendererElement)
                    {
                         ((BaseRendererElement)oldValue).Parent = null;
                    }
                    if (newValue is BaseRendererElement)
                    {
                        refId = _containerId + "/" + ((BaseRendererElement)newValue).Name;
                        ((BaseRendererElement)newValue).Parent = this;
                    }
                    else
                    {
                        if (!((BaseRendererControl)newValue).disposedValue)
                        {
                            refId = ((BaseRendererControl)newValue)._containerId;
                            newValue = "containerId:::" + refId;
                            OnRefChanged(refId, "\"" + newValue + "\"");
                        }
                    }
                }
            }
            else
            {
                refId = _dataSourceManager.OnRefChanged(propertyName, newValue);
            }
        } else {
            var str = newValue.ToString();
            
            if (str.StartsWith("event:::") || str.StartsWith("nativeEvent:::") ||  str.StartsWith("json:::") || str.StartsWith("localJson:::") || str.StartsWith("template:::"))
            {
                OnRefChanged(refId, "\"" + newValue.ToString() + "\"");
            }
            else
            {
                OnRefChanged(refId, "\"script:::" + newValue.ToString() + "\"");
            }
        }
        refChanged(refId, oldValue, newValue);
    }

        internal string DateToString(DateTime val)
        {
            return val.ToString("o");
        }

    /// <summary>
    /// Prevents data change notifications from be propagated to the component.
    /// </summary>
    /// <param name="dataSource">The datasource that is being changed.</param>
    public void SuspendNotifications(object dataSource)
    {
        _dataSourceManager.SuspendNotifications(dataSource);
    }
    /// <summary>
    /// Resumes data change notifications.
    /// </summary>
    /// <param name="dataSource">The datasource that is being changed.</param>
    public void ResumeNotifications(object dataSource, bool notify = true)
    {
        _dataSourceManager.ResumeNotifications(dataSource, notify);
    }

        public void NotifyInsertItem(object dataSource, int index, object refItem) {
        if (!_dataSourceManager.HasRefId(dataSource)) {
            return;
        }
        string refName = _dataSourceManager.GetRefId(dataSource);
        if (refName == null) {
            return;
        }
        _dataSourceManager.NotifyInsertItem(refName, index, refItem);
    }

    public void NotifyRemoveItem(object dataSource, int index, object oldItem) {
        if (!_dataSourceManager.HasRefId(dataSource)) {
            return;
        }
        string refName = _dataSourceManager.GetRefId(dataSource);
        if (refName == null) {
            return;
        }
        _dataSourceManager.NotifyRemoveItem(refName, index, oldItem);
    }

    public void NotifyClearItems(object dataSource) {
        if (!_dataSourceManager.HasRefId(dataSource)) {
            return;
        }
        string refName = _dataSourceManager.GetRefId(dataSource);
        if (refName == null) {
            return;
        }
        _dataSourceManager.NotifyClearItems(refName);
    }

    public void NotifySetItem(object dataSource, int index, object oldItem, object newItem) {
        if (!_dataSourceManager.HasRefId(dataSource)) {
            return;
        }
        string refName = _dataSourceManager.GetRefId(dataSource);
        if (refName == null) {
            return;
        }
        _dataSourceManager.NotifySetItem(refName, index, oldItem, newItem);
    }

    public void NotifyUpdateItem(object dataSource, int index, object refItem, bool syncDataOnly = false) {
        if (!_dataSourceManager.HasRefId(dataSource)) {
            return;
        }
        string refName = _dataSourceManager.GetRefId(dataSource);
        if (refName == null) {
            return;
        }
        _dataSourceManager.NotifyUpdateItem(refName, index, refItem, syncDataOnly);
    }

    public void OnRefChanged(string refName, object refValue) {
        RendererMessage m = new RendererMessage();
        m.Type = ("refChanged");
        m.SetData("refName", "\"" + refName + "\"");
        if (refValue is IJSDataSource) {
            var ds = (IJSDataSource)refValue;
            var dataIntents = ds == null ? null : ds.GetDataIntentsAsJson();
            if (dataIntents != null)
            {
                m.SetData("dataIntents", dataIntents);
            }
            if (ds.DataSourceType == JSDataSourceType.Json)
            {
                if (!ds.IsSent)
                {
                    ds.IsSent = true;
                    m.SetData("refValue", refValue == null ? "null" : ((JsonDataSource)refValue).ToJson());
                    m.SetData("dateCache", refValue == null ? "null" : ((JsonDataSource)refValue).GetDateCacheAsJson());
                }
                else
                {
                    return;
                }
            }
            else
            {
                var uds = (UnmarshalledDataSource)refValue;
                uds.SendCreate(_containerId, refName, dataIntents);

                return;
            }
        } else {
            m.SetData("refValue", refValue == null ? "null" : refValue.ToString());
        }
        SendMessage(m);
    }

    void RefSink.OnRefNotifyInsertItem(IJSDataSource dataSource, string refName, int index, object refItem) {
        if (dataSource.DataSourceType == JSDataSourceType.Json)
        {
            RendererMessage m = new RendererMessage();
            m.Type = ("refNotifyInsertItem");
            m.SetData("refName", "\"" + refName + "\"");
            m.SetData("index", index.ToString());
            m.SetData("newItem", refItem == null ? "null" : ((JsonDataSourceItem)refItem).ToJson());

            if (!((JsonDataSource)dataSource).DateCacheReady)
            {
                m.SetData("dateCache", ((JsonDataSource)dataSource).GetDateCacheAsJson());
            }
            //Console.WriteLine("sending insert message");
            SendMessage(m);
        }
        else
        {
            var uds = (UnmarshalledDataSource)dataSource;
            uds.SendInsert(_containerId, refName, index);
        }
    }

    void RefSink.OnRefNotifyRemoveItem(IJSDataSource dataSource, string refName, int index, object oldItem) {
        if (dataSource.DataSourceType == JSDataSourceType.Json)
        {
            RendererMessage m = new RendererMessage();
            m.Type = ("refNotifyRemoveItem");
            m.SetData("refName", "\"" + refName + "\"");
            m.SetData("index", index.ToString());
            m.SetData("oldItem", oldItem == null ? "null" : ((JsonDataSourceItem)oldItem).ToJson());
            if (!((JsonDataSource)dataSource).DateCacheReady)
            {
                m.SetData("dateCache", ((JsonDataSource)dataSource).GetDateCacheAsJson());
            }
            SendMessage(m);
        }
        else
        {
            var uds = (UnmarshalledDataSource)dataSource;
            uds.SendRemove(_containerId, refName, index);
        }
    }

    void RefSink.OnRefNotifyClearItems(IJSDataSource dataSource, string refName, object refValue) {
        if (dataSource.DataSourceType == JSDataSourceType.Json)
        {
            RendererMessage m = new RendererMessage();
            m.Type = ("refClearItems");
            m.SetData("refName", "\"" + refName + "\"");
            m.SetData("refValue", refValue == null ? "null" : ((JsonDataSource)refValue).ToJson());
            if (!((JsonDataSource)dataSource).DateCacheReady)
            {
                m.SetData("dateCache", ((JsonDataSource)dataSource).GetDateCacheAsJson());
            }
            SendMessage(m);
        }
        else
        {
            var uds = (UnmarshalledDataSource)dataSource;
            uds.SendClear(_containerId, refName);
        }
    }

    void RefSink.OnRefNotifySetItem(IJSDataSource dataSource, string refName, int index, object oldItem, object newItem) {
        if (dataSource.DataSourceType == JSDataSourceType.Json)
        {
            RendererMessage m = new RendererMessage();
            m.Type = ("refNotifySetItem");
            m.SetData("refName", "\"" + refName + "\"");
            m.SetData("index", index.ToString());
            m.SetData("oldItem", oldItem == null ? "null" : ((JsonDataSourceItem)oldItem).ToJson());
            m.SetData("newItem", oldItem == null ? "null" : ((JsonDataSourceItem)newItem).ToJson());
            if (!((JsonDataSource)dataSource).DateCacheReady)
            {
                m.SetData("dateCache", ((JsonDataSource)dataSource).GetDateCacheAsJson());
            }
            SendMessage(m);
        }
        else
        {
            var uds = (UnmarshalledDataSource)dataSource;
            uds.SendUpdate(_containerId, refName, index, false);
        }
    }

    void RefSink.OnRefNotifyUpdateItem(IJSDataSource dataSource, string refName, int index, object refItem, bool syncDataOnly) {
        if (dataSource.DataSourceType == JSDataSourceType.Json)
        {
            RendererMessage m = new RendererMessage();
            m.Type = ("refNotifyUpdateItem");
            m.SetData("refName", "\"" + refName + "\"");
            m.SetData("index", index.ToString());
            m.SetData("item", refItem == null ? "null" : ((JsonDataSourceItem)refItem).ToJson());
            m.SetData("syncDataOnly", syncDataOnly ? "true" : "false");
            if (!((JsonDataSource)dataSource).DateCacheReady)
            {
                m.SetData("dateCache", ((JsonDataSource)dataSource).GetDateCacheAsJson());
            }
            SendMessage(m);
        }
        else
        {
            var uds = (UnmarshalledDataSource)dataSource;
            uds.SendUpdate(_containerId, refName, index, syncDataOnly);
        }
    }

    private void SendMessage(RendererMessage m) {
        if (disposedValue)
        {
            return;
        }
        //Console.WriteLine("sending message");
        _messageQueue.AddLast(m);
        QueueUpdate();
    }

    private async Task<object> SendMessageImmediate(RendererMessage m) {
        if (disposedValue)
        {
            return null;
        }

        Update();
        return await SendJsonImmediate(m);
    }

    private object SendMessageSyncImmediate(RendererMessage m) {
        if (disposedValue)
        {
            return null;
        }
        UpdateSync();
        return SendJsonImmediateSync(m);
    }

    private void QueueUpdate() {
        if (!_updateQueued && _ready) {
            _updateQueued = true;
            Task.Delay(0).ContinueWith((t) => InvokeAsync(Update));
        }
    }

    private void Update() {
        this._updateQueued = false;

        if (!_ready) {
            return;
        }

        //Console.WriteLine("updateing: " + this.GetType().Name + " " + _messageQueue.Count);
        while (_messageQueue.Count > 0) {
            RendererMessage m = _messageQueue.First.Value;
            _messageQueue.RemoveFirst();
            ProcessMessage(m);
        }
    }

    private void UpdateSync() {
        this._updateQueued = false;

        if (!_ready) {
            return;
        }

        while (_messageQueue.Count > 0) {
            RendererMessage m = _messageQueue.First.Value;
            _messageQueue.RemoveFirst();
            ProcessMessageSync(m);
        }
    }

    private string _description = "description";
    private bool disposedValue;

    private void ProcessMessage(RendererMessage m) {
        if (m.Type == _description) {
            if (UseDirectRender)
            {
                _isContentDirty = false;
                return;
            }
            //Console.WriteLine("is description: " + this.GetType().Name);
            string ser = this.Serialize();
            //Console.WriteLine("ser");
            //Console.WriteLine(ser);
            m.SetData("description", ser);
            _isContentDirty = false;
        }
        else if (m.Type == "refChanged")
        {
            m.SetData("eventBehavior", "\"" + ResolveEventBehavior().ToString().ToLower() + "\"");
        }
        string json = m.ToJson();
        //Console.WriteLine("message");
        //Console.WriteLine("json: " + json);
        SendJson(json, m.NativeElements);
    }

     private void ProcessMessageSync(RendererMessage m) {
        if (m.Type == _description) {
            if (UseDirectRender)
            {
                 _isContentDirty = false;
                return;
            }
            string ser = this.Serialize();
            m.SetData("description", ser);
            _isContentDirty = false;
        }
        else if (m.Type == "refChanged")
        {
            m.SetData("eventBehavior", "\"" + ResolveEventBehavior().ToString().ToLower() + "\"");
        }
        string json = m.ToJson();
        SendJsonSync(json, m.NativeElements);
    }

    private async Task<object> SendJsonImmediate(RendererMessage m) {
        if (IgBlazor == null ||!IgBlazor.IsRuntimeValid(_shouldReevaluateRuntime)) {
            return null;
        }

        if (m.Type == _description) {
            if (UseDirectRender)
            {
                 _isContentDirty = false;
                return null;
            }
            string ser = this.Serialize();
            m.SetData("description", ser);
            _isContentDirty = false;
        }
        
        string json = m.ToJson();

        if (m.NativeElements != null)
        {
            return await JsRuntime.InvokeAsync<object>("igSendMessage", 
                new object[] { this._containerId, json,
                    GetObjectRef(), m.NativeElements });
        }
        else
        {
                //json = "window.sendMessage(`" + this._id + "`, `" + json + "`)";
            return await JsRuntime.InvokeAsync<object>("igSendMessage", 
                new object[] { this._containerId, json,
                    GetObjectRef() });
        }
    }

    private object SendJsonImmediateSync(RendererMessage m) {
        if (m.Type == _description) {
            string ser = this.Serialize();
            m.SetData("description", ser);
            _isContentDirty = false;
        }
        
        string json = m.ToJson();
        ElementReference[] nativeElements = m.NativeElements;

        if (nativeElements != null)
        {
//json = "window.sendMessage(`" + this._id + "`, `" + json + "`)";
            return this.JsInProcessRuntime.Invoke<object>("igSendMessage", new object[] { this._containerId, json,
                GetObjectRef(), nativeElements });
        }
        else
        {
        //json = "window.sendMessage(`" + this._id + "`, `" + json + "`)";
            return this.JsInProcessRuntime.Invoke<object>("igSendMessage", new object[] { this._containerId, json,
                GetObjectRef()});
        }
    }

    private void SendJson(string json, ElementReference[] nativeElements) {
        //json = "window.sendMessage(`" + this._containerId + "`, `" + json + "`)";
        //Console.WriteLine(json);
        if (!IgBlazor.IsRuntimeValid(_shouldReevaluateRuntime)) {
            return;
        }
                
        if (nativeElements != null)
        {                
            JsRuntime.InvokeAsync<object>("igSendMessage",
                new object[] { this._containerId, json,
                    GetObjectRef(), nativeElements });               
        }
        else
        {
            JsRuntime.InvokeAsync<object>("igSendMessage", 
                new object[] { this._containerId, json,
                    GetObjectRef() });
        }
    }

    internal void AttachChild(BaseRendererElement child) {
        if (child == null)
        {
            return;
        }
        child.Parent = this;
    }

    internal void AttachChild<T>(BaseCollection<T> child) {
        if (child == null)
        {
            return;
        }
        child.Parent = this;

        if (child != null)
        {
            for (var i = 0; i < child.Count; i++)
            {
                var subChild = child[i];
                if (subChild is BaseRendererElement)
                {
                    AttachChild((BaseRendererElement)(object)subChild);
                }
            }
        }
    }

    internal void DetachChild(BaseRendererElement child) {
        if (child == null)
        {
            return;
        }
        if (child.Parent == this)
        {
            child.Parent = null;
        }
    }

    internal void DetachChild<T>(BaseCollection<T> child) {
        if (child == null)
        {
            return;
        }
        if (child.Parent == this)
        {
            child.Parent = null;
        }
    }


    private void SendJsonSync(string json, ElementReference[] nativeElements) {
            //json = "window.sendMessage(`" + this._id + "`, `" + json + "`)";

        if (nativeElements != null)
        {
            JsInProcessRuntime.Invoke<object>("igSendMessage", 
                new object[] { 
                    this._containerId, json,
                    GetObjectRef(), nativeElements });
        }
        else
        {
            JsInProcessRuntime.Invoke<object>("igSendMessage", 
                new object[] { 
                    this._containerId, json,
                    GetObjectRef() });
        }
    }

    internal object ReturnToPrimitive(object returnValue) 
    {
        return ConvertReturnValue(returnValue, true);
    }

    internal T[] DowncastArray<T>(object val) 
    {
        if (val == null)
        {
            return null;
        }

        if (val is T[])
        {
            return (T[])val;
        }

        if (val is object[])
        {
            var objArr = (object[])val;
            var ret = new T[objArr.Length];
            for (var i = 0; i < objArr.Length; i++)
            {
                ret[i] = (T)objArr[i];
            }

            return ret;
        }

        return (T[])val;
    }

    internal object ConvertReturnValue(object returnValue, bool transformArrays = false, string typeGuess = null, bool acceptsNullIfMarshalDoesNotExist = false) {
        try {
            //Console.WriteLine(returnValue.GetType().ToString());
            if (returnValue is String || returnValue is Dictionary<string, object> || returnValue is JsonElement) {
                Dictionary<string, object> obj;
                if (returnValue is String)
                {
                    obj = JsonSerializer.Deserialize<Dictionary<string, object>>((string)returnValue, SerializerOptions);
                }
                else if (returnValue is JsonElement)
                {
                    var test = returnValue.ToString();
                    //Console.WriteLine("is ele");
                    obj = new Dictionary<string, object>();
                    var j = ((JsonElement)(returnValue));
                    // if (j.ValueKind == JsonValueKind.String)
                    // {
                    //     var str = j.ToString();
                    //     //Console.WriteLine(j.ToString());
                    //     JsonDocument document = JsonDocument.Parse(str);
                    //     if (document.RootElement.ValueKind == JsonValueKind.Object)
                    //     {
                    //         obj = JsonSerializer.Deserialize<Dictionary<string, object>>((string)j.GetString());
                    //     }
                    //     else
                    //     {
                    //         return str;
                    //     }
                    // } else 
                    if (j.ValueKind == JsonValueKind.String) {
                        returnValue = j.GetString();
                    }
                    else if (j.ValueKind == JsonValueKind.Number) {
                        returnValue = j.GetDouble();
                    } else if (j.ValueKind == JsonValueKind.Array) {
                        var str = j.ToString();
                        //Console.WriteLine(j.ToString());
                        JsonDocument document = JsonDocument.Parse(str);
                        if (document.RootElement.ValueKind == JsonValueKind.Array)
                        {
                            returnValue = JsonSerializer.Deserialize<Dictionary<string, object>[]>((string)j.ToString(), SerializerOptions);
                        }
                        else
                        {
                            return str;
                        }
                    } else if (j.ValueKind == JsonValueKind.Object) {
                        //var str = j.ToString();
                        //Console.WriteLine(j.ToString());
                        obj = JsonSerializer.Deserialize<Dictionary<string, object>>((string)j.ToString(), SerializerOptions);                       
                    }
                }
                else
                {
                    obj = (Dictionary<string, object>)returnValue;
                }
                if (obj.ContainsKey("refType")) {
                    String refType = ((JsonElement)obj["refType"]).ToString();
                    if ("uuid".Equals(refType)) {
                        String id = ((JsonElement)obj["id"]).ToString();
                        if (id.Contains("/")) {
                            returnValue = _dataSourceManager.FindItem(id);
                        } else {
                            Guid uuid = Guid.Parse(id);
                            returnValue = _dataSourceManager.FindItem(uuid);
                        }
                    } else {
                        String name = ((JsonElement)obj["id"]).ToString();
                        returnValue = FindByName(name);
                    }
                } else if (obj.ContainsKey("retType")) {
                    String retType = ((JsonElement)obj["retType"]).ToString();
                    if ("number".Equals(retType)) {
                        if (obj["value"] == null ||
                            ((JsonElement)obj["value"]).ValueKind == JsonValueKind.Null)
                        {
                            returnValue = double.NaN;
                        }
                        else
                        {
                            returnValue = (Object) double.Parse(((JsonElement)obj["value"]).ToString());
                        }
                    } else if ("string".Equals(retType)) {
                        returnValue = (Object) ((JsonElement)obj["value"]).ToString();
                    } else if ("boolean".Equals(retType)) {
                        returnValue = (Object) bool.Parse(((JsonElement)obj["value"]).ToString());
                    } else if ("date".Equals(retType)) {
                        returnValue = (Object) ReturnToDate(((JsonElement)obj["value"]).ToString(), false);
                    } else if ("undefined".Equals(retType)) {
                        returnValue = null;
                    } else if ("Array".Equals(retType)) {
                        var arr = ((JsonElement)obj["value"]);
                        if (transformArrays && arr.ValueKind == JsonValueKind.Array)
                        {
                            object[] ret = new object[arr.GetArrayLength()];
                            for (var i = 0; i < arr.GetArrayLength(); i++)
                            {
                                var item = arr[i];
                                var cItem = ConvertReturnValue(item);
                                ret[i] = cItem;
                            }
                            return ret;
                        }
                        //Console.WriteLine(arr.GetArrayLength());
                        returnValue = arr.ToString();
                    } else if ("localJson".Equals(retType)) {
                        var v = ((JsonElement)obj["value"]);
                        return v;
                    } else if ("object".Equals(retType)) {

                        if (obj.ContainsKey("type")) {
                            var type = obj["type"].ToString();
                            if (String.IsNullOrEmpty(type)) {
                                if (typeGuess != null) {
                                    type = typeGuess;
                                }
                            }
                            if (obj["value"] == null ||
                                ((JsonElement)obj["value"]).ValueKind == JsonValueKind.Null)
                            {
                                return null;
                            }

                            object o = null;
                            if (type != null)
                            {
                                o = MarshalByValueFactory.CreateInstance(type);
                            }
                            if (o != null)
                            {
                                if (o is BaseRendererElement)
                                {
                                    ((BaseRendererElement)o).TempParent = this;
                                    var v = ((JsonElement)obj["value"]);
                                    var str = v.ToString();
                                    //Console.WriteLine(str);
                                    var ev = JsonSerializer.Deserialize<Dictionary<string, object>>(str, SerializerOptions);
                                    ((BaseRendererElement)o).FromEventJson(this, ev);
                                    returnValue = o;
                                }
                                // else if (o is BaseRendererControl)
                                // {
                                //     var ev = JsonSerializer.Deserialize<Dictionary<string, string>>(((JsonElement)obj["value"]).GetString());
                                //     ((BaseRendererControl)o).FromEventJson(this, ev);
                                // }
                                else
                                {
                                    returnValue = ((JsonElement)obj["value"]).ToString();
                                }
                            }
                            else
                            {
                                if (acceptsNullIfMarshalDoesNotExist)
                                {
                                    return null;
                                }
                                var ret = obj["value"].ToString();
                                returnValue = JsonSerializer.Deserialize<Dictionary<string, object>>(ret, SerializerOptions);
                            }
                        } else {
                            returnValue = ((JsonElement)obj["value"]).ToString();
                        }
                    }
                }
            }
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }

        return returnValue;
    }

    public void OnInvokeReturn(long invokeId, Object returnValue) {
        //lock (_semLock) {
        //    if (returnValue instanceof String) {
        //        returnValue = convertReturnValue(returnValue);
        //    }

        //    _methodReturns.Add(invokeId, returnValue);
        //    if (_methodSemaphores.containsKey(invokeId)) {
        //        Semaphore s = _methodSemaphores.get(invokeId);
        //        s.release();
        //    }
        //}

            
        object result = returnValue;
        if (returnValue is JsonElement && ((JsonElement)returnValue).ValueKind == JsonValueKind.String)
        {
            var str = ((JsonElement)returnValue).GetString();
            result = JsonSerializer.Deserialize<Dictionary<string, object>>(str, SerializerOptions);
            
        }
        InvokeAsync(() =>
        {
            if (_methodTasks.ContainsKey(invokeId))
            {
                _methodTasks[invokeId].SetResult(result);
            }
            else
            {
                _methodReturns.Add(invokeId, result);
            }
        });
    }

    internal T ReturnToObject<T>(object val) {
        if (val == null) {
            return default(T);
        }
        val = ConvertReturnValue(val);

        return (T)val;
    }

    public virtual object FindByName(string name) {
        if ("mainControl".Equals(name)) {
            return this;
        }

        return null;
    }

    private object GetObjectById(long objId) {
        //TODO: this
        return null;
    }

    internal int ReturnToInt(object val) {
        if (val == null) {
            return 0;
        }
        val = ConvertReturnValue(val);
        if (val is String) {
            return int.Parse((String)val);
        } else if (val is IConvertible) {
            return ((IConvertible)val).ToInt32(CultureInfo.InvariantCulture);
        } else {
            return int.Parse(val.ToString());
        }
    }

    internal double ReturnToDouble(object val) {
        if (val == null) {
            return 0;
        }
        //Console.WriteLine("converting return");
        val = ConvertReturnValue(val);
        if (val == null) {
            return double.NaN;
        }
        //Console.WriteLine(val);
        if (val is String) {
            return double.Parse((String)val);
        } else if (val is IConvertible) {
            //Console.WriteLine(val);
            return ((IConvertible)val).ToDouble(CultureInfo.InvariantCulture);
        } else {
            //Console.WriteLine(val);
            return Double.Parse(val.ToString());
        }
    }

    internal long ReturnToLong(object val) {
        if (val == null) {
            return 0;
        }
        //Console.WriteLine("converting return");
        val = ConvertReturnValue(val);
        if (val == null) {
            return Int64.MinValue;
        }
        //Console.WriteLine(val);
        if (val is String) {
            return (long)double.Parse((String)val);
        } else if (val is IConvertible) {
            //Console.WriteLine(val);
            return ((IConvertible)val).ToInt64(CultureInfo.InvariantCulture);
        } else {
            //Console.WriteLine(val);
            return (long)Double.Parse(val.ToString());
        }
    }

    internal DateTime[] ReturnToDateArray(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        if (val == null) {
            return null;
        }
        try {
            var arr = JsonSerializer.Deserialize<object[]>((string)val.ToString(), SerializerOptions);
            DateTime[] ret = new DateTime[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                Object ele = arr[i];
                ele = ReturnToDate(ele);
                ret[i] = (DateTime)ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal DateTime ReturnToDate(object val, bool tryConvertValue = true) {
        if (val == null) {
            return DateTime.MinValue;
        }
        //Console.WriteLine("converting return");
        if (tryConvertValue)
        {
            val = ConvertReturnValue(val);
        }
        if (val == null) {
            return DateTime.MinValue;
        }
        //Console.WriteLine(val);
        if (val is String) {
            switch (RoundTripDateConversion)
            {
                case RoundTripDateConversion.UTC:
                    return DateTime.Parse((String)val.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                case RoundTripDateConversion.Auto:
                case RoundTripDateConversion.Local:
                default:
                    return DateTime.Parse((String)val.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).ToLocalTime();
            }
        } else if (val is IConvertible) {
            //Console.WriteLine(val);
            return ((IConvertible)val).ToDateTime(CultureInfo.InvariantCulture);
        } else {
            //Console.WriteLine(val);
            switch (RoundTripDateConversion)
            {
                case RoundTripDateConversion.UTC:
                    return DateTime.Parse((String)val.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                case RoundTripDateConversion.Auto:
                case RoundTripDateConversion.Local:
                default:
                    return DateTime.Parse((String)val.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).ToLocalTime();
            }
        }
    }

    internal bool ReturnToBoolean(object val) {
        if (val == null) {
            return false;
        }
        val = ConvertReturnValue(val);
        if (val is bool) {
            return (bool)val;
        } else if (val is Dictionary<string, object>) {
            return true;
        } else {
            return Boolean.Parse(val.ToString());
        }
    }

    internal string ComponentToJson(object val, int index) {
        if (val is BaseRendererControl || val is BaseRendererElement)
        {
            string refId;
            if (val is BaseRendererElement)
            {
                //TODO: this should be the parent component's _Container id.... but maybe we don't need elements here.
                refId = _containerId + "/" + ((BaseRendererElement)val).Name;
                //((BaseRendererElement)val).Parent = this;
            }
            else
            {
                refId = ((BaseRendererControl)val)._containerId;
                val = "containerId:::" + refId;
                //OnRefChanged(refId, "\"" + val + "\"");
            }
            return "\"" + val + "\"";
        }
        if (val is ElementReference)
        {
            return "\"elementIndex:::" + index + "\"";
        }
        return null;
    }

    internal String BooleanToString(bool val) {
        return ((bool)val).ToString().ToLower();
    }

    internal string ObjectToParam(object val) {
        using (MemoryStream ms = new MemoryStream())
        {
            using (Utf8JsonWriter w = new Utf8JsonWriter(ms))
            {
                SerializationContext c = new SerializationContext(w, null);
                ObjectToParam(c, val);
                w.Flush();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray()); 
            }
        }
    }

    internal string ObjectToParam(object val, Type type)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using (Utf8JsonWriter w = new Utf8JsonWriter(ms))
            {
                SerializationContext c = new SerializationContext(w, null);
                ObjectToParam(c, type, val);
                w.Flush();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray()); 
            }
        }
    }

    internal void ObjectToParam(SerializationContext context, object val) {
        if (val == null)
        {
            context.Writer.WriteNullValue();
            return;
        }
        var w = context.Writer;
        Guid id = _dataSourceManager.FindItemId(val);

        var typeName = "";

        if (val is JsonSerializable)
        {
            
            if (val is BaseRendererControl)
            {
                typeName = ((BaseRendererControl)val).Type;
            }
            else if (val is BaseRendererElement)
            {
                typeName = ((BaseRendererElement)val).Type;
            }
            else
            {
                typeName = val.GetType().Name;
            }
        }

        if (id != Guid.Empty) {
            w.WriteStartObject();
            w.WriteString("refType", "uuid");
            w.WriteString("id", id.ToString());
            w.WriteEndObject();
        }
        else if (val is JsonSerializable && MarshalByValueFactory.MustMarshalByValue(typeName))
        {
            ((JsonSerializable)(val)).Serialize(context);
        }
        else if (val is BaseRendererElement) {
            w.WriteStartObject();
            w.WriteString("refType", "name");
            w.WriteString("id", ((BaseRendererElement) val).Name);
            w.WriteEndObject();
        }
        else if (val is BaseRendererControl) {
             w.WriteStartObject();
            w.WriteString("refType", "name");
            w.WriteString("id", ((BaseRendererControl) val).Name);
            w.WriteEndObject();
        }
        else if (val is double)
        {
            w.WriteNumberValue((double)val);
        }
        else if (val is int)
        {
            w.WriteNumberValue((int)val);
        }
        else if (val is long)
        {
            w.WriteNumberValue((long)val);
        }
        else if (val is short)
        {
            w.WriteNumberValue((long)val);
        }
        else if (val is bool)
        {
            w.WriteBooleanValue((bool)val);
        }
        else if (val is DateTime)
        {
            w.WriteStringValue(((DateTime)val).ToString("o"));
        }
        else
        {
            w.WriteStringValue(val.ToString());
        }
    }
    internal void ObjectToParam(SerializationContext c, string propertyName, object val) {
        var w = c.Writer;
        if (val == null)
        {
            w.WriteNull(propertyName);
            return;
        }
        Guid id = _dataSourceManager.FindItemId(val);

        string typeName = "";
        if (val is JsonSerializable)
        {
            
            if (val is BaseRendererControl)
            {
                typeName = ((BaseRendererControl)val).Type;
            }
            else if (val is BaseRendererElement)
            {
                typeName = ((BaseRendererElement)val).Type;
            }
            else
            {
                typeName = val.GetType().Name;
            }
        }

        if (id != Guid.Empty) {
            w.WriteStartObject(propertyName);
            w.WriteString("refType", "uuid");
            w.WriteString("id", id.ToString());
            w.WriteEndObject();
        }
        else if (val is JsonSerializable && MarshalByValueFactory.MustMarshalByValue(typeName))
        {
            ((JsonSerializable)(val)).Serialize(c);
        }
        else if (val is BaseRendererElement) {
            w.WriteStartObject(propertyName);
            w.WriteString("refType", "name");
            w.WriteString("id", ((BaseRendererElement) val).Name);
            w.WriteEndObject();
        }
        else if (val is BaseRendererControl) {
             w.WriteStartObject(propertyName);
            w.WriteString("refType", "name");
            w.WriteString("id", ((BaseRendererControl) val).Name);
            w.WriteEndObject();
        }
        else if (val is double)
        {
            w.WriteNumber(propertyName, (double)val);
        }
        else if (val is int)
        {
            w.WriteNumber(propertyName, (int)val);
        }
        else if (val is long)
        {
            w.WriteNumber(propertyName, (long)val);
        }
        else if (val is short)
        {
            w.WriteNumber(propertyName, (long)val);
        }
        else if (val is bool)
        {
            w.WriteBoolean(propertyName, (bool)val);
        }
        else if (val is DateTime)
        {
            w.WriteString(propertyName, ((DateTime)val).ToString("o"));
        }
        else
        {
            w.WriteString(propertyName, val.ToString());
        }
    }
    internal void ObjectToParam(SerializationContext c, Type type, object val)
    {
        if (type.IsEnum)
        {
            foreach (var f in type.GetFields())
            {
                if (f.IsPublic && !f.IsSpecialName && f.Name == val.ToString())
                {
                    foreach (var attr in f.GetCustomAttributes(true))
                    {
                        if (attr.GetType().Name == "WCEnumNameAttribute")
                        {
                            var wc = (WCEnumNameAttribute)attr;
                            c.Writer.WriteStringValue(wc.Name);
                            return;
                        }
                    }
                }
            }

            if (UseCamelEnumValues)
            {
                c.Writer.WriteStringValue(Camelize(val.ToString()));
            }
            else
            {
                c.Writer.WriteStringValue(val.ToString());
            }
            return;
        }
        ObjectToParam(c, val);
    }

    internal string ReturnToString(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);

        if (val == null) {
            return null;
        }
        return val.ToString();
    }

    internal string StringToString(object val) {
        return val == null ?  null : JsonSerializer.Serialize(val.ToString(), SerializerOptions);
        //return val == null ? null : val.ToString();
    }

    protected virtual bool UseCamelEnumValues
    {
        get
        {
            return true;
        }
    }

    protected string Camelize(string value)
    {
        if (value == null || value.Length == 0) {
            return value;
        }
        return value.Substring(0, 1).ToLower() + value.Substring(1);
    }

    protected string ToPascal(string value)
    {
        if (value == null || value.Length == 0) {
            return value;
        }
        return value.Substring(0, 1).ToUpper() + value.Substring(1);
    }

    internal string EnumToString<T>(T val) where T: struct {
        if (UseCamelEnumValues)
        {
            return Camelize(val.ToString());
        }

        return val.ToString();
    }

    internal T StringToEnum<T>(Object val) where T: struct {
        if (val == null)
        {
            return default(T);
        }
        val = ConvertReturnValue(val);
        if (val == null) {
            return default(T);
        }
        var str = val.ToString();
        T ret;
        if (Enum.TryParse<T>(str, out ret)) {
            return ret;
        }
        return default(T);
    }

    internal string ObjectArrayToParam(object[] arr) {
        if (arr == null)
        {
            return null;
        }
        using (MemoryStream ms = new MemoryStream())
        {
            using (Utf8JsonWriter w = new Utf8JsonWriter(ms))
            {
                SerializationContext c = new SerializationContext(w, null);
                w.WriteStartArray();
                for (int i = 0; i < arr.Length; i++) {
                    var val = arr[i];
                    ObjectToParam(c, val);
                }
                w.WriteEndArray();
                w.Flush();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray()); 
            }
        }
        // object jarr = new JSONArray();
        // try {
        //     for (int i = 0; i < arr.length; i++) {
        //         jarr.put(i, arr[i]);
        //     }
        // } catch (Exception e) {
        //     return null;
        // }
        // return jarr.toString();
        // try
        // {
        //     return JsonSerializer.Serialize(arr);
        // }
        // catch (Exception e)
        // {
        //     return null;
        // }
    }

    internal string StringArrayToString(string[] arr) {
        // object jarr = new JSONArray();
        // try {
        //     for (int i = 0; i < arr.length; i++) {
        //         jarr.put(i, arr[i]);
        //     }
        // } catch (Exception e) {
        //     return null;
        // }
        // return jarr.toString();
        try
        {
            return JsonSerializer.Serialize(arr, SerializerOptions);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    internal string IntArrayToString(int[] arr) {
        // object jarr = new JSONArray();
        // try {
        //     for (int i = 0; i < arr.length; i++) {
        //         jarr.put(i, arr[i]);
        //     }
        // } catch (Exception e) {
        //     return null;
        // }
        // return jarr.toString();
        try
        {
            return JsonSerializer.Serialize(arr, SerializerOptions);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    internal string DoubleArrayToString(double[] arr) {
        // object jarr = new JSONArray();
        // try {
        //     for (int i = 0; i < arr.length; i++) {
        //         jarr.put(i, arr[i]);
        //     }
        // } catch (Exception e) {
        //     return null;
        // }
        // return jarr.toString();
        try
        {
            return JsonSerializer.Serialize(arr, SerializerOptions);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    internal object[] ReturnToObjectArray(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        if (val == null) {
            return null;
        }
        try {
            var arr = JsonSerializer.Deserialize<object[]>((string)val.ToString(), SerializerOptions);
            Object[] ret = new Object[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                Object ele = arr[i];
                ele = ConvertReturnValue(ele);
                ret[i] = ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal T[] ReturnToObjectArray<T>(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        try {
            var arr = JsonSerializer.Deserialize<Dictionary<string, object>[]>((string)val.ToString(), SerializerOptions);
            T[] ret = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                Object ele = arr[i];
                //Console.WriteLine("converting obj");
                //Console.WriteLine(ele);
                ele = ConvertReturnValue(ele);
                ret[i] = (T)ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal string[] ReturnToStringArray(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        if (val == null) {
            return null;
        }
        try {
            var valStr = val.ToString();
            var arr = JsonSerializer.Deserialize<string[]>((string)valStr, SerializerOptions);
            string[] ret = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                string ele = arr[i] != null ? arr[i].ToString() : null;
                ret[i] = ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal double[] ReturnToDoubleArray(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        try {
            var arr = JsonSerializer.Deserialize<object[]>((string)val.ToString(), SerializerOptions);
            double[] ret = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                double ele = arr[i] != null ? Convert.ToDouble(arr[i]) : double.NaN;
                ret[i] = ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal int[] ReturnToIntArray(object val) {
        if (val == null) {
            return null;
        }
        val = ConvertReturnValue(val);
        try {
            var arr = JsonSerializer.Deserialize<object[]>((string)val.ToString(), SerializerOptions);
            int[] ret = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                int ele = arr[i] != null ? Convert.ToInt32(arr[i]) : int.MinValue;
                ret[i] = ele;
            }
            return ret;
        } catch (Exception e) {
            return null;
        }
    }

    internal string Name
    {
        get
        {
            return "mainControl";
        }
    }

    protected internal void OnElementNameChanged(BaseRendererElement element, string oldName, string newName)
    {
        List<string> toRename = new List<string>();
        foreach (var key in _handlers.Keys)
        {
            if (key.Contains("/"))
            {
                var parts = key.Split('/');
                if (parts[0] == oldName) 
                {
                    toRename.Add(key);
                }
            }
        }

        foreach (var key in toRename)
        {
            var handler = _handlers[key];
            _handlers.Remove(key);
            var parts = key.Split('/');
            var newKey = newName + "/" + parts[1];
            _handlers[newKey] = handler;
        }
    }

    internal void SetHandler<T>(string name, string propertyName, EventCallback<T>? handler, Action<T> onArgs = null) where T: BaseRendererElement, new() {
        if (!handler.HasValue)
        {
            //Console.WriteLine("clearing handler: " + name + "/" + propertyName);
            _handlers.Remove(name + "/" + propertyName);
            return;
        }
        Action<object, object> inner = (sender, args) => {
            T a = new T();
            BaseRendererElement ele = (BaseRendererElement)a;
            ele.Parent = this;
            ele.FromEventJson(this, (Dictionary<string, object>)args);
             //Console.WriteLine("invoking async");
             if (onArgs != null)
             {
                onArgs(a);
             }
            var task = handler?.InvokeAsync(a);
            if (task.Exception != null)
            {
                throw task.Exception;
            }
            ele.ToEventJson(this, (Dictionary<string, object>)args);
            ele.Parent = (null);
        };

        //Console.WriteLine("setting handler: " + name + "/" + propertyName);
        _handlers[name + "/" + propertyName] = inner;
    }

    internal void SetHandlerSimple<T>(string name, string propertyName, EventCallback<T>? handler, Func<object, T> getReturn, Action<T> onArgs = null) {
        if (!handler.HasValue)
        {
            //Console.WriteLine("clearing handler: " + name + "/" + propertyName);
            _handlers.Remove(name + "/" + propertyName);
            return;
        }
        Action<object, object> inner = (sender, args) => {
            T a = getReturn(args);
             //Console.WriteLine("invoking async");
             if (onArgs != null)
             {
                onArgs(a);
             }
            var task = handler?.InvokeAsync(a);
            if (task.Exception != null)
            {
                throw task.Exception;
            }
        };

        //Console.WriteLine("setting handler: " + name + "/" + propertyName);
        _handlers[name + "/" + propertyName] = inner;
    }


    internal void SetActionHandler<T>(string name, string propertyName, Action<T> handler, Action<T> onArgs = null) where T: BaseRendererElement, new() {
        if (handler == null)
        {
            //Console.WriteLine("clearing handler: " + name + "/" + propertyName);
            _handlers.Remove(name + "/" + propertyName);
            return;
        }
        Action<object, object> inner = (sender, args) => {
            T a = new T();
            BaseRendererElement ele = (BaseRendererElement)a;
            ele.Parent = this;
            ele.FromEventJson(this, (Dictionary<string, object>)args);
             //Console.WriteLine("invoking async");
             if (onArgs != null)
             {
                onArgs(a);
             }
            handler(a);
            ele.ToEventJson(this, (Dictionary<string, object>)args);
            ele.Parent = (null);
        };

        //Console.WriteLine("setting handler: " + name + "/" + propertyName);
        _handlers[name + "/" + propertyName] = inner;
    }

    internal void SetActionHandlerSimple<T>(string name, string propertyName, Action<T> handler, Func<object, T> getReturn, Action<T> onArgs = null) {
        if (handler == null)
        {
            //Console.WriteLine("clearing handler: " + name + "/" + propertyName);
            _handlers.Remove(name + "/" + propertyName);
            return;
        }
        Action<object, object> inner = (sender, args) => {
            T a = getReturn(args);
            
            //Console.WriteLine("invoking async");
            if (onArgs != null)
            {
            onArgs(a);
            }
            handler(a);          
        };

        //Console.WriteLine("setting handler: " + name + "/" + propertyName);
        _handlers[name + "/" + propertyName] = inner;
    }

    internal void OnRaiseEvent(string name, string propertyName, string args) {
        //Console.WriteLine("handler time: " + Name + ", " + GetHashCode().ToString() + ", " + this.GetType().Name);
        //Console.WriteLine("getting handler: " + name + "/" + propertyName);
        //Console.WriteLine("_handlers: " + _handlers.Count);
        if (_handlers.ContainsKey(name + "/" + propertyName)) {
            //Console.WriteLine("got handler");
            bool usedTempParent = false;
            Object senderObj = null;
            try {
                var obj = JsonSerializer.Deserialize<Dictionary<string, object>>((string)args.ToString(), SerializerOptions);
                //Console.WriteLine(args.ToString());

                object sender = obj["sender"];
                if (sender is JsonElement && ((JsonElement)sender).ValueKind == JsonValueKind.String)
                {
                    sender = JsonSerializer.Deserialize<Dictionary<string, object>>(((JsonElement)sender).GetString(), SerializerOptions);
                }

                senderObj = ConvertReturnValue(sender);
                if (senderObj is BaseRendererElement)
                {
                    var ele = (BaseRendererElement)senderObj;
                    if (ele.Parent == null)
                    {
                        ele.TempParent = this;
                    }
                }

                var argsString = obj["args"] != null ? obj["args"].ToString() : null;
                object val = null;
                
                if (argsString != null)
                {
                    var doc = JsonDocument.Parse(argsString);
                    //Console.WriteLine(argsString);
                    //Console.WriteLine("here:");
                    //Console.WriteLine(doc.RootElement.ValueKind.ToString());
                    if (doc.RootElement.ValueKind == JsonValueKind.Object)
                    {
                        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>((string)argsString, SerializerOptions);
                        val = dict;

                        // Avoid double unwrapping primitive value types. This primarily only applies to events with primitive types for event args which
                        // there are not many of.
                        bool needsConversion = true;
                        if (dict.ContainsKey("retType"))
                        {
                            var retType = ((JsonElement)dict["retType"]).ToString();
                            if ("string".Equals(retType) ||
                                "number".Equals(retType) ||
                                "boolean".Equals(retType) ||
                                "date".Equals(retType))
                            {
                                needsConversion = false;
                            }
                        }

                        if (needsConversion)
                            val = ConvertReturnValue(val);
                    }
                    else
                    {
                        val = JsonSerializer.Deserialize<object>((string)argsString, SerializerOptions);
                    }
                }
                //Console.WriteLine("calling handler");
                _handlers[name + "/" + propertyName](senderObj, val);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            } finally {
                if (senderObj is BaseRendererElement)
                {
                    var ele = (BaseRendererElement)senderObj;
                    if (ele.Parent == null)
                    {
                        ele.TempParent = null;
                    }
                }
            }
        }
    }
    private bool _shouldReevaluateRuntime = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            _shouldReevaluateRuntime = true;
            SendCleanupMessage();
            _shouldReevaluateRuntime = false;

            if (disposing && _objRef != null)
            {
                _objRef.Dispose();
            }

            disposedValue = true;
        }
    }

    private void SendCleanupMessage()
    {
        RendererMessage m = new RendererMessage();
        m.Type = ("cleanup");
        
        _messageQueue.Clear();
        var ret = SendMessageImmediate(m);
    }

    public async Task<object> SetResourceStringAsync(string grouping, string id, string value)
    {
        if (!IgBlazor.IsRuntimeValid(_shouldReevaluateRuntime)) {
            return null;
        }
        return await JsRuntime.InvokeAsync<object>("igSetResourceString", new object[] { "set", grouping, id, value });
    }

    public async Task<object> SetResourceStringAsync(string grouping, string json)
    {
        if (!IgBlazor.IsRuntimeValid(_shouldReevaluateRuntime)) {
            return null;
        }
        return await JsRuntime.InvokeAsync<object>("igSetResourceString", new object[] { "register", grouping, "", json });
    }

    protected void SetPropertyValue(object item, System.Reflection.PropertyInfo property, JsonElement jsonElement)
    {
        System.Type type = Nullable.GetUnderlyingType(property.PropertyType);
        if (type == null)
        {
            type = property.PropertyType;
        }
        switch (System.Type.GetTypeCode(type))
        {
            case TypeCode.Byte: property.SetValue(item, jsonElement.GetByte()); break;
            case TypeCode.SByte: property.SetValue(item, jsonElement.GetSByte()); break;
            case TypeCode.UInt16: property.SetValue(item, jsonElement.GetUInt16()); break;
            case TypeCode.UInt32: property.SetValue(item, jsonElement.GetUInt32()); break;
            case TypeCode.UInt64: property.SetValue(item, jsonElement.GetUInt64()); break;
            case TypeCode.Int16: property.SetValue(item, jsonElement.GetInt16()); break;
            case TypeCode.Int32: property.SetValue(item, jsonElement.GetInt32()); break;
            case TypeCode.Int64: property.SetValue(item, jsonElement.GetInt64()); break;
            case TypeCode.Decimal: property.SetValue(item, jsonElement.GetDecimal()); break;
            case TypeCode.Double: property.SetValue(item, jsonElement.GetDouble()); break;
            case TypeCode.Single: property.SetValue(item, jsonElement.GetSingle()); break;
            case TypeCode.String: property.SetValue(item, jsonElement.GetString()); break;
            case TypeCode.Boolean: property.SetValue(item, jsonElement.GetBoolean()); break;
            case TypeCode.DateTime: property.SetValue(item, ReturnToDate(jsonElement.ToString())); break;
        }
    }
    protected void SetPropertyValue(object item, System.Reflection.PropertyInfo property, object value)
    {
        System.Type type = Nullable.GetUnderlyingType(property.PropertyType);
        if (type == null)
        {
            type = property.PropertyType;
        }
        else
        {
            if (value == null)
            {
                property.SetValue(item, null);
                return;
            }
        }
        if (type.IsArray)
        {
            var src = (Array)value;
            var dest = Array.CreateInstance(type.GetElementType(), src.Length);
            Array.Copy(src, dest, src.Length);
            property.SetValue(item, dest);
            return;
        }

        switch (System.Type.GetTypeCode(type))
        {
            case TypeCode.Byte: property.SetValue(item, Convert.ToByte(value)); break;
            case TypeCode.SByte: property.SetValue(item, Convert.ToSByte(value)); break;
            case TypeCode.UInt16: property.SetValue(item, Convert.ToUInt16(value)); break;
            case TypeCode.UInt32: property.SetValue(item, Convert.ToUInt32(value)); break;
            case TypeCode.UInt64: property.SetValue(item, Convert.ToUInt64(value)); break;
            case TypeCode.Int16: property.SetValue(item, Convert.ToInt16(value)); break;
            case TypeCode.Int32: property.SetValue(item, Convert.ToInt32(value)); break;
            case TypeCode.Int64: property.SetValue(item, Convert.ToInt64(value)); break;
            case TypeCode.Decimal: property.SetValue(item, Convert.ToDecimal(value)); break;
            case TypeCode.Double: property.SetValue(item, Convert.ToDouble(value)); break;
            case TypeCode.Single: property.SetValue(item, Convert.ToSingle(value)); break;
            case TypeCode.String: property.SetValue(item, Convert.ToString(value)); break;
            case TypeCode.Boolean: property.SetValue(item, Convert.ToBoolean(value)); break;
            case TypeCode.DateTime: property.SetValue(item, Convert.ToDateTime(value)); break;
            case TypeCode.Object: property.SetValue(item, value); break;
        }
    }

    /**
     * Workaround for comparing EventCallbacks correctly. It has been fixed only in .net 9 sadly. See: https://github.com/dotnet/aspnetcore/issues/53361
     * Basically access the Delegate and Receiver property that is not public for each callback and evaluate them manually.
     */
    public static bool CompareEventCallbacks<T>(T left, T right, ref Dictionary<Type, Dictionary<string, FieldInfo>>  eventFieldsDictionary)
    {
        if (left.Equals(null) || right.Equals(null))
        {
            return false;
        }
        if (left.GetHashCode() == right.GetHashCode() || left.Equals(right))
        {
            return true;
        }

        Dictionary<string, FieldInfo> eventFields;
        Type leftType = left.GetType();
        if (!eventFieldsDictionary.TryGetValue(leftType, out eventFields))
        {
            eventFields = new Dictionary<string, FieldInfo> {
                { "Delegate", leftType.GetField("Delegate", BindingFlags.NonPublic | BindingFlags.Instance) },
                { "Receiver", leftType.GetField("Receiver", BindingFlags.NonPublic | BindingFlags.Instance) }
            };
            eventFieldsDictionary.Add(leftType, eventFields);
        }

        Delegate leftDelegate = (Delegate)(eventFields["Delegate"].GetValue(left));
        Delegate rightDelegate = (Delegate)(eventFields["Delegate"].GetValue(right));
        IHandleEvent leftHandle = (IHandleEvent)(eventFields["Receiver"].GetValue(left));
        IHandleEvent rightHandle = (IHandleEvent)(eventFields["Receiver"].GetValue(right));
        return leftDelegate.Equals(rightDelegate) && leftHandle.Equals(rightHandle);
    }

    ~BaseRendererControl()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

//
// Summary:
//     This mirrors the options for System.Text.Json.JsonSerializer that we allow for customization for the component serialization.
public class IgniteUIJsonSerializerOptions
{
    public IgniteUIJsonSerializerOptions()
    {
        MaxDepth = 32;
    }

    public IgniteUIJsonSerializerOptions(int maxDepth)
    {
        MaxDepth = maxDepth;
    }

    public int MaxDepth { get; private set; }

    public IgniteUIJsonSerializerOptions(IgniteUIJsonSerializerOptions options)
    {
        MaxDepth = options.MaxDepth;
    }
}

public interface IIgniteUIBlazorSettings
{
    bool ForceJsonDataMarshalling { get; }
    IgniteUIJsonSerializerOptions JsonSerializerOptions { get; }
    ReadOnlyCollection<Type> ModulesToLoad { get; }
}

public class IgniteUIBlazorSettings
    : IIgniteUIBlazorSettings
{
    public bool ForceJsonDataMarshalling { get; private set; }
    public IgniteUIJsonSerializerOptions JsonSerializerOptions { get; private set; }
    public ReadOnlyCollection<Type> ModulesToLoad { get; private set; }

    public IgniteUIBlazorSettings()
    {
        ForceJsonDataMarshalling = false;
        JsonSerializerOptions = new IgniteUIJsonSerializerOptions();
        ModulesToLoad = null;
    }

    public static IgniteUIBlazorSettings Create() 
    {
        return new IgniteUIBlazorSettings();
    }

    public IgniteUIBlazorSettings ShouldForceJsonDataMarshalling()
    {
        var newSettings = new IgniteUIBlazorSettings(this);
        newSettings.ForceJsonDataMarshalling = true;
        return newSettings;
    }

    public IgniteUIBlazorSettings WithForceJsonDataMarshalling(bool forceJsonDataMarshalling)
    {
        var newSettings = new IgniteUIBlazorSettings(this);
        newSettings.ForceJsonDataMarshalling = forceJsonDataMarshalling;
        return newSettings;
    }

    public IgniteUIBlazorSettings WithJsonSerializerOptions(IgniteUIJsonSerializerOptions options)
    {
        var newSettings = new IgniteUIBlazorSettings(this);
        newSettings.JsonSerializerOptions = new IgniteUIJsonSerializerOptions(options);
        return newSettings;
    }

    public IgniteUIBlazorSettings WithModulesToLoad(ReadOnlyCollection<Type> modulesToLoad)
    {
        var newSettings = new IgniteUIBlazorSettings(this);
        newSettings.ModulesToLoad = modulesToLoad;
        return newSettings;
    }

    internal IgniteUIBlazorSettings(IIgniteUIBlazorSettings settings)
    {
        ForceJsonDataMarshalling = settings.ForceJsonDataMarshalling;
        JsonSerializerOptions = new IgniteUIJsonSerializerOptions(settings.JsonSerializerOptions);
        ModulesToLoad = settings.ModulesToLoad;
    }
}

public interface IIgniteUIBlazor {
    IJSRuntime JsRuntime { get; }
    IIgniteUIBlazorSettings Settings { get; }
    WebCallback WebCallback { get; }
    void RequestLoad(string moduleName);
    bool IsLoadRequested(string moduleName);
    void MarkIsLoadRequested(string moduleName);
    bool IsRuntimeValid(bool reevaluate = false);
}

public class IgniteUIBlazor: IIgniteUIBlazor {
    private bool _isRuntimeValid = false;
    private bool _isRuntimeChecked = false;
    private bool _isRemoteRuntime = false;
    private System.Reflection.PropertyInfo _remoteRuntimeProp;

    public IgniteUIBlazor(IJSRuntime runtime, IIgniteUIBlazorSettings settings)
    {
        JsRuntime = runtime;
        Settings = settings;
        WebCallback = new WebCallback();

        if (IsRuntimeValid())
        {
            if (Settings != null)
            {
                if (Settings.ModulesToLoad != null)
                {
                    foreach (var type in Settings.ModulesToLoad)
                    {
                        var meth = type.GetMethod("Register", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                        if (meth != null)
                        {
                            var parms = meth.GetParameters();
                            if (parms.Length == 1 && typeof(IIgniteUIBlazor).IsAssignableFrom(parms[0].ParameterType))
                            {
                                meth.Invoke(null, new object[] { this });
                            }
                        }
                    }
                }
            }
        }
    }

    public IgniteUIBlazor(IJSRuntime runtime)
    {
        JsRuntime = runtime;
        Settings = new IgniteUIBlazorSettings();
        WebCallback = new WebCallback();
    }

    public IJSRuntime JsRuntime { get; private set; }
    public IIgniteUIBlazorSettings Settings { get; private set; }
    public WebCallback WebCallback { get; private set; }

    private ConcurrentDictionary<string, bool> _loadedCache = new ConcurrentDictionary<string, bool>();
    public void RequestLoad(string moduleName)
    {
        if (!IsRuntimeValid() || _loadedCache.ContainsKey(moduleName))
        {
            return;
        }
        _loadedCache.AddOrUpdate(moduleName, true, (name, oldValue) => true);
        JsRuntime.InvokeAsync<object>("igRequestLoad", moduleName);
    }
    public bool IsLoadRequested(string moduleName)
    {
        if (_loadedCache.ContainsKey(moduleName))
        {
            return true;
        }
        return false;
    }

    public void MarkIsLoadRequested(string moduleName)
    {
        if (!IsRuntimeValid() || _loadedCache.ContainsKey(moduleName))
        {
            return;
        }
        _loadedCache.AddOrUpdate(moduleName, true, (name, oldValue) => true);
    }

    public bool IsRuntimeValid(bool reevaluate = false)
    {
        if (JsRuntime == null)
            return false;

        if (!_isRuntimeChecked)
        {
            var t = JsRuntime.GetType();
            _remoteRuntimeProp = t.GetProperty("IsInitialized");
            if (t.Name == "UnsupportedJavaScriptRuntime")
            {
                _isRuntimeValid = false;
            }
            else if (_remoteRuntimeProp != null)
            {
                _isRuntimeValid = (bool)_remoteRuntimeProp.GetValue(JsRuntime);
                _isRemoteRuntime = true;
            }
            else
            {
                _isRuntimeValid = true;
            }

            _isRuntimeChecked = true;
        }
        else
        {
            if (reevaluate && _isRemoteRuntime)
            {
                _isRuntimeValid = (bool)_remoteRuntimeProp.GetValue(JsRuntime);
            }
        }
        return _isRuntimeValid;
    }
}

public class SequenceInfo
{
    private ReadOnlyCollection<string> _attributeKeys = null;
    public ReadOnlyCollection<string> AttributeKeys
    {
        get
        {
            if (_attributeKeys == null ||
            _dirty)
            {
                _dirty = false;
                _attributeKeys = new ReadOnlyCollection<string>(_keysList);
            }
            return _attributeKeys;
        }
    }
    public int MaxSequence { get { return _currSequence; } }

    private Dictionary<string, int> SequenceMap { get; set; }
    private int _currSequence = 0;
    private bool _dirty = true;

    internal SequenceInfo(int startSequence)
    {
        _currSequence = startSequence;
        SequenceMap = new Dictionary<string, int>();
    }

    internal string TransformKey(string attributeKey)
    {
        if (_transforms.ContainsKey(attributeKey))
        {
            return _transforms[attributeKey];
        }
        return attributeKey;
    }

    internal bool IsTransformedEnum(string attributeKey) 
    {
        if (_enumTransforms.ContainsKey(attributeKey))
        {
            return true;
        }
        return false;
    }

    internal string TransformEnumValue(string attributeKey, string fieldName)
    {
        if (_enumTransforms.ContainsKey(attributeKey))
        {
            var d = _enumTransforms[attributeKey];
            if (d.ContainsKey(fieldName.ToLower())) {
                return d[fieldName.ToLower()];
            }
        }
        return fieldName;
    }

    internal void AddSequence(string attributeKey, string wcName = null, Dictionary<string, string> wcEnumTransform = null)
    {
        if (!_keysSet.Contains(attributeKey))
        {
            _dirty = true;
            _keysSet.Add(attributeKey);
            SequenceMap.Add(attributeKey, _currSequence);
            _currSequence++;
            _keysList.Add(attributeKey);

            if (wcName != null)
            {
                _transforms[attributeKey] = wcName;
            }
            if (wcEnumTransform != null)
            {
                _enumTransforms[attributeKey] = wcEnumTransform;
            }
        }
    }

    private List<string> _keysList = new List<string>();
    private HashSet<string> _keysSet = new HashSet<string>();

    private Dictionary<string, string> _transforms = new Dictionary<string, string>();
    private Dictionary<string, Dictionary<string, string>> _enumTransforms = new Dictionary<string, Dictionary<string, string>>();

    public int GetSequence(string key)
    {
        if (_keysSet.Contains(key))
        {
            return SequenceMap[key];
        }
        return -1;
    }
}

public class ModuleLoader 
{
    public static void Load(IIgniteUIBlazor runtime, string moduleName)
    {
        runtime.RequestLoad(moduleName);
        //runtime.JsRuntime.InvokeAsync<object>("igRequestLoad", moduleName);
    }

    public static void MarkIsLoadRequested(IIgniteUIBlazor runtime, string moduleName) {
        runtime.MarkIsLoadRequested(moduleName);
    }

    public static bool IsLoadRequested(IIgniteUIBlazor runtime, string moduleName)
    {
        return runtime.IsLoadRequested(moduleName);
    }
}
    /// <summary>
    /// Enum defining different round trip date conversions.
    /// </summary>
    public enum RoundTripDateConversion
    {
        /// <summary>
        /// The component will decide how to convert round tripped dates.
        /// </summary>
        Auto,

        /// <summary>
        /// The component will convert round tripped dates to UTC.
        /// </summary>
        UTC,

        /// <summary>
        /// The component will convert round tripped dates to local time.
        /// </summary>
        Local
    }

}