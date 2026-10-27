namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for component events that carry no payload, such as the focus and blur
    /// notifications of the input controls or the opening, opened, closing and closed notifications
    /// of the components that show an overlay.
    /// </summary>
    public partial class IgbVoidEventArgs : BaseJsonSerializable
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "VoidEventArgs"; } }

        /// <inheritdoc />
        internal override void ToEventJson(IgbComponentBase control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

        }

        /// <inheritdoc />
        internal override void FromEventJson(IgbComponentBase control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            this.SuppressParentNotify = false;
        }

    }
}
