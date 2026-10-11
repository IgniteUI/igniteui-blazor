namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry a date payload, without the component's type argument.
    /// The meaning of Detail depends on the event that raises it.
    /// </summary>
    [Obsolete("Use IgbComponentDateValueChangedEventArgs<TValue>, with the TValue of the component.")]
    public abstract partial class IgbComponentDateValueChangedEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComponentDateValueChangedEventArgs"; } }

        private DateTime? _detail;

        /// <summary>
        /// The date value carried by the event.
        /// </summary>
        public DateTime? Detail
        {
            get { return this._detail; }
        }

        private protected void SetDateDetail(DateTime? value)
        {
            MarkPropDirty("Detail");
            this._detail = value;
        }

        private protected abstract void ReadDetail(object? detailObj);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddPrimitiveProp("detail", this._detail); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = GenericValueString(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj))
            {
                this.ReadDetail(detailObj);
            }

            this.SuppressParentNotify = false;
        }
    }

#pragma warning disable CS0618 // The obsolete base keeps handlers written against IgbComponentDateValueChangedEventArgs binding to the date events.
    /// <summary>
    /// Event arguments for component events that carry a date payload.
    /// The meaning of <see cref="Detail"/> depends on the event that raises it.
    /// </summary>
    /// <typeparam name="TValue">The <c>TValue</c> of the date component that raises the event.</typeparam>
    public partial class IgbComponentDateValueChangedEventArgs<TValue> : IgbComponentDateValueChangedEventArgs
#pragma warning restore CS0618
    {
        private TValue? _detail;

        /// <summary>
        /// The date value carried by the event.
        /// </summary>
        public new TValue? Detail
        {
            get { return this._detail; }
        }

        private protected override void ReadDetail(object? detailObj)
        {
            this._detail = GenericValueFromEventJson<TValue>(detailObj);
            // A string TValue holds the date in round-trip format.
            this.SetDateDetail(this._detail switch
            {
                DateTime date => date,
                string text when DateTime.TryParse(text, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind, out var parsed) => parsed,
                _ => null
            });
        }
    }
}
