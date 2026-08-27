# Nullable reference type migration plan

`src/IgniteUI.Blazor.Lite.csproj` sets `<Nullable>disable</Nullable>`, overriding the repository-wide `<Nullable>enable</Nullable>` in [`Directory.Build.props`](../Directory.Build.props). This document is the accepted plan for removing that override, and the record of why it exists in the meantime.

Tracked in [#347](https://github.com/IgniteUI/igniteui-blazor/issues/347).

## Current state

Everything else in the repository — tests, the stories host, the test bed — compiles with nullable analysis enabled. Only the shipped library opts out.

The component wrappers under `src/components/Blazor` were carried forward from the pre-open-source `IgniteUI.Blazor` codebase, which predates nullable reference types. They are hand-maintained rather than generated, so there is no generator to teach and no regeneration that fixes them: annotating them means editing them. There are over a hundred such files, and turning the flag on today produces thousands of warnings across a public API surface.

## Consequence for consumers

The package's public API ships without nullability annotations. A consumer compiling with nullable enabled sees the library's reference types as *oblivious* — neither nullable nor non-nullable — so the compiler will not warn them about passing `null` to a parameter that does not accept it, nor about dereferencing a return value that may be `null`.

This is a real gap in the API contract and the reason the flag is worth turning on, not merely a build-log annoyance.

## Why not simply enable it

Two reasons, and only the second is about effort.

Enabling nullable analysis on a public API is an API change. Annotating a parameter as non-nullable makes previously-accepted `null` a warning at every call site, and annotating a return as nullable makes previously-clean consumer code warn. Done in one commit across the whole surface, that is a large and untestable diff arriving in a single release. Done wrongly — annotating for what makes the warnings go away rather than for what the code actually permits — it bakes an incorrect contract into the package that is then itself a breaking change to correct.

The second reason is that the warnings are not uniformly interesting. A large fraction come from a small number of patterns in the shared base classes, and fixing those first shrinks the remainder substantially. Ordering the work matters.

## Plan

The migration is staged per folder, enabling `#nullable enable` at file scope so that each stage is independently reviewable and independently revertable. The project-level `<Nullable>disable</Nullable>` stays until the last stage lands.

1. **Interop and serialization core** — `src/componentsBase/*.cs`. The base classes, the renderer, the serializer, and the data adapters. This is where the nullability contract actually lives; most component-level warnings are downstream of decisions made here.
2. **Input infrastructure** — `src/componentsBase/WebInputs/*.cs`.
3. **Component wrappers** — `src/components/Blazor`, in alphabetical batches sized to a reviewable pull request. Public parameters and event callbacks are annotated to match the behaviour of the underlying custom element, not to silence the compiler.
4. **Flip the project** — remove the `<Nullable>disable</Nullable>` override and, in the same change, add `<WarningsAsErrors>Nullable</WarningsAsErrors>` so the state cannot regress.

## Acceptance criteria

- Every stage builds clean on `net8.0`, `net9.0` and `net10.0` with no new suppressions beyond a documented, justified `!` at a genuine interop boundary.
- No `#pragma warning disable` for nullable warnings survives into the shipped source.
- The public API surface is annotated to reflect what the component actually accepts and returns.
- After stage 4, `<Nullable>enable</Nullable>` is inherited from `Directory.Build.props` and the override is gone.

## Interim commitment

Until stage 4 lands, new files added to the library carry `#nullable enable` at file scope so the annotated surface only grows. This is the practical half of the plan: it prevents the backlog from getting larger while the existing backlog is worked through.
