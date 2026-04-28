# Ignite UI Blazor Gotchas & Pitfalls

## Table of Contents
- [CSS Isolation & Scoping](#css-isolation--scoping)
- [Chart Properties](#chart-properties)
- [Component Properties](#component-properties)
- [Theming Pitfalls](#theming-pitfalls)
- [Map Component](#map-component)
- [Dark Theme Specifics](#dark-theme-specifics)
- [Blazor-specific Gotchas](#blazor-specific-gotchas)

---

## CSS Isolation & Scoping

Ignite UI for Blazor components render as web components (`igc-chip`, `igc-grid`, etc.). Use three CSS mechanisms depending on what you need and where the CSS lives:

| Mechanism | Purpose | Priority |
|---|---|---|
| Design tokens `--ig-*` | Override colors, borders, shadows via CSS vars | **Primary** - use whenever a token exists |
| `::part()` | Target a named shadow DOM part directly | **Secondary** - only when no token covers it; confirm part names via `get_doc` |
| `::deep` | Pierce Blazor CSS isolation in `.razor.css` files | **File helper** - add it in `.razor.css`; never needed in global CSS |

`::deep` and `::part()` solve different problems and are often combined: `::deep igc-chip::part(base) { ... }`.

**Global CSS (`app.css`)** - write selectors directly:
```css
igc-chip { --ig-chip-background: var(--ig-primary-500); }
igc-dialog::part(footer) { border-top: 1px solid var(--ig-gray-200); }
```

**`.razor.css` isolation file** - prefix every `igc-*` selector with `::deep`:
```css
::deep igc-chip { --ig-chip-background: var(--ig-primary-500); }
::deep igc-dialog::part(footer) { border-top: 1px solid var(--ig-gray-200); }
```

`create_component_theme(platform: "blazor", output: "css")` generates global CSS selectors - use as-is in `app.css`, or add `::deep` when placing in a `.razor.css` file. Never add `::deep` to `:root {}` blocks.

Blazor projects do **not** use Sass/SCSS.

---

## Chart Properties

### Markers shown by default
Category charts show markers at every data point by default. If the screenshot does not show markers, set `MarkerTypes` using the documented pattern for the installed version. When setting it from code, add the enum value after the chart reference is ready:
```razor
<IgbCategoryChart @ref="chart" ... />

@code {
    private IgbCategoryChart chart = default!;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            chart.MarkerTypes.Add(MarkerType.None);
        }
    }
}
```

### Charts do NOT inherit CSS theme colors
Charts, maps, gauges, and sparklines ignore the global CSS custom property theme. Set their visual properties explicitly via component parameters:
```razor
<IgbCategoryChart
    Brushes="#4FC3F7, #81C784, #FFB74D"
    Outlines="#4FC3F7, #81C784, #FFB74D"
    MarkerBrushes="#4FC3F7, #81C784, #FFB74D"
    MarkerOutlines="#4FC3F7, #81C784, #FFB74D"
    XAxisLabelTextColor="#666666"
    YAxisLabelTextColor="#666666" />
```

After a palette exists, prefer referencing palette tokens via `var(--ig-primary-500)` where the component supports CSS custom properties, or resolve the actual color value from the palette for DV component parameters.

### `Brushes`, `Outlines`, `MarkerBrushes`, `MarkerOutlines` are comma-separated strings
These are **string** parameters, not arrays. Pass colors as a single comma-separated string:
```razor
Brushes="#FF6B6B, #4ECDC4, #45B7D1"
```

### `AreaFillOpacity` exists on `IgbCategoryChart`
It does **NOT** exist on `IgbSparkline`. For sparkline fill, use the `Brush` parameter.

### `IncludedProperties` and `ExcludedProperties` are string arrays
Pass them as bound parameters:
```razor
<IgbCategoryChart IncludedProperties='@(new string[] { "Month", "Revenue" })' ... />
```

### Smooth area/line charts
Set `ChartType` to `Spline`, `SplineArea`, or `StepLine` / `StepArea` depending on the visual in the screenshot.

### Charts inside CSS Grid can collapse
Charts may render with zero height inside a CSS Grid container. Always set a `min-height` on the chart container or an explicit `Height` on the chart component:
```css
.chart-container {
  min-height: 0; /* Prevents CSS Grid blowout */
}
```
```razor
<IgbCategoryChart Height="300px" Width="100%" ... />
```

---

## Component Properties

### IgbAvatar: use `Shape` parameter
Use the `Shape` parameter with `AvatarShape.Circle`, `AvatarShape.Rounded`, or `AvatarShape.Square`. There is no `RoundShape` property:
```razor
<IgbAvatar Shape="AvatarShape.Circle" Initials="JD" />
```

### IgbList: slot-based structure
Use named `slot` attributes on child elements inside `IgbListItem` for positioning:
```razor
<IgbListItem>
    <IgbAvatar slot="start" Shape="AvatarShape.Circle" Initials="AB" />
    <span slot="title">Item Title</span>
    <span slot="subtitle">Secondary text</span>
    <IgbIcon slot="end" Collection="material" IconName="chevron_right" />
</IgbListItem>
```

### IgbNavDrawer: open state and mini slot
Use `Open`, `Position`, and the `mini` slot for expanded or collapsed drawer designs. Do not use Angular's `pin`/`pinned` model in Blazor:
```razor
<IgbNavDrawer Open="true" Position="NavDrawerPosition.Start">
    <IgbNavDrawerHeaderItem>Navigation</IgbNavDrawerHeaderItem>
    <div slot="mini">
        <IgbIcon Collection="material" IconName="home" />
    </div>
    <IgbNavDrawerItem>
        <IgbIcon slot="icon" Collection="material" IconName="home" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>
</IgbNavDrawer>
```

### IgbTabs: Panel and Id pairing
Each `IgbTab` must reference an `IgbTabPanel` via the `Panel` property matching the panel's `Id`:
```razor
<IgbTabs>
    <IgbTab Panel="panel-1">Tab A</IgbTab>
    <IgbTabPanel Id="panel-1">Content A</IgbTabPanel>
</IgbTabs>
```

### IgbTileManager: drag and resize modes
Set `DragMode` and `ResizeMode` for interactive dashboards:
```razor
<IgbTileManager DragMode="TileManagerDragMode.TileHeader" ResizeMode="TileManagerResizeMode.Always">
    <IgbTile ColSpan="2" RowSpan="1">...</IgbTile>
</IgbTileManager>
```

Valid drag modes include `TileHeader`, `Tile`, and `None`. Valid resize modes include `Always`, `Hover`, and `None`.

### IgbGrid: virtual rendering
The grid requires a container with a defined height. Without it, the grid will not virtualize and may render all rows:
```css
::deep igc-grid {
  height: 500px;
}
```

---

## Theming Pitfalls

### DV components do NOT inherit CSS theme colors
Charts, maps, gauges, and sparklines ignore the global CSS custom property theme entirely. Set their visual properties explicitly via component parameters. After a palette exists, use the resolved color values (not `var()` references) for DV component parameters:
```razor
<IgbCategoryChart
    Brushes="@primaryColor"
    Outlines="@primaryColor"
    XAxisLabelTextColor="@grayColor"
    YAxisMajorStroke="@grayLightColor" />
```

### Component-scoped theme overrides
For core UI component theming, use `create_component_theme` with `platform: "blazor"` and `output: "css"`. The MCP generates global CSS selectors (`igc-*`). Place the output in:

- **Global `app.css`** - use as-is (no `::deep`).
- **`.razor.css` isolation file** - add `::deep` to each selector:

```css
/* Dashboard.razor.css - add ::deep for isolation file */
::deep igc-grid {
  --ig-grid-header-background: var(--ig-primary-100);
  --ig-grid-content-background: var(--ig-surface-500);
}
```

Use `::part()` on an `igc-*` selector only when the property you need is not covered by any design token (verify via `get_component_design_tokens` first). In isolation files, combine `::deep` with `::part()`:

```css
/* MyView.razor.css */
::deep igc-dialog::part(footer) {
  border-top: 1px solid var(--ig-gray-200);
}
```

### Nav drawer sizing
Size the drawer with layout CSS around the drawer or with CSS parts/tokens verified from the current docs.
```css
.shell {
  display: grid;
  grid-template-columns: 260px 1fr;
}
```

### Dark theme overrides for grids
In dark themes, grid headers and rows may need explicit background overrides to match the design's surface hierarchy:
```css
::deep igc-grid {
  --ig-grid-header-background: var(--ig-surface-500);
  --ig-grid-content-background: var(--ig-surface-500);
  --ig-grid-row-hover-background: var(--ig-gray-100);
}
```

### No hardcoded colors after palette generation
Once a palette has been defined (via CSS custom property overrides or MCP `create_palette` / `create_theme`), core UI component colors should come from generated palette tokens - do not hardcode hex/RGB/HSL values. DV component parameters are the exception: charts, maps, gauges, and sparklines often need resolved color strings because they do not consume CSS variables consistently.

**WRONG** (hardcoded hex - breaks theme switching, ignores the palette):
```css
/* app.css - global */
igc-avatar {
  --ig-avatar-background: #e91e63;
}
```

**RIGHT** (palette token - stays in sync with the theme):
```css
/* app.css - global */
igc-avatar {
  --ig-avatar-background: var(--ig-primary-500);
  --ig-avatar-color: var(--ig-primary-500-contrast);
}

/* MyView.razor.css - isolation file: same tokens, add ::deep */
::deep igc-avatar {
  --ig-avatar-background: var(--ig-primary-500);
  --ig-avatar-color: var(--ig-primary-500-contrast);
}
```

### Read luminance warnings from theme generation
If `create_theme` returns a luminance warning for a generated surface, do not ignore it. If the design needs multiple surface depths, use `create_custom_palette` or define semantic CSS variables such as `--surface-1` and `--surface-2` in the global CSS file instead of relying on a single generated surface color.

---

## Map Component

### Adding markers programmatically
Map series are added as child components in Razor markup, not programmatically:
```razor
<IgbGeographicMap Height="500px" Width="100%">
    <IgbGeographicSymbolSeries
        DataSource="Locations"
        LatitudeMemberPath="Lat"
        LongitudeMemberPath="Lon"
        MarkerType="MarkerType.Circle"
        MarkerBrush="#FF5722" />
</IgbGeographicMap>
```

### Dark map styling
OpenStreetMap tiles are light by default. For dark themes, apply a CSS filter to the map container. Adjust the values to match the map tone in the design image:
```css
.map-container {
  /* tune grayscale (0-1) and brightness (0-1) to match the design */
  filter: grayscale(0.8) brightness(0.6);
}
```

---

## Dark Theme Specifics

### Use the dark theme CSS variant
Switch the theme `<link>` from light to dark:
```html
<!-- Light -->
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
<!-- Dark -->
<link href="_content/IgniteUI.Blazor/themes/dark/bootstrap.css" rel="stylesheet" />
```

### CSS custom properties for dark panels
When the design has multiple dark surface depths (e.g., sidebar darker than content area), define semantic tokens:
```css
:root {
  --surface-1: var(--ig-gray-900); /* deepest (sidebar, drawer) */
  --surface-2: var(--ig-gray-800); /* content area */
  --surface-3: var(--ig-gray-700); /* elevated cards/panels */
}
```

Apply these in layout CSS rather than hardcoding hex values.

### Programmatic dark/light toggle
Swap the CSS `<link>` element via JS interop:
```csharp
await JS.InvokeVoidAsync("eval",
    "document.getElementById('theme-css').href = '_content/IgniteUI.Blazor/themes/dark/bootstrap.css'");
```

Or toggle a CSS class with variable overrides (see `igniteui-blazor-theming` skill for full patterns).

---

## Blazor-specific Gotchas

### Always register modules in `Program.cs`
Every `Igb*` component needs its `IgbXxxModule` registered via `AddIgniteUIBlazor()`. If a component silently fails to render, the most common cause is a missing module registration:
```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbCategoryChartModule),
    typeof(IgbListModule),
    typeof(IgbNavbarModule)
);
```

### Two-way binding uses `@bind-` prefix where the component exposes bindable values
Blazor uses `@bind-Value`, `@bind-Checked`, etc. Verify the exact bindable parameter from the component docs before inventing one:
```razor
<IgbInput @bind-Value="searchText" />
<IgbSwitch @bind-Checked="isDarkMode" />
```

### Event handler pattern
Use the event pattern shown in the Blazor docs for that component. Native click handling on Ignite UI buttons commonly uses Blazor's `@onclick`; component events use their PascalCase event names:
```razor
<IgbButton @onclick="HandleClick">Save</IgbButton>
<IgbGrid RowSelectionChanging="OnRowSelection" />

@code {
    private void HandleClick() { /* ... */ }
    private void OnRowSelection(IgbRowSelectionEventArgs args) { /* ... */ }
}
```

### Template context with `<Template>`
Ignite UI Blazor uses `<Template>` child elements with the `context` parameter - not Angular's `<ng-template>`:
```razor
<IgbGrid DataSource="Data">
    <IgbColumn Field="Name" Header="Name">
        <Template>
            <div>
                <strong>@context.Cell.Value</strong>
            </div>
        </Template>
    </IgbColumn>
</IgbGrid>
```

### `@ref` for programmatic access
Use `@ref` with a matching field of the component type:
```razor
<IgbDialog @ref="dialog">
    <p>Dialog content</p>
</IgbDialog>

@code {
    private IgbDialog dialog = default!;

    private async Task ShowDialog()
    {
        await dialog.ShowAsync();
    }
}
```

### C# data models - use records or classes
Use C# `record` or `class` types for mock data - not TypeScript interfaces:
```csharp
record SalesRecord(string Month, double Revenue, double Profit);
record Contact(string Name, string Email, string Initials);
```

### Avoid inline styles - use CSS isolation
Keep layout, spacing, typography, and surface styling in `.razor.css` files rather than using `style=""` inline attributes. This makes theming consistent and maintainable.

### Parameter naming: PascalCase
Blazor component parameters use PascalCase (`ChartType`, `DataSource`, `MarkerTypes`) - not Angular's camelCase bindings (`[chartType]`, `[dataSource]`, `[markerTypes]`).
