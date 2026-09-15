namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Preferred side and alignment of a popover (dropdown, select list, tooltip) relative to its anchor.
    /// </summary>
    public enum PopoverPlacement
    {
        /// <summary>Above the anchor, centered.</summary>
        Top,
        /// <summary>Above the anchor, aligned to its start edge.</summary>
        [WCEnumName("top-start")]
        TopStart,
        /// <summary>Above the anchor, aligned to its end edge.</summary>
        [WCEnumName("top-end")]
        TopEnd,
        /// <summary>Below the anchor, centered.</summary>
        Bottom,
        /// <summary>Below the anchor, aligned to its start edge.</summary>
        [WCEnumName("bottom-start")]
        BottomStart,
        /// <summary>Below the anchor, aligned to its end edge.</summary>
        [WCEnumName("bottom-end")]
        BottomEnd,
        /// <summary>To the right of the anchor, centered.</summary>
        Right,
        /// <summary>To the right of the anchor, aligned to its top edge.</summary>
        [WCEnumName("right-start")]
        RightStart,
        /// <summary>To the right of the anchor, aligned to its bottom edge.</summary>
        [WCEnumName("right-end")]
        RightEnd,
        /// <summary>To the left of the anchor, centered.</summary>
        Left,
        /// <summary>To the left of the anchor, aligned to its top edge.</summary>
        [WCEnumName("left-start")]
        LeftStart,
        /// <summary>To the left of the anchor, aligned to its bottom edge.</summary>
        [WCEnumName("left-end")]
        LeftEnd

    }
}
