namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options controlling how an item is scrolled into view, passed to the
    /// <c>ScrollToIndex</c> methods of <see cref="IgbVirtualScroll"/>.
    /// Mirrors the browser scroll-into-view options.
    /// </summary>
    public partial class IgbScrollIntoViewOptions : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebScrollIntoViewOptions"; } }

        private string? _behavior;

        /// <summary>
        /// The scroll behavior: <c>auto</c> or <c>smooth</c>.
        /// </summary>
        public string? Behavior
        {
            get { return this._behavior; }
            set
            {
                if (this._behavior != value || !IsPropDirty("Behavior"))
                {
                    MarkPropDirty("Behavior");
                }
                this._behavior = value;

            }
        }
        private string? _block;

        /// <summary>
        /// The vertical alignment of the item in the viewport: <c>start</c>, <c>center</c>,
        /// <c>end</c> or <c>nearest</c>.
        /// </summary>
        public string? Block
        {
            get { return this._block; }
            set
            {
                if (this._block != value || !IsPropDirty("Block"))
                {
                    MarkPropDirty("Block");
                }
                this._block = value;

            }
        }
        private string? _inline;

        /// <summary>
        /// The horizontal alignment of the item in the viewport: <c>start</c>, <c>center</c>,
        /// <c>end</c> or <c>nearest</c>.
        /// </summary>
        public string? Inline
        {
            get { return this._inline; }
            set
            {
                if (this._inline != value || !IsPropDirty("Inline"))
                {
                    MarkPropDirty("Inline");
                }
                this._inline = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Behavior"))
            { ser.AddStringProp("behavior", this._behavior); }
            if (IsPropDirty("Block"))
            { ser.AddStringProp("block", this._block); }
            if (IsPropDirty("Inline"))
            { ser.AddStringProp("inline", this._inline); }

        }

    }
}
