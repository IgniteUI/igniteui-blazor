using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ThemeProviderTests : BlazorComponentTestBase
{
    [Fact]
    public void ThemeProvider_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbThemeProvider>();
        cut.Find("igc-theme-provider").Should_Exist();
    }

    [Fact(Skip = "Default should be Bootstrap")]
    public void ThemeProvider_DefaultValues_AreMaterialAndLight()
    {
        var provider = new IgbThemeProvider();

        Assert.Equal(Theme.Bootstrap, provider.Theme);
        Assert.Equal(ThemeVariant.Light, provider.Variant);
    }

    [Fact]
    public void ThemeProvider_DefaultValues_DoNotRenderAttributes()
    {
        var cut = RenderComponent<IgbThemeProvider>();
        var element = cut.Find("igc-theme-provider");

        Assert.Null(element.GetAttribute("theme"));
        Assert.Null(element.GetAttribute("variant"));
    }

    [Theory]
    [InlineData(Theme.Material, "material")]
    [InlineData(Theme.Bootstrap, "bootstrap")]
    [InlineData(Theme.Indigo, "indigo")]
    [InlineData(Theme.Fluent, "fluent")]
    public void ThemeProvider_Theme_SerializesAsAttribute(Theme theme, string expected)
    {
        var cut = RenderComponent<IgbThemeProvider>(parameters =>
            parameters.Add(p => p.Theme, theme));

        var element = cut.Find("igc-theme-provider");
        Assert.Equal(expected, element.GetAttribute("theme"));
    }

    [Theory]
    [InlineData(ThemeVariant.Light, "light")]
    [InlineData(ThemeVariant.Dark, "dark")]
    public void ThemeProvider_Variant_SerializesAsAttribute(ThemeVariant variant, string expected)
    {
        var cut = RenderComponent<IgbThemeProvider>(parameters =>
            parameters.Add(p => p.Variant, variant));

        var element = cut.Find("igc-theme-provider");
        Assert.Equal(expected, element.GetAttribute("variant"));
    }

    [Fact]
    public void ThemeProvider_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbThemeProvider>(parameters =>
            parameters.AddChildContent("Scoped Content"));

        var element = cut.Find("igc-theme-provider");
        Assert.Contains("Scoped Content", element.InnerHtml);
    }
}
