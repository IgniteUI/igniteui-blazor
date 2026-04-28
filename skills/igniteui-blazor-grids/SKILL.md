---
name: igniteui-blazor-grids
description: "Provides guidance on all Ignite UI for Blazor data grid types (Grid Lite, Flat Grid, Tree Grid, Hierarchical Grid, Pivot Grid) including setup, column configuration, sorting, filtering, selection, editing, grouping, summaries, toolbar, export, paging, remote data, and state persistence. Use when users ask about grids, tables, data grids, tabular data display, cell editing, batch editing, row selection, column pinning, column hiding, grouping rows, pivot tables, tree-structured data, hierarchical data, master-detail views, read-only data display, or exporting grid data. Do NOT use for non-grid UI components (forms, dialogs, navigation, charts) — use igniteui-blazor-components instead. Do NOT use for theming or styling — use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor — Grids Skill

> Use this skill when a user asks about **any** Ignite UI for Blazor data grid component: Grid Lite, Flat Grid, Tree Grid, Hierarchical Grid, or Pivot Grid. This includes column configuration, sorting, filtering, selection, editing, grouping, summaries, toolbar, export, paging, remote data, virtualization, and state persistence.

---

## MANDATORY AGENT PROTOCOL — YOU MUST FOLLOW THIS BEFORE PRODUCING ANY OUTPUT

**This file is a routing hub only. It contains NO code examples and NO API details.**

> **DO NOT write any code, component parameters, event names, or property names from memory.**
> Grid APIs change between versions. Anything generated without reading the reference files will be wrong.

You are **required** to complete ALL of the following steps before producing any grid-related code or answer:

**STEP 1 — Identify the grid type.**
Use the Grid Selection Decision Guide below. If the grid type is not explicitly stated, infer it from context or ask.

**STEP 2 — Identify every task category involved.**
Map the user's request to one or more rows in the Task → Reference File table below. A single request often spans multiple categories (e.g., remote paging AND editing requires reading both `paging-remote.md` AND `editing.md`).

**STEP 3 — Read every identified reference file in full (PARALLEL).**
Call `read_file` (or equivalent) on **all** reference files identified in Step 2 **in a single parallel batch** — do NOT read them one at a time sequentially. You must do this even if you believe you already know the answer. Do not skip, skim, or partially read a reference file.

**STEP 4 — Only then produce output.**
Base your code and explanation exclusively on what you read in Step 3. If the reference files do not cover something, say so explicitly rather than guessing.

> **When in doubt, read more rather than fewer reference files.** The cost of an unnecessary file read is negligible; the cost of hallucinated API usage is a broken application.

---

## Prerequisites

Before using any Ignite UI for Blazor grid component you must have:

| Requirement | Details |
|---|---|
| .NET SDK | .NET 8+, 9+, or 10+ |
| NuGet package | `IgniteUI.Blazor` (licensed or trial) for all full-featured grids; `IgniteUI.Blazor.GridLite` (MIT, separate NuGet) for Grid Lite only — grids are **not** included in `IgniteUI.Blazor.Lite` |
| Service registration | `builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule), ...)` in **Program.cs** with required module types |
| Using directive | `@using IgniteUI.Blazor.Controls` in **_Imports.razor** |
| CSS theme | `<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />` in **index.html** or **_Host.cshtml** |
| JS interop script | `<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>` in **index.html** or **_Host.cshtml** |

If these are not yet in place, read the `igniteui-blazor-components` skill's `references/setup.md` first.

---

## Grid Selection Decision Guide

Ignite UI for Blazor has **five grid types**. Ask these questions in order:

1. **Does the user need a lightweight, read-only data display** with sorting, filtering, and virtualization but no editing, selection, or paging? → **Grid Lite** (`IgbGridLite`, MIT licensed, separate `IgniteUI.Blazor.GridLite` package). **If the user later needs features beyond Grid Lite's capabilities, upgrade strictly to `IgbGrid`** — never recommend non-grid components as a substitute.
2. **Does the user need pivot-table analytics** (rows/columns/values/aggregations that users can drag-and-drop to reshape)? → **Pivot Grid** (`IgbPivotGrid`)
3. **Does the data have parent-child relationships where each level has a DIFFERENT schema** (e.g., Companies → Departments → Employees)? → **Hierarchical Grid** (`IgbHierarchicalGrid`)
4. **Does the data have parent-child relationships within a SINGLE schema** (e.g., Employees with a `ManagerId` field, or nested children arrays)? → **Tree Grid** (`IgbTreeGrid`)
5. **Is the data a flat list/table with enterprise features needed** (editing, batch editing, grouping, paging, export, etc.)? → **Flat Grid** (`IgbGrid`)

After choosing the grid type, **you must still complete Steps 2–4 from the mandatory protocol above** — return to the routing table and read every applicable `references/` file before writing any code.

> **AGENT INSTRUCTION — Documentation URL Pattern**: For grid-specific topics (sorting, filtering, editing, paging, etc.), docs URLs follow this naming pattern per grid type:
> - Grid Lite: `.../components/grid-lite/{topic}`
> - Flat Grid: `.../components/grids/grid/{topic}`
> - Tree Grid: `.../components/grids/treegrid/{topic}`
> - Hierarchical Grid: `.../components/grids/hierarchicalgrid/{topic}`
> - Pivot Grid: `.../components/grids/pivotgrid/{topic}`

---

## Grid Types & Registration

| Grid Component | Module to Register | Package | Typical Use Case |
|---|---|---|---|
| `IgbGridLite` | `IgbGridLiteModule` | `IgniteUI.Blazor.GridLite` (MIT) | Read-only display with sorting, filtering, virtualization — no editing or selection |
| `IgbGrid` | `IgbGridModule` | `IgniteUI.Blazor` (Commercial) | Flat tabular data, master-detail, CRUD, dashboards |
| `IgbTreeGrid` | `IgbTreeGridModule` | `IgniteUI.Blazor` (Commercial) | Self-referencing tree data (org charts, file trees, BOM) |
| `IgbHierarchicalGrid` | `IgbHierarchicalGridModule` | `IgniteUI.Blazor` (Commercial) | Multi-schema parent-child relationships (customers → orders → line items) |
| `IgbPivotGrid` | `IgbPivotGridModule` | `IgniteUI.Blazor` (Commercial) | Pivot table analytics, aggregations across dimensions |

> **AGENT INSTRUCTION — Grid Lite upgrade path**: When a user is working with `IgbGridLite` and their requirements exceed Grid Lite's capabilities (editing, selection, paging, grouping, summaries, export), you **MUST** recommend upgrading to `IgbGrid` from the `IgniteUI.Blazor` package. Never suggest a different component type — the upgrade path from Grid Lite is always to `IgbGrid`.

### Registration example

```csharp
// Program.cs — register only the modules you use
builder.Services.AddIgniteUIBlazor(
    typeof(IgbGridLiteModule),         // Grid Lite (IgniteUI.Blazor.GridLite package)
    typeof(IgbGridModule),             // Flat Grid
    typeof(IgbTreeGridModule),         // Tree Grid
    typeof(IgbHierarchicalGridModule), // Hierarchical Grid
    typeof(IgbPivotGridModule)         // Pivot Grid
);
```

> **Note:** `IgbGridLite` comes from the separate **`IgniteUI.Blazor.GridLite`** NuGet package (MIT licensed). All other grid types require the commercial **`IgniteUI.Blazor`** package.

---

## Feature Availability per Grid Type

| Feature | IgbGridLite | IgbGrid | IgbTreeGrid | IgbHierarchicalGrid | IgbPivotGrid |
|---|---|---|---|---|---|
| Column sorting | ✅ | ✅ | ✅ | ✅ | ✅ (dimension-based) |
| Column filtering | ✅ | ✅ | ✅ | ✅ | ✅ (dimension-based) |
| Row selection | ❌ | ✅ | ✅ (cascade) | ✅ | ❌ |
| Cell selection | ❌ | ✅ | ✅ | ✅ | ❌ |
| Column selection | ❌ | ✅ | ✅ | ✅ | ❌ |
| Grouping | ❌ | ✅ **Exclusive** | ❌ | ❌ | N/A (use dimensions) |
| Summaries | ❌ | ✅ | ✅ | ✅ | N/A (built-in aggregations) |
| Cell editing | ❌ | ✅ | ✅ | ✅ | ❌ (read-only) |
| Row editing | ❌ | ✅ | ✅ | ✅ | ❌ |
| Row adding | ❌ | ✅ | ✅ | ✅ | ❌ |
| Batch editing | ❌ | ✅ | ✅ | ✅ | ❌ |
| Paging | ❌ | ✅ | ✅ | ✅ | ❌ |
| Column pinning | ❌ | ✅ | ✅ | ✅ | ❌ |
| Column hiding | ✅ | ✅ | ✅ | ✅ | ❌ |
| Column moving | ❌ | ✅ | ✅ | ✅ | ❌ |
| Column resizing | ✅ | ✅ | ✅ | ✅ | ❌ |
| Multi-column headers | ❌ | ✅ | ✅ | ✅ | ❌ |
| Row dragging | ❌ | ✅ | ✅ | ✅ | ❌ |
| Master-detail | ❌ | ✅ **Exclusive** | ❌ | N/A (use RowIsland) | ❌ |
| Toolbar | ❌ | ✅ | ✅ | ✅ | ❌ |
| Export (Excel/CSV) | ❌ | ✅ | ✅ | ✅ | ❌ |
| State persistence | ❌ | ✅ | ✅ | ✅ | ✅ |
| Virtualization | ✅ | ✅ | ✅ | ✅ | ✅ |
| Cell merging | ❌ | ✅ | ❌ | ❌ | ❌ |
| Load on demand | ❌ | ❌ | ✅ **Exclusive** | ❌ | ❌ |
| Remote data ops | `DataPipelineConfiguration` | Events + noop strategies | Events + noop strategies | Events + noop strategies | N/A |

---

## Task → Reference File

| Task | Reference file |
|---|---|
| Grid type selection, quick start, column configuration, column templates, column groups, multi-row layout, pinning, sorting UI, filtering UI, selection | [`references/structure.md`](./references/structure.md) |
| Grouping, summaries, cell merging, toolbar, export, virtualization & performance, row drag, action strip, master-detail, clipboard | [`references/features.md`](./references/features.md) |
| Tree Grid specifics, Hierarchical Grid specifics, Grid Lite setup, Grid Lite data operations, Pivot Grid setup | [`references/types.md`](./references/types.md) |
| Programmatic sorting / filtering / grouping, `@ref` access, custom strategies | [`references/data-operations.md`](./references/data-operations.md) |
| Cell editing, row editing, batch editing, transactions, validation, summaries | [`references/editing.md`](./references/editing.md) |
| Paging, remote data, server-side ops, noop strategies, virtual scroll, multi-grid coordination | [`references/paging-remote.md`](./references/paging-remote.md) |
| State persistence, Tree Grid / Hierarchical Grid / Pivot Grid / Grid Lite data operations | [`references/state.md`](./references/state.md) |
| Grid sizing (width, height, column sizing, null/px/% modes, cell spacing CSS variables) | [`references/sizing.md`](./references/sizing.md) |

> **When in doubt, read more rather than fewer reference files.** The cost of an unnecessary file read is negligible; the cost of hallucinated API usage is a broken application.

---

## Related Skills

| Skill | Use for |
|---|---|
| `igniteui-blazor-components` | All non-grid Ignite UI Blazor components (forms, layout, data display, feedback, charts, etc.) |
| `igniteui-blazor-theming` | CSS custom properties, palettes, typography, theme switching, dark/light mode |
