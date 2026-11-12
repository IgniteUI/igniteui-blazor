using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Tests;

/// <summary>The icon registry methods on <see cref="IIgniteUIBlazor"/> call the <c>api.js</c> exports of the same name.</summary>
public class IconRegistryTests : BunitContext
{
    private readonly BunitJSModuleInterop _api;
    private readonly IIgniteUIBlazor _igniteUI;

    public IconRegistryTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        _api = JSInterop.SetupModule(InteropModule.ApiPath);
        Services.AddIgniteUIBlazor();
        _igniteUI = Services.GetRequiredService<IIgniteUIBlazor>();
    }

    [Fact]
    public async Task RegisterIconAsync_CallsRegisterIcon()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg", "material");

        var call = Assert.Single(_api.Invocations["registerIcon"]);
        Assert.Equal(["home", "/icons/home.svg", "material"], call.Arguments);
    }

    [Fact]
    public async Task RegisterIconAsync_WithoutCollection_PassesNull_SoTheClientDefaults()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg");

        var call = Assert.Single(_api.Invocations["registerIcon"]);
        Assert.Equal(["home", "/icons/home.svg", null], call.Arguments);
    }

    [Fact]
    public async Task RegisterIconAsync_WithOptions_PassesTheOptionsObject()
    {
        await _igniteUI.RegisterIconAsync("home", "/icons/home.svg", new IgbRegisterIconOptions { Collection = "material", StripMeta = true });

        var call = Assert.Single(_api.Invocations["registerIcon"]);
        Assert.Equal(["home", "/icons/home.svg"], call.Arguments.Take(2));
        Assert.Equal(new Dictionary<string, object?> { ["collection"] = "material", ["stripMeta"] = true }, call.Arguments[2]);
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_CallsRegisterIconFromText()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", "material");

        var call = Assert.Single(_api.Invocations["registerIconFromText"]);
        Assert.Equal(["home", "<svg></svg>", "material"], call.Arguments);
    }

    [Fact]
    public async Task RegisterIconFromTextAsync_WithOptions_PassesTheOptionsObject()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>", new IgbRegisterIconOptions { StripMeta = true });

        var call = Assert.Single(_api.Invocations["registerIconFromText"]);
        Assert.Equal(["home", "<svg></svg>"], call.Arguments.Take(2));
        Assert.Equal(new Dictionary<string, object?> { ["collection"] = null, ["stripMeta"] = true }, call.Arguments[2]);
    }

    [Fact]
    public async Task SetIconRefAsync_PassesTheTargetAsNameAndCollection()
    {
        await _igniteUI.SetIconRefAsync("close", "default", new IgbIconMeta { Name = "x-mark", Collection = "material" });

        var call = Assert.Single(_api.Invocations["setIconRef"]);
        Assert.Equal(["close", "default"], call.Arguments.Take(2));
        Assert.Equal(new Dictionary<string, object?> { ["name"] = "x-mark", ["collection"] = "material" }, call.Arguments[2]);
    }

    [Fact]
    public async Task Calls_ShareOneImportOfTheModule()
    {
        await _igniteUI.RegisterIconFromTextAsync("home", "<svg></svg>");
        await _igniteUI.RegisterIconFromTextAsync("search", "<svg></svg>");

        Assert.Single(JSInterop.Invocations["import"], i => Equals(i.Arguments[0], InteropModule.ApiPath));
    }

    [Fact]
    public async Task NullArguments_Throw_WithoutCallingTheClient()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("name", () => _igniteUI.RegisterIconFromTextAsync(null!, "<svg></svg>").AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("iconText", () => _igniteUI.RegisterIconFromTextAsync("home", null!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("options", () => _igniteUI.RegisterIconAsync("home", "/icons/home.svg", (IgbRegisterIconOptions)null!).AsTask());
        await Assert.ThrowsAsync<ArgumentNullException>("icon", () => _igniteUI.SetIconRefAsync("close", "default", null!).AsTask());

        Assert.Empty(_api.Invocations);
    }
}
