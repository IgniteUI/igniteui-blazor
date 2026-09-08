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

**Other common parameters:** default tooltips appear on hover with no configuration (`TooltipTemplate` replaces them); `IsTransitionInEnabled` / `TransitionInDuration` animate entry; `IsSeriesHighlightingEnabled` with `HighlightingMode` / `HighlightingBehavior` controls hover emphasis; `IsHorizontalZoomEnabled` / `IsVerticalZoomEnabled` and `CrosshairsDisplayMode` handle zoom and crosshairs.
