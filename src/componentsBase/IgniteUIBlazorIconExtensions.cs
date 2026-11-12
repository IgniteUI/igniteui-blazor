namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Registers icons for every <see cref="IgbIcon"/> and <see cref="IgbIconButton"/> on the page, without an
    /// icon instance. The icon registry is global: these call the icon functions of the package JS API
    /// (<c>_content/IgniteUI.Blazor/api.js</c>), which apps can also import and call from JavaScript.
    /// </summary>
    /// <remarks>
    /// The calls use JS interop, so they cannot run during prerendering; make them from
    /// <c>OnAfterRenderAsync</c>, or anywhere after the component is interactive.
    /// </remarks>
    public static class IgniteUIBlazorIconExtensions
    {
        /// <summary>
        /// Registers an icon by fetching it from a URL.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="url">The URL to fetch the SVG icon from.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        /// <returns>A task that completes once the icon is fetched and registered.</returns>
        public static ValueTask RegisterIconAsync(this IIgniteUIBlazor igniteUI, string name, string url, string? collection = null)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(url);
            return Api(igniteUI).InvokeVoidAsync("registerIcon", name, url, collection);
        }

        /// <summary>
        /// Registers an icon by fetching it from a URL.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="url">The URL to fetch the SVG icon from.</param>
        /// <param name="options">Options controlling the collection and SVG meta stripping.</param>
        /// <returns>A task that completes once the icon is fetched and registered.</returns>
        public static ValueTask RegisterIconAsync(this IIgniteUIBlazor igniteUI, string name, string url, IgbRegisterIconOptions options)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(options);
            return Api(igniteUI).InvokeVoidAsync("registerIcon", name, url, ToClient(options));
        }

        /// <summary>
        /// Registers an icon from SVG text content.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="iconText">The SVG markup as a string.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        /// <returns>A task that completes once the icon is registered.</returns>
        public static ValueTask RegisterIconFromTextAsync(this IIgniteUIBlazor igniteUI, string name, string iconText, string? collection = null)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(iconText);
            return Api(igniteUI).InvokeVoidAsync("registerIconFromText", name, iconText, collection);
        }

        /// <summary>
        /// Registers an icon from SVG text content.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="iconText">The SVG markup as a string.</param>
        /// <param name="options">Options controlling the collection and SVG meta stripping.</param>
        /// <returns>A task that completes once the icon is registered.</returns>
        public static ValueTask RegisterIconFromTextAsync(this IIgniteUIBlazor igniteUI, string name, string iconText, IgbRegisterIconOptions options)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(iconText);
            ArgumentNullException.ThrowIfNull(options);
            return Api(igniteUI).InvokeVoidAsync("registerIconFromText", name, iconText, ToClient(options));
        }

        /// <summary>
        /// Sets an icon reference (an alias) that points to another icon.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The alias name.</param>
        /// <param name="collection">The collection for the alias.</param>
        /// <param name="icon">The target icon metadata (name and collection).</param>
        /// <returns>A task that completes once the reference is set.</returns>
        public static ValueTask SetIconRefAsync(this IIgniteUIBlazor igniteUI, string name, string collection, IgbIconMeta icon)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(icon);
            return Api(igniteUI).InvokeVoidAsync("setIconRef", name, collection, ToClient(icon));
        }

        // The module the runtime keeps for the circuit: imported once, on first use, shared, and retried after a failed import.
        private static InteropModule Api(IIgniteUIBlazor igniteUI) =>
            igniteUI.AsRuntime().GetInteropModule(InteropModule.ApiPath);

        // The client option objects, as dictionaries of primitives: JS interop serializes them without reflecting over
        // library types, which stays trim-safe and leaves out the bookkeeping members of the BaseJsonSerializable types.
        private static Dictionary<string, object?> ToClient(IgbRegisterIconOptions options) => new Dictionary<string, object?>
        {
            ["collection"] = options.Collection,
            ["stripMeta"] = options.StripMeta,
        };

        private static Dictionary<string, object?> ToClient(IgbIconMeta icon) => new Dictionary<string, object?>
        {
            ["name"] = icon.Name,
            ["collection"] = icon.Collection,
        };
    }
}
