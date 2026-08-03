using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;
using Xunit.Sdk;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Marker type so a violation from a contract spec never gets double-wrapped, while
/// every other exception (asserts, harness errors, and unexpected ones like a bad cast
/// inside a component's generated handler) gains the offending member's name.
/// </summary>
public sealed class ContractViolationException : XunitException
{
    private readonly string? _specFrame;

    public ContractViolationException(string message, Exception inner, SpecSource? source)
        : base(message, inner)
    {
        if (source is { File.Length: > 0 })
        {
            _specFrame = $"   at contract spec in {source.File}:line {source.Line}";
        }
    }

    /// <summary>
    /// Prepends the declaring contract line as a synthetic top frame, so test UIs
    /// navigate to the violated spec instead of the shared runner.
    /// </summary>
    public override string? StackTrace => _specFrame is null
        ? base.StackTrace
        : _specFrame + Environment.NewLine + base.StackTrace;
}

/// <summary>
/// Test base for component suites whose component has an interop surface: extends
/// <see cref="BlazorComponentTestBase"/> with a declarative
/// <see cref="ComponentContract{TComponent}"/> executed against the component's
/// <see cref="InteropHarness"/> (resolved via <see cref="BlazorComponentTestBase.InteropFor{TComponent}"/>,
/// so a component remapped to a new interop stack runs the same contract unchanged).
/// Exactly one suite per component should carry the contract; components without
/// interop (e.g. Badge) stay on <see cref="BlazorComponentTestBase"/>.
/// </summary>
public abstract class ComponentWithContractTestBase<TComponent> : BlazorComponentTestBase
    where TComponent : class, IComponent
{
    /// <summary>
    /// The component's interop contract to exercise against a harness.
    /// <remarks>
    /// Pair with one or more specs:
    /// <code>Methods_FollowContract() => <see cref="VerifyMethodContract"/></code>
    /// <code>Props_FollowContract() => <see cref="VerifyPropContract"/></code>
    /// <code>Events_FollowContract() => <see cref="VerifyEventContract"/></code>
    /// Each method, property, and event in the contract is exercised in isolation, with a fresh component instance (or a shared instance for methods that don't require an arrangement).
    /// The harness is primed to a ready state before each test, and any stubbed return values are set up before invoking the method or reading the property.
    /// The test asserts that the expected interop calls are made with the correct arguments and types, and that the return values are as expected.
    /// For events, the test raises the event with the specified arguments.
    /// </remarks>
    /// </summary>
    protected abstract ComponentContract<TComponent> InteropContract { get; }

    /// <summary>
    /// Ensures a suite can't declare contract specs that never run: every non-empty
    /// contract section must have its runner fact declared on the suite (they are
    /// per-suite one-liners so failures navigate to the suite, and suites with nothing
    /// to check in a section simply omit that fact).
    /// </summary>
    [Fact]
    public void Contract_SectionsHaveFacts()
    {
        var contract = InteropContract;
        RequireSectionFact(contract.Methods.Count > 0, "Methods_FollowContract", nameof(VerifyMethodContract));
        RequireSectionFact(contract.Props.Count > 0, "Props_FollowContract", nameof(VerifyPropContract));
        RequireSectionFact(contract.Events.Count > 0, "Events_FollowContract", nameof(VerifyEventContract));
    }

    private void RequireSectionFact(bool hasSpecs, string factName, string runnerName)
    {
        if (!hasSpecs)
        {
            return;
        }
        var fact = GetType().GetMethod(factName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (fact is null || fact.GetCustomAttributes(typeof(FactAttribute), true).Length == 0)
        {
            throw new XunitException(
                $"{GetType().Name}'s contract declares specs that never run — add: " +
                $"[Fact] public {(runnerName == nameof(VerifyMethodContract) ? "Task" : "void")} {factName}() => {runnerName}();");
        }
    }

    /// <summary>
    /// Runner for the contract's <c>.Method</c>/<c>.Getter</c> specs — expose it on the suite as
    /// <c>[Fact] public Task Methods_FollowContract() => VerifyMethodContract();</c>.
    /// For each spec: stubs the JS-side result, invokes the .NET API member, and asserts
    /// the wire identifier, argument values, type tags, and the decoded return. Specs
    /// without an arrangement share one rendered instance; arranged specs render their own.
    /// </summary>
    /// <exception cref="ContractViolationException">
    /// Any spec failure, naming the violated member and carrying the declaring contract
    /// line as the top stack frame.
    /// </exception>
    protected async Task VerifyMethodContract()
    {
        var harness = InteropFor<TComponent>();
        harness.PrimeReady();
        IRenderedComponent<TComponent>? sharedCut = null;

        foreach (var method in InteropContract.Methods)
        {
            try
            {
                // Hosted specs render the parent structure and pick the cut out of it;
                // specs with an arrangement render their own instance.
                IRenderedComponent<TComponent> cut;
                IRenderedComponent<IComponent> scope;
                if (method.Host is not null)
                {
                    scope = method.Host(this);
                    cut = method.Target!(scope);
                }
                else
                {
                    cut = method.Arrange is null
                        ? sharedCut ??= Render<TComponent>()
                        : Render<TComponent>(ps => method.Arrange(ps));
                    scope = cut;
                }

                await RunSpec(harness, cut, scope, method);
            }
            catch (Exception ex) when (ex is not ContractViolationException)
            {
                var member = method.ReadsProperty is not null
                    ? $"getter \"{method.ReadsProperty}\""
                    : $"method \"{method.JsName}\"";
                throw Violation(member, method.Source, ex);
            }
        }
    }

    /// <summary>
    /// Stub → invoke → assert, for both kinds of spec. A current-state read differs from an
    /// API call in only two ways — who names the wire identifier (the harness, from the
    /// property name, vs. the spec) and that it carries no arguments — so the invocation,
    /// sync-twin and return-decoding dance is written once, here.
    /// </summary>
    private static async Task RunSpec(
        InteropHarness harness, IRenderedComponent<TComponent> cut, IRenderedComponent<IComponent> scope, MethodContractSpec<TComponent> method)
    {
        var isRead = method.ReadsProperty is not null;

        // Stub immediately before invoking so specs may reuse a member
        // with different stubbed results.
        var stub = method.Stub?.Get(harness, scope);
        if (isRead)
        {
            harness.SetupPropertyRead(method.ReadsProperty!, stub!);
        }
        else if (stub is not null)
        {
            harness.SetupMethodResult(method.JsName!, stub);
        }

        var containerId = harness.ContainerIdOf(cut);
        // How a read is identified on the wire stays harness-owned; a call's identifier is the spec's.
        Func<IEnumerable<InteropMethodCall>> matching = isRead
            ? () => harness.PropertyReads(containerId, method.ReadsProperty!)
            : () => harness.CallsOf(method.JsName!, containerId);
        var noNewCall = isRead
            ? $"no new current-state read was issued for \"{method.ReadsProperty}\""
            : $"\"{method.JsName}\" sent no new invocation";

        var (call, result) = await InvokeExpectingNewCall(matching, () => method.Invoke(cut.Instance), noNewCall);
        AssertObserved(harness, cut, scope, method, call, result);

        if (method.SyncInvoke is not null)
        {
            // The sync twin must produce its own invocation and decode the same reply
            // (the stub persists) to the same result.
            var (syncCall, syncResult) = await InvokeExpectingNewCall(
                matching,
                () => Task.FromResult(method.SyncInvoke(cut.Instance)),
                "sync twin: " + noNewCall);
            AssertObserved(harness, cut, scope, method, syncCall, syncResult);
        }
    }

    /// <summary>Asserts everything the spec pins about one observed invocation: its wire shape (calls only) and its decoded return.</summary>
    private static void AssertObserved(
        InteropHarness harness, IRenderedComponent<TComponent> cut, IRenderedComponent<IComponent> scope,
        MethodContractSpec<TComponent> method, InteropMethodCall call, object? result)
    {
        // A read carries no arguments, type tags or element handles — there is no wire shape to pin.
        if (method.ReadsProperty is null)
        {
            AssertCallShape(harness, cut, method, call);
        }
        if (method.HasExpectedReturn)
        {
            AssertReturn(method.ExpectedReturn, result);
        }
        method.AssertReturnWithCut?.Invoke(scope, result);
    }

    /// <summary>
    /// Runs an invocation and returns the newest call it added to the matching set —
    /// requiring a NEW entry regardless of prior history, so specs may chain the same
    /// member repeatedly (different args/stubs) without matching a predecessor's call.
    /// </summary>
    private static async Task<(InteropMethodCall Call, object? Result)> InvokeExpectingNewCall(
        Func<IEnumerable<InteropMethodCall>> matching,
        Func<Task<object?>> invoke,
        string noNewCallMessage)
    {
        var before = matching().Count();
        var result = await invoke();
        var after = matching().ToList();
        if (after.Count <= before)
        {
            throw new XunitException(noNewCallMessage);
        }
        return (after[^1], result);
    }

    /// <summary>Asserts a recorded invocation matches the spec's expected args, type tags, and element handles.</summary>
    private static void AssertCallShape(
        InteropHarness harness, IRenderedComponent<TComponent> cut, MethodContractSpec<TComponent> method, InteropMethodCall call)
    {
        Assert.Equal(method.ExpectedTypes, call.Types);
        Assert.Equal(method.ExpectedArgs.Length, call.Arguments.Count);
        for (var i = 0; i < method.ExpectedArgs.Length; i++)
        {
            AssertWireValue(Resolve(method.ExpectedArgs[i], harness, cut), call.Arguments[i]);
        }
        AssertElements(method.ExpectedElements?.Invoke() ?? [], call.Elements);
    }

    /// <summary>Settles a late expectation (see <see cref="FromRender{T}"/>) against the render; every other value is already final.</summary>
    private static object? Resolve(object? expected, InteropHarness harness, IRenderedComponent<IComponent> scope) =>
        expected is IFromRender late ? late.Resolve(harness, scope) : expected;

    /// <summary>
    /// Asserts the element handles riding with the invocation, by id — a handle's id is
    /// what makes it resolvable on the client, so a spec expecting one without an id means
    /// its arrangement never captured a real element (which would pass vacuously).
    /// </summary>
    private static void AssertElements(IReadOnlyList<ElementReference> expected, IReadOnlyList<ElementReference> actual)
    {
        Assert.Equal(expected.Count, actual.Count);
        for (var i = 0; i < expected.Count; i++)
        {
            if (string.IsNullOrEmpty(expected[i].Id))
            {
                throw new XunitException(
                    "the spec expects an element handle with no id — check its arrangement captured a rendered element");
            }
            Assert.Equal(expected[i].Id, actual[i].Id);
        }
    }

    /// <summary>
    /// Runner for the contract's <c>.Prop</c> specs — expose it on the suite as
    /// <c>[Fact] public void Props_FollowContract() => VerifyPropContract();</c>.
    /// Renders a fresh instance per spec with the parameter (plus any arrangement) set,
    /// then asserts a property update carrying the expected wire value was transmitted —
    /// on whichever channel the current stack uses (the harness abstracts state
    /// descriptions vs. ref transfers).
    /// </summary>
    /// <exception cref="ContractViolationException">
    /// Any spec failure, naming the violated member and carrying the declaring contract
    /// line as the top stack frame.
    /// </exception>
    protected void VerifyPropContract()
    {
        var harness = InteropFor<TComponent>();
        harness.PrimeReady();
        foreach (var prop in InteropContract.Props)
        {
            try
            {
                var cut = Render<TComponent>(ps =>
                {
                    prop.Arrange?.Invoke(ps);
                    prop.Set(ps);
                });

                var actual = harness.FindPropertyUpdate(harness.ContainerIdOf(cut), prop.WireName);

                if (actual is null)
                {
                    throw new XunitException(
                        "no property update transmission was observed — check the wire name, " +
                        "and whether this prop crosses as a rendered attribute instead (not a .Prop case)");
                }
                AssertWireValue(prop.ExpectedValue.Get(harness, cut), actual.Value);
            }
            catch (Exception ex) when (ex is not ContractViolationException)
            {
                throw Violation($"prop \"{prop.WireName}\"", prop.Source, ex);
            }
        }
    }

    /// <summary>
    /// Runner for the contract's <c>.Event</c> specs — expose it on the suite as
    /// <c>[Fact] public void Events_FollowContract() => VerifyEventContract();</c>.
    /// Renders a fresh instance per spec with the callback bound (plus any arrangement),
    /// then verifies the full loop: the member returns the exact callback bound and an
    /// event-handler registration transmits over interop (the client's cue to subscribe); the
    /// spec's args payload dispatched under the wire event name reaches the handler
    /// with args of the declared type satisfying the spec's asserts; and unbinding
    /// (setting the parameter back to an empty callback) resets the member, transmits
    /// the cleared registration (the client's cue to unsubscribe), and stops delivery —
    /// a subsequent dispatch must not reach the handler.
    /// </summary>
    /// <exception cref="ContractViolationException">
    /// Any spec failure, naming the violated member and carrying the declaring contract
    /// line as the top stack frame.
    /// </exception>
    protected void VerifyEventContract()
    {
        var harness = InteropFor<TComponent>();
        // Dispatch itself needs no readiness, but arranged data (e.g. data-source items
        // referenced by uuid in payloads) only transfers once the component is ready.
        harness.PrimeReady();
        foreach (var evt in InteropContract.Events)
        {
            try
            {
                object? received = null;
                object bound = null!;
                var cut = Render<TComponent>(ps =>
                {
                    evt.Arrange?.Invoke(ps);
                    bound = evt.Bind(ps, args => received = args);
                });
                var containerId = harness.ContainerIdOf(cut);

                // Binding must round-trip: the member returns the exact callback
                // assigned — and it must transmit an event-handler registration
                // (the client's cue to subscribe) carrying the member's event
                // identity; dispatch alone can't catch a registration that never
                // reached the wire.
                Assert.Equal(bound, evt.Get(cut.Instance));
                var wireName = char.ToLowerInvariant(evt.EventName[0]) + evt.EventName[1..];
                var registration = harness.FindPropertyUpdate(containerId, wireName)
                    ?? throw new XunitException("no event-handler registration transmission was observed");
                Assert.Equal(evt.EventName, registration.GetString());

                var argsJson = evt.ArgsJson.Get(harness, cut);
                harness.RaiseEvent(containerId, evt.EventName, argsJson);

                if (received is null)
                {
                    // Dispatch failures are swallowed by the control, so a missing
                    // callback is the signal for a wrong name or malformed payload.
                    throw new XunitException(
                        "handler was not invoked — check the event name and the args JSON shape");
                }
                Assert.IsAssignableFrom(evt.ArgsType, received);
                evt.AssertArgs?.Invoke(received);
                evt.AssertWithComponent?.Invoke(cut.Instance, received);
                evt.AssertWithCut?.Invoke(cut, received);

                // TODO: removing a bound callback crashes today, on either path. Razor-bound
                // values arrive wrapped by EventCallback.Factory.Create, so a "default"
                // carries a Receiver with a null Delegate — that fails the setter's Empty
                // check, takes the *bound* branch, and NREs in
                // BaseRendererControl.CompareEventCallbacks on leftDelegate.Equals(...);
                // a raw Empty (what bUnit/programmatic SetParameters passes) does reach the
                // unset branch and NREs in OnRefChanged on newValue.ToString(). Once fixed,
                // validate the removal round-trip; pin the actual cleared wire shape then:
                // cut.SetParametersAndRender(ps => bound = evt.Bind(ps, null));
                // Assert.Equal(bound, evt.Get(cut.Instance)); // member resets to the empty callback
                // var cleared = harness.FindPropertyUpdate(containerId, wireName);
                // Assert.Equal(JsonValueKind.Null, cleared!.Value.ValueKind);
                // received = null;
                // harness.RaiseEvent(containerId, evt.EventName, argsJson);
                // Assert.Null(received); // deregistered handlers must not be invoked
            }
            catch (Exception ex) when (ex is not ContractViolationException)
            {
                throw Violation($"event \"{evt.EventName}\"", evt.Source, ex);
            }
        }
    }

    /// <summary>Wraps a spec failure with the component and member it violates (see <see cref="ContractViolationException"/>).</summary>
    private static ContractViolationException Violation(string member, SpecSource? source, Exception inner) =>
        new($"{typeof(TComponent).Name} contract violated at {member}: {inner.Message}", inner, source);

    /// <summary>
    /// Compares an expected contract value against the transmitted JSON: scalars by value
    /// (numbers as double, dates as instants), <see cref="RawJson"/> structurally and
    /// exactly, <see cref="JsonSubset"/> per <see cref="AssertJsonSubset"/>.
    /// </summary>
    private static void AssertWireValue(object? expected, JsonElement actual)
    {
        switch (expected)
        {
            case null:
                Assert.Equal(JsonValueKind.Null, actual.ValueKind);
                break;
            case bool b:
                Assert.Equal(b ? JsonValueKind.True : JsonValueKind.False, actual.ValueKind);
                break;
            case string s:
                Assert.Equal(JsonValueKind.String, actual.ValueKind);
                Assert.Equal(s, actual.GetString());
                break;
            case DateTime dt:
                Assert.Equal(JsonValueKind.String, actual.ValueKind);
                Assert.Equal(
                    dt.ToUniversalTime(),
                    DateTime.Parse(actual.GetString()!, null, System.Globalization.DateTimeStyles.RoundtripKind).ToUniversalTime());
                break;
            case RawJson raw:
                using (var doc = JsonDocument.Parse(raw.Json))
                {
                    Assert.Equal(JsonSerializer.Serialize(doc.RootElement), JsonSerializer.Serialize(actual));
                }
                break;
            case JsonSubset subset:
                using (var doc = JsonDocument.Parse(subset.Json))
                {
                    AssertJsonSubset(doc.RootElement, actual);
                }
                break;
            case IConvertible:
                Assert.Equal(JsonValueKind.Number, actual.ValueKind);
                Assert.Equal(Convert.ToDouble(expected), actual.GetDouble());
                break;
            default:
                throw new ArgumentException(
                    $"Unsupported expected wire value of type {expected.GetType().Name}; use RawJson for structured values.");
        }
    }

    /// <summary>
    /// Subset comparison: objects require every expected property to match (extra actual
    /// properties are ignored, recursively); arrays require equal length with element-wise
    /// subset matching; scalars compare exactly.
    /// </summary>
    private static void AssertJsonSubset(JsonElement expected, JsonElement actual)
    {
        switch (expected.ValueKind)
        {
            case JsonValueKind.Object:
                Assert.Equal(JsonValueKind.Object, actual.ValueKind);
                foreach (var expectedProp in expected.EnumerateObject())
                {
                    if (!actual.TryGetProperty(expectedProp.Name, out var actualProp))
                    {
                        throw new XunitException(
                            $"expected property \"{expectedProp.Name}\" missing from transmitted value: {actual}");
                    }
                    AssertJsonSubset(expectedProp.Value, actualProp);
                }
                break;
            case JsonValueKind.Array:
                Assert.Equal(JsonValueKind.Array, actual.ValueKind);
                Assert.Equal(expected.GetArrayLength(), actual.GetArrayLength());
                var actualItems = actual.EnumerateArray().ToArray();
                var i = 0;
                foreach (var expectedItem in expected.EnumerateArray())
                {
                    AssertJsonSubset(expectedItem, actualItems[i++]);
                }
                break;
            default:
                Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
                break;
        }
    }

    /// <summary>Compares a decoded .NET return against the spec's expected value.</summary>
    private static void AssertReturn(object? expected, object? actual)
    {
        // Date returns are decoded to local time; compare instants instead of kinds.
        if (expected is DateTime expectedDate && actual is DateTime actualDate)
        {
            Assert.Equal(expectedDate.ToUniversalTime(), actualDate.ToUniversalTime());
            return;
        }
        Assert.Equal(expected, actual);
    }
}
