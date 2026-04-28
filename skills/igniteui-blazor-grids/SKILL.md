---
name: igniteui-blazor-grids
description: "Covers all Ignite UI for Blazor data grid components and their features: IgbGrid, IgbTreeGrid, IgbHierarchicalGrid, IgbPivotGrid, and IgbGridLite. Also covers grid structure, columns, sorting, filtering, advanced filtering, grouping, selection, editing, row adding, row actions, summaries, paging, virtualization, remote operations, state persistence, toolbar, export, search, keyboard navigation, column pinning/hiding/moving/resizing, sizing, and density. Use when users ask about any tabular data display or data grid. Do NOT use for non-grid UI components - use igniteui-blazor-components instead. Do NOT use for theming or styling - use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor - Data Grids

## MANDATORY AGENT PROTOCOL - YOU MUST FOLLOW THIS BEFORE PRODUCING ANY OUTPUT

**This file is a routing hub only. It contains NO code examples and NO API details.**

> **DO NOT write any grid property names, event names, column types, or module registration calls from memory.**
> Grid APIs change significantly between versions. Anything generated without reading the reference files will be incorrect.

You are **required** to complete ALL of the following steps before producing any grid-related code or answer:

**STEP 1 - Identify the grid type and all features needed.**
If the user has not specified a grid type, use the decision guide below to select the most appropriate one. Then map each feature to one or more rows in the Task → Reference File table.

**STEP 2 - Read every identified reference file in full (PARALLEL).**
Call `read_file` on **all** reference files identified in Step 1 **in a single parallel batch**. Reference files map grid types and features to exact Blazor MCP doc slugs.

**STEP 3 - Extract doc slugs, then call `get_doc` for the relevant grid and each feature.**
Use the Ignite UI MCP `get_doc` tool with `framework: "blazor"` and the exact doc slug listed in the reference files you just read. This is the primary source of truth for Blazor grid APIs. Do NOT skip this step.

If a reference file does not list a slug for the requested grid feature, call `search_docs(framework: "blazor", query: "<grid feature>")` to find the correct doc. If no Blazor doc exists, say that the feature is not covered.

**STEP 4 - Only then produce output.**
Base your code exclusively on what you read. If the reference files or MCP docs do not cover something, say so rather than guessing.

---

### Grid Type Decision Guide

| Data shape | Recommended grid |
|---|---|
| Flat array of objects | `IgbGrid` |
| Nested objects (parent has a `Children` array) OR flat records with foreign key parent-child | `IgbTreeGrid` |
| Master-detail where rows expand to reveal a full child grid | `IgbHierarchicalGrid` |
| Cross-tab / pivot analysis with rows, columns, and aggregated values | `IgbPivotGrid` |
| Read-only, non-interactive, simple list in a grid format | `IgbGridLite` |

---

### Grid Types & Modules

| Grid type | Component | Module | Best for |
|---|---|---|---|
| Grid Lite | `IgbGridLite` | `IgbGridLiteModule` | OSS read-only grid scenarios |
| Flat Grid | `IgbGrid` | `IgbGridModule` | Standard tabular data and most grid features |
| Tree Grid | `IgbTreeGrid` | `IgbTreeGridModule` | Hierarchical rows in one grid surface |
| Hierarchical Grid | `IgbHierarchicalGrid` | `IgbHierarchicalGridModule` | Master-detail with child grids / row islands |
| Pivot Grid | `IgbPivotGrid` | `IgbPivotGridModule` | Pivot/cross-tab analysis |

> **AGENT INSTRUCTION:** This table is only a routing aid. Always verify exact module and component names in the matching Blazor MCP doc before writing code.

---

### Feature Availability per Grid Type

| Feature | Grid Lite | IgbGrid | IgbTreeGrid | IgbHierarchicalGrid | IgbPivotGrid |
|---|---|---|---|---|---|
| Sorting / filtering | Limited; verify `overview` | Yes | Yes | Yes | Pivot-specific |
| Advanced filtering | Not covered here | Yes | Check docs | Check docs | Not typical |
| Group by | Not covered here | Yes | Not typical | Not typical | Pivot-specific grouping |
| Editing / row adding | No | Yes | Yes | Yes | Not typical |
| Row actions | No | Yes | Yes | Yes | Not typical |
| Selection | Limited; verify `overview` | Row/cell/column | Row/cell | Row/cell | Check docs |
| Column pinning/hiding/moving/resizing | Limited; verify `overview` | Yes | Yes | Yes | Check docs |
| Master-detail / row islands | No | No | No | Yes | No |
| Virtualization | Verify `overview` | Yes | Yes | Yes | Check docs |
| Remote data operations | Not covered here | Yes | Check docs | Check docs | Not typical |

> **AGENT INSTRUCTION:** Treat this matrix as a first-pass guardrail, not final API truth. If a feature is marked "check docs" or "limited", call `search_docs(framework: "blazor", query: "<grid type> <feature>")` before answering.

---

### Task → Reference File

| Task | Reference file to read |
|---|---|
| Grid type decision, structure, basic setup, column definition, registering modules | [`references/structure.md`](./references/structure.md) |
| Sorting/filtering/grouping state, programmatic data operations, initial expressions, `@ref` grid access | [`references/data-operations.md`](./references/data-operations.md) |
| Sorting/filtering/grouping UI, advanced filtering, column pinning, column hiding, column moving, column resizing, summaries, selection (cell/row/column), row adding, row actions, search, keyboard navigation, toolbar | [`references/features.md`](./references/features.md) |
| IgbTreeGrid (nested data, foreign key), IgbHierarchicalGrid (row islands), IgbPivotGrid (config object) | [`references/types.md`](./references/types.md) |
| Cell editing, row editing, edit events, validation, edit templates | [`references/editing.md`](./references/editing.md) |
| Paginator, remote paging, remote sorting, remote filtering, virtualization | [`references/paging-remote.md`](./references/paging-remote.md) |
| State persistence (save/restore grid state as JSON) | [`references/state.md`](./references/state.md) |
| Column width, row height, grid sizing, density | [`references/sizing.md`](./references/sizing.md) |

> **When in doubt, read more rather than fewer reference files.** The cost of an unnecessary file read is negligible; the cost of hallucinated property names breaks the application.

---

## Prerequisites

- .NET 6+ Blazor project (Server, WebAssembly, Web App, or MAUI Blazor Hybrid)
- `IgniteUI.Blazor` NuGet package installed for full grid components, or `IgniteUI.Blazor.GridLite` for OSS-only `IgbGridLite`
- `builder.Services.AddIgniteUIBlazor(typeof(IgbGridModule))` in `Program.cs`
- `@using IgniteUI.Blazor.Controls` in `_Imports.razor`
- A theme CSS file linked in the host page - see [`igniteui-blazor-components/references/setup.md`](../igniteui-blazor-components/references/setup.md)
- The **Ignite UI CLI MCP server** (`igniteui-cli`) is available as a tool provider

> **AGENT INSTRUCTION - MCP Server Setup (REQUIRED)**
>
> Before using any Ignite UI CLI MCP tools, you MUST verify the MCP server is available
> by calling `list_components(framework: "blazor", filter: "grid")` or `search_docs(framework: "blazor", query: "grid")`.
> If the tool is not available or the call fails, you MUST automatically configure it by following the setup process steps described below.
>
> After writing the file, inform the user that the MCP server has been
> configured and they may need to reload the editor for the tools to activate.
> Start the MCP server and wait for that MCP server to start before continuing the grid-related work.
> Do NOT skip this step or ask the user to do it manually.

## Setting Up the Ignite UI CLI MCP Server

> **Full setup instructions for VS Code, Cursor, Claude Desktop, and JetBrains IDEs are in [`references/mcp-setup.md`](./references/mcp-setup.md).** Read that file for editor-specific configuration steps and verification.

## Key Blazor-Specific Notes

> **AGENT INSTRUCTION - No `get_api_reference` for Blazor**
>
> `get_api_reference` and `search_api` MCP tools do **not** support Blazor. All property names, event names, and method signatures must be obtained from `get_doc` results.

> **AGENT INSTRUCTION - `@ref` for programmatic access**
>
> Use `@ref` to obtain a C# reference to the grid, then call methods on it:
>
> ```razor
> <IgbGrid @ref="Grid" />
> @code { public IgbGrid Grid { get; set; } }
> ```

> **AGENT INSTRUCTION - Batch Editing is NOT available for Blazor**
>
> The Ignite UI for Blazor grid does **not** support batch editing. Supported editing modes are Cell editing and Row editing only. Do not suggest or generate batch editing code.

## Doc-Slug Validation Checklist

Before publishing or substantially updating this skill:

1. Read every `references/*.md` file and collect each documented `get_doc(framework: "blazor", name: "...")` slug.
2. Validate each slug with the Ignite UI CLI MCP `get_doc` tool.
3. For any missing slug, use `search_docs(framework: "blazor", query: "...")` to find the current Blazor doc name.
4. Remove or mark unsupported any grid feature whose slug cannot be found in Blazor docs.
5. Keep `get_api_reference` and `search_api` out of this checklist because they do not support Blazor.

---

## Related Skills

- [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) - All non-grid UI components
- [`igniteui-blazor-theming`](../igniteui-blazor-theming/SKILL.md) - Theming & Styling
