# Using the Theming MCP Server

> **Part of the [`igniteui-blazor-theming`](../SKILL.md) skill hub.**

## Contents

- [Available MCP Theming Tools](#available-mcp-theming-tools)
- [Blazor Platform Context](#blazor-platform-context)
- [Getting Component Design Tokens](#getting-component-design-tokens)
- [Creating a Component Theme](#creating-a-component-theme)
- [Creating a Color Palette](#creating-a-color-palette)
- [Reading Palette Colors](#reading-palette-colors)
- [Global Token Generators](#global-token-generators)
- [Applying Generated CSS](#applying-generated-css)
- [Key Rules](#key-rules)

## Available MCP Theming Tools

The `igniteui-theming` MCP server provides these tools by name:

| Tool | Purpose |
|---|---|
| `read_resource` | Read theming documentation and verify the MCP server is available |
| `get_component_design_tokens` | List themeable tokens for a component |
| `create_component_theme` | Generate CSS or Sass overrides for component design tokens |
| `create_palette` | Generate a full color palette from seed colors |
| `create_custom_palette` | Generate a palette with explicit color values |
| `get_color` | Look up a palette CSS variable reference |
| `create_elevations` | Generate elevation styles |
| `create_typography` | Generate typography styles |
| `set_roundness` | Generate global border-radius token CSS |
| `set_spacing` | Generate global spacing token CSS |
| `set_size` | Generate global size token CSS |

## Blazor Platform Context

This skill is scoped to Ignite UI for Blazor applications. 

Use the `read_resource` call above as a lightweight MCP availability check when
needed. For all theming tools that accept a platform parameter, pass `platform: "blazor"` explicitly.

## Getting Component Design Tokens

Use this to discover available component tokens before writing override CSS:

```
get_component_design_tokens(
    component: "contained-button"
)
```

For components with variants, query the exact variant when possible. For example, use `contained-button`, `flat-button`, `outlined-button`, or `fab-button` instead of generic `button` before calling `create_component_theme`.

Example components to query: `"contained-button"`, `"input"`, `"checkbox"`, `"dialog"`, `"grid"`, `"combo"`, `"tabs"`, `"card"`, `"chip"`, `"badge"`, `"snackbar"`.

The tool returns token names and guidance. Use only valid token names returned by the tool.

## Creating a Component Theme

Generate a ready-to-use CSS block that overrides specific design tokens for a component:

```
create_component_theme(
    component: "contained-button",
    platform: "blazor",
    output: "css",
    tokens: {
        "background": "var(--ig-primary-500)",
        "hover-background": "var(--ig-primary-600)"
    }
)
```

Use `tokens`, not `overrides`. Prefer palette token references such as `var(--ig-primary-500)` over raw hex values after a palette is established.

Copy the returned CSS into your application's CSS file. Scope it to a container class if you want per-instance styling.

## Creating a Color Palette

Generate a complete Ignite UI palette from seed colors:

```
create_palette(
    platform: "blazor",
    output: "css",
    primary: "#3f51b5",
    secondary: "#e91e63",
    surface: "#ffffff",
    variant: "light"
)
```

The tool returns CSS custom properties for palette shades and semantic colors, such as `--ig-primary-500`, `--ig-secondary-300`, and `--ig-surface-500`.

Add the returned CSS to your app CSS loaded after the Ignite UI theme CSS.

### Custom Palette with Explicit Colors

When you need full control over palette shades, use `create_custom_palette` with explicit monochromatic shade maps:

```
create_custom_palette(
    platform: "blazor",
    output: "css",
    variant: "light",
    primary: {
        mode: "explicit",
        shades: {
            "50": "#e8eaf6",
            "100": "#c5cae9",
            "200": "#9fa8da",
            "300": "#7986cb",
            "400": "#5c6bc0",
            "500": "#3f51b5",
            "600": "#3949ab",
            "700": "#303f9f",
            "800": "#283593",
            "900": "#1a237e",
            "A100": "#8c9eff",
            "A200": "#536dfe",
            "A400": "#3d5afe",
            "A700": "#304ffe"
        }
    },
    secondary: { mode: "shades", baseColor: "#e91e63" },
    surface: { mode: "shades", baseColor: "#ffffff" }
)
```

## Reading Palette Colors

Look up a palette color reference by family and shade:

```
get_color(
    color: "primary",
    variant: "600"
)
```

Returns a CSS variable reference such as `var(--ig-primary-600)`. Use `contrast: true` when you need the matching text color token.

## Global Token Generators

### Roundness

```
set_roundness(
    platform: "blazor",
    output: "css",
    radiusFactor: 0.5
)
```

`radiusFactor` is `0` (square) to `1` (maximum roundness). The tool returns CSS custom property declarations.

### Spacing

```
set_spacing(
    platform: "blazor",
    output: "css",
    spacing: 1.25
)
```

`spacing` is a multiplier. Use `0.75` for tighter spacing or `1.25` for more generous spacing.

### Size

```
set_size(
    platform: "blazor",
    output: "css",
    size: "small"
)
```

Values are `"small"`, `"medium"`, and `"large"`.

## Applying Generated CSS

All CSS generated by the MCP theming tools should be placed in your app's main CSS file, such as `wwwroot/css/app.css`:

```css
/* wwwroot/css/app.css */

/* 1. Palette overrides */
:root {
    /* paste palette output here */
}

/* 2. Global token overrides */
:root {
    /* paste roundness/spacing/size output here */
}

/* 3. Component-specific overrides (global scope) */
igc-button {
    /* paste component theme output here */
}

/* 4. Scoped component overrides */
.my-custom-section igc-input {
    /* paste scoped component theme output here */
}
```

Make sure `app.css` is loaded **after** the Ignite UI theme CSS in the host page so overrides take effect:

```html
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />
```

## Key Rules

1. **Always call `get_component_design_tokens` before writing component CSS overrides.** Token names are not guessable from component names.
2. **Use `platform: "blazor"` consistently on tools that accept a platform parameter.**
3. **Use `output: "css"` for Blazor app CSS.** Blazor theming is CSS-only; do not use Sass mixins in Blazor projects.
4. **Place all generated CSS in a file loaded after the Ignite UI theme CSS.** CSS cascade order matters.
5. **Palette CSS goes in `:root`.** Component theme CSS goes on the generated `igc-` tag selector or a scoped wrapper selector.
6. **Use `tokens` with `create_component_theme`.** Do not use an `overrides` object.
7. **Use `radiusFactor`, `spacing`, and `size: "small" | "medium" | "large"`** for global layout token tools.
