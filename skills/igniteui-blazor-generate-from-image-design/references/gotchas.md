# Ignite UI Blazor Gotchas & Pitfalls

Pitfalls that hit when matching a design image. Verify exact APIs with `get_doc` / `get_api_reference` - this file only records what the docs and tools won't tell you.

## CSS isolation & scoping

Ignite UI components render as web components (`igc-chip`, `igc-grid`, ...). Three mechanisms, in priority order:

| Mechanism | Purpose | When |
|---|---|---|
| Design tokens `--ig-*` | Colors, borders, shadows via CSS vars | **Primary** - whenever `get_component_design_tokens` lists one |
| `::part()` | Target a named shadow DOM part | Only when no token covers it; confirm part names via `get_doc` |
| `::deep` | Pierce Blazor CSS isolation in `.razor.css` | File helper - required for `igc-*` selectors in isolation files, never in global CSS |

- `::deep` works **only** on `igc-*` selectors (including `::deep igc-chip::part(base)` combinations). Plain HTML/class selectors in `.razor.css` are already scoped - no `::deep`. It also cannot target a component's own root element, and never belongs on `:root {}` blocks.
- `create_component_theme(platform: "blazor", output: "css")` emits global selectors - use as-is in `app.css`, add `::deep` when pasting into a `.razor.css` file.
- Blazor projects do **not** use Sass/SCSS.

## Chart fidelity

- **Match the curve shape**: smooth flowing curves → `Spline`/`SplineArea`; angular → `Line`/`Area`; steps → `StepLine`/`StepArea`. Defaulting to `Line` when the image shows smooth curves is the most common chart fidelity mistake.
- **Markers show by default** on category charts. If the image has none, set `MarkerTypes` to `None` (add the enum value after first render via `@ref` - it is a collection, not an attribute).
- **DV components ignore the CSS theme.** Charts, maps, gauges, and sparklines take colors only via parameters (`Brushes`, `Outlines`, `MarkerBrushes`, axis text colors) with resolved color values - `var()` references don't work reliably in DV parameters.
- Brush-list parameters are space-separated **strings** (`Brushes="#FF6B6B #4ECDC4"`); `IncludedProperties`/`ExcludedProperties` are **string arrays** (`@(new string[] { ... })`).
- `AreaFillOpacity` exists on `IgbCategoryChart`, **not** on `IgbSparkline` (use `Brush` there).
- `IgbSparkline` supports only `Line`, `Area`, `Column`, `WinLoss` - no smooth types. For a smooth mountain-shaped mini chart, use a small `IgbCategoryChart` with `ChartType="CategoryChartType.SplineArea"`, transparent axis label colors, and a fixed small height.
- `InnerExtent` (donut hole size) belongs on the **chart** (`IgbDoughnutChart`/`IgbPieChart`) - placing it on `IgbRingSeries` throws at runtime.
- **Circular ring with a centered value**: thick static ring, no needle → `IgbDoughnutChart` + `IgbRingSeries` (label overlaid with `position: absolute` in a `position: relative` wrapper). Thin animated spinner → `IgbCircularProgress`. Needle on a scale arc → `IgbRadialGauge`. A radial gauge or progress spinner never produces a clean static ring.
- **Charts collapse inside CSS Grid**: set `min-height: 0` on the grid cell and `Height="100%"` on the chart.
- Set visual parameters **inline in markup**, never via `@ref` assignment in `OnAfterRenderAsync` (BL0005 warning).

## Layout & components

- **Icons in slots**: always `IgbIcon` (self-centering `inline-flex`), never `<span class="material-icons">` (drifts to the top of the slot's flex container).
- **Icons render blank until registered**: the library bundles no icons - every `IconName`/`Collection` pair the view uses (including `IgbIconButton`, and doc examples like the nav drawer that omit the registration code) must be registered with **real SVG markup** via `RegisterIconFromTextAsync(name, svgText, collection)` after `await EnsureReady()` in `OnAfterRenderAsync(firstRender)`. Registration is app-wide - register all icon names once through a single `IgbIcon` in the layout (full pattern in the `igniteui-blazor-components` skill). Never invent SVG path data; copy genuine Material icon SVGs, or load by URL with `RegisterIconAsync`.
- **`IgbNavDrawer` is always an overlay**: `::part(base)` is `position: fixed` and the host has zero layout width; there is no `pinned` parameter. For a pinned sidebar, override in **global CSS** (host width, `::part(base) { position: relative; transform: none; }`, hide `::part(overlay)`) and strip the `inert` attribute via JS - or size the shell with CSS Grid (`grid-template-columns: 260px 1fr`).
- **Grids need explicit height** or they render every row unvirtualized.
- **Module registration**: a missing `typeof(IgbXxxModule)` in `Program.cs` makes that component silently render nothing - the most common "blank region" cause.

## Dark themes

- Switch the theme link to the `dark/` variant; if the view has a full-featured grid, switch the grid stylesheet (`themes/grid/dark/...`) to the same variant; `IgbGridLite` swaps its own single stylesheet instead.
- Multiple dark surface depths (sidebar darker than content): define semantic tokens (`--surface-1: var(--ig-gray-900)` etc.) in `:root` and use those in layout CSS - don't hardcode hex values.
- Grid headers/rows often need explicit `--ig-grid-*-background` token overrides to match the design's surface hierarchy.
- Map tiles (OSM) are light-only - approximate dark maps with a CSS filter on the container (`filter: grayscale(0.8) brightness(0.6)`, tuned to the design).

## Razor build traps

- **RZ9986**: complex attribute content on components fails to build - `class="chip @(cond ? "a" : "")"` is an error on any `Igb*` component. Make the whole attribute a single C# expression or helper method call.
- **CS1012**: single-quoted paths inside a double-quoted attribute lambda (`@onclick="() => Go('/dash')"`) parse as char literals. Use named handler methods.
- Parameters are PascalCase (`DataSource`, `ChartType`) - not Angular-style bindings; templates use `<Template>` with `context`, not `<ng-template>`.
