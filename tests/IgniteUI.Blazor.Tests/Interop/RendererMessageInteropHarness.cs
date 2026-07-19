using System.Text;
using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
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
    private readonly HashSet<string> _stubbedMethods = new(StringComparer.Ordinal);
    private readonly Dictionary<string, JSRuntimeInvocationHandler<object>> _methodHandlers = new(StringComparer.Ordinal);

    public RendererMessageInteropHarness(BunitJSInterop js)
    {
        _js = js;
        _service = new IgniteUIBlazor(js.JSRuntime);

        // Answer every message with an "undefined" return envelope by default —
        // an unanswered invokeMethod would otherwise await its return forever.
        // Method-specific stubs are excluded here so their handlers always win,
        // regardless of bUnit's handler-resolution order.
        _js.Setup<object>(SendMessage, inv => !IsStubbedInvokeMethod(inv))
            .SetResult(ToResultPayload(InteropReturn.Undefined));
    }

    public override IIgniteUIBlazor Service => _service;

    public override void ConfigureServices(IServiceCollection services) =>
        services.AddSingleton<IIgniteUIBlazor>(_service);

    public override void PrimeReady()
    {
        _js.Setup<bool>("igCheckReady", _ => true).SetResult(true);
        _js.SetupVoid("igWaitForLoaded", _ => true).SetVoidResult();
    }

    public override void MakeReady() => _service.WebCallback.OnReady();

    public override string ContainerIdOf(IRenderedFragment cut) =>
        cut.Find("[data-ig-id]").GetAttribute("data-ig-id")
        ?? throw new InvalidOperationException("Rendered component has no data-ig-id container marker.");

    public override IReadOnlyList<InteropMethodCall> MethodCalls
    {
        get
        {
            var calls = new List<InteropMethodCall>();
            foreach (var (containerId, message) in Messages())
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
                        ? args.EnumerateArray().ToArray()
                        : Array.Empty<JsonElement>(),
                    message.TryGetProperty("types", out var types)
                        ? types.EnumerateArray().Select(t => t.GetString()!).ToArray()
                        : Array.Empty<string>(),
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
            foreach (var (containerId, message) in Messages())
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
        _stubbedMethods.Add(methodName);
        if (!_methodHandlers.TryGetValue(methodName, out var handler))
        {
            handler = _js.Setup<object>(SendMessage, inv => MethodNameOf(inv) == methodName);
            _methodHandlers[methodName] = handler;
        }
        handler.SetResult(ToResultPayload(result));
    }

    public override void RaiseEvent(string containerId, string eventName, string argsJson = "{}", string targetName = "mainControl")
    {
        var payload = $$"""{"sender": {"refType": "name", "id": "{{targetName}}"}, "args": {{argsJson}}}""";
        _service.WebCallback.OnRaiseEvent(containerId, targetName, eventName, payload);
    }

    public override void CompletePromise(InteropMethodCall call, InteropReturn result) =>
        _service.WebCallback.OnInvokeReturn(call.ContainerId, call.InvokeId, ToResultPayload(result));

    public override JsonElement SerializeState(object component)
    {
        var json = component switch
        {
            BaseRendererControl control => control.Serialize(),
            JsonSerializable serializable => SerializeToString(serializable),
            _ => throw new ArgumentException($"{component.GetType().Name} is not interop-serializable.", nameof(component)),
        };
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.Clone();
    }

    private static string SerializeToString(JsonSerializable serializable)
    {
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream))
        {
            serializable.Serialize(new SerializationContext(writer, null!));
        }
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>Enumerates every recorded igSendMessage as (containerId, parsed message).</summary>
    private IEnumerable<(string ContainerId, JsonElement Message)> Messages()
    {
        foreach (var invocation in _js.Invocations)
        {
            if (invocation.Identifier != SendMessage ||
                invocation.Arguments.Count < 2 ||
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

            yield return (containerId, message);
        }
    }

    private bool IsStubbedInvokeMethod(JSRuntimeInvocation invocation)
    {
        var methodName = MethodNameOf(invocation);
        return methodName is not null && _stubbedMethods.Contains(methodName);
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
                case InteropReturnKind.Promise:
                    w.WriteString("retType", "promise");
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
