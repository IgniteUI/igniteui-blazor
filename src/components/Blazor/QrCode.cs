using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Generates a QR code based on the provided value and options.
    /// The component renders an SVG representation of the QR code, which can be
    /// customized using various properties.
    /// </summary>
    public partial class IgbQrCode : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebQrCode"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbQrCodeModule.IsLoadRequested(IgBlazor))
            {
                IgbQrCodeModule.Register(IgBlazor);
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
                return "igc-qr-code";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private string? _value;

        /// <summary>
        /// The value to be encoded in the QR code. This can be any string, such as a URL, text, or other data.
        /// When this property is set, the component will generate a QR code representing the provided value.
        /// </summary>
        [Parameter]
        public string? Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }
        private double? _version;

        /// <summary>
        /// The version of the QR code to generate, which determines the size and data capacity of the QR code.
        /// Valid values are integers from 1 to 40, where each version corresponds to a specific module size and data capacity.
        /// If not specified, the component will automatically select the smallest version that can accommodate the provided value.
        /// </summary>
        [Parameter]
        public double? Version
        {
            get { return this._version; }
            set
            {
                if (this._version != value || !IsPropDirty("Version"))
                {
                    MarkPropDirty("Version");
                }
                this._version = value;

            }
        }
        private QrErrorCorrectionLevel _errorLevel = QrErrorCorrectionLevel.Medium;

        /// <summary>
        /// The error correction level for the QR code, which determines the QR code's ability to be read
        /// if it is partially obscured or damaged.
        /// <see cref="QrErrorCorrectionLevel.Low"/> provides the lowest level of error correction
        /// and <see cref="QrErrorCorrectionLevel.High"/> provides the highest level.
        /// </summary>
        [Parameter]
        public QrErrorCorrectionLevel ErrorLevel
        {
            get { return this._errorLevel; }
            set
            {
                if (this._errorLevel != value || !IsPropDirty("ErrorLevel"))
                {
                    MarkPropDirty("ErrorLevel");
                }
                this._errorLevel = value;

            }
        }
        private double _size = 128;

        /// <summary>
        /// The size of the QR code in pixels. This determines the width and height of the generated QR code.
        /// The default value is 128 pixels.
        /// </summary>
        [Parameter]
        public double Size
        {
            get { return this._size; }
            set
            {
                if (this._size != value || !IsPropDirty("Size"))
                {
                    MarkPropDirty("Size");
                }
                this._size = value;

            }
        }
        private double _margin = 4;

        /// <summary>
        /// The margin (quiet zone) around the QR code, expressed as a number of QR code modules rather
        /// than pixels. This is the blank border area surrounding the code, which helps ensure that it
        /// can be properly scanned.
        /// </summary>
        [Parameter]
        public double Margin
        {
            get { return this._margin; }
            set
            {
                if (this._margin != value || !IsPropDirty("Margin"))
                {
                    MarkPropDirty("Margin");
                }
                this._margin = value;

            }
        }
        private string? _logoSrc;

        /// <summary>
        /// The source URL of an optional logo image to be displayed at the center of the QR code.
        /// If provided, the component will attempt to render the logo within the QR code while maintaining scannability.
        /// </summary>
        [Parameter]
        public string? LogoSrc
        {
            get { return this._logoSrc; }
            set
            {
                if (this._logoSrc != value || !IsPropDirty("LogoSrc"))
                {
                    MarkPropDirty("LogoSrc");
                }
                this._logoSrc = value;

            }
        }
        private double _logoSize = 0.4;

        /// <summary>
        /// The size of the logo, as a ratio of the maximum area that can safely be obscured by a logo
        /// while the QR code remains scannable. The value should be a number between 0 and 1, where 0 means
        /// no logo and 1 means the logo will cover the full safe area (not the entire QR code).
        /// When <see cref="ErrorLevel"/> is not explicitly set, the smallest error correction level that can
        /// accommodate the requested logo size is chosen automatically.
        /// </summary>
        [Parameter]
        public double LogoSize
        {
            get { return this._logoSize; }
            set
            {
                if (this._logoSize != value || !IsPropDirty("LogoSize"))
                {
                    MarkPropDirty("LogoSize");
                }
                this._logoSize = value;

            }
        }
        private double? _logoMargin;

        /// <summary>
        /// The margin around the logo in pixels. This is the whitespace area surrounding the logo within the QR code,
        /// which helps ensure that the logo does not interfere with the QR code's scannability.
        /// </summary>
        [Parameter]
        public double? LogoMargin
        {
            get { return this._logoMargin; }
            set
            {
                if (this._logoMargin != value || !IsPropDirty("LogoMargin"))
                {
                    MarkPropDirty("LogoMargin");
                }
                this._logoMargin = value;

            }
        }
        private QrDotStyle _dotStyle = QrDotStyle.Square;

        /// <summary>
        /// The style of the data modules (dots) in the QR code, and of the inner dot of each
        /// finder-pattern corner.
        /// </summary>
        [Parameter]
        public QrDotStyle DotStyle
        {
            get { return this._dotStyle; }
            set
            {
                if (this._dotStyle != value || !IsPropDirty("DotStyle"))
                {
                    MarkPropDirty("DotStyle");
                }
                this._dotStyle = value;

            }
        }
        private QrCornerSquareStyle _squareStyle = QrCornerSquareStyle.Square;

        /// <summary>
        /// The style of the corner squares in the QR code.
        /// </summary>
        [Parameter]
        public QrCornerSquareStyle SquareStyle
        {
            get { return this._squareStyle; }
            set
            {
                if (this._squareStyle != value || !IsPropDirty("SquareStyle"))
                {
                    MarkPropDirty("SquareStyle");
                }
                this._squareStyle = value;

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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddStringProp("value", this._value); }
            if (IsPropDirty("Version"))
            { ser.AddNumberProp("version", this._version); }
            if (IsPropDirty("ErrorLevel"))
            { ser.AddEnumProp("errorLevel", this._errorLevel); }
            if (IsPropDirty("Size"))
            { ser.AddNumberProp("size", this._size); }
            if (IsPropDirty("Margin"))
            { ser.AddNumberProp("margin", this._margin); }
            if (IsPropDirty("LogoSrc"))
            { ser.AddStringProp("logoSrc", this._logoSrc); }
            if (IsPropDirty("LogoSize"))
            { ser.AddNumberProp("logoSize", this._logoSize); }
            if (IsPropDirty("LogoMargin"))
            { ser.AddNumberProp("logoMargin", this._logoMargin); }
            if (IsPropDirty("DotStyle"))
            { ser.AddEnumProp("dotStyle", this._dotStyle); }
            if (IsPropDirty("SquareStyle"))
            { ser.AddEnumProp("squareStyle", this._squareStyle); }

        }

    }
}
