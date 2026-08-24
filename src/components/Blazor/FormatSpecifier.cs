namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class for the format specifiers that describe how component values are formatted.
    /// See <see cref="IgbNumberFormatSpecifier"/> for the number formatting options.
    /// </summary>
    public partial class IgbFormatSpecifier : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "FormatSpecifier"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbFormatSpecifierModule.IsLoadRequested(IgBlazor))
            {
                IgbFormatSpecifierModule.Register(IgBlazor);
            }
        }

        private static bool _marshalByValue = true;

        /// <summary>
        /// Gets the culture of the browser, expanded to a culture with a region when the browser
        /// reports a bare language code.
        /// </summary>
        /// <returns>The resolved culture name.</returns>
        public async Task<String?> GetLocalCultureAsync()
        {
            var iv = await InvokeMethod("getLocalCulture", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }
        /// <summary>
        /// Gets the culture of the browser, expanded to a culture with a region when the browser
        /// reports a bare language code.
        /// </summary>
        /// <returns>The resolved culture name.</returns>
        public String? GetLocalCulture()
        {
            var iv = InvokeMethodSync("getLocalCulture", new object?[] { }, new string[] { });
            return ReturnToString(iv);
        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            this.SuppressParentNotify = false;
        }

    }

    public class IgbFormatSpecifierModule
    {
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "FormatSpecifierModule");
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "FormatSpecifierModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "FormatSpecifierModule");
        }
    }

}
