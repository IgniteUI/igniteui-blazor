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

Events: `PageChange` and `PerPageChange` (`IgbNumberEventArgs`), `PagingDone` (`IgbPageEventArgs`).

The Pivot Grid does not support paging.

## Remote paging

Bind `Data` to just the current page and tell the paginator the true total.

```razor
<IgbGrid Data="currentPageData" PrimaryKey="Id" AutoGenerate="false" Width="100%" Height="500px">
    <IgbColumn Field="Name" Header="Name" />
    <IgbPaginator PerPage="@pageSize" TotalRecords="@totalRecords"
                  PageChange="OnPageChange" PerPageChange="OnPerPageChange" />
</IgbGrid>

@code {
    private List<Employee> currentPageData = new();
    private int totalRecords, currentPage;
    private int pageSize = 20;

    protected override Task OnInitializedAsync() => LoadPageAsync();

    private Task OnPageChange(IgbNumberEventArgs args)
    {
        currentPage = (int)args.Detail;
        return LoadPageAsync();
    }

    private Task OnPerPageChange(IgbNumberEventArgs args)
    {
        pageSize = (int)args.Detail;
        currentPage = 0;
        return LoadPageAsync();
    }

    private async Task LoadPageAsync()
    {
        var result = await Api.GetEmployeesPagedAsync(currentPage, pageSize);
        currentPageData = result.Items;
        totalRecords = result.TotalCount;
        StateHasChanged();
    }
}
```

Without `TotalRecords` the paginator cannot compute the page count and paging silently misbehaves.

## Remote sorting and filtering

Handle `SortingDone` and `FilteringDone`, translate the expressions into a server query, and reassign `Data`. In production, fold paging, sorting, and filtering into one request rather than reloading per event.

```razor
<IgbGrid Data="data" PrimaryKey="Id" Height="600px" AllowFiltering="true"
         SortingDone="OnSortingDone" FilteringDone="OnFilteringDone">
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
    <IgbColumn Field="Salary" Sortable="true" DataType="GridColumnDataType.Number" />
    <IgbPaginator PerPage="@pageSize" TotalRecords="@totalRecords" PageChange="OnPageChange" />
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

    private Task OnPageChange(IgbNumberEventArgs args)
    {
        currentPage = (int)args.Detail;
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
        StateHasChanged();
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
- Always call `StateHasChanged()` after reassigning `Data` from an async operation.
- `Data` must be a materialized `List<T>` or `T[]` — do not hand the grid an `IQueryable` and expect it to compose `Skip`/`Take`.
