using System.Text;
using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Tests.Interop;

/// <summary>
/// <see cref="InteropHarness"/> adapter for the current interop implementation:
/// global JS functions (<c>igSendMessage</c>, <c>igCheckReady</c>, <c>igWaitForLoaded</c>)
/// carrying <c>RendererMessage</c> JSON envelopes, with JS→.NET traffic entering
/// through the public <see cref="WebCallback"/> JSInvokable surface.
/// All knowledge of that wire format is intentionally concentrated here.
/// </summary>
public sealed class RendererMessageInteropHarness : InteropHarness
{
    private const string SendMessage = "igSendMessage";

    private readonly BunitJSInterop _js;
    private readonly IgniteUIBlazor _service;
    // Read by invocation matchers on background flush threads while tests add
    // stubs on the test thread — must be thread-safe.
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, bool> _stubbedMethods = new(StringComparer.Ordinal);
    private readonly Dictionary<string, JSRuntimeInvocationHandler<object>> _methodHandlers = new(StringComparer.Ordinal);

    /// <summary>
    /// Default: forces the JSON data-source channel (as on Blazor Server), keeping data
    /// transfers observable as refChanged messages.
    /// </summary>
    public RendererMessageInteropHarness(BunitJSInterop js)
        : this(js, forceJsonDataMarshalling: true)
    {
    }

    /// <summary>
    /// With <paramref name="forceJsonDataMarshalling"/> false, drives the in-process
    /// (unmarshalled) data channel instead: the service's runtime carries an
    /// InvokeUnmarshalled method that RuntimeHelper discovers by reflection, so
    /// DataSourceManager picks UnmarshalledDataSource and the column messages are
    /// recorded in <see cref="UnmarshalledColumnMessages"/> instead of crossing to JS.
    /// </summary>
    public RendererMessageInteropHarness(BunitJSInterop js, bool forceJsonDataMarshalling)
    {
        _js = js;
        var runtime = forceJsonDataMarshalling
            ? js.JSRuntime
            : new UnmarshalledRecordingRuntime(js.JSRuntime, RecordUnmarshalledMessage);
        _service = new IgniteUIBlazor(runtime, IgniteUIBlazorSettings.Create().WithForceJsonDataMarshalling(forceJsonDataMarshalling));

        // Answer every message with an "undefined" return envelope by default —
        // an unanswered invokeMethod would otherwise await its return forever.
        // Method-specific stubs are excluded here so their handlers always win,
        // regardless of bUnit's handler-resolution order.
        _js.Setup<object>(SendMessage, inv => !IsStubbedInvokeMethod(inv))
            .SetResult(ToResultPayload(InteropReturn.Undefined));
    }

    /// <summary>A data-source column transfer observed on the unmarshalled channel.</summary>
    internal sealed record UnmarshalledColumnMessage(string MethodName, string RefName, int Index, UnmarshalledColumn[]? Columns);

    // Written by the channel on background flush threads, read on the test thread.
    private readonly List<UnmarshalledColumnMessage> _unmarshalledMessages = new();

    internal IReadOnlyList<UnmarshalledColumnMessage> UnmarshalledColumnMessages
    {
        get { lock (_unmarshalledMessages) { return _unmarshalledMessages.ToList(); } }
    }

    /// <summary>Retries briefly — column messages flush on an async queue tick.</summary>
    internal UnmarshalledColumnMessage? WaitForUnmarshalledMessage(Func<UnmarshalledColumnMessage, bool> match)
    {
        for (var attempt = 0; attempt < 80; attempt++)
        {
            var message = UnmarshalledColumnMessages.LastOrDefault(match);
            if (message is not null)
            {
                return message;
            }
            Thread.Sleep(25);
        }
        return null;
    }

    private void RecordUnmarshalledMessage(string methodName, string refName, int index, UnmarshalledColumn[]? columns)
    {
        lock (_unmarshalledMessages)
        {
            _unmarshalledMessages.Add(new UnmarshalledColumnMessage(methodName, refName, index, columns));
        }
    }

    /// <summary>
    /// In-process runtime whose InvokeUnmarshalled methods RuntimeHelper discovers by
    /// name-based reflection — the seam replacing the API modern runtimes removed.
    /// Everything else delegates to bUnit's runtime.
    /// </summary>
    private sealed class UnmarshalledRecordingRuntime : Microsoft.JSInterop.IJSInProcessRuntime
    {
        private readonly Microsoft.JSInterop.IJSInProcessRuntime _inner;
        private readonly Action<string, string, int, UnmarshalledColumn[]?> _record;

        public UnmarshalledRecordingRuntime(Microsoft.JSInterop.IJSRuntime inner, Action<string, string, int, UnmarshalledColumn[]?> record)
        {
            _inner = (Microsoft.JSInterop.IJSInProcessRuntime)inner;
            _record = record;
        }

        public TResult InvokeUnmarshalled<T0, T1, T2, TResult>(string identifier, T0 arg0, T1 arg1, T2 arg2)
        {
            _record(identifier, (string)(object)arg0!, (int)(object)arg1!, (UnmarshalledColumn[]?)(object?)arg2);
            return default!;
        }

        public TResult InvokeUnmarshalled<T0, T1, TResult>(string identifier, T0 arg0, T1 arg1)
        {
            // Data-intents variant; recorded with no columns.
            _record(identifier, (string)(object)arg0!, -1, null);
            return default!;
        }

        public TResult Invoke<TResult>(string identifier, params object?[]? args)
            => _inner.Invoke<TResult>(identifier, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
            => _inner.InvokeAsync<TValue>(identifier, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
            => _inner.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }

    public override IIgniteUIBlazor Service => _service;

    public override void ConfigureServices(IServiceCollection services) =>
        services.AddSingleton<IIgniteUIBlazor>(_service);

    private bool _primed;

    public override void PrimeReady()
    {
        // Idempotent — several contract facts prime independently; registering the
        // bUnit handlers once avoids piling up duplicates.
        if (_primed)
        {
            return;
        }
        _primed = true;
        _js.Setup<bool>("igCheckReady", _ => true).SetResult(true);
        _js.SetupVoid("igWaitForLoaded", _ => true).SetVoidResult();
    }

    public override void MakeReady() => _service.WebCallback.OnReady();

    public override string ContainerIdOf(IRenderedComponent<IComponent> cut) =>
        cut.Find("[data-ig-id]").GetAttribute("data-ig-id")
        ?? throw new InvalidOperationException("Rendered component has no data-ig-id container marker.");

    public override string ContainerIdOf(IRenderedComponent<IComponent> cut, string childSelector) =>
        cut.Find(childSelector).GetAttribute("data-ig-id")
        ?? throw new InvalidOperationException($"Element \"{childSelector}\" has no data-ig-id container marker.");

    public override IReadOnlyList<InteropMethodCall> MethodCalls
    {
        get
        {
            var calls = new List<InteropMethodCall>();
            foreach (var (containerId, message, elements) in Messages())
            {
                if (message.GetProperty("type").GetString() != "invokeMethod")
                {
                    continue;
                }

                calls.Add(new InteropMethodCall(
                    containerId,
                    message.GetProperty("methodName").GetString()!,
                    message.GetProperty("invokeId").GetInt64(),
                    message.TryGetProperty("arguments", out var args)
                        ? [.. args.EnumerateArray()]
                        : [],
                    message.TryGetProperty("types", out var types)
                        ? [.. types.EnumerateArray().Select(t => t.GetString()!)]
                        : [],
                    elements,
                    message));
            }
            return calls;
        }
    }

    public override IReadOnlyList<InteropStateSync> StateSyncs
    {
        get
        {
            var syncs = new List<InteropStateSync>();
            foreach (var (containerId, message, _) in Messages())
            {
                var type = message.GetProperty("type").GetString();
                if (type is not ("description" or "descriptionDelta") ||
                    !message.TryGetProperty("description", out var state))
                {
                    continue;
                }

                syncs.Add(new InteropStateSync(containerId, state, type == "descriptionDelta", message));
            }
            return syncs;
        }
    }

    public override void SetupMethodResult(string methodName, InteropReturn result)
    {
        _stubbedMethods.TryAdd(methodName, true);
        if (!_methodHandlers.TryGetValue(methodName, out var handler))
        {
            handler = _js.Setup<object>(SendMessage, inv => MethodNameOf(inv) == methodName);
            _methodHandlers[methodName] = handler;
        }
        handler.SetResult(ToResultPayload(result));
    }

    public override void SetupPropertyRead(string propertyName, InteropReturn result) =>
        SetupMethodResult(PropertyReadMethodName(propertyName), result);

    public override IEnumerable<InteropMethodCall> PropertyReads(string containerId, string propertyName) =>
        CallsOf(PropertyReadMethodName(propertyName), containerId);

    /// <summary>On this stack a property read is an invokeMethod with a "p:"-prefixed name.</summary>
    private static string PropertyReadMethodName(string propertyName) => "p:" + propertyName;

    public override void RaiseEvent(string containerId, string eventName, string argsJson = "{}", string targetName = "mainControl")
    {
        var payload = $$"""{"sender": {"refType": "name", "id": "{{targetName}}"}, "args": {{argsJson}}}""";
        _service.WebCallback.OnRaiseEvent(containerId, targetName, eventName, payload);
    }

    public override void CompleteDeferred(InteropMethodCall call, InteropReturn result) =>
        _service.WebCallback.OnInvokeReturn(call.ContainerId, call.InvokeId, ToResultPayload(result));

    public override JsonElement? FindPropertyUpdate(string containerId, string wireName)
    {
        // On this stack property updates ride description/descriptionDelta payloads;
        // ref-typed values ride refChanged messages with refName "<containerId>/<PropName>";
        // data sources ride refChanged messages under a generated ref id that the
        // description advertises as "<wireName>Ref". All flush on an async queue tick,
        // so retry briefly before concluding absence.
        var pascalRefName = containerId + "/" + char.ToUpperInvariant(wireName[0]) + wireName[1..];
        // Generous budget: multi-TFM test runs execute three processes concurrently and
        // can starve the queue-flush continuations well past their usual few milliseconds.
        for (var attempt = 0; attempt < 80; attempt++)
        {
            // One snapshot+parse per attempt, scanned newest-first.
            var messages = Messages().Where(m => m.ContainerId == containerId).Reverse().ToList();

            string? dataRefId = null;
            foreach (var (_, message, _) in messages)
            {
                var type = message.GetProperty("type").GetString();
                if (type is not ("description" or "descriptionDelta") ||
                    !message.TryGetProperty("description", out var state))
                {
                    continue;
                }
                if (state.TryGetProperty(wireName, out var value))
                {
                    return value;
                }
                if (dataRefId is null &&
                    state.TryGetProperty(wireName + "Ref", out var refId) &&
                    refId.ValueKind == JsonValueKind.String)
                {
                    dataRefId = refId.GetString();
                }
            }

            foreach (var (_, message, _) in messages)
            {
                if (message.GetProperty("type").GetString() == "refChanged" &&
                    message.TryGetProperty("refName", out var name) &&
                    (name.GetString() == pascalRefName || (dataRefId is not null && name.GetString() == dataRefId)) &&
                    message.TryGetProperty("refValue", out var refValue))
                {
                    return NormalizeRefValue(refValue);
                }
            }

            Thread.Sleep(25);
        }
        return null;
    }

    /// <summary>
    /// refChanged values embed their payload as prefixed strings
    /// (<c>localJson:::{...}</c>, <c>json:::{...}</c>); unwrap to the actual JSON value.
    /// </summary>
    private static JsonElement NormalizeRefValue(JsonElement refValue)
    {
        if (refValue.ValueKind != JsonValueKind.String)
        {
            return refValue;
        }

        var text = refValue.GetString()!;
        var separator = text.IndexOf(":::", StringComparison.Ordinal);
        if (separator < 0)
        {
            return refValue;
        }
        text = text[(separator + 3)..];

        try
        {
            using var doc = JsonDocument.Parse(text);
            return doc.RootElement.Clone();
        }
        catch (JsonException)
        {
            // Non-JSON payloads after the prefix are plain identifiers (script:::fnName,
            // event:::Name) — the identifier is the value; the framing stays harness-owned.
            return JsonSerializer.SerializeToElement(text);
        }
    }

    /// <summary>
    /// How many sends to skip: bUnit's invocation record is append-only, so forgetting earlier
    /// traffic means remembering where the test asked to start looking.
    /// </summary>
    private int _observedFrom;

    public override void ClearObserved() =>
        _observedFrom = SnapshotInvocations().Count(i => i.Identifier == SendMessage);

    /// <summary>
    /// Enumerates every recorded igSendMessage as (containerId, parsed message, element handles).
    /// Element handles are not part of the JSON envelope on this stack — they ride as a
    /// trailing marshalled argument of the call, alongside the component's object reference.
    /// </summary>
    private IEnumerable<(string ContainerId, JsonElement Message, IReadOnlyList<ElementReference> Elements)> Messages()
    {
        var seen = 0;
        foreach (var invocation in SnapshotInvocations())
        {
            if (invocation.Identifier != SendMessage)
            {
                continue;
            }
            if (seen++ < _observedFrom)
            {
                continue;
            }
            if (invocation.Arguments.Count < 2 ||
                invocation.Arguments[0] is not string containerId ||
                invocation.Arguments[1] is not string json)
            {
                continue;
            }

            JsonElement message;
            try
            {
                using var doc = JsonDocument.Parse(json);
                message = doc.RootElement.Clone();
            }
            catch (JsonException)
            {
                continue;
            }

            var elements = invocation.Arguments.Count > 3 && invocation.Arguments[3] is ElementReference[] refs
                ? refs
                : [];
            yield return (containerId, message, elements);
        }
    }

    /// <summary>
    /// Components flush queued messages from background continuations, so bUnit's
    /// append-only invocation record can grow while we read it. Snapshot with retry.
    /// </summary>
    private IReadOnlyList<JSRuntimeInvocation> SnapshotInvocations()
    {
        for (var attempt = 0; ; attempt++)
        {
            try
            {
                return [.. _js.Invocations];
            }
            catch (InvalidOperationException) when (attempt < 10)
            {
                Thread.Yield();
            }
        }
    }

    private bool IsStubbedInvokeMethod(JSRuntimeInvocation invocation)
    {
        var methodName = MethodNameOf(invocation);
        return methodName is not null && _stubbedMethods.ContainsKey(methodName);
    }

    private static string? MethodNameOf(JSRuntimeInvocation invocation)
    {
        if (invocation.Identifier != SendMessage ||
            invocation.Arguments.Count < 2 ||
            invocation.Arguments[1] is not string json)
        {
            return null;
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("type").GetString() == "invokeMethod" &&
                   doc.RootElement.TryGetProperty("methodName", out var name)
                ? name.GetString()
                : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>
    /// The value igSendMessage resolves with: a JSON *string* element containing the
    /// return envelope (<c>{"retType": ..., "value": ...}</c>), matching what the JS
    /// side produces for method invocations.
    /// </summary>
    private static JsonElement ToResultPayload(InteropReturn result) =>
        JsonSerializer.SerializeToElement(BuildReturnEnvelope(result));

    private static string BuildReturnEnvelope(InteropReturn result)
    {
        if (result.Kind == InteropReturnKind.Ref)
        {
            // Bound named objects cross as the bare reference itself — no retType
            // envelope (Loader.ts doReplace emits {refType, id} directly).
            return (string)result.Value!;
        }

        using var stream = new MemoryStream();
        using (var w = new Utf8JsonWriter(stream))
        {
            w.WriteStartObject();
            switch (result.Kind)
            {
                case InteropReturnKind.Undefined:
                    w.WriteString("retType", "undefined");
                    break;
                case InteropReturnKind.Boolean:
                    w.WriteString("retType", "boolean");
                    w.WriteBoolean("value", (bool)result.Value!);
                    break;
                case InteropReturnKind.Number:
                    w.WriteString("retType", "number");
                    w.WriteNumber("value", (double)result.Value!);
                    break;
                case InteropReturnKind.String:
                    w.WriteString("retType", "string");
                    w.WriteString("value", (string)result.Value!);
                    break;
                case InteropReturnKind.Date:
                    w.WriteString("retType", "date");
                    w.WriteString("value", ((DateTime)result.Value!).ToString("o"));
                    break;
                case InteropReturnKind.Deferred:
                    // This stack's spelling of a deferred reply: the client got a JS
                    // promise it can't serialize, so it tags the reply and delivers the
                    // real result later via WebCallback.OnInvokeReturn (invokeId-keyed).
                    w.WriteString("retType", "promise");
                    break;
                case InteropReturnKind.Array:
                    w.WriteString("retType", "Array");
                    w.WritePropertyName("value");
                    using (var doc = JsonDocument.Parse((string)result.Value!))
                    {
                        doc.RootElement.WriteTo(w);
                    }
                    break;
                case InteropReturnKind.Object:
                    w.WriteString("retType", "object");
                    w.WriteString("type", result.TypeName);
                    w.WritePropertyName("value");
                    using (var doc = JsonDocument.Parse((string)result.Value!))
                    {
                        doc.RootElement.WriteTo(w);
                    }
                    break;
            }
            w.WriteEndObject();
        }
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
