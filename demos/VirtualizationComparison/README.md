# Virtualization Comparison

Side-by-side evaluation of the built-in Blazor `<Virtualize>` component from the
**.NET 11 RC 1** SDK against **`IgbVirtualScroll`** (the wrapper over the
`igniteui-webcomponents` virtualization container), so the two can be compared
feature by feature: variable-height items, scroll-to-index, initial position,
viewport anchoring on prepend/append, lazy loading, orientation and templating.

## Requirements

- .NET 11 RC 1 SDK (`11.0.100-rc.1` or later prerelease) — pinned by the local
  `global.json`, which overrides the repository root pin to SDK 10.
- The client bundle built at least once from the repository root (`npm run build:dev`).

## Run

```sh
dotnet run
```

Then open <http://localhost:5300>.

The project targets `net11.0`, references `src/IgniteUI.Blazor.Lite.csproj`
directly and is intentionally **not** part of `IgniteUI.Blazor.Lite.sln`, so CI
and the main solution keep building with the SDK 10 pin.
