# Trimming follow-ups

Findings from the IsTrimmable change set (see PR-DESCRIPTION.md) that were deliberately left out of scope. Ordered roughly by value.

## 1. Native AOT compatibility (`IsAotCompatible`)

The big one. Blocked on `RequiresDynamicCode` surfaces:

- `JsonDataSourceSchema.cs:760-855` — six `Expression.Lambda(...).Compile()` getter builders (property/field/dictionary). Need interpreter-fallback awareness or a rewrite to delegate-free access; the untyped `PropertyGetters` (`Func<object,object>` via reflection) could serve as the AOT path.
- `RuntimeHelper.cs:57-102` — `MakeGenericMethod` + compiled expressions (net8-only, dead on net9+; consider `#if`-ing the whole probe out once net8 support drops).
- `BaseRendererControl.cs:~742` — `typeof(DynamicContentInfo<>).MakeGenericType(templateContentType)` + compiled lambda; `templateContentType` comes from a closed set of `typeof` literals, so a generated switch could replace it.
- The PublishSmoke app is named publish-scoped on purpose: add a wasm AOT publish profile (`RunAOTCompilation=true`) there when this work starts.

## 2. PublishSmoke coverage gaps

- Targets net10 only — the net8-only code (`EventCallbackExtensions` reflection path, `RuntimeHelper` InvokeUnmarshalled probe) is covered by the build-time analyzer but never by an actual ILLink pass. Consider a net8 publish leg.
- ILLink only analyzes marked code: components the smoke app doesn't reference are invisible to it. Consider growing the app toward representative coverage (templating/dynamic content, a `Type[]`-based module registration to exercise the documented failure mode) and/or wiring it into the Playwright integration-test infrastructure so the browser checks run in CI instead of by hand.

## 3. Docs cleanup: stale "generated code" claims

`src/components/Blazor/` is hand-maintained (the `npm run ingest` codegen was dropped), but these still claim otherwise and mislead exploration/tooling: the csproj TODO comment (`src/IgniteUI.Blazor.Lite.csproj:5-6`), `FORMATTING.md` ("re-formatted after every ingest"), `Child-Modules-PR-description.md` ("the emitter needs the same change"), `PR-tabs-collection-fix.md`, `skills/igniteui-blazor-lite-testing/SKILL.md:55`.

## 4. Module registration: analysis results & optional enhancement

Empirical findings (2026-08-20, via PublishSmoke experiments):

- All 75 `Register` bodies are string-only (`ModuleLoader.Load(runtime, "WebXModule")` → `RequestLoad(string)`) — no component-type references. Rooting them all costs ~nothing.
- `typeof(IgbXModule)` in a trimmed app keeps the type but NOT `Register` (member-level sweep); the legacy `Type[]` preload then silently no-ops. Only bites modules whose component is never statically used — every component roots its own module's `Register` via `EnsureModulesLoaded`.
- **Type-level `[DynamicallyAccessedMembers]` does NOT fix it** — tested on both the `IIgbModule` interface and directly on the module class: type-level DAM is *flow-dependent* (acts only where an annotated `Type` value reaches a recognized reflection sink), and the `typeof` dies in the unannotated `Type[]`. A conditional "keep Register iff module kept" is not expressible for this API shape.
- Using the `IgbModuleCollection` overload anywhere in the app keeps `Register` on ALL kept module types (constrained `IIgbModule.Register` call → interface implementations preserved) — the smoke app relies on this for its dual-path check.

**RESOLVED (2026-08-20)** by `IgbModuleRef`: the `params Type[]` overloads were replaced with `params IgbModuleRef[]` + an implicit conversion from `Type` whose `[DAM(PublicMethods)]` parameter is traced at each call site — source-compatible for `typeof(...)` args, conditional preservation, verified in isolation (True; raw-`Type[]` control False). Key mechanism: annotations on a struct's **member** ride through collections/arrays (location-based), unlike parameter-flow tracing which dies at any collection hop. The registry idea is superseded. Remaining relevant: the legacy `WithModulesToLoad` settings path keeps its suppression; a module listed in both paths registers once per path (client loading dedupes by name).

**Hard-won constraint for future annotation work:** never put `[DynamicallyAccessedMembers]` on *method parameters or fields* of `ComponentBase`-derived classes — `OpenComponent<T>`'s `DAM(All)` roots component members "via reflection" and ILLink then emits IL2111/IL2110 into every consuming app. Framework precedent (e.g. `RouteView.DefaultLayout`) allows annotated *properties* only. This is why `ObjectToParam`/`TryGetWCEnumName` use contained suppressions instead of the annotation chain.

## 5. Trim-suppression mechanics (verified 2026-08-20)

- Blazor WASM publish sets `SuppressTrimAnalysisWarnings=true` by default. Unmuzzling it in PublishSmoke surfaced real library issues once (RuntimeHelper IL2026, App-side IL2111 — both fixed), but the **framework's own assemblies** (`DotNetDispatcher`, `ComponentFactory`, …) fail the analysis under WASM, so the gate reverted to the default; the library's analysis cleanliness is enforced at build time by `WarningsAsErrors` instead. Periodically re-try unmuzzling after SDK updates — it finds things nothing else does.
- Empirically, this ILLink/Blazor version did **not** resurface library-internal dataflow warnings at publish even when unmuzzled — `#pragma warning disable IL2075` and `[UnconditionalSuppressMessage]` are currently equivalent for both in-repo gates. Prefer the attribute anyway (now policy, see the csproj comment): it persists in metadata for other trim tooling (NativeAOT's ILCompiler for the planned AOT work, other SDK pipelines); pragmas vanish at compile time.
- SDK 10.0.300 bug: consuming WASM publishes double-discover the library's `lib.module.js` JS initializer via the static-web-assets cross-project protocol (`ApplyCompressionNegotiation` duplicate-key crash, or "Conflicting assets" with compression off). Workaround: the library csproj exposes `-p:IgbExcludeJsInitializer=true` (used by the smoke publish, which loads `app.bundle.js` manually). Re-test on SDK updates and drop the workaround when fixed.
- For line-precision suppression (attributes can't target a line): extract the offending lines into a small private helper and put `[UnconditionalSuppressMessage]` on the helper. Caution: give the helper an *unannotated* `Type` parameter — annotating it just moves the warning to the call site (verified: the requirement re-materializes at the collection read, `IEnumerator<T>.Current`).

## 6. Small items

- **`BuildSequenceInfo` attribute matching** (`BaseRendererControl.cs:~411-427, ~436-450`): matches attributes by `GetType().Name` strings; typed `is` patterns would be safer (the code hard-casts right after anyway). Reverted from the trimming PR as an unrelated cleanup — do it in its own change.
- **`AddIgniteUIBlazor(null)`** now hits CS0121 ambiguity between the `Type[]` and `Action<IgbModuleCollection>` overloads. Decide whether to care (probably not; document if anyone reports it).
- **TRIMMING.md phrasing**: "your data item types" doesn't explicitly cover *library* types bound as data (e.g. `List<IgbChatMessage>` on `IgbChat.Data`). Works in practice (their `SerializeCore` statically references every property), but could be stated.
- **`GatherSimpleAttributes` MaxDepth**: now honors `Settings.JsonSerializerOptions.MaxDepth` (default 32) instead of the framework default 64. If a direct-render description ever legitimately nests deeper, bump the library default rather than special-casing the call site.
- **Trimmed self-contained Blazor Server** (unsupported by the platform) could strip `RemoteJSRuntime.IsInitialized` and break prerender detection (`IsRuntimeValid` suppression documents this). No action unless MS makes server trimming supported.

## Closed by the event-callback branch

- The old `CompareEventCallbacks` hash-equality short-circuit (identity-hash collision could report different callbacks equal) and the misleading "fixed in .net 9" comment — both gone with `EventCallbackExtensions.EqualsCompat`, which is also trim-clean with no annotations. Reference: only .NET 10 gave `EventCallback.Equals` delegate value equality; net9's override is `ReferenceEquals`-based (verified empirically against 9.0.0/9.0.19 and 10.0.8).
