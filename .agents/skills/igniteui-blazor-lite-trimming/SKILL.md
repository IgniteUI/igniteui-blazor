---
name: igniteui-blazor-lite-trimming
description: Keep IgniteUI.Blazor.Lite trim-compatible when changing library code. Use when touching reflection, JsonSerializer calls, DynamicallyAccessedMembers annotations, or suppressions in src/, when the build fails with IL2xxx errors, or when asked about trimming, trim warnings, or the PublishSmoke app.
---
# IgniteUI.Blazor.Lite — Trimming

The library ships `IsTrimmable` and must stay trim-clean: every trim-analysis diagnostic (IL2xxx) builds as an **error**, enforced by `dotnet_analyzer_diagnostic.category-Trimming.severity = error` in the repo `.editorconfig`. The source of truth for policy and consumer guidance is [docs/TRIMMING.md](../../../docs/TRIMMING.md) — read its "Maintaining trim compatibility (contributors)" section before working around any IL2xxx error.

## Rules

1. **Fix first.** Serialize through source-generated `JsonTypeInfo` — extend `IgbJsonContext` (`src/componentsBase/IgbJsonContext.cs`) rather than calling reflection-based `JsonSerializer` overloads. Annotate `Type` flows with `[DynamicallyAccessedMembers]` where the flow is traceable (scalar `Type`/`string` parameters, properties, generic parameters — annotations are invalid on `Type[]` and do not trace through collections). For "keep members whenever the type is kept, however its `Type` value flows" use the self-referencing generic attribute pattern: `[IgbModule<TSelf>]` on the module classes (see `IgbModuleAttribute<TModule>` in `src/componentsBase/IgbModule.cs`) — a new module MUST carry it, guarded by `ServiceRegistrationTests.EveryLibraryModule_CarriesSelfReferencingIgbModuleAttribute`.
2. **Never annotate method parameters or fields of component base classes** (`BaseRendererControl`, `BaseRendererElement`, or anything ComponentBase-derived). `OpenComponent<T>` roots component members "via reflection", so such annotations surface as IL2111/IL2110 errors in every consuming app. Annotated *properties* are fine (framework precedent: `RouteView.DefaultLayout`).
3. **Suppress narrowly when a fix is impossible.** `[UnconditionalSuppressMessage("Trimming", "ILxxxx", Justification = "...")]` on the smallest member, with a justification stating why the pattern is safe at runtime; extract a small private helper if needed so the justification matches exactly what the member does. Keep the helper's `Type` parameter unannotated — annotating it only moves the warning to the caller.
4. **Never use `#pragma warning disable` for ILxxxx.** It silences only the build analyzer and leaves no metadata for publish-time trim tooling (ILLink, NativeAOT's ILCompiler).
5. **Don't add reflection over user-supplied or runtime-discovered types** outside the documented data-source boundary (`JSDataSourceSchema` and the `ExtractSchema` entry points).

## Verification

- `dotnet build src/IgniteUI.Blazor.Lite.csproj` must be free of IL diagnostics (they fail the build).
- For changes touching reflection, serialization, or annotations, run the automated browser checks over the trimmed publish: `dotnet test tests/IgniteUI.Blazor.Lite.IntegrationTests --filter Category=TrimmedPublish --settings .runsettings` (`TrimmedPublishSmokeTest`; publishes the smoke app net10.0 on demand). It is category-scoped — not part of the per-component integration sweep — and covers net10.0 only; the manual checklist in `tests/IgniteUI.Blazor.Lite.PublishSmoke/README.md` covers the other TFMs. Trimming only happens at publish (`dotnet run` proves nothing).
- Verify claims about linker behavior empirically in the smoke app — a control-vs-fix publish pair, not reasoning from documentation alone.
