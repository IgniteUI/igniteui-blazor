namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for <see cref="IgbColorPicker{TValue}"/> events that carry a color.
    /// The meaning of <see cref="Detail"/> depends on the event that raises it.
    /// </summary>
    /// <typeparam name="TValue">The <c>TValue</c> of the <see cref="IgbColorPicker{TValue}"/> that raises the event.</typeparam>
    public partial class IgbColorPickerValueEventArgs<TValue> : BaseJsonSerializable
    {
        /// <inheritdoc />
        // The element sends the same string payload as for IgbComponentValueChangedEventArgs; only the decoding differs.
        internal override string RendererType { get { return "WebComponentValueChangedEventArgs"; } }

        private TValue? _detail;

        /// <summary>
        /// The color carried by the event, in the picker's <typeparamref name="TValue"/>.
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
            { ser.AddStringProp("detail", ColorPickerColorConverter.ToCss(this._detail)); }

        }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ColorPickerColorConverter.ToCss(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj))
            {
                this.Detail = ColorPickerColorConverter.FromCss<TValue>(ReturnToString(detailObj));
            }

            this.SuppressParentNotify = false;
        }

    }
}
