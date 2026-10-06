namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the expansion panel open and close events, carrying the
    /// <see cref="IgbExpansionPanel"/> instance the event applies to.
    /// Raised by <see cref="IgbExpansionPanel"/> for itself and by <see cref="IgbAccordion"/> for its
    /// child panels.
    /// </summary>
    public partial class IgbExpansionPanelComponentEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebExpansionPanelComponentEventArgs"; } }

        private IgbExpansionPanel _detail = new IgbExpansionPanel();

        /// <summary>
        /// The expansion panel the event was raised for.
        /// </summary>
        public IgbExpansionPanel Detail
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
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj) && ConvertReturnValue(detailObj, "ExpansionPanel", true) is IgbExpansionPanel detail)
            { this.Detail = detail; }

            this.SuppressParentNotify = false;
        }

    }
}
