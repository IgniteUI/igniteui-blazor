# IgniteUI Blazor Templates — local test loop

This directory contains `IgniteUI.Blazor.Templates`, a `dotnet new` template pack that exposes the `igb-blazor` short name.

## Prerequisites

- .NET SDK 10 on PATH. The template targets `net10.0` only.
- Access to nuget.org and the Infragistics licensed feed (`IgniteUI.Blazor.GridLite` is published there).

## 1. Pack the template

```bash
cd templates/IgniteUI.Blazor.Templates
dotnet pack -c Release -o ./bin/packed
```

Produces `./bin/packed/IgniteUI.Blazor.Templates.<version>.nupkg` (current version `0.5.1`).

## 2. Install (or reinstall) into the local template engine

Two options. **Install from the packed nupkg** for a release-shape test:

```bash
dotnet new uninstall IgniteUI.Blazor.Templates    # ignore errors if not installed
dotnet new install ./bin/packed/IgniteUI.Blazor.Templates.0.5.1.nupkg
```

**Install from the source folder** for fast iteration (skip `dotnet pack`):

```bash
dotnet new install ./content/igb-blazor
```

If both a folder install and a NuGet install of the same identity (`IgniteUI.Blazor.Templates.BlazorWebApp`) coexist, `dotnet new install` warns and the most recently installed wins. Uninstall the other to silence the warning.

## 3. Inspect what the template exposes

```bash
dotnet new igb-blazor -h
```

Parameter surface:

| Parameter | Values | Default |
| --- | --- | --- |
| `--Hosting` (`-H`) | `Server`, `Wasm`, `Auto` | `Server` |
| `--Theme` (`-T`) | `bootstrap`, `material`, `fluent`, `indigo` | `bootstrap` |
| `--Variant` (`-V`) | `light`, `dark` | `light` |
| `--IncludeWeatherSample` | `true`, `false` | `true` |
| `--SkipRestore` | `true`, `false` | `false` |

Localized template strings ship for `en`, `ja`, `ko`. To preview a non-default locale:

```bash
DOTNET_CLI_UI_LANGUAGE=ja dotnet new igb-blazor -h
```

## 4. Generate a project

```bash
mkdir -p ./_smoketest && cd ./_smoketest
dotnet new igb-blazor -n MyTestApp --Hosting Server --SkipRestore
```

`--SkipRestore` skips the auto-restore post-action so you can inspect the unrestored output before committing to a network round-trip.

Output layout depends on `--Hosting`:

- **Server** — single project at `MyTestApp/MyTestApp.csproj`. No solution file.
- **Wasm / Auto** (when generated from the CLI) — host at `MyTestApp/MyTestApp.csproj`, client at `MyTestApp.Client/MyTestApp.Client.csproj`, plus `MyTestApp.sln` at the root listing both. The `.sln` is intentionally omitted when the template runs from VS (host bound to `HostIdentifier`), so VS can create its own `.slnx` without colliding.

Things worth eyeballing in the generated project:

- Host `.csproj` — `<TargetFramework>net10.0</TargetFramework>`; package refs include `IgniteUI.Blazor.Lite`, and for Wasm/Auto also `Microsoft.AspNetCore.Components.WebAssembly.Server`. With `--IncludeWeatherSample true`, also `Blazor-ApexCharts` and `IgniteUI.Blazor.GridLite`.
- For Wasm/Auto, the client `.csproj` ships `<StaticWebAssetProjectMode>Default</StaticWebAssetProjectMode>` and `<NoDefaultLaunchSettingsFile>true</NoDefaultLaunchSettingsFile>`; pages, layout, models, and services live under `MyTestApp.Client/` (renamed in via `template.json`).
- `Components/App.razor` — IgniteUI theme `<link>`s and the `_content/IgniteUI.Blazor/app.bundle.js` `<script>` are present.
- Namespaces in `Program.cs`, `Models/`, `Services/`, `_Imports.razor` should be `MyTestApp.*` — never `IgniteBlazorApp.*`.

## 5. Build and run

For Server:

```bash
cd MyTestApp
dotnet build
dotnet run
```

For Wasm/Auto, build the solution to compile both projects, then run the host:

```bash
cd MyTestApp
dotnet build MyTestApp.sln
dotnet run --project MyTestApp/MyTestApp.csproj
```

Hit `/`, `/counter`, `/weather` (the latter only with `--IncludeWeatherSample true`) — all should return 200, the navbar/drawer/grid/chart should render, and the IgniteUI custom elements (`igc-navbar`, `igc-nav-drawer`, `igc-grid-lite`) should be in the DOM. The actual port comes from `Properties/launchSettings.json`.

Quick smoke without a browser (replace `5148` with the port from `launchSettings.json`):

```bash
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5148/
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5148/_content/IgniteUI.Blazor/app.bundle.js
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5148/_content/IgniteUI.Blazor/themes/light/bootstrap.css
curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5148/_content/IgniteUI.Blazor.GridLite/css/themes/light/bootstrap.css
```

## 6. Clean up

```bash
dotnet new uninstall IgniteUI.Blazor.Templates    # if installed from a nupkg
# or
dotnet new uninstall ./content/igb-blazor          # if installed from folder
rm -rf ./_smoketest
```

## Troubleshooting

- **`dotnet new` doesn't list `igb-blazor`** — install step failed. Run `dotnet new uninstall` (no args) to confirm `IgniteUI.Blazor.Templates` (or the folder path) shows up; reinstall if not.
- **`NU1101: Unable to find package IgniteUI.Blazor.GridLite`** — your NuGet config doesn't have the Infragistics licensed feed. Add it before restoring the generated project.
- **Port already taken** — the generated `Properties/launchSettings.json` pins it. Either stop the other process or override with `dotnet run --urls http://localhost:5901`.
- **Rapidly iterating on the template content from a nupkg** — bump `<Version>` in `IgniteUI.Blazor.Templates.csproj` so `dotnet new install` doesn't quietly reuse a cached copy of the previous nupkg. (If installing from `./content/igb-blazor` directly, version doesn't matter — uninstall + reinstall the same path each iteration.)
- **Two installs of the same template identity** — the warning at install time is harmless; the most recent install wins. Uninstall whichever you don't want.
