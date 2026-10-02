using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{

    /// <summary>
    /// Shared internal Json serializable handling.
    /// </summary>
    public partial class BaseJsonSerializable : JsonSerializable
    {
        internal bool IsComponentRooted
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
                return ((BaseJsonSerializable)Parent).IsComponentRooted;
            }
        }

        internal void AttachChild(BaseJsonSerializable child)
        {
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
        internal void DetachChild(BaseJsonSerializable child)
        {
            if (child == null)
            {
                return;
            }
            if (child.Parent == this)
            {
                child.Parent = null;
            }
        }

        private Dictionary<string, bool> _isDirty = new Dictionary<string, bool>();
        private Dictionary<string, bool> _isDirtyRef = new Dictionary<string, bool>();

        private bool _serializeDirty = false;

        private protected string _name = Guid.NewGuid().ToString();

        /// <summary>The name the client renderer resolves this element by; generated unless set.</summary>
        internal string RendererName
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

        internal void OnElementNameChanged(BaseJsonSerializable element, string oldName, string newName)
        {
            if (CurrParent != null)
            {
                if (CurrParent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)CurrParent).OnElementNameChanged(element, oldName, newName);
                }
                else if (CurrParent is BaseRendererControl)
                {
                    ((BaseRendererControl)CurrParent).OnElementNameChanged(element, oldName, newName);
                }
            }
            else
            {
                _deferredNameChanges.Add(() =>
                {
                    if (CurrParent is BaseJsonSerializable)
                    {
                        ((BaseJsonSerializable)CurrParent).OnElementNameChanged(element, oldName, newName);
                    }
                    else if (CurrParent is BaseRendererControl)
                    {
                        ((BaseRendererControl)CurrParent).OnElementNameChanged(element, oldName, newName);
                    }
                });
            }
        }

        private object? _tempParent = null;
        internal object? TempParent
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

        private Object? _parent = null;

        private class RefChange
        {
            public String propertyName = string.Empty;
            public Object? oldValue;
            public Object? newValue;
            public Action<string, object?, object?>? refChanged = null;
            public bool isScript;
            public bool isElement;
        }
        private LinkedList<RefChange> _queuedChanges = new LinkedList<RefChange>();
        private List<Action> _queuedTemplateUpdates = new List<Action>();
        private List<Action> _deferredNameChanges = new List<Action>();

        private void QueueRefChange(String propertyName, Object? oldValue, Object? newValue, bool isScript, bool isElement, Action<string, object?, object?> refChanged)
        {
            RefChange c = new RefChange();
            c.propertyName = propertyName;
            c.oldValue = oldValue;
            c.newValue = newValue;
            c.refChanged = refChanged;
            c.isScript = isScript;
            c.isElement = isElement;
            _queuedChanges.AddLast(c);
        }

        private void FlushRefs()
        {
            while (_queuedChanges != null && _queuedChanges.Count > 0)
            {
                RefChange? c = _queuedChanges.First?.Value;
                _queuedChanges.RemoveFirst();
                if (c != null && c.refChanged != null)
                {
                    OnRefChanged(c.propertyName, c.oldValue, c.newValue, c.isScript, c.isElement, c.refChanged);
                }
            }
        }

        internal object? Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                Object? oldParent = _parent;
                _parent = value;
                _serializeDirty = true;
                if (_parent != null)
                {
                    FlushRefs();
                    if (_deferredHandlers.Count > 0)
                    {
                        foreach (var handler in _deferredHandlers)
                        {
                            handler();
                        }
                        _deferredHandlers.Clear();
                    }
                    if (_deferredNameChanges.Count > 0)
                    {
                        foreach (var handler in _deferredNameChanges)
                        {
                            handler();
                        }
                        _deferredNameChanges.Clear();
                    }
                    if (_queuedTemplateUpdates.Count > 0)
                    {
                        foreach (var template in _queuedTemplateUpdates)
                        {
                            template();
                        }
                        _queuedTemplateUpdates.Clear();
                    }
                }
            }
        }

        void ChildDirty(object child)
        {
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null)
            {
                if (_parent is BaseRendererControl)
                {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                }
                else
                {
                    ((BaseJsonSerializable)_parent).ChildDirty(this);
                }
            }
        }

        internal void OnPropertyPropagatedOut(string name, string propertyName)
        {
            if (CurrParent == null)
            {
                throw new InvalidOperationException("cannot invoke method if not attached to parent.");
            }
            if (CurrParent is BaseJsonSerializable)
            {
                ((BaseJsonSerializable)CurrParent).OnPropertyPropagatedOut(name, propertyName);
            }
            else
            {
                ((BaseRendererControl)CurrParent).OnPropertyPropagatedOut(name, propertyName);
            }
        }

        internal void UpdateTemplate(string contentType, object? template, Type type)
        {
            Action templateUpdate = () =>
            {
                if (_parent is BaseRendererControl)
                {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                    ((BaseRendererControl)_parent).UpdateTemplate(contentType, template, type);
                }
                else if (_parent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)_parent).ChildDirty(this);
                    ((BaseJsonSerializable)_parent).UpdateTemplate(contentType, template, type);
                }
            };
            if (_parent != null)
            {
                templateUpdate();
            }
            else
            {
                _queuedTemplateUpdates.Add(templateUpdate);
            }
        }

        internal void OnRefChanged(string propertyName, object? oldValue, object? newValue, bool isScript, bool isElement, Action<string, object?, object?> refChanged)
        {
            _isDirtyRef[propertyName] = true;
            _isDirty[propertyName] = true;
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null)
            {
                if (_parent is BaseRendererControl)
                {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                    ((BaseRendererControl)_parent).OnRefChanged(_name + "/" + propertyName, oldValue, newValue, isScript, isElement, refChanged);
                }
                else
                {
                    ((BaseJsonSerializable)_parent).ChildDirty(this);
                    ((BaseJsonSerializable)_parent).OnRefChanged(_name + "/" + propertyName, oldValue, newValue, isScript, isElement, refChanged);
                }
            }
            else
            {
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

        /// <summary>Marks <paramref name="propertyName"/> as changed so the next render sends it to the client.</summary>
        internal void MarkPropDirty(String? propertyName)
        {
            if (propertyName == null)
            {
                return;
            }
            _isDirty[propertyName] = true;
            _serializeDirty = true;
            if (_suppressParentNotify)
            {
                return;
            }
            if (_parent != null)
            {
                if (_parent is BaseRendererControl)
                {
                    ((BaseRendererControl)_parent).ChildDirty(this);
                }
                else
                {
                    ((BaseJsonSerializable)_parent).ChildDirty(this);
                }
            }
        }

        /// <summary>Whether <paramref name="propertyName"/> changed since the component last sent its properties to the client.</summary>
        private protected bool IsPropDirty(string propertyName)
        {
            if (_isDirty.ContainsKey(propertyName))
            {
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
                    _mustSerializeByValue = MarshalByValueFactory.MustMarshalByValue(this.RendererType);
                    _checkedByVal = true;
                }
                return _mustSerializeByValue;
            }
        }

        internal virtual void SerializeCore(RendererSerializer ser)
        {
            ser.AddStringProp("name", _name);
            if (MustSerializeByValue)
            {
                ser.AddBooleanProp("___byValue", true);
            }
        }

        private String _cachedSerializedContent = "";

        /// <summary>The type name of this element.</summary>
        internal virtual string RendererType
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

        void JsonSerializable.Serialize(SerializationContext context, string? propertyName) => Serialize(context, propertyName);

        internal void Serialize(SerializationContext context, string? propertyName = null)
        {
            RendererSerializer ser = new RendererSerializer(context, this, RendererName);
            ser.Type = RendererType;
            ser.Start(propertyName);
            SerializeCore(ser);
            ser.End();
        }

        internal string Serialize()
        {
            if (_serializeDirty)
            {
                using (var stream = new System.IO.MemoryStream())
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

        private void EnsureValid()
        {
            if (_parent == null && _tempParent == null)
            {
                throw new InvalidOperationException("must be attached to parent to do this.");
            }
        }

        internal object? CurrParent
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

        internal T? ReturnToObject<T>(Object val)
        {
            return ReturnToObject<T>(val, null);
        }

        internal T? ReturnToObject<T>(Object val, string? typeGuess)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToObject<T>(val, typeGuess);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToObject<T>(val, typeGuess);
            }
            return default(T);
        }

        internal int ReturnToInt(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToInt(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToInt(val);
            }
            return default(int);
        }

        internal double ReturnToDouble(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToDouble(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToDouble(val);
            }
            return default(double);
        }

        internal long ReturnToLong(Object val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToLong(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToLong(val);
            }
            return default(long);
        }

        internal DateTime ReturnToDate(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToDate(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToDate(val);
            }
            return default(DateTime);
        }

        internal String? ComponentToJson(object val, int index)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ComponentToJson(val, index);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ComponentToJson(val, index);
            }
            return default(string);
        }

        internal string DateToString(DateTime val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).DateToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).DateToString(val);
            }
            return String.Empty;
        }

        internal string BooleanToString(bool val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).BooleanToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).BooleanToString(val);
            }
            return String.Empty;
        }

        internal string? EnumToString<T>(T val) where T : struct
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).EnumToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).EnumToString(val);
            }
            return default(string);
        }

        internal T StringToEnum<T>(Object? val) where T : struct
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).StringToEnum<T>(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).StringToEnum<T>(val);
            }
            return default(T);
        }

        internal string? ObjectArrayToParam(object[]? arr)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ObjectArrayToParam(arr);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ObjectArrayToParam(arr);
            }
            return default(string);
        }

        internal object[] ReturnToObjectArray(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToObjectArray(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToObjectArray(val);
            }
            return Array.Empty<object>();
        }

        internal T[]? ReturnToObjectArray<T>(Object? val)
        {
            return ReturnToObjectArray<T>(val, null);
        }
        internal T[]? ReturnToObjectArray<T>(Object? val, string? typeGuess)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToObjectArray<T>(val, typeGuess);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToObjectArray<T>(val, typeGuess);
            }
            return default;
        }

        internal string[]? ReturnToStringArray(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToStringArray(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToStringArray(val);
            }
            return default;
        }

        internal int[]? ReturnToIntArray(Object val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToIntArray(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToIntArray(val);
            }
            return default;
        }

        internal double[]? ReturnToDoubleArray(Object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToDoubleArray(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToDoubleArray(val);
            }
            return default;
        }

        internal string ObjectToParam(object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ObjectToParam(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ObjectToParam(val);
            }
            return String.Empty;
        }

        internal string ObjectToParam(object? val, Type type)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ObjectToParam(val, type);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ObjectToParam(val, type);
            }
            return String.Empty;
        }

        internal void ObjectToParam(SerializationContext c, string propertyName, object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                ((BaseJsonSerializable)CurrParent).ObjectToParam(c, propertyName, val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                ((BaseRendererControl)CurrParent).ObjectToParam(c, propertyName, val);
            }
        }

        internal void ObjectToParam(SerializationContext? c, object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                ((BaseJsonSerializable)CurrParent).ObjectToParam(c, val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                ((BaseRendererControl)CurrParent).ObjectToParam(c, val);
            }
        }

        internal string ReturnToString(object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToString(val);
            }
            return String.Empty;
        }

        internal bool ReturnToBoolean(object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToBoolean(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToBoolean(val);
            }
            return default;
        }

        internal object? ConvertReturnValue(object? val, string? typeGuess = null, bool acceptsNullIfMarshalDoesNotExist = false)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ConvertReturnValue(val, typeGuess, acceptsNullIfMarshalDoesNotExist);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ConvertReturnValue(val, false, typeGuess, acceptsNullIfMarshalDoesNotExist);
            }
            return null;
        }

        internal object? ReturnToPrimitive(object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).ReturnToPrimitive(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).ReturnToPrimitive(val);
            }
            return null;
        }

        internal T[]? DowncastArray<T>(object val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).DowncastArray<T>(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).DowncastArray<T>(val);
            }
            return default;
        }

        private List<Action> _deferredHandlers = new List<Action>();

        internal void SetHandler<T>(string name, string propertyName, EventCallback<T>? handler, Action<T>? onArgs = null) where T : BaseJsonSerializable, new()
        {
            Action add = () =>
            {
                if (CurrParent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)CurrParent).SetHandler(name, propertyName, handler, onArgs);
                }
                else if (CurrParent is BaseRendererControl)
                {
                    ((BaseRendererControl)CurrParent).SetHandler(name, propertyName, handler, onArgs);
                }
            };

            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
        }

        internal void SetHandlerSimple<T>(string name, string propertyName, EventCallback<T>? handler, Func<object, T> getReturn, Action<T>? onArgs = null)
        {
            Action add = () =>
            {
                if (CurrParent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)CurrParent).SetHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
                else if (CurrParent is BaseRendererControl)
                {
                    ((BaseRendererControl)CurrParent).SetHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
            };

            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
        }

        internal void SetActionHandler<T>(string name, string propertyName, Action<T> handler, Action<T>? onArgs = null) where T : BaseJsonSerializable, new()
        {
            Action add = () =>
            {
                if (CurrParent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)CurrParent).SetActionHandler(name, propertyName, handler, onArgs);
                }
                else if (CurrParent is BaseRendererControl)
                {
                    ((BaseRendererControl)CurrParent).SetActionHandler(name, propertyName, handler, onArgs);
                }
            };

            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();

        }

        internal void SetActionHandlerSimple<T>(string name, string propertyName, Action<T> handler, Func<object, T> getReturn, Action<T>? onArgs = null)
        {
            Action add = () =>
            {
                if (CurrParent is BaseJsonSerializable)
                {
                    ((BaseJsonSerializable)CurrParent).SetActionHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
                else if (CurrParent is BaseRendererControl)
                {
                    ((BaseRendererControl)CurrParent).SetActionHandlerSimple(name, propertyName, handler, getReturn, onArgs);
                }
            };

            if (_parent == null)
            {
                _deferredHandlers.Add(add);
                return;
            }
            add();
        }

        internal string? StringToString(object? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).StringToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).StringToString(val);
            }
            return default;
        }

        internal string? StringArrayToString(string[]? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).StringArrayToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).StringArrayToString(val);
            }
            return default;
        }

        internal string? IntArrayToString(int[]? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).IntArrayToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).IntArrayToString(val);
            }
            return default;
        }

        internal string? DoubleArrayToString(double[]? val)
        {
            EnsureValid();
            if (CurrParent is BaseJsonSerializable)
            {
                return ((BaseJsonSerializable)CurrParent).DoubleArrayToString(val);
            }
            else if (CurrParent is BaseRendererControl)
            {
                return ((BaseRendererControl)CurrParent).DoubleArrayToString(val);
            }
            return default;
        }

        /// <summary>Reads this element's values from the event payload the client sent for <paramref name="control"/>.</summary>
        internal virtual void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {

        }

        /// <summary>Writes this element's values into the event payload returned to the client for <paramref name="control"/>.</summary>
        internal virtual void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {

        }

        /// <summary>Resolves <paramref name="name"/> to the child element it identifies, or <c>null</c>.</summary>
        internal virtual object? FindByName(string name)
        {

            return null;
        }

        private protected async Task<object?> SetResourceStringAsync(string grouping, string id, string value)
        {
            if (CurrParent == null)
            {
                throw new InvalidOperationException("cannot set resource strings if not attached to parent.");
            }
            if (CurrParent is BaseJsonSerializable)
            {
                return await ((BaseJsonSerializable)CurrParent).SetResourceStringAsync(grouping, id, value);
            }
            else
            {
                return await ((BaseRendererControl)CurrParent).SetResourceStringAsync(grouping, id, value);
            }
        }
        private protected async Task<object?> SetResourceStringAsync(string grouping, string json)
        {
            if (CurrParent == null)
            {
                throw new InvalidOperationException("cannot set resource strings if not attached to parent.");
            }
            if (CurrParent is BaseJsonSerializable)
            {
                return await ((BaseJsonSerializable)CurrParent).SetResourceStringAsync(grouping, json);
            }
            else
            {
                return await ((BaseRendererControl)CurrParent).SetResourceStringAsync(grouping, json);
            }
        }

    }

}
