namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Selection mode of an <see cref="IgbButtonGroup"/>.
    /// </summary>
    public enum ButtonGroupSelection
    {
        /// <summary>At most one button is selected. Clicking the selected button deselects it.</summary>
        Single,
        /// <summary>Exactly one button stays selected. Clicking the selected button leaves it selected.</summary>
        [WCEnumName("single-required")]
        SingleRequired,
        /// <summary>Any number of buttons can be selected.</summary>
        Multiple

    }
}
