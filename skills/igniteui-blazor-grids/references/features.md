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

Row virtualization is always on: only the rows in the viewport are rendered, and scrolling reuses them. Column virtualization also kicks in when the total column width exceeds the grid width. Give the grid a `Height` so the viewport is what you expect ([`sizing.md`](./sizing.md)).

- Bind a materialized `List<T>` or `T[]`, never `IQueryable`.
- Past ~100k rows, move to remote paging or server-side loading ([`paging-remote.md`](./paging-remote.md)).
- Use on-demand summaries when summaries slow large data sets down.
- For grids that stream data, scroll a lot, or run on Blazor Server, use JavaScript cell templates and the live-data pattern below.

### Cell templates: render fragments vs. `BodyTemplateScript`

A Razor `BodyTemplate` costs a JS → .NET round trip **for every cell render**: the first render, every row scrolled into view, and every update that re-renders a cell. Each one carries 1–3 KB of serialized cell context. A `BodyTemplateScript` template ([`structure.md`](./structure.md#javascript-cell-templates-bodytemplatescript)) runs in the browser and costs nothing on the .NET side.

Measured on 26.2 (Release build; 10,000 rows, 6 templated columns, `Height="600px"`):

| | Razor `BodyTemplate` | `BodyTemplateScript` |
|---|---|---|
| WebAssembly: fill after a scrollbar jump | ~240 ms | ~7 ms |
| Blazor Server: traffic per scroll step | ~75 KB up, ~13 KB down | none |
| Blazor Server: first render | one 94–188 KB message to the server | none |

**Blazor Server and the hub message limit.** The first render sends every rendered templated cell's context in one message, 1–3 KB per cell. SignalR's default `MaximumReceiveMessageSize` is 32 KB, so a dozen or two templated cells can exceed it: 6 templated columns × 12 visible rows came to 94–188 KB. Past the limit the circuit drops, the cells stay empty, and the browser logs `Error: Connection disconnected with error 'Error: Server returned an error on close: Connection closed with an error.'` (#482). Switch the column to `BodyTemplateScript`, or raise the limit with room for about 3 KB per visible templated cell:

```csharp
// Program.cs (Blazor Web App)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options => options.MaximumReceiveMessageSize = 1024 * 1024);
```

A larger limit only lets the first render through. Every later re-render sends the context again for each re-rendered cell, so updates that touch many cells can exceed even 1 MB.

### Live data

How the grid sees a change decides what it costs:

| Update from C# | Result |
|---|---|
| Mutate your objects, then `StateHasChanged()` | **The grid does not update.** It holds its own copy of the data |
| Assign a new `List<T>` to `Data` on every tick | The whole list is serialized again (about 3 MB per tick for 10,000 rows on Server), and every rendered template re-runs. Fine for a few hundred rows on WebAssembly |
| `NotifyUpdateItem` / `NotifySetItem` / `UpdateCellAsync` per changed row | **Each** call re-renders every rendered cell. With Razor templates, 10 calls per tick blocked the page for 2.5 s (WebAssembly), and on Server each tick sent hundreds of KB |
| Patch the grid's own records in JavaScript (below) | Only the changed cells re-render. ~13 ms per 100-row tick on WebAssembly; ~0.5 KB up and ~6 KB down on Server |

The recommended pattern: assign `Data` once, then send only the changed fields to JavaScript. There, copy them onto the grid's own records and assign a new array with the same objects. The rows keep their identity, so a template re-runs only where its column's field changed, and sorting and filtering are re-applied. The same script gives the grid element its pixel height ([`sizing.md`](./sizing.md#live-data-give-the-grid-element-a-pixel-height)) and releases sparklines ([`structure.md`](./structure.md#javascript-cell-templates-bodytemplatescript)).

```razor
@* Pages/Live.razor. With per-page interactivity in a Blazor Web App, also add @rendermode InteractiveServer (or another interactive mode) *@
@page "/live"
@implements IAsyncDisposable
@inject IJSRuntime JS
@inject ILogger<Live> Logger

<div id="stocksHost" class="stocks-host">
    <IgbGrid Id="stocksGrid" Data="stocks" PrimaryKey="Id" AutoGenerate="false" Height="100%">
        <IgbColumn Field="Symbol" />
        <IgbColumn Field="Price" DataType="GridColumnDataType.Number" Sortable="true" BodyTemplateScript="LivePriceCell" />
    </IgbGrid>
</div>

@code {
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public double Price { get; set; }
    }

    public record PricePatch(int Id, double Price);

    // assigned once; never replaced
    private readonly List<Stock> stocks = Enumerable.Range(1, 500)
        .Select(i => new Stock { Id = i, Symbol = $"S{i:000}", Price = 100 }).ToList();
    private readonly CancellationTokenSource stop = new();
    private IJSObjectReference? gridHelpers;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        gridHelpers = await JS.InvokeAsync<IJSObjectReference>("stockGrid.attach", "stocksGrid", "stocksHost");
        _ = RunFeedAsync(stop.Token);
    }

    private async Task RunFeedAsync(CancellationToken token)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        try
        {
            while (await timer.WaitForNextTickAsync(token))
            {
                var patches = new List<PricePatch>();
                foreach (var stock in stocks.Where(_ => Random.Shared.Next(10) == 0))
                {
                    stock.Price = Math.Round(stock.Price * (0.98 + Random.Shared.NextDouble() * 0.04), 2);  // keeps C# in sync
                    patches.Add(new PricePatch(stock.Id, stock.Price));
                }
                await JS.InvokeVoidAsync("stockGrid.patchRows", token, "stocksGrid", patches);
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }   // page left
        catch (JSDisconnectedException) { }                                         // circuit gone (Server)
        catch (Exception ex) { Logger.LogError(ex, "The live feed stopped."); }       // e.g. a JSException
    }

    public async ValueTask DisposeAsync()
    {
        stop.Cancel();
        try
        {
            if (gridHelpers is not null)
            {
                await gridHelpers.InvokeVoidAsync("dispose");
                await gridHelpers.DisposeAsync();
            }
        }
        catch (JSDisconnectedException) { }
        stop.Dispose();
    }
}
```

```css
/* wwwroot/css/app.css */
.stocks-host { height: calc(100vh - 120px); }
```

```js
// wwwroot/grid-templates.js (structure.md; on 26.1.x register with igRegisterScript(…, false) and igTemplating.html)
registerScript('LivePriceCell', (ctx) => html`<span class="price-cell">${Number(ctx.implicit).toFixed(2)}</span>`);
```

```js
// wwwroot/grid-live.js, loaded with <script type="module" src="grid-live.js"></script>.
// It needs no api.js, so it works on 26.1.x and 26.2+. Paste keepPixelHeight (sizing.md) and watchSparklines (structure.md) into it.

// IgbGrid adds its igc-grid element a moment after the first render, also after `await grid.EnsureReady()` (#483).
function whenElement(id) {
    return new Promise((resolve) => {
        const found = document.getElementById(id);
        if (found) return resolve(found);
        const observer = new MutationObserver(() => {
            const element = document.getElementById(id);
            if (element) { observer.disconnect(); resolve(element); }
        });
        observer.observe(document.body, { childList: true, subtree: true });
    });
}

window.stockGrid = {
    async attach(gridId, hostId) {
        const grid = await whenElement(gridId);
        const sizer = keepPixelHeight(grid, document.getElementById(hostId));
        const releaseSparklines = watchSparklines(grid);
        return { dispose() { sizer.disconnect(); releaseSparklines(); } };
    },

    async patchRows(gridId, patches) {
        const grid = await whenElement(gridId);
        if (!grid.data) return;
        const byId = new Map(grid.data.map((row) => [row.Id, row]));   // grid rows keep the C# property names
        for (const patch of patches) {                                  // IJSRuntime sends camelCase
            const row = byId.get(patch.id);
            if (row) row.Price = patch.price;
        }
        grid.data = grid.data.slice();   // same records, new array: only changed Price cells re-render
    },
};
```

- **Keep both sides in sync.** JavaScript edits never reach your `List<T>`. A later `Data` assignment from C# replaces them, and re-renders everything. Update your C# objects as well (as above), and don't assign `Data` again per tick.
- **Templates that read other fields of the row** don't re-run when only those fields change ([`structure.md`](./structure.md#javascript-cell-templates-bodytemplatescript)). Show the column's own field. Otherwise, in `patchRows`, put an updated copy (`{ ...row, Price: patch.price }`) at the record's index in `grid.data` instead of mutating the record, which re-renders that row only. Or call `grid.markForCheck()` after the assignment, which re-runs every rendered cell.
- **The active cell is positional.** After a re-sort it stays at the same row position, so it can land on a different record.
- **Why the pixel height** (`attach` above): with any Blazor `Height`, every update re-measures the container and restarts the cells' CSS animations ([`sizing.md`](./sizing.md#live-data-give-the-grid-element-a-pixel-height)).
- On Blazor Server, arrays of primitives in the records arrive as `null` ([`structure.md`](./structure.md#javascript-cell-templates-bodytemplatescript), #479).

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
