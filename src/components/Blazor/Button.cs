using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a clickable button, used to submit forms or anywhere in a
    /// document for accessible, standard button functionality.
    /// The button supports multiple visual variants, can render as an anchor
    /// (<c>&lt;a&gt;</c>) element when <see cref="IgbButtonBase.Href"/> is set, and is fully
    /// form-associated, acting as a native <c>submit</c> or <c>reset</c> control.
    /// </summary>
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

        private ButtonVariant _variant = ButtonVariant.Contained;

        /// <summary>
        /// The variant of the button which determines its visual appearance.
        /// <list type="bullet">
        ///   <item><description><see cref="ButtonVariant.Contained"/> – filled background;
        ///     highest visual emphasis (default).</description></item>
        ///   <item><description><see cref="ButtonVariant.Outlined"/> – transparent background
        ///     with a visible border.</description></item>
        ///   <item><description><see cref="ButtonVariant.Flat"/> – no background or border;
        ///     lowest visual emphasis.</description></item>
        ///   <item><description><see cref="ButtonVariant.Fab"/> – floating action button shape;
        ///     typically used for primary actions.</description></item>
        /// </list>
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

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Variant"))
            { ser.AddEnumProp("variant", this._variant); }

        }

    }
}
