# State Persistence

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**
> For grid setup and column configuration — see [`structure.md`](./structure.md).

## Contents

- [State Persistence](#state-persistence)
- [Key Rules](#key-rules)

---

## State Persistence

Save and restore the full grid state (sorting, filtering, grouping, paging, selection, column order, column widths, column visibility, pinning) to survive page reloads, navigation, or user sessions.

### Save state

```razor
@inject IJSRuntime JS

<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" AutoGenerate="false"
         Width="100%" Height="500px">
    <IgbGridState @ref="gridState"></IgbGridState>
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
    <IgbColumn Field="Department" Sortable="true" Groupable="true" />
    <IgbColumn Field="Salary" DataType="GridColumnDataType.Currency" HasSummary="true" />
    <IgbPaginator PerPage="10" />
</IgbGrid>

<IgbButton @onclick="SaveState">Save State</IgbButton>
<IgbButton @onclick="RestoreState">Restore State</IgbButton>

@code {
    private IgbGrid grid = default!;
    private IgbGridState gridState = default!;

    private async Task SaveState()
    {
        string json = await gridState.GetStateAsStringAsync(new string[0]);
        await JS.InvokeVoidAsync("localStorage.setItem", "gridState", json);
    }

    private async Task RestoreState()
    {
        var json = await JS.InvokeAsync<string>("localStorage.getItem", "gridState");
        if (!string.IsNullOrEmpty(json))
        {
            await gridState.ApplyStateFromStringAsync(json, new string[0]);
        }
    }
}
```

### What state includes

| State Category | Properties Saved |
|---|---|
| Columns | Order, width, visibility, pinned, data type |
| Sorting | Active sort expressions and directions |
| Filtering | Active filter expressions |
| Grouping | Active group-by expressions (IgbGrid only) |
| Paging | Current page, page size |
| Selection | Selected rows, selected cells, selected columns |
| Advanced Filtering | Advanced filtering expressions tree |
| Row expansion | Expanded row IDs (Tree Grid, Hierarchical Grid) |
| Column moving | Column positions after user reordering |

### Auto-save on navigation

```razor
@implements IDisposable

<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" Rendered="OnGridRendered">
    <IgbGridState @ref="gridState"></IgbGridState>
    ...
</IgbGrid>

@code {
    private IgbGrid grid = default!;
    private IgbGridState gridState = default!;

    void OnGridRendered()
    {
        RestoreGridState();
    }

    void IDisposable.Dispose()
    {
        SaveGridState();
    }

    async void SaveGridState()
    {
        string state = await gridState.GetStateAsStringAsync(new string[0]);
        await JS.InvokeVoidAsync("localStorage.setItem", "gridState", state);
    }

    async void RestoreGridState()
    {
        string state = await JS.InvokeAsync<string>("localStorage.getItem", "gridState");
        if (!string.IsNullOrEmpty(state))
        {
            await gridState.ApplyStateFromStringAsync(state, new string[0]);
        }
    }
}
```

### Selective state save/restore

Save only specific parts of the state by passing feature name strings:

```razor
@code {
    private IgbGridState gridState = default!;

    private async Task SaveSortingOnly()
    {
        string state = await gridState.GetStateAsStringAsync(new string[] { "sorting" });
        await JS.InvokeVoidAsync("localStorage.setItem", "gridSorting", state);
    }
}
```

To disable specific features from being tracked, set `Options` on the `IgbGridState` component:

```razor
@code {
    private IgbGridState gridState = default!;

    protected override void OnInitialized()
    {
        gridState.Options = new IgbGridStateOptions
        {
            CellSelection = false,
            Sorting = false
        };
    }
}
```

### State with server storage

```razor
@code {
    private IgbGridState gridState = default!;

    private async Task SaveToServer()
    {
        string json = await gridState.GetStateAsStringAsync(new string[0]);
        await UserPreferencesApi.SaveGridStateAsync(userId, "employeesGrid", json);
    }

    private async Task LoadFromServer()
    {
        var json = await UserPreferencesApi.GetGridStateAsync(userId, "employeesGrid");
        if (!string.IsNullOrEmpty(json))
        {
            await gridState.ApplyStateFromStringAsync(json, new string[0]);
        }
    }
}
```

---

## Key Rules

1. **`PrimaryKey` must be set on the grid** for state persistence to work reliably (especially for selection and row pinning).
2. **`IgbGridState` is a child component of the grid** - add `<IgbGridState @ref="gridState">` inside the grid markup and use its methods.
3. **Use `GetStateAsStringAsync` / `ApplyStateFromStringAsync`** - these accept/return JSON strings directly. Pass `new string[0]` for all features, or specific feature names like `new string[] { "sorting", "filtering" }`.
4. **Restore state in the grid's `Rendered` event** - the grid must be fully rendered before you can call `ApplyStateFromStringAsync`.