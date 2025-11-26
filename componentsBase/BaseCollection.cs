using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IgniteUI.Blazor.Controls 
{

    public class BaseCollection<T> : ObservableCollection<T>  {
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

        public T[] ToArray()
        {
            var array = new T[Count];
            this.CopyTo(array, 0);
            return array;
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (item is BaseRendererElement) {
                BaseRendererElement c = (BaseRendererElement)(object)item;
                c.Parent = _parent;
            }
            NotifyParent();
        }

        protected override void RemoveItem(int index) 
        {
            var item = this[index];
            base.RemoveItem(index);
            if (item is BaseRendererElement) {
                BaseRendererElement c = (BaseRendererElement)(object)item;
                c.Parent = null;
            }
            NotifyParent();
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            if (item is BaseRendererElement) {
                BaseRendererElement c = (BaseRendererElement)(object)item;
                c.Parent = _parent;
            }
            NotifyParent();
        }

        internal object Parent
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

        private object _parent = null;
        private string _propertyName = null;
        internal string PropertyName
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

        public BaseCollection(object parent, string propertyName) {
            _parent = parent;
            _propertyName = propertyName;
        }

        private void NotifyParent() {
            if (_suppressNotify)
            {
                return;
            }
            if (_parent == null)
            {
                return;
            }
            if (_parent is BaseRendererElement) {
                ((BaseRendererElement)_parent).MarkPropDirty(_propertyName);
            }
            if (_parent is BaseRendererControl) {
                ((BaseRendererControl)_parent).MarkPropDirty(_propertyName);
            }
        }

        protected override void ClearItems()
        {
            for (var i = 0; i < Count; i++) {
                var item = this[i];
                if (item is BaseRendererElement) {
                    BaseRendererElement c = (BaseRendererElement)(object)item;
                    c.Parent = null;
                }
            }
            base.ClearItems();
            NotifyParent();
        }

         public void Serialize(SerializationContext context, string propertyName = null)  {
            //var vals = new List<string>();
            if (propertyName != null)
            {
                context.Writer.WriteStartArray(propertyName);
            }
            else
            {
                context.Writer.WriteStartArray();
            }
            for (var i = 0; i < Count; i++) {
                var val = this[i];
                if (val is JsonSerializable) {
                    ((JsonSerializable)val).Serialize(context);
                } else {
                    if (typeof(T) == typeof(int)) {
                        context.Writer.WriteNumberValue((int)(object)val);
                    }
                    else if (typeof(T) == typeof(long)) {
                        context.Writer.WriteNumberValue((long)(object)val);
                    }
                    else if (typeof(T) == typeof(short)) {
                        context.Writer.WriteNumberValue((short)(object)val);
                    }
                    else if (typeof(T) == typeof(decimal)) {
                        context.Writer.WriteNumberValue((decimal)(object)val);
                    }
                    else if (typeof(T) == typeof(float)) {
                        context.Writer.WriteNumberValue((float)(object)val);
                    }
                    else if (typeof(T) == typeof(double)) {
                        context.Writer.WriteNumberValue((double)(object)val);
                    }
                    else if (typeof(T) == typeof(byte)) {
                        context.Writer.WriteNumberValue((byte)(object)val);
                    }
                    else if (typeof(T) == typeof(string)) {
                        context.Writer.WriteStringValue((string)(object)val);
                    }
                    else
                    {
                        if (_parent is BaseRendererElement) {
                            ((BaseRendererElement)_parent).ObjectToParam(context, val);
                        }
                        if (_parent is BaseRendererControl) {
                            ((BaseRendererControl)_parent).ObjectToParam(context, val);
                        }
                    }
                }
            }
            context.Writer.WriteEndArray();
            //return "[" + string.Join(", \n", vals) + "]";
        }

        public object FindByName(string name)
        {
            //TODO: hash map
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                if (item is BaseRendererElement)
                {
                    var ele = (BaseRendererElement)(object)item;
                    if (name == ele.Name) {
                        return item;
                    }
                    var subEle = ele.FindByName(name);
                    if (subEle is BaseRendererElement)
                    {
                        if (name == ((BaseRendererElement)subEle).Name)
                        {
                            return subEle;
                        }
                    }
                } else if (item is BaseRendererControl) {
                    BaseRendererControl element = (BaseRendererControl)(object)item;
                    if (name == element.ContainerId)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        public bool HasName(string name) 
        {
            //TODO: hash map
           for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                if (item is BaseRendererElement)
                {
                    var ele = (BaseRendererElement)(object)item;
                    if (name == ele.Name)
                    {
                        return true;
                    }
                    var subEle = ele.FindByName(name);
                    if (subEle is BaseRendererElement)
                    {
                        if (name == ((BaseRendererElement)subEle).Name)
                        {
                            return true;
                        }
                    }
                }
                else if (item is BaseRendererControl) {
                    BaseRendererControl element = (BaseRendererControl)(object)item;
                    if (name == element.ContainerId) {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}