namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry a date payload.
    /// The meaning of Detail depends on the event that raises it.
    /// </summary>
    public partial class IgbComponentDateValueChangedEventArgs : IgbComponentDateValueChangedEventArgs<DateTime>
    {
    }

    /// <summary>
    /// Event arguments for component events that carry a date payload.
    /// The meaning of <see cref="Detail"/> depends on the event that raises it.
    /// </summary>
    public partial class IgbComponentDateValueChangedEventArgs<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComponentDateValueChangedEventArgs"; } }

        private protected TValue? _detail = default!;

        /// <summary>
        /// The date value carried by the event.
        /// </summary>
        public TValue? Detail
        {
            get { return this._detail; }
            set
            {
                if (!EqualityComparer<TValue>.Default.Equals(this._detail, value) || !IsPropDirty("Detail"))
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
            { ser.AddPrimitiveProp("detail", this._detail); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ReturnToString(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj))
            {
                this.Detail = GenericValueFromEventJson<TValue>(detailObj);
            }

            this.SuppressParentNotify = false;
        }

    }
}
