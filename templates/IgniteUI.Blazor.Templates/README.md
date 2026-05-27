# IgniteUI Blazor Templates

`dotnet new` project templates for creating Blazor Web Apps powered by [Ignite UI for Blazor](https://www.infragistics.com/products/ignite-ui-blazor).

## Installation

```bash
dotnet new install IgniteUI.Blazor.Templates
```

## Usage

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

## Uninstall

```bash
dotnet new uninstall IgniteUI.Blazor.Templates
```
