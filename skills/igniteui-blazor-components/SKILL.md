---
license: MIT
name: igniteui-blazor-components
description: "Ignite UI for Blazor non-grid components: project setup and module registration; form controls (input, textarea, combo, select, date/time pickers, calendar, checkbox, radio, switch, slider, rating, mask input); layout and navigation (tabs, stepper, accordion, expansion panel, nav drawer, navbar, tree, splitter, divider); data display (list, card, carousel, avatar, badge, chip, icon, progress, dropdown, tooltip, chat); overlays (dialog, snackbar, toast, banner); Dock Manager and Tile Manager; and visualizations (charts, gauges, maps, sparklines). Use for any Ignite UI Blazor component that is not a data grid. For data grids use igniteui-blazor-grids; for theming and CSS use igniteui-blazor-theming."
user-invocable: true
---

# Ignite UI for Blazor - UI Components

## How to use this skill

1. Map the request to rows in the routing table below and read **every** matching reference file (in one parallel batch). A request often spans several — a form inside a dialog needs `form-controls.md` **and** `feedback.md`.
2. If the `igniteui-cli` MCP server is available, call `get_doc(framework: "blazor", name: "<slug>")` for each component involved, and `search_api` / `get_api_reference` for exact property, event, and method signatures. MCP output wins over this skill on any conflict.
3. Write code from what you read. The reference files carry verified Blazor APIs — do not substitute Angular, React, or Web Components syntax from memory, and say so plainly when something is not covered rather than guessing.

**Without the MCP server this skill still works** — the reference files are self-contained. Do not configure MCP unprompted; if the user wants it, see [MCP server (optional)](#mcp-server-optional).

### Routing table

| Task | Read |
|---|---|
| NuGet install, `Program.cs`, `_Imports.razor`, theme CSS + script tags, project types (Server / WASM / Web App / MAUI), render modes | [`references/setup.md`](./references/setup.md) |
| Input, Textarea, Combo, Select, Date Picker, Date Range Picker, Calendar, Date Time Input, Mask Input, Checkbox, Radio, Switch, Slider, Range Slider, Rating, value binding | [`references/form-controls.md`](./references/form-controls.md) |
| Tabs, Stepper, Accordion, Expansion Panel, Nav Drawer, Navbar, Tree, Splitter, Divider | [`references/layout.md`](./references/layout.md) |
| List, Card, Carousel, Avatar, Badge, Chip, Icon, Icon Button, Button, Button Group, Circular/Linear Progress, Dropdown, Tooltip, Ripple, Chat, Highlight | [`references/data-display.md`](./references/data-display.md) |
| Dialog, Snackbar, Toast, Banner | [`references/feedback.md`](./references/feedback.md) |
| Dock Manager, Tile Manager | [`references/layout-manager.md`](./references/layout-manager.md) |
| Category / Data / Financial / Pie / Donut charts, Sparkline, Treemap, Geographic Map, Gauges, Dashboard Tile, chart features | [`references/charts.md`](./references/charts.md) |

## Packages

| Package | Source | Contains |
|---|---|---|
| `IgniteUI.Blazor.Lite` | NuGet.org, MIT | Core UI components: forms, layout, navigation, data display, feedback, Tile Manager, Theme Provider |
| `IgniteUI.Blazor.GridLite` | NuGet.org, MIT | `IgbGridLite` only |
| `IgniteUI.Blazor` | Infragistics licensed feed | Full suite: everything above plus charts, maps, gauges, Dock Manager, enterprise grids |
| `IgniteUI.Blazor.Trial` | NuGet.org | Same as `IgniteUI.Blazor`, watermarked |

All four use the `IgniteUI.Blazor.Controls` namespace and serve static assets from `_content/IgniteUI.Blazor/` (except `IgniteUI.Blazor.GridLite`, which uses `_content/IgniteUI.Blazor.GridLite/`). **Do not mix `IgniteUI.Blazor` with `IgniteUI.Blazor.Lite`** — duplicate types in the same namespace. If the project already references the full package, do not add Lite/GridLite unless the user asks to switch strategy.

## Rules that apply to every component

- **Registration.** `builder.Services.AddIgniteUIBlazor()` in `Program.cs` is required. Passing `typeof(Igb<Name>Module)` arguments eagerly pre-loads exactly those modules; with no arguments every module is available. In `IgniteUI.Blazor.Lite` each component also registers its own module on first render, so the explicit list is a bundle-size optimization rather than a correctness requirement.
- **Runtime script.** `<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>` must appear before the Blazor framework script in the host page. Missing it means no web components register and the app renders blank.
- **Theme CSS.** Exactly one theme stylesheet, e.g. `_content/IgniteUI.Blazor/themes/light/bootstrap.css`.
- **Slots.** Composition uses named slots (`slot="start"`, `slot="title"`, `slot="footer"`, …), not wrapper components. Use `IgbIcon` inside slots — a font-icon `<span>` is `display: inline` and drifts to the top of the slot's flex box.
- **`@ref`.** Declare a field of the component type and use `@ref` for programmatic calls (`await dialog.ShowAsync()`). The reference is `null` until after first render. Some components need `await component.EnsureReady()` before their async methods in `OnAfterRenderAsync(firstRender)` — icon registration especially.
- **Parameters are PascalCase** (`ChartType`, `DataSource`), never Angular-style `[chartType]`.
- **`Name` is not an HTML name attribute.** On every Ignite UI component `Name` is the framework's element identity used for lookups. Do not use it to group radios or to name a form field.
- **Forms.** There is no universal form-integration pattern; several components (`IgbCombo`, `IgbRadio`) do not participate in a plain HTML `<form>`. Bind explicitly with `@bind-Value` / `@bind-Checked` and check the component's doc before assuming form behavior.
- **Dynamic `class` values** must be a single C# expression (`class="@ChipClass(item)"`). Mixing literal text with `@(...)` in one attribute raises **RZ9986**.

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

- [`igniteui-blazor-grids`](../igniteui-blazor-grids/SKILL.md) — Grid, Tree Grid, Hierarchical Grid, Pivot Grid, Grid Lite
- [`igniteui-blazor-theming`](../igniteui-blazor-theming/SKILL.md) — themes, palettes, design tokens, CSS parts
