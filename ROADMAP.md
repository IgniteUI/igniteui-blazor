# Roadmap - Ignite UI for Blazor

# Current Milestone

## Milestone 12 (Due Oct 2026)

1. Grids performance improvements using a new and faster virtualization component
2. **[DONE]** `IgbColorPicker` - color input with an HSV canvas, hue/alpha sliders, editable color string, swatches and EyeDropper support
3. **[DONE]** `IgbQrCode` - renders a scannable QR code as an SVG, with styling options and an optional centered logo
4. **[DONE]** `IgbVirtualScroll` - virtualization container that renders only the visible items of large or unbounded lists
5. **[DONE]** Aligned to `igniteui-webcomponents@7.3.0` - Chip `Outlined`, Splitter collapsed panes and `LayoutChanged`, Tabs `SelectedTab`, Icon registration options

## Going down the road

1. Breadcrumb component

# Previous Milestone

## Milestone 11, version 26.1.51 (Released Jul 14th, 2026)

1. **[DONE]** `IgbChat` (preview) - Chat UI component for message display and input interaction; under active development, APIs may evolve
2. **[DONE]** `IgbSplitter` - resizable split-pane layout dividing the view into *start* and *end* panels separated by a draggable bar
3. **[DONE]** `IgbHighlight` - efficient searching and highlighting of text projected via the default slot
4. **[DONE]** Aligned to `igniteui-webcomponents@7.2.4`; rolls up `IgniteUI.Blazor` 25.2.102 (May 2026) and 26.1.51 (June 2026)
5. **[DONE]** Agent Skills for Blazor
6. **[DONE]** CLI MCP server support for Blazor
7. **[DONE]** Dock Manager 2.0
8. **[DONE]** Dock Manager - two-way binding for the layout properties

## Milestone 10, version 25.2.77 (Released Mar 2026)

1. **[DONE]** `IgbQueryBuilder` — visual query builder for complex filtering conditions
2. **[DONE]** `IgbThemeProvider` — scoped theming via Lit context API; multiple themes per page; Shadow and Light DOM
3. **[DONE]** Grid PDF export — `IgbGrid`, `IgbTreeGrid`, `IgbHierarchicalGrid` (`IgbPivotGrid` not covered in this release)
4. **[DONE]** Grids rendering performance — dynamic scroll throttle across all four grid types
5. **[DONE]** `IgbCombo` — new `disableClear` property
6. **[DONE]** Badge — new dot type; WCAG AA outline; theme-based sizing
7. **[DONE]** Checkbox — new `--tick-width` CSS custom property
8. **[DONE]** Accessibility improvements — Button, Button Group, Calendar, Checkbox, Date Picker, Date Range Picker, Nav Drawer, Radio Group, Stepper

## Milestone 9, version 25.2.32 (Released Nov 2025)

1. **[DONE]** Grids cell merging — configurable per-column; `OnSort` and `Always` modes
2. **[DONE]** Bilateral column pinning — columns can be pinned to start or end independently
3. **[DONE]** Grids sorting and grouping performance — Schwartzian transform; iterative sort and grouping algorithms
4. **[DONE]** `IgbDateRangePicker` — new component
5. **[DONE]** Chart user annotations — slice, strip, and point annotations at runtime via toolbar; `IgbDataChart` integration
6. **[DONE]** Chart axis annotation collision detection — `ShouldAvoidAnnotationCollisions`, `ShouldAutoTruncateAnnotations`
7. **[DONE]** `IgbTooltip` — new component (`Anchor` property, `ShowDelay`/`HideDelay`, `Placement`, `Show`/`HideTriggers`)

## Milestone 8, version 25.1.x (Released Jun 2025)

1. **[DONE]** Chart Data Annotations — Band, Line, Rect, Slice, Strip layers with `OverlayText` support
2. **[DONE]** Trendline Layer — per-series trend line; multiple trend lines per series
3. **[DONE]** Azure Map Imagery — GA (`IgbAzureMapsImagery`; Bing Maps being phased out)
4. **[DONE]** `IgbGrid` cell suffix content — `SuffixText`, `SuffixIconName`, and related styling properties on columns
5. **[DONE]** `IgbTabs` — simplified configuration; content assigned directly to `IgbTab`; `Label` property; `TabPanel` removed
6. **[DONE]** Companion Axis for `IgbDataChart` — `CompanionAxisEnabled` clones an axis to the opposite position

## Milestone 7, version 24.2 (Released Dec 2024)

1. **[DONE]** `IgbBanner` — new component
2. **[DONE]** `IgbDatePicker` — new component (old `IgbDatePicker` renamed to `IgbXDatePicker`)
3. **[DONE]** `IgbDivider` — new component
4. **[DONE]** `IgbCarousel` — new component
5. **[DONE]** `IgbTileManager` — new component
6. **[DONE]** Dashboard Tile — `IgbDashboardTile` container with built-in toolbar, auto-selects visualization from bound data
7. **[DONE]** Color Editor — standalone color picker; integrated into Toolbar `ToolAction`
8. **[DONE]** `IgbDataPieChart` — new component
9. **[DONE]** Proportional Category Angle Axis — radial pie series support in `IgbDataChart`
10. **[DONE]** Chart selection — series click styling; `SelectedSeriesItemsChanged` event

## Milestone 6, version 23.2.189 (Released Mar 2024)

1. **[DONE]** Hierarchical Grid [#97](https://github.com/IgniteUI/igniteui-blazor/issues/97)
2. **[DONE]** Text Area component
3. **[DONE]** Button Group component
4. **[DONE]** `IgbDockManager` — `ProximityDock`, `ContainedInBoundaries`, `ShowPaneHeaders` properties

## Milestone 5, version 23.1 (Released Dec 15, 2023)

1. **[DONE]** IgrDataGrid - Declarative Filtering [#48](https://github.com/IgniteUI/igniteui-react/issues/48)

## Milestone 4, version 23.1 (Released Jun 28, 2023) [Release Blog 23.1.0](https://www.infragistics.com/community/blogs/b/infragistics/posts/ignite-ui-for-blazor-23-1-release)

1. **[DONE]** IgbPivotGrid - Pivot Grid
2. **[DONE]** ComboBox
3. **[DONE]** Stepper
4. **[DONE]** Dialog

## Milestone 3, version 22.2 (Released Nov 3, 2022) [Release Blog 22.2.0](https://www.infragistics.com/community/blogs/b/infragistics/posts/ignite-ui-for-blazor---what-s-new-in-22-2)

1. **[DONE]** IgbDataGrid - Data Grid
2. **[DONE]** IgbDataGrid - Header Template
3. **[DONE]** IgbDataGrid - Cell Template
4. **[DONE]** IgbDataGrid - Data Binding
5. **[DONE]** IgbDataGrid - Complex Data Binding
6. **[DONE]** IgbDataGrid - Collapsible Column Groups
7. **[DONE]** IgbDataGrid - Column Hiding
8. **[DONE]** IgbDataGrid - Column Reordering & Moving
9. **[DONE]** IgbDataGrid - Column Pinning
10. **[DONE]** IgbDataGrid - Column Resizing
11. **[DONE]** IgbDataGrid - Column Types
12. **[DONE]** IgbDataGrid - Conditional Styling
13. **[DONE]** IgbDataGrid - Display Density
14. **[DONE]** IgbDataGrid - Editing
15. **[DONE]** IgbDataGrid - React Grid Cell Editing
16. **[DONE]** IgbDataGrid - Cascading Combos
17. **[DONE]** IgbDataGrid - Row Adding
18. **[DONE]** IgbDataGrid - Row Editing
19. **[DONE]** IgbDataGrid - Export to Excel Service
20. **[DONE]** IgbDataGrid - Filtering
21. **[DONE]** IgbDataGrid - React Grid Advanced Filtering
22. **[DONE]** IgbDataGrid - Excel Style Filtering
23. **[DONE]** IgbDataGrid - Group By
24. **[DONE]** IgbDataGrid - Live Data / Real-Time Updates
25. **[DONE]** IgbDataGrid - Master-Detail
26. **[DONE]** IgbDataGrid - Keyboard Navigation
27. **[DONE]** IgbDataGrid - Multi-Column Headers Overview
28. **[DONE]** IgbDataGrid - Multi-row Layout
29. **[DONE]** IgbDataGrid - Pagination
30. **[DONE]** IgbDataGrid - Remote Data Operations
31. **[DONE]** IgbDataGrid - Row Actions
32. **[DONE]** IgbDataGrid - Row Dragging
33. **[DONE]** IgbDataGrid - Row Pinning
34. **[DONE]** IgbDataGrid - Search Filter
35. **[DONE]** IgbDataGrid - Selection
36. **[DONE]** IgbDataGrid - Cell Selection
37. **[DONE]** IgbDataGrid - Column Selection
38. **[DONE]** IgbDataGrid - Row Selection
39. **[DONE]** IgbDataGrid - Sizing
40. **[DONE]** IgbDataGrid - Sorting
41. **[DONE]** IgbDataGrid - Summaries
42. **[DONE]** IgbDataGrid - Virtualization and Performance
43. **[DONE]** IgbDataGrid - Toolbar
44. **[DONE]** IgbDataGrid - Theming
44. **[DONE]** IgbTreeGrid - Tree Grid
45. **[DONE]** IgbTreeGrid - Cell Editing
46. **[DONE]** IgbTreeGrid - Clipboard Interactions
47. **[DONE]** IgbTreeGrid - Collapsible Column Groups
48. **[DONE]** IgbTreeGrid - Cell Selection
49. **[DONE]** IgbTreeGrid - Column Pinning
50. **[DONE]** IgbTreeGrid - Column Resizing
51. **[DONE]** IgbTreeGrid - Column Selection
52. **[DONE]** Accordion
53. **[DONE]** DataTime Input
54. **[DONE]** Select
55. **[DONE]** Tabs
56. **[DONE]** Chart Improvements

## Milestone 2, version 22.1 (Released Jun 15, 2022) [Release Blog 22.1.0](https://www.infragistics.com/community/blogs/b/infragistics/posts/ignite-ui-for-blazor---what-s-new-in-22-1)

1. **[DONE]** Dock Manager
2. **[DONE]** Data Legend
3. **[DONE]** Data Tooltip
4. **[DONE]** Chip
5. **[DONE]** Drop Down
6. **[DONE]** Mask Input
7. **[DONE]** Progress Bar - Linear and Circular
8. **[DONE]** Rating
9. **[DONE]** Slider and Range Slider
10. **[DONE]** Snackbar
11. **[DONE]** Toast

## Milestone 1, version 21.2 (Released Dec 16, 2021) [Release Blog 21.2.0](https://www.infragistics.com/community/blogs/b/blagunas/posts/ignite-ui-for-blazor-whats-new-in-212)

1. **[DONE]** Avatar
2. **[DONE]** Badge
3. **[DONE]** Card
4. **[DONE]** Checkbox
5. **[DONE]** Icon
6. **[DONE]** Input
7. **[DONE]** List
8. **[DONE]** Calendar
9. **[DONE]** Navigation Bar
10. **[DONE]** Navigation Drawer
11. **[DONE]** Radio & Radio Group
12. **[DONE]** Ripple
13. **[DONE]** Switch
