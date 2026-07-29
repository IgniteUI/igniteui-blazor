using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class NavDrawerExtendedTests : BlazorComponentTestBase
{
    [Fact]
    public void NavDrawer_Position_End()
    {
        var cut = Render<IgbNavDrawer>(p =>
            p.Add(x => x.Position, NavDrawerPosition.End));

        Assert.Equal("end", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Top()
    {
        var cut = Render<IgbNavDrawer>(p =>
            p.Add(x => x.Position, NavDrawerPosition.Top));

        Assert.Equal("top", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Bottom()
    {
        var cut = Render<IgbNavDrawer>(p =>
            p.Add(x => x.Position, NavDrawerPosition.Bottom));

        Assert.Equal("bottom", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Relative()
    {
        var cut = Render<IgbNavDrawer>(p =>
            p.Add(x => x.Position, NavDrawerPosition.Relative));

        Assert.Equal("relative", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawerItem_RendersCorrectElement()
    {
        var cut = Render<IgbNavDrawerItem>();
        cut.Find("igc-nav-drawer-item").Should_Exist();
    }

    [Fact]
    public void NavDrawerItem_Disabled_RendersAttribute()
    {
        var cut = Render<IgbNavDrawerItem>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-nav-drawer-item").GetAttribute("disabled"));
    }

    [Fact]
    public void NavDrawerItem_Active_RendersAttribute()
    {
        var cut = Render<IgbNavDrawerItem>(p =>
            p.Add(x => x.Active, true));

        Assert.NotNull(cut.Find("igc-nav-drawer-item").GetAttribute("active"));
    }

    [Fact]
    public void NavDrawerItem_ChildContent_Renders()
    {
        var cut = Render<IgbNavDrawerItem>(p =>
            p.AddChildContent("<span>Home</span>"));

        Assert.Contains("Home", cut.Find("igc-nav-drawer-item").InnerHtml);
    }

    [Fact]
    public void NavDrawerHeaderItem_RendersCorrectElement()
    {
        var cut = Render<IgbNavDrawerHeaderItem>();
        cut.Find("igc-nav-drawer-header-item").Should_Exist();
    }

    [Fact]
    public void NavDrawerHeaderItem_ChildContent_Renders()
    {
        var cut = Render<IgbNavDrawerHeaderItem>(p =>
            p.AddChildContent("Navigation"));

        Assert.Contains("Navigation", cut.Find("igc-nav-drawer-header-item").InnerHtml);
    }
}
