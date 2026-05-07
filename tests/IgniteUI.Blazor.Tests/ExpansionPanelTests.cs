using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ExpansionPanelTests : BlazorComponentTestBase
{
    [Fact]
    public void ExpansionPanel_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbExpansionPanel>();
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
        var cut = RenderComponent<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-expansion-panel");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void ExpansionPanel_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-expansion-panel");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void ExpansionPanel_IndicatorPosition_End()
    {
        var cut = RenderComponent<IgbExpansionPanel>(parameters =>
            parameters.Add(p => p.IndicatorPosition, ExpansionPanelIndicatorPosition.End));

        var element = cut.Find("igc-expansion-panel");
        Assert.Equal("end", element.GetAttribute("indicator-position"));
    }

    [Fact]
    public void ExpansionPanel_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbExpansionPanel>(parameters =>
            parameters.AddChildContent("Panel content"));

        Assert.Contains("Panel content", cut.Markup);
    }

    [Fact]
    public void ExpansionPanel_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbExpansionPanel).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
