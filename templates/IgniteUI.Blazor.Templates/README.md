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

## Smoke-testing the templates

Two equivalent scripts live next to this README and exercise the whole pack end to end — use `test-templates.ps1` on Windows/PowerShell and `test-templates.sh` on Linux/macOS/bash. They have identical behavior; pick whichever matches your shell.

Each run:

1. Installs the templates from this source folder (`dotnet new install . --force`).
2. Scaffolds a fresh Blazor **Server** project under `./tmp` via `dotnet new igb-blazor`.
3. Generates **every** item template (`igb-*`, `apexchart`) into the project's `Components/Pages` folder.
4. Wires each generated page into `Components/Layout/NavMenu.razor` as an `IgbNavDrawerItem`, so every demo is reachable from the nav drawer.

The result is a single project that hosts every component page — handy for verifying that the templates install, scaffold, and compile together after a change.

```bash
# bash (Linux/macOS)
./test-templates.sh

# PowerShell (Windows/cross-platform)
./test-templates.ps1
```

When it finishes it prints the generated project path and the `dotnet run` command to launch it.

### Configuration

Both scripts accept the same three settings. The PowerShell script exposes them as parameters (and also honors the env vars for parity); the bash script reads the env vars.

| Setting | PowerShell parameter | Env var (both) | Default | Description |
|---------|----------------------|----------------|---------|-------------|
| Project name | `-ProjectName` | `PROJECT_NAME` | `IgbTemplateTest` | Name of the generated project. |
| Nav icon | `-NavIcon` | `NAV_ICON` | `widgets` | `material-icons` name used for each generated nav item. |
| Skip restore | `-SkipRestore` | `SKIP_RESTORE` | `true` / `1` | Skip NuGet restore when creating the project (faster). |

```bash
# bash — override via env vars
PROJECT_NAME=MyDemo NAV_ICON=star SKIP_RESTORE=0 ./test-templates.sh
```

```powershell
# PowerShell — override via parameters
./test-templates.ps1 -ProjectName MyDemo -NavIcon star -SkipRestore $false
```

> The scripts wipe and recreate the `./tmp` folder on every run, so the generated project is disposable. Re-run after editing a template to regenerate from scratch.

## Uninstall

```bash
dotnet new uninstall IgniteUI.Blazor.Templates
```
