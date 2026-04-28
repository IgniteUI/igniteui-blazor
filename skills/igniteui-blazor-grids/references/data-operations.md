# Data Operations — Programmatic Sorting, Filtering, Grouping & Custom Strategies

This reference covers accessing grid instances programmatically and performing sorting, filtering, and grouping operations via C# code (not just the UI).

---

## Accessing Grid Instances via `@ref`

Every grid type supports `@ref` to get a component reference for programmatic API calls:

```razor
<IgbGrid @ref="flatGrid" Data="data" PrimaryKey="Id">
    ...
</IgbGrid>

<IgbTreeGrid @ref="treeGrid" Data="treeData" PrimaryKey="Id" ForeignKey="ParentId">
    ...
</IgbTreeGrid>

<IgbHierarchicalGrid @ref="hGrid" Data="hierarchicalData" PrimaryKey="Id">
    ...
</IgbHierarchicalGrid>

<IgbPivotGrid @ref="pivotGrid" Data="pivotData" PivotConfiguration="pivotConfig">
    ...
</IgbPivotGrid>

@code {
    private IgbGrid flatGrid = default!;
    private IgbTreeGrid treeGrid = default!;
    private IgbHierarchicalGrid hGrid = default!;
    private IgbPivotGrid pivotGrid = default!;
}
```

> **Important:** Always declare the reference with `default!` and access it only after the component has rendered (e.g., in lifecycle methods after `OnAfterRenderAsync` or in event handlers).

---

## Programmatic Sorting

### Sort a single column

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id">
    <IgbColumn Field="Name" Sortable="true" />
    <IgbColumn Field="Salary" Sortable="true" DataType="GridColumnDataType.Number" />
</IgbGrid>

<IgbButton @onclick="SortByName">Sort by Name (A-Z)</IgbButton>
<IgbButton @onclick="SortBySalaryDesc">Sort by Salary (High-Low)</IgbButton>
<IgbButton @onclick="ClearSorting">Clear Sorting</IgbButton>

@code {
    private IgbGrid grid = default!;

    private async Task SortByName()
    {
        await grid.SortAsync(new IgbSortingExpression[]
        {
            new IgbSortingExpression
            {
                FieldName = "Name",
                Dir = SortingDirection.Asc
            }
        });
    }

    private async Task SortBySalaryDesc()
    {
        await grid.SortAsync(new IgbSortingExpression[]
        {
            new IgbSortingExpression
            {
                FieldName = "Salary",
                Dir = SortingDirection.Desc
            }
        });
    }

    private async Task ClearSorting()
    {
        await grid.ClearSortAsync();
    }
}
```

### Multi-column sort

Pass multiple expressions in a single call:

```razor
@code {
    private async Task SortMultiple()
    {
        await grid.SortAsync(new IgbSortingExpression[]
        {
            new IgbSortingExpression { FieldName = "Department", Dir = SortingDirection.Asc },
            new IgbSortingExpression { FieldName = "Name", Dir = SortingDirection.Asc }
        });
    }
}
```

### Sorting events

| Event | Type | Description |
|---|---|---|
| `SortingDone` | `EventCallback<IgbSortingEventArgs>` | Fires after sorting is applied (UI or programmatic) |

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id"
         SortingDone="OnSortingDone">
    ...
</IgbGrid>

@code {
    private void OnSortingDone(IgbSortingEventArgs args)
    {
        // args.SortingExpressions — current sorting state
    }
}
```

### Applies to

| Grid Type | Sorting Support |
|---|---|
| IgbGrid | ✅ Column-based |
| IgbTreeGrid | ✅ Column-based (sorts within each level) |
| IgbHierarchicalGrid | ✅ Column-based (each level independent) |
| IgbPivotGrid | ✅ Dimension-based only (via configuration) |

---

## Programmatic Filtering

### Filter a column

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id" AllowFiltering="true">
    <IgbColumn Field="Name" Filterable="true" DataType="GridColumnDataType.String" />
    <IgbColumn Field="Salary" Filterable="true" DataType="GridColumnDataType.Number" />
    <IgbColumn Field="IsActive" Filterable="true" DataType="GridColumnDataType.Boolean" />
</IgbGrid>

<IgbButton @onclick="FilterSalaryAbove50k">High Earners</IgbButton>
<IgbButton @onclick="FilterActiveEmployees">Active Only</IgbButton>
<IgbButton @onclick="ClearFilters">Clear All Filters</IgbButton>

@code {
    private IgbGrid grid = default!;

    private async Task FilterSalaryAbove50k()
    {
        await grid.FilterAsync("Salary", 50000, IgbFilteringCondition.GreaterThan);
    }

    private async Task FilterActiveEmployees()
    {
        await grid.FilterAsync("IsActive", true, IgbFilteringCondition.True);
    }

    private async Task ClearFilters()
    {
        await grid.ClearFilterAsync();
    }
}
```

### Clear filter on a single column

```razor
@code {
    private async Task ClearNameFilter()
    {
        await grid.ClearFilterAsync("Name");
    }
}
```

### Complex filtering with FilteringExpressionsTree

For AND/OR grouped conditions, build a `FilteringExpressionsTree`:

```razor
@code {
    private async Task ApplyComplexFilter()
    {
        var tree = new IgbFilteringExpressionsTree(FilteringLogic.And);

        tree.FilteringOperands.Add(new IgbFilteringExpression
        {
            FieldName = "Department",
            Condition = IgbFilteringCondition.Equals,
            SearchVal = "Engineering"
        });

        var salaryGroup = new IgbFilteringExpressionsTree(FilteringLogic.Or);
        salaryGroup.FilteringOperands.Add(new IgbFilteringExpression
        {
            FieldName = "Salary",
            Condition = IgbFilteringCondition.GreaterThan,
            SearchVal = 80000
        });
        salaryGroup.FilteringOperands.Add(new IgbFilteringExpression
        {
            FieldName = "Title",
            Condition = IgbFilteringCondition.Contains,
            SearchVal = "Senior"
        });

        tree.FilteringOperands.Add(salaryGroup);

        grid.AdvancedFilteringExpressionsTree = tree;
    }
}
```

### Filtering events

| Event | Type | Description |
|---|---|---|
| `FilteringDone` | `EventCallback<IgbFilteringEventArgs>` | Fires after filtering is applied |

### Applies to

| Grid Type | Filtering Support |
|---|---|
| IgbGrid | ✅ Column-based |
| IgbTreeGrid | ✅ Recursive (filters entire tree, shows matching rows + ancestors) |
| IgbHierarchicalGrid | ✅ Per-level independent |
| IgbPivotGrid | ✅ Dimension-based only (via `Filters` in configuration) |

---

## Programmatic Grouping (IgbGrid Only)

Grouping is exclusive to the flat grid. Tree Grid and Hierarchical Grid do not support grouping.

### Group by a column

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id">
    <IgbColumn Field="Department" Groupable="true" />
    <IgbColumn Field="Name" />
    <IgbColumn Field="Salary" DataType="GridColumnDataType.Number" />
</IgbGrid>

<IgbButton @onclick="GroupByDepartment">Group by Department</IgbButton>
<IgbButton @onclick="ClearGrouping">Clear Grouping</IgbButton>

@code {
    private IgbGrid grid = default!;

    private async Task GroupByDepartment()
    {
        await grid.GroupByAsync(new IgbGroupingExpression
        {
            FieldName = "Department",
            Dir = SortingDirection.Asc
        });
    }

    private async Task ClearGrouping()
    {
        await grid.ClearGroupingAsync();
    }
}
```

### Clear grouping on a specific field

```razor
@code {
    private async Task UngroupDepartment()
    {
        await grid.ClearGroupingAsync("Department");
    }
}
```

### Grouping events

| Event | Type | Description |
|---|---|---|
| `GroupingDone` | `EventCallback<IgbGroupingDoneEventArgs>` | Fires after grouping changes |

---

## Custom Sorting Strategy

Provide a custom sort comparer for a column:

```razor
<IgbColumn Field="Priority" Sortable="true"
           SortStrategy="typeof(PrioritySortStrategy)" />

@code {
    public class PrioritySortStrategy : IgbSortingStrategy
    {
        public override int Compare(object? a, object? b, SortingDirection direction)
        {
            var order = new Dictionary<string, int>
            {
                ["Critical"] = 0, ["High"] = 1, ["Medium"] = 2, ["Low"] = 3
            };

            int aOrder = order.GetValueOrDefault(a?.ToString() ?? "", 99);
            int bOrder = order.GetValueOrDefault(b?.ToString() ?? "", 99);

            int result = aOrder.CompareTo(bOrder);
            return direction == SortingDirection.Desc ? -result : result;
        }
    }
}
```

---

## Custom Filtering Strategy

Provide a custom filter condition:

```razor
<IgbColumn Field="Name" Filterable="true"
           FilterStrategy="typeof(CaseInsensitiveFilterStrategy)" />
```

---

## Key Rules

1. **All async methods need `await`** — `SortAsync`, `FilterAsync`, `GroupByAsync`, `ClearSortAsync`, `ClearFilterAsync`, `ClearGroupingAsync` are all asynchronous.
2. **`@ref` requires correct type** — use `IgbGrid`, `IgbTreeGrid`, `IgbHierarchicalGrid`, or `IgbPivotGrid` to match the component in markup.
3. **Access `@ref` only after render** — the reference is `null` until the component renders. Use it in event handlers or `OnAfterRenderAsync`, not in `OnInitialized`.
4. **Grouping is IgbGrid only** — calling `GroupByAsync` on a Tree Grid or Hierarchical Grid reference will fail.
5. **Tree Grid filtering is recursive** — filtering finds matching rows and all their ancestor rows, preserving the tree structure.
6. **Hierarchical Grid levels are independent** — sorting/filtering the root does not affect child grids.
7. **Pivot Grid uses configuration** — sorting and filtering are managed through `IgbPivotConfiguration`, not through programmatic `SortAsync`/`FilterAsync`.
8. **`FieldName` is case-sensitive** — it must match the C# property name exactly.

---

## See Also

- [references/structure.md](references/structure.md) — Column setup, sorting UI, filtering UI
- [references/features.md](references/features.md) — Grouping UI, summaries, toolbar
- [references/types.md](references/types.md) — Grid-type-specific behaviors
- [references/editing.md](references/editing.md) — Editing modes and transactions
- [references/paging-remote.md](references/paging-remote.md) — Server-side data operations
