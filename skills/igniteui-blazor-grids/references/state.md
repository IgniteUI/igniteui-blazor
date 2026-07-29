# State Persistence

`IgbGridState` saves and restores the full grid state — sorting, filtering, advanced filtering, grouping, paging, selection, row expansion, and column order/width/visibility/pinning — as a JSON string. Available on `IgbGrid`, `IgbTreeGrid`, `IgbHierarchicalGrid`, and `IgbPivotGrid`; not on `IgbGridLite`.

It is a **child component of the grid**, and `PrimaryKey` must be set for selection and row state to round-trip.

## Save and restore

```razor
@inject IJSRuntime JS

<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" AutoGenerate="false"
         Width="100%" Height="500px" Rendered="OnGridRendered">
    <IgbGridState @ref="gridState" />
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
    <IgbColumn Field="Department" Sortable="true" Groupable="true" />
    <IgbPaginator PerPage="10" />
</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private IgbGridState gridState = default!;

    private async Task SaveState()
    {
        var json = await gridState.GetStateAsStringAsync(Array.Empty<string>());
        await JS.InvokeVoidAsync("localStorage.setItem", "gridState", json);
    }

    private async Task RestoreState()
    {
        var json = await JS.InvokeAsync<string>("localStorage.getItem", "gridState");
        if (!string.IsNullOrEmpty(json))
            await gridState.ApplyStateFromStringAsync(json, Array.Empty<string>());
    }

    private async void OnGridRendered() => await RestoreState();
}
```

**Restore only after the grid has rendered** — the `Rendered` event is the right hook; `ApplyStateFromStringAsync` from `OnInitialized` will not work.

The same JSON goes to a server just as easily — swap `localStorage` for an API call in `SaveState` / `RestoreState`.

## Selective state

Both methods take an array of feature names. Empty means every feature:

```razor
@code {
    private Task<string> SaveSortingOnly()
        => gridState.GetStateAsStringAsync(new[] { "sorting", "filtering" });
}
```

To exclude features from tracking altogether, set `Options`:

```razor
@code {
    protected override void OnInitialized()
        => gridState.Options = new IgbGridStateOptions { CellSelection = false, Sorting = false };
}
```

## Auto-save on navigation

```razor
@implements IDisposable

@code {
    void IDisposable.Dispose() => _ = SaveState();
}
```

Combine with `Rendered` for restore, as above, so state survives navigation away and back.
