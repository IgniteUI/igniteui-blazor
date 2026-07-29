using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class TabsTests : ComponentWithContractTestBase<IgbTabs>
{
    protected override ComponentContract<IgbTabs> InteropContract { get; } = new ComponentContract<IgbTabs>()
        .Event(c => c.Change,
            arrange: ps => ps.AddChildContent(builder =>
            {
                builder.OpenComponent<IgbTab>(0);
                builder.AddAttribute(1, "id", "tab-1");
                builder.CloseComponent();
                builder.OpenComponent<IgbTab>(2);
                builder.AddAttribute(3, "id", "tab-2");
                builder.CloseComponent();
            }),
            argsJson: (interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tab:nth-of-type(2)")}}}"}}""",
            assert: (cut, args) =>
            {
                Assert.Same(cut.Instance.ActualTabsCollection[1], args.Detail);
                Assert.True(args.Detail.Selected); // OnHandlingChange propagates selection
            })
        .Method(c => c.SelectAsync("tab-1"), c => c.Select("tab-1"), "select", args: ["tab-1"], types: ["String"])
        .Getter(c => c.GetSelectedAsync(), c => c.GetSelected(), "Selected", returns: "tab-1");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Tabs_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTabs>();
        Assert.NotNull(cut.Find("igc-tabs"));
    }

    [Fact]
    public void Tabs_TypeMetadata_IsCorrect()
    {
        var tabs = new IgbTabs();
        Assert.Equal("WebTabs", tabs.Type);
    }

    [Fact]
    public void Tabs_Alignment_RendersAttribute()
    {
        var cut = RenderComponent<IgbTabs>(parameters =>
            parameters.Add(p => p.Alignment, TabsAlignment.Center));

        var element = cut.Find("igc-tabs");
        Assert.Equal("center", element.GetAttribute("alignment"));
    }

    [Fact]
    public void Tabs_Activation_RendersAttribute()
    {
        var cut = RenderComponent<IgbTabs>(parameters =>
            parameters.Add(p => p.Activation, TabsActivation.Manual));

        var element = cut.Find("igc-tabs");
        Assert.Equal("manual", element.GetAttribute("activation"));
    }

    [Fact]
    public void Tabs_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbTabs).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class TabTests : BlazorComponentTestBase
{
    [Fact]
    public void Tab_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTab>();
        Assert.NotNull(cut.Find("igc-tab"));
    }

    [Fact]
    public void Tab_TypeMetadata_IsCorrect()
    {
        var tab = new IgbTab();
        Assert.Equal("WebTab", tab.Type);
    }

    [Fact]
    public void Tab_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Tab_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void Tab_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.AddChildContent("Tab Label"));

        Assert.Contains("Tab Label", cut.Markup);
    }
}
