# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

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
