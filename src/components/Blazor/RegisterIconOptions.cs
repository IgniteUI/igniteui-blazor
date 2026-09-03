using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options controlling how an icon is registered, passed to the
    /// <c>RegisterIcon</c> and <c>RegisterIconFromText</c> methods of <see cref="IgbIcon"/>.
    /// </summary>
    public partial class IgbRegisterIconOptions : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "RegisterIconOptions"; } }

        private string _collection;

        /// <summary>
        /// The collection to register the icon in. Defaults to <c>default</c>.
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
        private bool _stripMeta = false;

        /// <summary>
        /// Whether to strip SVG meta elements (<c>&lt;title&gt;</c> and <c>&lt;desc&gt;</c>) from the icon
        /// before storing it. This prevents the native browser tooltip on hover; the title text stays
        /// available as the <c>aria-label</c> of the host icon element.
        /// </summary>
        [Parameter]
        public bool StripMeta
        {
            get { return this._stripMeta; }
            set
            {
                if (this._stripMeta != value || !IsPropDirty("StripMeta"))
                {
                    MarkPropDirty("StripMeta");
                }
                this._stripMeta = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Collection"))
            { ser.AddStringProp("collection", this._collection); }
            if (IsPropDirty("StripMeta"))
            { ser.AddBooleanProp("stripMeta", this._stripMeta); }

        }

    }
}
