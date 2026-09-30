using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IgniteUI.Blazor.Controls
{

    /// <summary>
    /// Base of the collections that hold a component's child objects and report changes to the component that owns them.
    /// Infrastructure for Ignite UI component libraries built on this package; not intended for application code.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class BaseCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotify = false;

        internal bool SuppressNofify
        {
            get
            {
                return _suppressNotify;
            }
            set
            {
                _suppressNotify = value;
            }
        }

        internal BaseCollection<T> Fill(T[] items)
        {
            _suppressNotify = true;
            Clear();
            if (items != null)
            {
                for (var i = 0; i < items.Length; i++)
                {
                    Add(items[i]);
                }
            }
            _suppressNotify = false;
            return this;
        }

        /// <summary>Copies the items to a new array.</summary>
        /// <returns>An array with the items, in order.</returns>
        public T[] ToArray()
        {
            var array = new T[Count];
            this.CopyTo(array, 0);
            return array;
        }

        /// <inheritdoc />
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (item is BaseRendererElement element)
            {
                element.Parent = _parent;
            }
            NotifyParent();
        }

        /// <inheritdoc />
        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            if (item is BaseRendererElement element)
            {
                element.Parent = null;
            }
            NotifyParent();
        }

        /// <inheritdoc />
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            if (item is BaseRendererElement element)
            {
                element.Parent = _parent;
            }
            NotifyParent();
        }

        internal object? Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        private object? _parent = null;
        private string? _propertyName = null;
        internal string? PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
            }
        }

        /// <summary>Creates a collection owned by <paramref name="parent"/>, reported to it as the <paramref name="propertyName"/> property.</summary>
        /// <param name="parent">The component or element that owns the collection, or <see langword="null"/>.</param>
        /// <param name="propertyName">The name of the owner's property that holds the collection.</param>
        public BaseCollection(object? parent, string? propertyName)
        {
            _parent = parent;
            _propertyName = propertyName;
        }

        private void NotifyParent()
        {
            if (_suppressNotify)
            {
                return;
            }
            if (_parent == null)
            {
                return;
            }
            if (_parent is BaseRendererElement)
            {
                ((BaseRendererElement)_parent).MarkPropDirty(_propertyName);
            }
            if (_parent is BaseRendererControl)
            {
                ((BaseRendererControl)_parent).MarkPropDirty(_propertyName);
            }
        }

        /// <inheritdoc />
        protected override void ClearItems()
        {
            for (var i = 0; i < Count; i++)
            {
                var item = this[i];
                if (item is BaseRendererElement element)
                {
                    element.Parent = null;
                }
            }
            base.ClearItems();
            NotifyParent();
        }

        /// <summary>Writes the items as a JSON array into the description sent to the client renderer.</summary>
        /// <param name="context">The serialization context to write to.</param>
        /// <param name="propertyName">The property name to write the array under, or <see langword="null"/> to write a bare array.</param>
        public void Serialize(SerializationContext context, string? propertyName = null)
        {
            //var vals = new List<string>();
            if (propertyName != null)
            {
                context.Writer.WriteStartArray(propertyName);
            }
            else
            {
                context.Writer.WriteStartArray();
            }
            for (var i = 0; i < Count; i++)
            {
                var val = this[i];
                if (val is null)
                {
                    context.Writer.WriteNullValue();
                }
                else if (val is JsonSerializable serializable)
                {
                    serializable.Serialize(context);
                }
                else if (val is int intValue)
                {
                    context.Writer.WriteNumberValue(intValue);
                }
                else if (val is long longValue)
                {
                    context.Writer.WriteNumberValue(longValue);
                }
                else if (val is short shortValue)
                {
                    context.Writer.WriteNumberValue(shortValue);
                }
                else if (val is decimal decimalValue)
                {
                    context.Writer.WriteNumberValue(decimalValue);
                }
                else if (val is float floatValue)
                {
                    context.Writer.WriteNumberValue(floatValue);
                }
                else if (val is double doubleValue)
                {
                    context.Writer.WriteNumberValue(doubleValue);
                }
                else if (val is byte byteValue)
                {
                    context.Writer.WriteNumberValue(byteValue);
                }
                else if (val is string stringValue)
                {
                    context.Writer.WriteStringValue(stringValue);
                }
                else
                {
                    if (_parent is BaseRendererElement parentElement)
                    {
                        parentElement.ObjectToParam(context, val);
                    }
                    if (_parent is BaseRendererControl parentControl)
                    {
                        parentControl.ObjectToParam(context, val);
                    }
                }
            }
            context.Writer.WriteEndArray();
            //return "[" + string.Join(", \n", vals) + "]";
        }

        /// <summary>Finds the item, or a descendant of an item, with the given renderer name.</summary>
        /// <param name="name">The renderer name to look for.</param>
        /// <returns>The matching object, or <see langword="null"/>.</returns>
        public object? FindByName(string name)
        {
            //TODO: hash map
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                if (item is BaseRendererElement ele)
                {
                    if (name == ele.Name)
                    {
                        return item;
                    }
                    var subEle = ele.FindByName(name);
                    if (subEle is BaseRendererElement childElement && name == childElement.Name)
                    {
                        return childElement;
                    }
                }
                else if (item is BaseRendererControl element)
                {
                    if (name == element.ContainerId)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        /// <summary>Determines whether an item, or a descendant of an item, has the given renderer name.</summary>
        /// <param name="name">The renderer name to look for.</param>
        /// <returns><see langword="true"/> when a match exists; otherwise <see langword="false"/>.</returns>
        public bool HasName(string name)
        {
            //TODO: hash map
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                if (item is BaseRendererElement ele)
                {
                    if (name == ele.Name)
                    {
                        return true;
                    }
                    var subEle = ele.FindByName(name);
                    if (subEle is BaseRendererElement childElement && name == childElement.Name)
                    {
                        return true;
                    }
                }
                else if (item is BaseRendererControl element)
                {
                    if (name == element.ContainerId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
