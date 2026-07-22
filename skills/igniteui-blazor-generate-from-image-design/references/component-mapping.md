# Ignite UI Blazor Component Mapping Reference

Maps visual patterns in a design image to Ignite UI Blazor components. This is a selection aid only - confirm exact parameters and markup with `get_doc` before coding.

## Dashboard & layout

| UI pattern | Component | Notes |
|---|---|---|
| Top navigation bar | `IgbNavbar` | `slot="start"` leading actions, default content for brand/title, `slot="end"` trailing actions; plain `<header>` when a custom bar fits better |
| Side navigation | `IgbNavDrawer` | `IgbNavDrawerItem`/`IgbNavDrawerHeaderItem`, `mini` slot for icon-only collapsed state; plain `<aside>` for static custom sidebars (see gotchas for pinned sidebars) |
| Content cards / panels | `IgbCard` | `IgbCardHeader`/`IgbCardMedia`/`IgbCardContent`/`IgbCardActions`; plain `<div>` for flat or highly custom tiles |
| Tab strip | `IgbTabs` | Use when the image clearly shows tabbed state switching |
| Collapsible sections | `IgbAccordion` / `IgbExpansionPanel` | Accordion when only-one-open behavior is shown |
| Tile dashboard (drag/resize) | `IgbTileManager` | `ColumnCount`, `Gap`, `DragMode`, `ResizeMode`; static tile layouts → CSS Grid + `IgbCard` |
| IDE-like dockable panes | `IgbDockManager` | Layout object graph (`IgbSplitPane`/`IgbTabGroupPane`/`IgbContentPane`); floating, pinning, serialization |
| Repeated rows (icon + text + action) | `IgbList` + `IgbListItem` | `slot="start"`/`"title"`/`"subtitle"`/`"end"`; native `<ul>/<li>` when a closer visual fit |
| KPI summary row | Plain HTML in CSS Grid | Embed Ignite UI primitives (icons, badges, progress) inside semantic containers |
| Multi-step wizard | `IgbStepper` | Horizontal or vertical per the layout |

## Charts & visualizations

| UI pattern | Component | Notes |
|---|---|---|
| Line / area / column trend panel | `IgbCategoryChart` | Pick `ChartType` to match the curve (Spline vs Line - see gotchas) |
| Multi-series or custom-axis chart | `IgbDataChart` | Per-series and per-axis control; one module per series category |
| Financial / OHLC / candlestick | `IgbFinancialChart` | Data needs `Open`/`High`/`Low`/`Close` (+ date) fields |
| Pie | `IgbPieChart` | `ValueMemberPath`, `LabelMemberPath` |
| Donut (incl. ring gauges with centered label) | `IgbDoughnutChart` + `IgbRingSeries` | `InnerExtent` on the chart; overlay the label with CSS |
| Sparkline / microchart | `IgbSparkline` | `Line`/`Area`/`Column`/`WinLoss` only; smooth curves need a mini `IgbCategoryChart` |
| Hierarchical part-to-whole | `IgbTreemap` | `LabelMemberPath`, `ValueMemberPath` |
| Auto dashboard widget from data | `IgbDashboardTile` | Verify supported modes with `get_doc` |
| Geographic map | `IgbGeographicMap` | Series as child components: `IgbGeographicSymbolSeries` (markers), `...ProportionalSymbolSeries` (bubbles), `...ShapeSeries` (polygons), `...PolylineSeries` (routes) |
| Linear / radial gauge, bullet graph | `IgbLinearGauge` / `IgbRadialGauge` / `IgbBulletGraph` | Needle-and-scale KPIs; not for static rings |

## Data display

| UI pattern | Component | Notes |
|---|---|---|
| Spreadsheet-like table | `IgbGrid` | Only when content is truly tabular; see the grids skill |
| Tree-structured table | `IgbTreeGrid` | Single schema parent-child |
| Rows expanding to nested child grids | `IgbHierarchicalGrid` | Multi-schema master-detail via `IgbRowIsland` |
| Read-only simple table | `IgbGridLite` | OSS package; no editing/selection/paging |
| Avatar | `IgbAvatar` | `Shape`, `Initials`, `Src` |
| Status badge / count | `IgbBadge` | `Variant`, `Shape`, dot mode |
| Filter/tag chips | `IgbChip` | `Removable`, `Selectable`; custom pill markup when a simpler match is needed |
| Progress bar / ring | `IgbLinearProgress` / `IgbCircularProgress` | `Value`, `Max`; indeterminate for spinners |
| Tree view | `IgbTree` + `IgbTreeItem` | Navigation/selection trees, not tabular data |
| Action button | `IgbButton` | `Variant`: Contained / Outlined / Flat / Fab |
| Toggle button group | `IgbButtonGroup` + `IgbToggleButton` | Single or multiple selection |
| Icon-only button | `IgbIconButton` | When no text label is shown |
| Contextual action menu | `IgbDropdown` | Trigger via `slot="target"`; use `IgbSelect` when it is a form field |
| Hover tooltip | `IgbTooltip` | `Anchor` = target element ID |
| Image/content slides | `IgbCarousel` | `IgbCarouselSlide` children |

## Forms & inputs

| UI pattern | Component | Binding |
|---|---|---|
| Text input / search field | `IgbInput` | `@bind-Value` |
| Select dropdown | `IgbSelect` + `IgbSelectItem` | `@bind-Value` |
| Multi-select / autocomplete combo | `IgbCombo` | `Data`, `ValueKey`, `DisplayKey`, generic `T` |
| Date picker / range / calendar | `IgbDatePicker` / `IgbDateRangePicker` / `IgbCalendar` | `@bind-Value` |
| Inline date-time / masked input | `IgbDateTimeInput` / `IgbMaskInput` | `InputFormat` / `Mask` |
| Checkbox / switch | `IgbCheckbox` / `IgbSwitch` | `@bind-Checked` |
| Radio group | `IgbRadioGroup` + `IgbRadio` | `@bind-Value` on the group |
| Slider / range slider | `IgbSlider` / `IgbRangeSlider` | `Min`, `Max`, `Step` |
| Rating stars | `IgbRating` | `@bind-Value`, `Max` |

## Feedback & overlays

| UI pattern | Component | Notes |
|---|---|---|
| Modal confirmation / overlay form | `IgbDialog` | Blocking; action buttons in `slot="footer"`; `ShowAsync()`/`HideAsync()` via `@ref` |
| Brief message with action ("Undo") | `IgbSnackbar` | Auto-dismisses after `DisplayTime` |
| Auto-dismissing status message | `IgbToast` | No action button - prefer Snackbar when one is needed |
| Persistent inline alert | `IgbBanner` | Non-modal, pushes content down (offline notice, consent) |

## Package requirements

| Capability | Package |
|---|---|
| Core UI components (forms, lists, cards, navigation, feedback, Tile Manager) | `IgniteUI.Blazor.Lite` (MIT) or full |
| `IgbGridLite` | `IgniteUI.Blazor.GridLite` (MIT) or full |
| Charts, sparklines, maps, gauges, Dock Manager, full grids | `IgniteUI.Blazor` (licensed) or `IgniteUI.Blazor.Trial` (NuGet.org, watermarked) only |

Do not mix `IgniteUI.Blazor` with the Lite packages in one project. Registration, imports, and host-page requirements are in the `igniteui-blazor-components` skill and the `get_doc` setup docs.
