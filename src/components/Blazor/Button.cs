
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbButton : IgbButtonBase
    {
        public override string Type { get { return "WebButton"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbButtonModule.IsLoadRequested(IgBlazor))
            {
                IgbButtonModule.Register(IgBlazor);
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
                return "igc-button";
            }
        }

        public IgbButton() : base()
        {
            OnCreatedIgbButton();

        }

        partial void OnCreatedIgbButton();

        private ButtonVariant _variant = ButtonVariant.Contained;

        partial void OnVariantChanging(ref ButtonVariant newValue);
        /// <summary>
        /// The variant of the button which determines its visual appearance.
        /// - `contained` – filled background; highest visual emphasis (default).
        /// - `outlined` – transparent background with a visible border.
        /// - `flat` – no background or border; lowest visual emphasis.
        /// - `fab` – floating action button shape; typically used for primary actions.
        /// </summary>
        [Parameter]
        public ButtonVariant Variant
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

        partial void FindByNameButton(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameButton(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbButton(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbButton(ser);

            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }

        }

    }
}
