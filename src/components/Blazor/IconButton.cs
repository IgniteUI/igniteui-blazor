using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A button that displays a single icon, designed for compact, icon-only
    /// interactions such as toolbar actions, floating action buttons, or inline
    /// controls.
    /// The icon is sourced from the icon registry via the <see cref="IconName"/> and
    /// <see cref="Collection"/> properties. Like <see cref="IgbButton"/>, it can render as an anchor
    /// element when <see cref="IgbButtonBase.Href"/> is set and is fully form-associated.
    /// </summary>
    public partial class IgbIconButton : IgbButtonBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebIconButton"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbIconButtonModule.IsLoadRequested(IgBlazor))
            {
                IgbIconButtonModule.Register(IgBlazor);
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
                return "igc-icon-button";
            }
        }

        private string _iconName;

        /// <summary>
        /// The name of the icon to display.
        /// </summary>
        [Parameter]
        [WCWidgetMemberName("Name")]
        public string IconName
        {
            get { return this._iconName; }
            set
            {
                if (this._iconName != value || !IsPropDirty("IconName"))
                {
                    MarkPropDirty("IconName");
                }
                this._iconName = value;

            }
        }
        private string _collection;

        /// <summary>
        /// The collection the icon belongs to.
        /// </summary>
        [Parameter]
        public string Collection
        {
            get { return this._collection; }
            set
            {
                if (this._collection != value || !IsPropDirty("Collection"))
                {
                    MarkPropDirty("Collection");
                }
                this._collection = value;

            }
        }
        private bool _mirrored = false;

        /// <summary>
        /// Determines whether the icon should be mirrored in right-to-left contexts.
        /// </summary>
        [Parameter]
        public bool Mirrored
        {
            get { return this._mirrored; }
            set
            {
                if (this._mirrored != value || !IsPropDirty("Mirrored"))
                {
                    MarkPropDirty("Mirrored");
                }
                this._mirrored = value;

            }
        }
        private IconButtonVariant _variant = IconButtonVariant.Contained;

        /// <summary>
        /// The variant of the button which determines its visual appearance.
        /// <list type="bullet">
        ///   <item><description><see cref="IconButtonVariant.Contained"/> – filled background;
        ///     highest visual emphasis (default).</description></item>
        ///   <item><description><see cref="IconButtonVariant.Outlined"/> – transparent background
        ///     with a visible border.</description></item>
        ///   <item><description><see cref="IconButtonVariant.Flat"/> – no background or border;
        ///     lowest visual emphasis.</description></item>
        /// </list>
        /// </summary>
        [Parameter]
        public IconButtonVariant Variant
        {
            get { return this._variant; }
            set
            {
                if (this._variant != value || !IsPropDirty("Variant"))
                {
                    MarkPropDirty("Variant");
                }
                this._variant = value;

            }
        }

        /// <summary>
        /// Registers an icon by fetching it from a URL.
        /// </summary>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="url">The URL to fetch the SVG icon from.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        public async Task RegisterIconAsync(String name, String url, String collection = null)
        {
            await InvokeMethod("registerIcon", new object[] { StringToString(name), StringToString(url), StringToString(collection) }, new string[] { "String", "String", "String" });
        }

        /// <summary>
        /// Registers an icon by fetching it from a URL.
        /// </summary>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="url">The URL to fetch the SVG icon from.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        public void RegisterIcon(String name, String url, String collection = null)
        {
            InvokeMethodSync("registerIcon", new object[] { StringToString(name), StringToString(url), StringToString(collection) }, new string[] { "String", "String", "String" });
        }

        /// <summary>
        /// Registers an icon from SVG text content.
        /// </summary>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="iconText">The SVG markup as a string.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        public async Task RegisterIconFromTextAsync(String name, String iconText, String collection = null)
        {
            await InvokeMethod("registerIconFromText", new object[] { StringToString(name), StringToString(iconText), StringToString(collection) }, new string[] { "String", "String", "String" });
        }

        /// <summary>
        /// Registers an icon from SVG text content.
        /// </summary>
        /// <param name="name">The unique name for the icon.</param>
        /// <param name="iconText">The SVG markup as a string.</param>
        /// <param name="collection">The collection to register the icon in. Defaults to <c>default</c>.</param>
        public void RegisterIconFromText(String name, String iconText, String collection = null)
        {
            InvokeMethodSync("registerIconFromText", new object[] { StringToString(name), StringToString(iconText), StringToString(collection) }, new string[] { "String", "String", "String" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("IconName"))
            { ser.AddStringProp("iconName", this._iconName); }
            if (IsPropDirty("Collection"))
            { ser.AddStringProp("collection", this._collection); }
            if (IsPropDirty("Mirrored"))
            { ser.AddBooleanProp("mirrored", this._mirrored); }
            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }

        }

    }
}
