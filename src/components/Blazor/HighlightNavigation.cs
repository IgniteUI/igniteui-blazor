using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Options for controlling navigation behavior when moving the active highlight.
    /// </summary>
    public partial class IgbHighlightNavigation : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebHighlightNavigation"; } }

        private static bool _marshalByValue = true;

        private bool _preventScroll = false;

        /// <summary>
        /// When <see langword="true"/>, prevents the component from scrolling the new active match into view.
        /// </summary>
        [Parameter]
        public bool PreventScroll
        {
            get { return this._preventScroll; }
            set
            {
                if (this._preventScroll != value || !IsPropDirty("PreventScroll"))
                {
                    MarkPropDirty("PreventScroll");
                }
                this._preventScroll = value;

            }
        }

        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object?[] { ObjectToParam(element) }, new string[] { "Json" });
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("PreventScroll"))
            { ser.AddBooleanProp("preventScroll", this._preventScroll); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("PreventScroll"))
            { args["preventScroll"] = (this._preventScroll).ToString().ToLower(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("preventScroll"))
            { this.PreventScroll = ReturnToBoolean(args["preventScroll"]); }

            this.SuppressParentNotify = false;
        }

    }
}
