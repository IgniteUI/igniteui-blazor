namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events whose payload is a single number.
    /// </summary>
    public partial class IgbNumberEventArgs : IgbNumberEventArgs<double>
    {
    }

    /// <summary>Event arguments carrying a typed numeric payload.</summary>
    public partial class IgbNumberEventArgs<TValue> : BaseJsonSerializable
    {
        private readonly Type genericType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

        /// <inheritdoc />
        internal override string RendererType { get { return "WebNumberEventArgs"; } }

        private TValue _detail = default!;

        /// <summary>
        /// The numeric payload of the event. Its meaning depends on the event that carries it, for
        /// example the new value of the control or the index of the affected item.
        /// </summary>
        public TValue Detail
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
                var value = ConvertReturnValue(detailObj);
                this.Detail = value is null
                    ? default!
                    : (TValue)Convert.ChangeType(value, genericType, System.Globalization.CultureInfo.InvariantCulture);
            }

            this.SuppressParentNotify = false;
        }

    }
}
