using Microsoft.AspNetCore.Components;

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
        /// <inheritdoc />
        public override string Type { get { return "WebHighlight"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbHighlightModule.IsLoadRequested(IgBlazor))
            {
                IgbHighlightModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-highlight";
            }
        }

        /// <inheritdoc />
        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _caseSensitive = false;

        /// <summary>
        /// Whether to match the searched text with case sensitivity in mind.
        /// When <see langword="true"/>, only exact-case occurrences of <see cref="SearchText"/> are highlighted.
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
        private string? _searchText;

        /// <summary>
        /// The string to search and highlight in the DOM content of the component.
        /// Setting this property triggers a new search automatically.
        /// An empty string clears all highlights.
        /// </summary>
        [Parameter]
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

        /// <summary>
        /// Get the total number of matches found for the current <see cref="SearchText"/>.
        /// </summary>
        /// <returns>
        /// The number of matches, or <c>0</c> when there are no matches or <see cref="SearchText"/> is empty.
        /// </returns>
        public async Task<double> GetSizeAsync()
        {
            var iv = await InvokeMethod("p:Size", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Get the total number of matches found for the current <see cref="SearchText"/>.
        /// </summary>
        /// <returns>
        /// The number of matches, or <c>0</c> when there are no matches or <see cref="SearchText"/> is empty.
        /// </returns>
        public double GetSize()
        {
            var iv = InvokeMethodSync("p:Size", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Get the zero-based index of the currently active (focused) match.
        /// </summary>
        /// <returns>The index of the active match, or <c>0</c> when there are no matches.</returns>
        public async Task<double> GetCurrentAsync()
        {
            var iv = await InvokeMethod("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Get the zero-based index of the currently active (focused) match.
        /// </summary>
        /// <returns>The index of the active match, or <c>0</c> when there are no matches.</returns>
        public double GetCurrent()
        {
            var iv = InvokeMethodSync("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
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
        /// </summary>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public async Task NextAsync(IgbHighlightNavigation options)
        {
            await InvokeMethod("next", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Moves the active highlight to the next match.
        /// Wraps around to the first match after the last one.
        /// </summary>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public void Next(IgbHighlightNavigation options)
        {
            InvokeMethodSync("next", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }
        /// <summary>
        /// Moves the active highlight to the previous match.
        /// Wraps around to the last match when going back from the first one.
        /// </summary>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public async Task PreviousAsync(IgbHighlightNavigation options)
        {
            await InvokeMethod("previous", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Moves the active highlight to the previous match.
        /// Wraps around to the last match when going back from the first one.
        /// </summary>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public void Previous(IgbHighlightNavigation options)
        {
            InvokeMethodSync("previous", new object[] { ObjectToParam(options) }, new string[] { "Json" });
        }

        /// <summary>
        /// Moves the active highlight to the match at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the match to activate.</param>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public async Task SetActiveAsync(double index, IgbHighlightNavigation options)
        {
            await InvokeMethod("setActive", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }

        /// <summary>
        /// Moves the active highlight to the match at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the match to activate.</param>
        /// <param name="options">
        /// Optional navigation options, such as <see cref="IgbHighlightNavigation.PreventScroll"/>.
        /// </param>
        public void SetActive(double index, IgbHighlightNavigation options)
        {
            InvokeMethodSync("setActive", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }
        /// <summary>
        /// Re-runs the highlight search based on the current <see cref="SearchText"/>
        /// and <see cref="CaseSensitive"/> values.
        /// Call this method after the projected content changes dynamically (e.g. after lazy loading
        /// or programmatic DOM mutations) to ensure all matches are up to date.
        /// </summary>
        public async Task SearchAsync()
        {
            await InvokeMethod("search", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Re-runs the highlight search based on the current <see cref="SearchText"/>
        /// and <see cref="CaseSensitive"/> values.
        /// Call this method after the projected content changes dynamically (e.g. after lazy loading
        /// or programmatic DOM mutations) to ensure all matches are up to date.
        /// </summary>
        public void Search()
        {
            InvokeMethodSync("search", new object[] { }, new string[] { });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("CaseSensitive"))
            { ser.AddBooleanProp("caseSensitive", this._caseSensitive); }
            if (IsPropDirty("SearchText"))
            { ser.AddStringProp("searchText", this._searchText); }

        }

    }
}
