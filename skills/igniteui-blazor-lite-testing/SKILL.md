---
name: igniteui-blazor-lite-testing
description: "Testing in the IgniteUI Blazor (Lite) repository itself: the bUnit unit suite (tests/IgniteUI.Blazor.Tests — base classes, attribute/serialization specs, declarative interop wire contracts) and the Playwright integration suite (tests/IgniteUI.Blazor.Lite.IntegrationTests + TestBed — reflection-driven live-browser sweep, componentsConfig.json). Use when adding or changing tests in this repo, covering a new component, pinning interop behavior, or deciding which suite a check belongs in. Not for consumer-facing component usage — use igniteui-blazor-components for that."
user-invocable: true
---

# Testing in the IgniteUI Blazor (Lite) repo

Three test projects under `tests/`, two suites:

| Project | Suite | What it is |
|---|---|---|
| `IgniteUI.Blazor.Tests` | **Unit** | xUnit + bUnit, multi-targeted `net8.0`/`net9.0`/`net10.0`; renders components in-process with a recording JS runtime — no browser |
| `IgniteUI.Blazor.Lite.IntegrationTests` | **Integration** | NUnit + Playwright; one generated test fixture per component |
| `IgniteUI.Blazor.Lite.TestBed` | (integration host) | Blazor app the integration tests drive; renders one component per run and sweeps its surface |

## How the suites divide the work

| Behavior | Where it's tested |
|---|---|
| Property → rendered attribute (direct-render components) | unit: plain bUnit facts (`cut.Find(...).GetAttribute(...)`) |
| Markup/child content rendering | unit: plain bUnit facts |
| **Parent/child collection membership** — a child registering itself into its parent's collection and leaving again over the child's lifetime | unit: plain bUnit facts in the *parent's* suite, in a `#region Child collection lifecycle` — membership only; name resolution *off* that collection is interop behavior and stays in the contract |
| Message-borne state serialization shapes | unit: `PropertySerializationTests` / `EnumSerializationTests` / `RenderingSerializationTests` |
| **Interop wire behavior** — method identifiers, argument serialization + type tags, return decoding, event-handler registration/removal transmissions + JS→.NET event dispatch, `@bind-` two-way round trips, interop-borne props/data | unit: the component's `ComponentContract` — full authoring guide: [`references/interop-contracts.md`](./references/interop-contracts.md) |
| `<Member>Script` parameters (JS-side handlers/providers) | unit: automated sweep (`ScriptPropTests`, all components at once — no per-contract entries, no integration coverage exists) |
| End-to-end prop/event/method behavior against the **real web component in a real browser** | integration: the TestBed sweep |
| Visual output, client-side component logic | integration / e2e — never unit |

The two suites overlap on purpose but answer different questions: integration proves the full pipeline works end-to-end (but can't attribute a failure to a side of the boundary, and skips everything excluded in `componentsConfig.json`); interop contracts pin the .NET side of the wire protocol at unit speed, per member — including the members integration excludes — and can accommodate different implementations via the `InteropHarness` seam (`InteropHarnessRegistry` swaps stacks per component).

## Unit suite (`tests/IgniteUI.Blazor.Tests`)

### Base classes

- **`BlazorComponentTestBase`** — bUnit `BunitContext` with setup `IIgniteUIBlazor` service, a recording JS runtime (`JSRuntimeMode.Loose`; every invocation recorded) and an `Interop` harness property (an `InteropHarness`, resolved per component type via `InteropFor<TComponent>()`). Default base for suites without an interop contract.
- **`ComponentWithContractTestBase<TComponent>`** — adds a declarative `ComponentContract<TComponent>`, `protected` runners (`VerifyMethodContract`/`VerifyPropContract`/`VerifyEventContract`) that the suite exposes as one-liner `[Fact]`s, and an inherited `Contract_SectionsHaveFacts` guard that fails if a non-empty contract section has no runner fact. Exactly one suite per component carries the contract; sync/async method pairs are declared together via the twin overloads.

### Running

```
dotnet test tests/IgniteUI.Blazor.Tests -f net10.0 --nologo --filter "FullyQualifiedName~<Name>Tests"
```

- Iterate on one TFM (`-f net10.0`); finish with a full run on all TFMs (drop `-f`).
- Contract failure stacks carry the violated contract line as the top frame — the test UI navigates to the spec, not the shared runner.

### Conventions

- Generally follow **one file per component: `<Name>Tests.cs` holds `<Name>Tests`.** A component's **child** components may share the parent's file (`SelectTests.cs` also holds `SelectItemTests`/`SelectGroupTests`/`SelectHeaderTests`; likewise Card, List, Dropdown, NavDrawer, Tabs, Stepper, TileManager, Tree, Rating, Slider) — that's the only permitted multi-class file. Cross-cutting suites (`BaseControlTests`, `*SerializationTests`, `ScriptPropTests`, `Interop*Tests`) are keyed to a class/behavior, not a component, and keep their own files.
- New facts go into the existing `<Name>Tests` class; if the class carries a contract, plain bUnit facts sit alongside the contract runners in the same class.
- Shared test helpers live in `ElementAssertionExtensions.cs`.
- **Readable arrange/act/assert in the test beats sharing.** A little repetition across suites is fine; extract obvious repeated mechanics if needed, but ensure those are well documented and easy to grasp without breaking the flow of the test itself.
- **Skips need a mechanism reason.** Bug-tracker or `componentsConfig.json` listings are not skip reasons. If a member's decode hits a gap in shared infrastructure the change doesn't own, cover it pinning current behavior and write the *correct* assertion commented out under a `// TODO:` explaining the gap — ready to uncomment when fixed. A gap in a component under construction is a bug to fix, not to pin — see the two authoring modes in the interop reference.
- Components are `partial` — the generated `src/components/Blazor/<Name>.cs` is not the whole class; always check `src/componentsBase/WebInputs/<Name>.cs` for hand-written extensions before drawing conclusions.
- Formatting: only `dotnet format whitespace --folder` is safe in this repo — a full `dotnet format` writes conflict markers into multi-targeted sources.

## Integration suite (Playwright + TestBed)

One generic NUnit test per component (`ComponentTest`, fixture-sourced from the TestBed's
component list): it boots the TestBed app (in-memory by default), calls the browser-side
`renderComponent('<IgbName>')`, and asserts `getErrors()` returns nothing. The real work
happens inside the TestBed (`Components/Pages/Home.razor`): a **reflection-driven sweep**
over the component's public surface —

- **`TestProps`**: sets each `[Parameter]` server-side with generated sample values and compares against the live web component's DOM property in the browser (`TestUtil.PropertyValuesAreEqual`, client value via `stringifyObject`).
- **`TestEvents`**: dispatches synthetic events against the web component and verifies the bound .NET callbacks fire.
- **Method spies** (`spyOnMethod`/`checkOnSpy` in `wwwroot/app.js`): invoking the .NET API must call the matching web-component method with the right args/result. **Async methods only** — the sweep filters to `*Async` and also excludes `Get*Async` getters; sync variants can't run on the Server-hosted TestBed (no in-process JS runtime), so sync variants, getters, and `<Member>Script` params have no integration coverage — the unit suite is their only net.

**`componentsConfig.json`** (in the TestBed project) gates the sweep per component:
`ExcludedProps`, `ExcludedEvents`, `DependantProps`/`DependantMethods` — each entry usually cites a BUG number or a shape the generic sweep can't model. An excluded member has **no integration coverage at all** — treat those as *higher* priority for the unit interop contract, never as members to skip there too.

### Running

- One-time: build, then install browsers — `pwsh tests/IgniteUI.Blazor.Lite.IntegrationTests/bin/Debug/net<version>/playwright.ps1 install chromium`.
- `dotnet test tests/IgniteUI.Blazor.Lite.IntegrationTests` (or per component from the Test Explorer — fixtures are named by component).
- `.runsettings` knobs: headless off for local debugging; `useInMemoryClient: false` to run against your own `dotnet run` instance of the TestBed.
- Full setup details: [tests/IgniteUI.Blazor.Lite.IntegrationTests/README.md](../../tests/IgniteUI.Blazor.Lite.IntegrationTests/README.md).

## Adding coverage for a new component — checklist

1. Unit suite: bUnit facts for rendered attributes/markup; if the component has an interop surface (it sends interop invocations or registers JS-originated event handlers — the reference explains how to identify these per stack), add a `ComponentContract` per [`references/interop-contracts.md`](./references/interop-contracts.md). For a brand-new component this works TDD-style: author the contract first from the intended API following the library's conventions; the contract doubles as the API design record (spec mode in the reference).
2. If it is a parent that collects children, or a child that registers with a parent, exercise that membership **over the child's lifetime**, not just at first render: the children are the collection in order, a disposed child leaves it, all disposed empties it. Keep these facts to membership and asserting resolution that are covered by the interop contract.
3. Integration: the sweep picks the component up automatically; add  `componentsConfig.json` entries only for members the generic sweep genuinely cannot  model (cite why), and mirror each exclusion with unit contract coverage.
4. Verify: unit full-suite on all TFMs, plus the component's integration fixture.
