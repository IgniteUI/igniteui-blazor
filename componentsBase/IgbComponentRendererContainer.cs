using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IgniteUI.Blazor.Controls
{
    public class IgbComponentRendererContainer: ComponentBase
    {

        private Type _componentType;
        [Parameter]
        public Type ComponentType { 
            get
            {
                return _componentType;
            } 
            set
            {
                var oldValue = _componentType;
                _componentType = value;
                if (oldValue != _componentType)
                {
                    StateHasChanged();
                }
            } 
        }

        private object _rootComponent = null;
        public object RootComponent
        {
            get
            {
                return _rootComponent;
            }
            set
            {
                var oldValue = _rootComponent;
                _rootComponent = value;
                if (oldValue != _rootComponent) 
                {
                    OnRootComponentChanged(oldValue, _rootComponent);
                }
            }
        }

        private void OnRootComponentChanged(object oldComponent, object newComponent)
        {
            if (ComponentChanged != null)
            {
                ComponentChanged(this, new ComponentRendererComponentChangedEventArgs() { OldComponent = oldComponent, NewComponent = newComponent });

            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ComponentType != null)
            {
                builder.OpenComponent(0, ComponentType);
                builder.AddComponentReferenceCapture(1, delegate(object value)
                {
                    RootComponent = value;
                });
                builder.CloseComponent();
            }
            else
            {
                RootComponent = null;
            }
        }

        public event ComponentRendererComponentChangedEventHandler ComponentChanged;
    }
    
    public delegate void ComponentRendererComponentChangedEventHandler(object sender, ComponentRendererComponentChangedEventArgs args);

    public class ComponentRendererComponentChangedEventArgs
    {
        public object OldComponent { get; internal set; }
        public object NewComponent { get; internal set; }
    }

}