# Ignite UI Blazor Component Mapping Reference

## Table of Contents
- [Dashboard & Layout Components](#dashboard--layout-components)
- [Chart Components](#chart-components)
- [Data Display Components](#data-display-components)
- [Form & Input Components](#form--input-components)
- [Calendar & Scheduling Components](#calendar--scheduling-components)
- [Map Components](#map-components)
- [Gauge Components](#gauge-components)
- [Package Requirements](#package-requirements)
- [Import & Registration Patterns](#import--registration-patterns)

---

## Dashboard & Layout Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Top navigation bar | `IgbNavbar` | `NavbarAction`, `NavbarTitle` child content slots |
| Side navigation | `IgbNavDrawer` | `Open`, `Pin`, `Position`, `IgbNavDrawerItem`, `IgbNavDrawerHeaderItem` |
| Content cards/panels | `IgbCard` | `IgbCardHeader`, `IgbCardContent`, `IgbCardMedia`, `IgbCardActions` |
| Tabbed content | `IgbTabs` | `IgbTab`, `IgbTabPanel`, `@bind-SelectedIndex` |
| Accordion sections | `IgbAccordion` | `IgbExpansionPanel` children |
| Tile dashboard | `IgbTileManager` | `ColumnCount`, `Gap`, `MinColumnWidth`, `MinRowHeight`, `DragMode`, `ResizeMode` |
| Repeated rows with icon/text/action | `IgbList` + `IgbListItem` | `slot="start"`, `slot="title"`, `slot="subtitle"`, `slot="end"` |
| Spreadsheet-like editable or sortable table | `IgbGrid` | Full-featured data grid |
| Hierarchical or tree-structured table | `IgbTreeGrid` | `PrimaryKey`, `ForeignKey`, `ChildDataKey` |
| Content blocks / summary cards | `IgbCard` | Use `IgbCardHeader`, `IgbCardContent`, `IgbCardActions` slots with custom projected content. Use plain `<div>` containers for flat or highly custom tiles |
| Any text input field | `IgbInput` | Use when the input anatomy matches the screenshot, including search fields and inline editors |
| Dropdown or select | `IgbSelect` | Use when the screenshot clearly shows select/dropdown behavior |
| Form fields with labels and inputs | `IgbInput`, `IgbSelect`, `IgbCombo` | Text, select, combo, and date inputs |
| Multi-step form / wizard | `IgbStepper` | Use when a sequence of steps is visually present |
| Filter chips / tag inputs | `IgbChip` | Use when chip anatomy matches status badges, filter tags, or removable labels |
| Calendar or date picker as a primary view element | `IgbCalendar`, `IgbDatePicker` | Use when scheduling or date selection is the core UI |
| Top icon/action bar | `IgbNavbar` with projected icon buttons | Use when a navbar structure matches the screenshot; use plain icon buttons or custom containers when that is a closer fit |

Decision rules:

- Use `IgbNavbar` for a top horizontal bar when its slot structure and behavior match the screenshot. Use custom projected content and CSS flex overrides to achieve multi-zone layouts inside it. Use a plain `<header>` element when that is a closer structural fit.
- Use `IgbNavDrawer` for a sidebar or side-navigation panel when drawer structure and behavior match the screenshot. Configure `Pin` and `Position` according to whether the design shows fixed, collapsible, or icon-only navigation. Use a plain `<aside>` when a static custom sidebar matches the screenshot better.
- Use `IgbTabs` for a horizontal tab strip when the screenshot clearly shows tabbed state switching.

Component decision matrix (by visual pattern, domain-neutral):

| Visual Pattern | Recommended Component | Notes |
|---|---|---|
| Sidebar with icon rows | `IgbNavDrawer` | Pinned mode, `IgbNavDrawerItem` children |
| Top bar with brand/search/actions | `IgbNavbar` | Project icon buttons into action slots |
| Card grid / tile layout | `IgbTileManager` or `IgbCard` in CSS Grid | `IgbTileManager` for drag/resize; plain CSS Grid + `IgbCard` for static layouts |
| KPI summary row | Plain HTML in CSS Grid | Bind Ignite UI primitives (icons, badges, progress) inside semantic containers |
| Collapsible section | `IgbExpansionPanel` or `IgbAccordion` | `IgbAccordion` when design shows only-one-open behavior |
| Tab strip | `IgbTabs` | Use `IgbTab` + `IgbTabPanel` pairs |
| Step-by-step wizard | `IgbStepper` | Horizontal or vertical orientation based on layout |
| Data table | `IgbGrid` / `IgbTreeGrid` | Use only when content is truly tabular |

---

## Chart Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Area chart | `IgbCategoryChart` | `ChartType="Area"`, `MarkerTypes`, `DataSource` |
| Line chart | `IgbCategoryChart` | `ChartType="Line"`, `MarkerTypes`, `DataSource` |
| Column/bar chart | `IgbCategoryChart` | `ChartType="Column"` / `ChartType="Bar"`, `DataSource` |
| Sparkline (mini chart) | `IgbSparkline` | `DisplayType`, `ValueMemberPath`, `DataSource` |
| Pie chart | `IgbPieChart` | `ValueMemberPath`, `LabelMemberPath`, `DataSource` |
| Donut/data pie chart | `IgbDataPieChart` | `ValueMemberPath`, `LabelMemberPath`, `InnerExtent` |
| Financial chart | `IgbFinancialChart` | `ChartType`, OHLC member paths, `DataSource` |
| Complex multi-series | `IgbDataChart` | Multiple `IgbSeries` + `IgbAxis` children |

Decision rules:

- Financial or OHLC screenshot: prefer `IgbFinancialChart`
- Simple or moderate trend panel: prefer `IgbCategoryChart`; move to `IgbDataChart` when you need lower-level per-series control
- Highly custom sparkline or microchart: use `IgbSparkline` or a custom fallback if the built-in anatomy is not a close visual match
- Part-to-whole: prefer `IgbPieChart` or `IgbDataPieChart`

---

## Data Display Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Item list | `IgbList` + `IgbListItem` | `slot="start"` (avatar/icon), `slot="title"`, `slot="subtitle"`, `slot="end"` |
| User avatar | `IgbAvatar` | `Shape` (`Circle`, `Rounded`, `Square`), `Initials`, `Src` |
| Status badge | `IgbBadge` | `Value`, `Type` |
| Icons | `IgbIcon` | `Collection`, `Name` |
| Progress bar | `IgbLinearProgress` | `Value`, `Max` |
| Circular progress | `IgbCircularProgress` | `Value`, `Max` |
| Flat data grid | `IgbGrid` | Full-featured data grid with sorting, filtering, editing |
| Hierarchical/tree data grid | `IgbTreeGrid` | `PrimaryKey`, `ForeignKey`, `ChildDataKey` |
| Filter/tag chips | `IgbChip` | `Removable`, `Selectable` |
| Tree view | `IgbTree` + `IgbTreeItem` | `Label`, `Expanded`, `Selection` mode |
| Content card | `IgbCard` | `IgbCardHeader`, `IgbCardMedia`, `IgbCardContent`, `IgbCardActions` |
| Carousel | `IgbCarousel` | Slide-based navigation |

Decision rules:

- Use `IgbList` for repeated-row content lists when its row structure and interaction model match the screenshot. The component adds accessible keyboard navigation, item structure, and theming when those benefits fit the design. Use native `<ul>/<li>` or custom containers when they are a closer visual fit.
- Choose `IgbGrid` only when the image is truly tabular (flat rows and columns, spreadsheet-style).
- Choose `IgbTreeGrid` when rows have parent-child or hierarchical structure.
- Use `IgbChip` when chip anatomy matches the screenshot's status badges, tags, or label pills. Use custom badge or pill markup when a simpler or more exact visual match is needed.

---

## Form & Input Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Text input / search field | `IgbInput` | `Type`, `Label`, `Placeholder`, `@bind-Value` |
| Select / dropdown | `IgbSelect` | `IgbSelectItem` children, `@bind-Value` |
| Multi-select combo | `IgbCombo` | `DataSource`, `DisplayKey`, `ValueKey`, `@bind-Value` |
| Date picker | `IgbDatePicker` | `@bind-Value`, `DisplayFormat` |
| Calendar | `IgbCalendar` | `Selection`, `@bind-Value` |
| Checkbox | `IgbCheckbox` | `@bind-Checked` |
| Radio button | `IgbRadio` / `IgbRadioGroup` | `@bind-Value` on group |
| Switch / toggle | `IgbSwitch` | `@bind-Checked` |
| Slider | `IgbSlider` / `IgbRangeSlider` | `@bind-Value`, `Min`, `Max`, `Step` |
| Rating | `IgbRating` | `@bind-Value`, `Max` |
| Stepper / wizard | `IgbStepper` | `IgbStep` children, `Orientation` |

---

## Calendar & Scheduling Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Calendar view | `IgbCalendar` | `Selection`, `@bind-Value` |
| Date picker in form | `IgbDatePicker` | `@bind-Value`, `DisplayFormat` |
| Date range selection | `IgbDateRangePicker` | `@bind-Value` |

---

## Map Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| World / region map | `IgbGeographicMap` | `Zoomable`, `BackgroundContent` |
| Map markers (points) | `IgbGeographicSymbolSeries` | `LatitudeMemberPath`, `LongitudeMemberPath`, `MarkerType`, `MarkerBrush` |
| Bubble overlay | `IgbGeographicProportionalSymbolSeries` | Sized markers based on data value |
| Shape regions (polygons) | `IgbGeographicShapeSeries` | Polygon rendering |
| Polyline paths | `IgbGeographicPolylineSeries` | Route/path rendering |

---

## Gauge Components

| UI Pattern | Ignite UI Blazor Component | Key Properties |
|---|---|---|
| Linear gauge | `IgbLinearGauge` | `Value`, `MinimumValue`, `MaximumValue`, `NeedleBrush` |
| Radial gauge | `IgbRadialGauge` | `Value`, `MinimumValue`, `MaximumValue` |
| Bullet graph | `IgbBulletGraph` | `Value`, `TargetValue`, `MinimumValue`, `MaximumValue` |

---

## Package Requirements

All Ignite UI Blazor components ship in a **single NuGet package**. No additional DV-specific packages are needed.

| NuGet Package | Description |
|---|---|
| `IgniteUI.Blazor` | Licensed / trial — full component suite including charts, grids, maps, gauges, DockManager |
| `IgniteUI.Blazor.Lite` | Open-source / MIT — core UI components only (no charts, grids, maps, gauges, or DockManager) |

| Capability | Package Required |
|---|---|
| Core UI components (list, avatar, navbar, drawer, card, badge, progress, icon, etc.) | `IgniteUI.Blazor.Lite` or `IgniteUI.Blazor` |
| Charts / sparklines | `IgniteUI.Blazor` only |
| Maps | `IgniteUI.Blazor` only |
| Gauges / bullet graphs | `IgniteUI.Blazor` only |
| Data grids | `IgniteUI.Blazor` only |
| Tile Manager | `IgniteUI.Blazor` only |
| Dock Manager | `IgniteUI.Blazor` only |

---

## Import & Registration Patterns

### 1. NuGet package reference (`.csproj`)

```xml
<PackageReference Include="IgniteUI.Blazor" Version="*" />
```

### 2. Service registration (`Program.cs`)

Every `Igb*` component requires its corresponding `IgbXxxModule` to be registered:

```csharp
using IgniteUI.Blazor.Controls;

builder.Services.AddIgniteUIBlazor(
    typeof(IgbNavbarModule),
    typeof(IgbNavDrawerModule),
    typeof(IgbListModule),
    typeof(IgbListItemModule),
    typeof(IgbCardModule),
    typeof(IgbAvatarModule),
    typeof(IgbBadgeModule),
    typeof(IgbCategoryChartModule),
    typeof(IgbGridModule)
    // ... add every IgbXxxModule your page uses
);
```

> **If you forget to register a module, the component will silently fail to render.** Always double-check that every `Igb*` component used in Razor has its module registered in `Program.cs`.

### 3. Using directive (`_Imports.razor`)

```razor
@using IgniteUI.Blazor.Controls
```

### 4. CSS theme (`wwwroot/index.html` or `App.razor`)

```html
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
```

### 5. JS interop script (`wwwroot/index.html` or `App.razor`)

```html
<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>
```

---

Treat this file as a component selection reference, not as authoritative API guidance for a specific version. Confirm exact parameters and behavior from `get_doc` results and the current workspace's reference files (`igniteui-blazor-components` skill).
