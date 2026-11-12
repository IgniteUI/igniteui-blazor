namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// How keyboard navigation between tab headers selects a tab.
    /// </summary>
    public enum TabsActivation
    {
        /// <summary>The focused tab is selected immediately and its panel shown.</summary>
        Auto,
        /// <summary>Navigation only moves focus. Space or Enter selects the focused tab.</summary>
        Manual

    }
}
