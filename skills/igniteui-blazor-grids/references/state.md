# State Persistence & Grid-Type-Specific Operations

This reference covers saving and restoring grid state, and grid-type-specific data operation patterns for Tree Grid, Hierarchical Grid, and Pivot Grid.

---

## State Persistence

Save and restore the full grid state (sorting, filtering, grouping, paging, selection, column order, column widths, column visibility, pinning) to survive page reloads, navigation, or user sessions.

### Save state

```razor
@inject IJSRuntime JS

<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" AutoGenerate="false"
         Width="100%" Height="500px">
    <IgbColumn Field="Name" Sortable="true" Filterable="true" />
    <IgbColumn Field="Department" Sortable="true" Groupable="true" />
    <IgbColumn Field="Salary" DataType="GridColumnDataType.Currency" HasSummary="true" />
    <IgbPaginator PerPage="10" />
</IgbGrid>

<IgbButton @onclick="SaveState">Save State</IgbButton>
<IgbButton @onclick="RestoreState">Restore State</IgbButton>

@code {
    private IgbGrid grid = default!;

    private async Task SaveState()
    {
        var state = await grid.GetStateAsync();
        var json = state.ToJson();
        await JS.InvokeVoidAsync("localStorage.setItem", "gridState", json);
    }

    private async Task RestoreState()
    {
        var json = await JS.InvokeAsync<string>("localStorage.getItem", "gridState");
        if (!string.IsNullOrEmpty(json))
        {
            var state = IgbGridState.FromJson(json);
            await grid.SetStateAsync(state);
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

<IgbGrid @ref="grid" Data="data" PrimaryKey="Id">
    ...
</IgbGrid>

@code {
    private IgbGrid grid = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RestoreState();
        }
    }

    public async void Dispose()
    {
        await SaveState();
    }

    private async Task SaveState()
    {
        var state = await grid.GetStateAsync();
        await JS.InvokeVoidAsync("localStorage.setItem", "gridState", state.ToJson());
    }

    private async Task RestoreState()
    {
        var json = await JS.InvokeAsync<string>("localStorage.getItem", "gridState");
        if (!string.IsNullOrEmpty(json))
        {
            await grid.SetStateAsync(IgbGridState.FromJson(json));
        }
    }
}
```

### Selective state save/restore

Save only specific parts of the state:

```razor
@code {
    private async Task SaveSortingOnly()
    {
        var state = await grid.GetStateAsync(new IgbGridStateOptions
        {
            Sorting = true,
            Filtering = false,
            Paging = false,
            Selection = false,
            Columns = false
        });
        await JS.InvokeVoidAsync("localStorage.setItem", "gridSorting", state.ToJson());
    }
}
```

### State with server storage

```razor
@code {
    private async Task SaveToServer()
    {
        var state = await grid.GetStateAsync();
        var json = state.ToJson();
        await UserPreferencesApi.SaveGridStateAsync(userId, "employeesGrid", json);
    }

    private async Task LoadFromServer()
    {
        var json = await UserPreferencesApi.GetGridStateAsync(userId, "employeesGrid");
        if (!string.IsNullOrEmpty(json))
        {
            await grid.SetStateAsync(IgbGridState.FromJson(json));
        }
    }
}
```

---

## Tree Grid Data Operations

### Recursive filtering

Tree Grid filtering is recursive by default — when a child matches a filter, all its ancestors are shown to preserve the tree structure:

```razor
<IgbTreeGrid Data="data" PrimaryKey="Id" ForeignKey="ParentId"
              AllowFiltering="true">
    <IgbColumn Field="Name" Filterable="true" />
    <IgbColumn Field="Department" Filterable="true" />
</IgbTreeGrid>
```

If you need non-recursive (flat) filtering behavior:

```razor
<IgbTreeGrid Data="data" PrimaryKey="Id" ForeignKey="ParentId"
              AllowFiltering="true"
              FilterStrategy="typeof(TreeGridFlatFilteringStrategy)">
    ...
</IgbTreeGrid>
```

### Per-level sorting

Sorting in the Tree Grid sorts within each parent's children, maintaining the hierarchy:

```razor
<IgbTreeGrid Data="data" PrimaryKey="Id" ChildDataKey="Children">
    <IgbColumn Field="Name" Sortable="true" />
    <IgbColumn Field="Priority" Sortable="true" />
</IgbTreeGrid>
```

### Batch editing with hierarchical transactions

When using `BatchEditing="true"` on a Tree Grid, transactions are hierarchical — deleting a parent queues deletion of its children (when `CascadeOnDelete="CascadeOnDelete.Cascade"`):

```razor
<IgbTreeGrid @ref="treeGrid" Data="data" PrimaryKey="Id" ForeignKey="ParentId"
              BatchEditing="true" RowEditable="true"
              CascadeOnDelete="CascadeOnDelete.Cascade">
    <IgbColumn Field="Name" Editable="true" />
    <IgbColumn Field="Department" Editable="true" />
</IgbTreeGrid>

@code {
    private IgbTreeGrid treeGrid = default!;

    private void CommitChanges()
    {
        treeGrid.Transactions.Commit(treeGrid.Data);
    }
}
```

### Expand/collapse programmatically

```razor
@code {
    private IgbTreeGrid treeGrid = default!;

    private async Task ExpandAll()
    {
        await treeGrid.ExpandAllAsync();
    }

    private async Task CollapseAll()
    {
        await treeGrid.CollapseAllAsync();
    }

    private async Task ToggleRow(int rowId)
    {
        await treeGrid.ToggleRowAsync(rowId);
    }
}
```

---

## Hierarchical Grid Data Operations

### Independent level operations

Each child grid in a Hierarchical Grid is an independent grid instance. Sorting, filtering, and editing at one level do not affect other levels:

```razor
<IgbHierarchicalGrid Data="customers" PrimaryKey="CustomerId"
                      AllowFiltering="true"
                      SortingDone="OnRootSortingDone">
    <IgbColumn Field="CompanyName" Sortable="true" Filterable="true" />

    <IgbRowIsland Key="Orders" PrimaryKey="OrderId"
                   AllowFiltering="true"
                   SortingDone="OnOrdersSortingDone"
                   FilterMode="FilterMode.ExcelStyleFilter">
        <IgbColumn Field="OrderDate" Sortable="true" Filterable="true" />
    </IgbRowIsland>
</IgbHierarchicalGrid>

@code {
    private void OnRootSortingDone(IgbSortingEventArgs args)
    {
        // Only affects the root level
    }

    private void OnOrdersSortingDone(IgbSortingEventArgs args)
    {
        // Fires for any child grid at the Orders level
    }
}
```

### Accessing child grid instances

```razor
<IgbRowIsland Key="Orders" PrimaryKey="OrderId"
               GridCreated="OnOrderGridCreated">
    ...
</IgbRowIsland>

@code {
    private List<IgbGrid> childGrids = new();

    private void OnOrderGridCreated(IgbGridCreatedEventArgs args)
    {
        childGrids.Add(args.Owner);
    }

    // Later, operate on a specific child grid:
    private async Task SortAllChildGrids()
    {
        foreach (var childGrid in childGrids)
        {
            await childGrid.SortAsync(new IgbSortingExpression[]
            {
                new IgbSortingExpression
                {
                    FieldName = "OrderDate",
                    Dir = SortingDirection.Desc
                }
            });
        }
    }
}
```

### State persistence for Hierarchical Grid

State is saved per level. The root grid state includes the state of all Row Islands:

```razor
@code {
    private IgbHierarchicalGrid hGrid = default!;

    private async Task SaveState()
    {
        var state = await hGrid.GetStateAsync();
        // The state includes child level configurations
        await JS.InvokeVoidAsync("localStorage.setItem", "hGridState", state.ToJson());
    }
}
```

---

## Pivot Grid Data Operations

### Dimension-based filtering

Pivot Grid filtering is done through the pivot configuration, not through column filtering:

```razor
@code {
    private IgbPivotConfiguration pivotConfig = default!;

    private void FilterByCountry(string country)
    {
        var countryDimension = pivotConfig.Filters?.FirstOrDefault(d => d.MemberName == "Country");
        if (countryDimension != null)
        {
            countryDimension.Filter = new IgbPivotDimensionFilter
            {
                FilterValues = new[] { country }
            };
        }
    }
}
```

### Dimension-based sorting

```razor
@code {
    private void SortByRow()
    {
        var rowDimension = pivotConfig.Rows?.FirstOrDefault();
        if (rowDimension != null)
        {
            rowDimension.SortDirection = SortingDirection.Asc;
        }
    }
}
```

### Toggling dimensions and values

```razor
@code {
    private void ToggleDimension(IgbPivotDimension dimension)
    {
        dimension.Enabled = !dimension.Enabled;
        // Trigger re-render
        StateHasChanged();
    }

    private void ToggleValue(IgbPivotValue value)
    {
        value.Enabled = !value.Enabled;
        StateHasChanged();
    }
}
```

### Pivot Grid read-only nature

The Pivot Grid does not support:
- Cell editing (`Editable`, `RowEditable`, `BatchEditing`)
- Row selection
- Cell selection
- Column selection
- Row dragging
- Action strip
- Paging
- Summaries (aggregations are built-in via `IgbPivotValue`)

If users need to edit source data, display an `IgbGrid` alongside the `IgbPivotGrid` pointing to the same data set.

---

## Key Rules

1. **State serialization is JSON** — `GetStateAsync()` returns an object that can be serialized with `ToJson()`.
2. **Restore state in `OnAfterRenderAsync`** — the grid must be rendered before you can call `SetStateAsync`.
3. **Tree Grid filtering is recursive** — matching children show their entire ancestor chain.
4. **Hierarchical Grid levels are independent** — sorting/filtering one level never affects another.
5. **Pivot Grid filtering is dimension-based** — use `IgbPivotConfiguration.Filters`, not column-level filtering.
6. **Pivot Grid is read-only** — never attempt editing on a Pivot Grid.
7. **Cascade delete on Tree Grid** — `CascadeOnDelete.Cascade` queues child deletions when a parent is deleted.
8. **Child grid access requires `GridCreated`** — child grid instances in Hierarchical Grid only exist after the user expands a row.

---

## See Also

- [references/structure.md](references/structure.md) — Column setup, sorting, filtering
- [references/types.md](references/types.md) — Grid type specifics
- [references/editing.md](references/editing.md) — Editing and batch transactions
- [references/data-operations.md](references/data-operations.md) — Programmatic sorting/filtering
- [references/paging-remote.md](references/paging-remote.md) — Paging and remote data
