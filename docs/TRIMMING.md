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

Not supported yet. The data-source layer compiles expression-tree getters (`RequiresDynamicCode`), which is a planned follow-up; trimming and wasm AOT compilation of the interpreter-hosted kind are unaffected.

## Maintaining trim compatibility (contributors)

The library must stay trim-clean. Every trim-analysis diagnostic builds as an error (`dotnet_analyzer_diagnostic.category-Trimming.severity = error` in the repo `.editorconfig`; category bulk-config covers future IL2xxx codes, inert where the analyzer is off). The analyzer cannot validate suppression justifications or retention-based mechanisms like `[IgbModule<TSelf>]` — `tests/IgniteUI.Blazor.Lite.PublishSmoke` is the authoritative check for those, automated as the `TrimmedPublish`-category browser facts in `IgniteUI.Blazor.Lite.IntegrationTests` (net10.0, runs in CI); its README has the manual checklist covering the other TFMs.

When the analyzer flags new code, follow the standard playbook — avoid reflection and dynamic code where possible, otherwise annotate or fix the root cause, and suppress only as a last resort: see [preparing libraries for trimming: recommendations](https://learn.microsoft.com/dotnet/core/deploying/trimming/prepare-libraries-for-trimming#recommendations) and [resolving trim warnings](https://learn.microsoft.com/dotnet/core/deploying/trimming/fixing-warnings). Repo-specific rules on top:

1. **Serialization goes through source-generated `JsonTypeInfo`** — extend `IgbJsonContext` rather than calling reflection-based `JsonSerializer` overloads.
2. **Never annotate *method parameters or fields* of component base classes** with `[DynamicallyAccessedMembers]` — `OpenComponent<T>` roots component members "via reflection" and the annotation then surfaces as IL2111/IL2110 in every consuming app (annotated *properties* are fine).
3. **Suppress narrowly** — `[UnconditionalSuppressMessage]` on the smallest member with a justification that states why the pattern is safe; extract a small helper if needed so the justification matches exactly what the member does.
4. **Never use `#pragma` for ILxxxx** — it silences only the build analyzer and leaves no metadata for publish-time trim tooling (ILLink, NativeAOT's ILCompiler).
