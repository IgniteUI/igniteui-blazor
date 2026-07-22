---
name: igniteui-blazor-generate-from-image-design
description: Implement Blazor application views from design images using Ignite UI Blazor components. Uses the igniteui-cli and igniteui-theming MCP servers to discover components, look up docs, and generate themes. Triggers when the user provides a design image (screenshot, mockup, wireframe) and wants it built as a working Blazor view, or asks to "implement this design", "build this UI", "convert this mockup", or "create a page from this image" in an Ignite UI Blazor project.
user-invocable: true
---

# Implementing Ignite UI Blazor Views from Design Images

This is a workflow skill. Component facts come from the `igniteui-cli` MCP server, theme CSS from the `igniteui-theming` MCP server (both described in the `igniteui-blazor-components` and `igniteui-blazor-theming` skills, including config fallback). Read [references/component-mapping.md](references/component-mapping.md) before choosing components and [references/gotchas.md](references/gotchas.md) before writing code.

## Step 1 - Analyze the image

Identify every visible region: layout structure (rows/columns/sidebar proportions), component type, color palette, typography, surface styling (radius, shadows, dividers), spacing scale, and what mock data each region needs. Capture high-level structure and relative proportions - exact CSS is refined later against the running app.

Produce a short decomposition table (one row per region: visual role → candidate component → custom CSS needed → data type) and an implementation brief: components per region, plain-HTML fallback regions, theme strategy, package needs, assumptions. Start every region from the best-matching Ignite UI component in [references/component-mapping.md](references/component-mapping.md); fall back to semantic HTML only when a component's DOM is fundamentally incompatible after CSS overrides, and note why in a comment. Translate the layout into CSS Grid desktop-first, then add breakpoint stacking.

## Step 2 - Discover and read docs

- `list_components(framework: "blazor", filter: ...)` with narrow filters per UI pattern (`chart`, `list view`, `navbar`, `gauge`, `map`, `grid`).
- `get_doc(framework: "blazor", name: <doc name from the results>)` for **every** chosen component family before coding; `search_docs` for feature questions.
- `get_api_reference` / `search_api` for exact parameters and events.

Confirm package coverage: `IgniteUI.Blazor.Lite` (OSS) has **no** charts, maps, gauges, full grids, or Dock Manager - those need `IgniteUI.Blazor` (licensed) or `IgniteUI.Blazor.Trial`; the lightweight grid is `IgniteUI.Blazor.GridLite`. If the needed package is not referenced, ask before modifying the `.csproj`.

## Step 3 - Theme

Follow the `igniteui-blazor-theming` skill for all rules. In image order:

1. **Existing-app guard**: if the host page or global CSS already defines a theme/palette, reuse its tokens and apply only minimal scoped overrides - do not regenerate the global palette.
2. **New palette**: read `theming://guidance/colors/rules` first, resolve the design system and variant, extract seed colors from the image, then `create_palette(platform: "blazor", output: "css", ...)` (never `create_theme` - it outputs Sass). Optionally `create_typography` / `create_elevations`.
3. **Per-component tokens** (core UI components only): `get_component_design_tokens(component)` → read the image region for those token slots → `create_component_theme(..., tokens)` passing only values that differ from the global theme. Place output in global CSS as-is, or prefix selectors with `::deep` in `.razor.css` files.
4. Charts, maps, gauges, and sparklines have **no** CSS design tokens - skip token discovery and set their colors via component parameters (see gotchas).

## Step 4 - Implement

- `.razor` view + `.razor.css` isolation file; keep layout/spacing/typography in CSS, not inline styles. `@code` block for small components, `.razor.cs` code-behind for complex ones.
- Register every `Igb*` module in `Program.cs`; `@using IgniteUI.Blazor.Controls` in `_Imports.razor`; theme CSS + `app.bundle.js` in the host page.
- Register every icon name the view uses with real SVG content - the library bundles no icons, so unregistered `IconName`s render blank (see gotchas for the app-wide registration pattern).
- Typed C# mock data (records/classes) matching the design's domain and density - no generic placeholders when the image shows domain content.
- Use `<Template>` child elements with `context` (not `<ng-template>`), `@bind-` for two-way binding, `EventCallback<T>` for outputs.
- Preserve spacing, hierarchy, and data density before adding interactivity; note assumptions where the image is ambiguous.

## Step 5 - Refine and validate

Use `set_size`, `set_spacing`, and `set_roundness` to close global fidelity gaps, then iterate: `dotnet build` (fix errors immediately) → run → compare against the image → adjust. Check in order: panel proportions, control density, chart shape (spline vs line!), legend placement, button prominence, row heights, region spacing. In terminal-only environments the user does the visual comparison; act on their feedback. Re-check [references/gotchas.md](references/gotchas.md) whenever a chart, drawer, or themed component doesn't match.

## Related skills

- [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) - component setup and Blazor gotchas
- [`igniteui-blazor-grids`](../igniteui-blazor-grids/SKILL.md) - if the design contains a data table
- [`igniteui-blazor-theming`](../igniteui-blazor-theming/SKILL.md) - theming rules and MCP workflow
