---
name: igniteui-blazor-components
description: "Covers all non-grid Ignite UI for Blazor UI components: application scaffolding and setup, form controls (inputs, combos, selects, date/time pickers, calendar, checkbox, radio, switch, slider), layout containers (tabs, stepper, accordion, expansion panel, navigation drawer), data-display components (list, tree, card, chips, carousel, progress indicators), feedback overlays (dialog, snackbar, toast, banner), buttons and button groups, rating, ripple, tooltip, Tile Manager, and Charts. Use when users ask about any Ignite UI Blazor component that is NOT a data grid — such as forms, dropdowns, pickers, dialogs, navigation, lists, trees, cards, charts, or initial project setup. Do NOT use for data grids, tables, or tabular data — use igniteui-blazor-grids instead. Do NOT use for theming or styling — use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor — Components Skill

> Use this skill when a user asks about **any** Ignite UI for Blazor UI component that is NOT a data grid or theming/styling. This includes setup, form controls, layout containers, data-display components, feedback overlays, buttons, charts, gauges, Tile Manager, and more.

---

## Prerequisites

Before using any Ignite UI for Blazor component you must have:

| Requirement | Details |
|---|---|
| .NET SDK | .NET 8+, 9+, or 10+ |
| NuGet package | `IgniteUI.Blazor` (licensed/trial) **or** `IgniteUI.Blazor.Lite` (open-source/community) |
| Service registration | `builder.Services.AddIgniteUIBlazor(...)` in **Program.cs** with required module types |
| Using directive | `@using IgniteUI.Blazor.Controls` in **_Imports.razor** |
| CSS theme | `<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />` in **index.html** or **_Host.cshtml** |
| JS interop script | `<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>` in **index.html** or **_Host.cshtml** |

If these are not yet in place, read `references/setup.md` first.

---

## MCP Server Setup (igniteui-cli)

The `igniteui-cli` MCP server provides live component documentation, code samples, and scaffolding for Ignite UI across Angular, Blazor, React, and Web Components. Using the MCP server is **optional** but recommended for the best assistance.

Full setup instructions are in `references/mcp-setup.md`.

**Quick start (VS Code):**

Add to `.vscode/mcp.json`:

```json
{
  "servers": {
    "igniteui": {
      "command": "npx",
      "args": ["-y", "igniteui-cli@next", "mcp"]
    }
  }
}
```

---

## MANDATORY AGENT PROTOCOL

Every time you receive a task involving Ignite UI for Blazor components, you **MUST** follow these three steps in order:

### Step 1 — Identify the components

Read the user's request and determine which Ignite UI Blazor components are needed. Map each component to the correct reference file using the table below.

### Step 2 — Read the reference files

**Before writing any code**, open and read every matched reference file in full. Do NOT rely on memory or prior knowledge — the reference files contain verified API surfaces, correct enum values, valid parameter names, and tested patterns specific to the Blazor wrapper library.

### Step 3 — Produce output

Using **only** the patterns and APIs confirmed in the reference files, generate the code, explanation, or migration the user asked for.

> **Never skip Step 2.** If you generate code from memory without reading the reference files you will produce incorrect parameter names, wrong enum values, or Angular/React patterns that do not apply to Blazor.

---

## Task → Reference File

| Task / Components | Reference file |
|---|---|
| App setup, Program.cs registration, _Imports.razor, NuGet installation, CSS theme link, JS script | `references/setup.md` |
| `IgbInput`, `IgbCombo`, `IgbSelect`, `IgbDatePicker`, `IgbDateRangePicker`, `IgbDateTimeInput`, `IgbCalendar`, `IgbCheckbox`, `IgbRadio`, `IgbRadioGroup`, `IgbSwitch`, `IgbSlider`, `IgbRangeSlider`, `IgbRating`, forms | `references/form-controls.md` |
| `IgbTabs`, `IgbStepper`, `IgbAccordion`, `IgbExpansionPanel`, `IgbNavDrawer` | `references/layout.md` |
| `IgbList`, `IgbTree`, `IgbCard`, `IgbChip`, `IgbAvatar`, `IgbBadge`, `IgbIcon`, `IgbCarousel`, `IgbLinearProgress`, `IgbCircularProgress` | `references/data-display.md` |
| `IgbDialog`, `IgbSnackbar`, `IgbToast`, `IgbBanner` | `references/feedback.md` |
| `IgbButton`, `IgbIconButton`, `IgbToggleButton`, `IgbButtonGroup`, `IgbRipple`, `IgbTooltip` | `references/directives.md` |
| `IgbTileManager`, `IgbTile`, `IgbDockManager` | `references/layout-manager.md` |
| `IgbCategoryChart`, `IgbFinancialChart`, `IgbDataChart`, `IgbPieChart`, `IgbDataPieChart`, `IgbSparkline`, `IgbGeographicMap`, `IgbLinearGauge`, `IgbRadialGauge`, `IgbBulletGraph` | `references/charts.md` |
| MCP server installation and configuration | `references/mcp-setup.md` |

If a task spans multiple categories (e.g., a form inside a dialog), read **all** relevant reference files.

---

## Package Variants

| NuGet Package | Description |
|---|---|
| `IgniteUI.Blazor.Lite` | Open-source / community edition. MIT-licensed. Contains core UI components. |
| `IgniteUI.Blazor` | Licensed / trial edition. Includes all components plus premium features (charts, grids, DockManager, etc.). |

Both packages follow the same API patterns. The Lite package is a subset — if a component isn't available in Lite, the user needs the full `IgniteUI.Blazor` package.

---

## Related Skills

| Skill | Use for |
|---|---|
| `igniteui-blazor-grids` | Data grids, tree grids, hierarchical grids, pivot grids, and all tabular data components |
| `igniteui-blazor-theming` | CSS custom properties, palettes, typography, theme switching, dark/light mode |
