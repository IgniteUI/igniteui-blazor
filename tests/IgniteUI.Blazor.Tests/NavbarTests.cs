using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class NavbarTests : BlazorComponentTestBase
{
    [Fact]
    public void Navbar_RendersCorrectElement()
    {
        var cut = Render<IgbNavbar>();
        Assert.NotNull(cut.Find("igc-navbar"));
    }

    [Fact]
    public void Navbar_TypeMetadata_IsCorrect()
    {
        var navbar = new IgbNavbar();
        Assert.Equal("WebNavbar", navbar.Type);
    }

    [Fact]
    public void Navbar_ChildContent_Renders()
    {
        var cut = Render<IgbNavbar>(parameters =>
            parameters.AddChildContent("Navigation Title"));

        Assert.Contains("Navigation Title", cut.Markup);
    }

    [Fact]
    public void Navbar_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbNavbar).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
