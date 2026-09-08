# Features — Grouping, Summaries, Toolbar, Export, Row Drag, Action Strip, Master-Detail

Setup and columns are in [`structure.md`](./structure.md); editing in [`editing.md`](./editing.md).

## Grouping — `IgbGrid` only

Tree Grid and Hierarchical Grid have no grouping; the Pivot Grid uses dimensions instead.

```razor
<IgbGrid Data="data" PrimaryKey="Id"
         GroupingExpressions="groupingExpressions"
         HideGroupedColumns="true"
         ShowGroupArea="true"
         GroupingDone="OnGroupingDone">
    <IgbColumn Field="Department" Header="Department" Groupable="true" />
    <IgbColumn Field="Name" Header="Name" />

    <GroupRowTemplate>
        @{ var groupRow = (IgbGroupByRowTemplateContext)context; }
        <span>
            <strong>@groupRow.Implicit.Expression.FieldName</strong>: @groupRow.Implicit.Value
            (@groupRow.Implicit.Records.Length items)
        </span>
    </GroupRowTemplate>
</IgbGrid>

@code {
    private IgbGroupingExpression[] groupingExpressions =
    {
        new() { FieldName = "Department", Dir = SortingDirection.Asc }
    };

    private void OnGroupingDone(IgbGroupingDoneEventArgs args) { }
}
```

Users group by dragging a header into the group area or via the column menu. `HideGroupedColumns` removes the grouped column from the body; `ShowGroupArea="false"` hides the drop zone.

## Summaries

`HasSummary="true"` on a column enables the built-ins for its data type: String/Boolean → Count; Number → Count, Min, Max, Sum, Avg; Date → Count, Earliest, Latest.

```razor
<IgbGrid Data="data" PrimaryKey="Id"
         SummaryPosition="GridSummaryPosition.Bottom"
         SummaryCalculationMode="GridSummaryCalculationMode.RootAndChildLevels">
    <IgbColumn Field="Salary" HasSummary="true" DataType="GridColumnDataType.Number" />
</IgbGrid>
```

`GridSummaryPosition`: `Top`, `Bottom` (default). `GridSummaryCalculationMode`: `RootLevelOnly`, `ChildLevelsOnly`, `RootAndChildLevels` — set it deliberately on Tree and Hierarchical grids.

Custom summaries go through JavaScript: register a class exposing `operate(data, allData, fieldName)` returning `{ key, label, summaryResult }` objects and attach it with `ColumnInitScript`. Read `get_doc(framework: "blazor", name: "grid-summaries")` for the exact shape.

## Cell merging — `IgbGrid` only

```razor
<IgbColumn Field="Country" Merge="true" />
<IgbColumn Field="City" Merge="true" />
```

Merges visually identical adjacent cells. Sort by the merged columns first, or the merges will be scattered.

## Toolbar

```razor
<IgbGrid Data="data" PrimaryKey="Id">
    <IgbGridToolbar>
        <IgbGridToolbarTitle>Employees</IgbGridToolbarTitle>
        <IgbGridToolbarActions>
            <IgbGridToolbarHiding />
            <IgbGridToolbarPinning />
            <IgbGridToolbarAdvancedFiltering />
            <IgbGridToolbarExporter ExportExcel="true" ExportCSV="true" Filename="employees" />
            <IgbButton @onclick="RefreshData">Refresh</IgbButton>
        </IgbGridToolbarActions>
    </IgbGridToolbar>
    …
</IgbGrid>
```

`IgbGridToolbarActions` accepts arbitrary content alongside the built-in action components.

## Export

```razor
<IgbGridToolbarExporter @ref="exporter" ExportExcel="true" ExportCSV="true" Filename="employees" />

@code {
    private IgbGridToolbarExporter exporter = default!;

    private Task ExportToExcel() => exporter.ExportGridAsync(GridToolbarExporterType.Excel);
    private Task ExportToCsv()   => exporter.ExportGridAsync(GridToolbarExporterType.CSV);
}
```

Configure or cancel an export from the grid's `ToolbarExporting` event. In Blazor the practical hook is the JS variant, because the exporter's own per-column and per-row events are only reachable from script:

```razor
<IgbGrid Data="data" PrimaryKey="Id" ToolbarExportingScript="OnToolbarExporting">…</IgbGrid>
```

```javascript
igRegisterScript("OnToolbarExporting", (evt) => {
    const args = evt.detail;
    args.options.fileName = `Report_${new Date().toDateString()}`;
    // args.cancel = true;
    args.exporter.columnExporting.subscribe((colArgs) => {
        if (colArgs.header === "ID") colArgs.cancel = true;
    });
}, false);
```

Options on the exporting args (`IgbExporterOptionsBase`): `FileName`, `IgnoreFiltering`, `IgnoreSorting`, `IgnoreColumnsVisibility`, `IgnoreGrouping`, `AlwaysExportHeaders`, `ExportSummaries`.

Events: `ExportStarted` / `ExportEnded` on `IgbGridToolbarExporter`; `ToolbarExporting` on the grid; `ColumnExporting` / `RowExporting` reachable through the exporter in the event args.

## Virtualization & performance

Row and column virtualization are on by default once the grid has a fixed `Height` — only the visible viewport is rendered. Column virtualization additionally kicks in when the total column width exceeds the grid width.

- Bind a materialized `List<T>` or `T[]`, never `IQueryable`.
- Past ~100k rows, move to remote paging or server-side loading ([`paging-remote.md`](./paging-remote.md)).
- Keep templates on frequently re-rendered columns cheap.
- Use on-demand summaries when summaries slow large data sets down.

## Row drag

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowDraggable="true"
         RowDragStart="OnRowDragStart" RowDragEnd="OnRowDragEnd">
    <DragGhostCustomTemplate>
        @{ var row = (IgbGridRowDragGhostContext)context; }
        <div class="custom-ghost">Moving: @row.Data</div>
    </DragGhostCustomTemplate>
    …
</IgbGrid>

@code {
    private void OnRowDragStart(IgbRowDragStartEventArgs args) { }
    private void OnRowDragEnd(IgbRowDragEndEventArgs args) => _ = args.DragData;
}
```

## Action strip

```razor
<IgbGrid Data="data" PrimaryKey="Id" RowEditable="true">
    <IgbColumn Field="Name" Editable="true" />
    <IgbActionStrip>
        <IgbGridEditingActions AddRow="true" />
        <IgbGridPinningActions />
    </IgbActionStrip>
</IgbGrid>
```

`IgbGridEditingActions` renders edit/delete (plus add-row when `AddRow="true"`) and needs `RowEditable="true"` on the grid to function. `IgbGridPinningActions` renders row pin/unpin. Extra buttons can be placed inside `IgbActionStrip` alongside them.

## Master-detail — `IgbGrid` only

```razor
<IgbGrid Data="customers" PrimaryKey="CustomerId" AutoGenerate="false">
    <IgbColumn Field="Name" Header="Customer Name" />
    <IgbColumn Field="Country" Header="Country" />
    <DetailTemplate>
        @{
            var ctx = (IgbGridMasterDetailContext)context;
            var customer = (Customer)ctx.Implicit;
        }
        <div style="padding: 16px;">
            <h4>Orders for @customer.Name</h4>
            <IgbGrid Data="customer.Orders" PrimaryKey="OrderId" AutoGenerate="true"
                     Width="100%" Height="200px" />
        </div>
    </DetailTemplate>
</IgbGrid>
```

`DetailTemplate` exists only on `IgbGrid`. For multi-schema hierarchies use `IgbHierarchicalGrid` with `IgbRowIsland` ([`types.md`](./types.md)).

## Clipboard

Copy works out of the box: `Ctrl+C` copies the selection, `Ctrl+Shift+H` copies it with headers.

```razor
<IgbGrid Data="data" PrimaryKey="Id" ClipboardOptions="clipboardOptions">…</IgbGrid>

@code {
    private IgbClipboardOptions clipboardOptions = new()
    {
        Enabled = true, CopyHeaders = true, CopyFormatters = true, Separator = "\t"
    };
}
```
