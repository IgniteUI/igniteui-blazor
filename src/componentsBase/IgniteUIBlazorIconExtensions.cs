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
        /// Registers an icon in the <c>default</c> collection by fetching it from a URL. Completes once the icon is
        /// fetched and registered.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="url">The URL of the SVG icon.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/> or <paramref name="url"/> is <see langword="null"/>.
        /// </exception>
        public static ValueTask RegisterIconAsync(this IIgniteUIBlazor igniteUI, string name, string url)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(url);
            return Api(igniteUI).InvokeVoidAsync("registerIcon", name, url);
        }

        /// <summary>
        /// Registers an icon in a collection by fetching it from a URL. Completes once the icon is fetched and
        /// registered.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="url">The URL of the SVG icon.</param>
        /// <param name="collection">The collection to register the icon in.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/>, <paramref name="url"/> or <paramref name="collection"/>
        /// is <see langword="null"/>.
        /// </exception>
        public static ValueTask RegisterIconAsync(this IIgniteUIBlazor igniteUI, string name, string url, string collection)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(collection);
            return Api(igniteUI).InvokeVoidAsync("registerIcon", name, url, collection);
        }

        /// <summary>
        /// Registers an icon by fetching it from a URL, with options for the collection and SVG meta stripping.
        /// Completes once the icon is fetched and registered.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="url">The URL of the SVG icon.</param>
        /// <param name="options">The registration options.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/>, <paramref name="url"/> or <paramref name="options"/>
        /// is <see langword="null"/>.
        /// </exception>
        public static ValueTask RegisterIconAsync(this IIgniteUIBlazor igniteUI, string name, string url, IgbRegisterIconOptions options)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(options);
            return Api(igniteUI).InvokeVoidAsync("registerIcon", name, url, ToClient(options));
        }

        /// <summary>
        /// Registers an icon in the <c>default</c> collection from SVG text.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="iconText">The SVG markup of the icon.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/> or <paramref name="iconText"/> is <see langword="null"/>.
        /// </exception>
        public static ValueTask RegisterIconFromTextAsync(this IIgniteUIBlazor igniteUI, string name, string iconText)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(iconText);
            return Api(igniteUI).InvokeVoidAsync("registerIconFromText", name, iconText);
        }

        /// <summary>
        /// Registers an icon in a collection from SVG text.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="iconText">The SVG markup of the icon.</param>
        /// <param name="collection">The collection to register the icon in.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/>, <paramref name="iconText"/> or
        /// <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        public static ValueTask RegisterIconFromTextAsync(this IIgniteUIBlazor igniteUI, string name, string iconText, string collection)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(iconText);
            ArgumentNullException.ThrowIfNull(collection);
            return Api(igniteUI).InvokeVoidAsync("registerIconFromText", name, iconText, collection);
        }

        /// <summary>
        /// Registers an icon from SVG text, with options for the collection and SVG meta stripping.
        /// </summary>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="name">The unique name of the icon.</param>
        /// <param name="iconText">The SVG markup of the icon.</param>
        /// <param name="options">The registration options.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/>, <paramref name="iconText"/> or
        /// <paramref name="options"/> is <see langword="null"/>.
        /// </exception>
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
        /// <param name="name">The name of the alias.</param>
        /// <param name="collection">The collection of the alias.</param>
        /// <param name="icon">The name and collection of the icon the alias points to.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="igniteUI"/>, <paramref name="name"/>, <paramref name="collection"/> or
        /// <paramref name="icon"/> is <see langword="null"/>.
        /// </exception>
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
        // An unset member is left out rather than sent as null, so the client applies its own default.
        private static Dictionary<string, object?> ToClient(IgbRegisterIconOptions options)
        {
            var client = new Dictionary<string, object?> { ["stripMeta"] = options.StripMeta };
            if (options.Collection != null)
            {
                client["collection"] = options.Collection;
            }
            return client;
        }

        // Both members are required on the client; a null one is the caller's to answer for, as with any member.
        private static Dictionary<string, object?> ToClient(IgbIconMeta icon) => new Dictionary<string, object?>
        {
            ["name"] = icon.Name,
            ["collection"] = icon.Collection,
        };
    }
}
