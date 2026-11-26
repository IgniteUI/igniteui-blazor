namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Filtering options for the <see cref="IgbCombo{TValue, TItem}"/> component.
    /// </summary>
    public partial class IgbFilteringOptions : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebFilteringOptions"; } }

        private string? _filterKey;

        /// <summary>
        /// The key in the data source used when filtering the list of options.
        /// </summary>
        public string? FilterKey
        {
            get { return this._filterKey; }
            set
            {
                if (this._filterKey != value || !IsPropDirty("FilterKey"))
                {
                    MarkPropDirty("FilterKey");
                }
                this._filterKey = value;

            }
        }
        private bool _caseSensitive = false;

        /// <summary>
        /// Determines whether the filtering operation should be case sensitive.
        /// </summary>
        public bool CaseSensitive
        {
            get { return this._caseSensitive; }
            set
            {
                if (this._caseSensitive != value || !IsPropDirty("CaseSensitive"))
                {
                    MarkPropDirty("CaseSensitive");
                }
                this._caseSensitive = value;

            }
        }
        private bool _matchDiacritics = false;

        /// <summary>
        /// When <see langword="true"/>, the filter distinguishes between accented letters and their base letters.
        /// </summary>
        public bool MatchDiacritics
        {
            get { return this._matchDiacritics; }
            set
            {
                if (this._matchDiacritics != value || !IsPropDirty("MatchDiacritics"))
                {
                    MarkPropDirty("MatchDiacritics");
                }
                this._matchDiacritics = value;

            }
        }
        private ComboFilteringMode _mode = ComboFilteringMode.Local;

        /// <summary>
        /// Where the filtering runs. With <see cref="ComboFilteringMode.Remote"/>, the combo does not filter its
        /// options: handle <see cref="IgbCombo{TValue, TItem}.SearchInput"/> to set the data to the results of a
        /// server search, and <see cref="IgbCombo{TValue, TItem}.DataRequest"/> to append more items.
        /// </summary>
        public ComboFilteringMode Mode
        {
            get { return this._mode; }
            set
            {
                if (this._mode != value || !IsPropDirty("Mode"))
                {
                    MarkPropDirty("Mode");
                }
                this._mode = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("FilterKey"))
            { ser.AddStringProp("filterKey", this._filterKey); }
            if (IsPropDirty("CaseSensitive"))
            { ser.AddBooleanProp("caseSensitive", this._caseSensitive); }
            if (IsPropDirty("MatchDiacritics"))
            { ser.AddBooleanProp("matchDiacritics", this._matchDiacritics); }
            if (IsPropDirty("Mode"))
            { ser.AddEnumProp("mode", this._mode); }

        }

    }
}
