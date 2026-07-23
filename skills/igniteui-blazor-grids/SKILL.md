---
name: igniteui-blazor-grids
description: "Provides guidance on all Ignite UI for Blazor data grid types (Grid Lite, Flat Grid, Tree Grid, Hierarchical Grid, Pivot Grid) including setup, columns, sorting, filtering, selection, editing, grouping, summaries, toolbar, export, paging, remote data, and state persistence. Use when users ask about grids, tables, tabular data, cell editing, row selection, column pinning, grouping, pivot tables, tree-structured or hierarchical data, master-detail views, or exporting grid data. Do NOT use for non-grid UI components (forms, dialogs, navigation, charts) - use igniteui-blazor-components instead. Do NOT use for theming or styling - use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor - Data Grids

All grid documentation and API facts come from the **Ignite UI CLI MCP server** (`igniteui-cli`). Do not write grid parameters, events, or expression types from memory - grid APIs change significantly between versions.

## Workflow

1. **Pick the grid type** with the decision guide below.
2. **Read the docs**: `get_doc(framework: "blazor", name: "<slug>")` for the grid overview and each feature involved, using the slug patterns below. Use `search_docs(framework: "blazor", query: "<feature>")` when unsure of the slug.
3. **Look up exact API**: `get_api_reference(platform: "blazor", component: "IgbGrid", section: "events")` etc. for parameters, events, methods, and enums; `search_api` when the exact name is unknown.
4. Base output only on what the tools returned. If a feature has no Blazor doc, say it is not covered instead of guessing.

**Doc slug patterns per grid type** (feature docs follow the overview's prefix):

| Grid | Overview slug | Feature slug pattern |
|---|---|---|
| Grid Lite | `grid-lite-overview` | `grid-lite-{topic}` (e.g. `grid-lite-sorting`) |
| Flat Grid | `data-grid` | `grid-{topic}` (e.g. `grid-editing`, `grid-paging`) |
| Tree Grid | `tree-grid-overview` | `tree-grid-{topic}` |
| Hierarchical Grid | `hierarchical-grid-overview` | `hierarchical-grid-{topic}` |
| Pivot Grid | `pivot-grid-overview` | `pivot-grid-{topic}` |

**If the MCP tools are unavailable**, add the server to your client's MCP config (`.vscode/mcp.json` uses a top-level `servers` key; `.mcp.json` and most other clients use `mcpServers`), then start it:

```json
{ "mcpServers": { "igniteui-cli": { "command": "npx", "args": ["-y", "igniteui-cli", "mcp"] } } }
```

## Grid selection decision guide

Ask in order:

1. **Lightweight, read-only display** with sorting, filtering, column resizing, and virtualization - no editing, selection, or paging? → `IgbGridLite` (`IgbGridLiteModule`, MIT `IgniteUI.Blazor.GridLite` package)
2. **Pivot-table analytics** (drag-and-drop rows/columns/values/aggregations)? → `IgbPivotGrid` (`IgbPivotGridModule`)
3. **Parent-child data where each level has a DIFFERENT schema** (Companies → Orders → Line Items)? → `IgbHierarchicalGrid` (`IgbHierarchicalGridModule`) with nested `IgbRowIsland` templates
4. **Parent-child data within a SINGLE schema** (`ManagerId` field or nested children of the same type)? → `IgbTreeGrid` (`IgbTreeGridModule`)
5. **Flat list with enterprise features** (editing, grouping, paging, export)? → `IgbGrid` (`IgbGridModule`)

All full-featured grids ship in `IgniteUI.Blazor` (licensed) / `IgniteUI.Blazor.Trial`; they are **not** in `IgniteUI.Blazor.Lite`. When `IgbGridLite` requirements grow beyond its capabilities, the upgrade path is always `IgbGrid` - never a non-grid component.

## Capability limits to respect

- **Batch editing is not available in Ignite UI for Blazor** - any grid. Supported modes are cell editing and row editing only; never generate batch-editing code.
- **Grouping, master-detail (`DetailTemplate`), and cell merging are `IgbGrid`-only.** Tree Grid and Hierarchical Grid do not support them.
- **Pivot Grid is read-only**: no editing, row/cell/column selection, paging, or column moving. Configure it via `IgbPivotConfiguration`; sorting/filtering are dimension-based.
- **Grid Lite** has no editing, selection, paging, grouping, summaries, pinning, or export. Tri-state sorting is always on. Columns are declared as `<IgbGridLiteColumn>` children; toggle them with `@if` blocks.
- **Load-on-demand** (Tree Grid `LoadChildrenOnDemandScript`, Hierarchical Grid `GridCreatedScript` on `IgbRowIsland`) works only through registered JavaScript interop functions - there is no pure-C# callback.

## Setup and behavior gotchas

- **CSS links**: full-featured grids need **two** stylesheets in the host page - the base theme (`_content/IgniteUI.Blazor/themes/light/bootstrap.css`) **and** the grid theme (`_content/IgniteUI.Blazor/themes/grid/light/bootstrap.css`), matching variants. `IgbGridLite` instead uses its own package path (`_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css`), which is the same theme as the base theme, so include it only if there is no base theme link. Missing links render an unstyled grid; a missing module registration renders nothing at all.
- **Always set `PrimaryKey`** - selection, editing, row APIs, and state persistence require it.
- **Always set `Height`** (pixel, vh, or % with a sized parent) - without it the grid renders every row and virtualization is disabled. This is the top performance mistake.
- **Do not set column `Width` unless asked** - width-free columns share the available space with no dead gap. If some columns have widths, leave at least one column width-free to absorb the remainder.
- **`Data` must be a materialized collection** (`List<T>`, `T[]`) - not `IQueryable`. After reloading data asynchronously, reassign `Data` and call `StateHasChanged()`.
- **`@ref` is `null` until after render** - call grid APIs from event handlers or `OnAfterRenderAsync`, and type the field to match the component (`IgbGrid`, `IgbTreeGrid`, ...).
- **`IgbPaginator` is a child of the grid**, not a sibling; there is no grid-level paging property. Remote paging needs `TotalRecords` plus `PageChange`/`PerPageChange` handlers.
- **Remote data operations** hook the `SortingDone` / `FilteringDone` events (and paginator events): read the expressions from `args.Detail`, query the server, reassign `Data`.
- **`FieldName` in sorting/filtering/grouping expression objects is case-sensitive** and must match the C# property name exactly.
- **Custom summaries are JavaScript-based** (`ColumnInitScript` + `igRegisterScript`) - see the `grid-summaries` doc; there is no C# summary operand class.
- **State persistence** uses the `IgbGridState` child component (`GetStateAsStringAsync` / `ApplyStateFromStringAsync`); restore in the grid's `Rendered` event, not earlier.

## Related skills

- `igniteui-blazor-components` - all non-grid components and visualizations
- `igniteui-blazor-theming` - themes, palettes, design tokens, CSS parts
