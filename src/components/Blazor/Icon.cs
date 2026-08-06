using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The icon component allows visualizing collections of pre-registered SVG icons.
    /// </summary>
    public partial class IgbIcon : BaseRendererControl
    {
        public override string Type { get { return "WebIcon"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbIconModule.IsLoadRequested(IgBlazor))
            {
                IgbIconModule.Register(IgBlazor);
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
                return "igc-icon";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private string _iconName;

        /// <summary>
        /// The name of the icon glyph to draw.
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
        /// The name of the registered collection for look up of icons.
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
        /// Whether to flip the icon horizontally. Useful for RTL (right-to-left) layouts.
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

        partial void FindByNameIcon(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameIcon(name, ref item);
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

        /// <summary>
        /// Sets an icon reference/alias that points to another icon.
        /// </summary>
        /// <param name="name">The alias name.</param>
        /// <param name="collection">The collection for the alias.</param>
        /// <param name="icon">The target icon metadata (name and collection).</param>
        public async Task SetIconRefAsync(String name, String collection, IgbIconMeta icon)
        {
            await InvokeMethod("setIconRef", new object[] { StringToString(name), StringToString(collection), ObjectToParam(icon) }, new string[] { "String", "String", "Json" });
        }

        /// <summary>
        /// Sets an icon reference/alias that points to another icon.
        /// </summary>
        /// <param name="name">The alias name.</param>
        /// <param name="collection">The collection for the alias.</param>
        /// <param name="icon">The target icon metadata (name and collection).</param>
        public void SetIconRef(String name, String collection, IgbIconMeta icon)
        {
            InvokeMethodSync("setIconRef", new object[] { StringToString(name), StringToString(collection), ObjectToParam(icon) }, new string[] { "String", "String", "Json" });
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

        }

    }
}
