using Microsoft.AspNetCore.Components;

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
    public partial class IgbComponentDateValueChangedEventArgs<T> : BaseRendererElement
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebComponentDateValueChangedEventArgs"; } }

        private protected T _detail = default!;

        /// <summary>
        /// The date value carried by the event.
        /// </summary>
        [Parameter]
        public T Detail
        {
            get { return this._detail; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this._detail, value) || !IsPropDirty("Detail"))
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
        internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ReturnToString(this._detail); }

        }

        /// <inheritdoc />
        internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.TryGetValue("detail", out var detailObj))
            {
                var value = ConvertReturnValue(detailObj);
                if (value is null)
                {
                    this.Detail = default!;
                }
                else
                {
                    var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                    this.Detail = (T)Convert.ChangeType(value, targetType);
                }
            }

            this.SuppressParentNotify = false;
        }

    }
}
