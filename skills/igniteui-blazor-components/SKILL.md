---
name: igniteui-blazor-components
description: "Covers non-grid Ignite UI for Blazor UI components: application setup, form controls (inputs, combos, selects, date/time pickers, calendar, checkbox, radio, switch, slider, rating), layout containers (tabs, stepper, accordion, expansion panel, navigation drawer, navbar, tree), data-display components (list, card, carousel, avatar, badge, chip, icon, progress indicators, dropdown, tooltip), feedback overlays (dialog, snackbar, toast, banner), layout managers (Dock Manager, Tile Manager), and visualizations (charts, gauges, maps, sparklines). Use when users ask about any Ignite UI Blazor component that is NOT a data grid. Do NOT use for data grids or tabular data - use igniteui-blazor-grids instead. Do NOT use for theming or styling - use igniteui-blazor-theming instead."
user-invocable: true
---

# Ignite UI for Blazor - UI Components

All component documentation and API facts come from the **Ignite UI CLI MCP server** (`igniteui-cli`). Do not write Ignite UI component names, parameters, events, or module registrations from memory - APIs change between versions.

## Workflow

1. **Discover**: `list_components(framework: "blazor", filter: "<keyword>")` to browse by component name, or `search_docs(framework: "blazor", query: "<feature>")` for feature/free-text questions.
2. **Read the doc**: `get_doc(framework: "blazor", name: "<doc-slug>")` for every component family you will use, before writing markup. Use the doc `name` returned by discovery - do not guess slugs.
3. **Look up exact API**: `get_api_reference(platform: "blazor", component: "<IgbName>")` for properties, methods, events, and enums. Use `section` or `member` to keep responses small. Use `search_api` when the exact name is unknown.
4. Base output only on what the tools returned. If a component or feature has no Blazor doc, say so instead of guessing.

For new-project scaffolding, call `get_project_setup_guide(framework: "blazor")`. Setup details (NuGet install, feeds, project types) are in the `general-installing-blazor`, `general-getting-started*`, and `general-nuget-feed` docs via `get_doc`.

**If the MCP tools are unavailable**, add the server to your client's MCP config (`.vscode/mcp.json` uses a top-level `servers` key; `.mcp.json` and most other clients use `mcpServers`), then start it:

```json
{ "mcpServers": { "igniteui-cli": { "command": "npx", "args": ["-y", "igniteui-cli", "mcp"] } } }
```

## Packages

| Package | Source | Contents |
|---|---|---|
| `IgniteUI.Blazor.Lite` | NuGet.org (MIT) | Core UI components: forms, layout, navigation, data display, feedback |
| `IgniteUI.Blazor.GridLite` | NuGet.org (MIT) | Lightweight `IgbGridLite` data grid only |
| `IgniteUI.Blazor` | Infragistics private feed (licensed) | Full suite: everything plus charts, maps, gauges, Dock Manager, full grids |
| `IgniteUI.Blazor.Trial` | NuGet.org | Full suite with trial watermark, for evaluation |

All use the `IgniteUI.Blazor.Controls` namespace. Do **not** mix `IgniteUI.Blazor` with the Lite packages in one project - they duplicate components. If full `IgniteUI.Blazor` is already referenced, keep it unless the user explicitly switches strategy. See the `general-open-source-vs-premium` doc for details.

## Setup essentials (silent-failure checklist)

Every doc from `get_doc` shows the exact registration for its component. These are the things that fail *silently* when missing:

- **Module registration**: every `Igb*` component (and sub-component) needs its module in `Program.cs`: `builder.Services.AddIgniteUIBlazor(typeof(IgbComboModule), ...)`. A missing module means the component renders nothing, with no error. No-argument `AddIgniteUIBlazor()` registers everything (fine for prototypes, bigger bundles).
- **Runtime script**: `<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>` must be in the host page before the Blazor framework script. Without it, web components never register and the page renders blank.
- **Theme CSS link** in the host page (see the theming skill), and `@using IgniteUI.Blazor.Controls` in `_Imports.razor`.
- **Interactive render mode**: on .NET 8+ Blazor Web App, pages using Ignite UI components need `@rendermode InteractiveServer` / `InteractiveWebAssembly` / `InteractiveAuto` (or global interactivity in `App.razor`). Static SSR pages will not work. In split server/client solutions, register the service in **both** `Program.cs` files.

## Blazor-specific gotchas (not in the docs)

**Razor compiler traps** - these apply to any `Igb*` component:
- **RZ9986**: complex attribute content fails to build. `<IgbChip class="chip @(cond ? "a" : "")" />` is an error - make the whole attribute one C# expression or a helper method call.
- **CS1012**: single-quoted strings inside a double-quoted attribute lambda (`@onclick="() => Go('/home')"`) parse as char literals. Use a named handler method instead.
- **BL0005**: do not set component parameters via `@ref` from code (`chart.Brushes = "..."`). Pass them as markup attributes.

**Component references**:
- `@ref` fields are `null` until after first render - use them in event handlers or `OnAfterRenderAsync`, never in `OnInitialized`.

**Icons render blank until registered** - the most common cause of broken generated UIs:
- The library bundles **no icons**. Every `IconName`/`Collection` pair used anywhere - `IgbIcon`, `IgbIconButton`, icons in navbar/drawer/list/chip slots - renders empty space until that name is registered in that collection with real SVG content.
- Registration is **app-wide** (a global cached collection). Register each icon once - e.g. through a single `IgbIcon` in the main layout - instead of per usage:

```razor
<IgbIcon @ref="IconRegistrar" style="display: none" />
@code {
    IgbIcon IconRegistrar = default!;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await IconRegistrar.EnsureReady();
        await IconRegistrar.RegisterIconFromTextAsync("home",
            "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z'/></svg>",
            "material");
        // ...one call per icon name used anywhere in the app
    }
}
```

- The SVG string must be **genuine markup** (e.g. copied from the Material Icons/Symbols SVG set). Never emit placeholder strings or invented path data - they render blank or garbage. `RegisterIconAsync(name, url, collection)` loads from a URL instead, at the cost of a runtime network dependency.
- Some `get_doc` examples (e.g. the navigation drawer) show `IconName="..."` markup **without** the registration code - when copying doc markup, always add registration for every icon name it references.

**Slots and icons**:
- Always put `IgbIcon` in slots (`prefix`, `suffix`, `start`, `end`, `icon`, `label`) - never `<span class="material-icons">`. Font spans are `display: inline` and drift to the top of the slot's flex container; `IgbIcon` self-centers.

**Forms and binding**:
- There is no universal form pattern. `IgbCombo` and `IgbRadio` do **not** work inside a plain HTML `<form>`. Bind values explicitly (`@bind-Value`, `@bind-Checked`, or the documented change event) and only use form integration the current doc shows.
- Read input values from the bound property - `IgbInput` has no `GetValueAsync()`.

**Navigation Drawer** (`IgbNavDrawer`):
- Its `::part(base)` is always `position: fixed` - the drawer overlays content and the host contributes zero width, regardless of `Open`. There is no `pinned` parameter. For a pinned sidebar that occupies layout space, override in **global CSS** (not `.razor.css`): explicit width on the host, `::part(base) { position: relative; transform: none; }`, hide `::part(overlay)`, and remove the `inert` attribute via JS in `OnAfterRenderAsync`. Do not call `Show()` in `OnAfterRenderAsync` - it throws.
- Nav drawer items have no automatic selection tracking - set `item.Active` yourself.

**Charts, maps, gauges, sparklines** (require `IgniteUI.Blazor` / `.Trial`):
- They **ignore CSS theme variables**. Set colors via component parameters (`Brushes`, `Outlines`, axis text colors), using resolved color values.
- Brush-list parameters are space-separated **strings**: `Brushes="#4FC3F7 #81C784"`. `IncludedProperties`/`ExcludedProperties` are **string arrays**: `@(new string[] { "Id" })`.
- `CategoryChartType.Bar` does not exist - use `Column`, or `IgbDataChart` + `IgbBarSeries` for horizontal bars.
- `IgbSparkline` has no spline types - for smooth area sparklines use a small `IgbCategoryChart` with `SplineArea`.
- `InnerExtent` (donut hole) is a chart-level property - putting it on `IgbRingSeries` crashes at runtime.
- Category charts show markers by default; each `IgbDataChart` series category needs its own module.
- Always set explicit `Width`/`Height`; inside CSS Grid give the cell `min-height: 0` so the chart doesn't collapse or overflow.

**Dock Manager / Tile Manager**:
- `IgbContentPane.ContentId` must exactly match the projected element's `slot` attribute, and the Dock Manager needs an explicit height or it renders 0px tall.
- Layout serialization persists pane structure only - slot content stays static in Razor markup.

## Related skills

- `igniteui-blazor-grids` - all data grids (Grid, Tree Grid, Hierarchical Grid, Pivot Grid, Grid Lite)
- `igniteui-blazor-theming` - themes, palettes, design tokens, CSS parts
