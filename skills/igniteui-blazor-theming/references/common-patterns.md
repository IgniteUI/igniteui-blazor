# Common Theming Patterns

> **Part of the [`igniteui-blazor-theming`](../SKILL.md) skill hub.**

## Contents

- [Switching Between Light and Dark Themes](#switching-between-light-and-dark-themes)
- [Scoping a Theme to a Container](#scoping-a-theme-to-a-container)
- [CSS Isolation in Blazor](#css-isolation-in-blazor)
- [Licensed Package Users](#licensed-package-users)

---

## Switching Between Light and Dark Themes

Blazor doesn't use Sass, so light/dark switching is done by swapping the CSS file or toggling CSS custom property overrides.

### Option A — Swap the CSS `<link>` Element

The simplest approach: change which pre-built CSS file is loaded.

**In `App.razor` (.NET 8+):**

```razor
@inject IJSRuntime JS

<head>
    <link id="theme-css" href="@_themeHref" rel="stylesheet" />
</head>

@code {
    private string _themeHref = "_content/IgniteUI.Blazor/themes/light/bootstrap.css";

    private async Task ToggleTheme()
    {
        _themeHref = _themeHref.Contains("/light/")
            ? _themeHref.Replace("/light/", "/dark/")
            : _themeHref.Replace("/dark/", "/light/");

        await JS.InvokeVoidAsync("eval",
            $"document.getElementById('theme-css').href = '{_themeHref}'");
    }
}
```

**Alternative — pure JS in `wwwroot/index.html`:**

```html
<link id="theme-css" href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />

<script>
    window.setTheme = function(variant) {
        const link = document.getElementById('theme-css');
        link.href = link.href.replace(/\/(light|dark)\//, '/' + variant + '/');
    };
</script>
```

Then call from Blazor:

```csharp
await JS.InvokeVoidAsync("setTheme", "dark");
```

### Option B — CSS Class Toggle with Variable Overrides

Include only one theme file (e.g., light) and override the palette for dark mode using a CSS class:

```css
/* wwwroot/css/app.css */

/* Dark theme overrides — applied when <body class="dark-theme"> */
.dark-theme {
  --ig-primary-h: 207;
  --ig-primary-s: 90%;
  --ig-primary-l: 77%;

  --ig-secondary-h: 30;
  --ig-secondary-s: 100%;
  --ig-secondary-l: 72%;

  --ig-surface-h: 0;
  --ig-surface-s: 0%;
  --ig-surface-l: 7%;

  --ig-gray-h: 0;
  --ig-gray-s: 0%;
  --ig-gray-l: 62%;
}
```

Toggle in Blazor:

```csharp
await JS.InvokeVoidAsync("eval",
    "document.body.classList.toggle('dark-theme')");
```

### Option C — `prefers-color-scheme` Media Query

Automatically follow the OS preference:

```css
/* wwwroot/css/app.css */
@media (prefers-color-scheme: dark) {
  :root {
    --ig-primary-h: 207;
    --ig-primary-s: 90%;
    --ig-primary-l: 77%;

    --ig-surface-h: 0;
    --ig-surface-s: 0%;
    --ig-surface-l: 7%;
  }
}
```

---

## Scoping a Theme to a Container

Override CSS custom properties on a container class to create a scoped theme region:

```css
/* A branded "admin" panel with its own palette */
.admin-panel {
  --ig-primary-h: 262;
  --ig-primary-s: 52%;
  --ig-primary-l: 47%;

  --ig-secondary-h: 340;
  --ig-secondary-s: 82%;
  --ig-secondary-l: 52%;

  --ig-surface-h: 260;
  --ig-surface-s: 20%;
  --ig-surface-l: 96%;
}
```

All Ignite UI components inside `.admin-panel` will inherit these overrides:

```razor
<div class="admin-panel">
    <IgbButton>Admin Action</IgbButton>
    <IgbAvatar Shape="AvatarShape.Circle" />
</div>
```

You can also scope layout controls:

```css
.compact-sidebar {
  --ig-size: 1;
  --ig-spacing: 0.5;
  --ig-radius-factor: 0.3;
}
```

---

## CSS Isolation in Blazor

Blazor CSS isolation (`.razor.css` files) scopes styles to a single component using a unique attribute. This works for standard HTML elements but requires `::deep` to target child web components.

### Basic Scoped Override

**MyComponent.razor.css:**

```css
/* This scopes to MyComponent only */
:host {
  --ig-size: 1;
}
```

### Targeting Ignite UI Web Components with `::deep`

Ignite UI for Blazor components render as web components (`igc-button`, `igc-grid`, etc.) inside the Blazor host element. To style them from an isolated CSS file, use `::deep`:

**MyComponent.razor.css:**

```css
::deep igc-avatar {
  --ig-avatar-background: var(--ig-primary-500);
  --ig-avatar-color: var(--ig-primary-500-contrast);
}

::deep igc-button {
  --ig-button-background: var(--ig-secondary-500);
  --ig-button-foreground: var(--ig-secondary-500-contrast);
}
```

### When to Use CSS Isolation vs. Global CSS

| Scenario | Approach |
|---|---|
| App-wide palette and theme | Global CSS file (`app.css` or theme override in `<style>`) |
| Light/dark theme switching | Global CSS file or `<link>` swap |
| Component-specific token overrides for one page | CSS isolation (`PageName.razor.css`) with `::deep` |
| Design tokens shared across many pages | Global CSS file |

> **Limitation:** CSS isolation does not work with `@media` queries for theme switching in the same file in all Blazor hosting models. For light/dark switching, prefer global CSS or JS-based `<link>` swapping.

---

## Licensed Package Users

Both `IgniteUI.Blazor` (licensed/trial) and `IgniteUI.Blazor.Lite` (open-source/community) serve theme CSS from the same static content path:

```
_content/IgniteUI.Blazor/themes/{light|dark}/{material|bootstrap|fluent|indigo}.css
```

The CSS custom properties, token names, and theming architecture are **identical** between the two packages. The only difference is which components are available (grids, charts, and some premium components are not in `IgniteUI.Blazor.Lite`).

When using MCP tools, the `licensed` parameter does not change CSS output for Blazor — the theme files are the same regardless of license tier.

### NuGet Package Reference

```xml
<!-- Licensed / trial — full suite including grids, charts, DockManager -->
<PackageReference Include="IgniteUI.Blazor" Version="*" />

<!-- Community / open-source (MIT) — core components only -->
<PackageReference Include="IgniteUI.Blazor.Lite" Version="*" />
```
