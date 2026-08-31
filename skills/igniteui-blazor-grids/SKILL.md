---
license: MIT
name: igniteui-blazor-grids
description: "All Ignite UI for Blazor data grids — Grid Lite, Flat Grid, Tree Grid, Hierarchical Grid, Pivot Grid: setup, columns and templates, sorting, filtering, selection, cell and row editing, grouping, summaries, toolbar, Excel/CSV export, paging, remote and server-side data, virtualization, sizing, state persistence, and migrating Grid Lite to IgbGrid. Use for grids, tables, tabular data, cell editing, row selection, column pinning or hiding, grouped rows, pivot tables, tree or hierarchical data, master-detail views, and grid export. For non-grid components use igniteui-blazor-components; for theming use igniteui-blazor-theming."
user-invocable: true
---

# Ignite UI for Blazor - Data Grids

## How to use this skill

1. Pick the grid type from the [decision guide](#choosing-a-grid-type).
2. Read **every** reference file matching the request (in one parallel batch) — a request often spans several, e.g. remote paging plus editing needs `paging-remote.md` **and** `editing.md`.
3. If the `igniteui-cli` MCP server is available, call `get_doc(framework: "blazor", name: "<slug>")` for the grid and each feature, and `search_api` / `get_api_reference` for exact signatures. MCP output wins over this skill on any conflict.
4. Write code from what you read. Grid APIs differ sharply between Ignite UI's Angular, React, and Blazor products — do not carry syntax over from another framework, and say so plainly when something is not covered rather than guessing.

**Without the MCP server this skill still works** — the reference files are self-contained. Do not configure MCP unprompted; if the user wants it, see [MCP server (optional)](#mcp-server-optional).

### Routing table

| Task | Read |
|---|---|
| Quick start, columns, data types, cell/header/editor templates, column groups, multi-row layout, pinning, sorting UI, filtering UI, selection | [`references/structure.md`](./references/structure.md) |
| Grouping, summaries, cell merging, toolbar, Excel/CSV export, virtualization, row drag, action strip, master-detail, clipboard | [`references/features.md`](./references/features.md) |
| Grid Lite, Tree Grid, Hierarchical Grid, Pivot Grid specifics | [`references/types.md`](./references/types.md) |
| Programmatic sort / filter / group, `@ref` access, custom strategies | [`references/data-operations.md`](./references/data-operations.md) |
| Cell editing, row editing, validation, custom editors, add/delete rows | [`references/editing.md`](./references/editing.md) |
| Paging, remote data, server-side operations, virtual scrolling | [`references/paging-remote.md`](./references/paging-remote.md) |
| State persistence (`IgbGridState`, save/restore) | [`references/state.md`](./references/state.md) |
| Grid width/height, column sizing, row height, density | [`references/sizing.md`](./references/sizing.md) |
| Migrating `IgbGridLite` → `IgbGrid` | [`references/grid-migration.md`](./references/grid-migration.md) |

## Choosing a grid type

Ask in order:

1. **Read-only display** with sorting, filtering, and virtualization but no editing, selection, or paging → **`IgbGridLite`** (MIT, separate package).
2. **Pivot analytics** — rows/columns/values users can drag to reshape → **`IgbPivotGrid`**.
3. **Parent-child where each level has a different schema** (Companies → Departments → Employees) → **`IgbHierarchicalGrid`**.
4. **Parent-child within one schema** (`ManagerId` self-reference, or a nested children array) → **`IgbTreeGrid`**.
5. **Flat table needing enterprise features** (editing, grouping, paging, export) → **`IgbGrid`**.

When Grid Lite's capabilities run out, the upgrade path is always `IgbGrid` — never a non-grid component. See [`grid-migration.md`](./references/grid-migration.md).

| Grid | Module | Package |
|---|---|---|
| `IgbGridLite` | No module registry required | `IgniteUI.Blazor.GridLite` (MIT) |
| `IgbGrid` | `IgbGridModule` | `IgniteUI.Blazor` / `.Trial` |
| `IgbTreeGrid` | `IgbTreeGridModule` | `IgniteUI.Blazor` / `.Trial` |
| `IgbHierarchicalGrid` | `IgbHierarchicalGridModule` | `IgniteUI.Blazor` / `.Trial` |
| `IgbPivotGrid` | `IgbPivotGridModule` | `IgniteUI.Blazor` / `.Trial` |

Grids are **not** included in `IgniteUI.Blazor.Lite`.

## Prerequisites

| Requirement | Value |
|---|---|
| .NET SDK | 8.0 or later |
| Registration | `builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule), …)` in `Program.cs` |
| Using directive | `@using IgniteUI.Blazor.Controls` in `_Imports.razor` |
| CSS — full grids | **both** `_content/IgniteUI.Blazor/themes/light/bootstrap.css` **and** `_content/IgniteUI.Blazor/themes/grid/light/bootstrap.css` |
| CSS — Grid Lite | `_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css` only — not the two above |
| Script | `_content/IgniteUI.Blazor/app.bundle.js` before the Blazor framework script |

The grid-specific stylesheet is easy to miss and is required whenever any full-featured grid is on the page. Full setup detail lives in the components skill's [`setup.md`](../igniteui-blazor-components/references/setup.md).

## Feature availability

| Feature | GridLite | Grid | TreeGrid | HierarchicalGrid | PivotGrid |
|---|---|---|---|---|---|
| Sorting | ✅ | ✅ | ✅ | ✅ | dimension-based |
| Filtering | ✅ | ✅ | ✅ | ✅ | dimension-based |
| Column hiding / resizing | ✅ | ✅ | ✅ | ✅ | ❌ |
| Row / cell / column selection | ❌ | ✅ | ✅ (cascade) | ✅ | ❌ |
| Cell / row editing, row adding | ❌ | ✅ | ✅ | ✅ | ❌ |
| Grouping | ❌ | ✅ **only** | ❌ | ❌ | use dimensions |
| Summaries | ❌ | ✅ | ✅ | ✅ | built-in aggregations |
| Paging | ❌ | ✅ | ✅ | ✅ | ❌ |
| Column pinning / moving | ❌ | ✅ | ✅ | ✅ | ❌ |
| Multi-column headers | ❌ | ✅ | ✅ | ✅ | ❌ |
| Row dragging | ❌ | ✅ | ✅ | ✅ | ❌ |
| Master-detail | ❌ | ✅ **only** | ❌ | use `IgbRowIsland` | ❌ |
| Toolbar, Excel/CSV export | ❌ | ✅ | ✅ | ✅ | ❌ |
| Cell merging | ❌ | ✅ **only** | ❌ | ❌ | ❌ |
| State persistence | ❌ | ✅ | ✅ | ✅ | ✅ |
| Virtualization | ✅ | ✅ | ✅ | ✅ | ✅ |
| Load on demand | ❌ | ❌ | ✅ via `LoadChildrenOnDemandScript` | ✅ via `GridCreatedScript` | ❌ |
| Remote data ops | `DataPipelineConfiguration` | events + noop strategies | events + noop strategies | events + noop strategies | ❌ |

**Batch editing is not available in Blazor** on any grid type. Supported editing modes are cell editing and row editing only — never generate batch-editing code.

## Grid-wide rules

- **Set `PrimaryKey`.** Selection, editing, row-targeted APIs, and state persistence all depend on it.
- **Set `Height`.** Row virtualization only activates with a fixed height; without one every row renders to the DOM.
- **Prefer `AutoGenerate="false"`** so column order, types, and templates are explicit.
- **Set `DataType` on every column** — it drives the filter conditions, sort comparison, editor, and formatting.
- **`Data` must be a materialized collection** (`List<T>`, `T[]`), not `IQueryable` or JSON.
- **Use `@ref` for programmatic access**, and only after first render — the reference is `null` in `OnInitialized`.
- **Do not set column `Width` unless asked.** Without widths the grid distributes space proportionally and fills the container; fixed widths usually leave a gap on the right.
- Docs slugs follow `components/grid-lite/{topic}` and `components/grids/{grid|treegrid|hierarchicalgrid|pivotgrid}/{topic}`.

## MCP server (optional)

`igniteui-cli` provides `list_components`, `get_doc`, `search_docs`, `search_api`, `get_api_reference`, all taking `framework: "blazor"`. To enable it, add to `.vscode/mcp.json` (VS Code, key `servers`):

```json
{ "servers": { "igniteui-cli": { "command": "npx", "args": ["-y", "igniteui-cli", "mcp"] } } }
```

Or `.cursor/mcp.json` / `claude_desktop_config.json` (key `mcpServers`):

```json
{ "mcpServers": { "igniteui-cli": { "command": "npx", "args": ["-y", "igniteui-cli", "mcp"] } } }
```

Reload the editor afterwards. JetBrains: **Settings → Tools → AI Assistant → MCP Servers**, command `npx`, arguments `igniteui-cli mcp`.

## Related skills

- [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) — every non-grid component, plus charts
- [`igniteui-blazor-theming`](../igniteui-blazor-theming/SKILL.md) — themes, palettes, design tokens
