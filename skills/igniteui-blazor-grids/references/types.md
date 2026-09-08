# Grid Types — Grid Lite, Tree Grid, Hierarchical Grid, Pivot Grid

`IgbGrid` setup and columns are in [`structure.md`](./structure.md); everything here is what differs.

## Grid Lite (`IgbGridLite`)

Lightweight read-only grid with sorting, filtering, column resizing/hiding, and virtualization. MIT-licensed, in its own `IgniteUI.Blazor.GridLite` package — **not** bundled with `IgniteUI.Blazor`. No editing, selection, or paging; when those are needed, migrate to `IgbGrid` ([`grid-migration.md`](./grid-migration.md)).

```csharp
// dotnet add package IgniteUI.Blazor.GridLite
builder.Services.AddIgniteUIBlazor(typeof(IgbGridLiteModule));
```

```html
<link href="_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css" rel="stylesheet" />
```

```razor
<IgbGridLite TItem="Employee" Data="@employees" SortingOptions="@sortingOptions"
             Sorting="@HandleSorting" Sorted="@HandleSorted"
             Filtering="@HandleFiltering" Filtered="@HandleFiltered">
    <IgbGridLiteColumn Field="@nameof(Employee.Name)" Header="Name"
                       DataType="GridLiteColumnDataType.String" Sortable Filterable Resizable />
    <IgbGridLiteColumn Field="@nameof(Employee.Salary)" Header="Salary"
                       DataType="GridLiteColumnDataType.Number" Sortable />
    @if (showHireDate)
    {
        <IgbGridLiteColumn Field="@nameof(Employee.HireDate)" Header="Hire Date"
                           DataType="GridLiteColumnDataType.Date" Sortable />
    }
</IgbGridLite>

@code {
    private IgbGridLiteSortingOptions sortingOptions = new()
    {
        Mode = GridLiteSortingMode.Multiple   // default Single
    };
    private bool showHireDate = true;
}
```

Column parameters: `Field`, `Header`, `DataType` (`String | Number | Boolean | Date`), `Width`, `Sortable`, `SortingCaseSensitive`, `Filterable`, `FilteringCaseSensitive`, `Resizable`, `Hidden`.

Grid Lite specifics that trip people up:

- Columns are **declarative children** (`IgbGridLiteColumn`). The old `Columns` parameter taking `List<IgbColumnConfiguration>` is gone.
- Renames from the old API: `Key` → `Field`, `Type` → `DataType`, `HeaderText` → `Header`, `Sort` → `Sortable`, `Filter` → `Filterable`, `SortConfiguration` → `SortingOptions`, `IgbGridLiteSortConfiguration` → `IgbGridLiteSortingOptions`, `Multiple = true/false` → `Mode = GridLiteSortingMode.Multiple/Single`.
- **`Field` on columns, `Key` in expressions.** `IgbGridLiteSortingExpression` and `IgbGridLiteFilterExpression` identify the field with `Key`.
- Change the column set with `@if` — there is no `UpdateColumnsAsync()`.
- Tri-state sorting (asc → desc → none) is always on and cannot be disabled.
- Remote operations go through `DataPipelineConfiguration`, not events + noop strategies.

## Tree Grid (`IgbTreeGrid`)

Self-referencing parent-child data in a single schema. Register `IgbTreeGridModule`.

Choose **one** shape — never both:

```razor
@* A: flat data with a parent reference *@
<IgbTreeGrid Data="employees" PrimaryKey="Id" ForeignKey="ManagerId"
             AutoGenerate="false" Width="100%" Height="600px">
    <IgbColumn Field="Name" Header="Employee" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Title" Header="Title" DataType="GridColumnDataType.String" />
</IgbTreeGrid>

@* B: nested child collections *@
<IgbTreeGrid Data="fileSystem" PrimaryKey="Id" ChildDataKey="Children"
             ExpansionDepth="1" AutoGenerate="false" Width="100%" Height="500px">
    <IgbColumn Field="Name" Header="Name" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Size" Header="Size (KB)" DataType="GridColumnDataType.Number" />
</IgbTreeGrid>

@code {
    // A: root rows have null (or 0/default) in the foreign key
    record Employee(int Id, string Name, string Title, int? ManagerId, DateTime HireDate);

    // B: ChildDataKey names the collection property on each row
    class FileNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Size { get; set; }
        public List<FileNode> Children { get; set; } = new();
    }
}
```

| Parameter | Type | Purpose |
|---|---|---|
| `PrimaryKey` | `string` | Required |
| `ForeignKey` | `string` | Parent reference field — flat data |
| `ChildDataKey` | `string` | Child collection property — nested data |
| `HasChildrenKey` | `string` | Boolean field marking expandable rows, for load-on-demand |
| `ExpansionDepth` | `double` | Levels expanded initially; `0` collapses all |
| `CascadeOnDelete` | `bool` | Deleting a parent deletes its descendants |

`RowSelection="GridSelectionMode.MultipleCascade"` selects all descendants with the parent. Filtering is recursive — matching rows are kept along with their ancestors, so the tree stays navigable.

**Load on demand is JS-interop only.** There is no C# callback: set `HasChildrenKey` and register a handler through `LoadChildrenOnDemandScript` to supply children when a row expands.

## Hierarchical Grid (`IgbHierarchicalGrid`)

Multi-schema parent-child, where each level has its own columns. Register `IgbHierarchicalGridModule`.

```razor
<IgbHierarchicalGrid Data="customers" PrimaryKey="CustomerId" AutoGenerate="false"
                     Width="100%" Height="600px"
                     RowSelection="GridSelectionMode.Multiple" AllowFiltering="true">
    <IgbColumn Field="CompanyName" Header="Company" Sortable="true" Filterable="true" />

    <IgbRowIsland ChildDataKey="Orders" PrimaryKey="OrderId" AutoGenerate="false"
                  RowSelection="GridSelectionMode.Single"
                  AllowFiltering="true" FilterMode="FilterMode.ExcelStyleFilter"
                  GridCreated="OnChildGridCreated">
        <IgbColumn Field="OrderDate" Header="Date" DataType="GridColumnDataType.Date" />
        <IgbColumn Field="Total" HasSummary="true" DataType="GridColumnDataType.Currency" />

        <IgbRowIsland ChildDataKey="OrderDetails" PrimaryKey="DetailId" AutoGenerate="false">
            <IgbColumn Field="ProductName" Header="Product" />
            <IgbColumn Field="Quantity" Header="Qty" DataType="GridColumnDataType.Number" />
        </IgbRowIsland>
    </IgbRowIsland>
</IgbHierarchicalGrid>

@code {
    private void OnChildGridCreated(IgbGridCreatedEventArgs args)
    {
        var childGrid = args.Detail.Grid;   // the created child grid instance
        var rowIsland = args.Detail.Owner;  // the IgbRowIsland template
    }
}
```

- `IgbRowIsland.ChildDataKey` must match the parent class's collection property name **exactly**, case included.
- A row island is a **template**: it configures every child grid created at that level. Islands nest to any depth.
- Each child grid instance keeps its own sorting, filtering, selection, and paging state, and none of it is affected by the root grid's state.
- Child grids do not exist until their row is expanded — reach them through `GridCreated` / `GridInitialized`, never by assumption.
- All standard grid features are available on `IgbRowIsland`.

## Pivot Grid (`IgbPivotGrid`)

Pivot-table analytics. Register `IgbPivotGridModule`. **Read-only** — no cell, row, or batch editing, and no row/cell/column selection or paging. Show the source data in a separate `IgbGrid` if it must be edited.

```razor
<div style="display: flex; gap: 16px;">
    <IgbPivotDataSelector Grid="pivotGrid" />
    <IgbPivotGrid @ref="pivotGrid" Data="salesData" PivotConfiguration="pivotConfig"
                  Width="100%" Height="600px" />
</div>

@code {
    private IgbPivotGrid pivotGrid = default!;
    private IgbPivotConfiguration pivotConfig = default!;

    protected override void OnInitialized()
    {
        pivotConfig = new IgbPivotConfiguration
        {
            Rows = new IgbPivotDimension[]
            {
                new() { MemberName = "Country", Enabled = true },
                new() { MemberName = "City", Enabled = true }
            },
            Columns = new IgbPivotDimension[]
            {
                new() { MemberName = "Year", Enabled = true }
            },
            Values = new IgbPivotValue[]
            {
                new()
                {
                    Member = "Revenue",
                    Aggregate = new IgbPivotAggregator
                    {
                        AggregatorName = PivotAggregationType.SUM,
                        Key = "SUM",
                        Label = "Sum of Revenue"
                    },
                    Enabled = true
                }
            },
            Filters = new IgbPivotDimension[]
            {
                new() { MemberName = "ProductCategory", Enabled = true }
            }
        };
    }
}
```

`IgbPivotConfiguration` holds `Rows`, `Columns`, `Values`, `Filters`.
`IgbPivotDimension`: `MemberName`, `Enabled`, `DisplayName`, `ChildLevel` (nested hierarchy), `SortDirection`.
`IgbPivotValue`: `Member`, `Aggregate`, `Enabled`, `DisplayName`, `FormatterScript` (a JS function registered via `igRegisterScript`).
`PivotAggregationType`: `SUM`, `COUNT`, `MIN`, `MAX`, `AVG`, `EARLIEST`, `LATEST`.

Sorting and filtering are dimension-based and configured through `IgbPivotConfiguration` — not through `SortAsync` / `FilterAsync`. `IgbPivotDataSelector` adds the drag-and-drop panel for reshaping at runtime.
