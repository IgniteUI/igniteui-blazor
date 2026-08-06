using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Defines a gradient stop for an <see cref="IgbCircularProgress"/> component.
    /// Nest one or more of these in the <c>gradient</c> slot of an <see cref="IgbCircularProgress"/>;
    /// each one produces an SVG stop element.
    /// <see cref="Color"/>, <see cref="Offset"/> and <see cref="Opacity"/> are applied as the
    /// <c>stop-color</c>, <c>offset</c> and <c>stop-opacity</c> of that stop without further validation.
    /// </summary>
    public partial class IgbCircularGradient : BaseRendererControl
    {
        public override string Type { get { return "WebCircularGradient"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbCircularGradientModule.IsLoadRequested(IgBlazor))
            {
                IgbCircularGradientModule.Register(IgBlazor);
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
                return "igc-circular-gradient";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private string _offset;

        partial void OnOffsetChanging(ref string newValue);
        /// <summary>
        /// Defines where the gradient stop is placed along the gradient vector.
        /// </summary>
        [Parameter]
        public string Offset
        {
            get { return this._offset; }
            set
            {
                if (this._offset != value || !IsPropDirty("Offset"))
                {
                    MarkPropDirty("Offset");
                }
                this._offset = value;

            }
        }
        private string _color;

        partial void OnColorChanging(ref string newValue);
        /// <summary>
        /// Defines the color of the gradient stop.
        /// </summary>
        [Parameter]
        public string Color
        {
            get { return this._color; }
            set
            {
                if (this._color != value || !IsPropDirty("Color"))
                {
                    MarkPropDirty("Color");
                }
                this._color = value;

            }
        }
        private double _opacity = 1;

        partial void OnOpacityChanging(ref double newValue);
        /// <summary>
        /// Defines the opacity of the gradient stop.
        /// </summary>
        [Parameter]
        public double Opacity
        {
            get { return this._opacity; }
            set
            {
                if (this._opacity != value || !IsPropDirty("Opacity"))
                {
                    MarkPropDirty("Opacity");
                }
                this._opacity = value;

            }
        }

        partial void FindByNameCircularGradient(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameCircularGradient(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Offset"))
            { ser.AddStringProp("offset", this._offset); }
            if (IsPropDirty("Color"))
            { ser.AddStringProp("color", this._color); }
            if (IsPropDirty("Opacity"))
            { ser.AddNumberProp("opacity", this._opacity); }

        }

    }
}
