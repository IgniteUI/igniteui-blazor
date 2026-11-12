using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace IgniteUI.Blazor.Tests;

/// <summary><c>ConfigureThemeAsync</c> on <see cref="IIgniteUIBlazor"/> calls the <c>configureTheme</c> export of <c>api.js</c>.</summary>
public class ThemeConfigurationTests : BunitContext
{
    private readonly BunitJSModuleInterop _api;
    private readonly IIgniteUIBlazor _igniteUI;

    public ThemeConfigurationTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        _api = JSInterop.SetupModule(InteropModule.ApiPath);
        Services.AddIgniteUIBlazor();
        _igniteUI = Services.GetRequiredService<IIgniteUIBlazor>();
    }

    [Theory]
    [InlineData(Theme.Material, ThemeVariant.Dark, "material", "dark")]
    [InlineData(Theme.Bootstrap, ThemeVariant.Light, "bootstrap", "light")]
    [InlineData(Theme.Indigo, ThemeVariant.Dark, "indigo", "dark")]
    [InlineData(Theme.Fluent, ThemeVariant.Light, "fluent", "light")]
    public async Task ConfigureThemeAsync_PassesTheClientNames(Theme theme, ThemeVariant variant, string clientTheme, string clientVariant)
    {
        await _igniteUI.ConfigureThemeAsync(theme, variant);

        var call = Assert.Single(_api.Invocations["configureTheme"]);
        Assert.Equal([clientTheme, clientVariant], call.Arguments);
    }

    [Fact]
    public async Task ConfigureThemeAsync_WithoutVariant_UsesLight()
    {
        await _igniteUI.ConfigureThemeAsync(Theme.Material);

        var call = Assert.Single(_api.Invocations["configureTheme"]);
        Assert.Equal(["material", "light"], call.Arguments);
    }

    [Fact]
    public async Task Calls_ShareOneImportOfTheModule()
    {
        await _igniteUI.ConfigureThemeAsync(Theme.Material, ThemeVariant.Dark);
        await _igniteUI.ConfigureThemeAsync(Theme.Material, ThemeVariant.Light);

        Assert.Single(JSInterop.Invocations["import"], i => Equals(i.Arguments[0], InteropModule.ApiPath));
    }

    [Fact]
    public async Task NullService_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>("igniteUI", () => ((IIgniteUIBlazor)null!).ConfigureThemeAsync(Theme.Material).AsTask());
    }
}
