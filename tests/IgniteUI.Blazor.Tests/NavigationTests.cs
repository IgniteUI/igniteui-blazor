using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class NavbarTests : BlazorComponentTestBase
{
    [Fact]
    public void Navbar_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbNavbar>();
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
        var cut = RenderComponent<IgbNavbar>(parameters =>
            parameters.AddChildContent("Navigation Title"));

        Assert.Contains("Navigation Title", cut.Markup);
    }

    [Fact]
    public void Navbar_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbNavbar).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class NavDrawerTests : BlazorComponentTestBase
{
    [Fact]
    public void NavDrawer_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbNavDrawer>();
        Assert.NotNull(cut.Find("igc-nav-drawer"));
    }

    [Fact]
    public void NavDrawer_TypeMetadata_IsCorrect()
    {
        var drawer = new IgbNavDrawer();
        Assert.Equal("WebNavDrawer", drawer.Type);
    }

    [Fact]
    public void NavDrawer_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-nav-drawer");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void NavDrawer_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbNavDrawer).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
