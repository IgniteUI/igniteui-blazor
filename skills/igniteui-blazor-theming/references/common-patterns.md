# Common Theming Patterns

## Built-in themes

Eight stylesheets under `_content/IgniteUI.Blazor/themes/`: `{light|dark}/{bootstrap|material|fluent|indigo}.css`. Link **one** in the host page — `wwwroot/index.html` (WASM), `Pages/_Host.cshtml` (Server), or `Components/App.razor` (Web App):

```html
<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />
<script src="_content/IgniteUI.Blazor/app.bundle.js"></script>
```

.NET 9+ Web App projects can use the fingerprinted asset collection:

```razor
<link rel="stylesheet" href="@Assets["_content/IgniteUI.Blazor/themes/light/fluent.css"]" />
```

If the page hosts any full-featured grid, add the grid stylesheet in the **same variant**:

```html
<link href="_content/IgniteUI.Blazor/themes/grid/light/bootstrap.css" rel="stylesheet" />
```

`IgbGridLite` replaces both with its own: `_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css`.

## Switching theme at runtime

### `IgbThemeProvider` — preferred

Bind the parameters to state; no JS interop, no stylesheet swapping, and it can theme one region rather than the whole page.

```razor
<IgbSwitch @bind-Checked="IsDark">Dark mode</IgbSwitch>

<IgbThemeProvider Theme="Theme.Bootstrap" Variant="@(IsDark ? ThemeVariant.Dark : ThemeVariant.Light)">
    @Body
</IgbThemeProvider>

@code { bool IsDark; }
```

Register `IgbThemeProviderModule`. Providers nest — an inner one overrides the outer for its subtree.

### Swapping the stylesheet

Use this when the whole document, including your own CSS that reads palette variables, has to follow the theme.

```html
<link id="igTheme" href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
<script>
    window.setIgTheme = href => document.getElementById('igTheme').setAttribute('href', href);
</script>
```

```razor
@inject IJSRuntime JS

@code {
    bool IsDark;

    async Task ToggleDark()
    {
        IsDark = !IsDark;
        var variant = IsDark ? "dark" : "light";
        await JS.InvokeVoidAsync("setIgTheme", $"_content/IgniteUI.Blazor/themes/{variant}/bootstrap.css");
    }
}
```

Preloading both stylesheets and toggling their `disabled` flags avoids the fetch flash:

```js
window.toggleDarkMode = isDark => {
    document.getElementById('igLightTheme').disabled = isDark;
    document.getElementById('igDarkTheme').disabled = !isDark;
};
```

Remember to swap the grid stylesheet variant too when a full grid is on the page.

## CSS custom properties (design tokens)

Every visual attribute is a CSS custom property. Tokens are the **primary** customization layer — reach for `::part()` only when no token covers the property.

1. Call `get_component_design_tokens("<component-or-variant>")` — never guess names.
2. Override the ones you need in a rule matching the component's scope.

```css
/* app.css — global, no ::deep */
:root {
    --ig-primary-500: #7b2fff;
    --ig-primary-600: #6200ee;
}

igc-chip {
    --ig-chip-background: var(--ig-primary-500);
    --ig-chip-text-color: var(--ig-primary-500-contrast);
}
```

```css
/* MyView.razor.css — isolation file, ::deep required on igc-* selectors */
::deep igc-chip {
    --ig-chip-background: var(--ig-primary-500);
}
```

## `::deep` — what it does and does not do

`::deep` is Blazor's CSS-isolation combinator, not a shadow-DOM piercer. It only applies in `.razor.css` files, and only when Blazor's scope attribute sits on a parent **above** the target.

| Do | Don't |
|---|---|
| `::deep igc-chip { … }` | `::deep .my-class { … }` — plain class selectors are already scoped |
| `::deep igc-dialog::part(footer) { … }` | `::deep :root { … }` — never scope a `:root` block |
| Plain `.layout { … }` for your own HTML | `::deep` on the component's own root element — no scoped parent exists above it |

MCP output from `create_component_theme` is global CSS: drop it into `app.css` unchanged, or add `::deep` to each `igc-*` selector when it goes into an isolation file.

## Scoped overrides without `IgbThemeProvider`

Wrap the components and scope the selector to the wrapper — useful for one-off tweaks that are narrower than a whole theme.

```razor
<div class="cta-area">
    <IgbButton>Custom Style</IgbButton>
</div>
```

```css
/* global */
.cta-area igc-button {
    --ig-primary-500: #ff5722;
    --ig-primary-600: #e64a19;
}

/* isolation file */
::deep .cta-area igc-button { --ig-primary-500: #ff5722; }
```

## CSS shadow parts

Setting a design token and styling a part are different operations — a token flows through the component's own styling logic, a `::part()` rule overrides one element directly. Prefer the token.

1. `get_component_design_tokens` — is there a token for this property? If yes, use it.
2. If not, `get_doc` for the component to get the **exact** part names. Do not claim a component exposes parts without confirming.
3. Write `igc-<tag>::part(<name>)`, adding `::deep` in an isolation file.

```css
/* app.css */
igc-dialog::part(footer) {
    background-color: var(--ig-gray-100);
    border-top: 1px solid var(--ig-gray-200);
    padding: 12px 16px;
}

/* MyView.razor.css */
::deep igc-dialog::part(footer) { border-top: 1px solid var(--ig-gray-200); }
```

## Global layout tokens

```
set_roundness(platform: "blazor", output: "css", radiusFactor: 0.5)   # 0 = square, 1 = fully rounded
set_spacing(platform: "blazor", output: "css", spacing: 1.25)
set_size(platform: "blazor", output: "css", size: "small")            # small | medium | large
```

Put the returned CSS in `:root` in your global stylesheet. `--ig-size` also drives grid density directly — see the grids skill's [`sizing.md`](../../igniteui-blazor-grids/references/sizing.md).

## Multiple surface depths in dark themes

A single generated surface color rarely covers a design with a darker sidebar and lighter cards. Define semantic variables layered on the palette and use those in layout CSS instead of hardcoding hex:

```css
:root {
    --surface-1: var(--ig-gray-900);   /* sidebar, drawer */
    --surface-2: var(--ig-gray-800);   /* content area */
    --surface-3: var(--ig-gray-700);   /* elevated cards */
}
```

`create_custom_palette` is the alternative when the extra depths belong in the palette itself.

## Rules

1. One built-in theme stylesheet at a time.
2. Selectors use `igc-*` tag names, never Razor component names.
3. Call `get_component_design_tokens` before writing any `--ig-<component>-*` name.
4. Load your overrides after the built-in theme so the cascade favors them.
5. Once a palette exists, use its tokens instead of raw hex — hardcoded colors break theme switching. Charts and other DV components are the exception; they need resolved color values.
6. Blazor theming is plain CSS. There is no Sass pipeline.
