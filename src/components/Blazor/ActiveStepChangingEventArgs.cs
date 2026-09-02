using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbStepper.ActiveStepChanging"/> event, raised before the
    /// active step changes.
    /// </summary>
    public partial class IgbActiveStepChangingEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebActiveStepChangingEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbActiveStepChangingEventArgsDetail _detail = new IgbActiveStepChangingEventArgsDetail();

        /// <summary>
        /// The payload of the event, carrying the index of the currently active step and the index of
        /// the step that is about to become active.
        /// </summary>
        [Parameter]
        public IgbActiveStepChangingEventArgsDetail Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                this._detail = value;
                if (value != null)
                {
                    this.AttachChild(value);
                }
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
            { this.Detail = (IgbActiveStepChangingEventArgsDetail)ConvertReturnValue(args["detail"], "ActiveStepChangingEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
