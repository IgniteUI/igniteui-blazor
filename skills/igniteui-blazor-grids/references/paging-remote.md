# Paging, Remote Data & Virtualization

## Paging with `IgbPaginator`

Paging is not a grid parameter — place an `IgbPaginator` **inside** the grid as a child component.

```razor
<IgbGrid Data="data" PrimaryKey="Id" Width="100%" Height="500px">
    <IgbColumn Field="Name" Header="Name" />
    <IgbPaginator PerPage="10" />
</IgbGrid>
```

Users page with the paginator's built-in controls. Changing the page from code does not work: `PaginateAsync`, `NextPageAsync` and `PreviousPageAsync` fail (`target doesn't have prop: paginate` in the console; the `Task` faults), and a bound `Page` is ignored whenever the new value equals the last value set from code, even after the user has paged away (so setting it back to `0` a second time does nothing), and syncing the field from `PagingDone` doesn't avoid this ([#569](https://github.com/IgniteUI/igniteui-blazor/issues/569)). Don't build custom First / Next / Previous buttons on them.

| Parameter | Type | Default | Purpose |
|---|---|---|---|
| `PerPage` | `double` | `15` | Rows per page |
| `TotalRecords` | `double` | — | Server-side total; required for remote paging |
| `SelectOptions` | `double[]` | `[5, 10, 15, 25, 50]` | Page-size dropdown |

Events: `PageChange` and `PerPageChange` (`IgbNumberEventArgs`), `PagingDone` (`IgbPageEventArgs`; `Detail.Previous` / `Detail.Current`). Drive page loads from **`PagingDone`, not `PageChange`**: a `PageChange` to page 0 — the first-page button, Previous back to the first page, or the reset after a page-size change — never reaches the handler, and nothing is logged ([#171](https://github.com/IgniteUI/igniteui-blazor/issues/171)), so the grid keeps showing the old page. `PagingDone` fires for every page change, including that reset.

The Pivot Grid does not support paging.

## Remote paging

Bind `Data` to just the current page, set `PagingMode="GridPagingMode.Remote"`, and tell the paginator the true total.

```razor
<IgbGrid Data="currentPageData" PrimaryKey="Id" AutoGenerate="false" Width="100%" Height="500px"
         PagingMode="GridPagingMode.Remote">
    <IgbColumn Field="Name" Header="Name" />
    <IgbPaginator PerPage="@pageSize" TotalRecords="@totalRecords"
                  PagingDone="OnPagingDone" PerPageChange="OnPerPageChange" />
</IgbGrid>

@code {
    private List<Employee> currentPageData = new();
    private int totalRecords, currentPage;
    private int pageSize = 20;

    protected override Task OnInitializedAsync() => LoadPageAsync();

    private Task OnPagingDone(IgbPageEventArgs args)
    {
        var page = (int)args.Detail.Current;
        if (page == currentPage) return Task.CompletedTask;   // the reset after a page-size change is already loaded
        currentPage = page;
        return LoadPageAsync();
    }

    private Task OnPerPageChange(IgbNumberEventArgs args)
    {
        pageSize = (int)args.Detail;
        currentPage = 0;
        return LoadPageAsync();
    }

    private int loadVersion;

    // No StateHasChanged(): the event handlers and OnInitializedAsync return this Task,
    // and the component re-renders when it completes.
    private async Task LoadPageAsync()
    {
        var version = ++loadVersion;
        var result = await Api.GetEmployeesPagedAsync(currentPage, pageSize);
        if (version != loadVersion) return;   // a newer request started (fast clicks): drop this stale page
        currentPageData = result.Items;
        totalRecords = result.TotalCount;
    }
}
```

Without `PagingMode="GridPagingMode.Remote"` the grid pages only the rows it holds and overrides `TotalRecords`: the pager shows "1 of 1" and neither `PageChange` nor `PagingDone` fires. Without `TotalRecords` the paginator cannot compute the page count.

## Remote sorting and filtering

Reload from the server when the sort or filter changes, and fold paging, sorting and filtering into one request. Three things differ from what the event names suggest:

- **Sorting:** handle `SortingExpressionsChange`. Its `Detail` holds every sorted column in order. `SortingDone` carries only the column just clicked, so storing it loses multi-column sorting (the default). Sorting does not move the paginator, and the page can't be changed from code ([#569](https://github.com/IgniteUI/igniteui-blazor/issues/569)), so reload the **current** page rather than page 0.
- **Filtering:** the C# filter events (`FilteringDone`, `Filtering`, `FilteringExpressionsTreeChange`) deliver a tree with the right `FieldName` but **no operands** — no condition, no search value. Use `FilteringDone` only as the trigger and read the grid's filter trees through JS interop. Keep the and/or structure: the filter row's operator and the Excel-style custom dialog produce `or` groups, and the Excel-style value list produces an `or` group of `equals` for one or two values and a single `in` condition, whose value is a JS `Set`, for three or more. The advanced-filtering dialog keeps its conditions in a separate tree (`advancedFilteringExpressionsTree`); its Apply raises `FilteringDone`, but its **Clear filter** button raises no C# event (the JS `filteringDone` has a `null` `detail`), so the snippet listens for it in JS.
- **Paging after a filter:** adding, changing or removing one of several conditions moves the paginator to the first page and raises `PagingDone` before `FilteringDone`. Clearing a column's last condition, Reset, *Clear column filters* and the advanced dialog leave it on its page — even past the new last page, which then shows an empty page under e.g. "3 of 2" until the user pages back. Keep `currentPage` as `PagingDone` leaves it, and don't reset it in the filter handler.
- **Local pass:** the grid still sorts and filters the rows it holds, even with `PagingMode="GridPagingMode.Remote"`, and has no parameter to turn that off. It compares strings case-insensitively by character code and dates in local time. A server that sorts culture-aware or case-sensitively gets rows reordered within the page, and one that matches accents loosely (SQL `*_CI_AI` collations) returns rows the local pass then hides ("No records found" under a non-zero total). Match these rules on the server.
- **Excel-style value list:** it lists only the values on the current page, and there is no Blazor API to feed it from the server. Ticking values filters to exactly those; unticking one filters to the page's *other* values, not "everything except X"; and if the page holds a single value, picking it sends no filter at all. Prefer the filter row or the custom dialog for remote data.

```razor
@inject IJSRuntime JS
@implements IDisposable

<div @ref="gridHost">
    <IgbGrid Data="data" PrimaryKey="Id" Height="600px" AllowFiltering="true"
             PagingMode="GridPagingMode.Remote"
             SortingExpressionsChange="OnSortingChanged" FilteringDone="OnFilteringDone">
        <IgbColumn Field="Name" Sortable="true" Filterable="true" />
        <IgbColumn Field="Salary" Sortable="true" DataType="GridColumnDataType.Number" />
        <IgbPaginator PerPage="@pageSize" TotalRecords="@totalRecords"
                      PagingDone="OnPagingDone" PerPageChange="OnPerPageChange" />
    </IgbGrid>
</div>

@code {
    private ElementReference gridHost;
    private List<Employee> data = new();
    private int totalRecords, currentPage, loadVersion;
    private int pageSize = 20;
    private IgbSortingExpression[] currentSort = [];
    private GridFilter[] currentFilter = [];   // every group must match
    private IDisposable? selfRef;

    public record GridFilter(string Operator, GridCondition[] Conditions, GridFilter[] Groups);   // Operator: "and" | "or"
    public record GridCondition(string Field, string Condition, System.Text.Json.JsonElement Value);

    protected override Task OnInitializedAsync() => LoadDataAsync();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        var self = DotNetObjectReference.Create(this);
        selfRef = self;
        await JS.InvokeVoidAsync("watchGridFilterClear", gridHost, self);
    }

    private Task OnSortingChanged(IgbSortingExpressionEventArgs args)
    {
        currentSort = args.Detail ?? [];   // all sorted columns, in order
        return LoadDataAsync();            // the paginator stays on its page
    }

    private Task OnFilteringDone(IgbFilteringExpressionsTreeEventArgs args) => ReloadFilterAsync();

    // The advanced dialog's "Clear filter" raises no C# event; grid-filter.js reports it here.
    [JSInvokable]
    public async Task OnFilterCleared()
    {
        await ReloadFilterAsync();
        StateHasChanged();   // a [JSInvokable] call does not re-render on its own
    }

    // args.Detail has no operands: read the grid's trees instead.
    // No currentPage = 0: when the grid resets the paginator, PagingDone has already set it.
    private async Task ReloadFilterAsync()
    {
        currentFilter = await JS.InvokeAsync<GridFilter[]>("readGridFilter", gridHost);
        await LoadDataAsync();
    }

    private Task OnPagingDone(IgbPageEventArgs args)
    {
        var page = (int)args.Detail.Current;
        if (page == currentPage) return Task.CompletedTask;
        currentPage = page;
        return LoadDataAsync();
    }

    private Task OnPerPageChange(IgbNumberEventArgs args)
    {
        pageSize = (int)args.Detail;
        currentPage = 0;
        return LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        var version = ++loadVersion;
        var result = await Api.QueryAsync(currentPage, pageSize, currentSort, currentFilter);
        if (version != loadVersion) return;   // a newer request started: drop this stale result
        data = result.Items;
        totalRecords = result.TotalCount;
    }

    public void Dispose() => selfRef?.Dispose();
}
```

```js
// wwwroot/js/grid-filter.js — converts the grid's filter trees into { operator, conditions, groups } groups
window.readGridFilter = host => {
    const grid = host.querySelector('igc-grid');
    // a date is a Date at local midnight: send the calendar date, not the UTC instant
    const day = v => v instanceof Date ? v.toLocaleDateString('sv') : v;
    const isDate = field => ['date', 'dateTime'].includes(grid.getColumnByName(field)?.dataType);
    // the Excel-style list's "in" condition holds a Set (date columns: toDateString() strings)
    const value = o => o.searchVal instanceof Set
        ? [...o.searchVal].map(v => isDate(o.fieldName) && typeof v === 'string' ? new Date(v).toLocaleDateString('sv') : day(v))
        : day(o.searchVal ?? null);
    const group = tree => ({
        operator: tree.operator === 1 ? 'or' : 'and',
        conditions: tree.filteringOperands.filter(o => !o.filteringOperands).map(o => ({
            field: o.fieldName,
            condition: o.condition?.name ?? o.conditionName,
            value: value(o)
        })),
        groups: tree.filteringOperands.filter(o => o.filteringOperands).map(group)
    });
    // the filter row / Excel-style menu and the advanced-filtering dialog keep separate trees; a row must match both
    return [grid.filteringExpressionsTree, grid.advancedFilteringExpressionsTree]
        .filter(tree => tree?.filteringOperands?.length)
        .map(group);
};

// The advanced dialog's "Clear filter" fires filteringDone with a null detail, which never reaches C#.
// Capture phase on the host: works for a non-bubbling event and before igc-grid exists.
window.watchGridFilterClear = (host, dotNetRef) => host.addEventListener('filteringDone', e => {
    if (e.target.localName === 'igc-grid' && e.detail == null) dotNetRef.invokeMethodAsync('OnFilterCleared');
}, true);
```

```html
<!-- host page (wwwroot/index.html, or App.razor in a Blazor Web App) -->
<script src="js/grid-filter.js"></script>
```

A row matches a group when all (`and`) or any (`or`) of its conditions and subgroups match; groups nest, e.g. `an OR (ri AND x)` from a filter row with mixed operators. `Condition` is the condition name (`contains`, `equals`, `in`, `greaterThan`, `before`, `true`, …), and `Value` is a `JsonElement` because its type depends on the column and condition: a string, a number, a date as `yyyy-MM-dd`, an array of those for `in`, or `null` for unary conditions such as `empty` or `thisYear`.

`IgbGridLite` handles remote operations differently, through `DataPipelineConfiguration` rather than events — see [`types.md`](./types.md).

## Virtualization

Row and column virtualization are automatic; the grid renders only the visible viewport and recycles rows as the user scrolls. Even at 100k+ rows only a few dozen exist in the DOM.

Two requirements:

1. **A fixed `Height`** (`"600px"`, `"80vh"`, or `100%` inside a sized parent). Without it every row renders and virtualization is off — this is the single biggest grid performance factor.
2. For column virtualization, total column width must exceed the grid width. It works without per-column widths (the grid falls back to a minimum column width), so do not add widths just for this.

## Rules

- `IgbPaginator` is a **child** of the grid, not a sibling.
- For remote paging set `PagingMode="GridPagingMode.Remote"` on the grid.
- Don't call `StateHasChanged()` after reassigning `Data` in an `EventCallback` handler (`PagingDone`, `PerPageChange`, `SortingDone`, `FilteringDone`, …) or in `OnInitializedAsync` / `OnParametersSetAsync`: the component re-renders when the returned `Task` completes, and an extra call inside an Ignite UI callback renders immediately, adding a render each time. Call it once only when `Data` changes outside those paths. In a `[JSInvokable]` method or an `Action<T>` event (`RowAdded`, `RowDeleted`, `CellEditDone`) call it directly — they already run on the renderer's dispatcher. From a timer or a service event, wrap the update and the call: `await InvokeAsync(() => { Data = fresh; StateHasChanged(); });` — on Blazor Server those callbacks run off the dispatcher, and a direct `StateHasChanged()` throws `InvalidOperationException`.
- `Data` must be a materialized `List<T>` or `T[]` — do not hand the grid an `IQueryable` and expect it to compose `Skip`/`Take`.
