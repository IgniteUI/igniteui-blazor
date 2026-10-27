namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Element a notification component (toast, snackbar) is positioned relative to.
    /// </summary>
    public enum NotificationPositioning
    {
        /// <summary>The viewport. Ancestor elements are ignored.</summary>
        Viewport,
        /// <summary>The nearest visible ancestor. The component is constrained to that ancestor's bounding box.</summary>
        Container

    }
}
