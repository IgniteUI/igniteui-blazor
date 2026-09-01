# Trimming support

`IgniteUI.Blazor.Lite` is trim-compatible (`IsTrimmable=true`): the library builds warning-free under the .NET trim analyzer, and its own serialization uses source-generated `System.Text.Json` contexts and hand-written `Utf8JsonWriter` code that needs no reflection over your types.

Two library features intrinsically depend on runtime type information that the trimmer cannot see. Applications published with `PublishTrimmed=true` (the default for Blazor WebAssembly publish) must follow the guidance below when using them.

## App-provided data types: when to preserve them

Parameters that carry your application's own types — data sources such as **`IgbCombo.Data`**, and value-carrying parameters such as `IgbTreeItem.Value` — work off the public properties and fields of those types at runtime. The trimmer cannot detect this, so unused members of your types may be removed and silently disappear from the rendered output.

Preserve the item types you bind, either with a `DynamicDependency` attribute on any kept method (e.g. your root component or `Program.Main`):

```csharp
[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields, typeof(MyDataItem))]
```

or by annotating the type itself:

```csharp
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)]
public class MyDataItem { ... }
```

or with a [trimmer root descriptor](https://learn.microsoft.com/dotnet/core/deploying/trimming/trimming-options#root-descriptors) listing the types.

Preservation must cover **every complex type reachable from the item type**, not just the root: if `MyDataItem` has an `Address` property whose members the component renders, `Address` needs the same treatment — the schema builder reflects over nested object types as it encounters them.

## Module preloading: trim-safe by design

Module preloading works unchanged under trimming:

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTreeModule), typeof(IgbComboModule));
```

Every library `*Module` class carries a self-referencing `[IgbModule<TSelf>]` attribute that makes the trimmer preserve the module's registration surface whenever the type itself is kept — so `Type`-based preloading works no matter how the `Type` value reaches the library (a `typeof` argument, an array, a value computed at runtime), while modules the app never references still trim away entirely.

The only caveat is **third-party module types**: a custom class with a `Register(IIgniteUIBlazor)` method passed by `Type` doesn't carry the attribute. Such types can opt into the same preservation by implementing `IIgbModule` and marking themselves with `[IgbModule<TSelf>]`, or the app can preserve their `Register` with its own annotations.

## Native AOT

The library's code is AOT-clean: it builds warning-free under the [AOT analyzer](https://learn.microsoft.com/dotnet/core/deploying/native-aot/) (`EnableAotAnalyzer`, errors via `.editorconfig`), verified under real ILC by the AotSmoke gate. The public `IsAotCompatible` flag is deliberately **not** set yet — Blazor itself is not AOT-compatible (`Microsoft.AspNetCore.Components` ships `IsTrimmable` only; [dotnet/aspnetcore#51598](https://github.com/dotnet/aspnetcore/issues/51598) tracks it), so the claim would outrun the platform; flip it when that lands. What this means per deployment model:

- **Blazor WebAssembly** — unaffected either way: both the default interpreter and `RunAOTCompilation=true` publishes use Mono AOT with the interpreter retained, so no NativeAOT semantics apply.
- **NativeAOT (ILC)** — the library's expression-tree getters run in `System.Linq.Expressions`' interpreted form (a documented NativeAOT limitation: slower, not broken), and all generic instantiations the library creates at runtime are statically visible or reference-type-shared. Note ILC deployment of *Blazor apps* is not a supported platform scenario today (ASP.NET Core NativeAOT excludes Blazor; MAUI BlazorWebView under `PublishAot` is undocumented upstream and unverified) — the analyzer-clean code positions the library for those consumers as they materialize.
- The trimming guidance above (preserving data item types) applies identically under AOT.

## Maintaining AOT compatibility (contributors)

AOT diagnostics (IL3xxx) build as errors like trim ones (`dotnet_analyzer_diagnostic.category-AOT.severity = error`). Follow [intrinsic RequiresDynamicCode APIs](https://learn.microsoft.com/dotnet/core/deploying/native-aot/intrinsic-requiresdynamiccode-apis) and these repo rules:

1. **Avoid dynamic code outright** where a static shape exists.
2. **Expression trees: the delegate type must be statically known** — use `Expression.Lambda<TDelegate>(...)` or `Expression.Lambda(Type delegateType, ...)` with a `typeof` literal (see the closed delegate-type map in `JsonDataSourceSchema`). Never `Expression.GetFuncType` or the non-generic `Lambda(body, params)` overloads — those are the `RequiresDynamicCode` sites; `Compile()` itself is AOT-safe (interprets).
3. **`MakeGenericType`/`MakeGenericMethod` only over closed sets** that are statically visible or class-constrained (the net9+ analyzer proves the `where T : class` pattern; net8 needs a narrow suppression).
4. **Suppression policy**: `[UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = ...)]` on the smallest member — the ID is what ILC matches; the justification states the runtime invariant. Same no-`#pragma` rule as trimming.
5. **Verify under real ILC**: `tests/IgniteUI.Blazor.Lite.AotSmoke` (see its README) — CI publishes and runs it; locally `dotnet run -p:SimulateNoDynamicCode=true` covers the interpreter paths without the native toolchain.

## Maintaining trim compatibility (contributors)

The library must stay trim-clean. Every trim-analysis diagnostic builds as an error (`dotnet_analyzer_diagnostic.category-Trimming.severity = error` in the repo `.editorconfig`; category bulk-config covers future IL2xxx codes, inert where the analyzer is off). The analyzer cannot validate suppression justifications or retention-based mechanisms like `[IgbModule<TSelf>]` — `tests/IgniteUI.Blazor.Lite.PublishSmoke` is the authoritative check for those, automated as the `TrimmedPublish`-category browser facts in `IgniteUI.Blazor.Lite.IntegrationTests` (net10.0, runs in CI); its README has the manual checklist covering the other TFMs.

When the analyzer flags new code, follow the standard playbook — avoid reflection and dynamic code where possible, otherwise annotate or fix the root cause, and suppress only as a last resort: see [preparing libraries for trimming: recommendations](https://learn.microsoft.com/dotnet/core/deploying/trimming/prepare-libraries-for-trimming#recommendations) and [resolving trim warnings](https://learn.microsoft.com/dotnet/core/deploying/trimming/fixing-warnings). Repo-specific rules on top:

1. **Serialization goes through source-generated `JsonTypeInfo`** — extend `IgbJsonContext` rather than calling reflection-based `JsonSerializer` overloads.
2. **Never annotate *method parameters or fields* of component base classes** with `[DynamicallyAccessedMembers]` — `OpenComponent<T>` roots component members "via reflection" and the annotation then surfaces as IL2111/IL2110 in every consuming app (annotated *properties* are fine).
3. **Suppress narrowly** — `[UnconditionalSuppressMessage]` on the smallest member with a justification that states why the pattern is safe; extract a small helper if needed so the justification matches exactly what the member does.
4. **Never use `#pragma` for ILxxxx** — it silences only the build analyzer and leaves no metadata for publish-time trim tooling (ILLink, NativeAOT's ILCompiler).
