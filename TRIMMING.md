# Trimming support

`IgniteUI.Blazor.Lite` is trim-compatible (`IsTrimmable=true`): the library builds warning-free under the .NET trim analyzer, and its own serialization uses source-generated `System.Text.Json` contexts and hand-written `Utf8JsonWriter` code that needs no reflection over your types.

Two library features intrinsically depend on runtime type information that the trimmer cannot see. Applications published with `PublishTrimmed=true` (the default for Blazor WebAssembly publish) must follow the guidance below when using them.

## Data sources: preserve your data item types

Components that accept data through an `object`-typed `Data` parameter (`IgbCombo`, `IgbTree`, `IgbChat`, `IgbCarousel`, `IgbSplitter`, …) build their schema by reflecting over the public properties and fields of your item type at runtime. The trimmer cannot detect this, so unused members of your item types may be removed and silently disappear from the rendered data.

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

`AddIgniteUIBlazor` takes typed module references (`IgbModuleRef`) that convert implicitly from `Type`, so the familiar call shape keeps working — and is trim-safe:

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTreeModule), typeof(IgbComboModule));
```

The conversion's annotated parameter tells the trimmer to preserve each referenced module's registration surface at the call site (verified empirically: the same preload silently no-ops when made through a raw unannotated `Type`). The types must implement `IIgbModule` — all library `*Module` classes do. The fluent collection overload is equivalent and reflection-free:

```csharp
builder.Services.AddIgniteUIBlazor(m => m.Add<IgbTreeModule>().Add<IgbComboModule>());
```

Two caveats:

- Pass `typeof(...)` expressions (or values you annotated with `DynamicallyAccessedMemberTypes.PublicMethods` yourself) — an untraceable `Type` variable produces an IL2072 warning in your app, which is the analyzer telling you the preservation cannot be guaranteed.
- A pre-built `Type[]` array no longer compiles against the params (arrays don't apply element conversions) — use `IgbModuleRef[]` or expanded arguments instead.

The legacy `IgniteUIBlazorSettings.WithModulesToLoad` Type collection still registers via reflection and remains trim-unsafe for modules whose component the app never uses statically; prefer the call shapes above.

## Native AOT

Not supported yet. The data-source layer compiles expression-tree getters (`RequiresDynamicCode`), which is a planned follow-up; trimming and wasm AOT compilation of the interpreter-hosted kind are unaffected.
