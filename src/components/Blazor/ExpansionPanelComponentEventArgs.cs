using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the expansion panel open and close events, carrying the
    /// <see cref="IgbExpansionPanel"/> instance the event applies to.
    /// Raised by <see cref="IgbExpansionPanel"/> for itself and by <see cref="IgbAccordion"/> for its
    /// child panels.
    /// </summary>
    public partial class IgbExpansionPanelComponentEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebExpansionPanelComponentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbExpansionPanel? _detail;

        /// <summary>
        /// The expansion panel the event was raised for.
        /// </summary>
        [Parameter]
        public IgbExpansionPanel? Detail
        {
            get { return this._detail; }
            set
            {
                if (this._detail != value || !IsPropDirty("Detail"))
                {
                    MarkPropDirty("Detail");
                }
                this._detail = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.ContainsKey("detail"))
            { this.Detail = (IgbExpansionPanel?)ConvertReturnValue(args["detail"], "ExpansionPanel", true); }

            this.SuppressParentNotify = false;
        }

    }
}
