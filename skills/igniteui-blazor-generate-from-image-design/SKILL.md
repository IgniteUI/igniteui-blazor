---
name: igniteui-blazor-generate-from-image-design
description: "Implement a Blazor view from a design image, screenshot, mockup, or wireframe using Ignite UI for Blazor components — analyze the image, map regions to components, generate a matching theme, build the view with mock data, and refine until it matches. Triggers when a design image is provided along with a request to implement this design, build this UI, convert this mockup, or create a page from this image in an Ignite UI Blazor project."
user-invocable: true
---

# Building an Ignite UI Blazor View from a Design Image

## Order of work

Do not start writing markup until steps 1–4 are done. The two reference files are short and the failure mode they prevent — a view that compiles but looks wrong and uses APIs that do not exist — costs far more than reading them.

1. **Analyze the image** into regions (below).
2. **Read [`references/component-mapping.md`](references/component-mapping.md)** and pick a component per region.
3. **Look up each chosen component** — `get_doc(framework: "blazor", name: "<doc name>")` if the `igniteui-cli` MCP server is available, otherwise the [`igniteui-blazor-components`](../igniteui-blazor-components/SKILL.md) and [`igniteui-blazor-grids`](../igniteui-blazor-grids/SKILL.md) reference files. Use the doc `name` from `list_components` results, not the display title.
4. **Read [`references/gotchas.md`](references/gotchas.md) in full** and check every entry against your component list. Chart, CSS-scoping, and Razor-syntax entries apply broadly, not only to the components they name.
5. **Theme**, **implement**, **refine**, **validate** — steps 3–6 below.

Everything works without the MCP servers; you lose per-component doc verification and generated palette/token CSS, so lean on the skills' reference files and say what you could not verify.

## Step 1 — Analyze the image

Produce a decomposition table before writing anything:

| Region | Visual role | Candidate component | Custom CSS needed | Data |
|---|---|---|---|---|
| sidebar item list | icon + label rows | `IgbNavDrawer` (Position=Relative) | yes — width, item height | nav model |
| top bar | brand + tabs + search | `IgbNavbar` | yes — multi-zone flex | n/a |
| KPI row | four stat tiles | plain HTML in CSS Grid | yes | domain records |

For each region record: layout structure and relative proportions, component type, colors (primary, secondary, surface, accent, text), typography, surface treatment (border, radius, shadow, dividers), spacing scale, and what mock data it needs.

Then translate the whole image into CSS Grid rows and columns. Get desktop proportions right first, add breakpoint stacking after. Do not chase exact pixel values at this stage — you will tune them in step 5.

Start every region with the most appropriate Ignite UI component. Fall back to plain semantic HTML only when the component's DOM structure genuinely cannot reach the design after CSS overrides, and note why in a comment. Finish with a short brief: chosen components, HTML fallbacks, theme strategy, package needs, assumptions.

## Step 2 — Package check

| Need | Package |
|---|---|
| Core UI: navbar, drawer, list, card, inputs, chips, tabs, tile manager | `IgniteUI.Blazor.Lite` |
| Lightweight read-only grid | `IgniteUI.Blazor.GridLite` |
| Charts, maps, gauges, sparklines, full grids, Dock Manager | `IgniteUI.Blazor` (licensed) or `IgniteUI.Blazor.Trial` |

There are no separate DV packages. If a required package is missing from the project, identify the right package and version and **ask before editing the `.csproj`**.

Register every `Igb*Module` you use in `Program.cs`, add `@using IgniteUI.Blazor.Controls` to `_Imports.razor`, and confirm the theme stylesheet and `app.bundle.js` are in the host page — see the components skill's [`setup.md`](../igniteui-blazor-components/references/setup.md).

## Step 3 — Theme

The [`igniteui-blazor-theming`](../igniteui-blazor-theming/SKILL.md) skill owns theming rules. What is specific to working from an image:

**Guard first.** Inspect the host page, `wwwroot/css/app.css` / `site.css`, and any existing theme file for a theme `<link>` or `:root` palette overrides.

- **A theme already exists** → the global palette is set. Do not call `create_palette` unless the user explicitly wants to change it globally. Reuse the existing design system, variant, and tokens where they already match the image, and add only scoped per-component overrides.
- **No theme** → generate one.

**Generating one — MCP guidance before image extraction.** Read `theming://guidance/colors/rules` first so you know which slots exist and what the luminance constraints are; only then pull values out of the image for the slots that actually exist. Resolve the design system from the workspace, an explicit request, or the closest visual match — do not assume.

```
create_palette(platform: "blazor", output: "css",
               primary: "<from image>", secondary: "<from image>",
               surface: "<from image>", variant: "<light|dark>")
```

Use `create_palette` for a coherent small color system; `create_custom_palette` when the design has several surface depths or accent families. Act on luminance warnings. For extra surface depths a single generated surface cannot cover, define semantic variables (`--surface-1`, `--surface-2`) in global CSS.

**Per component, MCP before image again.** For every **core** component: call `get_component_design_tokens(component)`, read the token list, *then* zoom into that region of the image and read the value for each relevant token, then `create_component_theme(component, platform: "blazor", output: "css", tokens: …)` passing only tokens that differ from the global theme.

Query the exact variant (`contained-button`, not `button`). Skip this loop entirely for charts, maps, gauges, and sparklines — they have no design tokens; style them through component parameters. Skip it for regions built from plain HTML.

Place the generated CSS in the global stylesheet as-is, or add `::deep` to each `igc-*` selector in a `.razor.css` file.

## Step 4 — Implement

- **Layout:** Ignite UI components for standard regions, CSS Grid/Flexbox and token overrides for the rest.
- **View:** a `.razor` file plus a matching `.razor.css`. Keep layout, spacing, typography, and surface styling in the isolation file, not in `style=""` attributes.
- **Code:** `@code { }` at the end for small components; a `.razor.cs` code-behind for complex ones.
- **Data:** typed C# `record`s or classes matching the density and domain the image shows. Avoid `Item 1`, `Lorem ipsum` placeholders when the image shows real domain content.
- **Bindings:** `[Parameter]`, `EventCallback<T>`, `@ref`, `@bind-Value` / `@bind-Checked`, `@inject`. Templates are `<Template>`-style render fragments with a `context` parameter — never Angular's `<ng-template>`.

Preserve spacing, hierarchy, and data density before adding interactivity. Document assumptions where the image is ambiguous rather than silently guessing.

## Step 5 — Refine

Close the gap with the image using `set_size`, `set_spacing`, and `set_roundness`, plus layout CSS. Work biggest-first: panel proportions, chart shape and curve type, control density, legend placement, button prominence — then row heights and inter-region spacing.

## Step 6 — Validate

1. `dotnet build` — fix every C# and Razor error before moving on.
2. Run tests if the project has them.
3. `dotnet run` / `dotnet watch`.
4. Compare against the image: panel proportions, control density, chart shape, legend placement, button prominence, row heights, region spacing.
5. Adjust and repeat.

In a terminal-only environment the user does the visual comparison — ask for feedback rather than declaring a match. Only check visually yourself when browser and screenshot tools are available.

Typical second-pass fixes: chart curve type and data density, marker visibility, layout ratios, navigation mode or panel chrome, map filter treatment, dark-surface hierarchy, and sections of the image overlooked on the first read.
