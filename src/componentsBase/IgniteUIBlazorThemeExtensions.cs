namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Configures the theme of every component on the page. These call the theme functions of the package JS API
    /// (<c>_content/IgniteUI.Blazor/api.js</c>), which apps can also import and call from JavaScript.
    /// </summary>
    /// <remarks>
    /// The calls use JS interop, so they cannot run during prerendering; make them from
    /// <c>OnAfterRenderAsync</c>, or anywhere after the component is interactive.
    /// </remarks>
    public static class IgniteUIBlazorThemeExtensions
    {
        /// <summary>
        /// Sets the theme, in its light variant, whose structural styles every component on the page uses: the
        /// page-wide equivalent of an <see cref="IgbThemeProvider"/> around the whole UI. Components inside a provider
        /// follow the provider instead.
        /// </summary>
        /// <remarks>
        /// It does not swap the theme stylesheet, which holds the palette. To switch themes at runtime, swap the
        /// stylesheet and call this with the theme it is for.
        /// </remarks>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="theme">The theme.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="igniteUI"/> is <see langword="null"/>.</exception>
        public static ValueTask ConfigureThemeAsync(this IIgniteUIBlazor igniteUI, Theme theme)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            return Api(igniteUI).InvokeVoidAsync("configureTheme", ToClient(theme));
        }

        /// <summary>
        /// Sets the theme and variant whose structural styles every component on the page uses: the page-wide
        /// equivalent of an <see cref="IgbThemeProvider"/> around the whole UI. Components inside a provider follow
        /// the provider instead.
        /// </summary>
        /// <remarks>
        /// It does not swap the theme stylesheet, which holds the palette. To switch themes at runtime, swap the
        /// stylesheet and call this with the theme and variant it is for.
        /// </remarks>
        /// <param name="igniteUI">The Ignite UI service.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="variant">The variant of the theme.</param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="igniteUI"/> is <see langword="null"/>.</exception>
        public static ValueTask ConfigureThemeAsync(this IIgniteUIBlazor igniteUI, Theme theme, ThemeVariant variant)
        {
            ArgumentNullException.ThrowIfNull(igniteUI);
            return Api(igniteUI).InvokeVoidAsync("configureTheme", ToClient(theme), ToClient(variant));
        }

        // The module the runtime keeps for the circuit: imported once, on first use, shared, and retried after a failed import.
        private static InteropModule Api(IIgniteUIBlazor igniteUI) =>
            igniteUI.AsRuntime().GetInteropModule(InteropModule.ApiPath);

        // The client names are the lowercase member names; the client ignores a name it does not know.
        private static string ToClient(Theme theme) => theme.ToString().ToLowerInvariant();

        private static string ToClient(ThemeVariant variant) => variant.ToString().ToLowerInvariant();
    }
}
