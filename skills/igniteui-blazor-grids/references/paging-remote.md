# Paging, Remote Data & Virtualization

## Paging with `IgbPaginator`

Paging is not a grid parameter — place an `IgbPaginator` **inside** the grid as a child component.

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" Width="100%" Height="500px">
    <IgbColumn Field="Name" Header="Name" />
    <IgbPaginator @ref="paginator" PerPage="10" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private IgbPaginator paginator = default!;

    private Task First() => paginator.PaginateAsync(0);   // 0-based page index
    private Task Next()  => paginator.NextPageAsync();
    private Task Prev()  => paginator.PreviousPageAsync();
}
```

| Parameter | Type | Default | Purpose |
|---|---|---|---|
| `PerPage` | `double` | `15` | Rows per page |
| `TotalRecords` | `double` | — | Server-side total; required for remote paging |
| `SelectOptions` | `double[]` | `[5, 10, 15, 25, 50]` | Page-size dropdown |

Events: `PageChange` and `PerPageChange` (`IgbNumberEventArgs`), `PagingDone` (`IgbPageEventArgs`; `Detail.Previous` / `Detail.Current`). Drive page loads from **`PagingDone`, not `PageChange`**: when the page size changes on a later page, the paginator resets to page 0 and raises a `PageChange` the Blazor wrapper fails to convert — an `InvalidCastException` is logged to the console (as `console.log`) and the handler never runs. `PagingDone` fires for user navigation and for that reset.

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

    // No StateHasChanged(): the event handlers and OnInitializedAsync return this Task,
    // and the component re-renders when it completes.
    private async Task LoadPageAsync()
    {
        var result = await Api.GetEmployeesPagedAsync(currentPage, pageSize);
        currentPageData = result.Items;
        totalRecords = result.TotalCount;
    }
}
```

Without `PagingMode="GridPagingMode.Remote"` the grid pages only the rows it holds and overrides `TotalRecords`: the pager shows "1 of 1" and no paging event fires. Without `TotalRecords` the paginator cannot compute the page count.

## Remote sorting and filtering

Handle `SortingDone` and `FilteringDone`, translate the expressions into a server query, and reassign `Data`. In production, fold paging, sorting, and filtering into one request rather than reloading per event.

```razor
<IgbGrid Data="data" PrimaryKey="Id" Height="600px" AllowFiltering="true"
         PagingMode="GridPagingMode.Remote"
         SortingDone="OnSortingDone" FilteringDone="OnFilteringDone">
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
    <IgbColumn Field="Salary" Sortable="true" DataType="GridColumnDataType.Number" />
    <IgbPaginator PerPage="@pageSize" TotalRecords="@totalRecords"
                  PagingDone="OnPagingDone" PerPageChange="OnPerPageChange" />
</IgbGrid>

@code {
    private List<Employee> data = new();
    private int totalRecords, currentPage;
    private int pageSize = 20;
    private IgbSortingExpression[]? currentSort;
    private IgbFilteringExpressionsTree? currentFilter;

    private Task OnSortingDone(IgbSortingExpressionEventArgs args)
    {
        currentSort = args.Detail;
        currentPage = 0;
        return LoadDataAsync();
    }

    private Task OnFilteringDone(IgbFilteringExpressionsTreeEventArgs args)
    {
        currentFilter = args.Detail;
        currentPage = 0;
        return LoadDataAsync();
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
        var result = await Api.QueryAsync(new QueryRequest
        {
            Page = currentPage,
            PageSize = pageSize,
            SortField = currentSort?.FirstOrDefault()?.FieldName,
            SortDirection = currentSort?.FirstOrDefault()?.Dir.ToString(),
            Filter = Translate(currentFilter)
        });

        data = result.Items;
        totalRecords = result.TotalCount;
    }
}
```

The grid still renders sort and filter UI while you own the actual data transformation. To stop it re-sorting or re-filtering the page it already has, assign noop sorting/filtering strategies on the grid — check `get_api_reference` for the current strategy types.

`IgbGridLite` handles remote operations differently, through `DataPipelineConfiguration` rather than events — see [`types.md`](./types.md).

## Virtualization

Row and column virtualization are automatic; the grid renders only the visible viewport and recycles rows as the user scrolls. Even at 100k+ rows only a few dozen exist in the DOM.

Two requirements:

1. **A fixed `Height`** (`"600px"`, `"80vh"`, or `100%` inside a sized parent). Without it every row renders and virtualization is off — this is the single biggest grid performance factor.
2. For column virtualization, total column width must exceed the grid width. It works without per-column widths (the grid falls back to a minimum column width), so do not add widths just for this.

## Rules

- `IgbPaginator` is a **child** of the grid, not a sibling.
- For remote paging set `PagingMode="GridPagingMode.Remote"` on the grid.
- Don't call `StateHasChanged()` after reassigning `Data` in an `EventCallback` handler (`PagingDone`, `PerPageChange`, `SortingDone`, `FilteringDone`, …) or in `OnInitializedAsync` / `OnParametersSetAsync`: the component re-renders when the returned `Task` completes, and an extra call inside an Ignite UI callback renders immediately, adding a render each time. Call it once only when `Data` changes outside those paths — a timer, a service event, a `[JSInvokable]` method, or an `Action<T>` event (`RowAdded`, `RowDeleted`, `CellEditDone`).
- `Data` must be a materialized `List<T>` or `T[]` — do not hand the grid an `IQueryable` and expect it to compose `Skip`/`Take`.
