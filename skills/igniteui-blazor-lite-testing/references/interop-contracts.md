# Interop wire contracts — authoring reference

Goal: every component with an interop surface carries a declarative `ComponentContract<TComponent>` in its test suite, pinning how its public API maps onto the wire — method identifiers, argument serialization and type tags, return decoding, and event names + args deserialization. Contracts run through the `InteropHarness` seam (`tests/IgniteUI.Blazor.Tests/Interop/`), so the contract verifies a component regardless of the interop stack (swap per component via `InteropHarnessRegistry`).

Scope note: contracts cover **methods, current-state getters, events, and props that travel over interop**. Simple property → attribute rendering (direct-render components) is already covered by the existing attribute tests and the integration suite — never duplicate it with `.Prop`.

Exemplars to imitate: `ButtonTests.cs` (simplest), `CarouselTests.cs` (void/bool methods, args, getters, number event), `ChipTests.cs` (bool-detail events, getter), `ChatTests.cs` (renderer-container component: `.Prop` config object, complex event args), `ComboTests.cs` (data sources, uuid refs), `TreeTests.cs` (child refs, hosted getter).

Cross-check the integration exclusions: members listed for the component in `tests/IgniteUI.Blazor.Lite.TestBed/componentsConfig.json` (`ExcludedProps`, `DependantMethods`, `ExcludedEvents`) have **no integration coverage at all** — treat them as higher priority for the contract, never as members to skip. Client-side bugs cannot reach the stubbed boundary, so the contract covers the public API as exposed today; reference the bug in a note (not the skip comment) for context.

**The file is not the whole class.** `partial` classes may have split implementations — always check `src/` for other parts.

## Two authoring modes — know which one you're in

- **Pin mode** — the component exists and is presumed working (retrofitting coverage, gating a migration). Author specs from the source and run them; when a spec disagrees with what actually crossed (failure output shows the recorded wire values), judge which side is off. Usually the spec mis-derived a wire form — correct it. When the *implementation* looks wrong instead, decide whether there is an issue that can't be asserted correctly right now: if so, assert what holds today and leave the correct assertion commented out under a `// TODO:` explaining the issue (exemplars: `StepperTests` `GetSteps`, `ComboTests` `ChangeType`; grep `TODO` in the test project — every hit is an uncomment-when-fixed marker).
- **Spec mode (TDD / new component / intended behavior change)** — the contract is the reference and is written *first*, from the intended public API and the stack's wire conventions (cheat-sheet). Where the component wraps a web component, its emitted shapes (`node_modules/igniteui-webcomponents` `.d.ts` / `custom-elements.json`) supply the payload truth. For a **Blazor-specific component with no web-component counterpart**, the API and the wire vocabulary are themselves design deliverables: propose them from the component's requirements, patterned on the library's conventions (`*Async` methods with sync twins, `EventCallback<TArgs>` with a dedicated args type, camelCase wire identifiers, the existing type-tag vocabulary) and the closest existing contracts as exemplars — the contract then doubles as the reviewable API design record.

The recipe below is phrased for pin mode ("read the source"); in spec mode, substitute the intended API design for the source — everything else (the DSL, the guards, the payload derivation from WC metadata) applies unchanged.

## Where a contract lives

Each component has (at most) **one** suite carrying the contract:

- If the component already has a test class, change its base from `BlazorComponentTestBase` to `ComponentWithContractTestBase<Igb<Name>>`, add `using IgniteUI.Blazor.Tests.Interop;`, and add the `Contract` property at the top. Leave all existing facts untouched. `<Name>Tests` lives in `<Name>Tests.cs` — except for **child** components, whose suite may shares the parent's file (`SelectItemTests` is in `SelectTests.cs`).
- If no suite exists anywhere, create `<Name>Tests.cs` with the contract.
- Components with **no interop surface** (they never send an interop invocation and register no JS-originated event handlers — e.g. Badge) stay on `BlazorComponentTestBase`.

## Recipe — pin the component's interop surface

Read the component's source (all `partial` parts) and find every place it:

1. **sends an outbound interop invocation** from a public API member → a `.Method` or `.Getter` spec;
2. **registers a handler for a JS-originated event** (paired with an `EventCallback<TArgs>` parameter) → an `.Event` spec;
3. **transmits state over interop** rather than rendering it as an attribute → a `.Prop` spec.

The *concrete idioms* for all three depend on which interop stack the component currently sits on — the [cheat-sheet below](#current-stack-cheat-sheet-legacy-renderermessage-pipeline) lists them for the legacy `RendererMessage` pipeline. For a component on a different stack, find the equivalent call sites in its implementation (whatever sits between the public member and the JS runtime — a direct `IJSRuntime.InvokeAsync`, a channel service, etc.); the principles and the DSL below are unchanged.

### 1. Methods and getters

- Every public API member that produces an outbound invocation gets a spec. Distinguish two kinds:
  - **Actions** → `.Method(c => c.NextAsync(), c => c.Next(), "next", ...)` — async selector, sync twin selector, then the wire identifier (matching the member-first pattern of `.Event`/`.Prop`; single-selector overloads exist for async-only members). The contract pins the identifier, the serialized arguments, their type tags, and the decoded return — for both dispatches.
  - **Current-state reads** (a `Get...Async` that decodes a component property's live value) → `.Getter(c => c.GetTotalAsync(), "Total", returns: 5.0)` with the **bare property name** — how a read travels (its wire identifier or call shape) is implementation-specific and owned by the harness, never written in contracts.
- **Args and type tags**: state what crosses — sample values chosen by you, wire forms derived from the member's serialization (cheat-sheet for the legacy conversions) or, in pin mode, by running the spec and reading the recorded wire values from the failure output. Write both as collection expressions: `args: [2.0, "next"], types: ["Number", "Json"]`. Structured values use `new JsonSubset(...)` (subset match) or `new RawJson(...)` (exact).
- **Returns** — choose by the *semantic* return kind:
  | Return kind | Contract form |
  |---|---|
  | none | omit — void overload |
  | scalar (bool/number/string/date) | just the value: `returns: true` / `5.0` / a UTC `DateTime` — the wire return kind is derived from the value's type, and the decoded .NET return must round-trip back to it (dates compared as instants) |
  | single serialized object | arranged `Getter` overload with `InteropReturn.Object(...)` + value-level `assert:` — see `ChatTests.cs` `DraftMessage` |
  | array of component/data-item references | arranged `Getter` overload with `InteropReturn.Array` of refs, `Assert.Same` against arranged instances — see `TileManagerTests.cs` `Tiles`, `ComboTests.cs` `Value` |
  | single bound-object reference | arranged `Getter` overload with `InteropReturn.Ref(...)`, `Assert.Same` against the arranged child — see `SelectTests.cs`/`DropdownTests.cs` `SelectedItem` |

  (An explicit `InteropReturn` + separate `expect:` overload exists for wire returns that decode to a different value than sent — rare.)
- **Value-returning methods must state their return** — enforced at compile time: a `Task<TResult>`-returning lambda on the void `.Method` overload is `[Obsolete(error: true)]` as a guard, so the contract states the `returns:` stub. The void overload needs no stub.
- **Hosted arrange — child components that only exist inside a parent** - Currently available in the hosted `Getter` overload (e.g. a tree item's `GetPathAsync` needs real ancestors). Build the full render with `ContractHost.Of<THost>(ps => ps.AddChildContent(...))` (parent as the root, structure nested inside), pick the component under test with `target: h => h.FindComponents<TComponent>()[n]`, and note that `returns:`/`assert:` receive the whole host render — so `interop.ContainerIdOf(h, "<selector>")` reaches ancestors and siblings outside the cut's subtree. The read still travels on the target's own container. See `TreeItemTests` in `TreeTests.cs`.
- **Sync twins are declared inside the member's spec**: every `X()`/`XAsync()` pair uses the twin overloads — both selectors up front, async first: `.Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)` (same for `.Getter`, including the arranged/hosted forms). The runner re-invokes the sync twin against the same expectations (identifier, args, types, decoded return). Declaring the twin is the contract author's responsibility — when enumerating methods, treat an `X()`/`XAsync()` pair as one member and use the twin overload. Sync dispatch rides the in-process JS runtime (`IJSInProcessRuntime` — WASM/WebView-only in production; the Server-hosted integration TestBed can't run sync variants, so contracts are their only coverage).
- **Skip and list**: a member whose decode hits an implementation gap (unregistered child type, missing marshal-by-value entry) is covered, not skipped — per the authoring modes above.

### 2. Events

- The contract entry is member-first — `.Event(c => c.SlideChanged, argsJson, assert)`, or the bare `.Event(c => c.Closing)` for a void event. The wire event name is derived from the member name (it matches the registered handler name on every generated component — if you ever hit one that differs, pass `name:` explicitly), the args type is inferred from the `EventCallback<TArgs>` signature.
- **What an `.Event` spec verifies** — the full loop, automatically: (1) binding round-trips — the member returns the exact callback assigned; (2) binding transmits an **event-handler registration** over interop, carrying the member's event identity — the client's cue to subscribe (same ref machinery as Script props, harness-normalized); (3) dispatching the spec's payload under the wire event name reaches the handler with args of the declared type, satisfying the spec's asserts; (4) unbinding (setting the parameter back to an empty callback) resets the member, transmits the **cleared registration** — the client's cue to unsubscribe — and stops delivery: a subsequent dispatch must not reach the handler.
- **The bare form compiles only for `EventCallback<IgbVoidEventArgs>` members** (the type is invariant), and `argsJson` is required on the data overload.
- **Args JSON**: derive from the args type's deserialization code (which keys it reads, into which .NET properties), and/or the web component's emitted event detail (the events are not black boxes: check `node_modules/igniteui-webcomponents` `.d.ts` / `custom-elements.json` for the emitted shape). Wire envelope framing for object details is stack-specific — see the cheat-sheet; `ChatTests.cs` `MessageCreated` is the exemplar.
- **Assert** each property you put in the payload: `assert: args => Assert.Equal(3, args.Detail)`. Keep assertions **value-level** (compare data, not instance identity) — with one exception: **self-reference details**
- **Assert references**
  - When a component's own event carries a reference to itself, the contract asserts it resolves back to the instance via the component-aware overload: `assert: (panel, a) => Assert.Same(panel, a.Detail)` (see `ExpansionPanelTests.cs`, `TileTests` in `TileManagerTests.cs`). Resolution of a reference IS its value semantics.
  - For child-component references (a parent's event whose detail contains a child — Accordion's panels, Tabs' tab, Tree's items, TileManager's tiles): use the arranged-children `Event` overload — `arrange:` adds child components via `AddChildContent`, `argsJson:` is a factory building a reference payload with `interop.ContainerIdOf(cut, "igc-tab:nth-of-type(2)")`, and `assert:` receives the cut to `Assert.Same` against the arranged child instance. See `TabsTests.cs` `Change` for example.
- Specs need no component state by default — the interop boundary is state-free (a method sends its invocation regardless of slides/messages existing; effects are integration's job). Use `arrange:` only when a member genuinely needs .NET-side arrangement (bound children, data) just to *reach* the boundary — e.g. payloads carrying references that must resolve, or data-source items referenced by uuid.
- The `<PropName>Script` string parameters are not events but ARE valid public API — covered **automatically** by the generic sweep in `ScriptPropTests`: every generated `<Member>Script` prop must transmit a script reference for its target member, carrying the script name. No per-contract entries needed (the integration TestBed does not exercise these either, so the sweep is their only coverage).

### 3. Interop-borne props

The contract entry is the member selector plus a sample value: the wire name is derived from the member (camelCase, honoring `[WCWidgetMemberName]`; `wireName:` overrides), and for scalar values the wire value is the value itself — `.Prop(c => c.Open, true)`. Enums and serialized objects/arrays must state the wire form explicitly: `.Prop(c => c.GroupSorting, GroupingDirection.Desc, wire: "desc")`, `.Prop(c => c.Options, new IgbChatOptions { ... }, wire: new JsonSubset("""..."""))`.

- A property belongs in the contract **only when its value travels over interop** rather than as a rendered attribute. Determine which from the component's serialization path (stack-specific — see the cheat-sheet) or empirically: for an attribute prop, the runner fails with *"no property update transmission was observed"*. Attribute props are covered by the suite's attribute facts instead.
- **Serialized config objects and arrays**: set an instance with 2–3 distinctive values and expect a `JsonSubset` — subset match, extra bookkeeping fields on the transmitted value are ignored; arrays compare element-wise (equal length, per-element subset). See `ChatTests.cs`, `CalendarTests.cs`.
- **Data sources** (`Data`/`DataSource` props): covered by `.Prop(c => c.Data, ...)` too — the harness follows whatever indirection the stack uses to the actual transfer. Item property names cross with their .NET names (not camelized). See `ComboTests.cs`.
- **Props referencing data items** (e.g. Combo's `Value`): use the arranged overload — arrange the `Data` the value points into and state the wire value as a factory, since tracked items cross as refs whose ids are only assigned on transfer: `.Prop(c => c.Value, [_item1], arrange: ps => ps.Add(c => c.Data, items), wire: (interop, cut) => new RawJson(...))`.

## Current-stack cheat-sheet (legacy `RendererMessage` pipeline)

> Everything in this section is specific to components still on the legacy
> `BaseRendererControl`/`RendererMessage` stack.

**Surface markers** in the component's source:
- `InvokeMethod("...", args, types)` inside `public async Task...` wrappers → methods/getters;
- `SetHandler<TArgs>(this.Name, "EventName", ...)` (the `[Parameter] EventCallback<TArgs>` is right above it) → events;
- `SerializeCore` at the bottom of the class (each `if (IsPropDirty("X")) { ser.AddXxxProp(...) }` line) → interop-borne props.

**Methods**: the JS name is the first `InvokeMethod` argument, used verbatim — *except* a `p:` prefix (e.g. `InvokeMethod("p:Total", ...)`), which marks a current-state read: use `.Getter` with the bare property name; the prefix is harness-owned. `types:` = the `new string[] { ... }` values verbatim (getters have none).

**Argument wire forms** (per argument expression in the source):

| Argument expression in source              | Sample .NET arg          | Expected wire arg (contract `args:`) |
|--------------------------------------------|--------------------------|--------------------------------------|
| bare value, type tag `"Number"`            | `2` / `2.5`              | `2.0` / `2.5` (always as double)     |
| `StringToString(x)`, tag `"String"`        | `"message-42"`           | `"message-42"`                       |
| bare bool, tag `"Boolean"`                 | `true`                   | `true`                               |
| `ObjectToParam(x, typeof(SomeEnum))`, tag `"Json"` | `SomeEnum.Member` | the `[WCEnumName("...")]` value if the member has one, else camelCase of the member name (`Next` → `"next"`) |
| `ObjectToParam(x)` with a plain string, tag `"Json"` | `"item-1"`     | `"item-1"`                           |
| `ObjectToParam(x)` with a marshal-by-value object, tag `"Json"` | an options object | `new JsonSubset("""{"key": value, ...}""")` |
| `x` DateTime, tag `"Date"`                 | a `DateTime`             | the same `DateTime` (compared as instant) |
| array helpers (`IntArrayToString`, ...), tag `"NumberArray"` etc. | array | `new RawJson("[1,2,3]")`            |

**Return decodes** (the line after `InvokeMethod` → the semantic kinds in the recipe's table):

| Source decode | Semantic kind |
|---|---|
| `ReturnToBoolean/Double/Int/Long/String/Date(iv)` | scalar (`returns:` the value; note `ReturnToBoolean(null) == false` — why unstubbed void specs would pass silently) |
| `ReturnToObject<T>(iv, "TypeName")` | single serialized object — stub `InteropReturn.Object("", ...)`: the wire type is empty, .NET fills it from its typeGuess |
| `ReturnToObjectArray<T>` | array of refs — component refs are `{"refType": "name", "id": <containerId>}`; data-item refs are `{"refType": "uuid", "id": <observed ___id>}` |
| bare `ConvertReturnValue(iv)` cast to a component | single bound-object reference — crosses as a bare `{"refType": "name", "id": ...}` with no retType envelope (`InteropReturn.Ref`) |
| `StringToEnum` returns | not modeled — skip with this reason |

**Events**: Payload keys come from the args type's `FromEventJson` (`if (args.ContainsKey("key")) { this.X = ReturnToXxx(args["key"]); }` → key + sample value: `ReturnToBoolean` → `true`, `ReturnToDouble` → `3`, `ReturnToDate` → `"2026-01-02T03:04:05.000Z"`).
Object details are envelope-wrapped by the client bridge (`src/src/index.ts` `toReturn` → `Loader.transformReturn`) as `{"detail": {"retType": "object", "type": "", "value": { ...detail... }}}` — type empty, .NET fills it from the typeGuess. Child references resolve through named cascading-parameter registration.

**Props**: Wire details: enum values are `[WCEnumName]` or camelCase; nested keys camelCase; dates inside serialized objects cross as `"@d:<ISO>"` strings (`AddPrimitiveProp`) while dedicated date arrays (`AddDateArrayProp`) cross as plain ISO strings; data sources ride a `refChanged` transfer under a generated ref id the description advertises as `<wireName>Ref` (the harness follows it). Skip `SerializeCore` entries whose wire name ends in `Ref` (event/script ref advertisements — the script side is swept by `ScriptPropTests`; script refs transmit as `refChanged` with refValue `script:::<name>`, which the harness normalizes to the bare name).

## Contract template

```csharp
public class <Name>Tests : ComponentWithContractTestBase<Igb<Name>>
{
    protected override ComponentContract<Igb<Name>> InteropContract { get; } = new ComponentContract<Igb<Name>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: ...)
        .Event(c => c.SomeEvent, ...)
        .Prop(c => c.SomeProp, ...);

    // One runner fact per non-empty contract section (omit facts for empty sections
    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    // ... other/existing facts ...
}
```

Contract failures carry the violated member and, as the top stack frame, the contract line that declared it (captured via caller info).

## Verify

```
dotnet test tests/IgniteUI.Blazor.Tests/IgniteUI.Blazor.Tests.csproj -f net10.0 --nologo --filter "FullyQualifiedName~<Name>Tests"
```

Common failures:

- `handler was not invoked` → wrong event name (must match the name the component registers) or a payload key the args deserialization doesn't read.
- Types/args mismatch → the assertion message shows the recorded wire values; judge which side is off per the authoring modes (in spec mode an unintended wire value is the implementation's bug — the red spec is doing its job).
- A method call that never returns (test timeout) means its identifier reached JS without a stub match — check the name matches the stub you declared (getters: bare property name).
- `no property update transmission was observed` → wrong wire name, an attribute-rendered prop, or a serialization exception upstream (nothing transmits at all).

Finish with a full-suite sanity run: same command without `--filter`.

## Definition of done (per component)

- Contract on exactly one suite, all facts green, plus full suite green on all TFMs.
- Every public method that goes out through interop — async **and** its sync twin (declared together via the twin overloads) — every registered JS-originated event, and every interop-borne prop is either covered, globally excluded, or named in the skip comment with a mechanism reason — nothing silently omitted. (`<Member>Script` params are covered by the `ScriptPropTests` sweep.)
- Implementation issues found along the way are handled per the authoring modes (`// TODO:` markers or fixes), never silently skipped.
