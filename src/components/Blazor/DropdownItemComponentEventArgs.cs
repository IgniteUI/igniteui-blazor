using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbDropdown.Change"/> event, carrying the
    /// <see cref="IgbDropdownItem"/> instance the event applies to.
    /// </summary>
    public partial class IgbDropdownItemComponentEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebDropdownItemComponentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbDropdownItem? _detail;

        /// <summary>
        /// The dropdown item that became selected.
        /// </summary>
        [Parameter]
        public IgbDropdownItem? Detail
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
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbDropdownItem)ConvertReturnValue(args["detail"], "DropdownItem", true); }

            this.SuppressParentNotify = false;
        }

    }
}
