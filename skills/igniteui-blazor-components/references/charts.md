# Charts & Data Visualization

All visualization components require `IgniteUI.Blazor` or `IgniteUI.Blazor.Trial` — they are **not** in `IgniteUI.Blazor.Lite`. Ignite UI ships 65+ chart types; this file covers the common ones and the traps. Call `get_doc` / `get_api_reference` for anything beyond it.

## Choosing a component

| Need | Component |
|---|---|
| Line / area / column / point / spline / waterfall with minimal config | `IgbCategoryChart` |
| Several series types on one chart, custom axes, annotations, true horizontal bars | `IgbDataChart` |
| Candlestick / OHLC with range selector | `IgbFinancialChart` |
| Part-to-whole slices | `IgbPieChart` |
| Ring with a hollow center | `IgbDoughnutChart` + `IgbRingSeries` |
| Inline micro-chart in a table or card | `IgbSparkline` |
| Hierarchical part-to-whole | `IgbTreemap` |
| Geographic points, shapes, routes | `IgbGeographicMap` |
| KPI value on a scale, needle, bullet comparison | `IgbLinearGauge` / `IgbRadialGauge` / `IgbBulletGraph` |
| Auto-generated dashboard visual from data | `IgbDashboardTile` |

## Category Chart

```razor
<IgbCategoryChart DataSource="SalesData"
                  ChartType="CategoryChartType.Line"
                  Width="100%" Height="400px"
                  XAxisTitle="Month" YAxisTitle="Revenue (USD)"
                  Brushes="DodgerBlue IndianRed"
                  IncludedProperties='@(new string[] { "Month", "Revenue" })'
                  IsHorizontalZoomEnabled="true"
                  IsTransitionInEnabled="true" />
```

The chart auto-detects the string/date property for the X axis and creates a series for every numeric property. Narrow that with `IncludedProperties` / `ExcludedProperties`.

- `CategoryChartType.Bar` **does not exist**. `Column` gives vertical bars; true horizontal bars need `IgbDataChart` + `IgbBarSeries`.
- `IncludedProperties` / `ExcludedProperties` are `string[]` — bind them as `@(new string[] { … })`, never as a plain string.
- `XAxisLabel` formats labels; it does not select a data field.
- Match `ChartType` to the curve in the design: `Spline` / `SplineArea` for smooth curves, `Line` / `Area` for angular, `StepLine` / `StepArea` for steps. Defaulting to `Line` when the design shows smooth curves is the most visible fidelity mistake.
- Markers are shown at every point by default. Suppress them by adding `MarkerType.None` to `MarkerTypes` once the chart ref is ready.

## Data Chart

```razor
<IgbLegend @ref="Legend" Orientation="LegendOrientation.Horizontal" />
<IgbDataChart Legend="Legend" Width="100%" Height="500px" IsHorizontalZoomEnabled="true">
    <IgbCategoryXAxis Name="xAxis" DataSource="ChartData" Label="Month" />
    <IgbNumericYAxis Name="yAxis" />
    <IgbLineSeries DataSource="ChartData" XAxisName="xAxis" YAxisName="yAxis"
                   ValueMemberPath="Revenue" Title="Revenue" />
    <IgbColumnSeries DataSource="ChartData" XAxisName="xAxis" YAxisName="yAxis"
                     ValueMemberPath="Expenses" Title="Expenses" />
</IgbDataChart>

@code {
    private IgbLegend _legend = default!;
    private IgbLegend Legend
    {
        get => _legend;
        set { _legend = value; StateHasChanged(); }   // re-render so Legend="Legend" receives the ref
    }
}
```

- Every series must reference its axes by matching `Name` ↔ `XAxisName` / `YAxisName`.
- `IgbDataChart` needs one module per **series category** — `IgbDataChartCoreModule` plus e.g. `IgbDataChartCategoryModule`, `IgbDataChartScatterModule`, `IgbDataChartFinancialModule`. Check `get_doc` for the exact names.
- `IgbScatterSeries` / `IgbBubbleSeries` map fields with `XMemberPath` / `YMemberPath` (not `XAxisMemberPath`); bubbles also need `RadiusMemberPath`.

The legend property-with-`StateHasChanged` idiom above is required for any chart that binds a legend by reference.

## Financial Chart

```razor
<IgbFinancialChart DataSource="StockData" Width="100%" Height="500px"
                   ChartType="FinancialChartType.Candle"
                   ZoomSliderType="FinancialChartZoomSliderType.Line" />
```

The data source must expose `Open`, `High`, `Low`, `Close` (and usually `Volume`) numeric fields plus a date/time field, or nothing renders.

## Pie & Donut

```razor
<IgbPieChart DataSource="SliceData" LabelMemberPath="Department" ValueMemberPath="Budget"
             Width="500px" Height="400px" Legend="PieLegend" SliceClick="OnSliceClick" />

<IgbDoughnutChart InnerExtent="0.6" Width="220px" Height="220px">
    <IgbRingSeries DataSource="DonutData" LabelMemberPath="Category" ValueMemberPath="Share"
                   Brushes="#5B57E8 #E8E8F5" Outlines="transparent transparent"
                   LabelsPosition="LabelsPosition.None" />
</IgbDoughnutChart>
```

- **`InnerExtent` is a chart property, never a series property.** `<IgbRingSeries InnerExtent="…">` throws `InvalidOperationException: IgbRingSeries does not have a property matching 'InnerExtent'`.
- `IgbDoughnutChart` supports multiple `IgbRingSeries` for concentric rings.
- For a static colored ring with a centered value and no needle, use `IgbDoughnutChart` and absolutely position the label over it — `IgbRadialGauge` and `IgbCircularProgress` will not produce that look.

## Sparkline

```razor
<IgbSparkline DataSource="TrendData" ValueMemberPath="Value"
              DisplayType="SparklineDisplayType.Line"
              Width="120px" Height="40px" Brush="DodgerBlue" LineThickness="2" />
```

`DisplayType` supports only `Line`, `Area`, `Column`, `WinLoss`, and `Area` renders as an angular polygon. For a smooth mountain-shaped micro-chart use a small `IgbCategoryChart` with `ChartType="CategoryChartType.SplineArea"` and `AreaFillOpacity` — `AreaFillOpacity` does not exist on `IgbSparkline`.

## Treemap, Geographic Map, Gauges, Dashboard Tile

Binding shapes differ enough between these that guessing fails: read `get_doc` for `treemap-chart`, the geographic-map series types, the specific gauge, or `dashboard-tile` before writing markup. Map series (`IgbGeographicSymbolSeries`, `IgbGeographicShapeSeries`, `IgbGeographicPolylineSeries`, `IgbGeographicProportionalSymbolSeries`) are declared as child components, not added from code, and do not follow Data Chart axis conventions.

## Tooltips

Tooltip support differs per component:

| Component | Tooltip on hover | How to get one |
|---|---|---|
| `IgbCategoryChart`, `IgbFinancialChart` | Yes, by default | `ToolTipType`: `Data` (default), `Item`, `Category`, `Default`, `None` |
| `IgbDataPieChart` | Yes, by default | Built-in tooltip only — it has `ToolTipType` but **no** `TooltipTemplate`; `DataToolTipUnitsText="%"` appends a unit to the value |
| `IgbDataChart` series | No | `ShowDefaultTooltip="true"` or a `TooltipTemplate` on each series, **and** `IgbDataChartInteractivityModule` registered — without the module nothing shows and nothing is logged |
| `IgbPieChart` | No tooltip API | Handle `SliceEnter` / `SliceHover` / `SliceLeave` and position your own element at `e.Position`, which is relative to the chart element (wrap the chart in a `position: relative` element). Moving onto a new slice raises `SliceLeave` (old) and `SliceEnter` (new); `SliceHover` comes only with the next move |
| `IgbDoughnutChart` | No tooltip API; a template set from JS throws on hover ([#491](https://github.com/IgniteUI/igniteui-blazor/issues/491)) | For a ring with tooltips use `IgbDataPieChart` with `InnerExtent` (built-in tooltip; its `InnerExtent` scale differs from the doughnut's — about `0.35` gives a typical donut, `0.55` a thin ring), or `IgbPieChart` with `InnerExtent` plus the `SliceHover` approach above |
| `IgbSparkline`, `IgbRadialGauge` | No working tooltip | — |
| `IgbLinearGauge`, `IgbBulletGraph` | `ShowToolTip="true"` shows only an empty box | Wrap the gauge in an element with an `id` and anchor an `IgbTooltip` to it (see `data-display.md`) |

- **`TooltipTemplate` on `IgbCategoryChart`** (a child `<TooltipTemplate>` `RenderFragment<IgbDataContext>`; `context.Item` is your record) is used only with `ToolTipType` `Item`, `Category` or `Default`. With the default `Data` it is ignored and the built-in tooltip shows; with `None` nothing shows.
- **Guard the template context.** With `ToolTipType.Default`, and for an `IgbDataChart` series `TooltipTemplate`, the template is also rendered with a `null` `context`: `context.Item` then throws a `NullReferenceException` and Blazor shows its error UI. Write `@if (context?.Item is MyRecord row) { … }`.
- **Wrapper.** With `ToolTipType.Default` and on `IgbDataChart` series, the template sits in a `div.ui-tooltip-container` with a white background, a 1px `#666` border and 5px padding. Style your content to suit it.
- **Placement.** `Data`, `Item` and `Category` tooltips render in a `div.ui-chart-pointer-tooltip-container` appended to `<body>`, outside the chart. `IgbDataChart` series tooltips are a `<body>`-level popover unless the series sets `AttachTooltipToRoot="false"` (ignored once a tooltip layer is present). Only `ToolTipType.Default` and `AttachTooltipToRoot="false"` keep the tooltip inside the chart element.
- **Styling templates.** Inline styles and global CSS always work. A selector in the page's `.razor.css` also matches a Razor `TooltipTemplate` when it needs no page ancestor — `.tip`, or a bare `::deep .tip` — because the template's elements keep the scope attribute. Selectors that start from a page element (`.page .tip`, `.page ::deep .tip`) do not match a `<body>`-level tooltip; they match only with `ToolTipType.Default` or `AttachTooltipToRoot="false"`. A template set from JS (`series.tooltipTemplate = …`, for example in a `SeriesAddedScript` handler) carries no scope attribute: use global CSS or inline styles.
- **The `Data` tooltip is drawn on a canvas** — CSS cannot reach its text; use the `DataToolTip*` text parameters.
- **Callout box.** `Data`, `Item` and `Category` tooltips sit on a canvas callout (white fill, grey border) whose colors come from built-in defaults: no token or CSS rule sets them, and `IgbCategoryChart` / `IgbFinancialChart` have no parameter for it ([#493](https://github.com/IgniteUI/igniteui-blazor/issues/493)). In `IgbDataChart`, add an `IgbItemToolTipLayer` or `IgbCategoryToolTipLayer` and set `ToolTipBackground`, `ToolTipBorderBrush`, `ToolTipBorderThickness` (corner radius is not settable). A layer shows only series that have `ShowDefaultTooltip="true"` or a `TooltipTemplate`, and it also needs `IgbDataChartInteractivityModule`.

## Cross-cutting rules

**Explicit `Width` and `Height` are mandatory** — charts do not size to their container on their own. Inside a CSS Grid track also set `min-height: 0` on the cell and `Height="100%"` on the chart, or the track collapses to zero.

**Charts ignore the CSS theme.** Charts, maps, gauges, and sparklines do not read `--ig-*` design tokens. Set their colors through component parameters, using resolved color values rather than `var()` references:

```razor
<IgbCategoryChart Brushes="#4FC3F7 #81C784" Outlines="#4FC3F7 #81C784"
                  MarkerBrushes="#4FC3F7 #81C784"
                  XAxisLabelTextColor="#666666" YAxisLabelTextColor="#666666" />
```

`Brushes`, `Outlines`, `MarkerBrushes`, `MarkerOutlines` are **space-separated strings**, not arrays.

**Set visual parameters inline in markup, not via `@ref` in `OnAfterRenderAsync`** — assigning them to a component reference raises Blazor warning **BL0005** (component parameter set outside its component).

**Other common parameters:** `IsTransitionInEnabled` / `TransitionInDuration` animate entry; `IsSeriesHighlightingEnabled` with `HighlightingMode` / `HighlightingBehavior` controls hover emphasis; `IsHorizontalZoomEnabled` / `IsVerticalZoomEnabled` and `CrosshairsDisplayMode` handle zoom and crosshairs.
