# Grid Features

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Sorting](#sorting)
- [Filtering](#filtering)
- [Advanced Filtering](#advanced-filtering)
- [Column Pinning](#column-pinning)
- [Column Hiding](#column-hiding)
- [Column Moving](#column-moving)
- [Column Resizing](#column-resizing)
- [Summaries](#summaries)
- [Group By](#group-by)
- [Selection](#selection)
- [Row Adding](#row-adding)
- [Row Actions](#row-actions)
- [Search](#search)
- [Keyboard Navigation](#keyboard-navigation)
- [Toolbar](#toolbar)
- [Excel & CSV Export](#excel--csv-export)
- [Key Rules](#key-rules)

---

## Sorting

```razor
<!-- Enable sorting on columns -->
<IgbColumn Field="Name" Sortable="true" />
<IgbColumn Field="Price" Sortable="true" />
```

Programmatic sort:

```razor
@code {
    public IgbGrid Grid { get; set; }

    void SortByPrice()
    {
        Grid.SortAsync(new IgbSortingExpression[]
        {
            new IgbSortingExpression { FieldName = "Price", Dir = SortingDirection.Desc }
        });
    }

    void ClearSort() => Grid.ClearSortAsync();
}
```

Multi-column sort: hold Shift while clicking headers, or pass multiple `IgbSortingExpression` objects to `SortAsync`.

---

## Filtering

```razor
<!-- Quick filter (filter row) -->
<IgbGrid AllowFiltering="true" FilterMode="FilterMode.QuickFilter">
    <IgbColumn Field="Name" Filterable="true" />
</IgbGrid>

<!-- Excel-style filter dropdowns -->
<IgbGrid AllowFiltering="true" FilterMode="FilterMode.ExcelStyleFilter">
    <IgbColumn Field="Name" Filterable="true" />
</IgbGrid>
```

Programmatic filter:

```csharp
Grid.FilterAsync("Price", 100, IgbFilteringOperand.LessThan);
Grid.ClearFilterAsync("Price");
Grid.ClearFilterAsync(); // clear all
```

---

## Advanced Filtering

Use Advanced Filtering when users need a condition builder across multiple columns.

```razor
<IgbGrid Data="Data" AllowAdvancedFiltering="true">
    <IgbGridToolbar>
        <IgbGridToolbarAdvancedFiltering />
    </IgbGridToolbar>
    <IgbColumn Field="Name" />
    <IgbColumn Field="Price" DataType="GridColumnDataType.Number" />
</IgbGrid>
```

Always confirm toolbar child component names and filtering expression APIs from `grid-advanced-filtering`.

---

## Column Pinning

```razor
<!-- Declarative pin -->
<IgbColumn Field="ID" Pinned="true" />

<!-- Runtime API -->
@code {
    void PinName() => Grid.PinColumnAsync("ProductName", 0);
    void UnpinName() => Grid.UnpinColumnAsync("ProductName");
}
```

Pin from toolbar: `<IgbGridToolbarPinning />` child inside `IgbGridToolbar`.

---

## Column Hiding

```razor
<!-- Declarative hide -->
<IgbColumn Field="InternalCode" Hidden="true" />
```

Show/hide from toolbar: add `<IgbGridToolbarHiding />` to the toolbar. Users can toggle columns from the column chooser panel.

Programmatic:

```csharp
void ToggleColumn() => Grid.GetColumnByNameAsync("InternalCode")
    .ContinueWith(t => t.Result.Hidden = !t.Result.Hidden);
```

---

## Column Moving

```razor
<IgbGrid Moving="true">
    <IgbColumn Field="Name" />
    <IgbColumn Field="Price" />
</IgbGrid>
```

Enable on individual columns instead of the grid level:

```razor
<IgbGrid>
    <IgbColumn Field="Name" MovingEnabled="true" />
</IgbGrid>
```

---

## Column Resizing

```razor
<IgbGrid Data="Data">
    <IgbColumn Field="Name" Resizable="true" MinWidth="120px" MaxWidth="360px" />
    <IgbColumn Field="Price" Resizable="true" Width="140px" />
</IgbGrid>
```

Column resizing can be configured per column. Use `grid-column-resizing` to confirm current resize events, autosize APIs, and min/max width behavior before writing code.

---

## Summaries

```razor
<IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" HasSummary="true" />
<IgbColumn Field="InStock" DataType="GridColumnDataType.Boolean" HasSummary="true" />
```

The grid shows built-in summaries based on `DataType`:
- `String` → Count
- `Number` → Count, Min, Max, Sum, Avg
- `Boolean` → Count, True count
- `Date` → Count, Earliest, Latest

Custom summaries: implement the `IgbSummaryOperand` interface in a C# class and assign it via `SummaryOperand` on the column. Check `get_doc` for the exact interface contract.

---

## Group By

```razor
<IgbGrid AllowGrouping="true">
    <IgbColumn Field="Country" Sortable="true" GroupByOrder="SortingDirection.Asc" />
    <IgbColumn Field="Revenue" DataType="GridColumnDataType.Number" />
</IgbGrid>
```

Programmatic group-by:

```csharp
Grid.GroupByAsync(new IgbGroupByRecord[]
{
    new IgbGroupByRecord { FieldName = "Country", Dir = SortingDirection.Asc }
});
Grid.ClearGroupByAsync();
```

---

## Selection

```razor
<IgbGrid RowSelection="GridSelectionMode.Multiple"
         CellSelection="GridSelectionMode.Multiple">
    <IgbColumn Field="ID" />
</IgbGrid>
```

Row selection values: `GridSelectionMode.None` / `Single` / `Multiple`.

Programmatic selection:

```csharp
// Row selection by primary key
Grid.SelectRowsAsync(new object[] { 1, 2, 3 }, clearOthers: true);
Grid.DeselectAllRowsAsync();

// Get selected rows
var selected = await Grid.GetSelectedRowsAsync();

// Cell selection
Grid.SelectCellsAsync(new IgbGridCellRange[] {
    new IgbGridCellRange { RowStart = 0, RowEnd = 2, ColumnStart = 0, ColumnEnd = 1 }
});
```

Events: `RowSelectionChanging` (cancellable), `CellSelectionChanging` (cancellable).

---

## Row Adding

Use row adding when the user needs an inline add-new-row workflow. Always set `PrimaryKey` for editing workflows and confirm the current add-row API in `grid-row-adding`.

```razor
<IgbGrid Data="Data" PrimaryKey="ProductID" RowEditable="true">
    <IgbActionStrip>
        <IgbGridEditingActions AddRow="true" />
    </IgbActionStrip>
    <IgbColumn Field="ProductID" />
    <IgbColumn Field="ProductName" Editable="true" />
</IgbGrid>
```

---

## Row Actions

Use row actions for per-row editing, delete, pinning, and custom action buttons shown in an action strip.

```razor
<IgbGrid Data="Data" PrimaryKey="ProductID" RowEditable="true">
    <IgbActionStrip>
        <IgbGridEditingActions />
        <IgbGridPinningActions />
    </IgbActionStrip>
</IgbGrid>
```

Confirm the available built-in action components and custom action templates with `grid-row-actions`.

---

## Search

Use grid search for text search across visible/searchable columns.

```razor
<IgbGrid @ref="Grid" Data="Data">
    <IgbColumn Field="ProductName" Searchable="true" />
    <IgbColumn Field="Category" Searchable="true" />
</IgbGrid>

@code {
    public IgbGrid Grid { get; set; }

    async Task FindNext(string term)
    {
        await Grid.FindNextAsync(term);
    }
}
```

Always verify method names such as `FindNextAsync`, `FindPrevAsync`, and `ClearSearchAsync` with `grid-search`.

---

## Keyboard Navigation

Keyboard navigation is a grid behavior, but custom keyboard handling must follow the Blazor grid docs. Use `grid-keyboard-navigation` for supported key combinations, active node behavior, and event names before writing custom handlers.

---

## Toolbar

```razor
<IgbGrid Data="Data" AllowFiltering="true">
    <IgbGridToolbar>
        <IgbGridToolbarTitle>Product Catalog</IgbGridToolbarTitle>
        <IgbGridToolbarSeparator />
        <IgbGridToolbarPinning />
        <IgbGridToolbarHiding />
        <IgbGridToolbarExporter ExportCSV="true" ExportExcel="true" />
    </IgbGridToolbar>
    <IgbColumn Field="Name" Sortable="true" />
</IgbGrid>
```

All toolbar child components are optional. Add only the features you need:

| Component | Feature |
|---|---|
| `IgbGridToolbarTitle` | Title label |
| `IgbGridToolbarSeparator` | Visual separator |
| `IgbGridToolbarPinning` | Column pin/unpin panel |
| `IgbGridToolbarHiding` | Column chooser (show/hide) |
| `IgbGridToolbarExporter` | Export to CSV / Excel |
| `IgbGridToolbarAdvancedFiltering` | Advanced filter dialog |

---

## Excel & CSV Export

```csharp
// Register export services in Program.cs
builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule), typeof(IgbExcelExporterModule), typeof(IgbCsvExporterModule));
// Also inject IgbExcelExporterService and IgbCsvExporterService
builder.Services.AddScoped<IgbExcelExporterService>();
builder.Services.AddScoped<IgbCsvExporterService>();
```

```razor
@inject IgbExcelExporterService ExcelExporter
@inject IgbCsvExporterService CsvExporter

<IgbGrid @ref="Grid" Data="Data">...</IgbGrid>
<IgbButton @onclick="ExportExcel">Export Excel</IgbButton>
<IgbButton @onclick="ExportCsv">Export CSV</IgbButton>

@code {
    IgbGrid Grid { get; set; }

    async Task ExportExcel()
    {
        var options = new IgbExcelExporterOptions { FileName = "products" };
        await ExcelExporter.ExportAsync(Grid, options);
    }

    async Task ExportCsv()
    {
        var options = new IgbCsvExporterOptions { FileName = "products" };
        await CsvExporter.ExportAsync(Grid, options);
    }
}
```

---

## Key Rules

1. **Always call `get_doc` before writing feature code.** Method signatures and enums are version-specific.
2. **`HasSummary` must be paired with the correct `DataType`** on the column - summaries use the data type to select built-in operands.
3. **`AllowGrouping` on the grid, plus `Sortable="true"` on columns** is required for group-by to work properly.
4. **Export services must be registered in `Program.cs`** as scoped services in addition to the modules.
5. **`RowSelection` and `CellSelection` are independent settings** - you can enable row selection without cell selection and vice versa.
6. **`PrimaryKey` must be set for programmatic row selection by key.** Without a primary key, row index is used instead.
7. **Advanced filtering, row adding, row actions, search, resizing, and keyboard navigation each have their own Blazor docs.** Do not route them through generic feature text without calling the matching slug.
