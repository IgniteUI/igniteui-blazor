# Grid Data Operations - Sorting, Filtering & Grouping

> **Part of the [`igniteui-blazor-grids`](../SKILL.md) skill hub.**

## Contents

- [Accessing the Grid Instance](#accessing-the-grid-instance)
- [Sorting](#sorting)
- [Filtering](#filtering)
- [Grouping](#grouping)
- [Remote Data Operations](#remote-data-operations)
- [Key Rules](#key-rules)

---

## Accessing the Grid Instance

Use `@ref` to access the grid from C#:

```razor
<IgbGrid @ref="Grid" Data="Data" AutoGenerate="false">
    <IgbColumn Field="ProductName" Sortable="true" Filterable="true" />
</IgbGrid>

@code {
    private IgbGrid Grid { get; set; }
}
```

For code that depends on rendered elements or child components, initialize after first render.

---

## Sorting

Enable sorting per column:

```razor
<IgbColumn Field="ProductName" Sortable="true" />
```

Initial sorting is controlled with `SortingExpressions`:

```razor
<IgbGrid Data="Data" SortingExpressions="InitialSort">
    <IgbColumn Field="Category" Sortable="true" />
</IgbGrid>

@code {
    private IgbSortingExpression[] InitialSort =
    {
        new IgbSortingExpression
        {
            FieldName = "Category",
            Dir = SortingDirection.Asc,
            IgnoreCase = true
        }
    };
}
```

Programmatic sorting uses the grid instance:

```csharp
await Grid.SortAsync(new[]
{
    new IgbSortingExpression
    {
        FieldName = "Country",
        Dir = SortingDirection.Asc
    }
});

await Grid.ClearSortAsync("Country");
await Grid.ClearSortAsync("");
```

> **AGENT INSTRUCTION:** Sorting changes the displayed order, not the underlying data source. Confirm method signatures from `get_doc` before writing final code.

---

## Filtering

Enable filtering on the grid and, when needed, on individual columns:

```razor
<IgbGrid Data="Data"
         AutoGenerate="false"
         AllowFiltering="true"
         FilterMode="FilterMode.QuickFilter">
    <IgbColumn Field="ProductName" DataType="GridColumnDataType.String" />
    <IgbColumn Field="UnitPrice" DataType="GridColumnDataType.Number" Filterable="false" />
</IgbGrid>
```

Initial filtering is controlled with `FilteringExpressionsTree`:

```razor
<IgbGrid Data="Data"
         AllowFiltering="true"
         FilteringExpressionsTree="InitialFilter">
</IgbGrid>
```

Create the filter tree in C# using the operand and condition names confirmed by `get_doc`.

> **AGENT INSTRUCTION:** Do not invent filtering condition names. Blazor filtering uses condition names such as `"contains"` in MCP examples, but the exact condition set depends on data type and version.

---

## Grouping

Enable grouping on each column that can be grouped:

```razor
<IgbGrid Data="Data" GroupingExpressions="InitialGroups">
    <IgbColumn Field="ShipCountry" Groupable="true" />
    <IgbColumn Field="ShipCity" Groupable="true" />
</IgbGrid>
```

Initial grouping uses `GroupingExpressions`:

```csharp
private IgbGroupingExpression[] InitialGroups =
{
    new IgbGroupingExpression
    {
        FieldName = "ShipCountry",
        Dir = SortingDirection.Asc
    }
};
```

Programmatic grouping uses the grid instance:

```csharp
Grid.GroupBy(InitialGroups);
```

Grouping supports expand/collapse state, group-row selection, group row templates, summaries, and paging interactions. Read `grid-groupby` before combining these features.

---

## Remote Data Operations

Use [`paging-remote.md`](./paging-remote.md) when sorting, filtering, paging, or virtualization is handled by a server. Remote scenarios usually require:

- Loading only the visible page or chunk from the server
- Setting `TotalRecords` or `TotalItemCount` so the grid or paginator can calculate the full data size
- Replacing the bound data collection when server data changes
- Resetting paging when sort or filter state changes

---

## Key Rules

1. **Always call `get_doc` for the specific operation before writing code.** Method names, event args, and expression object shapes are version-specific.
2. **Use `@ref` for programmatic access.**
3. **Keep UI feature setup and programmatic state separate.** Use `features.md` for toolbar/UI behaviors and this file for C# sorting, filtering, grouping, and expression state.
4. **Use `paging-remote.md` for server-side operations.** Local data operations and remote data operations have different responsibilities.
5. **Do not use `get_api_reference` or `search_api` for Blazor.** They do not support Blazor; use `get_doc` and `search_docs`.
