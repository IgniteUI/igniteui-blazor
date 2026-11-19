namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbCombo{TValue, TItem}.SearchInput"/> event.
    /// </summary>
    public partial class IgbComboSearchInputEventArgsDetail : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComboSearchInputEventArgsDetail"; } }

        private string? _searchText;

        /// <summary>
        /// The text of the search input, or of the main input in single selection mode.
        /// </summary>
        public string? SearchText
        {
            get { return this._searchText; }
            set
            {
                if (this._searchText != value || !IsPropDirty("SearchText"))
                {
                    MarkPropDirty("SearchText");
                }
                this._searchText = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("SearchText"))
            { ser.AddStringProp("searchText", this._searchText); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("SearchText"))
            { args["searchText"] = this._searchText; }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("searchText", out var searchTextObj))
            { this.SearchText = ReturnToString(searchTextObj); }

            this.SuppressParentNotify = false;
        }

    }
}
