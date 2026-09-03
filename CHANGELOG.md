# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

This release updates Ignite UI for Blazor to the latest [igniteui-webcomponents@7.3.0 release](https://github.com/IgniteUI/igniteui-webcomponents/releases/tag/7.3.0) with highlights noted below:

### Added

#### New Components

- `IgbColorPicker` - A color input component. Users pick a color with the HSV saturation/value canvas, the hue slider and the optional alpha slider, or type a color string (hex, rgb(a), hsl(a) or a named CSS color). Supports two-way binding via `@bind-Value`, pre-defined `Swatches`, a trigger-button or editable-input anchor (`Mode`), and the native EyeDropper API where the browser provides one.
- `IgbQrCode` - Renders a scannable QR code as an SVG from the `Value` property. Supports an explicit `Version` (1-40) and `ErrorLevel`, `Size` and `Margin` (quiet zone), `DotStyle`/`SquareStyle` shapes, an optional centered logo (`LogoSrc`, `LogoSize`, `LogoMargin`), and theming via CSS custom properties. [#2308](https://github.com/IgniteUI/igniteui-webcomponents/pull/2308)

#### Chip
- New `Outlined` property. When set, the chip shows an outlined style. [#2307](https://github.com/IgniteUI/igniteui-webcomponents/pull/2307)

#### Splitter
- New `StartCollapsed` and `EndCollapsed` properties. Use them to read and to set the collapsed state of each pane.
- New `LayoutChanged` event. Emitted after a user-driven resize or expansion change, with a full snapshot of the current layout (`StartSize`, `EndSize`, `StartCollapsed`, `EndCollapsed`).

#### Tabs
- New `GetSelectedTab` / `GetSelectedTabAsync` methods. They return the selected `IgbTab`, or `null` when no tab is selected.
- `Select()` now also matches the `Label` of a tab, in addition to its IDREF.

#### Icon
- `RegisterIcon` and `RegisterIconFromText` now accept an `IgbRegisterIconOptions` argument, in addition to the plain collection string. `StripMeta = true` removes the `<title>` and `<desc>` elements from the stored SVG, preventing the native browser tooltip on hover; the title text stays available as the `aria-label` of the host icon element. [#1822](https://github.com/IgniteUI/igniteui-webcomponents/issues/1822)

#### Mask Input, Date Time Input, Date Range Picker
- The masked editors now support the standard undo and redo shortcuts: `Ctrl + Z` / `Cmd + Z` to undo, and `Ctrl + Y`, `Ctrl + Shift + Z` / `Cmd + Shift + Z` to redo.

### Changed

- **Date Time Input, Date Picker, Date Range Picker:** the components no longer change `Value` while the user types. `Value` now holds only a committed value and changes together with the `Change` event, which the components still emit on blur. The value being typed is available in the detail of the `Input` event. Thus the components can be used in templates that bind `Value` externally, such as grid edit templates. [#1346](https://github.com/IgniteUI/igniteui-webcomponents/issues/1346)
- **Dropdown:** the component no longer emits `Change` when the item that is already selected is selected again. The list still closes, as before.
- **Button Group:** a disabled group no longer sets `Disabled` on its buttons - the buttons inherit the state. A button that is disabled on its own stays disabled when the group is enabled again.
- **Tooltip:** a tooltip that closes from a hide trigger now waits exactly `HideDelay` (an undocumented extra 180 ms stage was removed); in sticky mode the default close button hides the tooltip immediately; and `focusin` / `focusout` are now part of the default show/hide triggers, so a tooltip opens when its anchor gets keyboard focus.
- **Tabs:** the scroll buttons now scroll to the nearest tab that is not fully visible, instead of a fixed step of 180px.

### Fixed

For the complete list of fixes arriving with the updated web components, see the [igniteui-webcomponents 7.3.0 release notes](https://github.com/IgniteUI/igniteui-webcomponents/releases/tag/7.3.0) - highlights include per-element selection tracking in Button Group, correct `WeekStart` on the Calendar's initial render, form-associated components keeping their validation messages after a failed form submission, Highlight painting matches in recent Firefox versions, Select keyboard-navigation and type-ahead fixes, significantly faster large Tree operations, and Tooltip show/hide race fixes.

## 0.1.0 - 2026-07-14

This release updates the Ignite UI for Blazor to the latest [igniteui-webcomponents@7.2.4 release](https://github.com/IgniteUI/igniteui-webcomponents/releases/tag/7.2.4) and matching related changes from `IgniteUI.Blazor` [25.2.77 (March 2026)](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/general-changelog-dv-blazor#25277-march-2026), [25.2.102 (May 2026)](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/general-changelog-dv-blazor#252102-may-2026) and [26.1.51 (June 2026)](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/general-changelog-dv-blazor#26151-june-2026) with highlights noted below:

### Added

#### New Components
<!-- From 26.1.51 (June 2026) -->
- [IgbChat](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/interactivity/chat) (preview) - A Chat UI component for displaying messages and input interaction. This component is in preview and under active development. Some features are not yet implemented, and APIs may evolve in upcoming releases.
- [IgbSplitter](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/layouts/splitter) - The Splitter component provides a resizable split-pane layout that divides the view into two panels — *start* and *end* — separated by a draggable bar.
- [IgbHighlight](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/inputs/highlight) - The Highlight component provides efficient searching and highlighting of text projected into it via its default slot.
<!-- From 25.2.77 (March 2026) -->
- `IgbThemeProvider` - allows scoping themes to specific page sections using Lit's context API, enabling multiple themes on a single page. Works in both Shadow and Light DOM.

#### Badge
 - New dot type, improved outline implementation following WCAG AA accessibility standards and theme based sizing. [#1889](https://github.com/IgniteUI/igniteui-webcomponents/pull/1889)
#### Checkbox
  - New --tick-width CSS property. [#1897](https://github.com/IgniteUI/igniteui-webcomponents/pull/1897)
#### Combo
  - New disableClear property which disables the clear button of the combo component. [#1896](https://github.com/IgniteUI/igniteui-webcomponents/pull/1896)
#### Mask input
  - Transform unicode digit code points to ASCII numbers for numeric patterns. [#1907](https://github.com/IgniteUI/igniteui-webcomponents/pull/1907)

<!-- From 26.1.51 (June 2026) -->
#### AI Skills
- Ignite UI for Blazor now provides 4 skills for improving AI assistants coding results. Please, find more information in the [AI Skills documentation](./ai/skills.md).


### Fixed

<!-- From 25.2.77 (March 2026) -->
| Bug Number | Control | Description |
|------------|---------|-------------|
| [#2079](https://github.com/IgniteUI/igniteui-webcomponents/pull/2079) | Calendar | `aria-hidden` state for weeks outside of the current month |
| [#2078](https://github.com/IgniteUI/igniteui-webcomponents/pull/2078) | Date Picker | CSS border for slotted actions in dialog mode |
| [#2068](https://github.com/IgniteUI/igniteui-webcomponents/pull/2068) | Input | Placeholder color on focus |
| [#2073](https://github.com/IgniteUI/igniteui-webcomponents/pull/2073) | Input | CSS border when suffix slot content is present |
| [#2069](https://github.com/IgniteUI/igniteui-webcomponents/pull/2069) | Textarea | Align bottom padding to the design system |
| [#2063](https://github.com/IgniteUI/igniteui-webcomponents/pull/2063) | Validation | Slotted validation text follows the current theme |
| [#2059](https://github.com/IgniteUI/igniteui-webcomponents/pull/2059) | Tile Manager | Header is hidden only when there is no content and maximize/fullscreen are disabled |
| [#2061](https://github.com/IgniteUI/igniteui-webcomponents/pull/2061) | Theming | Resolve initial theme based on document computed styles rather than stylesheets |
| [#2030](https://github.com/IgniteUI/igniteui-webcomponents/pull/2030) | Calendar | Focus styles for month/year views |
| [#1965](https://github.com/IgniteUI/igniteui-webcomponents/pull/1965) | Combo | Notch border styles |
| [#1964](https://github.com/IgniteUI/igniteui-webcomponents/pull/1964) | Checkbox & Switch | Internal ripple opacity when hovering over slotted content in the `helper-text` slot |
| [#1947](https://github.com/IgniteUI/igniteui-webcomponents/pull/1947) | Dialog | Underlying dialog element now has `display: contents` and won't participate in DOM layout |
| [#1986](https://github.com/IgniteUI/igniteui-webcomponents/pull/1986) | Dialog | `keepOpenOnEscape` not preventing the dialog from closing when Escape is pressed |
| [#1997](https://github.com/IgniteUI/igniteui-webcomponents/pull/1997) | Dialog | Base styles and theming |
| [#1985](https://github.com/IgniteUI/igniteui-webcomponents/pull/1985) | List & List Item | Added missing styles for slotted `igc-icon` in the list item |
| [#2010](https://github.com/IgniteUI/igniteui-webcomponents/pull/2010) | List & List Item | Icon and icon button sizes for the Indigo theme |
| [#2006](https://github.com/IgniteUI/igniteui-webcomponents/pull/2006) | Mask Input | Auto-fill behavior for mask patterns with literals |
| [#1956](https://github.com/IgniteUI/igniteui-webcomponents/pull/1956) | Navbar | Icon and icon button sizes |
| [#1957](https://github.com/IgniteUI/igniteui-webcomponents/pull/1957) | Select | Color for outlined type |
| [#1998](https://github.com/IgniteUI/igniteui-webcomponents/pull/1998) | Tabs | Add active pseudo-elements backgrounds for the active tab in Material theme |
| [#2008](https://github.com/IgniteUI/igniteui-webcomponents/pull/2008) | Tabs | Take scale factor when positioning the active tab indicator |
| [#2028](https://github.com/IgniteUI/igniteui-webcomponents/pull/2028) | Tabs | Selected indicator alignment |
| [#1828](https://github.com/IgniteUI/igniteui-webcomponents/issues/1828) | Tooltip | Do not show the tooltip when the tooltip target is clicked |
| [#1936](https://github.com/IgniteUI/igniteui-webcomponents/pull/1936) | Tooltip | Removed the max-width constraint for slotted content |
| 2754 <!-- From 25.2.102 (May 2026) --> | IgbTabs | Changing the check state for IgbSwitch inside the tab causes the tab content to disappear |
