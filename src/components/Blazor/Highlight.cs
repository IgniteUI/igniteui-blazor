
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The highlight component provides efficient searching and highlighting of text
    /// projected into it via its default slot. It uses the native CSS Custom Highlight API
    /// to apply highlight styles to matched text nodes without modifying the DOM.
    /// The component supports case-sensitive matching, programmatic navigation between
    /// matches, and automatic scroll-into-view of the active match.
    /// </summary>
    public partial class IgbHighlight : BaseRendererControl
    {
        public override string Type { get { return "WebHighlight"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbHighlightModule.IsLoadRequested(IgBlazor))
            {
                IgbHighlightModule.Register(IgBlazor);
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
                return "igc-highlight";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbHighlight() : base()
        {
            OnCreatedIgbHighlight();

        }

        partial void OnCreatedIgbHighlight();

        private bool _caseSensitive = false;

        partial void OnCaseSensitiveChanging(ref bool newValue);
        /// <summary>
        /// Whether to match the searched text with case sensitivity in mind.
        /// When `true`, only exact-case occurrences of `searchText` are highlighted.
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
        private string _searchText;

        partial void OnSearchTextChanging(ref string newValue);
        /// <summary>
        /// The string to search and highlight in the DOM content of the component.
        /// Setting this property triggers a new search automatically.
        /// An empty string clears all highlights.
        /// </summary>
        [Parameter]
        public string SearchText
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
        public async Task<double> GetSizeAsync()
        {
            var iv = await InvokeMethod("p:Size", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }
        public double GetSize()
        {
            var iv = InvokeMethodSync("p:Size", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }
        public async Task<double> GetCurrentAsync()
        {
            var iv = await InvokeMethod("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }
        public double GetCurrent()
        {
            var iv = InvokeMethodSync("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        partial void FindByNameHighlight(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameHighlight(name, ref item);
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
        /// Moves the active highlight to the next match.
        /// Wraps around to the first match after the last one.
        /// options - Optional navigation options (e.g. `preventScroll`).
        /// </summary>
        /// <param name="options">- Optional navigation options (e.g. `preventScroll`).</param>
        public async Task NextAsync(IgbHighlightNavigation options)
        {
            await InvokeMethod("next", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        public void Next(IgbHighlightNavigation options)
        {
            InvokeMethodSync("next", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Moves the active highlight to the previous match.
        /// Wraps around to the last match when going back from the first one.
        /// options - Optional navigation options (e.g. `preventScroll`).
        /// </summary>
        /// <param name="options">- Optional navigation options (e.g. `preventScroll`).</param>
        public async Task PreviousAsync(IgbHighlightNavigation options)
        {
            await InvokeMethod("previous", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        public void Previous(IgbHighlightNavigation options)
        {
            InvokeMethodSync("previous", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        public async Task SetActiveAsync(double index, IgbHighlightNavigation options)
        {
            await InvokeMethod("setActive", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }
        public void SetActive(double index, IgbHighlightNavigation options)
        {
            InvokeMethodSync("setActive", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }
        /// <summary>
        /// Re-runs the highlight search based on the current `searchText` and `caseSensitive` values.
        /// Call this method after the slotted content changes dynamically (e.g. after lazy loading
        /// or programmatic DOM mutations) to ensure all matches are up to date.
        /// </summary>
        public async Task SearchAsync()
        {
            await InvokeMethod("search", new object[] { }, new string[] { });
        }
        public void Search()
        {
            InvokeMethodSync("search", new object[] { }, new string[] { });
        }

        partial void SerializeCoreIgbHighlight(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbHighlight(ser);

            if (IsPropDirty("CaseSensitive"))
            { ser.AddBooleanProp("caseSensitive", this._caseSensitive); }
            if (IsPropDirty("SearchText"))
            { ser.AddStringProp("searchText", this._searchText); }

        }

    }
}
