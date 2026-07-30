using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

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
        var cut = Render<IgbNavDrawer>();
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
        var cut = Render<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-nav-drawer");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void NavDrawer_Position_End()
    {
        var cut = Render<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Position, NavDrawerPosition.End));

        Assert.Equal("end", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Top()
    {
        var cut = Render<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Position, NavDrawerPosition.Top));

        Assert.Equal("top", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Bottom()
    {
        var cut = Render<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Position, NavDrawerPosition.Bottom));

        Assert.Equal("bottom", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_Position_Relative()
    {
        var cut = Render<IgbNavDrawer>(parameters =>
            parameters.Add(p => p.Position, NavDrawerPosition.Relative));

        Assert.Equal("relative", cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    [Fact]
    public void NavDrawer_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbNavDrawer).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class NavDrawerItemTests : BlazorComponentTestBase
{
    [Fact]
    public void NavDrawerItem_RendersCorrectElement()
    {
        var cut = Render<IgbNavDrawerItem>();
        cut.Find("igc-nav-drawer-item").Should_Exist();
    }

    [Fact]
    public void NavDrawerItem_Disabled_RendersAttribute()
    {
        var cut = Render<IgbNavDrawerItem>(parameters =>
            parameters.Add(p => p.Disabled, true));

        Assert.NotNull(cut.Find("igc-nav-drawer-item").GetAttribute("disabled"));
    }

    [Fact]
    public void NavDrawerItem_Active_RendersAttribute()
    {
        var cut = Render<IgbNavDrawerItem>(parameters =>
            parameters.Add(p => p.Active, true));

        Assert.NotNull(cut.Find("igc-nav-drawer-item").GetAttribute("active"));
    }

    [Fact]
    public void NavDrawerItem_ChildContent_Renders()
    {
        var cut = Render<IgbNavDrawerItem>(parameters =>
            parameters.AddChildContent("<span>Home</span>"));

        Assert.Contains("Home", cut.Find("igc-nav-drawer-item").InnerHtml);
    }
}

public class NavDrawerHeaderItemTests : BlazorComponentTestBase
{
    [Fact]
    public void NavDrawerHeaderItem_RendersCorrectElement()
    {
        var cut = Render<IgbNavDrawerHeaderItem>();
        cut.Find("igc-nav-drawer-header-item").Should_Exist();
    }

    [Fact]
    public void NavDrawerHeaderItem_ChildContent_Renders()
    {
        var cut = Render<IgbNavDrawerHeaderItem>(parameters =>
            parameters.AddChildContent("Navigation"));

        Assert.Contains("Navigation", cut.Find("igc-nav-drawer-header-item").InnerHtml);
    }
}
