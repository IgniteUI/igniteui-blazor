# Migrating `IgbGridLite` → `IgbGrid`

Grid Lite is read-only by design. When the app outgrows it, the upgrade target is always `IgbGrid` — never a different component.

## What you gain

Everything below is unavailable in Grid Lite and available in `IgbGrid`: cell and row editing (`Editable`, `RowEditable`), row adding/deleting, row/cell/column selection, paging (`IgbPaginator`), grouping, summaries (`HasSummary`), column pinning and moving, master-detail rows, toolbar with column hiding/pinning/advanced filtering, Excel and CSV export, state persistence (`IgbGridState`), clipboard options, action strip, and row drag-and-drop.

Batch editing with undo is not supported in Blazor on either grid.

## Setup changes

```csharp
// Program.cs
- builder.Services.AddIgniteUIBlazor(typeof(IgbGridLiteModule));
+ builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule));
```

```html
<!-- index.html / App.razor — Grid Lite ships one stylesheet, IgbGrid needs two -->
- <link href="_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css" rel="stylesheet" />
+ <link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
+ <link href="_content/IgniteUI.Blazor/themes/grid/light/bootstrap.css" rel="stylesheet" />
```

`_Imports.razor` is unchanged — `IgniteUI.Blazor.Controls` covers both.

## Markup changes

```razor
@* Before *@
<IgbGridLite TItem="Product" Data="@products">
    <IgbGridLiteColumn Field="Name" Header="Name" DataType="GridLiteColumnDataType.String" Sortable Filterable Resizable />
    <IgbGridLiteColumn Field="Price" Header="Price" DataType="GridLiteColumnDataType.Number" />
</IgbGridLite>

@* After *@
<IgbGrid @ref="grid" Data="@products" PrimaryKey="Id" AutoGenerate="false"
         Width="100%" Height="600px" AllowFiltering="true">
    <IgbColumn Field="Name" Header="Name" DataType="GridColumnDataType.String"
               Sortable="true" Filterable="true" Resizable="true" />
    <IgbColumn Field="Price" Header="Price" DataType="GridColumnDataType.Number" Sortable="true" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;
}
```

Four things must be added that Grid Lite had no equivalent for:

- **`PrimaryKey`** — required for editing, selection, row-targeted APIs, and `IgbActionStrip`.
- **`Height`** — required for row virtualization.
- **`AllowFiltering="true"` on the grid** — Grid Lite needed only `Filterable` per column; `IgbGrid` needs the grid-level switch too.
- **`@ref`** — for any programmatic API call.

Also note the boolean attributes: Grid Lite accepts bare `Sortable`, `IgbColumn` wants `Sortable="true"`.

## Rename table

| Grid Lite | `IgbGrid` |
|---|---|
| `IgbGridLite` | `IgbGrid` |
| `IgbGridLiteColumn` | `IgbColumn` |
| `IgbGridLiteModule` | `IgbGridModule` |
| `GridLiteColumnDataType` | `GridColumnDataType` |
| `IgbGridLiteSortingExpression` | `IgbSortingExpression` |
| `IgbGridLiteFilterExpression` | `IgbFilteringExpression` |
| `IgbGridLiteSortingOptions` | `IgbSortingOptions` |
| `Key` in sort/filter expressions | `FieldName` |

`TItem` stays as-is.

## Templates

Grid Lite has no templates at all. `IgbColumn` adds Blazor render fragments — not callbacks or delegates:

```razor
<IgbColumn Field="Status" Header="Status">
    <BodyTemplate>
        @{
            var cell = (IgbCellTemplateContext)context;
            var status = cell.Cell.Value?.ToString();
        }
        <span style="color: @(status == "Active" ? "green" : "red")">@status</span>
    </BodyTemplate>
    <HeaderTemplate>
        <strong>@((context as IgbColumnTemplateContext)?.Column.Header)</strong>
    </HeaderTemplate>
</IgbColumn>
```

Row data inside a body template is `cell.Cell.Row.Data`. Editors use `InlineEditorTemplate` bound to `cell.Cell.EditValue`. Full detail in [`structure.md`](./structure.md) and [`editing.md`](./editing.md).

## Remote data

Grid Lite drives server-side work through `DataPipelineConfiguration`. `IgbGrid` uses events instead — handle `SortingDone` / `FilteringDone` / `PageChange` and reload `Data` yourself:

```razor
<IgbGrid @ref="grid" Data="@data" PrimaryKey="Id" Height="600px"
         SortingDone="OnSortingDone" FilteringDone="OnFilteringDone" />

@code {
    private async Task OnSortingDone(IgbSortingExpressionEventArgs args)
    {
        data = await DataService.SortAsync(args.Detail);
        StateHasChanged();
    }
}
```

See [`paging-remote.md`](./paging-remote.md) for the combined paging + sorting + filtering pattern.

## Enabling the new features

```razor
<IgbGrid Data="@data" PrimaryKey="Id" Height="600px"
         RowEditable="true" RowSelection="GridSelectionMode.Multiple">
    <IgbGridToolbar>
        <IgbGridToolbarTitle>Products</IgbGridToolbarTitle>
        <IgbGridToolbarActions>
            <IgbGridToolbarHiding />
            <IgbGridToolbarPinning />
            <IgbGridToolbarAdvancedFiltering />
            <IgbGridToolbarExporter ExportExcel="true" ExportCSV="true" />
        </IgbGridToolbarActions>
    </IgbGridToolbar>
    <IgbColumn Field="Name" Editable="true" />
    <IgbColumn Field="Price" Editable="true" DataType="GridColumnDataType.Number" HasSummary="true" />
    <IgbPaginator PerPage="15" />
</IgbGrid>
```

Details live in [`editing.md`](./editing.md), [`features.md`](./features.md), and [`state.md`](./state.md).

## Cleanup

1. Remove `IgbGridLiteModule` from `Program.cs`.
2. Remove the Grid Lite `<link>` from the host page.
3. Rename components, enum types, and expression `Key` → `FieldName` across `.razor` files.
4. Once no `IgbGridLite` remains: `dotnet remove package IgniteUI.Blazor.GridLite`.
