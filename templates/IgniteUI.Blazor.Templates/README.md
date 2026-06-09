# IgniteUI Blazor Templates

`dotnet new` templates for [Ignite UI for Blazor](https://www.infragistics.com/products/ignite-ui-blazor). The pack ships two kinds of templates:

- **A project template** (`igb-blazor`) that scaffolds a complete Blazor Web App wired up with Ignite UI.
- **Component item templates** (`igb-button`, `igb-grid`, …) that scaffold a single ready-to-edit Ignite UI component page into an existing project.

## Installation

```bash
dotnet new install IgniteUI.Blazor.Templates
```

## Project template

```bash
dotnet new igb-blazor -n MyApp
```

### Parameters

| Parameter | Description | Choices | Default |
|-----------|-------------|---------|---------|
| `--Hosting` | Interactivity hosting model | `Server`, `Wasm`, `Auto` | `Server` |
| `--Theme` | IgniteUI design theme | `bootstrap`, `material`, `fluent`, `indigo` | `bootstrap` |
| `--Variant` | Light or dark color variant | `light`, `dark` | `light` |
| `--IncludeWeatherSample` | Include the Weather page (IgbGridLite + chart demo) | `true`, `false` | `true` |
| `--SkipRestore` | Skip automatic NuGet restore on create | `true`, `false` | `false` |

### Examples

Create a project with Interactive WebAssembly hosting and the Material dark theme:

```bash
dotnet new igb-blazor -n MyApp --Hosting Wasm --Theme material --Variant dark
```

Create a server-rendered project with the Fluent theme, without the Weather sample page:

```bash
dotnet new igb-blazor -n MyApp --Theme fluent --IncludeWeatherSample false
```

## Component item templates

Each component template scaffolds a single `.razor` page (plus a scoped `.css` file) demonstrating one Ignite UI component. They are **item** templates, so run them from inside an existing project folder and pass the file name with `-n`:

```bash
dotnet new igb-button -n ButtonsDemo
```

This drops `ButtonsDemo.razor` into the current directory. In Visual Studio they also appear under **Add → New Item → Ignite UI for Blazor**, grouped by category.

> **Project setup required once.** Item templates can't modify project files, so before a scaffolded page will run, the host project must reference Ignite UI and register it. If you started from the `igb-blazor` project template this is already done. Otherwise, do it once:
>
> 1. `dotnet add package IgniteUI.Blazor.Lite`
> 2. In `Program.cs`: `builder.Services.AddIgniteUIBlazor();`
> 3. In `_Imports.razor`: `@using IgniteUI.Blazor.Controls`
> 4. In the host page (`App.razor` / `index.html` / `_Host.cshtml`) add the theme stylesheet and bundle:
>    `<link href="_content/IgniteUI.Blazor/themes/light/bootstrap.css" rel="stylesheet" />`
> 5. For a Blazor Web App (.NET 8+), give the scaffolded page an interactive render mode (e.g. `@rendermode InteractiveServer`).
>
> Don't mix `IgniteUI.Blazor.Lite` with the licensed `IgniteUI.Blazor` package.

### Available components

| Category | Short names |
|----------|-------------|
| Buttons & Actions | `igb-button`, `igb-button-group`, `igb-chip`, `igb-icon-button`, `igb-toggle-button` |
| Inputs | `igb-input`, `igb-mask-input`, `igb-checkbox`, `igb-switch`, `igb-radio-group`, `igb-rating`, `igb-slider`, `igb-range-slider` |
| Selection | `igb-select`, `igb-combo`, `igb-dropdown` |
| Date & Time | `igb-calendar`, `igb-date-picker`, `igb-date-range-picker`, `igb-date-time-input` |
| Layout | `igb-card`, `igb-accordion`, `igb-expansion-panel`, `igb-tabs`, `igb-stepper`, `igb-carousel`, `igb-tile-manager` |
| Navigation | `igb-navbar`, `igb-nav-drawer` |
| Data Display | `igb-list`, `igb-tree`, `igb-avatar`, `igb-badge`, `igb-icon`, `igbgrid` |
| Feedback | `igb-banner`, `igb-dialog`, `igb-snackbar`, `igb-toast`, `igb-tooltip`, `igb-circular-progress`, `igb-linear-progress` |
| Charts | `apexchart` |

See each template's options with `dotnet new <short-name> -h`.

## Uninstall

```bash
dotnet new uninstall IgniteUI.Blazor.Templates
```
