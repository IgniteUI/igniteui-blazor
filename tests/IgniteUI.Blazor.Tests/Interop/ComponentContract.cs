using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests.Interop;

/// <summary>Where a contract spec was declared (captured by the builders) — lets a violation point back at the suite's contract line.</summary>
public sealed record SpecSource(string File, int Line);

/// <summary>
/// Builds the render for a hosted spec: the parent (plus structure) as the root, with the
/// component under test nested inside and picked out by the spec's target selector. For
/// child components that only meaningfully exist inside a parent (e.g. a tree item's
/// GetPath needs real ancestors).
/// </summary>
public static class ContractHost
{
    /// <summary>
    /// Produces a thunk that renders the host through the test context. bUnit v2 dropped the
    /// public way to materialize an arranged builder into a detached <see cref="RenderFragment"/>
    /// (<c>ComponentParameterCollection</c> is now internal), so the host defers to
    /// <see cref="BunitContext.Render{TComponent}(Action{ComponentParameterCollectionBuilder{TComponent}})"/>
    /// at run time instead. The rendered host is exposed as <see cref="IRenderedComponent{IComponent}"/>
    /// via the interface's covariance.
    /// </summary>
    public static Func<BunitContext, IRenderedComponent<IComponent>> Of<THost>(Action<ComponentParameterCollectionBuilder<THost>> arrange)
        where THost : class, IComponent
        => ctx => ctx.Render<THost>(arrange);
}

/// <summary>Expected wire value that is itself JSON (object/array arguments); compared structurally and exactly.</summary>
public sealed record RawJson(string Json);

/// <summary>
/// Expected wire value for JSON objects compared as a subset: every property listed here
/// must match the actual value; extra properties on the actual value are ignored (useful
/// for serialized config objects that carry bookkeeping fields).
/// </summary>
public sealed record JsonSubset(string Json);

public sealed class MethodContractSpec<TComponent> where TComponent : IComponent
{
    /// <summary>Wire identifier; null when <see cref="ReadsProperty"/> is set (resolved via the harness).</summary>
    public string? JsName { get; init; }

    /// <summary>For current-state property reads: the property name, translated to a wire id by the harness.</summary>
    public string? ReadsProperty { get; init; }

    public required Func<TComponent, Task<object?>> Invoke { get; init; }
    public object?[] ExpectedArgs { get; init; } = [];
    public string[] ExpectedTypes { get; init; } = [];
    public InteropReturn? Stub { get; init; }
    public object? ExpectedReturn { get; init; }
    public bool HasExpectedReturn { get; init; }

    /// <summary>Extra render setup the member needs (child components, data). A spec with an arrangement renders its own instance.</summary>
    public Action<ComponentParameterCollectionBuilder<TComponent>>? Arrange { get; init; }

    /// <summary>
    /// Hosted specs: the full render, parent included (see <see cref="ContractHost.Of{THost}"/>);
    /// the component under test is picked out of it by <see cref="Target"/>.
    /// </summary>
    public Func<BunitContext, IRenderedComponent<IComponent>>? Host { get; init; }

    /// <summary>Selects which rendered <typeparamref name="TComponent"/> inside <see cref="Host"/> is the component under test.</summary>
    public Func<IRenderedComponent<IComponent>, IRenderedComponent<TComponent>>? Target { get; init; }

    /// <summary>
    /// The member's sync twin (<c>Show()</c> for <c>ShowAsync()</c>), when declared: the
    /// runner re-invokes it against the same expectations after the async path.
    /// </summary>
    public Func<TComponent, object?>? SyncInvoke { get; init; }

    /// <summary>
    /// Dynamic stub for returns referencing arranged children (ids only known after render);
    /// wins over <see cref="Stub"/>. The fragment is the render scope: the cut itself for
    /// arranged specs, the whole host for hosted ones (so ancestors are reachable).
    /// </summary>
    public Func<InteropHarness, IRenderedComponent<IComponent>, InteropReturn>? StubFactory { get; init; }

    /// <summary>Dynamic return assert receiving the render scope (see <see cref="StubFactory"/>) to compare against arranged instances.</summary>
    public Action<IRenderedComponent<IComponent>, object?>? AssertReturnWithCut { get; init; }

    public SpecSource? Source { get; init; }
}

public sealed class StatePropContractSpec<TComponent> where TComponent : IComponent
{
    public required string WireName { get; init; }
    public required Action<ComponentParameterCollectionBuilder<TComponent>> Set { get; init; }
    public object? ExpectedValue { get; init; }

    /// <summary>Extra render setup the transmission depends on (e.g. data items the value must reference).</summary>
    public Action<ComponentParameterCollectionBuilder<TComponent>>? Arrange { get; init; }

    /// <summary>Dynamic wire value for transmissions referencing arranged state (ids only known after render); wins over <see cref="ExpectedValue"/>.</summary>
    public Func<InteropHarness, IRenderedComponent<TComponent>, object?>? ExpectedValueFactory { get; init; }

    public SpecSource? Source { get; init; }
}

public sealed class EventContractSpec<TComponent> where TComponent : IComponent
{
    public required string EventName { get; init; }

    /// <summary>
    /// Sets the event parameter and returns the boxed <see cref="EventCallback{TValue}"/> it
    /// assigned, so the runner can assert the member round-trips that exact value: with a
    /// sink, a callback forwarding received args to it; with null, an empty callback (the
    /// removal round-trip — bUnit has no parameter removal, unbinding IS an add).
    /// </summary>
    public required Func<ComponentParameterCollectionBuilder<TComponent>, Action<object>?, object> Bind { get; init; }

    /// <summary>Reads the event member back (boxed) — the typed read half of <see cref="Bind"/>.</summary>
    public required Func<TComponent, object> Get { get; init; }

    /// <summary>The declared event args type; the runner asserts the received args are assignable to it.</summary>
    public required Type ArgsType { get; init; }
    public string ArgsJson { get; init; } = "{}";
    public Action<object>? AssertArgs { get; init; }

    /// <summary>Like <see cref="AssertArgs"/> but also receives the rendered component (for reference-resolution asserts).</summary>
    public Action<TComponent, object>? AssertWithComponent { get; init; }

    /// <summary>Extra render setup the event needs to be reachable (child components, data, ...).</summary>
    public Action<ComponentParameterCollectionBuilder<TComponent>>? Arrange { get; init; }

    /// <summary>Dynamic payload builder for references only known after render (child ids); wins over <see cref="ArgsJson"/>.</summary>
    public Func<InteropHarness, IRenderedComponent<TComponent>, string>? ArgsJsonFactory { get; init; }

    /// <summary>Like <see cref="AssertWithComponent"/> but receives the rendered cut (to reach arranged children).</summary>
    public Action<IRenderedComponent<TComponent>, object>? AssertWithCut { get; init; }

    public SpecSource? Source { get; init; }
}

/// <summary>
/// Pins a component's interop contract: how its public API maps onto the wire —
/// method identifiers, argument serialization and type tags, return decoding, and
/// event names + args deserialization. Runs through the <see cref="InteropHarness"/>
/// seam, so the same contract verifies the component before and after it migrates to
/// a new interop stack (see <see cref="InteropHarnessRegistry"/>). Authoring guide:
/// skills/igniteui-blazor-lite-testing/references/interop-contracts.md.
/// </summary>
public sealed class ComponentContract<TComponent> where TComponent : IComponent
{
    private readonly List<MethodContractSpec<TComponent>> _methods = [];
    private readonly List<EventContractSpec<TComponent>> _events = [];
    private readonly List<StatePropContractSpec<TComponent>> _props = [];

    public IReadOnlyList<MethodContractSpec<TComponent>> Methods => _methods;
    public IReadOnlyList<EventContractSpec<TComponent>> Events => _events;
    public IReadOnlyList<StatePropContractSpec<TComponent>> Props => _props;

    /// <summary>A void API method (async-only members): asserts identifier, arguments and type tags.</summary>
    public ComponentContract<TComponent> Method(
        Func<TComponent, Task> invoke,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => { await invoke(c); return null; },
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary> Compile-time guard: a value-returning API method must state its wire return. </summary>
    [Obsolete("This method returns a value — its return decoding is part of the contract. Use the overload with returns: (or InteropReturn + expect:).", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string jsName,
        object?[]? args = null,
        string[]? types = null)
        => throw new NotSupportedException();

    /// <summary>
    /// A value-returning API method (async-only members): the wire return kind is derived
    /// from <typeparamref name="TResult"/>, the JS side is stubbed with <paramref name="returns"/>,
    /// and the decoded .NET return must round-trip back to the same value.
    /// </summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string jsName,
        TResult returns,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Method(invoke, jsName, StubFor(returns), returns, args, types, atFile, atLine);

    /// <summary>
    /// Value-returning method overload (async-only members) for wire returns the value
    /// form can't express (object/array envelopes, or stubs that decode to a different
    /// value than sent).
    /// </summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string jsName,
        InteropReturn returns,
        TResult expect,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => await invoke(c),
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Stub = returns,
            ExpectedReturn = expect,
            HasExpectedReturn = true,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// A void API method declared with its sync twin (<c>Toggle()</c> for <c>ToggleAsync()</c>):
    /// the runner re-invokes the twin against the same expectations after the async path.
    /// </summary>
    public ComponentContract<TComponent> Method(
        Func<TComponent, Task> invoke,
        Action<TComponent> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => { await invoke(c); return null; },
            SyncInvoke = c => { sync(c); return null; },
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>Value-returning method with its sync twin; wire return derived from <paramref name="returns"/>.</summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        TResult returns,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Method(invoke, sync, jsName, StubFor(returns), returns, args, types, atFile, atLine);

    /// <summary>Value-returning method with its sync twin, for wire returns the value form can't express.</summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        InteropReturn returns,
        TResult expect,
        object?[]? args = null,
        string[]? types = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => await invoke(c),
            SyncInvoke = c => sync(c),
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Stub = returns,
            ExpectedReturn = expect,
            HasExpectedReturn = true,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    // Compile-time drift guards for twin declarations (same rationale as the single-selector
    // poison above): each candidate is a better overload-resolution match than the legitimate
    // void twin form when one or both sides start returning a value, so drift fails the build
    // instead of silently decoding defaults.
    [Obsolete("This method pair returns a value — state the wire return via the returns: twin overload.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null)
        => throw new NotSupportedException();

    [Obsolete("The async method returns a value but its sync twin is void — align the pair and state the wire return.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Action<TComponent> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null)
        => throw new NotSupportedException();

    [Obsolete("The sync twin returns a value but the async method is void — align the pair and state the wire return.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null)
        => throw new NotSupportedException();

    /// <summary>
    /// A current-state property read (async-only members; e.g. <c>GetTotalAsync</c> reading
    /// "Total"): the wire identifier is implementation-specific and resolved by the
    /// harness, the wire return kind is derived from <typeparamref name="TResult"/>, and
    /// the decoded .NET return must round-trip back to <paramref name="returns"/>.
    /// </summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string propertyName,
        TResult returns,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Getter(invoke, propertyName, StubFor(returns), returns, atFile, atLine);

    /// <summary>Getter overload (async-only members) for wire returns the value form can't express.</summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string propertyName,
        InteropReturn returns,
        TResult expect,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            Stub = returns,
            ExpectedReturn = expect,
            HasExpectedReturn = true,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// A current-state read (async-only members) whose stubbed return references arranged
    /// children (e.g. an array of child component refs): <paramref name="arrange"/> adds
    /// the children, <paramref name="returns"/> builds the stub after render (when child
    /// ids exist), and <paramref name="assert"/> receives the rendered cut plus the
    /// decoded return.
    /// </summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string propertyName,
        Action<ComponentParameterCollectionBuilder<TComponent>> arrange,
        Func<InteropHarness, IRenderedComponent<TComponent>, InteropReturn> returns,
        Action<IRenderedComponent<TComponent>, TResult> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            Arrange = arrange,
            // For arranged specs the render scope IS the typed cut.
            StubFactory = (h, scope) => returns(h, (IRenderedComponent<TComponent>)scope),
            AssertReturnWithCut = (scope, o) => assert((IRenderedComponent<TComponent>)scope, (TResult)o!),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// A current-state read (async-only members) on a component hosted inside a parent —
    /// for child components whose member only makes sense with real ancestors (e.g. a tree
    /// item's path). <paramref name="host"/> is the full render (see
    /// <see cref="ContractHost.Of{THost}"/>), <paramref name="target"/> picks the component
    /// under test out of it, and <paramref name="returns"/>/<paramref name="assert"/>
    /// receive the whole host render so refs to ancestors and siblings can be built and compared.
    /// </summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string propertyName,
        Func<BunitContext, IRenderedComponent<IComponent>> host,
        Func<IRenderedComponent<IComponent>, IRenderedComponent<TComponent>> target,
        Func<InteropHarness, IRenderedComponent<IComponent>, InteropReturn> returns,
        Action<IRenderedComponent<IComponent>, TResult> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            Host = host,
            Target = target,
            StubFactory = returns,
            AssertReturnWithCut = (scope, o) => assert(scope, (TResult)o!),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>Current-state read with its sync twin (<c>GetTotal()</c> for <c>GetTotalAsync()</c>); wire return derived from <paramref name="returns"/>.</summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string propertyName,
        TResult returns,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Getter(invoke, sync, propertyName, StubFor(returns), returns, atFile, atLine);

    /// <summary>Current-state read with its sync twin, for wire returns the value form can't express.</summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string propertyName,
        InteropReturn returns,
        TResult expect,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            SyncInvoke = c => sync(c),
            Stub = returns,
            ExpectedReturn = expect,
            HasExpectedReturn = true,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>Arranged current-state read with its sync twin (see the arranged overload).</summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string propertyName,
        Action<ComponentParameterCollectionBuilder<TComponent>> arrange,
        Func<InteropHarness, IRenderedComponent<TComponent>, InteropReturn> returns,
        Action<IRenderedComponent<TComponent>, TResult> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            SyncInvoke = c => sync(c),
            Arrange = arrange,
            StubFactory = (h, scope) => returns(h, (IRenderedComponent<TComponent>)scope),
            AssertReturnWithCut = (scope, o) => assert((IRenderedComponent<TComponent>)scope, (TResult)o!),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>Hosted current-state read with its sync twin (see the hosted overload).</summary>
    public ComponentContract<TComponent> Getter<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string propertyName,
        Func<BunitContext, IRenderedComponent<IComponent>> host,
        Func<IRenderedComponent<IComponent>, IRenderedComponent<TComponent>> target,
        Func<InteropHarness, IRenderedComponent<IComponent>, InteropReturn> returns,
        Action<IRenderedComponent<IComponent>, TResult> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            SyncInvoke = c => sync(c),
            Host = host,
            Target = target,
            StubFactory = returns,
            AssertReturnWithCut = (scope, o) => assert(scope, (TResult)o!),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// A property whose value travels to the client over interop (rather than as a
    /// rendered attribute): setting the parameter must produce a property update
    /// transmission. The wire name is derived from the member (camelCase, honoring
    /// WCWidgetMemberName), and for scalar values the wire value is the value itself.
    /// </summary>
    public ComponentContract<TComponent> Prop<TValue>(
        Expression<Func<TComponent, TValue>> member,
        TValue value,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Prop(member, value, wire: ScalarWire(value), atFile: atFile, atLine: atLine);

    /// <summary>
    /// Prop overload stating the wire value explicitly — required for enums (wire enum
    /// value) and serialized objects/arrays (<see cref="JsonSubset"/>/<see cref="RawJson"/>).
    /// </summary>
    public ComponentContract<TComponent> Prop<TValue>(
        Expression<Func<TComponent, TValue>> member,
        TValue value,
        object? wire,
        string? wireName = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _props.Add(new StatePropContractSpec<TComponent>
        {
            WireName = wireName ?? WirePropertyName(member),
            Set = ps => ps.Add(member, value),
            ExpectedValue = wire,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// Prop overload with render arrangement and a dynamic wire value — for values whose
    /// transmission references arranged state (e.g. data items crossing as uuid refs
    /// whose ids are only assigned once the data source transfers).
    /// </summary>
    public ComponentContract<TComponent> Prop<TValue>(
        Expression<Func<TComponent, TValue>> member,
        TValue value,
        Action<ComponentParameterCollectionBuilder<TComponent>> arrange,
        Func<InteropHarness, IRenderedComponent<TComponent>, object?> wire,
        string? wireName = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _props.Add(new StatePropContractSpec<TComponent>
        {
            WireName = wireName ?? WirePropertyName(member),
            Set = ps => ps.Add(member, value),
            Arrange = arrange,
            ExpectedValueFactory = wire,
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary> A void JS-originated event (dispatched with an empty payload) shorthand. </summary>
    public ComponentContract<TComponent> Event(
        Expression<Func<TComponent, EventCallback<IgbVoidEventArgs>>> member,
        string? name = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _events.Add(new EventContractSpec<TComponent>
        {
            EventName = name ?? MemberName(member),
            Bind = (ps, on) => BindMember(ps, member, on),
            Get = GetterOf(member),
            ArgsType = typeof(IgbVoidEventArgs),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// A JS-originated event, identified by its EventCallback parameter: the wire event
    /// name is the member name (all generated components register handlers under it;
    /// override via <paramref name="name"/> should they ever differ) and the args type is
    /// inferred from the callback signature and asserted on the received args.
    /// <paramref name="argsJson"/> is the plain JS args payload (e.g. <c>{"detail": true}</c>).
    /// The runner verifies the full loop: the member round-trips the exact callback bound
    /// and the event-handler registration transmits over interop; dispatch delivers the
    /// decoded args; and unbinding resets the member, transmits the cleared registration,
    /// and stops delivery.
    /// </summary>
    public ComponentContract<TComponent> Event<TArgs>(
        Expression<Func<TComponent, EventCallback<TArgs>>> member,
        string argsJson,
        Action<TArgs>? assert = null,
        string? name = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _events.Add(new EventContractSpec<TComponent>
        {
            EventName = name ?? MemberName(member),
            Bind = (ps, on) => BindMember(ps, member, on),
            Get = GetterOf(member),
            ArgsType = typeof(TArgs),
            ArgsJson = argsJson,
            AssertArgs = assert is null ? null : o => assert((TArgs)o),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// Event overload whose assert also receives the rendered component — for payloads
    /// carrying references (e.g. <c>{"refType": "name", "id": "mainControl"}</c> details)
    /// that must resolve back to the .NET instance.
    /// </summary>
    public ComponentContract<TComponent> Event<TArgs>(
        Expression<Func<TComponent, EventCallback<TArgs>>> member,
        string argsJson,
        Action<TComponent, TArgs> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _events.Add(new EventContractSpec<TComponent>
        {
            EventName = MemberName(member),
            Bind = (ps, on) => BindMember(ps, member, on),
            Get = GetterOf(member),
            ArgsType = typeof(TArgs),
            ArgsJson = argsJson,
            AssertWithComponent = (c, o) => assert(c, (TArgs)o),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    /// <summary>
    /// Event overload for payloads referencing arranged children: <paramref name="arrange"/>
    /// adds the children (or data) the event needs, <paramref name="argsJson"/> builds the
    /// payload after render (when child ids exist), and <paramref name="assert"/> receives
    /// the rendered cut so it can compare against the arranged child instances.
    /// </summary>
    public ComponentContract<TComponent> Event<TArgs>(
        Expression<Func<TComponent, EventCallback<TArgs>>> member,
        Action<ComponentParameterCollectionBuilder<TComponent>> arrange,
        Func<InteropHarness, IRenderedComponent<TComponent>, string> argsJson,
        Action<IRenderedComponent<TComponent>, TArgs> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _events.Add(new EventContractSpec<TComponent>
        {
            EventName = MemberName(member),
            Bind = (ps, on) => BindMember(ps, member, on),
            Get = GetterOf(member),
            ArgsType = typeof(TArgs),
            Arrange = arrange,
            ArgsJsonFactory = argsJson,
            AssertWithCut = (cut, o) => assert(cut, (TArgs)o),
            Source = new SpecSource(atFile, atLine),
        });
        return this;
    }

    private static System.Reflection.MemberInfo MemberOf(LambdaExpression member) =>
        member.Body is MemberExpression m
            ? m.Member
            : throw new ArgumentException("Selector must be a simple member access (c => c.Member).", nameof(member));

    private static string MemberName<TArgs>(Expression<Func<TComponent, EventCallback<TArgs>>> member) =>
        MemberOf(member).Name;

    /// <summary>
    /// The write half of an event spec's bind/read loop, captured here where the args type
    /// is known: assigns the parameter (an empty callback when <paramref name="sink"/> is
    /// null) and returns the exact boxed callback for the runner's round-trip assert.
    /// </summary>
    private static EventCallback<TArgs> BindMember<TArgs>(
        ComponentParameterCollectionBuilder<TComponent> ps,
        Expression<Func<TComponent, EventCallback<TArgs>>> member,
        Action<object>? sink)
    {
        var callback = sink is null ? default : new EventCallback<TArgs>(null, sink);
        ps.Add(member, callback);
        return callback;
    }

    /// <summary>The read half: the compiled member getter, boxed (see <see cref="EventContractSpec{TComponent}.Get"/>).</summary>
    private static Func<TComponent, object> GetterOf<TArgs>(Expression<Func<TComponent, EventCallback<TArgs>>> member)
    {
        var get = member.Compile();
        return c => get(c);
    }

    /// <summary>Derives the wire return kind from the .NET return type; exotic shapes use the InteropReturn overloads.</summary>
    private static InteropReturn StubFor<TResult>(TResult value) => value switch
    {
        bool b => InteropReturn.Bool(b),
        double d => InteropReturn.Number(d),
        int i => InteropReturn.Number(i),
        long l => InteropReturn.Number(l),
        string s => InteropReturn.String(s),
        DateTime dt => InteropReturn.Date(dt),
        _ => throw new ArgumentException(
            $"Cannot derive a wire return for {typeof(TResult).Name}; use the InteropReturn overload."),
    };

    /// <summary>Scalars cross the wire as themselves; enums and objects must state their wire form explicitly.</summary>
    private static object ScalarWire<TValue>(TValue value) => value switch
    {
        bool or double or int or long or string or DateTime => value,
        _ => throw new ArgumentException(
            $"A wire value cannot be derived for {typeof(TValue).Name}; use the overload with an explicit wire: argument."),
    };

    private static string WirePropertyName<TValue>(Expression<Func<TComponent, TValue>> member)
    {
        var info = MemberOf(member);
        var name = info.GetCustomAttributes(typeof(WCWidgetMemberNameAttribute), true)
            .Cast<WCWidgetMemberNameAttribute>()
            .FirstOrDefault()?.Name ?? info.Name;
        return char.ToLowerInvariant(name[0]) + name[1..];
    }
}
