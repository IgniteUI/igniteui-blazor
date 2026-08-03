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

/// <summary>
/// Non-generic view of a <see cref="FromRender{T}"/>, so the runner can spot and resolve a
/// late value sitting inside an untyped collection (a method's expected arguments).
/// </summary>
internal interface IFromRender
{
    object? Resolve(InteropHarness harness, IRenderedComponent<IComponent> scope);
}

/// <summary>
/// A contract value that can only be settled once the component has rendered — a child's
/// interop instance id, a captured element handle, a data item's assigned uuid. This is the
/// only such mechanism in the DSL: any parameter typed <see cref="FromRender{T}"/> accepts
/// either a fixed value (implicitly, written exactly as it would be otherwise) or a late one
/// built with <see cref="FromRender.Of{T}"/>. The render scope is the cut for an arranged
/// spec and the whole host render for a hosted one, matching the spec's own scope.
/// </summary>
public readonly struct FromRender<T> : IFromRender
{
    private readonly T _value;
    private readonly Func<InteropHarness, IRenderedComponent<IComponent>, T>? _resolve;

    internal FromRender(T value)
    {
        _value = value;
        _resolve = null;
    }

    internal FromRender(Func<InteropHarness, IRenderedComponent<IComponent>, T> resolve)
    {
        _value = default!;
        _resolve = resolve;
    }

    public static implicit operator FromRender<T>(T value) => new(value);

    /// <summary>The settled value: computed against the render when declared late, otherwise as given.</summary>
    internal T Get(InteropHarness harness, IRenderedComponent<IComponent> scope) =>
        _resolve is null ? _value : _resolve(harness, scope);

    object? IFromRender.Resolve(InteropHarness harness, IRenderedComponent<IComponent> scope) => Get(harness, scope);
}

/// <summary>Builds late <see cref="FromRender{T}"/> values — the counterpart to <see cref="ContractHost.Of{THost}"/>.</summary>
public static class FromRender
{
    public static FromRender<T> Of<T>(Func<InteropHarness, IRenderedComponent<IComponent>, T> resolve) => new(resolve);
}

public sealed class MethodContractSpec<TComponent> where TComponent : IComponent
{
    /// <summary>Wire identifier; null when <see cref="ReadsProperty"/> is set (resolved via the harness).</summary>
    public string? JsName { get; init; }

    /// <summary>For current-state property reads: the property name, translated to a wire id by the harness.</summary>
    public string? ReadsProperty { get; init; }

    public required Func<TComponent, Task<object?>> Invoke { get; init; }
    public object?[] ExpectedArgs { get; init; } = [];
    public string[] ExpectedTypes { get; init; } = [];
    /// <summary>The value the JS side hands back, settled against the render when the spec declared it late.</summary>
    public FromRender<InteropReturn>? Stub { get; init; }
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

    /// <summary>Dynamic return assert receiving the render scope, to compare against arranged instances.</summary>
    public Action<IRenderedComponent<IComponent>, object?>? AssertReturnWithCut { get; init; }

    /// <summary>
    /// The element handles the invocation must carry (see <see cref="InteropMethodCall.Elements"/>).
    /// Read after the render, since the handles only exist then; when not declared, the
    /// invocation must carry none.
    /// </summary>
    public Func<IReadOnlyList<ElementReference>>? ExpectedElements { get; init; }

    public SpecSource? Source { get; init; }
}

public sealed class StatePropContractSpec<TComponent> where TComponent : IComponent
{
    public required string WireName { get; init; }
    public required Action<ComponentParameterCollectionBuilder<TComponent>> Set { get; init; }
    /// <summary>The transmitted value, settled against the render when the spec declared it late.</summary>
    public FromRender<object?> ExpectedValue { get; init; }

    /// <summary>Extra render setup the transmission depends on (e.g. data items the value must reference).</summary>
    public Action<ComponentParameterCollectionBuilder<TComponent>>? Arrange { get; init; }

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
    /// <summary>The dispatched payload, settled against the render when the spec declared it late.</summary>
    public FromRender<string> ArgsJson { get; init; } = "{}";
    public Action<object>? AssertArgs { get; init; }

    /// <summary>Like <see cref="AssertArgs"/> but also receives the rendered component (for reference-resolution asserts).</summary>
    public Action<TComponent, object>? AssertWithComponent { get; init; }

    /// <summary>Extra render setup the event needs to be reachable (child components, data, ...).</summary>
    public Action<ComponentParameterCollectionBuilder<TComponent>>? Arrange { get; init; }

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

    /// <summary>
    /// A void API method (async-only members): asserts identifier, arguments and type tags.
    /// <paramref name="arrange"/>/<paramref name="elements"/> as on the twin overload below.
    /// </summary>
    public ComponentContract<TComponent> Method(
        Func<TComponent, Task> invoke,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => { await invoke(c); return null; },
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Arrange = arrange,
            ExpectedElements = elements,
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
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null)
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
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Method(invoke, jsName, StubFor(returns), returns, args, types, arrange, elements, atFile, atLine);

    /// <summary>
    /// Value-returning method overload (async-only members) for wire returns the value
    /// form can't express (object/array envelopes, or stubs that decode to a different
    /// value than sent, or a stub only known once rendered — see <see cref="FromRender{T}"/>).
    /// </summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        string jsName,
        FromRender<InteropReturn> returns,
        TResult expect,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            JsName = jsName,
            Invoke = async c => await invoke(c),
            ExpectedArgs = args ?? [],
            ExpectedTypes = types ?? [],
            Arrange = arrange,
            ExpectedElements = elements,
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
    /// <paramref name="arrange"/> adds render setup the member needs (children the call
    /// references); an argument whose value or wire form only exists once rendered is
    /// declared with <see cref="FromRender"/>, and <paramref name="elements"/> states the
    /// element handles the invocation must carry alongside its arguments (none when omitted).
    /// </summary>
    public ComponentContract<TComponent> Method(
        Func<TComponent, Task> invoke,
        Action<TComponent> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
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
            Arrange = arrange,
            ExpectedElements = elements,
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
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
        => Method(invoke, sync, jsName, StubFor(returns), returns, args, types, arrange, elements, atFile, atLine);

    /// <summary>
    /// Value-returning method with its sync twin, for wire returns the value form can't express.
    /// <paramref name="arrange"/>/<paramref name="elements"/> as on the void twin overload.
    /// </summary>
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        FromRender<InteropReturn> returns,
        TResult expect,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null,
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
            Arrange = arrange,
            ExpectedElements = elements,
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
    // instead of silently decoding defaults. They mirror the void twin's optional parameters
    // so that passing arrange:/elements: cannot make them inapplicable — an arranged spec
    // whose member starts returning a value must fail here too.
    [Obsolete("This method pair returns a value — state the wire return via the returns: twin overload.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null)
        => throw new NotSupportedException();

    [Obsolete("The async method returns a value but its sync twin is void — align the pair and state the wire return.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task<TResult>> invoke,
        Action<TComponent> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null)
        => throw new NotSupportedException();

    [Obsolete("The sync twin returns a value but the async method is void — align the pair and state the wire return.", error: true)]
    public ComponentContract<TComponent> Method<TResult>(
        Func<TComponent, Task> invoke,
        Func<TComponent, TResult> sync,
        string jsName,
        object?[]? args = null,
        string[]? types = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        Func<IReadOnlyList<ElementReference>>? elements = null)
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
        FromRender<InteropReturn> returns,
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
        FromRender<InteropReturn> returns,
        Action<IRenderedComponent<TComponent>, TResult> assert,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _methods.Add(new MethodContractSpec<TComponent>
        {
            ReadsProperty = propertyName,
            Invoke = async c => await invoke(c),
            Arrange = arrange,
            Stub = returns,
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
        FromRender<InteropReturn> returns,
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
            Stub = returns,
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
        FromRender<InteropReturn> returns,
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
        FromRender<InteropReturn> returns,
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
            Stub = returns,
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
        FromRender<InteropReturn> returns,
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
            Stub = returns,
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
    /// <paramref name="arrange"/> adds render setup the transmission depends on (e.g. the
    /// data items the value references); pass a late <paramref name="wire"/>
    /// (<see cref="FromRender.Of{T}"/>) when it can only be known once that has rendered —
    /// tracked items, for instance, cross as refs whose ids are assigned on transfer.
    /// </summary>
    public ComponentContract<TComponent> Prop<TValue>(
        Expression<Func<TComponent, TValue>> member,
        TValue value,
        FromRender<object?> wire,
        string? wireName = null,
        Action<ComponentParameterCollectionBuilder<TComponent>>? arrange = null,
        [CallerFilePath] string atFile = "",
        [CallerLineNumber] int atLine = 0)
    {
        _props.Add(new StatePropContractSpec<TComponent>
        {
            WireName = wireName ?? WirePropertyName(member),
            Set = ps => ps.Add(member, value),
            ExpectedValue = wire,
            Arrange = arrange,
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
    /// adds the children (or data) the event needs, a late <paramref name="argsJson"/>
    /// (<see cref="FromRender.Of{T}"/>) builds the payload after render (when child ids
    /// exist), and <paramref name="assert"/> receives the rendered cut so it can compare
    /// against the arranged child instances.
    /// </summary>
    public ComponentContract<TComponent> Event<TArgs>(
        Expression<Func<TComponent, EventCallback<TArgs>>> member,
        Action<ComponentParameterCollectionBuilder<TComponent>> arrange,
        FromRender<string> argsJson,
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
            ArgsJson = argsJson,
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
