using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

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

public class NavDrawerTests : ComponentWithContractTestBase<IgbNavDrawer>
{
    protected override ComponentContract<IgbNavDrawer> InteropContract { get; } = new ComponentContract<IgbNavDrawer>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Event(c => c.Closing)
        .Event(c => c.Closed);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

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
