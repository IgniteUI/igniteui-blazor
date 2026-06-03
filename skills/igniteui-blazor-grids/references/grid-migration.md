# Grid Migration - Grid Lite → Premium IgbGrid

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**
> For `IgbGrid` setup and column configuration — see [`structure.md`](./structure.md).
> For specialized grid types including `IgbGridLite` — see [`types.md`](./types.md).
> For cell and row editing after migration — see [`editing.md`](./editing.md).

## Contents

- [When to Migrate from Grid Lite to IgbGrid](#when-to-migrate-from-grid-lite-to-igbgrid)
- [Setup](#setup)
- [Minimal Migration Example](#minimal-migration-example)
- [Component and API Changes](#component-and-api-changes)
- [Cell Templates](#cell-templates)
- [Header Templates](#header-templates)
- [Remote Data](#remote-data)
- [Programmatic Sort / Filter](#programmatic-sort--filter)
- [Common Enterprise Features](#common-enterprise-features)
- [Cleanup After Migration](#cleanup-after-migration)
- [Key Rules](#key-rules)

---

## When to Migrate from Grid Lite to IgbGrid

Migrate when you need any of the following features (not available in `IgbGridLite`):

| Feature | Grid Lite | Premium Grid (`IgbGrid`) |
|---|---|---|
| Cell editing | ✗ | ✓ `Editable` on column, `RowEditable` on grid |
| Batch editing (with undo) | ✗ | ✗ (not supported in Blazor) |
| Row adding / deleting | ✗ | ✓ `RowEditable` + `IgbActionStrip` |
| Row selection | ✗ | ✓ `RowSelection="GridSelectionMode.Single|Multiple"` |
| Cell selection | ✗ | ✓ `CellSelection` |
| Column selection | ✗ | ✓ `ColumnSelection` |
| Paging | ✗ | ✓ `IgbPaginator` child |
| GroupBy | ✗ | ✓ `GroupingExpressions` (IgbGrid only) |
| Column summaries | ✗ | ✓ `HasSummary` on `IgbColumn` |
| Column pinning | ✗ | ✓ `Pinned` on `IgbColumn` |
| Column moving | ✗ | ✓ `Moving="true"` on grid |
| Master-detail rows | ✗ | ✓ `IgbGrid` row expansion |
| Excel / CSV export (toolbar) | ✗ | ✓ `IgbGridToolbarExporter` |
| Column hiding toolbar | ✗ | ✓ `IgbGridToolbarHiding` |
| Column pinning toolbar | ✗ | ✓ `IgbGridToolbarPinning` |
| Advanced filtering UI | ✗ | ✓ `IgbGridToolbarAdvancedFiltering` |
| State persistence | ✗ | ✓ `IgbGridState` |
| Clipboard operations | ✗ | ✓ `ClipboardOptions` |
| Action strip | ✗ | ✓ `IgbActionStrip` |
| Row drag and drop | ✗ | ✓ `RowDraggable="true"` |

---

## Setup

### 1. Replace the NuGet package registration

```csharp
// Remove:
builder.Services.AddIgniteUIBlazor(typeof(IgbGridLiteModule));

// Add:
builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule));
```

### 2. Replace the CSS link in `index.html`

```html
<!-- Remove: -->
<link href="_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css" rel="stylesheet" />

<!-- Add: -->
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
```

### 3. Update `_Imports.razor`

`IgniteUI.Blazor.Controls` covers both `IgbGridLite` and `IgbGrid` — no change needed if already present.

```razor
@using IgniteUI.Blazor.Controls
```

---

## Minimal Migration Example

```razor
<!-- Before: -->
<IgbGridLite TItem="Product" Data="@products">
    <IgbGridLiteColumn Field="Name" Header="Name" DataType="GridLiteColumnDataType.String" Sortable Filterable Resizable />
    <IgbGridLiteColumn Field="Price" Header="Price" DataType="GridLiteColumnDataType.Number" />
</IgbGridLite>

<!-- After: -->
<IgbGrid @ref="grid" Data="@products" PrimaryKey="Id" AutoGenerate="false"
         Width="100%" Height="600px" AllowFiltering="true">
    <IgbColumn Field="Name" Header="Name" DataType="GridColumnDataType.String" Sortable="true" Filterable="true" Resizable="true" />
    <IgbColumn Field="Price" Header="Price" DataType="GridColumnDataType.Number" Sortable="true" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private List<Product> products = new();
}
```

**Key additions vs Grid Lite:**
- `PrimaryKey` — required for editing, selection, and row-targeted APIs
- `Height` — required for row virtualization
- `AllowFiltering="true"` on the grid — enables the filter row UI; `Filterable="true"` on a column opts that column in
- `@ref` — required for programmatic API access

---

## Component and API Changes

| Grid Lite | Premium Grid |
|---|---|
| `IgbGridLite` | `IgbGrid` |
| `IgbGridLiteColumn` | `IgbColumn` |
| `GridLiteColumnDataType` | `GridColumnDataType` |
| `IgbGridLiteModule` | `IgbGridModule` |
| `IgbGridLiteSortingExpression` | `IgbSortingExpression` |
| `IgbGridLiteFilterExpression` | `IgbFilteringExpression` |
| Column `Key` (in sort/filter objects) | Column `FieldName` (in sort/filter objects) |
| `TItem` generic parameter | `TItem` (unchanged) |
| No `PrimaryKey` | `PrimaryKey` required for most features |

---

## Cell Templates

Grid Lite has no cell templates. In `IgbGrid`, use the `BodyTemplate` render fragment on `IgbColumn`:

```razor
<IgbColumn Field="Status" Header="Status">
    <BodyTemplate>
        @{
            var cell = (IgbCellTemplateContext)context;
            var status = cell.Cell.Value?.ToString();
        }
        <span style="color: @(status == "Active" ? "green" : "red")">@status</span>
    </BodyTemplate>
</IgbColumn>
```

| | Grid Lite | Premium Grid |
|---|---|---|
| Cell template | Not supported | `BodyTemplate` render fragment |
| Cell value | — | `((IgbCellTemplateContext)context).Cell.Value` |
| Row data | — | `((IgbCellTemplateContext)context).Cell.Row.Data` |
| Edit template | — | `InlineEditorTemplate` render fragment |

---

## Header Templates

Grid Lite has no header templates. In `IgbGrid`, use the `HeaderTemplate` render fragment:

```razor
<IgbColumn Field="Price" Header="Price">
    <HeaderTemplate>
        <strong>@((context as IgbColumnTemplateContext)?.Column.Header)</strong>
    </HeaderTemplate>
</IgbColumn>
```

---

## Remote Data

For server-side sort/filter, handle the `SortingDone` / `FilteringDone` events to reload data:
```razor
<IgbGrid @ref="grid" Data="@data" PrimaryKey="Id" Height="600px"
         SortingDoneScript="onSortingDone" FilteringDoneScript="onFilteringDone">
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private List<MyItem> data = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        // Disable local processing so the grid does not sort/filter data in memory
        await grid.SetSortStrategyAsync(new IgbNoopSortingStrategy());
        await grid.SetFilterStrategyAsync(new IgbNoopFilteringStrategy());
    }

    private async Task OnSortingDone()
    {
        data = await DataService.SortAsync(grid.SortingExpressions);
        StateHasChanged();
    }

    private async Task OnFilteringDone()
    {
        data = await DataService.FilterAsync(grid.FilteringExpressionsTree);
        StateHasChanged();
    }
}
```

---

## Programmatic Sort / Filter

```razor
@code {
    private IgbGrid grid = default!;

    // Sort
    private async Task SortByName()
    {
        await grid.SortAsync(new IgbSortingExpression[]
        {
            new IgbSortingExpression { FieldName = "Name", Dir = SortingDirection.Asc }
        });
    }

    private async Task ClearSorting() => await grid.ClearSortAsync();

    // Filter
    private void FilterActive()
    {
        var tree = new IgbFilteringExpressionsTree() { Operator = FilteringLogic.And };
        tree.FilteringOperands = new IgbFilteringExpression[]
        {
            new IgbFilteringExpression { FieldName = "IsActive", ConditionName = "true", SearchVal = true }
        };
        grid.FilteringExpressionsTree = tree;
    }

    private async Task ClearFilters() => await grid.ClearFilterAsync();
}
```

> In Grid Lite, sort/filter expressions used a `Key` property. In `IgbGrid`, use `FieldName`.

---

## Common Enterprise Features

### Editing

```razor
<IgbGrid Data="@data" PrimaryKey="Id" RowEditable="true" Height="600px">
    <IgbColumn Field="Name" Editable="true" />
    <IgbColumn Field="Price" Editable="true" DataType="GridColumnDataType.Number" />
</IgbGrid>
```

See [`editing.md`](./editing.md) for cell editing, row editing, validation, and custom editors.

### Row Selection

```razor
<IgbGrid Data="@data" PrimaryKey="Id" RowSelection="GridSelectionMode.Multiple" Height="600px">
    <IgbColumn Field="Name" />
</IgbGrid>

@code {
    // Read selected rows via grid.SelectedRows
}
```

### Paging

```razor
<IgbGrid Data="@data" PrimaryKey="Id" Height="600px">
    <IgbPaginator PerPage="15" />
    <IgbColumn Field="Name" />
</IgbGrid>
```

See [`paging-remote.md`](./paging-remote.md) for remote paging and paginator configuration.

### Summaries

```razor
<IgbColumn Field="Price" DataType="GridColumnDataType.Number" HasSummary="true" />
```

### Toolbar + Export

```razor
<IgbGrid Data="@data" PrimaryKey="Id" Height="600px">
    <IgbGridToolbar>
        <IgbGridToolbarTitle>Products</IgbGridToolbarTitle>
        <IgbGridToolbarActions>
            <IgbGridToolbarHiding />
            <IgbGridToolbarPinning />
            <IgbGridToolbarAdvancedFiltering />
            <IgbGridToolbarExporter ExportExcel="true" ExportCSV="true" />
        </IgbGridToolbarActions>
    </IgbGridToolbar>
    <IgbColumn Field="Name" />
    <IgbColumn Field="Price" />
</IgbGrid>
```

See [`features.md`](./features.md) for toolbar customization, export events, grouping, summaries, and action strip.

---

## Cleanup After Migration

1. Remove `IgbGridLiteModule` registration from `Program.cs`.
2. Remove the `IgniteUI.Blazor.GridLite` CSS `<link>` from `index.html`.
3. Rename all `IgbGridLite` → `IgbGrid`, `IgbGridLiteColumn` → `IgbColumn` in `.razor` files.
4. Replace `GridLiteColumnDataType` → `GridColumnDataType` enum values.
5. In sort/filter expression objects, rename the `Key` property → `FieldName`.
6. Remove the `IgniteUI.Blazor.GridLite` NuGet package if no `IgbGridLite` instances remain:
   ```bash
   dotnet remove package IgniteUI.Blazor.GridLite
   ```

---

## Key Rules

- `PrimaryKey` is required for editing, selection, row APIs, and `IgbActionStrip` — Grid Lite had no equivalent.
- `Height` (or a CSS-constrained container) is required for row virtualization in `IgbGrid`.
- `AllowFiltering="true"` must be set on the grid to show the filter row; `Filterable="true"` on a column opts that column in.
- Cell and header templates use Blazor render fragments (`BodyTemplate`, `HeaderTemplate`, `InlineEditorTemplate`) — not callbacks or delegates.
- For remote data, disable local strategies with `IgbNoopSortingStrategy` / `IgbNoopFilteringStrategy` before binding events.
- `IgbColumn.FieldName` (in expression objects) replaces `IgbGridLiteColumn.Key` used in Grid Lite sort/filter expressions.

