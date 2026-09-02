using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Filtering options for the <see cref="IgbCombo{T}"/> component.
    /// </summary>
    public partial class IgbFilteringOptions : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebFilteringOptions"; } }

        private string? _filterKey;

        /// <summary>
        /// The key in the data source used when filtering the list of options.
        /// </summary>
        [Parameter]
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
        [Parameter]
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
        [Parameter]
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

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
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

        }

    }
}
