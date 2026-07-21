
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A theme provider component that uses Lit context to provide theme information
    /// to descendant components.
    /// This component allows you to scope a theme to a specific part of the page.
    /// All library components within this provider will use the specified theme
    /// instead of the global theme.
    /// </summary>
    public partial class IgbThemeProvider : BaseRendererControl
    {
        public override string Type { get { return "WebThemeProvider"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbThemeProviderModule.IsLoadRequested(IgBlazor))
            {
                IgbThemeProviderModule.Register(IgBlazor);
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
                return "igc-theme-provider";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbThemeProvider() : base()
        {
            OnCreatedIgbThemeProvider();

        }

        partial void OnCreatedIgbThemeProvider();

        private Theme _theme = Theme.Material;

        partial void OnThemeChanging(ref Theme newValue);
        /// <summary>
        /// The theme to provide to descendant components.
        /// </summary>
        [Parameter]
        public Theme Theme
        {
            get { return this._theme; }
            set
            {
                if (this._theme != value || !IsPropDirty("Theme"))
                {
                    MarkPropDirty("Theme");
                }
                this._theme = value;

            }
        }
        private ThemeVariant _variant = ThemeVariant.Light;

        partial void OnVariantChanging(ref ThemeVariant newValue);
        /// <summary>
        /// The theme variant to provide to descendant components.
        /// </summary>
        [Parameter]
        public ThemeVariant Variant
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

        partial void FindByNameThemeProvider(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameThemeProvider(name, ref item);
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

        partial void SerializeCoreIgbThemeProvider(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbThemeProvider(ser);

            if (IsPropDirty("Theme"))
            { ser.AddEnumProp("theme", this._theme); }
            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }

        }

    }
}
