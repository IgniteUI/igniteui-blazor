# Charts & Data Visualization

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration, see [`setup.md`](./setup.md).

## Contents

- [Chart Type Decision Guide](#chart-type-decision-guide)
- [Category Chart](#category-chart)
- [Data Chart](#data-chart)
- [Financial / Stock Chart](#financial--stock-chart)
- [Pie Chart](#pie-chart)
- [Donut Chart](#donut-chart)
- [Sparkline](#sparkline)
- [Treemap](#treemap)
- [Geographic Map](#geographic-map)
- [Gauges](#gauges)
- [Dashboard Tile](#dashboard-tile)
- [Common Chart Features](#common-chart-features)
- [Key Rules](#key-rules)

---

## Chart Type Decision Guide

| Use case | Recommended component |
|---|---|
| Simple line/area/column/bar charts with minimal config | `IgbCategoryChart` |
| Multiple series types on the same chart, custom axes, annotations | `IgbDataChart` |
| Candlestick / OHLC financial data with range selector | `IgbFinancialChart` / stock chart docs |
| Part-to-whole proportions (slices) | `IgbPieChart` |
| Donut with center label | `IgbDoughnutChart` |
| Inline sparkline for tables or cards | `IgbSparkline` |
| Hierarchical part-to-whole data | Treemap docs |
| Geographic points, shapes, map markers | Geographic Map docs |
| KPI values, ranges, scales, bullet comparisons | Gauge docs |
| Auto-generated dashboard visualization from data | Dashboard Tile docs |

---

## Category Chart

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCategoryChartModule));
```

```razor
<IgbCategoryChart DataSource="SalesData"
                  ChartType="CategoryChartType.Line"
                  Height="400px"
                  Width="100%"
                  YAxisTitle="Revenue (USD)"
                  XAxisTitle="Month"
                  Brushes="DodgerBlue IndianRed"
                  IsHorizontalZoomEnabled="true"
                  IsVerticalZoomEnabled="false"
                  IsTransitionInEnabled="true" />

@code {
    public List<SalesRecord> SalesData { get; set; } = SampleData.GetSales();
}
```

Key attributes: `DataSource`, `ChartType` (`CategoryChartType.Line` / `Area` / `Column` / `Bar` / `Spline` / `SplineArea` / `StepLine` / `Auto`), `Height`, `Width`, `YAxisTitle`, `XAxisTitle`, `Brushes`, `Outlines`, `MarkerBrushes`, `IsTransitionInEnabled`, `IsHorizontalZoomEnabled`, `IsVerticalZoomEnabled`.

> **AGENT INSTRUCTION:** `IgbCategoryChart` auto-detects numeric properties in `DataSource` objects and creates series for them. To control which properties are charted, use `IncludedProperties` or `ExcludedProperties` attributes.

---

## Data Chart

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDataChartCoreModule), typeof(IgbDataChartCategoryModule));
// Add additional modules for other series types:
// IgbDataChartScatterModule, IgbDataChartFinancialModule, etc.
```

```razor
<IgbDataChart Height="500px" Width="100%" IsHorizontalZoomEnabled="true">
    <IgbCategoryXAxis Name="xAxis" DataSource="ChartData" Label="Month" />
    <IgbNumericYAxis Name="yAxis" />
    <IgbLineSeries DataSource="ChartData"
                   XAxisName="xAxis"
                   YAxisName="yAxis"
                   ValueMemberPath="Revenue"
                   Title="Revenue" />
    <IgbColumnSeries DataSource="ChartData"
                     XAxisName="xAxis"
                     YAxisName="yAxis"
                     ValueMemberPath="Expenses"
                     Title="Expenses" />
    <IgbChartLegend Name="legend" />
</IgbDataChart>

@code {
    public List<MonthData> ChartData { get; set; } = SampleData.GetMonthly();
}
```

Common axis types: `IgbCategoryXAxis`, `IgbNumericXAxis`, `IgbNumericYAxis`, `IgbCategoryYAxis`, `IgbTimeXAxis`, `IgbOrdinalTimeXAxis`.

Common series types: `IgbLineSeries`, `IgbAreaSeries`, `IgbColumnSeries`, `IgbBarSeries`, `IgbSplineSeries`, `IgbBubbleSeries`, `IgbScatterSeries`.

Every series must reference its axes by matching `Name` and `XAxisName` / `YAxisName`.

> **AGENT INSTRUCTION:** `IgbDataChart` requires separate NuGet module registrations for each category of series. Always check `get_doc` to find the exact module names, for example category, scatter, polar, radial, stacked, or financial modules.

---

## Financial / Stock Chart

> **Docs:** [Financial Chart](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/charts/types/stock-chart)

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbFinancialChartModule));
```

```razor
<IgbFinancialChart DataSource="StockData"
                   Width="100%"
                   Height="500px"
                   ChartType="FinancialChartType.Candle"
                   ZoomSliderType="FinancialChartZoomSliderType.Line" />

@code {
    public List<StockPrice> StockData { get; set; } = SampleData.GetStockPrices();
}
```

The data source must contain `Open`, `High`, `Low`, `Close`, and `Volume` numeric fields plus a `Date`/`Time` field. Key attributes: `ChartType` (`FinancialChartType.Candle` / `Bar` / `Line`), `ZoomSliderType`, `IsTooltipVisible`, `VolumeType`.

---

## Pie Chart

> **Docs:** [Pie Chart](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/charts/types/pie-chart)

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbPieChartModule));
```

```razor
<IgbPieChart DataSource="SliceData"
             LabelMemberPath="Department"
             ValueMemberPath="Budget"
             Width="500px"
             Height="400px"
             LegendLabelMemberPath="Department"
             SliceClick="OnSliceClick" />

@code {
    public List<BudgetSlice> SliceData { get; set; } = SampleData.GetBudget();

    void OnSliceClick(IgbSliceClickEventArgs e) { /* handle click */ }
}
```

Key attributes: `DataSource`, `LabelMemberPath`, `ValueMemberPath`, `RadiusFactor` (0-1), `StartAngle`, `OthersCategoryThreshold`, `OthersCategoryType`, `LegendLabelMemberPath`, `LegendItemTemplate`.

Events: `SliceClick`.

---

## Donut Chart

> **Docs:** [Doughnut Chart](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/charts/types/donut-chart)

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDoughnutChartModule), typeof(IgbRingSeriesModule));
```

```razor
<IgbDoughnutChart Width="400px" Height="400px">
    <IgbRingSeries DataSource="DonutData"
                   LabelMemberPath="Category"
                   ValueMemberPath="Share" />
</IgbDoughnutChart>
```

Supports multiple `IgbRingSeries` for concentric rings.

---

## Sparkline

> **Docs:** [Sparkline](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/charts/types/sparkline-chart)

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSparklineModule));
```

```razor
<IgbSparkline DataSource="TrendData"
              ValueMemberPath="Value"
              DisplayType="SparklineDisplayType.Line"
              Width="120px"
              Height="40px"
              Brushes="DodgerBlue"
              LineThickness="2" />
```

Key attributes: `DataSource`, `ValueMemberPath`, `DisplayType` (`SparklineDisplayType.Line` / `Area` / `Column` / `WinLoss`), `Width`, `Height`, `Brushes`, `LineThickness`, `MarkerVisibility`, `FirstMarkerVisibility`, `LastMarkerVisibility`, `HighMarkerVisibility`, `LowMarkerVisibility`.

---

## Treemap

> **Docs:** [Treemap](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/charts/types/treemap-chart)

Use Treemap for hierarchical, weighted, part-to-whole data. Always call `get_doc("treemap-chart")` before writing markup because hierarchy binding, member paths, and layout options are component-specific.

---

## Geographic Map

> **Docs:** [Geographic Map](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/geo-map-type-scatter-area-series)

Use Geographic Map for map backgrounds, shape files, geographic points, bubbles, and marker layers. Do not adapt Data Chart axis or series examples to maps unless the Blazor Geographic Map docs explicitly show the same API.

---

## Gauges

> **Docs:** [Radial Gauge](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/radial-gauge), [Linear Gauge](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/linear-gauge), [Bullet Graph](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/bullet-graph)

Use gauges for KPI values, ranges, thresholds, progress-style numeric summaries, and bullet comparisons. Select the exact gauge doc first, then use the MCP-documented property names for ranges, labels, scale, and value binding.

---

## Dashboard Tile

> **Docs:** [Dashboard Tile](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/dashboard-tile)

Use Dashboard Tile when the requested component should infer or render compact dashboard visualizations from bound data. Verify supported chart modes and binding shape with MCP before producing code.

---

## Common Chart Features

### Legends

```razor
<IgbLegend @ref="Legend" Orientation="LandmarkOrientation.Horizontal" />
<IgbCategoryChart DataSource="Data" Legend="Legend" />
```

### Tooltips

```razor
<IgbCategoryChart IsDefaultTooltipEnabled="true" />
<!-- Or custom tooltip via ToolTipTemplate slot -->
```

### Animations

```razor
<IgbCategoryChart IsTransitionInEnabled="true" TransitionInDuration="1000" />
```

### Highlighting

```razor
<IgbCategoryChart IsSeriesHighlightingEnabled="true"
                  HighlightingBehavior="SeriesHighlightingBehavior.BrightenSpecificItemsAndDarkenOthers" />
```

### Zooming and Panning

```razor
<IgbCategoryChart IsHorizontalZoomEnabled="true"
                  IsVerticalZoomEnabled="false"
                  CrosshairsDisplayMode="CrosshairsDisplayMode.Both" />
```

---

## Key Rules

1. **Always call `get_doc` for each chart, map, gauge, or dashboard component before writing code.** Visualization APIs are extensive and version-specific.
2. **`IgbCategoryChart` is the fastest path for standard charts.** It auto-generates series from data. Use `IgbDataChart` only when you need multiple series types, custom axes, or advanced features.
3. **`IgbDataChart` requires one module per series category.** Check `get_doc` for the exact module name.
4. **Financial chart data must have `Open`, `High`, `Low`, `Close` fields.** If the data model is different, the chart will not render correctly.
5. **Always set explicit `Width` and `Height` on charts.** Charts do not auto-size to their container without a height.
6. **`IgbDataChart` series must match axes by name.** The `XAxisName` / `YAxisName` on each series must match the `Name` attribute of the axis component.
