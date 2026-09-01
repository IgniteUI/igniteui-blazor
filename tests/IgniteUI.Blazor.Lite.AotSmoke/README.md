# AotSmoke — NativeAOT (ILC) verification app

A console app that publishes `IgniteUI.Blazor.Lite` **under real NativeAOT** and runs the `RequiresDynamicCode`-adjacent paths. The build-time AOT analyzer only checks library source against reference assemblies — ILC at publish sees the whole closed program (IL3052–IL3055 exist only there), and no analyzer verifies runtime behavior: that expression getters produce correct values interpreted, that every closed-set `Func<object, T>` instantiation is actually pregenerated, that suppression justifications hold under ILC. This app is to AOT what PublishSmoke's browser checklist is to trimming.

The csproj roots the whole library (`TrimmerRootAssembly`), so ILC analyzes all of it — not just what `Main` reaches — and any ILC warning fails the publish (`ILLinkTreatWarningsAsErrors`).

## What Main checks (asserted; success = exit code 100, the aspnetcore trimming-test convention — a silent early exit with the default 0 cannot pass)

- Reflection-built schema over a user POCO (preserved via the docs/TRIMMING.md pattern): every untyped and typed getter across the closed delegate set — int/double/string/DateTime/bool/decimal, enum→underlying conversion, nullables, and the public-field getters.
- Dictionary-shaped data: indexer reflection + typed dictionary getters.
- `ExtractSchema`/`ExtractSchemaFromType` entry points, the `MarshalByValueFactory` switch, and an `IgbJsonContext` round-trip.

## Run it

```bash
# Real thing (CI runs this on linux-x64; locally needs the native toolchain —
# VS "Desktop development with C++" on Windows, clang + zlib1g-dev on Linux):
dotnet publish tests/IgniteUI.Blazor.Lite.AotSmoke -c Release -r win-x64
tests/IgniteUI.Blazor.Lite.AotSmoke/bin/Release/net10.0/win-x64/publish/IgniteUI.Blazor.Lite.AotSmoke.exe

# No native toolchain: run on CoreCLR with dynamic code disabled — forces the same
# interpreter paths ILC uses at runtime (does NOT validate ILC analysis or codegen):
dotnet run --project tests/IgniteUI.Blazor.Lite.AotSmoke -c Release -p:SimulateNoDynamicCode=true
```

The wasm-AOT consumer scenario (Mono AOT, interpreter retained) is separate and gated by the manual `Wasm AOT Smoke` workflow — it validates the Blazor product path but emits no ILC diagnostics.
