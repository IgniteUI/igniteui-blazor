using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Tests.Interop;

/// <summary>
/// A component API method invocation observed on the interop layer,
/// normalized away from the concrete wire format.
/// </summary>
public sealed record InteropMethodCall(
    string ContainerId,
    string MethodName,
    long InvokeId,
    IReadOnlyList<JsonElement> Arguments,
    IReadOnlyList<string> Types,
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
    Promise
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

    /// <summary>A deferred return; complete it later via <see cref="InteropHarness.CompletePromise"/>.</summary>
    public static readonly InteropReturn Promise = new(InteropReturnKind.Promise);

    public static InteropReturn Bool(bool value) => new(InteropReturnKind.Boolean, value);
    public static InteropReturn Number(double value) => new(InteropReturnKind.Number, value);
    public static InteropReturn String(string value) => new(InteropReturnKind.String, value);
    public static InteropReturn Date(DateTime value) => new(InteropReturnKind.Date, value);
    public static InteropReturn Object(string typeName, string valueJson) => new(InteropReturnKind.Object, valueJson, typeName);
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
    public abstract string ContainerIdOf(IRenderedFragment cut);

    /// <summary>All API method invocations sent to JS so far, in order.</summary>
    public abstract IReadOnlyList<InteropMethodCall> MethodCalls { get; }

    /// <summary>All component state synchronizations sent to JS so far, in order.</summary>
    public abstract IReadOnlyList<InteropStateSync> StateSyncs { get; }

    /// <summary>Stubs the JS-side return value for invocations of <paramref name="methodName"/>.</summary>
    public abstract void SetupMethodResult(string methodName, InteropReturn result);

    /// <summary>
    /// Dispatches a JS-originated component event into .NET.
    /// <paramref name="argsJson"/> is the plain JSON payload of the event args
    /// (e.g. <c>{"detail": true}</c>); the harness applies any wire framing.
    /// </summary>
    public abstract void RaiseEvent(string containerId, string eventName, string argsJson = "{}", string targetName = "mainControl");

    /// <summary>Completes a method invocation that was answered with <see cref="InteropReturn.Promise"/>.</summary>
    public abstract void CompletePromise(InteropMethodCall call, InteropReturn result);

    /// <summary>The component's serialized state as it would cross the interop boundary.</summary>
    public abstract JsonElement SerializeState(object component);

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
