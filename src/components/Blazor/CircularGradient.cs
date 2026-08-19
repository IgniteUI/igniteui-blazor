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
        /// <inheritdoc />
        public override string Type { get { return "WebCircularGradient"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCircularGradientModule.IsLoadRequested(IgBlazor))
            {
                IgbCircularGradientModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-circular-gradient";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private string _offset;

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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        /// <inheritdoc />
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
