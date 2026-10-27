namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The file format an <see cref="IgbQrCode"/> is exported to.
    /// </summary>
    public enum QrCodeExportFormat
    {
        /// <summary>A vector image, <c>image/svg+xml</c>.</summary>
        Svg,
        /// <summary>A raster image, <c>image/png</c>.</summary>
        Png,
        /// <summary>A raster image, <c>image/jpeg</c>.</summary>
        Jpeg,
        /// <summary>A raster image, <c>image/webp</c>.</summary>
        Webp

    }
}
