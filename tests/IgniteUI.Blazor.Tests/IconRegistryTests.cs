using System.Text.Json.Nodes;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// The icon registry methods on <see cref="IIgniteUIBlazor"/> call the <c>api.js</c> exports of the same name.
/// Asserted on the JSON that reaches JS, so a member left out and a member sent as <c>null</c> differ.
/// </summary>
public class IconRegistryTests : IDisposable
{
    private readonly SerializingJSRuntime _js = new();
    private readonly ServiceProvider _services;
    private readonly IIgniteUIBlazor _igniteUI;

    public IconRegistryTests()
    {
        _services = new ServiceCollection()
            .AddSingleton<IJSRuntime>(_js)
            .AddIgniteUIBlazor()
            .BuildServiceProvider();
        _igniteUI = _services.GetRequiredService<IIgniteUIBlazor>();
    }

    public void Dispose() => _services.Dispose();

    [Fact]
    public async Task RegisterIconAsync_WithoutCollection_LeavesItOut()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg");

        AssertApiCall("registerIcon", """["home", "/icons/home.svg"]""");
    }

    [Fact]
    public async Task RegisterIconAsync_WithCollection_PassesIt()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg", "material");

        AssertApiCall("registerIcon", """["home", "/icons/home.svg", "material"]""");
    }

    [Fact]
    public async Task RegisterIconAsync_WithOptions_PassesEachMember()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg", new IgbRegisterIconOptions { Collection = "material", StripMeta = true });

        AssertApiCall("registerIcon", """["home", "/icons/home.svg", { "collection": "material", "stripMeta": true }]""");
    }

    [Fact]
    public async Task RegisterIconAsync_WithEmptyOptions_LeavesTheUnsetCollectionOut()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg", new IgbRegisterIconOptions());

        AssertApiCall("registerIcon", """["home", "/icons/home.svg", { "stripMeta": false }]""");
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_WithoutCollection_LeavesItOut()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>");

        AssertApiCall("registerIconFromText", """["home", "<svg></svg>"]""");
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_WithCollection_PassesIt()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", "material");

        AssertApiCall("registerIconFromText", """["home", "<svg></svg>", "material"]""");
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_WithOptions_PassesEachMember()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", new IgbRegisterIconOptions { Collection = "material", StripMeta = true });

        AssertApiCall("registerIconFromText", """["home", "<svg></svg>", { "collection": "material", "stripMeta": true }]""");
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_WithEmptyOptions_LeavesTheUnsetCollectionOut()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", new IgbRegisterIconOptions());

        AssertApiCall("registerIconFromText", """["home", "<svg></svg>", { "stripMeta": false }]""");
    }

    [Fact]
    public async Task SetIconRefAsync_PassesTheTargetAsNameAndCollection()
    {
        await _igniteUI.SetIconRefAsync("close", "default", new IgbIconMeta { Name = "x-mark", Collection = "material" });

        AssertApiCall("setIconRef", """["close", "default", { "name": "x-mark", "collection": "material" }]""");
    }

    [Fact]
    public async Task Calls_ShareOneImportOfTheModule()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>");
        await _igniteUI.RegisterIconFromTextAsync("search", "<svg></svg>");

        var import = Assert.Single(_js.Calls, c => c.Identifier == "import");
        AssertJson($"""["{InteropModule.ApiPath}"]""", import.ArgsJson);
        Assert.Equal(2, _js.Calls.Count(c => c.TargetInstanceId == SerializingJSRuntime.ModuleId));
    }

    [Fact]
    public async Task NullArguments_Throw_WithoutCallingTheClient()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("igniteUI", () => ((IIgniteUIBlazor)null!).RegisterIconFromTextAsync("home", "<svg></svg>").AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("name", () => _igniteUI.RegisterIconFromTextAsync(null!, "<svg></svg>").AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("iconText", () => _igniteUI.RegisterIconFromTextAsync("home", null!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("collection", () => _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", default(string)!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("url", () => _igniteUI.RegisterIconAsync("home", null!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("collection", () => _igniteUI.RegisterIconAsync("home", "/icons/home.svg", default(string)!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("options", () => _igniteUI.RegisterIconAsync("home", "/icons/home.svg", default(IgbRegisterIconOptions)!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("icon", () => _igniteUI.SetIconRefAsync("close", "default", null!).AsTask());

        Assert.Empty(_js.Calls);
    }

    private void AssertApiCall(string identifier, string expectedArgsJson)
    {
        var call = Assert.Single(_js.Calls, c => c.TargetInstanceId == SerializingJSRuntime.ModuleId);
        Assert.Equal(identifier, call.Identifier);
        AssertJson(expectedArgsJson, call.ArgsJson);
    }

    // Structural: member order and the serializer's escaping (e.g. of '<') do not matter.
    private static void AssertJson(string expected, string actual)
        => Assert.True(JsonNode.DeepEquals(JsonNode.Parse(expected), JsonNode.Parse(actual)), $"Expected {expected}, but JS received {actual}.");

    /// <summary>
    /// Records the arguments of each call as the JSON <see cref="JSRuntime"/> serialized them, and completes the
    /// call at once: the module import with a module reference, everything else with no value.
    /// </summary>
    private sealed class SerializingJSRuntime : JSRuntime
    {
        internal const long ModuleId = 1;

        public List<(string Identifier, string ArgsJson, long TargetInstanceId)> Calls { get; } = [];

        protected override void BeginInvokeJS(long taskId, string identifier, string? argsJson)
            => BeginInvokeJS(taskId, identifier, argsJson, JSCallResultType.Default, 0);

        protected override void BeginInvokeJS(long taskId, string identifier, string? argsJson, JSCallResultType resultType, long targetInstanceId)
        {
            Calls.Add((identifier, argsJson ?? "[]", targetInstanceId));
            var result = resultType == JSCallResultType.JSObjectReference ? $$"""{"__jsObjectId":{{ModuleId}}}""" : "null";
            DotNetDispatcher.EndInvokeJS(this, $"[{taskId},true,{result}]");
        }

        protected override void EndInvokeDotNet(DotNetInvocationInfo invocationInfo, in DotNetInvocationResult invocationResult)
        {
        }
    }
}
