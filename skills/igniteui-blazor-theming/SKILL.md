---
name: igniteui-blazor-theming
description: "Generates and customizes Ignite UI for Blazor themes including color palettes, typography, elevations, and component-level styles using CSS custom properties and the igniteui-theming MCP server. Use when users ask to theme, restyle, or style Ignite UI components, change colors or the color palette, switch between light and dark themes, create or apply a global theme, customize typography or elevation shadows, adjust spacing, sizing, or roundness, or configure per-component design tokens. Do NOT use for component behavior, APIs, or data binding — use igniteui-blazor-components or igniteui-blazor-grids instead."
user-invocable: true
---

# Ignite UI for Blazor — Theming Skill

## Description

This skill teaches AI agents how to theme Ignite UI for Blazor applications using **CSS custom properties**, pre-built CSS themes, and the **Ignite UI Theming MCP server**. It covers global themes, palettes, typography, elevations, component-level theming, layout controls (spacing, sizing, roundness), and how to use the MCP tools for code generation.

> **Key difference from Angular:** Blazor projects do **not** use Sass. All theming is done via pre-built CSS files and CSS custom property overrides. The same underlying CSS custom properties (`--ig-primary-500`, `--ig-surface-500`, etc.) are used across Angular, Blazor, React, and Web Components.

---

## Prerequisites

- A Blazor project (.NET 8+, 9+, or 10+) with the `IgniteUI.Blazor` or `IgniteUI.Blazor.Lite` NuGet package installed
- A CSS theme file linked in the host page (`wwwroot/index.html`, `Pages/_Host.cshtml`, or `App.razor`)
- The **Ignite UI Theming MCP server** (`igniteui-theming`) available as a tool provider

> **AGENT INSTRUCTION — MCP Server Setup (REQUIRED)**
>
> Before using any theming tools, you MUST verify the MCP server is available
> by calling `detect_platform`. If the tool is not available or the call fails,
> you MUST automatically configure it by following the setup process steps described below.
>
> After writing the file, inform the user that the MCP server has been
> configured and they may need to reload the editor for the tools to activate.
> Start the MCP server and wait for that MCP server to start before continuing the theming related work.
> Do NOT skip this step or ask the user to do it manually.

---

## Setting Up the Theming MCP Server

> **Full setup instructions for VS Code, Cursor, Claude Desktop, and JetBrains IDEs are in [`references/mcp-setup.md`](./references/mcp-setup.md).** Read that file for editor-specific configuration steps and verification.

---

## Theming Architecture

> **Docs:** [Blazor Theming Overview](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/themes/overview)

The Ignite UI theming system is built on four pillars:

| Concept        | Purpose                                                                                                                               |
| -------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| **Palette**    | Color system with primary, secondary, surface, gray, info, success, warn, error families, each with shades 50–900 + accents A100–A700 |
| **Typography** | Font family, type scale (h1–h6, subtitle, body, button, caption, overline)                                                            |
| **Elevations** | Box-shadow levels 0–24 for visual depth                                                                                               |
| **Schema**     | Per-component recipes mapping palette colors to component tokens                                                                      |

### Design Systems

Four built-in design systems are available:

- **Material** (default) — Material Design 3
- **Bootstrap** — Bootstrap-inspired
- **Fluent** — Microsoft Fluent Design
- **Indigo** — Infragistics Indigo Design

Each has light and dark variants, delivered as pre-built CSS files.

---

## Pre-built Themes

The quickest way to theme a Blazor app is to include a pre-built CSS file in your host page.

**Blazor WebAssembly** — `wwwroot/index.html`:

```html
<head>
    <link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
</head>
```

**Blazor Server / Interactive Server** — `Pages/_Host.cshtml` or `App.razor`:

```html
<head>
    <link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
</head>
```

### Available Pre-built CSS Files

| Path                                                 | Theme           |
| ---------------------------------------------------- | --------------- |
| `_content/IgniteUI.Blazor/themes/light/material.css` | Material Light  |
| `_content/IgniteUI.Blazor/themes/dark/material.css`  | Material Dark   |
| `_content/IgniteUI.Blazor/themes/light/fluent.css`   | Fluent Light    |
| `_content/IgniteUI.Blazor/themes/dark/fluent.css`    | Fluent Dark     |
| `_content/IgniteUI.Blazor/themes/light/bootstrap.css`| Bootstrap Light |
| `_content/IgniteUI.Blazor/themes/dark/bootstrap.css` | Bootstrap Dark  |
| `_content/IgniteUI.Blazor/themes/light/indigo.css`   | Indigo Light    |
| `_content/IgniteUI.Blazor/themes/dark/indigo.css`    | Indigo Dark     |

> **Package note:** Both `IgniteUI.Blazor` (licensed/trial) and `IgniteUI.Blazor.Lite` (open-source) use the same `_content/IgniteUI.Blazor/themes/` path for CSS files.

---

## Custom Theme via CSS Custom Properties

Unlike Angular (which uses Sass), Blazor projects customize themes by overriding **CSS custom properties** on `:root` or a scoped selector. The pre-built CSS files define all custom properties; you override only the ones you want to change.

### Overriding the Palette

Set the HSL components for each color family on `:root`:

```css
:root {
  /* Primary color */
  --ig-primary-h: 210;
  --ig-primary-s: 79%;
  --ig-primary-l: 46%;

  /* Secondary color */
  --ig-secondary-h: 36;
  --ig-secondary-s: 100%;
  --ig-secondary-l: 50%;

  /* Surface color */
  --ig-surface-h: 0;
  --ig-surface-s: 0%;
  --ig-surface-l: 98%;

  /* Gray */
  --ig-gray-h: 0;
  --ig-gray-s: 0%;
  --ig-gray-l: 62%;

  /* Semantic colors */
  --ig-info-h: 190;
  --ig-info-s: 90%;
  --ig-info-l: 37%;

  --ig-success-h: 153;
  --ig-success-s: 60%;
  --ig-success-l: 53%;

  --ig-warn-h: 45;
  --ig-warn-s: 100%;
  --ig-warn-l: 51%;

  --ig-error-h: 0;
  --ig-error-s: 100%;
  --ig-error-l: 42%;
}
```

The theming engine automatically generates all shades (50–900, A100–A700) and contrast colors from these HSL base values.

### Where to Place Custom CSS

Place your CSS overrides in one of these locations:

1. **Global CSS file** — `wwwroot/css/app.css` (Blazor WASM) or `wwwroot/css/site.css` (Blazor Server), linked in your host page after the theme CSS
2. **Inline `<style>` block** — in `wwwroot/index.html`, `_Host.cshtml`, or `App.razor`
3. **CSS isolation file** — `Component.razor.css` for component-scoped overrides (see [Common Patterns](./references/common-patterns.md))

---

## Component-Level Theming

Override individual component appearance using CSS custom properties scoped to the component's element selector.

> **AGENT INSTRUCTION — No Hardcoded Colors (CRITICAL)**
>
> Once a palette has been generated (via CSS custom property overrides or `create_palette` / `create_theme` via MCP),
> **every color reference MUST come from the generated palette tokens** — never hardcode hex/RGB/HSL values.
>
> Use `var(--ig-primary-500)`, `var(--ig-secondary-300)`, `var(--ig-surface-500)`, etc. in CSS,
> or the `get_color` MCP tool to obtain the correct token reference.
>
> **WRONG** (hardcoded hex — breaks theme switching, ignores the palette):
>
> ```css
> igc-avatar {
>   --ig-avatar-background: #e91e63;
>   --ig-avatar-color: #ffffff;
> }
> ```
>
> **RIGHT** (palette token — stays in sync with the theme):
>
> ```css
> igc-avatar {
>   --ig-avatar-background: var(--ig-primary-500);
>   --ig-avatar-color: var(--ig-primary-500-contrast);
> }
> ```
>
> This applies to **all** style code: component theme overrides, custom CSS rules, CSS variables used
> for borders/backgrounds/text, and inline styles.
> The only place raw hex values belong is the **initial palette seed** (HSL values on `:root` or `create_palette` / `create_theme` MCP tool inputs).
> Everything downstream must reference the palette.

### Example: Customizing a Component

```css
/* Override avatar colors using palette tokens */
igc-avatar {
  --ig-avatar-background: var(--ig-primary-500);
  --ig-avatar-color: var(--ig-primary-500-contrast);
}

/* Override button colors */
igc-button {
  --ig-button-foreground: var(--ig-secondary-500-contrast);
  --ig-button-background: var(--ig-secondary-500);
  --ig-button-hover-background: var(--ig-secondary-600);
}

/* Override grid header */
igc-grid {
  --ig-grid-header-background: var(--ig-primary-50);
  --ig-grid-header-text-color: var(--ig-primary-800);
}
```

### Discovering Available Tokens

Use the `get_component_design_tokens` MCP tool to discover all available design tokens for a component:

```
Tool: get_component_design_tokens
Params: { component: "avatar" }
```

This returns the full list of CSS custom properties that can be overridden for each component. **Always call this tool before writing component-level theme overrides** — do not guess token names from memory.

### Compound Components

Some components (e.g., grid, combo, date picker) are composed of multiple inner components. The `get_component_design_tokens` tool will return tokens for all child components in a hierarchical list. You must theme each sub-component with its own scoped selector:

```css
/* Grid has its own tokens plus child components like header, row, cell */
igc-grid {
  --ig-grid-header-background: var(--ig-primary-50);
  --ig-grid-content-background: var(--ig-surface-500);
  --ig-grid-row-hover-background: var(--ig-primary-100);
}
```

---

## Layout Controls

### Sizing

> **Docs:** [Display Density / Sizing](https://www.infragistics.com/products/ignite-ui-blazor/blazor/components/general-display-density)

Controls the size of components via `--ig-size` (values: 1 = small, 2 = medium, 3 = large):

```css
/* Global */
:root {
  --ig-size: 2;
}

/* Component-scoped */
igc-grid {
  --ig-size: 1;
}
```

### Spacing

Controls internal padding via `--ig-spacing` (1 = default, 0.5 = compact, 2 = spacious):

```css
:root {
  --ig-spacing: 1;
}
.compact-section {
  --ig-spacing: 0.75;
}
```

### Roundness

Controls border-radius via `--ig-radius-factor` (0 = square, 1 = maximum radius):

```css
:root {
  --ig-radius-factor: 1;
}
igc-avatar {
  --ig-radius-factor: 0.5;
}
```

---

## Using the Theming MCP Server

The Ignite UI Theming MCP server provides tools for AI-assisted theme code generation.

> **IMPORTANT — File Safety Rule**: When generating or updating theme code, **never overwrite existing style files directly**. Instead, always **propose the changes as an update** and let the user review and approve before writing to disk. If a CSS file already exists, show the generated code as a diff or suggestion rather than replacing the file contents. This prevents accidental loss of custom styles the user has already written.

Always follow this workflow:

### Step 1 — Detect Platform

```
Tool: detect_platform
```
This auto-detects the platform from the project files (`.csproj`, `package.json`). For Blazor projects it will return `blazor` or `web-components`.

### Step 2 — Generate a Full Theme

```
Tool: create_theme
Params: {
  platform: "blazor",
  designSystem: "bootstrap",
  primaryColor: "#1976D2",
  secondaryColor: "#FF9800",
  surfaceColor: "#FAFAFA",
  variant: "light",
  fontFamily: "'Roboto', sans-serif",
  includeTypography: true,
  includeElevations: true
}
```

Generates a complete CSS file with palette custom property overrides, typography, and elevation definitions. For Blazor, the output is pure CSS (no Sass) that you add to your project.

> **Platform parameter:** Use `platform: "blazor"` or `platform: "web-components"` — both produce the same CSS custom property output. Do not use `platform: "angular"` for Blazor projects.

### Step 3 — Customize Individual Components

```
Tool: get_component_design_tokens
Params: { component: "grid" }
```

Then use **palette token references** (not hardcoded hex values) for every color:

```
Tool: create_component_theme
Params: {
  platform: "blazor",
  designSystem: "bootstrap",
  variant: "light",
  component: "grid",
  tokens: {
    "header-background": "var(--ig-primary-50)",
    "header-text-color": "var(--ig-primary-800)"
  }
}
```

> **Reminder**: After a palette is generated, all token values passed to
> `create_component_theme` must reference palette CSS custom properties
> (e.g., `var(--ig-primary-500)`, `var(--ig-secondary-A200)`,
> `var(--ig-gray-100)`). Never pass raw hex values like `"#E3F2FD"`.

### Step 4 — Generate a Palette

For simple mid-luminance base colors:

```
Tool: create_palette
Params: {
  platform: "blazor",
  primary: "#1976D2",
  secondary: "#FF9800",
  surface: "#FAFAFA",
  variant: "light"
}
```

For brand-specific exact shade values, use `create_custom_palette` with `mode: "explicit"` for full control over each shade.

### Step 5 — Adjust Layout

```
Tool: set_size     → { size: "medium" }
Tool: set_spacing  → { spacing: 0.75, component: "grid" }
Tool: set_roundness → { radiusFactor: 0.8 }
```

### Step 6 — Reference Palette Colors (MANDATORY for All Color Usage)

After a palette is generated, **always** use the `get_color` tool to obtain the correct CSS custom property reference. Never hardcode hex/RGB/HSL values in component themes or custom CSS.

```
Tool: get_color
Params: { color: "primary", variant: "600" }
→ var(--ig-primary-600)

Params: { color: "primary", variant: "600", contrast: true }
→ var(--ig-primary-600-contrast)

Params: { color: "primary", opacity: 0.5 }
→ hsl(from var(--ig-primary-500) h s l / 0.5)
```

Use these token references everywhere:
- Component theme `tokens` values
- Custom CSS rules (`color`, `background`, `border-color`, `fill`, `stroke`, etc.)
- CSS variables for derived values (`--sidebar-bg: var(--ig-surface-500);`)

The **only** place raw hex values are acceptable is in the initial palette seed (the HSL values on `:root` or the `create_palette` / `create_theme` MCP tool inputs that seed the color system).

### Loading Reference Data

Use `read_resource` with these URIs for preset values and documentation:

| URI                               | Content                            |
| --------------------------------- | ---------------------------------- |
| `theming://presets/palettes`      | Preset palette colors              |
| `theming://presets/typography`    | Typography presets                 |
| `theming://presets/elevations`    | Elevation shadow presets           |
| `theming://guidance/colors/usage` | Which shades for which purpose     |
| `theming://guidance/colors/roles` | Semantic color roles               |
| `theming://guidance/colors/rules` | Light/dark theme rules             |
| `theming://platforms/blazor`      | Blazor platform specifics          |

---

## Referencing Colors in Custom Styles

After a theme is applied, the palette is available as CSS custom properties on `:root`. Use these tokens in all custom CSS — never introduce standalone hex/RGB variables for colors that the palette already provides.

### Correct: Palette Tokens

```css
/* All colors come from the theme — respects palette changes and dark/light switching */
.sidebar {
  background: var(--ig-surface-500);
  color: var(--ig-gray-900);
  border-right: 1px solid var(--ig-gray-200);
}

.accent-badge {
  background: var(--ig-secondary-500);
  color: var(--ig-secondary-500-contrast);
}

.hero-section {
  /* Semi-transparent primary overlay */
  background: hsl(from var(--ig-primary-500) h s l / 0.12);
}
```

### Incorrect: Hardcoded Values

```css
/* WRONG — these break when the palette changes and ignore dark/light mode */
.sidebar {
  background: #f0f5fa; /* ✗ not a palette token */
  color: #333; /* ✗ not a palette token */
}

.accent-badge {
  background: #3d5afe; /* ✗ hardcoded */
  color: #ffffff; /* ✗ hardcoded */
}
```

### When Raw Hex Values Are OK

Raw hex values are acceptable **only** in these contexts:

1. **Initial palette seed** — the HSL base values on `:root` that generate the full palette
2. **`create_palette` / `create_theme` MCP tool inputs** — the base colors passed to the tool
3. **Non-palette decorative values** — e.g., a one-off SVG illustration color that intentionally stays fixed regardless of theme

Everything else must use `var(--ig-<family>-<shade>)` tokens.

---

## Common Patterns

> **Light/dark theme switching, scoped themes, CSS isolation, and package configuration are in [`references/common-patterns.md`](./references/common-patterns.md).** Read that file for ready-to-use CSS patterns applicable to Blazor.

---

## Key Rules

1. **Never overwrite existing files directly** — always propose theme code as an update for user review; do not replace existing style files without confirmation
2. **Always call `detect_platform` first** when using MCP tools
3. **Always call `get_component_design_tokens` before `create_component_theme`** to discover valid token names
4. **Palette shades 50 = lightest, 900 = darkest** for all chromatic colors — never invert for dark themes (only gray inverts)
5. **Surface color must match the variant** — light color for `light`, dark color for `dark`
6. **No Sass in Blazor** — all theming is done via CSS custom properties and pre-built CSS files; never generate Sass code for a Blazor project
7. **Component themes use CSS custom properties on element selectors** — scope overrides to web component selectors like `igc-avatar`, `igc-grid`, `igc-button`, etc.
8. **For compound components**, follow the full checklist returned by `get_component_design_tokens` — theme each child component with its scoped selector
9. **Never hardcode colors after palette generation** — once a palette is created, every color in component themes and custom CSS must use `var(--ig-<family>-<shade>)` palette tokens (e.g., `var(--ig-primary-500)`, `var(--ig-gray-200)`). Raw hex/RGB/HSL values are only acceptable in the initial palette seed. This ensures themes remain consistent, switchable (light/dark), and maintainable
10. **Use `platform: "blazor"` or `platform: "web-components"`** in MCP tool calls — never use `platform: "angular"` for Blazor projects

---

## Related Skills

- [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) — UI components (form controls, layout, data display, feedback/overlays, charts)
- [`igniteui-blazor-grids`](../igniteui-blazor-grids/SKILL.md) — Data Grids (Flat Grid, Tree Grid, Hierarchical Grid, Pivot Grid)
