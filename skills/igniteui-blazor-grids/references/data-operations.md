# Data Operations — Programmatic Sorting, Filtering, Grouping

Declarative sorting/filtering UI is in [`structure.md`](./structure.md).

## Getting a grid reference

```razor
<IgbGrid @ref="grid" Data="data" PrimaryKey="Id">…</IgbGrid>

@code {
    private IgbGrid grid = default!;   // or IgbTreeGrid / IgbHierarchicalGrid / IgbPivotGrid
}
```

The reference is `null` until after the first render — use it from event handlers or `OnAfterRenderAsync`, never `OnInitialized`. The declared type must match the component in markup.

## Sorting

```razor
@code {
    private Task SortByName() => grid.SortAsync(new IgbSortingExpression[]
    {
        new() { FieldName = "Name", Dir = SortingDirection.Asc }
    });

    private Task SortByDeptThenName() => grid.SortAsync(new IgbSortingExpression[]
    {
        new() { FieldName = "Department", Dir = SortingDirection.Asc },
        new() { FieldName = "Name", Dir = SortingDirection.Asc }
    });

    private Task ClearSorting() => grid.ClearSortAsync();
}
```

Multi-column sort is a single call with several expressions.

## Filtering

Simple filters assign a `FilteringExpressionsTree`; the advanced dialog's state lives in `AdvancedFilteringExpressionsTree`.

```razor
@code {
    private void FilterHighEarners()
    {
        var tree = new IgbFilteringExpressionsTree { Operator = FilteringLogic.And };
        tree.FilteringOperands = new IgbFilteringExpression[]
        {
            new() { FieldName = "Salary", ConditionName = "greaterThan", SearchVal = 50000 }
        };
        grid.FilteringExpressionsTree = tree;
    }

    private Task ClearAll()        => grid.ClearFilterAsync();
    private Task ClearNameFilter() => grid.ClearFilterAsync("Name");
}
```

Nest trees for AND/OR groups — `FilteringOperands` accepts both expressions and nested trees:

```razor
@code {
    private void ApplyComplexFilter()
    {
        // Department = "Engineering" AND (Salary > 80000 OR Title contains "Senior")
        var salaryOrTitle = new IgbFilteringExpressionsTree { Operator = FilteringLogic.Or };
        salaryOrTitle.FilteringOperands = new IgbFilteringExpression[]
        {
            new() { FieldName = "Salary", ConditionName = "greaterThan", SearchVal = 80000 },
            new() { FieldName = "Title",  ConditionName = "contains", IgnoreCase = true, SearchVal = "Senior" }
        };

        var tree = new IgbFilteringExpressionsTree { Operator = FilteringLogic.And };
        tree.FilteringOperands = new object[]
        {
            new IgbFilteringExpression { FieldName = "Department", ConditionName = "equals", IgnoreCase = true, SearchVal = "Engineering" },
            salaryOrTitle
        };

        grid.AdvancedFilteringExpressionsTree = tree;
    }
}
```

`ConditionName` strings are camelCase and depend on the column's `DataType` — `"contains"`, `"startsWith"`, `"equals"`, `"greaterThan"`, `"before"`, `"true"`, `"empty"`, and so on. Confirm an unfamiliar one with `get_api_reference` rather than inventing it.

## Grouping — `IgbGrid` only

```razor
@code {
    private Task GroupByDepartment() => grid.GroupByAsync(new IgbGroupingExpression[]
    {
        new() { FieldName = "Department", Dir = SortingDirection.Asc }
    });

    private Task ClearGrouping()      => grid.ClearGroupingAsync();
    private Task UngroupDepartment()  => grid.ClearGroupingAsync("Department");
}
```

`GroupByAsync` on a Tree or Hierarchical grid reference fails — those types have no grouping.

## Custom strategies

Set `SortStrategy="typeof(YourStrategy)"` or `FilterStrategy="typeof(YourStrategy)"` on a column and implement the matching base class. Look up the exact base type and method signature with `get_api_reference` before writing it — these differ from the Angular and Web Components equivalents.

## Rules

- Every operation is asynchronous: `SortAsync`, `ClearSortAsync`, `ClearFilterAsync`, `GroupByAsync`, `ClearGroupingAsync`. Await them.
- `FieldName` is case-sensitive and must match the C# property name exactly.
- Tree Grid filtering is recursive — matches keep their ancestor rows so the hierarchy stays intact.
- Hierarchical Grid levels are independent; sorting or filtering the root does nothing to child grids.
- Pivot Grid sorting and filtering go through `IgbPivotConfiguration`, not these methods.
