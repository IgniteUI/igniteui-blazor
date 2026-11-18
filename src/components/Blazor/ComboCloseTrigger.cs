namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The user action that closes the list of an <see cref="IgbCombo{TValue, TItem}"/>.
    /// </summary>
    public enum ComboCloseTrigger
    {
        /// <summary>The Enter key.</summary>
        Enter,
        /// <summary>The Escape key.</summary>
        Escape,
        /// <summary>The Tab key.</summary>
        Tab,
        /// <summary>The Arrow Up key on the first option.</summary>
        [WCEnumName("arrow-up")]
        ArrowUp,
        /// <summary>A click outside the combo.</summary>
        [WCEnumName("outside-click")]
        OutsideClick,
        /// <summary>A scroll of the page.</summary>
        Scroll,
        /// <summary>A click on the input or the toggle icon.</summary>
        Toggle,
        /// <summary>An item click in single selection mode.</summary>
        Selection,
        /// <summary>The Apply button of the confirm mode.</summary>
        Apply,
        /// <summary>The Cancel button of the confirm mode.</summary>
        Cancel

    }
}
