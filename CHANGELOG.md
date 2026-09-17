# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

## 0.2.0 - 2026-09-17

This release updates Ignite UI for Blazor to the latest [igniteui-webcomponents@7.3.2 release](https://github.com/IgniteUI/igniteui-webcomponents/releases/tag/7.3.2) with highlights noted below:

### Added

#### New Components

- `IgbColorPicker` - A color input component. Users pick a color with the HSV saturation/value canvas, the hue slider and the optional alpha slider, or type a color string (hex, rgb(a), hsl(a) or a named CSS color). Supports two-way binding via `@bind-Value`, pre-defined `Swatches`, a trigger-button or editable-input anchor (`Mode`), and the native EyeDropper API where the browser provides one.
- `IgbQrCode` - Renders a scannable QR code as an SVG from the `Value` property. Supports an explicit `Version` (1-40) and `ErrorLevel`, `Size` and `Margin` (quiet zone), `DotStyle`/`SquareStyle` shapes, an optional centered logo (`LogoSrc`, `LogoSize`, `LogoMargin`), and theming via CSS custom properties. [#2308](https://github.com/IgniteUI/igniteui-webcomponents/pull/2308)

#### QR Code
- New `ToImage` / `ToImageAsync` methods. They export the code as an image file in `Svg`, `Png`, `Jpeg` or `Webp` format via `IgbQrCodeExportOptions`. The `Scale` option multiplies the component `Size` - a 256px code with a scale of 2 exports as a 512x512 image - and the file is delivered to the user through the browser download dialog. [#2367](https://github.com/IgniteUI/igniteui-webcomponents/pull/2367)

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

#### Packaging and release
- Public ES module at `_content/IgniteUI.Blazor/api.js` (typings at `api.d.ts`): `registerScript(name, fn, shouldCall = false)` / `removeScript(name)` for `*Script` component parameters, and `html` — the lit-html template tag for client templates, the same instance the components render with. Importing these functions is now the preferred way to register `*Script` parameters:

  ```js
  import { registerScript, html } from './_content/IgniteUI.Blazor/api.js';

  registerScript('CityItem', ({ item }) => html`<b>${item.Name}</b>`);
  ```
- Every release now publishes an SPDX 2.2 SBOM, an SPDX 3.0 SBOM, and a CycloneDX SBOM covering the NuGet and npm dependencies the package actually ships, plus three Sigstore attestations — build provenance, the SPDX SBOM, and the CycloneDX SBOM — each bound to the SHA-256 digest of the signed package. All of it is attached to the GitHub release next to the package and its checksum. Verify with `gh attestation verify <package>.nupkg -R IgniteUI/igniteui-blazor`.
- Every release additionally scans the NuGet and npm dependencies it actually ships and attaches the report to the GitHub release.
- XML documentation was expanded across public component APIs and existing enums for improved discoverability in IDEs and generated API documentation.

### Changed

- **Chip:** the `Remove` event now carries `IgbVoidEventArgs` instead of `IgbComponentBoolValueChangedEventArgs`, matching the corrected `igcRemove: CustomEvent<void>` typing of the web component - the event never carried a boolean detail. Update `Remove` handler signatures accordingly.
- **Date Time Input, Date Picker, Date Range Picker:** the components no longer change `Value` while the user types. `Value` now holds only a committed value and changes together with the `Change` event, which the components still emit on blur. The value being typed is available in the detail of the `Input` event. Thus the components can be used in templates that bind `Value` externally, such as grid edit templates. [#1346](https://github.com/IgniteUI/igniteui-webcomponents/issues/1346)
- **Calendar, Date Picker, Date Range Picker:** when `WeekStart` is not set, the week starts on the first day of the week of the `Locale`, as reported by the browser's `Intl.Locale` week info - for example `bg` starts on Monday while `en` stays on Sunday. An explicit `WeekStart` has priority, and browsers without week-info support keep the Sunday default. The header date and the month/year navigation also follow the field order of the locale. [#1020](https://github.com/IgniteUI/igniteui-webcomponents/issues/1020) [#1712](https://github.com/IgniteUI/igniteui-webcomponents/issues/1712)
- **Dropdown:** the component no longer emits `Change` when the item that is already selected is selected again. The list still closes, as before.
- **Button Group:** a disabled group no longer sets `Disabled` on its buttons - the buttons inherit the state. A button that is disabled on its own stays disabled when the group is enabled again.
- **Tooltip:** a tooltip that closes from a hide trigger now waits exactly `HideDelay` (an undocumented extra 180 ms stage was removed); in sticky mode the default close button hides the tooltip immediately; and `focusin` / `focusout` are now part of the default show/hide triggers, so a tooltip opens when its anchor gets keyboard focus.
- **Tabs:** the scroll buttons now scroll to the nearest tab that is not fully visible, instead of a fixed step of 180px.
- **Binary compatibility:** shipped assemblies are now strong-name signed. This changes the assembly identity, so `PublicKeyToken` moves from null to `7dd5c3163f2cd0cb`. Normal NuGet consumers that rebuild should require no source changes, but precompiled dependents, binding redirects, and explicit fully-qualified assembly references may need updating.
- The client build now emits native ES modules; the package's JS initializer loads the whole script graph during Blazor startup, so no `<script src="_content/IgniteUI.Blazor/app.bundle.js">` tag is needed on any hosting model (Blazor Server, standalone WASM, Blazor Web App, BlazorWebView). Existing tags keep working — including wrapped in `@Assets[...]`. When removing the tag, use the preferred `api.js` module above to register `*Script` parameters; the deprecated globals are not available until the library loads unless the tag is present to queue earlier calls.
- A component whose `*Script` parameter names a function that is not registered now logs a console warning naming it (previously the parameter was silently dropped).
- Hosting the library's assets elsewhere (CDN/self-host) is done with a standard [import map](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/static-files?view=aspnetcore-10.0#importmap-component) prefix entry — e.g. `{"imports": {"./_content/IgniteUI.Blazor/": "https://cdn.example.com/ig/"}}` relocates the entire module graph, initializer included.
- Authenticode signatures are now validated against a repository-pinned certificate fingerprint allowlist (`eng/IG.authenticode-certificates.sha256`) rather than only checking that a signature is valid.
- Nullable reference type analysis is enabled for the public API, making nullability contracts explicit for consumers.
- `IgniteUI.Blazor.Lite` is now trim-compatible, including the required serializer and reflection annotations.

### Deprecated

- The `window.igRegisterScript`, `window.igRemoveScript`, and `window.igTemplating.html` globals — use the `api.js` module exports instead. The globals keep working with the same signatures and log a one-time console notice; they exist once `api.js` has loaded, so a classic script calling them while the page parses still needs the `app.bundle.js` tag, which queues those calls. Note the defaults differ: `igRegisterScript` still defaults `shouldCall` to `true` (call the function and use its result), `registerScript` defaults to `false` (the function is the script) so passing one explicitly in most cases is no longer needed.

### Fixed

- Fixed `FocusComponent` / `FocusComponentAsync` and `BlurComponent` / `BlurComponentAsync` methods throwing or not working on supported components. [#297](https://github.com/IgniteUI/igniteui-blazor/issues/297) [#428](https://github.com/IgniteUI/igniteui-blazor/pull/428)
- Loading no longer breaks under .NET 9+ static asset fingerprinting — a script tag wrapped in `@Assets[...]` previously left the app blank with no error. [#233](https://github.com/IgniteUI/igniteui-blazor/issues/233)
- The package's `.nuspec` now carries the repository URL alongside the commit, and both are asserted against the released tag before the package is signed. `0.1.1` shipped a `<repository>` element with a commit but no URL, which left consumers unable to reach the source for the version they restored.
- `<Authors>` is now set explicitly, so the package no longer reports its own package id as its author.
- Async `EventCallback` faults from component event dispatch and generated two-way bindings are now observed instead of being dropped.
- Nested public fields in unmarshalled data are now transferred correctly as data columns.
- Combo change event values now decode to the correct `ChangeType`.

### Breaking Changes

#### Public API nullability

> [!NOTE]
> As part of this release the public API was annotated for nullable reference types. Beyond the members listed below, reference-type parameters, properties, and return values now declare whether they accept or produce `null`. Consumers building with nullable reference types enabled may see new nullable warnings — or errors, if warnings are treated as errors — and may need to update their code accordingly.

The following public members changed from nullable to non-nullable. Value-type changes (`double?` → `double`) are binary-breaking.

| Type | Member | Before | After |
|------|--------|--------|-------|
| `IgbTile` | `ColStart` | `double?` | `double` |
| `IgbTile` | `RowStart` | `double?` | `double` |
| `IgbCalendar` | `SpecialDates` | `IgbDateRangeDescriptor[]?` | `IgbDateRangeDescriptor[]` |
| `IgbCalendar` | `DisabledDates` | `IgbDateRangeDescriptor[]?` | `IgbDateRangeDescriptor[]` |

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
