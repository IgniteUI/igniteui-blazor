using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options controlling how a QR code is exported to an image file,
    /// passed to the <c>ToImage</c> methods of <see cref="IgbQrCode"/>.
    /// </summary>
    public partial class IgbQrCodeExportOptions : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebQrCodeExportOptions"; } }

        private static bool _marshalByValue = true;

        private string _fileName;

        /// <summary>
        /// The name of the exported file. The extension of the format is appended when the name
        /// does not end with it. Defaults to <c>qr-code</c>.
        /// </summary>
        [Parameter]
        public string FileName
        {
            get { return this._fileName; }
            set
            {
                if (this._fileName != value || !IsPropDirty("FileName"))
                {
                    MarkPropDirty("FileName");
                }
                this._fileName = value;

            }
        }
        private QrCodeExportFormat _format = QrCodeExportFormat.Png;

        /// <summary>
        /// The output format.
        /// </summary>
        [Parameter]
        public QrCodeExportFormat Format
        {
            get { return this._format; }
            set
            {
                if (this._format != value || !IsPropDirty("Format"))
                {
                    MarkPropDirty("Format");
                }
                this._format = value;

            }
        }
        private double _scale = 1;

        /// <summary>
        /// Multiplier applied to the <see cref="IgbQrCode.Size"/> of the component. A 256px QR code
        /// with a scale of 2 exports as a 512x512 image. For <see cref="QrCodeExportFormat.Svg"/>,
        /// the multiplier applies to the <c>width</c> and <c>height</c> attributes while the
        /// <c>viewBox</c> is unchanged.
        /// </summary>
        [Parameter]
        public double Scale
        {
            get { return this._scale; }
            set
            {
                if (this._scale != value || !IsPropDirty("Scale"))
                {
                    MarkPropDirty("Scale");
                }
                this._scale = value;

            }
        }
        private bool _download = false;

        /// <summary>
        /// Whether to open the browser download dialog for the exported file.
        /// </summary>
        [Parameter]
        public bool Download
        {
            get { return this._download; }
            set
            {
                if (this._download != value || !IsPropDirty("Download"))
                {
                    MarkPropDirty("Download");
                }
                this._download = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("FileName"))
            { ser.AddStringProp("fileName", this._fileName); }
            if (IsPropDirty("Format"))
            { ser.AddEnumProp("format", this._format); }
            if (IsPropDirty("Scale"))
            { ser.AddNumberProp("scale", this._scale); }
            if (IsPropDirty("Download"))
            { ser.AddBooleanProp("download", this._download); }

        }

    }
}
