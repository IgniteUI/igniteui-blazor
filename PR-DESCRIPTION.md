# feat: make IgniteUI.Blazor.Lite trim-compatible

## What & why

Enabling [`<IsTrimmable>true</IsTrimmable>`](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/prepare-libraries-for-trimming) surfaced 48 unique trim-analysis warnings per TFM (net8/net9/net10), concentrated in the JSON interop layer and the reflective data-source/module plumbing. This PR resolves all of them — real fixes where possible, narrowly-justified suppressions only where the reflection is over types the app supplies at runtime — so the library is safe to consume from apps published with [trimming](https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/configure-trimmer) (the Blazor WebAssembly publish default).

Based on `dpetev/event-callback-compare-fix`: its `EventCallbackExtensions.EqualsCompat` replaces the old reflective `CompareEventCallbacks` and is already trim-clean under the new analyzer gate with no annotations (`typeof(EventCallback<TValue>).GetField` is statically analyzable).

## Changes

### Real fixes

- **JSON source generation** ([docs](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)): new `IgbJsonContext` (`src/componentsBase/IgbJsonContext.cs`) covers the closed set of wire shapes (`Dictionary<string,object>`, its array, `object`, `object[]`, `string`, `string[]`, `int[]`, `double[]`); all ~25 `JsonSerializer` call sites in `BaseRendererControl` now use typed `JsonTypeInfo` overloads — the IL2026s are gone for real, not suppressed. Deserialized `object` values still surface as `JsonElement`, identical to the reflection-based behavior.
- **`DateRangePicker` Change handler**: the `Serialize(args.Detail) → Deserialize<IgbDateRangeValue>` round-trip (with a per-event `JsonSerializerOptions` allocation) is replaced by a direct `Start`/`End` copy — the two types share the exact shape.
- **Trim-safe module registration**: new `IIgbModule` interface (`static abstract Register`) implemented by all 75 `*Module` classes, plus two registration shapes that survive trimming: the fluent `AddIgniteUIBlazor(m => m.Add<IgbTreeModule>())` collection (zero reflection), and — replacing the `params Type[]` overloads — `params IgbModuleRef[]` with an implicit conversion from `Type`, so existing `AddIgniteUIBlazor(typeof(IgbTreeModule))` call sites compile unchanged while the conversion's `[DynamicallyAccessedMembers]` parameter makes the trimmer preserve each module's `Register` per call site (raw `typeof` in a `Type[]` roots the type but **not** the method — verified empirically, the preload silently no-ops). `IgbModuleRef` validates the type implements `IIgbModule`. The legacy `WithModulesToLoad` settings path remains reflective for back-compat (suppressed + documented).

### Annotations ([DynamicallyAccessedMembers](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/fixing-warnings))

- `BaseRendererControl` class: `PublicProperties` — inherited by every component, satisfies the `BuildSequenceInfo` property walk; costs nothing real since rendered components already get `All` via `OpenComponent<T>`.
- `Utils.TryGetWCEnumName` / `ObjectToParam(…, Type, …)` chain: `PublicFields` (all callers pass `typeof` literals).
- `IgbComponentRendererContainer.ComponentType` (+ backing field) and `DynamicContentInfo.ControlType` / `TypedDynamicContent(Type)`: `All`, as required by `RenderTreeBuilder.OpenComponent(Type)`.
- `UnmarshalledDataSource.GetIListTypeArg`/`GetIEnumerableTypeArg`: `Interfaces`.

### Suppression inventory (each `[UnconditionalSuppressMessage]`, with justification)

| Site | Code | Why it is safe |
|---|---|---|
| `BaseRendererControl.InvokeSendMessageSync` | IL2026 | Arguments are strings, `DotNetObjectReference`, `ElementReference[]`; the return is only consumed as opaque `JsonElement` — no user types cross the JS interop boundary. |
| `BaseRendererControl.GetWCEnumTransform` (helper extracted from `BuildSequenceInfo`) | IL2070 | ILLink preserves all fields of kept enum types; enum parameter property types are kept with their declaring component. Validated in the trimmed browser smoke (enum values render as camelCase strings, not numbers). |
| `RendererSerializer.AddEnumProp` | IL2075 | Same enum-field preservation. |
| `Utils.TryGetWCEnumName` | IL2070 | Same enum-field preservation. Deliberately suppressed instead of annotated: DAM-annotated *method parameters* on component base classes trigger IL2111 in every consuming app, because `OpenComponent<T>` roots component members "via reflection". |
| `IgniteUIBlazor` ctor (legacy settings `Register` loop) | IL2075 | Reflection over the legacy `WithModulesToLoad` Type collection; the `IgbModuleRef`/collection call shapes are the trim-safe paths — documented in TRIMMING.md. |
| `IgniteUIBlazor.IsRuntimeValid` | IL2075 | Probes optional `RemoteJSRuntime.IsInitialized`; in supported trim scenarios (WASM, MAUI BlazorWebView) the type doesn't exist and the probe correctly finds nothing. A string `DynamicDependency` can't be used (IL2035 when the Server assembly is absent). |
| `RuntimeHelper` ctor | IL2075, IL2060, IL2026 | net8-only `InvokeUnmarshalled` probe (API removed from the framework in net9+), preserved via the existing `DynamicDependency`; absence degrades to the raw-pointer `InvokeVoid` path that is the only path on net9+. IL2026: `GetMethods()` reflection-marks the runtime's RUC members (net10 `GetValue`/`SetValue`/…), which the probe never invokes. |
| `IgbComponentRendererContainer.ComponentType` | IL2078 | The backing field is only assigned through the annotated property; annotating the field itself would expose IL2110 to consuming apps. |
| `JSDataSourceSchema.GetPropertiesFromType`/`GetFieldsFromType` | IL2067 | Data-source boundary: item types are supplied by the app at runtime — documented requirement in TRIMMING.md. |
| `JSDataSourceSchema.CreateFromDictionary` | IL2075 | Same boundary. |
| `UnmarshalledDataSource.ExtractSchema` / `ExtractSchemaFromType` | IL2072 / IL2067 | Same boundary. |

### Guards & docs

- Library csproj: the full IL2xxx trim-analyzer code range is now `WarningsAsErrors` — newly-added trim-unsafe code fails the build.
- New **`TRIMMING.md`** (linked from README): consumer guidance — preserving data item types (including nested complex types), the trim-safe module registration overload, AOT status.
- New **`tests/IgniteUI.Blazor.Lite.PublishSmoke`**: Blazor WASM app publishing the library trimmed with `TrimmerSingleWarn=false` + `ILLinkTreatWarningsAsErrors=true`; named publish-scoped so the future AOT pass reuses it.

## Verification

- `dotnet build` (all 3 TFMs, rebuild): **0 IL warnings** with the warnings-as-errors gate active.
- `dotnet test`: 958 passed / 0 failed on each of net8.0, net9.0, net10.0.
- `dotnet publish` of PublishSmoke: **0 ILLink warnings** — with `SuppressTrimAnalysisWarnings=false` (Blazor WASM suppresses linker analysis warnings by default; the smoke app opts back in), single-warn off, warnings-as-errors on.
- Headless-browser smoke against the trimmed publish output: enum attributes render as camelCase strings (`variant="outlined"`, `shape="circle"` — proves enum fields survive trimming), the combo binds a 3-item reflected POCO list preserved per the TRIMMING.md pattern, and a dispatched `igcChange` round-trips through the new date-range copy path into rendered output.
- Module-registration semantics verified empirically with isolated trimmed publishes of an app whose preloaded module's component is never statically used (`IgbChatModule`): a raw `Type[]` preload silently loses `Register` (**False**); the same call shape through `IgbModuleRef`'s annotated implicit conversion preserves it (**True**) — the annotation rides the struct's annotated property through collections, unlike type-level `[DynamicallyAccessedMembers]` (on the interface or the module class), which is flow-dependent and does *not* fix a `typeof` dying in an unannotated `Type[]` (also verified). The smoke app permanently exercises `Add<T>`, `Add(typeof(...))`, and the settings overload.
- Smoke publish uses `-p:IgbExcludeJsInitializer=true`, a property-gated workaround for an SDK 10.0.300 static-web-assets bug that double-discovers the library's JS initializer in consuming WASM publishes (see the csproj comment).

## Behavioral notes

- `GatherSimpleAttributes` previously deserialized with no options (default `MaxDepth` 64); it now honors `Settings.JsonSerializerOptions.MaxDepth` (library default 32) like its sibling call sites.
- **Breaking (module overloads)**: replacing `params Type[]` with `params IgbModuleRef[]` is binary-breaking; source stays compatible for expanded `typeof(...)` arguments, but a pre-built `Type[]` array no longer compiles (arrays don't apply element conversions — use `IgbModuleRef[]` or expanded args), and non-`IIgbModule` types now throw `ArgumentException` at registration instead of being silently probed. A module listed both in legacy settings and in the params has `Register` invoked once per path (client-side loading dedupes by module name).
- `AddIgniteUIBlazor(null)` is now a compile-time ambiguity (CS0121) between the params and delegate overloads; unusual pattern, no runtime impact.
- Object-initializing `IgbDateRangeValue` in the Change handler triggers two BL0005 analyzer warnings (parameter set outside component) — same class of pre-existing warnings as `Chat.cs`/`Tabs.cs`.

Follow-ups are tracked in `PLAN-TRIMMING-FOLLOWUPS.md` (AOT/`RequiresDynamicCode`, smoke-app coverage, stale-docs cleanup).

🤖 Generated with [Claude Code](https://claude.com/claude-code)
