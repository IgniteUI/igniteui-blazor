using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Reflection;

namespace IgniteUI.Blazor.Controls
{

    public partial class BaseRendererElement : ComponentBase, JsonSerializable 
    {
        // public BaseRendererElement()
        // {
        //     Console.WriteLine("constructing: " + this.GetType().Name);
        // }

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
                
                EnsureModulesLoaded();
            }
        }
        protected virtual void EnsureModulesLoaded()
        {
            //Console.WriteLine("ensuring element modules loaded");
        }

        public bool IsComponentRooted
        {
            get
            {
                if (Parent is BaseRendererControl)
                {
                    return true;
                }
                if (Parent == null)
                {
                    return false;
                }
                return ((BaseRendererElement)Parent).IsComponentRooted;
            }
        }

        internal void AttachChild(BaseRendererElement child) {
            if (child == null)
            {
                return;
            }
            if (IsComponentRooted)
            {
                child.Parent = this;
            }
            else
            {
                if (child.Parent == null)
                {
                    child.Parent = this;
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
        

        protected virtual string ParentTypeName
        {
            get
            {
                return null;
            }
        }

        protected virtual bool UseDirectRender
        {
            get
            {
                return false;
            }
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected virtual bool SupportsVisualChildren 
        {
            get
            {
                return false;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ParentTypeName != null) 
            {
                if (!SupportsVisualChildren) 
                {
                    builder.OpenComponent<CascadingValue<BaseRendererElement>>(0);
                    builder.AddAttribute(1, "Value", this);
                    builder.AddAttribute(2, "Name", ParentTypeName);
                    builder.AddAttribute(3, "ChildContent", (RenderFragment)delegate(RenderTreeBuilder builder2)
                    {
                        builder2.AddMarkupContent(4, "\r\n        ");
                        builder2.AddContent(5, ChildContent);
                        builder2.AddMarkupContent(6, "\r\n    ");
                    });
                    builder.CloseComponent();
                }

                if (SupportsVisualChildren && Parent != null)
                {
                    var currParent = Parent;
                    while (currParent != null && !(currParent is BaseRendererControl))
                    {
                        currParent = ((BaseRendererElement)currParent).Parent;
                    }
                    if (currParent != null)
                    {
                        var parentId = ((BaseRendererControl)currParent).ContainerId;
                        builder.OpenComponent<CascadingValue<BaseRendererElement>>(10);
                        builder.AddAttribute(1, "Value", this);
                        builder.AddAttribute(2, "Name", ParentTypeName);
                        builder.AddAttribute(3, "ChildContent", (RenderFragment)delegate(RenderTreeBuilder builder2)
                        {
                            builder2.AddMarkupContent(4, "\r\n        ");
                            builder2.OpenElement(5, "igc-portal-entrance");
                            builder2.AddAttribute(6, "portal-id", "portal-" + parentId + "/" + Name);
                            builder2.AddAttribute(7, "move-once-mode", "true");
                            builder2.AddContent(8, ChildContent);
                            builder2.CloseElement();
                            builder2.AddMarkupContent(9, "\r\n    ");
                        });
                        builder.CloseComponent();
                    }
                }
            }
        }

        private Dictionary<string, bool> _isDirty = new Dictionary<string, bool>();
        private Dictionary<string, bool> _isDirtyRef = new Dictionary<string, bool>();

        private bool _hasDirty = false;
        private bool _serializeDirty = false;

        protected string _name = Guid.NewGuid().ToString();

        /**
         * Cache the delegate and receiver field of each EventCallback type for increased performance when comparing.
         */
        protected Dictionary<Type, Dictionary<string, FieldInfo>> eventCallbacksCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();

        [Parameter]
        public string Name 
        {
            set 
            {
                var oldName = _name;
                _name = value;
                OnElementNameChanged(this, oldName, _name);
            }
            get 
            {
                return _name;
            }
        }

        protected void OnElementNameChanged(BaseRendererElement element, string oldName, string newName)
        {
            if (CurrParent != null)
            {
                if (CurrParent is BaseRendererElement) {
                    ((BaseRendererElement)CurrParent).OnElementNameChanged(element, oldName, newName);
                } else {
                    ((BaseRendererControl)CurrParent).OnElementNameChanged(element, oldName, newName);
                }
            } else {
                _deferredNameChanges.Add(() => {
                    if (CurrParent is BaseRendererElement) {
                        ((BaseRendererElement)CurrParent).OnElementNameChanged(element, oldName, newName);
                    } else {
                        ((BaseRendererControl)CurrParent).OnElementNameChanged(element, oldName, newName);
                    }
                });
            }
        }

        private object _tempParent = null;
        internal object TempParent
        {
            get
            {
                return _tempParent;
            }
            set
            {
                _tempParent = value;
            }
        }

        private Object _parent = null;


        private class RefChange {
            public String propertyName;
            public Object oldValue;
            public Object newValue;
            public Action<string, object, object> refChanged;
            public bool isScript;
            public bool isElement;
        }
        private LinkedList<RefChange> _queuedChanges = new LinkedList<RefChange>();
        private List<Action> _queuedTemplateUpdates = new List<Action>();
        private List<Action> _deferredNameChanges = new List<Action>();

        private void QueueRefChange(String propertyName, Object oldValue, Object newValue, bool isScript, bool isElement, Action<string, object, object> refChanged) {
            RefChange c = new RefChange();
            c.propertyName = propertyName;
            c.oldValue = oldValue;
            c.newValue = newValue;
            c.refChanged = refChanged;
            c.isScript = isScript;
            c.isElement = isElement;
            _queuedChanges.AddLast(c);
        }

        private void FlushRefs() {
            while (_queuedChanges.Count > 0) {
                RefChange c = _queuedChanges.First.Value;
                _queuedChanges.RemoveFirst();
                OnRefChanged(c.propertyName, c.oldValue, c.newValue, c.isScript, c.isElement, c.refChanged);
            }
        }

        public object Parent
        {
            get
            {
                return _parent;
            }
            internal set 
            {
                 Object oldParent = _parent;
                _parent = value;
                _serializeDirty = true;
                if (_parent != null) {
                    FlushRefs();
                    if (_deferredHandlers.Count > 0) 
                    {
                        foreach(var handler in _deferredHandlers)
                        {
                            handler();
                        }
                        _deferredHandlers.Clear();
                    }
                    if (_deferredNameChanges.Count > 0)
                    {
                        foreach(var handler in _deferredNameChanges)
                        {
                            handler();
                        }
                        _deferredNameChanges.Clear();
                    }
                    if (_queuedTemplateUpdates.Count > 0)
                    {
                        foreach(var template in _queuedTemplateUpdates)
                        {
                            template();
                        }
                        _queuedTemplateUpdates.Clear();
                    }
                }
            }
        }

        void ChildDirty(object child) {
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null) {
                if (_parent is BaseRendererControl) {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                } else {
                    ((BaseRendererElement)_parent).ChildDirty(this);
                }
            }
        }

        protected virtual string MethodTarget
        {
            get
            {
                return Name;
            }
        }

        protected async Task<object> InvokeMethod(string methodName, object[] arguments, string[] types, ElementReference[] nativeElements = null) {
            return await InvokeMethodHelper(MethodTarget, methodName, arguments, types, nativeElements);
        }

         protected object InvokeMethodSync(string methodName, object[] arguments, string[] types, ElementReference[] nativeElements = null) {
            return InvokeMethodHelperSync(MethodTarget, methodName, arguments, types, nativeElements);
        }

        protected async Task<object> InvokeMethodHelper(string target, string methodName, object[] arguments, string[] types, ElementReference[] nativeElements) {
            if (CurrParent == null) {
                throw new InvalidOperationException("cannot invoke method if not attached to parent.");
            }
            if (CurrParent is BaseRendererElement) {
                return await ((BaseRendererElement)CurrParent).InvokeMethodHelper(target, methodName, arguments, types, nativeElements);
            } else {
                return await ((BaseRendererControl)CurrParent).InvokeMethodHelper(target, methodName, arguments, types, nativeElements);
            }
        }

        protected object InvokeMethodHelperSync(string target, string methodName, object[] arguments, string[] types, ElementReference[] nativeElements) {
            if (CurrParent == null) {
                throw new InvalidOperationException("cannot invoke method if not attached to parent.");
            }
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement)CurrParent).InvokeMethodHelperSync(target, methodName, arguments, types, nativeElements);
            } else {
                return ((BaseRendererControl)CurrParent).InvokeMethodHelperSync(target, methodName, arguments, types, nativeElements);
            }
        }

        internal void OnPropertyPropagatedOut(string name, string propertyName)
        {
            if (CurrParent == null) {
                throw new InvalidOperationException("cannot invoke method if not attached to parent.");
            }
            if (CurrParent is BaseRendererElement) {
                ((BaseRendererElement)CurrParent).OnPropertyPropagatedOut(name, propertyName);
            } else {
                ((BaseRendererControl)CurrParent).OnPropertyPropagatedOut(name, propertyName);
            }
        }

        internal void UpdateTemplate(string contentType, object template, Type type)
        {
            Action templateUpdate = () => {
                if (_parent is BaseRendererControl) {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                    ((BaseRendererControl)_parent).UpdateTemplate(contentType, template, type);
                } else {
                    ((BaseRendererElement)_parent).ChildDirty(this);
                    ((BaseRendererElement)_parent).UpdateTemplate(contentType, template, type);
                }
            };
            if (_parent != null) {
                templateUpdate();
            } else {
                _queuedTemplateUpdates.Add(templateUpdate);
            }
        }

        internal void OnRefChanged(string propertyName, object oldValue, object newValue, bool isScript, bool isElement, Action<string, object, object> refChanged) {
            _isDirtyRef[propertyName] = true;
            _isDirty[propertyName] = true;
            _hasDirty = true;
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null) {
                if (_parent is BaseRendererControl) {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                    ((BaseRendererControl)_parent).OnRefChanged(_name + "/" + propertyName, oldValue, newValue, isScript, isElement, refChanged);
                } else {
                    ((BaseRendererElement)_parent).ChildDirty(this);
                    ((BaseRendererElement)_parent).OnRefChanged(_name + "/" + propertyName, oldValue, newValue, isScript, isElement, refChanged);
                }
            } else {
                QueueRefChange(propertyName, oldValue, newValue, isScript, isElement, refChanged);
            }
        }

        private bool _suppressParentNotify = false;
        internal bool SuppressParentNotify
        {
            get
            {
                return _suppressParentNotify;
            }
            set
            {
                _suppressParentNotify = value;
            }
        }

        internal void MarkPropDirty(String propertyName) {
            _isDirty[propertyName] = true;
            _hasDirty = true;
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null) {
                if (_parent is BaseRendererControl) {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                } else {
                    ((BaseRendererElement)_parent).ChildDirty(this);
                }
            }
        }

        protected bool IsPropDirty(string propertyName) {
            if (_isDirty.ContainsKey(propertyName)) {
                return _isDirty[propertyName];
            }

            return false;
        }

        private bool _checkedByVal = false;
        private bool _mustSerializeByValue = false;
        internal bool MustSerializeByValue
        {
            get
            {
                if (!_checkedByVal)
                {
                    _mustSerializeByValue = MarshalByValueFactory.MustMarshalByValue(this.Type);
                    _checkedByVal = true;
                }
                return _mustSerializeByValue;
            }
        }


        internal virtual void SerializeCore(RendererSerializer ser) {
            ser.AddStringProp("name", _name);
            if (MustSerializeByValue) {
                ser.AddBooleanProp("___byValue", true);
            }
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

        public void Serialize(SerializationContext context, string propertyName = null)
        {
            RendererSerializer ser = new RendererSerializer(context, this, Name);
            ser.Type = Type;
            ser.Start(propertyName);
            SerializeCore(ser);
            ser.End();
        }

        public string Serialize() {
            if (_serializeDirty) {
                using(var stream = new System.IO.MemoryStream())
                {
                    System.Text.Json.Utf8JsonWriter uw = new System.Text.Json.Utf8JsonWriter(stream);
                    SerializationContext c = new SerializationContext(uw, null);
                    //RendererSerializer ser = new RendererSerializer(uw);
                    
                    Serialize(c);
                    uw.Flush();
                    _cachedSerializedContent = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                }
                _serializeDirty = false;
            }
            return _cachedSerializedContent;
        }

        protected void EnsureValid() {
            if (_parent == null && _tempParent == null) {
                throw new InvalidOperationException("must be attached to parent to do this.");
            }
        }

        protected object CurrParent
        {
            get
            {
                if (_parent != null)
                {
                    return _parent;
                }
                return _tempParent;
            }
        } 

        internal T ReturnToObject<T>(Object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToObject<T>(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToObject<T>(val);
            }
        }

        internal int ReturnToInt(Object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToInt(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToInt(val);
            }
        }

        internal double ReturnToDouble(Object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToDouble(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToDouble(val);
            }
        }

        internal long ReturnToLong(Object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToLong(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToLong(val);
            }
        }

        internal DateTime ReturnToDate(Object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToDate(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToDate(val);
            }
        }

        internal String ComponentToJson(object val, int index) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ComponentToJson(val, index);
            } else {
                return ((BaseRendererControl) CurrParent).ComponentToJson(val, index);
            }
        }

        internal string DateToString(DateTime val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).DateToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).DateToString(val);
            }
        }

        internal string BooleanToString(bool val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).BooleanToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).BooleanToString(val);
            }
        }

        internal string EnumToString<T>(T val) where T: struct {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).EnumToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).EnumToString(val);
            }
        }

        internal T StringToEnum<T>(Object val) where T: struct {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).StringToEnum<T>(val);
            } else {
                return ((BaseRendererControl) CurrParent).StringToEnum<T>(val);
            }
        }

    internal string ObjectArrayToParam(object[] arr) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ObjectArrayToParam(arr);
        } else {
            return ((BaseRendererControl) CurrParent).ObjectArrayToParam(arr);
        }
    }

    internal object[] ReturnToObjectArray(Object val) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ReturnToObjectArray(val);
        } else {
            return ((BaseRendererControl) CurrParent).ReturnToObjectArray(val);
        }
    }

    internal T[] ReturnToObjectArray<T>(Object val) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ReturnToObjectArray<T>(val);
        } else {
            return ((BaseRendererControl) CurrParent).ReturnToObjectArray<T>(val);
        }
    }

    internal string[] ReturnToStringArray(Object val) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ReturnToStringArray(val);
        } else {
            return ((BaseRendererControl) CurrParent).ReturnToStringArray(val);
        }
    }

    internal int[] ReturnToIntArray(Object val) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ReturnToIntArray(val);
        } else {
            return ((BaseRendererControl) CurrParent).ReturnToIntArray(val);
        }
    }

    internal double[] ReturnToDoubleArray(Object val) {
        EnsureValid();
        if (CurrParent is BaseRendererElement) {
            return ((BaseRendererElement) CurrParent).ReturnToDoubleArray(val);
        } else {
            return ((BaseRendererControl) CurrParent).ReturnToDoubleArray(val);
        }
    }

        internal string ObjectToParam(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ObjectToParam(val);
            } else {
                return ((BaseRendererControl) CurrParent).ObjectToParam(val);
            }
        }

        internal string ObjectToParam(object val, Type type) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ObjectToParam(val, type);
            } else {
                return ((BaseRendererControl) CurrParent).ObjectToParam(val, type);
            }
        }

        internal void ObjectToParam(SerializationContext c, string propertyName, object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                 ((BaseRendererElement) CurrParent).ObjectToParam(c, propertyName, val);
            } else {
                 ((BaseRendererControl) CurrParent).ObjectToParam(c, propertyName, val);
            }
        }

        internal void ObjectToParam(SerializationContext c, object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                 ((BaseRendererElement) CurrParent).ObjectToParam(c, val);
            } else {
                 ((BaseRendererControl) CurrParent).ObjectToParam(c, val);
            }
        }

        internal string ReturnToString(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToString(val);
            }
        }

        internal bool ReturnToBoolean(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToBoolean(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToBoolean(val);
            }
        }

        internal object ConvertReturnValue(object val, string typeGuess = null, bool acceptsNullIfMarshalDoesNotExist = false) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ConvertReturnValue(val, typeGuess, acceptsNullIfMarshalDoesNotExist);
            } else {
                return ((BaseRendererControl) CurrParent).ConvertReturnValue(val, false, typeGuess, acceptsNullIfMarshalDoesNotExist);
            }
        }

        internal object ReturnToPrimitive(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).ReturnToPrimitive(val);
            } else {
                return ((BaseRendererControl) CurrParent).ReturnToPrimitive(val);
            }
        }

         internal T[] DowncastArray<T>(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).DowncastArray<T>(val);
            } else {
                return ((BaseRendererControl) CurrParent).DowncastArray<T>(val);
            }
        }

        private List<Action> _deferredHandlers = new List<Action>();

        internal void SetHandler<T>(string name, string propertyName, EventCallback<T>? handler, Action<T> onArgs = null) where T: BaseRendererElement, new() {
            Action add = () => {
                if (CurrParent is BaseRendererElement) {
                ((BaseRendererElement) CurrParent).SetHandler(name, propertyName, handler, onArgs);
                } else {
                    ((BaseRendererControl) CurrParent).SetHandler(name, propertyName, handler, onArgs);
                }
            };
            
            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
        }

        internal void SetHandlerSimple<T>(string name, string propertyName, EventCallback<T>? handler, Func<object, T> getReturn, Action<T> onArgs = null) {
            Action add = () => {
                if (CurrParent is BaseRendererElement) {
                ((BaseRendererElement) CurrParent).SetHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                } else {
                    ((BaseRendererControl) CurrParent).SetHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
            };
            
            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
        }

        internal void SetActionHandler<T>(string name, string propertyName, Action<T> handler, Action<T> onArgs = null) where T: BaseRendererElement, new() {
            Action add = () => {
                if (CurrParent is BaseRendererElement) {
                ((BaseRendererElement) CurrParent).SetActionHandler(name, propertyName, handler, onArgs);
                } else {
                    ((BaseRendererControl) CurrParent).SetActionHandler(name, propertyName, handler, onArgs);
                }
            };
            
            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
            
        }

        internal void SetActionHandlerSimple<T>(string name, string propertyName, Action<T> handler, Func<object, T> getReturn, Action<T> onArgs = null) {
            Action add = () => {
                if (CurrParent is BaseRendererElement) {
                ((BaseRendererElement) CurrParent).SetActionHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                } else {
                    ((BaseRendererControl) CurrParent).SetActionHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
            };
            
            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();  
        }

        internal string StringToString(object val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).StringToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).StringToString(val);
            }
        }

        internal string StringArrayToString(string[] val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).StringArrayToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).StringArrayToString(val);
            }
        }

        internal string IntArrayToString(int[] val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).IntArrayToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).IntArrayToString(val);
            }
        }

        internal string DoubleArrayToString(double[] val) {
            EnsureValid();
            if (CurrParent is BaseRendererElement) {
                return ((BaseRendererElement) CurrParent).DoubleArrayToString(val);
            } else {
                return ((BaseRendererControl) CurrParent).DoubleArrayToString(val);
            }
        }


        protected internal virtual void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {

        }
        protected internal virtual void ToEventJson(BaseRendererControl control, Dictionary<string, object> args) {

        }

        public virtual object FindByName(string name)
	    {
	
	        return null;
	    }

        protected async Task<object> SetResourceStringAsync(string grouping, string id, string value) {
            if (CurrParent == null) {
                throw new InvalidOperationException("cannot set resource strings if not attached to parent.");
            }
            if (CurrParent is BaseRendererElement) {
                return await ((BaseRendererElement)CurrParent).SetResourceStringAsync(grouping, id, value);
            } else {
                return await ((BaseRendererControl)CurrParent).SetResourceStringAsync(grouping, id, value);
            }
        }
        protected async Task<object> SetResourceStringAsync(string grouping, string json) {
            if (CurrParent == null) {
                throw new InvalidOperationException("cannot set resource strings if not attached to parent.");
            }
            if (CurrParent is BaseRendererElement) {
                return await ((BaseRendererElement)CurrParent).SetResourceStringAsync(grouping, json);
            } else {
                return await ((BaseRendererControl)CurrParent).SetResourceStringAsync(grouping, json);
            }
        }

        /**
         * Workaround for comparing EventCallbacks correctly. It has been fixed only in .net 9 sadly. See: https://github.com/dotnet/aspnetcore/issues/53361
         * Basically access the Delegate and Receiver property that is not public for each callback and evaluate them manually.
         */
        protected static bool CompareEventCallbacks<T>(T left, T right, ref Dictionary<Type, Dictionary<string, FieldInfo>> eventFieldsDictionary)
        {
            return BaseRendererControl.CompareEventCallbacks(left, right, ref eventFieldsDictionary);
        }
    }

}