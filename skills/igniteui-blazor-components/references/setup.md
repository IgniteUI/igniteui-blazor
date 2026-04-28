# Setup — Ignite UI for Blazor

This reference covers initial project setup: NuGet installation, service registration, imports, CSS theme, and JS interop script.

---

## 1. Install the NuGet Package

Choose the package that matches your license:

```bash
# Community / open-source (MIT)
dotnet add package IgniteUI.Blazor.Lite

# Licensed / trial (full suite including charts, grids, DockManager)
dotnet add package IgniteUI.Blazor
```

Or add via the `<PackageReference>` in your `.csproj`:

```xml
<PackageReference Include="IgniteUI.Blazor" Version="*" />
```

---

## 2. Register Services in Program.cs

Every Ignite UI Blazor component has a corresponding **Module** type that must be registered at startup. Pass the module types you use to `AddIgniteUIBlazor()`:

```csharp
// Program.cs
using IgniteUI.Blazor.Controls;

var builder = WebApplication.CreateBuilder(args);

// Register Ignite UI Blazor with the modules your app needs
builder.Services.AddIgniteUIBlazor(
    typeof(IgbInputModule),
    typeof(IgbButtonModule),
    typeof(IgbDialogModule),
    typeof(IgbComboModule)
    // ... add every IgbXxxModule your app uses
);
```

### Module naming convention

Every component `IgbXxx` has a module `IgbXxxModule`. Examples:

| Component | Module |
|---|---|
| `IgbButton` | `IgbButtonModule` |
| `IgbDialog` | `IgbDialogModule` |
| `IgbCategoryChart` | `IgbCategoryChartModule` |
| `IgbTileManager` | `IgbTileManagerModule` |

> **Tip:** If you forget to register a module, the component will silently fail to render. Always double-check that every `Igb*` component used in Razor has its module registered in `Program.cs`.

---

## 3. Add the Using Directive

In your project's **_Imports.razor**, add:

```razor
@using IgniteUI.Blazor.Controls
```

This makes all `Igb*` components available in every Razor file without per-file `@using` statements.

---

## 4. Add the CSS Theme

Add the theme stylesheet to your host page.

**Blazor WebAssembly** — `wwwroot/index.html`:

```html
<head>
    <!-- Ignite UI Blazor theme -->
    <link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
</head>
```

**Blazor Server / Interactive Server** — `Pages/_Host.cshtml` or `App.razor`:

```html
<head>
    <link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />
</head>
```

### Available themes

| Path | Description |
|---|---|
| `themes/light/bootstrap.css` | Light Bootstrap theme |
| `themes/dark/bootstrap.css` | Dark Bootstrap theme |
| `themes/light/material.css` | Light Material theme |
| `themes/dark/material.css` | Dark Material theme |
| `themes/light/fluent.css` | Light Fluent theme |
| `themes/dark/fluent.css` | Dark Fluent theme |
| `themes/light/indigo.css` | Light Indigo theme |
| `themes/dark/indigo.css` | Dark Indigo theme |

---

## 5. Add the JS Interop Script

Ignite UI for Blazor wraps Ignite UI web components via JavaScript interop. Add the bundled script to your host page, **after** the Blazor framework script:

**Blazor WebAssembly** — `wwwroot/index.html`:

```html
<body>
    <div id="app">Loading...</div>

    <script src="_framework/blazor.webassembly.js"></script>
    <script src="_content/IgniteUI.Blazor/app.bundle.js"></script>
</body>
```

**Blazor Server** — `Pages/_Host.cshtml` or `App.razor`:

```html
<body>
    <component type="typeof(App)" render-mode="ServerPrerendered" />

    <script src="_framework/blazor.server.js"></script>
    <script src="_content/IgniteUI.Blazor/app.bundle.js"></script>
</body>
```

> For .NET 8+ with Interactive WebAssembly / Auto render modes, place the script in the `<body>` of `App.razor` or `_Host.cshtml` as appropriate for your project template.

---

## 6. Architecture Overview

Ignite UI for Blazor components are C# wrappers around Ignite UI web components:

- Each `IgbXxx` Blazor component renders its corresponding `igc-xxx` web component element via JS interop.
- No separate entry-point imports or ES module imports are required — the `app.bundle.js` script handles all web component registration.
- All component classes live in the `IgniteUI.Blazor.Controls` namespace.
- Component parameters use PascalCase (e.g., `Variant`, `DisplayType`, `KeepOpenOnEscape`).
- Enum values use their C# PascalCase names (e.g., `ButtonVariant.Contained`, `CalendarSelection.Single`).

---

## 7. Common Blazor Patterns

### Parameters (`[Parameter]`)

Component properties are set as Razor attributes:

```razor
<IgbButton Variant="ButtonVariant.Contained" Disabled="true">Submit</IgbButton>
```

### Two-way binding (`@bind-`)

Many components support two-way binding:

```razor
<IgbInput @bind-Value="username" Label="Username" />

@code {
    private string username = "";
}
```

### Event callbacks

Events use `EventCallback` or typed `EventCallback<T>`:

```razor
<IgbButton Click="OnClick">Click me</IgbButton>

@code {
    private void OnClick() { /* handle click */ }
}
```

### Component references (`@ref`)

For programmatic control (e.g., calling `Open()` / `Close()` methods):

```razor
<IgbDialog @ref="dialog">
    <p>Hello!</p>
</IgbDialog>

<IgbButton Click="() => dialog.ShowAsync()">Open Dialog</IgbButton>

@code {
    private IgbDialog dialog = default!;
}
```

### Child content (`RenderFragment`)

Components that accept children use Blazor's implicit `ChildContent`:

```razor
<IgbCard>
    <IgbCardHeader>
        <h3>Card Title</h3>
    </IgbCardHeader>
    <IgbCardContent>
        <p>Card body content</p>
    </IgbCardContent>
</IgbCard>
```

---

## 8. Minimal Complete Example

```razor
@page "/"
@using IgniteUI.Blazor.Controls

<h1>Hello Ignite UI</h1>

<IgbInput @bind-Value="name" Label="Your name" />
<IgbButton Variant="ButtonVariant.Contained" Click="OnGreet">Greet</IgbButton>
<IgbDialog @ref="dialog">
    <p>Hello, @name!</p>
</IgbDialog>

@code {
    private string name = "";
    private IgbDialog dialog = default!;

    private async Task OnGreet()
    {
        await dialog.ShowAsync();
    }
}
```

**Program.cs:**

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbInputModule),
    typeof(IgbButtonModule),
    typeof(IgbDialogModule)
);
```

---

## See Also

- [form-controls.md](form-controls.md) — Input, Combo, Select, DatePicker, Calendar, Checkbox, Radio, Switch, Slider, Rating
- [layout.md](layout.md) — Tabs, Stepper, Accordion, ExpansionPanel, NavDrawer
- [data-display.md](data-display.md) — List, Tree, Card, Chip, Avatar, Badge, Icon, Carousel, Progress
- [feedback.md](feedback.md) — Dialog, Snackbar, Toast, Banner
- [directives.md](directives.md) — Button, IconButton, ToggleButton, ButtonGroup, Ripple, Tooltip
- [layout-manager.md](layout-manager.md) — TileManager, Tile, DockManager
- [charts.md](charts.md) — CategoryChart, DataChart, PieChart, Gauges, Maps, Sparkline
