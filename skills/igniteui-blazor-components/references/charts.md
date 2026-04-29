# Charts, Gauges & Maps — Ignite UI for Blazor

This reference covers the Ignite UI for Blazor data visualization (DV) suite: Category Chart, Financial Chart, Data Chart, Pie Chart, Sparkline, Geographic Map, and Gauges.

> **Note:** Chart and gauge components are available in the licensed `IgniteUI.Blazor` package. They are not included in `IgniteUI.Blazor.Lite`.

---

## Registration Pattern

All DV components follow the same registration pattern in **Program.cs**:

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbCategoryChartModule),
    typeof(IgbFinancialChartModule),
    typeof(IgbDataChartModule),
    typeof(IgbPieChartModule),
    typeof(IgbDataPieChartModule),
    typeof(IgbSparklineModule),
    typeof(IgbGeographicMapModule),
    typeof(IgbLinearGaugeModule),
    typeof(IgbRadialGaugeModule),
    typeof(IgbBulletGraphModule)
    // Register only the modules you actually use
);
```

All components come from the `IgniteUI.Blazor.Controls` namespace — no additional `@using` directives beyond `@using IgniteUI.Blazor.Controls` are needed.

---

## IgbCategoryChart

A simplified chart that auto-detects data structure and renders area, bar, column, line, point, spline, step, and waterfall charts.

### Basic usage

```razor
<IgbCategoryChart Height="400px" Width="100%"
                   ChartType="CategoryChartType.Column"
                   DataSource="salesData"
                   ChartTitle="Monthly Sales"
                   YAxisTitle="Revenue ($)"
                   XAxisTitle="Month" />

@code {
    private List<SalesRecord> salesData = new()
    {
        new("Jan", 12000),
        new("Feb", 15000),
        new("Mar", 18000),
        new("Apr", 14000),
        new("May", 21000),
        new("Jun", 19500)
    };

    record SalesRecord(string Month, double Revenue);
}
```

### Multi-series

```razor
<IgbCategoryChart Height="400px" Width="100%"
                   ChartType="CategoryChartType.Line"
                   DataSource="multiSeriesData"
                   ChartTitle="Product Comparison" />

@code {
    private List<ProductData> multiSeriesData = new()
    {
        new("Q1", 100, 80, 120),
        new("Q2", 130, 95, 110),
        new("Q3", 110, 120, 140),
        new("Q4", 150, 140, 130)
    };

    record ProductData(string Quarter, double ProductA, double ProductB, double ProductC);
}
```

The chart auto-detects numeric properties as separate series.

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `DataSource` | `object` | Data collection (IEnumerable) |
| `ChartType` | `CategoryChartType` | `Auto`, `Area`, `Bar`, `Column`, `Line`, `Point`, `Spline`, `SplineArea`, `StepArea`, `StepLine`, `Waterfall` |
| `ChartTitle` | `string` | Chart title |
| `XAxisTitle` | `string` | X-axis label |
| `YAxisTitle` | `string` | Y-axis label |
| `Height` | `string` | Chart height (e.g., `"400px"`) |
| `Width` | `string` | Chart width (e.g., `"100%"`) |
| `IsHorizontalZoomEnabled` | `bool` | Enables horizontal zoom |
| `IsVerticalZoomEnabled` | `bool` | Enables vertical zoom |
| `Brushes` | `string` | Comma-separated fill colors (e.g., `"#FF6B6B, #4ECDC4, #45B7D1"`) |
| `Outlines` | `string` | Comma-separated outline colors |
| `MarkerTypes` | `string` | Marker shapes (e.g., `"Circle, Triangle, Square"`) |
| `YAxisMinimumValue` | `double` | Y-axis minimum |
| `YAxisMaximumValue` | `double` | Y-axis maximum |
| `IncludedProperties` | `string[]` | Specific data properties to include |
| `ExcludedProperties` | `string[]` | Data properties to exclude |
| `ToolTipType` | `ToolTipType` | `Default`, `Item`, `Category`, `None` |
| `CrosshairsDisplayMode` | `CrosshairsDisplayMode` | `Default`, `None`, `Horizontal`, `Vertical`, `Both` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCategoryChartModule));
```

---

## IgbFinancialChart

A chart optimized for financial (OHLC) data with candlestick/bar rendering, volume panes, and technical indicators.

### Basic usage

```razor
<IgbFinancialChart Height="600px" Width="100%"
                    DataSource="stockData"
                    ChartType="FinancialChartType.Candle"
                    IsToolbarVisible="true"
                    ZoomSliderType="FinancialChartZoomSliderType.Candle" />

@code {
    private List<StockPrice> stockData = new()
    {
        new(new DateTime(2025, 1, 2), 150.0, 155.0, 148.0, 153.0, 1200000),
        new(new DateTime(2025, 1, 3), 153.0, 158.0, 151.0, 156.0, 1350000),
        new(new DateTime(2025, 1, 6), 156.0, 160.0, 154.0, 159.0, 1100000)
    };

    record StockPrice(DateTime Date, double Open, double High, double Low, double Close, double Volume);
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `DataSource` | `object` | OHLCV data collection |
| `ChartType` | `FinancialChartType` | `Bar`, `Candle`, `Column`, `Line`, `Area` |
| `Height` / `Width` | `string` | Dimensions |
| `IsToolbarVisible` | `bool` | Shows the toolbar with chart type selector, indicators, etc. |
| `ZoomSliderType` | `FinancialChartZoomSliderType` | Type of the zoom slider at the bottom |
| `VolumePaneVisible` | `bool` | Shows the volume pane |
| `IndicatorTypes` | `string` | Comma-separated indicator names |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbFinancialChartModule));
```

---

## IgbDataChart

An advanced chart with explicitly defined axes and series. Use when you need full control over chart composition.

### Basic usage — line series with category X axis and numeric Y axis

```razor
<IgbDataChart Height="400px" Width="100%">
    <IgbCategoryXAxis Name="xAxis" DataSource="data" Label="Category" />
    <IgbNumericYAxis Name="yAxis" />
    <IgbLineSeries XAxisName="xAxis" YAxisName="yAxis"
                    DataSource="data" ValueMemberPath="Value"
                    Title="Sales"
                    Brush="#4ECDC4" />
</IgbDataChart>

@code {
    private List<DataItem> data = new()
    {
        new("Jan", 100),
        new("Feb", 130),
        new("Mar", 115),
        new("Apr", 150)
    };

    record DataItem(string Category, double Value);
}
```

### Multi-series with different types

```razor
<IgbDataChart Height="500px" Width="100%">
    <IgbCategoryXAxis Name="xAxis" DataSource="data" Label="Month" />
    <IgbNumericYAxis Name="yAxis" Title="Revenue" />
    <IgbNumericYAxis Name="yAxis2" Title="Units" LabelLocation="OutsideRight" />

    <IgbColumnSeries XAxisName="xAxis" YAxisName="yAxis"
                      DataSource="data" ValueMemberPath="Revenue"
                      Title="Revenue" Brush="#3498db" />
    <IgbLineSeries XAxisName="xAxis" YAxisName="yAxis2"
                    DataSource="data" ValueMemberPath="Units"
                    Title="Units Sold" Brush="#e74c3c" />
</IgbDataChart>

@code {
    private List<MixedData> data = new()
    {
        new("Jan", 50000, 320),
        new("Feb", 62000, 410),
        new("Mar", 55000, 380),
        new("Apr", 71000, 450)
    };

    record MixedData(string Month, double Revenue, double Units);
}
```

### Axis types

| Axis Component | Description |
|---|---|
| `IgbCategoryXAxis` | String/category labels on X axis |
| `IgbCategoryYAxis` | String/category labels on Y axis |
| `IgbNumericXAxis` | Numeric values on X axis |
| `IgbNumericYAxis` | Numeric values on Y axis |
| `IgbTimeXAxis` | DateTime values on X axis |
| `IgbCategoryAngleAxis` | Angle axis for polar/radial charts |
| `IgbNumericAngleAxis` | Numeric angle axis |
| `IgbNumericRadiusAxis` | Radius axis for polar/radial charts |

### Series types (partial list)

| Series Component | Chart Type |
|---|---|
| `IgbLineSeries` | Line chart |
| `IgbSplineSeries` | Smooth line chart |
| `IgbAreaSeries` | Area chart |
| `IgbSplineAreaSeries` | Smooth area chart |
| `IgbColumnSeries` | Vertical column chart |
| `IgbBarSeries` | Horizontal bar chart |
| `IgbScatterSeries` | Scatter/bubble (requires NumericX + NumericY) |
| `IgbBubbleSeries` | Bubble chart with radius |
| `IgbStepLineSeries` | Stepped line chart |
| `IgbWaterfallSeries` | Waterfall chart |
| `IgbStackedColumnSeries` | Stacked columns |
| `IgbStacked100ColumnSeries` | 100% stacked columns |
| `IgbPolarLineSeries` | Polar line |
| `IgbRadialLineSeries` | Radial line |
| `IgbFinancialPriceSeries` | OHLC in DataChart |

### Common series parameters

| Parameter | Type | Description |
|---|---|---|
| `DataSource` | `object` | Data collection |
| `XAxisName` | `string` | Name of the X axis to bind to |
| `YAxisName` | `string` | Name of the Y axis to bind to |
| `ValueMemberPath` | `string` | Property name for the value |
| `Title` | `string` | Series name (shown in legend) |
| `Brush` | `string` | Fill/line color |
| `Outline` | `string` | Outline color |
| `Thickness` | `double` | Line thickness |
| `MarkerType` | `MarkerType` | `None`, `Circle`, `Triangle`, `Square`, `Diamond`, `Pentagon`, `Hexagon`, `Octagon`, `Star4`, `Star5`, `Star6`, `Star8`, `Star10` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDataChartModule));
```

---

## IgbPieChart & IgbDataPieChart

Part-to-whole charts.

### IgbPieChart — basic

```razor
<IgbPieChart Height="400px" Width="400px"
              DataSource="budgetData"
              LabelMemberPath="Category"
              ValueMemberPath="Amount"
              LabelsPosition="PieChartLabelsPosition.OutsideEnd" />

@code {
    private List<BudgetItem> budgetData = new()
    {
        new("Housing", 1500),
        new("Food", 600),
        new("Transport", 400),
        new("Entertainment", 200),
        new("Savings", 800)
    };

    record BudgetItem(string Category, double Amount);
}
```

### IgbDataPieChart — enhanced

```razor
<IgbDataPieChart Height="400px" Width="400px"
                  DataSource="marketShare"
                  LabelMemberPath="Company"
                  ValueMemberPath="Share"
                  InnerExtent="0.3"
                  OthersCategoryThreshold="5"
                  OthersCategoryType="OthersCategoryType.Number" />

@code {
    private List<MarketData> marketShare = new()
    {
        new("Company A", 35),
        new("Company B", 25),
        new("Company C", 20),
        new("Company D", 10),
        new("Others", 10)
    };

    record MarketData(string Company, double Share);
}
```

### Common pie parameters

| Parameter | Type | Description |
|---|---|---|
| `DataSource` | `object` | Data collection |
| `LabelMemberPath` | `string` | Property for labels |
| `ValueMemberPath` | `string` | Property for values |
| `LabelsPosition` | `PieChartLabelsPosition` | `None`, `BestFit`, `Center`, `InsideEnd`, `OutsideEnd` |
| `InnerExtent` | `double` | Inner radius (0-1) for donut charts |
| `Brushes` | `string` | Comma-separated colors |
| `Outlines` | `string` | Comma-separated outline colors |
| `LegendItemBadgeTemplate` | `object` | Custom legend badge |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbPieChartModule));
// or
builder.Services.AddIgniteUIBlazor(typeof(IgbDataPieChartModule));
```

---

## IgbSparkline

A mini inline chart for embedding within text, tables, or cards.

### Basic usage

```razor
<IgbSparkline Height="40px" Width="200px"
               DataSource="trend"
               DisplayType="SparklineDisplayType.Line"
               ValueMemberPath="Value" />

<IgbSparkline Height="40px" Width="200px"
               DataSource="trend"
               DisplayType="SparklineDisplayType.Area"
               ValueMemberPath="Value"
               Brush="#4ECDC4" />

<IgbSparkline Height="40px" Width="200px"
               DataSource="trend"
               DisplayType="SparklineDisplayType.Column"
               ValueMemberPath="Value" />

@code {
    private List<TrendPoint> trend = new()
    {
        new(10), new(25), new(18), new(30), new(22), new(35), new(28)
    };

    record TrendPoint(double Value);
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `DataSource` | `object` | Data collection |
| `ValueMemberPath` | `string` | Property for the value |
| `LabelMemberPath` | `string` | Property for the label |
| `DisplayType` | `SparklineDisplayType` | `Line`, `Area`, `Column`, `WinLoss` |
| `Brush` | `string` | Fill/line color |
| `NegativeBrush` | `string` | Color for negative values |
| `LineThickness` | `double` | Line width |
| `MarkerVisibility` | `Visibility` | `Visible`, `Collapsed` |
| `HighMarkerVisibility` | `Visibility` | Show marker on highest point |
| `LowMarkerVisibility` | `Visibility` | Show marker on lowest point |
| `FirstMarkerVisibility` | `Visibility` | Show marker on first point |
| `LastMarkerVisibility` | `Visibility` | Show marker on last point |
| `TrendLineType` | `TrendLineType` | `None`, `LinearFit`, `QuadraticFit`, `CubicFit`, `ExponentialFit`, etc. |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbSparklineModule));
```

---

## IgbGeographicMap

A map component for geographic data visualization.

### Basic usage — symbol series

```razor
<IgbGeographicMap Height="500px" Width="100%"
                   Zoomable="true">
    <IgbGeographicSymbolSeries DataSource="locations"
                                LatitudeMemberPath="Lat"
                                LongitudeMemberPath="Lon"
                                MarkerType="MarkerType.Circle"
                                MarkerBrush="#e74c3c"
                                MarkerOutline="#c0392b" />
</IgbGeographicMap>

@code {
    private List<GeoLocation> locations = new()
    {
        new("New York", 40.7128, -74.0060),
        new("London", 51.5074, -0.1278),
        new("Tokyo", 35.6762, 139.6503)
    };

    record GeoLocation(string City, double Lat, double Lon);
}
```

### Proportional symbol series

```razor
<IgbGeographicMap Height="500px" Width="100%">
    <IgbGeographicProportionalSymbolSeries DataSource="cityPopulations"
                                            LatitudeMemberPath="Lat"
                                            LongitudeMemberPath="Lon"
                                            RadiusMemberPath="Population"
                                            RadiusScale="@sizeScale"
                                            MarkerType="MarkerType.Circle"
                                            FillMemberPath="Population"
                                            FillScale="@colorScale" />
</IgbGeographicMap>
```

### Map series types

| Series Component | Description |
|---|---|
| `IgbGeographicSymbolSeries` | Point markers at lat/lon positions |
| `IgbGeographicProportionalSymbolSeries` | Sized markers based on data values |
| `IgbGeographicShapeSeries` | Renders shape/polygon data |
| `IgbGeographicPolylineSeries` | Renders line/path data |
| `IgbGeographicHighDensityScatterSeries` | Heat-map style for large datasets |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbGeographicMapModule));
```

---

## IgbLinearGauge / IgbRadialGauge / IgbBulletGraph

Gauge and bullet-graph components for KPI displays.

### IgbLinearGauge

```razor
<IgbLinearGauge Height="80px" Width="300px"
                 Value="72"
                 MinimumValue="0" MaximumValue="100"
                 Interval="10"
                 NeedleBrush="#4ECDC4"
                 ScaleBrush="#e0e0e0"
                 BackingBrush="white">
    <IgbLinearGraphRange StartValue="0" EndValue="30" Brush="#e74c3c" />
    <IgbLinearGraphRange StartValue="30" EndValue="70" Brush="#f39c12" />
    <IgbLinearGraphRange StartValue="70" EndValue="100" Brush="#2ecc71" />
</IgbLinearGauge>
```

### IgbRadialGauge

```razor
<IgbRadialGauge Height="300px" Width="300px"
                 Value="72"
                 MinimumValue="0" MaximumValue="100"
                 Interval="10"
                 NeedleBrush="#4ECDC4"
                 ScaleStartAngle="135"
                 ScaleEndAngle="45"
                 ScaleBrush="#e0e0e0"
                 BackingBrush="white">
    <IgbRadialGaugeRange StartValue="0" EndValue="30" Brush="#e74c3c" />
    <IgbRadialGaugeRange StartValue="30" EndValue="70" Brush="#f39c12" />
    <IgbRadialGaugeRange StartValue="70" EndValue="100" Brush="#2ecc71" />
</IgbRadialGauge>
```

### IgbBulletGraph

```razor
<IgbBulletGraph Height="80px" Width="400px"
                 Value="72"
                 TargetValue="85"
                 MinimumValue="0" MaximumValue="100"
                 Interval="10"
                 ValueBrush="#3498db"
                 TargetValueBrush="#2c3e50">
    <IgbLinearGraphRange StartValue="0" EndValue="50" Brush="#ecf0f1" />
    <IgbLinearGraphRange StartValue="50" EndValue="75" Brush="#bdc3c7" />
    <IgbLinearGraphRange StartValue="75" EndValue="100" Brush="#95a5a6" />
</IgbBulletGraph>
```

### Common gauge parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `double` | Current gauge value |
| `MinimumValue` | `double` | Scale minimum |
| `MaximumValue` | `double` | Scale maximum |
| `Interval` | `double` | Major tick interval |
| `MinorTickCount` | `double` | Minor ticks between major ticks |
| `NeedleBrush` | `string` | Needle/value indicator color |
| `ScaleBrush` | `string` | Scale track color |
| `BackingBrush` | `string` | Background color |
| `Height` / `Width` | `string` | Dimensions |
| `TargetValue` | `double` | Target value (BulletGraph only) |
| `TargetValueBrush` | `string` | Target indicator color (BulletGraph only) |
| `ScaleStartAngle` | `double` | Start angle (RadialGauge only) |
| `ScaleEndAngle` | `double` | End angle (RadialGauge only) |

### Range parameters

| Parameter | Type | Description |
|---|---|---|
| `StartValue` | `double` | Range start |
| `EndValue` | `double` | Range end |
| `Brush` | `string` | Range color |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbLinearGaugeModule),
    typeof(IgbRadialGaugeModule),
    typeof(IgbBulletGraphModule)
);
```

---

## Common API Members (All Charts)

| Parameter | Type | Description |
|---|---|---|
| `Height` | `string` | Component height (required — charts don't auto-size) |
| `Width` | `string` | Component width |
| `DataSource` | `object` | Data collection to bind |
| `Brushes` | `string` | Comma-separated fill colors for series |
| `Outlines` | `string` | Comma-separated outline colors |
| `ChartTitle` | `string` | Title text (CategoryChart, FinancialChart) |
| `Subtitle` | `string` | Subtitle text |
| `IsLegendVisible` | `bool` | Shows/hides the integrated legend |

---

## Data Binding Patterns

Charts bind to C# collections. Use `List<T>`, `ObservableCollection<T>`, or arrays:

```csharp
// Simple record — properties are auto-detected
record SalesData(string Month, double Revenue, double Profit);

// The chart reads public properties via reflection
private List<SalesData> data = new()
{
    new("Jan", 10000, 3000),
    new("Feb", 12000, 4000)
};
```

### Refreshing data

To update a chart after modifying data, reassign the `DataSource`:

```csharp
private void RefreshChart()
{
    // Modify the list
    data.Add(new SalesData("Mar", 15000, 5000));

    // Reassign to trigger Blazor change detection
    data = new List<SalesData>(data);
}
```

Or use `ObservableCollection<T>` for automatic updates:

```csharp
private ObservableCollection<SalesData> data = new(new[]
{
    new SalesData("Jan", 10000, 3000),
    new SalesData("Feb", 12000, 4000)
});

private void AddDataPoint()
{
    data.Add(new SalesData("Mar", 15000, 5000));
    // Chart updates automatically
}
```

---

## Key Rules

1. **Height is required** — All chart and gauge components require an explicit `Height`. Without it, the component renders with zero height.
2. **Licensed package** — Charts, gauges, and maps require the `IgniteUI.Blazor` package (not `IgniteUI.Blazor.Lite`).
3. **Register each module** — Every chart type needs its module registered (e.g., `typeof(IgbCategoryChartModule)`).
4. **DataChart axes** — In `IgbDataChart`, every series must reference axes by `Name`. Forgetting to add axes or mismatching names causes the series to not render.
5. **DataSource type** — Use `object` for the parameter type but pass strongly-typed collections. The chart reads public properties via reflection.
6. **Brushes/Outlines** — Pass as comma-separated CSS color strings: `"#FF6B6B, #4ECDC4, #45B7D1"`.
7. **CategoryChart auto-detection** — `IgbCategoryChart` automatically creates series for each numeric property in the data. Use `IncludedProperties` / `ExcludedProperties` to control which properties are charted.

---

## See Also

- [setup.md](setup.md) — Project setup and NuGet package installation
- [layout-manager.md](layout-manager.md) — TileManager for dashboard layouts containing charts
- [data-display.md](data-display.md) — Cards for chart containers, progress indicators
- [directives.md](directives.md) — Buttons for chart toolbar actions
