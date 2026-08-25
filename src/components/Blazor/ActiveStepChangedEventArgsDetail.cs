using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbStepper.ActiveStepChanged"/> event.
    /// </summary>
    public partial class IgbActiveStepChangedEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebActiveStepChangedEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private double _index = 0;

        /// <summary>
        /// The index of the step that became active.
        /// </summary>
        [Parameter]
        public double Index
        {
            get { return this._index; }
            set
            {
                if (this._index != value || !IsPropDirty("Index"))
                {
                    MarkPropDirty("Index");
                }
                this._index = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Index"))
            { ser.AddNumberProp("index", this._index); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Index"))
            { args["index"] = (this._index).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("index"))
            { this.Index = ReturnToDouble(args["index"]); }

            this.SuppressParentNotify = false;
        }

    }
}
