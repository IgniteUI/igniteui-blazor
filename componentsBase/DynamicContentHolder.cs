using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace IgniteUI.Blazor.Controls
{

    public class DynamicContentHolder: ComponentBase
    {
        protected List<DynamicContentInfo> DynamicContentInfo
        {
            get;
            set;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DynamicContentInfo = new List<DynamicContentInfo>();
        }

        private Dictionary<string, DynamicContentInfo> _contentInfos = new Dictionary<string, DynamicContentInfo>();

        public void AddDynamicContent(DynamicContentInfo content)
        {
            _contentInfos[content.RefName] = content;
            DynamicContentInfo.Add(content);
            StateHasChanged();
        }

        public void RemoveDynamicContent(DynamicContentInfo content)
        {
            if (_contentInfos.ContainsKey(content.RefName))
            {
                _contentInfos.Remove(content.RefName);
            }
            DynamicContentInfo.Remove(content);
            StateHasChanged();
        }

        protected void OnDynamicChildRef(string refName, object child)
        {
            if (_contentInfos.ContainsKey(refName))
            {
                var info = _contentInfos[refName];
                info.Component = child;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "ig-dynamic-content-holder");
            __builder.AddAttribute(2, "style", "display: none");
            __builder.AddMarkupContent(3, "\r\n");
            foreach (DynamicContentInfo item in DynamicContentInfo)
            {
                __builder.AddContent(4, "        ");
                __builder.OpenElement(5, "div");
                __builder.AddAttribute(6, "id", item.RefDivName);
                __builder.SetKey(item.RefDivName);
                __builder.AddMarkupContent(7, "\r\n            ");
                __builder.OpenElement(8, "div");
                __builder.AddAttribute(9, "id", item.RefName);
                __builder.AddMarkupContent(10, "\r\n            ");
                __builder.OpenComponent(11, item.ControlType);
                __builder.SetKey(item.RefName);
                __builder.AddComponentReferenceCapture(12, delegate(object __value)
                {
                    OnDynamicChildRef(item.RefName, __value);
                });
                __builder.CloseComponent();
                __builder.AddMarkupContent(13, "\r\n        ");
                __builder.CloseElement();
                __builder.AddMarkupContent(14, "\r\n        ");
                __builder.CloseElement();
                __builder.AddMarkupContent(15, "\r\n");
            }
            __builder.CloseElement();
        }
    }

    
    public abstract class DynamicContentInfo
    {
        public Type ControlType { get; set; }
        public DynamicContentInfo()
        {
            RefName = Guid.NewGuid().ToString();
        }

        public string RefName { get; set; }
        public string RefDivName
        {
            get
            {
                return RefName + "_div";
            }
        }

        private object _component = null;
        public object Component 
        { 
            get
            {
                return _component;
            }
            set
            {
                var oldValue = _component;
                _component = value;
                OnComponentChanged(oldValue, _component);
            } 
        }

        public BaseRendererControl Owner { get; internal set; }

        protected virtual void OnComponentChanged(object oldValue, object component)
        {
            
        }

        public virtual void UpdateTemplate(object template)
        {

        }

        public virtual void UpdateContext(object context)
        {

        }
    }

    public class TypedDynamicContent
        : DynamicContentInfo
    {
        public TypedDynamicContent(Type t)
        {
            ControlType = t;
        }

        protected override void OnComponentChanged(object oldValue, object component)
        {
            //if (component != null)
            {
                if (OnComponentChanging != null)
                {
                    OnComponentChanging(this, new DynamicComponentChangingEventArgs() { OldComponent = oldValue, NewComponent = component });
                }
            }

            List<TaskCompletionSource<object>> toSignal;
            lock (_lock)
            {
                toSignal = new List<TaskCompletionSource<object>>(waiting);
                waiting.Clear();
            }

            foreach (var item in toSignal)
            {
                item.SetResult(Component);
            }
        }

        private object _lock = new object();
        private List<TaskCompletionSource<object>> waiting = new List<TaskCompletionSource<object>>();
        public Task<object> GetInstanceAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            object component = null;
            List<TaskCompletionSource<object>> toSignal = null;
            
            lock (_lock)
            {
                component = Component;
                waiting.Add(tcs);

                if (component != null)
                {
                    toSignal = new List<TaskCompletionSource<object>>(waiting);
                    waiting.Clear();
                }
            }

            if (component != null)
            {
                foreach (var item in toSignal)
                {
                    item.SetResult(Component);
                }
            }

            return tcs.Task;
        }

        public event DynamicComponentChangingEventHandler OnComponentChanging;
    }

    public delegate void DynamicComponentChangingEventHandler(object sender, DynamicComponentChangingEventArgs e);

    public class DynamicComponentChangingEventArgs
    {
        public object OldComponent { get; internal set; }
        public object NewComponent { get; internal set; }
    }

    public class DynamicContentInfo<T>
        : DynamicContentInfo
    {
        public DynamicContentInfo()
        {
            ControlType = typeof(IgbTemplateContent<T>);
        }

        private RenderFragment<T> _template;
        private T _context;

        private bool _hasPopulatedContext = false;

        public RenderFragment<T> Template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
        }
        public T Context
        {
            get
            {
                return _context;
            }
            set
            {
                var oldValue = _context;
                _context = value;
                _hasPopulatedContext = true;
                OnContextChanged(oldValue, _context);
            }
        }

        protected override void OnComponentChanged(object oldValue, object component)
        {
            if (component is IgbTemplateContent<T>)
            {
                OnContextChanged((T)Context, (T)Context);
            }
        }
        
        private void OnContextChanged(T oldValue, T newValue)
        {
            if (Component is IgbTemplateContent<T>)
            {
                var template = (IgbTemplateContent<T>)Component;

                if (_hasPopulatedContext) {
                    template.Context = (T)Context;
                }
                template.Template = Template;
                template.Update();
            }
        }

        public override void UpdateTemplate(object template)
        {
            Template = (RenderFragment<T>)template;
        }

        public override void UpdateContext(object context)
        {
            Context = (T)context;
        }
    }

}