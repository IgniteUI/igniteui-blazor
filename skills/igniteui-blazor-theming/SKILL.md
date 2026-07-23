---
name: igniteui-blazor-theming
description: "Covers theming and visual customization for Ignite UI for Blazor: choosing and switching built-in themes (Bootstrap, Material, Fluent, Indigo - light and dark variants), generating color palettes and component themes with the igniteui-theming MCP server, CSS design tokens, dark mode, scoped component theming, CSS shadow parts, and global roundness/spacing/size tokens. Use when users ask about changing the look and feel of Ignite UI Blazor components, applying a color scheme, generating CSS variables, using CSS parts, or customizing design tokens. Do NOT use for grid data features or component configuration - use igniteui-blazor-grids or igniteui-blazor-components instead."
user-invocable: true
---

# Ignite UI for Blazor - Theming and Styling

All token names, palette variables, and generated CSS come from the **Ignite UI Theming MCP server** (`igniteui-theming`). Do not write `--ig-*` variable names, token names, or part names from memory - they are version- and theme-specific. Component docs (CSS parts lists, theme file paths) come from the **Ignite UI CLI MCP server** via `get_doc` (see `themes-overview` and `theming-grid`).

Blazor theming is **CSS-first**: there is no Sass pipeline. Always pass `platform: "blazor"` and `output: "css"` to theming tools that accept them.

**If the theming tools are unavailable**, add the server to your client's MCP config (`.vscode/mcp.json` uses a top-level `servers` key; `.mcp.json` and most other clients use `mcpServers`), then start it:

```json
{ "mcpServers": { "igniteui-theming": { "command": "npx", "args": ["-y", "igniteui-theming", "igniteui-theming-mcp"] } } }
```

## Workflow

1. **Check for an existing theme first.** Inspect the host page (`wwwroot/index.html`, `App.razor`, `_Host.cshtml`) and global CSS for a theme `<link>` or `:root` palette overrides. If a palette already exists, reuse its tokens and skip to component-level theming - do not regenerate the global palette unless the user asks.
2. **Palette**: `create_palette(platform: "blazor", output: "css", primary, secondary, surface, variant)`. Use `create_custom_palette` only for explicit per-shade control. Do **not** use `create_theme` for Blazor - it always outputs Sass requiring a compilation step, even with `platform: "blazor"`. Act on any luminance warnings the tool returns.
3. **Component themes**: call `get_component_design_tokens(component)` first, then `create_component_theme(platform: "blazor", output: "css", component, tokens: {...})` using only returned token names. The argument is `tokens` - there is no `overrides`. Query exact variants (`contained-button`, `flat-button`, `outlined-button`, `fab-button`), not generic `button`. When the token response returns compound-component guidance, follow its checklist; prefer primary tokens over refinement tokens unless the user asked for the refined state.
4. **Colors**: after a palette exists, use `get_color(color, variant)` (add `contrast: true` for the matching text token) and palette references like `var(--ig-primary-500)` - raw hex values belong only in palette seed inputs.
5. **Global layout tokens**: `set_roundness(radiusFactor: 0..1)`, `set_spacing(spacing: number)`, `set_size(size: "small" | "medium" | "large")`. Legacy size names (`compact`, `cosy`, `comfortable`) are invalid for Blazor.
6. **Reference data**: `read_resource` with `theming://platforms/blazor`, `theming://presets/palettes`, `theming://guidance/colors/usage`, `theming://guidance/colors/roles`, or `theming://guidance/colors/rules` (read the rules before choosing surface/gray colors).

**File safety**: never overwrite an existing stylesheet wholesale. Propose generated CSS as an addition or diff so the user's custom styles survive.

## Applying generated CSS

- Built-in themes: exactly **one** theme CSS link active at a time (`_content/IgniteUI.Blazor/themes/{light|dark}/{bootstrap|material|fluent|indigo}.css`). If the app uses full-featured grids, switch the grid stylesheet (`themes/grid/{light|dark}/...`) to the same variant; `IgbGridLite` has its own single stylesheet in the `IgniteUI.Blazor.GridLite` package, which is included in the base theme, so reference it only if there is no base theme link.
- Generated palette and layout-token CSS goes in `:root` in a stylesheet loaded **after** the built-in theme link, so overrides win the cascade.
- Surface color must match the variant: light surface for `variant: "light"`, dark surface for `variant: "dark"`. Chromatic shades run `50` (lightest) to `900` (darkest) and are not inverted for dark themes - only gray is.
- Runtime dark-mode toggle: swap the theme `<link>` href via JS interop (or toggle `disabled` between preloaded light/dark links). There is no C# theme-switch API.

## Blazor CSS scoping rules

- Selectors target the rendered web-component tags: `igc-button`, `igc-chip` - never the Razor names (`IgbButton`).
- **Global CSS** (`app.css`): use generated selectors as-is: `igc-chip { --ig-chip-background: var(--ig-primary-500); }`.
- **`.razor.css` isolation files**: prefix `igc-*` selectors with `::deep` (`::deep igc-chip { ... }`), otherwise Blazor's CSS isolation blocks them. `::deep` applies **only** to `igc-*` selectors - plain HTML/class selectors don't need it, and it does not work on a component's own root element. Never add `::deep` to `:root` blocks.
- Scoped theming: wrap the instance in a classed container and scope the selector (`.my-area igc-button { ... }`).
- **`::part()` is the fallback, not the default.** Check `get_component_design_tokens` first; use `igc-<tag>::part(<name>)` only for properties no token covers, and only after the component's Blazor `get_doc` entry confirms the exact part names. In isolation files combine both: `::deep igc-dialog::part(footer) { ... }`.
- Charts, maps, gauges, and sparklines do **not** consume CSS theme variables - set their colors via component parameters with resolved values (see the components skill).

## Related skills

- `igniteui-blazor-components` - all non-grid components
- `igniteui-blazor-grids` - data grids
