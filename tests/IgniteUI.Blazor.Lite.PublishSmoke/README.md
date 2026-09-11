# PublishSmoke — trimmed-publish verification app

A minimal Blazor WebAssembly app that publishes `IgniteUI.Blazor.Lite` **with assembly trimming** and exercises the reflection-adjacent features in a real browser. The trim analyzer cannot validate `[UnconditionalSuppressMessage]` justifications or retention-based mechanisms like `[IgbModule<TSelf>]` — this app is the end-to-end check for those.

## Why publish? Can't I just `dotnet run -c Release`?

No — the trimmer (ILLink) only runs during **publish**. `dotnet run`/`dotnet build` always use the full, untrimmed assemblies regardless of configuration, so nothing trim-related can be observed that way. Verification is always: publish, then serve the publish output as static files.

## Run it

```bash
# 1. Publish trimmed (from the repo root). The app multi-targets the library's TFMs;
#    publish requires picking one — check all three after linker-sensitive changes.
dotnet publish tests/IgniteUI.Blazor.Lite.PublishSmoke -c Release -f net10.0

# 2. Serve the publish output (any static file server works; npx needs no install — Node is a repo prerequisite):
npx http-server tests/IgniteUI.Blazor.Lite.PublishSmoke/bin/Release/net10.0/publish/wwwroot -p 5620

# 3. Open http://localhost:5620/ and check the page against the table below.
```

Three gates, in order of what they can see:

1. **Build** — the trim analyzer runs over this app's own source (`EnableTrimAnalyzer`, findings are errors via the repo `.editorconfig`); the library's source is analyzed the same way at its own build. Build-time analysis cannot see linker behavior or referenced-assembly IL.
2. **Publish** — ILLink runs; fails on any linker warning (`ILLinkTreatWarningsAsErrors`, single-warn off) such as unresolved `DynamicDependency` assemblies (IL2035 class). Linker *analysis* warnings stay muzzled — Blazor default — because the framework's own assemblies emit them.
3. **Browser** — the only gate that catches *silent* trims: a publish can succeed while a suppression's justification or a retention mechanism quietly stopped holding. That's the checklist below.

## What to check in the browser

| Page section | Expected | A failure means |
|---|---|---|
| `ChatModule preloaded:` | `True` | `[IgbModule<TSelf>]` preservation broke — the Type-based module preload silently lost `Register` (IgbChat's component is deliberately never referenced by this app, so nothing else can keep it alive) |
| Outlined button + avatar render styled | camelCase enum attributes (`variant="outlined"`, `shape="circle"` in dev tools) | enum fields were trimmed — the enum-preservation justification in `TryGetWCEnumName`/`GetWCEnumTransform` no longer holds |
| Button group | `selection` is `single-required` in dev tools | enum **field attributes** were trimmed — the `[WCEnumName]` mapping path degraded to camelCase (`singleRequired`) |
| Combo shows 3 items | `Alpha`, `Beta`, `Gamma` | data-source reflection over the (preserved) POCO broke — the documented `DynamicallyAccessedMembers` consumer pattern in docs/TRIMMING.md no longer suffices |
| Date range picker present; selecting a range updates the result line | date text below the picker | the event payload path (`IgbDateRangeValue` materialization) broke |
| Browser console | no errors (a stray `favicon` 404 is fine) | anything else: investigate |

The checklist is automated as `TrimmedPublishSmokeTest` in `IgniteUI.Blazor.Lite.IntegrationTests` (`Category=TrimmedPublish`, runs in CI after the publish step). It serves the net10.0 publish output — publishing it on demand if missing — so locally it's just:

```bash
dotnet test tests/IgniteUI.Blazor.Lite.IntegrationTests --filter Category=TrimmedPublish --settings .runsettings
```

The manual browser pass above remains useful for the other TFMs and for linker experiments.

## Wasm AOT

The manual **`Wasm AOT Smoke`** workflow (`workflow_dispatch`) publishes this app with `-p:RunAOTCompilation=true` (net10.0, slow multi-minute compile) and runs the same browser checks against that output. Locally:

```bash
dotnet publish tests/IgniteUI.Blazor.Lite.PublishSmoke -c Release -f net10.0 -p:RunAOTCompilation=true
dotnet test tests/IgniteUI.Blazor.Lite.IntegrationTests --filter Category=TrimmedPublish --settings .runsettings
```

Wasm AOT is Mono AOT with the interpreter retained — it validates the product path but emits no NativeAOT diagnostics; that gate is `tests/IgniteUI.Blazor.Lite.AotSmoke` (per-PR CI).

## When to run

- After any change to reflection, serialization, `[DynamicallyAccessedMembers]` annotations, or suppressions in `src/` (see the `igniteui-blazor-lite-trimming` skill in `.agents/skills/` and docs/TRIMMING.md).
- After SDK updates — linker behavior should be re-validated.
- For linker experiments, run a control (publish *without* the change) first. Beware: any constrained static-abstract call (e.g. `IIgbModule.Register` through a generic) roots the implementations on all kept module types, contaminating controls.
