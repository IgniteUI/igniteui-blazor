using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A virtual scroll component for large lists. Only the items visible in the
    /// viewport, plus a configurable overscan, are rendered.
    /// </summary>
    public partial class IgbVirtualScroll : BaseRendererControl
    {
        /// <inheritdoc />
        public override string Type { get { return "WebVirtualScroll"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbVirtualScrollModule.IsLoadRequested(IgBlazor))
            {
                IgbVirtualScrollModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        private string _dataRef;
        private Object _data;

        /// <summary>
        /// The array of items to virtualize.
        /// Compared by reference: a mutation in place causes no update; assign a new
        /// collection instead. The <see cref="DataRequest"/> flow also expects a new collection.
        /// </summary>
        [Parameter]
        public Object Data
        {
            get { return this._data; }

            set
            {
                var oldValue = this._data;

                if (oldValue != value || !IsPropDirty("Data"))
                {
                    MarkPropDirty("Data");
                    this._data = value;
                    this.OnRefChanged("Data", oldValue, value, false, false, (string refName, object old, object newValue) =>
                    {
                        this._dataRef = refName;
                        this.MarkPropDirty("DataRef");
                    });
                }
            }
        }

        private string _dataScript;

        ///<summary>Provides a means of setting Data in the JavaScript environment.</summary>
        [Parameter]
        public string DataScript
        {
            get { return _dataScript; }

            set
            {
                var oldValue = this._dataScript;
                if (oldValue != value || !IsPropDirty("Data"))
                {
                    this._dataScript = value;
                    MarkPropDirty("Data");
                    this.OnRefChanged("Data", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._dataRef = refName;
                        this.MarkPropDirty("DataRef");
                    });
                }
            }
        }
        private ContentOrientation _orientation = ContentOrientation.Vertical;

        /// <summary>
        /// Scroll orientation of the virtual scroll.
        /// </summary>
        [Parameter]
        public ContentOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                if (this._orientation != value || !IsPropDirty("Orientation"))
                {
                    MarkPropDirty("Orientation");
                }
                this._orientation = value;

            }
        }
        private double _overScan = 2;

        /// <summary>
        /// Number of extra items to render beyond the visible area of the viewport.
        /// Higher values reduce blank flashes during fast scrolling but can lower performance.
        /// </summary>
        [Parameter]
        public double OverScan
        {
            get { return this._overScan; }
            set
            {
                if (this._overScan != value || !IsPropDirty("OverScan"))
                {
                    MarkPropDirty("OverScan");
                }
                this._overScan = value;

            }
        }
        private double _estimatedItemSize = 50;

        /// <summary>
        /// Estimated item size in pixels, used before an item is measured in the DOM.
        /// After the first render of an item, the component replaces the estimate with the measured size.
        /// </summary>
        [Parameter]
        public double EstimatedItemSize
        {
            get { return this._estimatedItemSize; }
            set
            {
                if (this._estimatedItemSize != value || !IsPropDirty("EstimatedItemSize"))
                {
                    MarkPropDirty("EstimatedItemSize");
                }
                this._estimatedItemSize = value;

            }
        }
        private string _itemTemplateRef;
        private string _itemTemplateScript;

        /// <summary>
        /// Name of a client-side function that renders the template used for the content
        /// of each item in the virtual scroll list. The function receives the item context
        /// (<c>value</c>, <c>index</c>, <c>count</c>) and returns a template built with
        /// <c>window.igTemplating.html</c>. Without a template, nothing is rendered.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyTemplate", function (ctx) { return ...; }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ItemTemplateScript
        {
            get { return _itemTemplateScript; }

            set
            {
                var oldValue = this._itemTemplateScript;
                if (oldValue != value || !IsPropDirty("ItemTemplate"))
                {
                    this._itemTemplateScript = value;
                    MarkPropDirty("ItemTemplate");
                    this.OnRefChanged("ItemTemplate", oldValue, value, true, false, (string refName, object old, object newValue) =>
                    {
                        this._itemTemplateRef = refName;
                        this.MarkPropDirty("ItemTemplateRef");
                    });
                }
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
        /// <summary>
        /// Scrolls to the specified item index.
        /// Items outside the rendered window have only an estimated size, so the component
        /// measures the items at the landing point and corrects the scroll position until
        /// the offset is stable.
        /// </summary>
        public async Task ScrollToIndexAsync(double index)
        {
            await InvokeMethod("scrollToIndex", new object[] { index }, new string[] { "Number" });
        }

        /// <summary>
        /// Scrolls to the specified item index.
        /// Items outside the rendered window have only an estimated size, so the component
        /// measures the items at the landing point and corrects the scroll position until
        /// the offset is stable.
        /// </summary>
        public void ScrollToIndex(double index)
        {
            InvokeMethodSync("scrollToIndex", new object[] { index }, new string[] { "Number" });
        }
        /// <summary>
        /// Scrolls to the specified item index, with a configurable alignment and scroll behavior.
        /// </summary>
        public async Task ScrollToIndexAsync(double index, IgbScrollIntoViewOptions options)
        {
            await InvokeMethod("scrollToIndex", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }

        /// <summary>
        /// Scrolls to the specified item index, with a configurable alignment and scroll behavior.
        /// </summary>
        public void ScrollToIndex(double index, IgbScrollIntoViewOptions options)
        {
            InvokeMethodSync("scrollToIndex", new object[] { index, ObjectToParam(options) }, new string[] { "Number", "Json" });
        }

        private string _stateChangeRef = null;
        private string _stateChangeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="StateChange"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string StateChangeScript
        {

            set
            {
                if (value != this._stateChangeScript)
                {
                    this._stateChangeScript = value;
                    this.OnRefChanged("StateChange", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._stateChangeRef = refName;
                        this.MarkPropDirty("StateChangeRef");
                    });
                }
            }
            get
            {
                return this._stateChangeScript;
            }
        }

        private EventCallback<IgbVirtualScrollStateChangeEventArgs>? _stateChange = null;

        /// <summary>
        /// Emitted when the rendered virtual window changes, with a snapshot of the
        /// current virtual window.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVirtualScrollStateChangeEventArgs> StateChange
        {
            get
            {
                return this._stateChange != null ? this._stateChange.Value : EventCallback<IgbVirtualScrollStateChangeEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_stateChange))
                    {
                        _stateChange = value;
                        this.SetHandler<IgbVirtualScrollStateChangeEventArgs>(this.Name, "StateChange", value);
                        this.OnRefChanged("StateChange", null, "event:::StateChange", true, false, (refName, oldValue, newValue) =>
                        {
                            this._stateChangeRef = refName;
                            this.MarkPropDirty("StateChangeRef");
                        });
                    }
                }
                else
                {
                    _stateChange = null;
                    this.SetHandler<IgbVirtualScrollStateChangeEventArgs>(this.Name, "StateChange", null);
                    this.OnRefChanged("StateChange", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._stateChangeRef = null;
                        this.MarkPropDirty("StateChangeRef");
                    });
                }
            }
        }

        private string _dataRequestRef = null;
        private string _dataRequestScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="DataRequest"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string DataRequestScript
        {

            set
            {
                if (value != this._dataRequestScript)
                {
                    this._dataRequestScript = value;
                    this.OnRefChanged("DataRequest", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._dataRequestRef = refName;
                        this.MarkPropDirty("DataRequestRef");
                    });
                }
            }
            get
            {
                return this._dataRequestScript;
            }
        }

        private EventCallback<IgbVirtualScrollDataRequestEventArgs>? _dataRequest = null;

        /// <summary>
        /// Emitted when the scroll position comes near the end of the loaded data.
        /// Also emitted on the first render, when the loaded items do not fill the viewport.
        /// Use it for infinite scroll and for remote data.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVirtualScrollDataRequestEventArgs> DataRequest
        {
            get
            {
                return this._dataRequest != null ? this._dataRequest.Value : EventCallback<IgbVirtualScrollDataRequestEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_dataRequest))
                    {
                        _dataRequest = value;
                        this.SetHandler<IgbVirtualScrollDataRequestEventArgs>(this.Name, "DataRequest", value);
                        this.OnRefChanged("DataRequest", null, "event:::DataRequest", true, false, (refName, oldValue, newValue) =>
                        {
                            this._dataRequestRef = refName;
                            this.MarkPropDirty("DataRequestRef");
                        });
                    }
                }
                else
                {
                    _dataRequest = null;
                    this.SetHandler<IgbVirtualScrollDataRequestEventArgs>(this.Name, "DataRequest", null);
                    this.OnRefChanged("DataRequest", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._dataRequestRef = null;
                        this.MarkPropDirty("DataRequestRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("DataRef"))
            { ser.AddStringProp("dataRef", this._dataRef); }
            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }
            if (IsPropDirty("OverScan"))
            { ser.AddNumberProp("overScan", this._overScan); }
            if (IsPropDirty("EstimatedItemSize"))
            { ser.AddNumberProp("estimatedItemSize", this._estimatedItemSize); }
            if (IsPropDirty("ItemTemplateRef"))
            { ser.AddStringProp("itemTemplateRef", this._itemTemplateRef); }
            if (IsPropDirty("StateChangeRef"))
            { ser.AddStringProp("stateChangeRef", this._stateChangeRef); }
            if (IsPropDirty("DataRequestRef"))
            { ser.AddStringProp("dataRequestRef", this._dataRequestRef); }

        }

    }
}
