using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class ExpansionPanelTests : ComponentWithContractTestBase<IgbExpansionPanel>
{
    // The panel's own events carry a self-reference detail ({"refType": "name",
    // "id": "mainControl"}) that must resolve back to the .NET instance via FindByName.
    protected override ComponentContract<IgbExpansionPanel> InteropContract { get; } = new ComponentContract<IgbExpansionPanel>()
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Event(c => c.Opening,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (panel, args) => Assert.Same(panel, args.Detail))
        .Event(c => c.Opened,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (panel, args) => Assert.Same(panel, args.Detail))
        .Event(c => c.Closing,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (panel, args) => Assert.Same(panel, args.Detail))
        .Event(c => c.Closed,
            """{"detail": {"refType": "name", "id": "mainControl"}}""",
            assert: (panel, args) => Assert.Same(panel, args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void ExpansionPanel_RendersCorrectElement()
    {
        var cut = Render<IgbExpansionPanel>();
        Assert.NotNull(cut.Find("igc-expansion-panel"));
    }

    [Fact]
    public void ExpansionPanel_TypeMetadata_IsCorrect()
    {
        var panel = new IgbExpansionPanel();
        Assert.Equal("WebExpansionPanel", panel.Type);
    }

    [Fact]
    public void ExpansionPanel_Open_RendersAttribute()
    {
        var cut = Render<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-expansion-panel");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void ExpansionPanel_Disabled_RendersAttribute()
    {
        var cut = Render<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-expansion-panel");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void ExpansionPanel_IndicatorPosition_End()
    {
        var cut = Render<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.IndicatorPosition, ExpansionPanelIndicatorPosition.End));

        var element = cut.Find("igc-expansion-panel");
        Assert.Equal("end", element.GetAttribute("indicator-position"));
    }

    [Fact]
    public void ExpansionPanel_ChildContent_Renders()
    {
        var cut = Render<IgbExpansionPanel>(parameters =>
            parameters.AddChildContent("Panel content"));

        Assert.Contains("Panel content", cut.Markup);
    }

    [Fact]
    public void ExpansionPanel_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbExpansionPanel).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
