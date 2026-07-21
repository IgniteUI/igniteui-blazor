using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbFilteringOptions : BaseRendererElement
    {
        public override string Type { get { return "WebFilteringOptions"; } }

        public IgbFilteringOptions() : base()
        {
            OnCreatedIgbFilteringOptions();

        }

        partial void OnCreatedIgbFilteringOptions();

        private string _filterKey;

        partial void OnFilterKeyChanging(ref string newValue);
        /// <summary>
        /// The key in the data source used when filtering the list of options.
        /// </summary>
        [Parameter]
        public string FilterKey
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

        partial void OnCaseSensitiveChanging(ref bool newValue);
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

        partial void OnMatchDiacriticsChanging(ref bool newValue);
        /// <summary>
        /// If true, the filter distinguishes between accented letters and their base letters
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

        partial void FindByNameFilteringOptions(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameFilteringOptions(name, ref item);
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

        partial void SerializeCoreIgbFilteringOptions(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbFilteringOptions(ser);

            if (IsPropDirty("FilterKey"))
            { ser.AddStringProp("filterKey", this._filterKey); }
            if (IsPropDirty("CaseSensitive"))
            { ser.AddBooleanProp("caseSensitive", this._caseSensitive); }
            if (IsPropDirty("MatchDiacritics"))
            { ser.AddBooleanProp("matchDiacritics", this._matchDiacritics); }

        }

    }
}
