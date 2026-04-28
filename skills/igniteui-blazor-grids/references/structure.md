# Grid Structure & Column Configuration

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**
> For project setup and CSS — see [`igniteui-blazor-components/references/setup.md`](../../igniteui-blazor-components/references/setup.md).

## Contents

- [Module Registration](#module-registration)
- [Basic Grid Setup](#basic-grid-setup)
- [Column Definition](#column-definition)
- [Column Data Types](#column-data-types)
- [Column Templates](#column-templates)
- [Multi-Column Headers](#multi-column-headers)
- [Key Rules](#key-rules)

---

## Module Registration

```csharp
// Program.cs
builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule));
```

Other grids use their own modules:
- Tree Grid: `typeof(IgbTreeGridModule)`
- Hierarchical Grid: `typeof(IgbHierarchicalGridModule)`
- Pivot Grid: `typeof(IgbPivotGridModule)`
- Grid Lite: `typeof(IgbGridLiteModule)`

---

## Basic Grid Setup

```razor
<IgbGrid @ref="Grid"
         Data="Products"
         PrimaryKey="ProductID"
         AutoGenerate="false"
         Height="600px"
         Width="100%">
    <IgbColumn Field="ProductID" Header="ID" DataType="GridColumnDataType.Number" />
    <IgbColumn Field="ProductName" Header="Name" DataType="GridColumnDataType.String" Sortable="true" Resizable="true" />
    <IgbColumn Field="UnitPrice" Header="Price" DataType="GridColumnDataType.Number" />
    <IgbColumn Field="InStock" Header="In Stock" DataType="GridColumnDataType.Boolean" />
    <IgbColumn Field="OrderDate" Header="Order Date" DataType="GridColumnDataType.Date" />
</IgbGrid>

@code {
    public IgbGrid Grid { get; set; }
    public List<Product> Products { get; set; } = SampleData.GetProducts();
}
```

Key attributes on `IgbGrid`:

| Attribute | Type | Description |
|---|---|---|
| `Data` | `IEnumerable<T>` | The data source |
| `PrimaryKey` | `string` | Required for editing, selection, and row pinning |
| `AutoGenerate` | `bool` | When `true`, creates columns from object properties automatically |
| `Height` | `string` | Explicit height (CSS string); required for row virtualization |
| `Width` | `string` | Explicit width |
| `AllowFiltering` | `bool` | Enables the filter row |
| `FilterMode` | `FilterMode` | `FilterMode.QuickFilter` (row) / `ExcelStyleFilter` (dropdown) / `ExternalFilter` |
| `RowEditable` | `bool` | Enables row editing mode |
| `RowSelection` | `GridSelectionMode` | `GridSelectionMode.None` / `Single` / `Multiple` |
| `CellSelection` | `GridSelectionMode` | `GridSelectionMode.None` / `Single` / `Multiple` |
| `Class` | `string` | CSS class used for sizing/theming, including `--ig-size` density overrides |

---

## Column Definition

```razor
<IgbColumn Field="ProductName"
           Header="Product Name"
           DataType="GridColumnDataType.String"
           Sortable="true"
           Filterable="true"
           Editable="true"
           Resizable="true"
           Pinned="true"
           Width="200px"
           MinWidth="100px"
           MaxWidth="400px"
           HasSummary="true" />
```

Key `IgbColumn` attributes:

| Attribute | Type | Description |
|---|---|---|
| `Field` | `string` | Property name on the data object |
| `Header` | `string` | Column header label |
| `DataType` | `GridColumnDataType` | Type used for sorting, filtering, editing |
| `Sortable` | `bool` | Enables sorting on this column |
| `Filterable` | `bool` | Enables filtering on this column |
| `Editable` | `bool` | Allows cell editing in this column |
| `Resizable` | `bool` | Allows column resize by dragging |
| `Pinned` | `bool` | Pins the column to the left edge |
| `Hidden` | `bool` | Hides the column (toggleable at runtime) |
| `Width` | `string` | CSS width |
| `MinWidth` | `string` | Minimum column width during resize |
| `HasSummary` | `bool` | Enables summary row for this column |
| `GroupByOrder` | `SortingDirection` | Initial group-by direction |
| `MovingEnabled` | `bool` | Allows drag-to-reorder this column |

---

## Column Data Types

| `GridColumnDataType` | Use for |
|---|---|
| `String` | Text fields |
| `Number` | Integer and decimal fields |
| `Boolean` | True/false toggles |
| `Date` | `DateTime` values (date only) |
| `DateTime` | Full `DateTime` values |
| `Time` | `TimeSpan` values |
| `Currency` | Monetary values (uses locale formatting) |
| `Percent` | Percentage values (0.5 = 50%) |
| `Image` | URL string rendered as an image |

---

## Column Templates

Use `RenderFragment` child content inside `IgbColumn` to customize cell rendering.

```razor
<IgbColumn Field="Status" Header="Status">
    <IgbCellTemplateDirective>
        @{
            var status = (string)((context as IgbCellTemplateContext).Cell.Value);
        }
        <span class="status-badge status-@status.ToLower()">@status</span>
    </IgbCellTemplateDirective>
</IgbColumn>
```

> **AGENT INSTRUCTION:** Always call `get_doc` with slug `data-grid` or the grid-templates doc for the exact Razor template directive syntax for the current version.

Header template:

```razor
<IgbColumn Field="Total" Header="Total">
    <IgbHeaderTemplateDirective>
        <strong>Total ($)</strong>
    </IgbHeaderTemplateDirective>
</IgbColumn>
```

---

## Multi-Column Headers

Group related columns under a shared header:

```razor
<IgbGrid Data="Data" AutoGenerate="false">
    <IgbColumnGroup Header="Personal Info">
        <IgbColumn Field="FirstName" Header="First" />
        <IgbColumn Field="LastName" Header="Last" />
        <IgbColumn Field="Email" Header="Email" />
    </IgbColumnGroup>
    <IgbColumnGroup Header="Address">
        <IgbColumn Field="City" Header="City" />
        <IgbColumn Field="Country" Header="Country" />
    </IgbColumnGroup>
</IgbGrid>
```

---

## Key Rules

1. **Always call `get_doc` before writing any grid code.** Property names change between versions.
2. **Set `PrimaryKey`** whenever you use editing, selection, row pinning, or state persistence.
3. **Set an explicit `Height`** to enable row virtualization for large datasets.
4. **`AutoGenerate="false"` is recommended for production** — explicit columns prevent unexpected columns from appearing when the data shape changes.
5. **Each grid type (Tree, Hierarchical, Pivot) uses its own module** — `IgbGridModule` only covers the flat grid.
6. **Column templates use Blazor `RenderFragment` with MCP-provided directive types.** Confirm directive names from `get_doc` before writing template code.
