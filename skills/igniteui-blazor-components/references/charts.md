# Charts & Data Visualization

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration, see [`setup.md`](./setup.md).

## Contents

- [Overview](#overview)
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
## Overview

Ignite UI for Blazor provides 65+ chart types for data visualization. Charts are part of the IgniteUI.Blazor package (trial watermarked version available in the IgniteUI.Blazor.Trial public NuGet package).
This reference gives high-level guidance on when to use some of the chart types, their key features, and common API members. For detailed documentation, call `get_doc` and `get_api_reference` from `igniteui-cli` with the specific chart component or feature you're interested in.

## Chart Type Decision Guide

| Use case | Recommended component |
|---|---|
| Simple line/area/column/point/spline/waterfall charts with minimal config | `IgbCategoryChart` |
| Multiple series types on the same chart, custom axes, annotations | `IgbDataChart` |
| Candlestick / OHLC financial data with range selector | `IgbFinancialChart` |
| Part-to-whole proportions (slices) | `IgbPieChart` |
| Donut with center label | `IgbDoughnutChart` |
| Inline sparkline for tables or cards | `IgbSparkline` |
| Hierarchical part-to-whole data | `IgbTreemap` |
| Geographic points, shapes, map markers | `IgbGeographicMap` |
| KPI values, ranges, scales, bullet comparisons | `IgbLinearGauge` / `IgbRadialGauge` / `IgbBulletGraph` |
| Auto-generated dashboard visualization from data | `IgbDashboardTile` |

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

> **AGENT INSTRUCTION:** `CategoryChartType.Bar` does **not** exist. For a horizontal bar-style chart, use `CategoryChartType.Column` (vertical) or switch to `IgbDataChart` with `IgbBarSeries` for true horizontal bars. Never generate `ChartType="CategoryChartType.Bar"`.

> **AGENT INSTRUCTION:** `IgbCategoryChart` auto-detects which string or date property in the data to use as X-axis labels. To control which data properties are charted, use `IncludedProperties` or `ExcludedProperties` (string arrays).

Brush list properties such as `Brushes`, `Outlines`, `MarkerBrushes`, and `MarkerOutlines` are **string** parameters. Separate multiple colors with spaces, as in `Brushes="DodgerBlue IndianRed"`.

> **AGENT INSTRUCTION:** `IgbCategoryChart` auto-detects numeric properties in `DataSource` objects and creates series for them. To control which properties are charted, use `IncludedProperties` or `ExcludedProperties` attributes. These are **array** parameters - bind them with `@(new string[] { ... })` syntax:
> ```razor
> <IgbCategoryChart DataSource="SalesData"
>                   ExcludedProperties='@(new string[] { "Id", "InternalCode" })' />
> ```
> Do NOT pass a plain string like `ExcludedProperties="Id"` - this causes a type mismatch error.

---

## Data Chart

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDataChartCoreModule), typeof(IgbDataChartCategoryModule));
// Add additional modules for other series types:
// IgbDataChartScatterModule, IgbDataChartFinancialModule, etc.
```

```razor
<IgbLegend @ref="Legend" Orientation="LegendOrientation.Horizontal" />
<IgbDataChart Legend="Legend" Height="500px" Width="100%" IsHorizontalZoomEnabled="true">
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
</IgbDataChart>

@code {
    public List<MonthData> ChartData { get; set; } = SampleData.GetMonthly();

    private IgbLegend _legend;
    private IgbLegend Legend
    {
        get { return _legend; }
        set { _legend = value; StateHasChanged(); } // triggers re-render so Legend="Legend" receives the ref
    }
}
```

Every series must reference its axes by matching `Name` and `XAxisName` / `YAxisName`.

> **AGENT INSTRUCTION:** `IgbDataChart` requires separate NuGet module registrations for each category of series. Always check `get_doc` to find the exact module names, for example category, scatter, polar, radial, stacked, or financial modules.

---

## Financial / Stock Chart

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

The data source must contain `Open`, `High`, `Low`, `Close`, and `Volume` numeric fields plus a `Date`/`Time` field.

---

## Pie Chart

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbPieChartModule), typeof(IgbItemLegendModule));
```

```razor
<IgbItemLegend @ref="PieLegend" Orientation="LegendOrientation.Horizontal" />
<IgbPieChart DataSource="SliceData"
             LabelMemberPath="Department"
             ValueMemberPath="Budget"
             Width="500px"
             Height="400px"
             LegendLabelMemberPath="Department"
             Legend="PieLegend"
             SliceClick="OnSliceClick" />

@code {
    public List<BudgetSlice> SliceData { get; set; } = SampleData.GetBudget();

    private IgbItemLegend _pieLegend;
    private IgbItemLegend PieLegend
    {
        get { return _pieLegend; }
        set { _pieLegend = value; StateHasChanged(); }
    }

    void OnSliceClick(IgbSliceClickEventArgs e) { /* handle click */ }
}
```

---

## Donut Chart

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

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSparklineModule));
```

```razor
<IgbSparkline DataSource="TrendData"
              ValueMemberPath="Value"
              DisplayType="SparklineDisplayType.Line"
              Width="120px"
              Height="40px"
              Brush="DodgerBlue"
              LineThickness="2" />
```

---

## Treemap

Always call `get_doc("treemap-chart")` before writing markup because hierarchy binding, member paths, and layout options are component-specific.

---

## Geographic Map

Use Geographic Map for map backgrounds, shape files, geographic points, bubbles, and marker layers. Do not adapt Data Chart axis or series examples to maps unless the Blazor Geographic Map docs explicitly show the same API.

---

## Gauges

Use gauges for KPI values, ranges, thresholds, progress-style numeric summaries, and bullet comparisons. Select the exact gauge doc first, then use the MCP-documented property names for ranges, labels, scale, and value binding.

---

## Dashboard Tile

Use Dashboard Tile when the requested component should infer or render compact dashboard visualizations from bound data. Verify supported chart modes and binding shape with MCP before producing code.

---

## Common Chart Features

### Legends

```razor
<IgbLegend @ref="Legend" Orientation="LegendOrientation.Horizontal" />
<IgbCategoryChart DataSource="Data" Legend="Legend" />

@code {
    private IgbLegend _legend;
    private IgbLegend Legend
    {
        get { return _legend; }
        set { _legend = value; StateHasChanged(); } // triggers re-render so Legend="Legend" receives the ref
    }
}
```

### Tooltips

```razor
<!-- Default tooltip is shown automatically on hover — no property needed -->
<IgbCategoryChart DataSource="Data" />

<!-- Custom tooltip via TooltipTemplate -->
<IgbCategoryChart DataSource="Data" TooltipTemplate="@TooltipFragment" />
```

### Animations

```razor
<IgbCategoryChart IsTransitionInEnabled="true" TransitionInDuration="1000" />
```

### Highlighting

```razor
<IgbCategoryChart IsSeriesHighlightingEnabled="true"
                  HighlightingMode="SeriesHighlightingMode.FadeOthers"
                  HighlightingBehavior="SeriesHighlightingBehavior.NearestItemsAndSeries" />
```

### Zooming and Panning

```razor
<IgbCategoryChart IsHorizontalZoomEnabled="true"
                  IsVerticalZoomEnabled="false"
                  CrosshairsDisplayMode="CrosshairsDisplayMode.Both" />
```

---

## Key Rules

1. **`IgbCategoryChart` is the fastest path for standard charts.** It auto-generates series from data. Use `IgbDataChart` only when you need multiple series types, custom axes, or advanced features.
2. **`IgbDataChart` requires one module per series category.** Check `get_doc` for the exact module name.
3. **Financial chart data must have `Open`, `High`, `Low`, `Close` fields.** If the data model is different, the chart will not render correctly.
4. **Always set explicit `Width` and `Height` on charts.** Charts do not auto-size to their container without a height.
5. **`IgbDataChart` series must match axes by name.** The `XAxisName` / `YAxisName` on each series must match the `Name` attribute of the axis component.
6. **`IncludedProperties` and `ExcludedProperties` are `string[]` arrays.** Bind with `@(new string[] { "Prop1", "Prop2" })`. Do not pass a plain string.
7. **`CategoryChartType.Bar` does not exist.** Use `Column` for vertical bars in `IgbCategoryChart`. For horizontal bars, use `IgbDataChart` with `IgbBarSeries`.
8. **Do not use `XAxisLabel` to specify a data field.** It controls label formatting only. Use `IncludedProperties`/`ExcludedProperties` on `IgbCategoryChart`, or `Label` on `IgbCategoryXAxis` for `IgbDataChart`.
9. **Scatter and bubble series use `XMemberPath`/`YMemberPath`, not `XAxisMemberPath`.** For `IgbScatterSeries` and `IgbBubbleSeries`, map data fields with `XMemberPath="FieldX"` and `YMemberPath="FieldY"`. `IgbBubbleSeries` additionally requires `RadiusMemberPath`.
