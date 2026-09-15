# Visual Pattern → Ignite UI Blazor Component

A selection reference, not an API reference. Confirm exact parameters with `get_doc` or the [`igniteui-blazor-components`](../../igniteui-blazor-components/SKILL.md) reference files before writing markup.

## Structure & navigation

| What the image shows | Component | Notes |
|---|---|---|
| Top horizontal bar with brand, actions | `IgbNavbar` | slots `start` / default / `end`; use a plain `<header>` if the slot structure fights the design |
| Sidebar / side navigation | `IgbNavDrawer` + `IgbNavDrawerItem` | **`Position="NavDrawerPosition.Relative"` for a pinned in-flow sidebar**; `slot="mini"` for an icon-only collapsed rail; plain `<aside>` for a fully static custom sidebar |
| Tab strip switching content | `IgbTabs` + `IgbTab` | header via `Label` or the `label` slot; there is no `IgbTabPanel` |
| Collapsible sections | `IgbExpansionPanel`, or `IgbAccordion` when only one opens at a time | |
| Step-by-step wizard | `IgbStepper` + `IgbStep` | `Orientation`, `Linear` |
| Resizable two-pane split | `IgbSplitter` | `slot="start"` / `slot="end"`, `StartSize`, min/max sizes |
| Section separator line | `IgbDivider` | `Vertical`, `Middle`, `LineType` |
| Very long / endless scrolling list of uniform rows | `IgbVirtualScroll` | `Data`, `EstimatedItemSize`, `ItemTemplateScript` (client-side template), `DataRequest` for infinite scroll |
| Draggable/resizable widget dashboard | `IgbTileManager` + `IgbTile` | `ColumnCount`, `Gap`, `ColSpan`/`RowSpan`, `DragMode`, `ResizeMode` |
| Static card grid | `IgbCard` inside CSS Grid | use `IgbTileManager` only when tiles actually drag or resize |
| IDE-style dockable/floating panes | `IgbDockManager` | licensed package only; needs an explicit height |

## Content & data display

| What the image shows | Component | Notes |
|---|---|---|
| Repeated rows: icon/avatar + text + trailing action | `IgbList` + `IgbListItem` | slots `start`, `title`, `subtitle`, `end` |
| Content / summary card | `IgbCard` | `IgbCardMedia`, `IgbCardHeader`, `IgbCardContent`, `IgbCardActions`; no default width |
| KPI / stat tile row | plain HTML in CSS Grid | compose Ignite UI primitives (icon, badge, progress) inside your own containers |
| Avatar | `IgbAvatar` | `Shape` (`Circle`/`Rounded`/`Square`), `Initials`, `Src` |
| Status badge / count | `IgbBadge` | `Variant` (`StyleVariant`), `Shape`, `Dot`, `Outlined` |
| Filter tag, removable pill | `IgbChip` | `Removable`, `Selectable`, `Variant` |
| Icon | `IgbIcon` | `IconName` + `Collection`; register before first render |
| Progress bar / ring | `IgbLinearProgress` / `IgbCircularProgress` | `Value`, `Max`, `Indeterminate` — not for data viz |
| Tree view | `IgbTree` + `IgbTreeItem` | `Label`, `Expanded`, `Selection` |
| Carousel / slideshow | `IgbCarousel` + `IgbCarouselSlide` | `Interval`, `AnimationType` |
| Highlighted search matches in text | `IgbHighlight` | `SearchText`, `CaseSensitive` |
| Chat / conversation surface | `IgbChat` | configured through `IgbChatOptions` |
| QR code | `IgbQrCode` | `Value` (encoded payload), `Size`, `ErrorLevel`, `DotStyle`, `LogoSrc` |

Use `IgbList` when its row anatomy and keyboard behavior fit; native `<ul>/<li>` when they do not.

## Tables

| What the image shows | Component |
|---|---|
| Flat spreadsheet-style rows and columns | `IgbGrid` |
| Read-only table, no editing or selection | `IgbGridLite` (OSS, own package) |
| Rows expanding to child rows in the same schema | `IgbTreeGrid` |
| Rows expanding to a complete nested child grid | `IgbHierarchicalGrid` + `IgbRowIsland` |
| Pivot table with draggable dimensions | `IgbPivotGrid` |

Only pick a grid when the content is genuinely tabular. A list of records with rich per-row layout is a list, not a grid.

## Forms & input

| What the image shows | Component | Notes |
|---|---|---|
| Text field, search box, inline editor | `IgbInput` | `Label`, `Placeholder`, `Outlined`, `@bind-Value` |
| Multi-line text | `IgbTextarea` | `Rows`, `Resize` |
| Dropdown selecting a value | `IgbSelect` + `IgbSelectItem` | |
| Searchable / multi-select picker | `IgbCombo` | generic parameter is `T`; `Data`, `DisplayKey`, `ValueKey` |
| Contextual action menu | `IgbDropdown` | trigger in `slot="target"` — not for form values |
| Date picker | `IgbDatePicker` (`DateTime?`) | |
| Date range | `IgbDateRangePicker` | |
| Always-visible calendar | `IgbCalendar` (`DateTime`, non-nullable) | `Selection`, `VisibleMonths` |
| Masked entry (phone, postal) | `IgbMaskInput` | `Mask`: `0` digit, `L` letter, `A` alphanumeric |
| Checkbox / switch | `IgbCheckbox` / `IgbSwitch` | `@bind-Checked` |
| Radio options | `IgbRadioGroup` + `IgbRadio` | `@bind-Value` on the **group**; do not use `Name` to group |
| Slider / range slider | `IgbSlider` / `IgbRangeSlider` | range uses `Lower` / `Upper` |
| Star rating | `IgbRating` | |
| Color picker / color swatch input | `IgbColorPicker` | `@bind-Value` (CSS color string), `Mode` (`Default` trigger / `Input` field), `Format`, `ShowAlpha`, `Swatches` |
| Primary action button | `IgbButton` | `Variant`: `Contained`/`Outlined`/`Flat`/`Fab` |
| Segmented / toggle control | `IgbButtonGroup` + `IgbToggleButton` | |
| Icon-only button | `IgbIconButton` | |
| Hover hint | `IgbTooltip` | `Anchor` is the target element's **id string** |

## Charts, maps, gauges

Licensed or trial package only.

| What the image shows | Component | Key parameters |
|---|---|---|
| Line / area / column trend | `IgbCategoryChart` | `ChartType`, `DataSource`, `Brushes`; match `Spline`/`SplineArea` to smooth curves |
| Multiple series types, custom axes, horizontal bars | `IgbDataChart` | series + axis children matched by `Name` |
| Candlestick / OHLC | `IgbFinancialChart` | needs Open/High/Low/Close fields |
| Pie | `IgbPieChart` | `ValueMemberPath`, `LabelMemberPath` |
| Donut, or a colored ring with a centered value | `IgbDoughnutChart` + `IgbRingSeries` | `InnerExtent` on the **chart** |
| Inline micro-chart | `IgbSparkline` | `DisplayType`; no smooth curves — use a small `IgbCategoryChart` for those |
| Weighted hierarchy blocks | `IgbTreemap` | |
| Map with markers / regions / routes | `IgbGeographicMap` + a geographic series | series are child components |
| Needle on a scale | `IgbRadialGauge` / `IgbLinearGauge` | |
| Value vs target bar | `IgbBulletGraph` | |
| Auto-generated dashboard visual | `IgbDashboardTile` | verify binding shape first |

Read [`gotchas.md`](./gotchas.md) before writing any chart markup — the chart section there covers the mistakes that produce a compiling but visibly wrong result.

## Overlays & feedback

| What the image shows | Component |
|---|---|
| Modal confirmation or form overlay | `IgbDialog` — buttons in `slot="footer"` |
| Brief floating message with an action | `IgbSnackbar` — `ActionText` + `Action` |
| Brief floating message, no action | `IgbToast` |
| Persistent inline notice that pushes content down | `IgbBanner` — `slot="actions"` |
