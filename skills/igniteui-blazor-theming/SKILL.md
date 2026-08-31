---
license: MIT
name: igniteui-blazor-theming
description: "Theming and visual customization for Ignite UI for Blazor: choosing and switching the built-in Bootstrap, Material, Fluent and Indigo themes in light or dark, scoping a theme to part of the page with IgbThemeProvider, generating palettes and component design tokens with the igniteui-theming MCP server, overriding CSS custom properties, using CSS shadow parts, dark mode, and global roundness/spacing/size tokens. Use when changing the look and feel of Ignite UI Blazor components, applying a color scheme, or writing component CSS. For component configuration use igniteui-blazor-components; for grid data features use igniteui-blazor-grids."
user-invocable: true
---

# Ignite UI for Blazor - Theming & Styling

Blazor theming is **CSS-first**: a built-in theme stylesheet plus CSS custom properties. There is no Sass step. Ignite UI components render as web components with `igc-*` tag names, so every CSS selector targets `igc-button`, never `IgbButton`.

## How to use this skill

1. Identify the layer you need to change — the table below.
2. For anything version-specific (palette variables, component token names, layout token CSS), call the `igniteui-theming` MCP tools rather than writing names from memory. Token names are not guessable from component names.
3. Read [`references/common-patterns.md`](./references/common-patterns.md) for theme switching, dark mode, scoped overrides, and `::part()` recipes.

**Without the MCP server this skill still works** for theme selection, `IgbThemeProvider`, dark mode, and CSS structure. What it cannot give you is the exact set of `--ig-<component>-<token>` names — in that case say the token list was not verified, or read the generated theme CSS in `_content/IgniteUI.Blazor/themes/` to confirm. Do not configure MCP unprompted; see [MCP server (optional)](#mcp-server-optional).

| Layer | Mechanism |
|---|---|
| Baseline look | One built-in theme stylesheet: Bootstrap, Material, Fluent, Indigo × light, dark |
| Theme for a page region | `IgbThemeProvider` component |
| Global colors | Palette CSS custom properties in `:root` (`create_palette`) |
| One component's appearance | Component design tokens (`get_component_design_tokens` → `create_component_theme`) |
| Internal parts a token doesn't cover | `::part()` — only after confirming the part name |
| Global density | `set_roundness`, `set_spacing`, `set_size` |

## Built-in themes

Link exactly **one** stylesheet in the host page; loading two conflicts.

```html
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />                    <!-- your overrides, after -->
<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>
```

Paths are `_content/IgniteUI.Blazor/themes/{light|dark}/{bootstrap|material|fluent|indigo}.css`. Any full-featured grid needs the grid stylesheet in the same variant as well (`themes/grid/light/bootstrap.css`); `IgbGridLite` uses its own package path instead.

## Scoped theming with `IgbThemeProvider`

`IgbThemeProvider` applies a theme and variant to its subtree, overriding the global theme for those components. It is the built-in way to preview themes, theme one region differently, or switch theme at runtime without swapping stylesheets.

```razor
<IgbThemeProvider Theme="Theme.Bootstrap" Variant="ThemeVariant.Dark">
    <IgbCard>
        <IgbCardHeader><h3 slot="title">Dark region</h3></IgbCardHeader>
        <IgbCardContent><IgbButton>Sign In</IgbButton></IgbCardContent>
    </IgbCard>
</IgbThemeProvider>
```

`Theme`: `Material` (default), `Bootstrap`, `Indigo`, `Fluent`. `ThemeVariant`: `Light`, `Dark`. Register `IgbThemeProviderModule`. Bind the parameters to fields for a runtime theme switcher — no JS interop and no stylesheet juggling.

Use it when the change is scoped or interactive. Use a different stylesheet when the whole application should ship with one theme.

## Palettes

```
create_palette(platform: "blazor", output: "css",
               primary: "#3f51b5", secondary: "#e91e63",
               surface: "#ffffff", variant: "light")
```

- Use `create_palette` with `output: "css"`.
- The parameter names are `primary` / `secondary` / `surface` (the `primaryColor`-style names belong to `create_theme`).
- Use `create_custom_palette` only when the design needs explicit control over individual shades.
- Palette CSS belongs in `:root` in a stylesheet loaded **after** the built-in theme.
- Shades run `50` (lightest) to `900` (darkest). Do not invert chromatic colors for dark themes — only gray inverts.
- Surface must match the variant: light surface with `variant: "light"`, dark with `variant: "dark"`. Act on any luminance warning the tool returns.
- Raw hex belongs in the palette seed. After the palette exists, reference `var(--ig-primary-500)` and `var(--ig-primary-500-contrast)` downstream; `get_color(color: "primary", variant: "600")` resolves a token, with `contrast: true` for the matching text color.

## Component design tokens

```
get_component_design_tokens(component: "contained-button")

create_component_theme(platform: "blazor", output: "css", component: "contained-button",
                       tokens: { "background": "var(--ig-primary-500)",
                                 "foreground": "var(--ig-primary-500-contrast)" })
```

- **Always discover before you write.** Use only token names the tool returned.
- The argument is `tokens`, not `overrides`.
- Query the **exact variant** for variant-based components: `contained-button`, `flat-button`, `outlined-button`, `fab-button` — not plain `button`.
- If the response separates primary from refinement tokens, use the primary ones unless the user asked for a specific state or subpart.
- Compound components: follow the checklist in the tool's response. Standard compounds want the related child themes generated and scoped under the parent selector; composed compounds want only the parent's tokens.
- Charts, maps, gauges, and sparklines have **no** design tokens — style them through component parameters instead ([`charts.md`](../igniteui-blazor-components/references/charts.md)).

## Global layout tokens

| Goal | Tool | Value |
|---|---|---|
| Roundness | `set_roundness` | `radiusFactor: 0..1` |
| Spacing | `set_spacing` | `spacing: number`, optional `inline` / `block` |
| Size / density | `set_size` | `size: "small" \| "medium" \| "large"` |

All take `platform: "blazor"`, `output: "css"`, and their output goes in `:root`. Do not use the legacy names `compact` / `cosy` / `comfortable`.

## Where generated CSS goes

- **Global stylesheet** (`wwwroot/css/app.css`) — use MCP output as-is, loaded after the built-in theme.
- **`.razor.css` isolation file** — prefix every `igc-*` selector with `::deep`, or CSS isolation blocks it. Never add `::deep` to a `:root {}` block or a plain HTML class selector.
- Palette and layout tokens go in `:root`; component themes go on the `igc-*` selector or a scoped wrapper.

**Do not overwrite an existing stylesheet.** If `app.css`, `site.css`, or a `.razor.css` already exists, show the generated CSS as an addition for review rather than replacing the file — custom styles are easy to destroy and hard to recover.

## MCP server (optional)

`igniteui-theming` provides `create_palette`, `create_custom_palette`, `get_color`, `get_component_design_tokens`, `create_component_theme`, `set_roundness`, `set_spacing`, `set_size`, and `read_resource`. Always pass `platform: "blazor"` and `output: "css"` where accepted.

Reference resources: `theming://platforms/blazor`, `theming://presets/palettes`, `theming://guidance/colors/usage`, `theming://guidance/colors/roles`, `theming://guidance/colors/rules`.

To enable the server, add to `.vscode/mcp.json` (VS Code, key `servers`):

```json
{ "servers": { "igniteui-theming": { "command": "npx", "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"] } } }
```

Or `.cursor/mcp.json` / `claude_desktop_config.json` (key `mcpServers`):

```json
{ "mcpServers": { "igniteui-theming": { "command": "npx", "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"] } } }
```

Reload the editor afterwards. JetBrains: **Settings → Tools → AI Assistant → MCP Servers**, command `npx`, arguments `igniteui-theming igniteui-theming-mcp`.

## Related skills

- [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) — component APIs
- [`igniteui-blazor-grids`](../igniteui-blazor-grids/SKILL.md) — grid features and the `--ig-size` density scale
