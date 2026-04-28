# Paging, Remote Operations & Virtualization

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Local Paging with IgbPaginator](#local-paging-with-igbpaginator)
- [Remote Paging](#remote-paging)
- [Remote Sorting & Filtering](#remote-sorting--filtering)
- [Virtual Scrolling](#virtual-scrolling)
- [Key Rules](#key-rules)

---

## Local Paging with IgbPaginator

```razor
<IgbGrid Data="AllProducts" PrimaryKey="ProductID" AutoGenerate="false">
    <IgbColumn Field="ProductName" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" />
    <IgbPaginator PerPage="15" />
</IgbGrid>
```

`IgbPaginator` is a child component of `IgbGrid`. It handles all local pagination automatically.

Key attributes on `IgbPaginator`: `PerPage` (rows per page, default: 15), `SelectOptions` (array of per-page options shown in the page size dropdown), `TotalRecords` (only needed for remote paging - see below).

---

## Remote Paging

Remote paging loads only the current page from the server. The grid displays the page data and the paginator shows the correct total count.

```razor
<IgbGrid @ref="Grid"
         Data="CurrentPageData"
         PrimaryKey="ProductID"
         AutoGenerate="false"
         Height="500px">
    <IgbColumn Field="ProductName" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" />
    <IgbPaginator @ref="Paginator"
                  PerPage="@PageSize"
                  TotalRecords="@TotalRecords"
                  PageChange="OnPageChange"
                  PerPageChange="OnPerPageChange" />
</IgbGrid>

@code {
    IgbGrid Grid { get; set; }
    IgbPaginator Paginator { get; set; }

    List<Product> CurrentPageData { get; set; } = new();
    int TotalRecords { get; set; } = 0;
    int PageSize { get; set; } = 15;
    int CurrentPage { get; set; } = 0;

    protected override async Task OnInitializedAsync()
    {
        await LoadPageAsync(0, PageSize);
    }

    async Task OnPageChange(int newPage)
    {
        CurrentPage = newPage;
        await LoadPageAsync(newPage, PageSize);
    }

    async Task OnPerPageChange(int newSize)
    {
        PageSize = newSize;
        CurrentPage = 0;
        await LoadPageAsync(0, newSize);
    }

    async Task LoadPageAsync(int page, int perPage)
    {
        var result = await MyApi.GetProductsAsync(skip: page * perPage, take: perPage);
        CurrentPageData = result.Items;
        TotalRecords = result.TotalCount;
        StateHasChanged();
    }
}
```

> **AGENT INSTRUCTION:** When implementing remote paging, `TotalRecords` on `IgbPaginator` must be set to the server's total record count (not the page count). The grid `Data` must be replaced with each new page's data when the page changes.

---

## Remote Sorting & Filtering

For remote operations, disable local sorting/filtering and handle the events yourself:

```razor
<IgbGrid @ref="Grid"
         Data="CurrentPageData"
         PrimaryKey="ProductID"
         AutoGenerate="false"
         AllowFiltering="true"
         SortingChanged="OnSortingChanged"
         FilteringExpressionsTreeChange="OnFilteringChanged">
    <IgbColumn Field="ProductName" Sortable="true" Filterable="true" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" Sortable="true" />
    <IgbPaginator TotalRecords="@TotalRecords" PageChange="OnPageChange" />
</IgbGrid>

@code {
    IgbGrid Grid { get; set; }
    List<Product> CurrentPageData { get; set; } = new();
    int TotalRecords { get; set; }

    IgbSortingExpression[] CurrentSort { get; set; } = Array.Empty<IgbSortingExpression>();
    IgbFilteringExpressionsTree CurrentFilter { get; set; }

    async Task OnSortingChanged(IgbSortingExpression[] sortExpressions)
    {
        CurrentSort = sortExpressions;
        await LoadDataAsync();
    }

    async Task OnFilteringChanged(IgbFilteringExpressionsTree filterTree)
    {
        CurrentFilter = filterTree;
        await LoadDataAsync();
    }

    async Task LoadDataAsync()
    {
        var result = await MyApi.GetProductsAsync(sort: CurrentSort, filter: CurrentFilter);
        CurrentPageData = result.Items;
        TotalRecords = result.TotalCount;
        StateHasChanged();
    }
}
```

> **AGENT INSTRUCTION:** Confirm event names (`SortingChanged`, `FilteringExpressionsTreeChange`) from `get_doc`. These names may differ between versions.

---

## Virtual Scrolling

`IgbGrid` uses row virtualization by default when `Height` is set. Only the visible rows are rendered in the DOM.

```razor
<!-- Virtualization is enabled automatically when Height is set -->
<IgbGrid Data="LargeDataset" Height="600px" Width="100%">
    <IgbColumn Field="Name" />
</IgbGrid>
```

For column virtualization (useful when there are many columns):

```razor
<IgbGrid Data="Data" Height="600px" ColumnWidth="150px">
    <!-- many columns -->
</IgbGrid>
```

Programmatic scroll to a row:

```csharp
await Grid.NavigateToAsync(rowIndex: 500, columnIndex: 0, () =>
{
    // callback when navigation is complete
});
```

---

## Key Rules

1. **Always call `get_doc` before writing paging or remote data code.** Event names and paginator property names are version-specific.
2. **For remote paging, set `TotalRecords` on `IgbPaginator` to the server's total count**, not the count of the current page.
3. **For remote paging, replace `Grid.Data` (or the bound list) with new data on each page change.** The grid does not automatically fetch data.
4. **Row virtualization is enabled automatically by setting `Height` on the grid.** Remove the height to disable it (not recommended for large datasets).
5. **For remote sorting and filtering, handle the grid events and re-fetch data from the server.** Do not try to sort/filter the local `Data` list - that defeats remote operations.
6. **When combining remote paging + remote sorting + remote filtering, always reset to page 0 when sort or filter changes**, otherwise the user sees an empty page.
