# Gotchas — Mistakes That Compile But Look Wrong

Check every entry against your component list before writing code. Chart, CSS, and Razor entries apply broadly, not only to the components they name.

## Razor & Blazor

### RZ9986 — dynamic `class` on an Ignite UI component

Mixing literal text with `@(...)` in one attribute on a component fails the build with *"Component attributes do not support complex content"*. Make the whole value a single C# expression.

```razor
@* ❌ RZ9986 *@
<IgbChip class="chip @(item == _selected ? "chip-active" : "")" />

@* ✅ *@
<IgbChip class="@ChipClass(item)" />

@code { string ChipClass(Item i) => i == _selected ? "chip chip-active" : "chip"; }
```

### CS1012 — single quotes in an inline lambda

`@onclick="() => Navigate('/dashboard')"` fails: the single quotes are parsed as a C# `char` literal. Use a named handler.

```razor
@* ❌ *@ <IgbNavDrawerItem @onclick="() => Navigate('/dashboard')">
@* ✅ *@ <IgbNavDrawerItem @onclick="NavigateToDashboard">

@code { void NavigateToDashboard() => NavigationManager.NavigateTo("/dashboard"); }
```

### BL0005 — setting parameters through `@ref`

Assigning component parameters from `OnAfterRenderAsync` (`chart.Brushes = "…"`) raises *"Component parameter should not be set outside of its component"*. Pass them as inline markup attributes instead.

### Async vs sync methods

Every component method has an `XAsync()` form and a sync twin `X()`. **Use `Async`.** The sync twin requires `IJSInProcessRuntime` and throws `InvalidOperationException` on Blazor Server; it only works in WebAssembly and MAUI WebView.

### Other basics

- Parameters are PascalCase (`ChartType`, `DataSource`) — never Angular's `[chartType]`.
- Templates are Blazor render fragments with a cast `context`, not `<ng-template>`. On a grid column the cell template is `BodyTemplate`:
  ```razor
  <IgbColumn Field="Name" Header="Name">
      <BodyTemplate>
          @{ var cell = (IgbCellTemplateContext)context; }
          <strong>@cell.Cell.Value</strong>
      </BodyTemplate>
  </IgbColumn>
  ```
- `@bind-Value` / `@bind-Checked` — verify the bindable parameter exists rather than inventing one.
- Mock data is C# `record` or `class`, not a TypeScript interface.
- `AddIgniteUIBlazor()` must be called in `Program.cs`. Listing `typeof(Igb…Module)` values pre-loads those modules; in `IgniteUI.Blazor.Lite` components also self-register on first render.

## CSS scoping

Full rules are in the theming skill's [`common-patterns.md`](../../igniteui-blazor-theming/references/common-patterns.md). The short version:

| Mechanism | When |
|---|---|
| `--ig-*` design tokens | **primary** — whenever a token exists for the property |
| `::part(name)` | only when no token covers it, and only after confirming the part name |
| `::deep` | only in `.razor.css`, only on `igc-*` selectors |

```css
/* app.css — global, no ::deep */
igc-chip { --ig-chip-background: var(--ig-primary-500); }
igc-dialog::part(footer) { border-top: 1px solid var(--ig-gray-200); }

/* MyView.razor.css — ::deep on igc-*, never on your own class selectors */
::deep igc-chip { --ig-chip-background: var(--ig-primary-500); }
.dashboard-shell { display: grid; grid-template-columns: 260px 1fr; }
```

`::deep` does **not** work on the component's own root element — Blazor puts the scope attribute there, so there is no scoped parent above it. Never add `::deep` to a `:root {}` block. `create_component_theme` emits global CSS; use it verbatim in `app.css`. Blazor projects have no Sass step.

## Charts

### They ignore the CSS theme entirely

Charts, maps, gauges, and sparklines do not read `--ig-*` custom properties. Set colors through parameters, using resolved values rather than `var()` references:

```razor
<IgbCategoryChart Brushes="#4FC3F7 #81C784" Outlines="#4FC3F7 #81C784"
                  MarkerBrushes="#4FC3F7 #81C784"
                  XAxisLabelTextColor="#666666" YAxisMajorStroke="#EEEEEE" />
```

`Brushes`, `Outlines`, `MarkerBrushes`, `MarkerOutlines` are **space-separated strings**, not arrays.

### Curve type is the most visible fidelity mistake

Match `ChartType` to the shape in the image: smooth flowing → `Spline` / `SplineArea`; angular → `Line` / `Area`; stepped → `StepLine` / `StepArea`. Do not default to `Line` when the image shows curves.

### Markers appear at every point by default

If the image has no dots on the line, add `MarkerType.None` to `MarkerTypes` once the chart reference is ready.

### `IncludedProperties` / `ExcludedProperties` are `string[]`

```razor
<IgbCategoryChart IncludedProperties='@(new string[] { "Month", "Revenue" })' />
```

A plain string is a type mismatch. `XAxisLabel` formats labels — it does not select a data field.

### `InnerExtent` is a chart property, never a series property

```razor
@* ✅ *@ <IgbDoughnutChart InnerExtent="0.45"><IgbRingSeries … /></IgbDoughnutChart>
@* ❌ runtime crash: IgbRingSeries does not have a property matching 'InnerExtent' *@
<IgbRingSeries InnerExtent="0.45" />
```

### Choosing the right circular component

- Thick static ring, centered label, no needle → `IgbDoughnutChart` + `IgbRingSeries`, label absolutely positioned over it.
- Thin animated spinner → `IgbCircularProgress` (not a visualization).
- Needle on a scale arc → `IgbRadialGauge`.

Neither gauge nor progress will produce a clean static ring.

```razor
<div class="gauge-wrapper">
    <IgbDoughnutChart InnerExtent="0.62" Height="160px" Width="160px" AllowSliceExplosion="false">
        <IgbRingSeries DataSource="@GaugeData" ValueMemberPath="Value" LabelMemberPath="Category"
                       LabelsPosition="LabelsPosition.None"
                       Brushes="#5B57E8 #E8E8F5" Outlines="transparent transparent"
                       RadiusFactor="0.95" />
    </IgbDoughnutChart>
    <div class="gauge-label">75%</div>
</div>
```

```css
.gauge-wrapper { position: relative; display: inline-flex; align-items: center; justify-content: center; }
.gauge-label   { position: absolute; font-size: 1.9rem; font-weight: 700; }
```

### `IgbSparkline` has no smooth area

It supports only `Line`, `Area`, `Column`, `WinLoss`, and `Area` fills with angular polygons. For a smooth mountain-shaped micro-chart use a small `IgbCategoryChart` with `ChartType="CategoryChartType.SplineArea"` and `AreaFillOpacity` — `AreaFillOpacity` does not exist on `IgbSparkline`.

### Charts collapse to zero height inside CSS Grid

```css
.chart-container { min-height: 0; }   /* stops the grid track collapsing */
```
```razor
<IgbCategoryChart Height="100%" Width="100%" />
```

Charts never auto-size — always set `Width` and `Height`.

### Map series are declared, not added from code

```razor
<IgbGeographicMap Height="500px" Width="100%">
    <IgbGeographicSymbolSeries DataSource="Locations"
                               LatitudeMemberPath="Lat" LongitudeMemberPath="Lon"
                               MarkerType="MarkerType.Circle" MarkerBrush="#FF5722" />
</IgbGeographicMap>
```

OpenStreetMap tiles are light. For a dark design, filter the container: `filter: grayscale(0.8) brightness(0.6);` — tune to match the image.

## Components

### `IgbNavDrawer` — use `Position="Relative"` for a pinned sidebar

With `Start`/`End`/`Top`/`Bottom` the panel is `position: fixed`, floats over the content behind a dimming overlay, and the host contributes `width: 0` to the layout. There is no `Pin` parameter — but **`NavDrawerPosition.Relative` is the built-in pinned mode**: the panel becomes in-flow, the overlay is hidden, and closing slides it out by a negative margin.

```razor
<div class="app-shell">
    <IgbNavDrawer Open="true" Position="NavDrawerPosition.Relative">…</IgbNavDrawer>
    <main>@Body</main>
</div>
```
```css
igc-nav-drawer { --menu-full-width: 260px; --menu-mini-width: 60px; }
.app-shell { display: flex; height: 100vh; }
```

Width comes from `--menu-full-width` — no `::part()` override needed. Content in `slot="mini"` shows while the drawer is closed, giving an icon rail. Do not call `ShowAsync()` from `OnAfterRenderAsync`; the component is not ready. Drive it with `Open`.

### Icons in slots must be `IgbIcon`

A font-icon `<span>` is `display: inline`, so `vertical-align` is ignored by the slot's flex container and the glyph drifts to the top. `IgbIcon` is `inline-flex; align-items: center` and self-centers.

```razor
@* ❌ *@ <IgbInput><span slot="prefix" class="material-icons">search</span></IgbInput>
@* ✅ *@ <IgbInput><IgbIcon @ref="_icon" slot="prefix" IconName="search" Collection="material" /></IgbInput>
```

The parameter is `IconName`; `Name` is the framework's element identity on every component. Register the icon in `OnAfterRenderAsync(firstRender)` after `await EnsureReady()`, or nothing renders.

### `Name` never groups or names a form field

`<IgbRadioGroup Name="plan">` does not group radios — being children of the group does, and the selection is the group's `@bind-Value`.

### `IgbAvatar` uses `Shape`

`AvatarShape.Circle | Rounded | Square`. There is no `RoundShape`.

### `IgbCombo`'s generic parameter is `T`

`<IgbCombo T="Person" …>`, not `TValue`.

### `IgbCard` has no default width

Always set one.

### `IgbTileManager` drag and resize modes

`DragMode`: `None | TileHeader | Tile`. `ResizeMode`: `None | Hover | Always`.

### Grids need a height

Without one the grid renders every row and virtualization is off. Also remember the **grid-specific stylesheet** alongside the base theme for any full-featured grid.

## Dark themes

Switch both stylesheets together:

```html
<link href="_content/IgniteUI.Blazor/themes/dark/bootstrap.css" rel="stylesheet" />
<link href="_content/IgniteUI.Blazor/themes/grid/dark/bootstrap.css" rel="stylesheet" />
<!-- IgbGridLite instead: _content/IgniteUI.Blazor.GridLite/css/themes/dark/bootstrap.css -->
```

For a runtime toggle, prefer `IgbThemeProvider` (`Theme`, `Variant`) over swapping `<link href>` via JS interop — no interop and it can scope to a region.

**Multiple surface depths.** A single generated surface will not cover a sidebar darker than the content area. Define semantic variables and use them in layout CSS:

```css
:root {
    --surface-1: var(--ig-gray-900);   /* sidebar */
    --surface-2: var(--ig-gray-800);   /* content */
    --surface-3: var(--ig-gray-700);   /* elevated cards */
}
```

Grid headers and rows often need explicit backgrounds to match the design's hierarchy in dark mode:

```css
igc-grid {
    --ig-grid-header-background: var(--ig-surface-500);
    --ig-grid-content-background: var(--ig-surface-500);
    --ig-grid-row-hover-background: var(--ig-gray-100);
}
```

**No hardcoded hex after the palette exists.** Core component colors come from palette tokens (`var(--ig-primary-500)`, `var(--ig-primary-500-contrast)`) so theme switching keeps working. Chart, map, gauge and sparkline parameters are the exception — they need resolved color values.
