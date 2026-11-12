namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Placement of the value label of an <see cref="IgbLinearProgress"/> relative to its bar.
    /// </summary>
    public enum LinearProgressLabelAlign
    {
        /// <summary>Above the bar, aligned to the start.</summary>
        [WCEnumName("top-start")]
        TopStart,
        /// <summary>Above the bar, centered.</summary>
        Top,
        /// <summary>Above the bar, aligned to the end.</summary>
        [WCEnumName("top-end")]
        TopEnd,
        /// <summary>Below the bar, aligned to the start.</summary>
        [WCEnumName("bottom-start")]
        BottomStart,
        /// <summary>Below the bar, centered.</summary>
        Bottom,
        /// <summary>Below the bar, aligned to the end.</summary>
        [WCEnumName("bottom-end")]
        BottomEnd

    }
}
