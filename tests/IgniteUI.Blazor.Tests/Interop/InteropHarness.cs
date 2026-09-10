using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Tests.Interop;

/// <summary>
/// A component API method invocation observed on the interop layer,
/// normalized away from the concrete wire format. <c>Elements</c> are the DOM element
/// handles that ride with the call rather than inside <c>Arguments</c> — an element-typed
/// argument crosses as a placeholder referencing its position among them.
/// </summary>
public sealed record InteropMethodCall(
    string ContainerId,
    string MethodName,
    long InvokeId,
    IReadOnlyList<JsonElement> Arguments,
    IReadOnlyList<string> Types,
    IReadOnlyList<ElementReference> Elements,
    JsonElement RawMessage);

/// <summary>
/// A component state synchronization (full or delta) observed on the interop layer.
/// </summary>
public sealed record InteropStateSync(
    string ContainerId,
    JsonElement State,
    bool IsDelta,
    JsonElement RawMessage);

public enum InteropReturnKind
{
    Undefined,
    Boolean,
    Number,
    String,
    Date,
    Object,
    Array,
    Ref,
    Deferred
}

/// <summary>
/// Implementation-agnostic description of a value the JS side hands back for a
/// method invocation. Concrete harnesses translate it into their wire format.
/// </summary>
public sealed class InteropReturn
{
    private InteropReturn(InteropReturnKind kind, object? value = null, string? typeName = null)
    {
        Kind = kind;
        Value = value;
        TypeName = typeName;
    }

    public InteropReturnKind Kind { get; }
    public object? Value { get; }

    /// <summary>Wire type name for <see cref="InteropReturnKind.Object"/> returns (e.g. "WebDropdownItem").</summary>
    public string? TypeName { get; }

    public static readonly InteropReturn Undefined = new(InteropReturnKind.Undefined);

    /// <summary>
    /// A deferred return: the reply carries no value, and the result arrives in a later
    /// completion message — deliver it via <see cref="InteropHarness.CompleteDeferred"/>.
    /// (No promise crosses the wire; how a stack tags deferral is its own spelling.)
    /// </summary>
    public static readonly InteropReturn Deferred = new(InteropReturnKind.Deferred);

    public static InteropReturn Bool(bool value) => new(InteropReturnKind.Boolean, value);
    public static InteropReturn Number(double value) => new(InteropReturnKind.Number, value);
    public static InteropReturn String(string value) => new(InteropReturnKind.String, value);
    public static InteropReturn Date(DateTime value) => new(InteropReturnKind.Date, value);
    public static InteropReturn Object(string typeName, string valueJson) => new(InteropReturnKind.Object, valueJson, typeName);

    /// <summary>An array return; <paramref name="itemsJson"/> is the JSON array (items may be reference objects).</summary>
    public static InteropReturn Array(string itemsJson) => new(InteropReturnKind.Array, itemsJson);

    /// <summary>
    /// A single bound-object reference return (<c>{"refType": "name"|"uuid", "id": ...}</c>) —
    /// the client sends these bare, resolved back to the .NET instance by reference.
    /// </summary>
    public static InteropReturn Ref(string refJson) => new(InteropReturnKind.Ref, refJson);
}

/// <summary>
/// The seam between component tests and the concrete JS interop implementation.
/// Tests speak only in terms of this API — observed <see cref="MethodCalls"/> and
/// <see cref="StateSyncs"/>, stubbed returns, readiness, and JS-originated events.
/// Everything specific to the current message-based renderer protocol lives in
/// <see cref="RendererMessageInteropHarness"/>. When the interop infrastructure is
/// rewritten, implement a new harness and map migrated components to it in
/// <see cref="InteropHarnessRegistry"/> — the tests themselves stay unchanged.
/// </summary>
public abstract class InteropHarness
{
    private readonly Func<Dispatcher> _dispatcher;

    /// <param name="dispatcher">
    /// Resolves the renderer's dispatcher. Taken as a required argument rather than set afterwards
    /// so a harness cannot exist without one, and resolved on use rather than up front because the
    /// harness is built while the test's services are still being configured - asking for the
    /// renderer that early settles the container.
    /// </param>
    protected InteropHarness(Func<Dispatcher> dispatcher) =>
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

    /// <summary>
    /// Runs <paramref name="work"/> on the renderer's dispatcher, where Blazor delivers real
    /// JS-to-.NET calls and application code invokes component APIs. Anything that makes a component
    /// transmit belongs here: off the dispatcher the send races the renderer's own flush, and bUnit
    /// records both on one unsynchronized list, so a raced message can be lost outright.
    /// </summary>
    public void OnDispatcher(Action work) =>
        _dispatcher().InvokeAsync(work).GetAwaiter().GetResult();

    /// <summary>
    /// <inheritdoc cref="OnDispatcher(Action)" path="/summary"/>
    /// Hands back a still-running task instead of awaiting it on the dispatcher, which would hold
    /// the dispatcher until it completes and deadlock a deferred return needing it to get there.
    /// An interop call transmits before it yields, so the send still happens here.
    /// </summary>
    public T OnDispatcher<T>(Func<T> work) => _dispatcher().InvokeAsync(work).GetAwaiter().GetResult();

    /// <summary>The service instance components resolve via DI.</summary>
    public abstract IIgniteUIBlazor Service { get; }

    /// <summary>Registers whatever the implementation needs into the test DI container.</summary>
    public abstract void ConfigureServices(IServiceCollection services);

    /// <summary>
    /// Arranges the interop layer so components rendered afterwards become ready
    /// through their natural readiness flow.
    /// </summary>
    public abstract void PrimeReady();

    /// <summary>Forces readiness on all already-rendered components (JS-side "loaded" signal).</summary>
    public abstract void MakeReady();

    /// <summary>Resolves the interop instance id of a rendered component.</summary>
    public abstract string ContainerIdOf(IRenderedComponent<IComponent> cut);

    /// <summary>Resolves the interop instance id of a child component inside a rendered fragment.</summary>
    public abstract string ContainerIdOf(IRenderedComponent<IComponent> cut, string childSelector);

    /// <summary>All API method invocations sent to JS so far, in order.</summary>
    public abstract IReadOnlyList<InteropMethodCall> MethodCalls { get; }

    /// <summary>All component state synchronizations sent to JS so far, in order.</summary>
    public abstract IReadOnlyList<InteropStateSync> StateSyncs { get; }

    /// <summary>Stubs the JS-side return value for invocations of <paramref name="methodName"/>.</summary>
    public abstract void SetupMethodResult(string methodName, InteropReturn result);

    /// <summary>
    /// Stubs the JS-side value for current-state reads of <paramref name="propertyName"/>.
    /// How a read travels is implementation-specific (a <c>"p:Name"</c> method message
    /// today; possibly a dedicated interop call with different arguments later).
    /// </summary>
    public abstract void SetupPropertyRead(string propertyName, InteropReturn result);

    /// <summary>All current-state reads of <paramref name="propertyName"/> issued for the instance so far, in order (count before/after an invocation to require a new one).</summary>
    public abstract IEnumerable<InteropMethodCall> PropertyReads(string containerId, string propertyName);

    /// <summary>
    /// Dispatches a JS-originated component event into .NET.
    /// <paramref name="argsJson"/> is the plain JSON payload of the event args
    /// (e.g. <c>{"detail": true}</c>); the harness applies any wire framing.
    /// </summary>
    public abstract void RaiseEvent(string containerId, string eventName, string argsJson = "{}", string targetName = "mainControl");

    /// <summary>Completes a method invocation that was answered with <see cref="InteropReturn.Deferred"/>.</summary>
    public abstract void CompleteDeferred(InteropMethodCall call, InteropReturn result);

    /// <summary>
    /// Finds the client-bound property update transmission for <paramref name="wireName"/>
    /// on the instance, returning the transmitted value normalized to JSON. Which channel
    /// an update rides (bulk state description, ref transfer, a dedicated call) is
    /// implementation-specific; simple props that cross as rendered attributes are not
    /// interop and are covered by attribute tests instead. Returns null when no update
    /// was transmitted.
    /// </summary>
    public abstract JsonElement? FindPropertyUpdate(string containerId, string wireName);

    /// <summary>
    /// Forgets the traffic observed so far, so <see cref="MethodCalls"/>, <see cref="StateSyncs"/> and
    /// <see cref="FindPropertyUpdate"/> report only what crosses from here on instead of at any point.
    /// For tests with phases - bind, then unbind, etc - ensures the next phase's observations are correct.
    /// </summary>
    public abstract void ClearObserved();

    /// <summary>
    /// The positions of the item insertions transmitted for the instance's bound data, in the order
    /// the client received them. Transmission is asynchronous and nothing observable says it has
    /// finished, so <paramref name="expected"/> - what the caller is waiting for - is what the wait
    /// is pinned to. Everything transmitted comes back, so both a shortfall and an overshoot are
    /// returned to be asserted on rather than hidden. How an insertion is spelled on the wire is
    /// implementation-specific; that every one arrives exactly once, in order, is not.
    /// </summary>
    public abstract IReadOnlyList<int> DataItemInsertions(string containerId, int expected);

    /// <summary>
    /// A short account of what the instance has transmitted, to report alongside an expected
    /// transmission that never showed up. Absence on its own is ambiguous: a message that was never
    /// queued reads exactly like one that was queued and never flushed, and only the second is a
    /// timing problem. "Sent nothing at all" separates them.
    /// </summary>
    public abstract string DescribeTraffic(string containerId);

    public IEnumerable<InteropMethodCall> CallsOf(string methodName, string? containerId = null) =>
        MethodCalls.Where(c => c.MethodName == methodName && (containerId is null || c.ContainerId == containerId));

    public InteropMethodCall? FindCall(string methodName, string? containerId = null) =>
        CallsOf(methodName, containerId).LastOrDefault();

    /// <summary>Like <see cref="FindCall"/> but fails the test when the call was never made.</summary>
    public InteropMethodCall RequireCall(string methodName, string? containerId = null) =>
        FindCall(methodName, containerId)
        ?? throw new InvalidOperationException(
            $"Expected an interop invocation of \"{methodName}\" but none was recorded. " +
            $"Recorded methods: [{string.Join(", ", MethodCalls.Select(c => c.MethodName))}]");
}
