using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload of the <see cref="IgbStepper.ActiveStepChanging"/> event.
    /// </summary>
    public partial class IgbActiveStepChangingEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebActiveStepChangingEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private double _oldIndex = 0;

        /// <summary>
        /// The index of the step that is currently active.
        /// </summary>
        [Parameter]
        public double OldIndex
        {
            get { return this._oldIndex; }
            set
            {
                if (this._oldIndex != value || !IsPropDirty("OldIndex"))
                {
                    MarkPropDirty("OldIndex");
                }
                this._oldIndex = value;

            }
        }
        private double _newIndex = 0;

        /// <summary>
        /// The index of the step that is about to become active.
        /// </summary>
        [Parameter]
        public double NewIndex
        {
            get { return this._newIndex; }
            set
            {
                if (this._newIndex != value || !IsPropDirty("NewIndex"))
                {
                    MarkPropDirty("NewIndex");
                }
                this._newIndex = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("OldIndex"))
            { ser.AddNumberProp("oldIndex", this._oldIndex); }
            if (IsPropDirty("NewIndex"))
            { ser.AddNumberProp("newIndex", this._newIndex); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("OldIndex"))
            { args["oldIndex"] = (this._oldIndex).ToString(); }
            if (IsPropDirty("NewIndex"))
            { args["newIndex"] = (this._newIndex).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("oldIndex"))
            { this.OldIndex = ReturnToDouble(args["oldIndex"]); }
            if (args.ContainsKey("newIndex"))
            { this.NewIndex = ReturnToDouble(args["newIndex"]); }

            this.SuppressParentNotify = false;
        }

    }
}
