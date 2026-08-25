using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbStepper.ActiveStepChanged"/> event, raised after the
    /// active step has changed.
    /// </summary>
    public partial class IgbActiveStepChangedEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebActiveStepChangedEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbActiveStepChangedEventArgsDetail? _detail;

        /// <summary>
        /// The payload of the event, carrying the index of the step that became active.
        /// </summary>
        [Parameter]
        public IgbActiveStepChangedEventArgsDetail? Detail
        {
            get { return this._detail; }
            set
            {
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                if (value != null)
                {
                    this.AttachChild(value);
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
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbActiveStepChangedEventArgsDetail?)ConvertReturnValue(args["detail"], "ActiveStepChangedEventArgsDetail", true); }

            this.SuppressParentNotify = false;
        }

    }
}
