---
name: igniteui-blazor-grids
description: "Provides guidance on all Ignite UI for Blazor data grid types (Flat Grid, Tree Grid, Hierarchical Grid, Pivot Grid) including setup, column configuration, sorting, filtering, selection, editing, grouping, summaries, toolbar, export, paging, remote data, and state persistence. Use when users ask about grids, tables, data grids, tabular data display, cell editing, batch editing, row selection, column pinning, column hiding, grouping rows, pivot tables, tree-structured data, hierarchical data, master-detail views, or exporting grid data. Do NOT use for non-grid UI components (forms, dialogs, navigation, charts) — use igniteui-blazor-components instead. Do NOT use for theming or styling — use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor — Grids Skill

> Use this skill when a user asks about **any** Ignite UI for Blazor data grid component: Flat Grid, Tree Grid, Hierarchical Grid, or Pivot Grid. This includes column configuration, sorting, filtering, selection, editing, grouping, summaries, toolbar, export, paging, remote data, virtualization, and state persistence.

---

## MANDATORY AGENT PROTOCOL

Every time you receive a task involving Ignite UI for Blazor grids, you **MUST** follow these three steps in order:

### Step 1 — Identify the grid type

Read the user's request and determine which grid type is needed. Use the **Grid Selection Decision Guide** below to pick the correct component.

### Step 2 — Read the reference files

**Before writing any code**, open and read every matched reference file in full using the **Task → Reference File** table below. Do NOT rely on memory or prior knowledge — the reference files contain verified API surfaces, correct enum values, valid parameter names, and tested patterns specific to the Blazor wrapper library.

### Step 3 — Produce output

Using **only** the patterns and APIs confirmed in the reference files, generate the code, explanation, or migration the user asked for.

> **Never skip Step 2.** If you generate code from memory without reading the reference files you will produce incorrect parameter names, wrong enum values, or Angular/React patterns that do not apply to Blazor.

---

## Prerequisites

Before using any Ignite UI for Blazor grid component you must have:

| Requirement | Details |
|---|---|
| .NET SDK | .NET 8+, 9+, or 10+ |
| NuGet package | `IgniteUI.Blazor` (licensed or trial) — grids are **not** included in `IgniteUI.Blazor.Lite` |
| Service registration | `builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule), ...)` in **Program.cs** with required module types |
| Using directive | `@using IgniteUI.Blazor.Controls` in **_Imports.razor** |
| CSS theme | `<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />` in **index.html** or **_Host.cshtml** |
| JS interop script | `<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>` in **index.html** or **_Host.cshtml** |

If these are not yet in place, read the `igniteui-blazor-components` skill's `references/setup.md` first.

---

## Grid Selection Decision Guide

| Question | Answer → Grid Type |
|---|---|
| Is the data flat (no parent-child relationships)? | **IgbGrid** (Flat Grid) |
| Does the data have parent-child relationships within one schema (e.g., Employee → Manager)? | **IgbTreeGrid** |
| Does each level have a **different** schema (e.g., Customer → Orders → OrderDetails)? | **IgbHierarchicalGrid** + **IgbRowIsland** |
| Do you need pivot-table analytics with dimensions, values, rows, columns, and filters? | **IgbPivotGrid** |

> **Note:** There is no "Grid Lite" equivalent in Blazor. All grid types are available exclusively in the `IgniteUI.Blazor` package.

---

## Grid Types & Registration

| Grid Component | Module to Register | Typical Use Case |
|---|---|---|
| `IgbGrid` | `IgbGridModule` | Flat tabular data, master-detail, CRUD, dashboards |
| `IgbTreeGrid` | `IgbTreeGridModule` | Self-referencing tree data (org charts, file trees, BOM) |
| `IgbHierarchicalGrid` | `IgbHierarchicalGridModule` | Multi-schema parent-child relationships (customers → orders → line items) |
| `IgbPivotGrid` | `IgbPivotGridModule` | Pivot table analytics, aggregations across dimensions |

### Registration example

```csharp
// Program.cs
builder.Services.AddIgniteUIBlazor(
    typeof(IgbGridModule),        // Flat Grid
    typeof(IgbTreeGridModule),    // Tree Grid
    typeof(IgbHierarchicalGridModule), // Hierarchical Grid
    typeof(IgbPivotGridModule)    // Pivot Grid
    // Only register modules you actually use
);
```

---

## Feature Availability per Grid Type

| Feature | IgbGrid | IgbTreeGrid | IgbHierarchicalGrid | IgbPivotGrid |
|---|---|---|---|---|
| Column sorting | ✅ | ✅ | ✅ | ✅ (dimension-based) |
| Column filtering | ✅ | ✅ | ✅ | ✅ (dimension-based) |
| Row selection | ✅ | ✅ (cascade) | ✅ | ❌ |
| Cell selection | ✅ | ✅ | ✅ | ❌ |
| Column selection | ✅ | ✅ | ✅ | ❌ |
| Grouping | ✅ | ❌ | ❌ | N/A (use dimensions) |
| Summaries | ✅ | ✅ | ✅ | N/A (built-in aggregations) |
| Cell editing | ✅ | ✅ | ✅ | ❌ (read-only) |
| Row editing | ✅ | ✅ | ✅ | ❌ |
| Batch editing | ✅ | ✅ | ✅ | ❌ |
| Paging | ✅ | ✅ | ✅ | ❌ |
| Column pinning | ✅ | ✅ | ✅ | ❌ |
| Column hiding | ✅ | ✅ | ✅ | ❌ |
| Column moving | ✅ | ✅ | ✅ | ❌ |
| Column resizing | ✅ | ✅ | ✅ | ❌ |
| Multi-column headers | ✅ | ✅ | ✅ | ❌ |
| Row dragging | ✅ | ✅ | ✅ | ❌ |
| Master-detail | ✅ | ❌ | N/A (use RowIsland) | ❌ |
| Toolbar | ✅ | ✅ | ✅ | ❌ |
| Export (Excel/CSV) | ✅ | ✅ | ✅ | ❌ |
| State persistence | ✅ | ✅ | ✅ | ✅ |
| Virtualization | ✅ | ✅ | ✅ | ✅ |
| Cell merging | ✅ | ❌ | ❌ | ❌ |

---

## Task → Reference File

| Task | Reference file |
|---|---|
| Grid type selection, quick start, column configuration, column templates, column groups, multi-row layout, pinning, sorting UI, filtering UI, selection | `references/structure.md` |
| Grouping, summaries, cell merging, toolbar, export, virtualization & performance, row drag, action strip, master-detail, clipboard | `references/features.md` |
| Tree Grid specifics, Hierarchical Grid specifics, Pivot Grid setup | `references/types.md` |
| Programmatic sorting/filtering/grouping, component references, custom strategies | `references/data-operations.md` |
| Cell editing, row editing, batch editing, validation, custom editors | `references/editing.md` |
| Paging, remote data, server-side operations, virtual scroll | `references/paging-remote.md` |
| State persistence, grid-type-specific operations | `references/state.md` |
| Grid sizing (width, height, column sizing, cell spacing CSS variables) | `references/sizing.md` |

If a task spans multiple categories (e.g., editing in a tree grid), read **all** relevant reference files.

---

## Related Skills

| Skill | Use for |
|---|---|
| `igniteui-blazor-components` | All non-grid Ignite UI Blazor components (forms, layout, data display, feedback, charts, etc.) |
| `igniteui-blazor-theming` | CSS custom properties, palettes, typography, theme switching, dark/light mode |
