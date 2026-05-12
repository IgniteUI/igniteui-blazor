using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class AccordionTests : BlazorComponentTestBase
{
    [Fact]
    public void Accordion_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbAccordion>();
        Assert.NotNull(cut.Find("igc-accordion"));
    }

    [Fact]
    public void Accordion_TypeMetadata_IsCorrect()
    {
        var accordion = new IgbAccordion();
        Assert.Equal("WebAccordion", accordion.Type);
    }

    [Fact]
    public void Accordion_SingleExpand_RendersAttribute()
    {
        var cut = RenderComponent<IgbAccordion>(parameters =>
            parameters.Add(p => p.SingleExpand, true));

        var element = cut.Find("igc-accordion");
        Assert.NotNull(element.GetAttribute("single-expand"));
    }

    [Fact]
    public void Accordion_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbAccordion>(parameters =>
            parameters.AddChildContent("Accordion content"));

        Assert.Contains("Accordion content", cut.Markup);
    }

    [Fact]
    public void Accordion_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbAccordion).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class BannerTests : BlazorComponentTestBase
{
    [Fact]
    public void Banner_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbBanner>();
        Assert.NotNull(cut.Find("igc-banner"));
    }

    [Fact]
    public void Banner_TypeMetadata_IsCorrect()
    {
        var banner = new IgbBanner();
        Assert.Equal("WebBanner", banner.Type);
    }

    [Fact]
    public void Banner_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbBanner>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-banner");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Banner_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBanner).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class DividerTests : BlazorComponentTestBase
{
    [Fact]
    public void Divider_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbDivider>();
        Assert.NotNull(cut.Find("igc-divider"));
    }

    [Fact]
    public void Divider_TypeMetadata_IsCorrect()
    {
        var divider = new IgbDivider();
        Assert.Equal("WebDivider", divider.Type);
    }

    [Fact]
    public void Divider_Type_Dashed()
    {
        var cut = RenderComponent<IgbDivider>(parameters =>
            parameters.Add(p => p.LineType, DividerType.Dashed));

        var element = cut.Find("igc-divider");
        Assert.Equal("dashed", element.GetAttribute("type"));
    }

    [Fact]
    public void Divider_Middle_RendersAttribute()
    {
        var cut = RenderComponent<IgbDivider>(parameters =>
            parameters.Add(p => p.Middle, true));

        var element = cut.Find("igc-divider");
        Assert.NotNull(element.GetAttribute("middle"));
    }

    [Fact]
    public void Divider_Vertical_RendersAttribute()
    {
        var cut = RenderComponent<IgbDivider>(parameters =>
            parameters.Add(p => p.Vertical, true));

        var element = cut.Find("igc-divider");
        Assert.NotNull(element.GetAttribute("vertical"));
    }
}

public class RippleTests : BlazorComponentTestBase
{
    [Fact]
    public void Ripple_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRipple>();
        Assert.NotNull(cut.Find("igc-ripple"));
    }

    [Fact]
    public void Ripple_TypeMetadata_IsCorrect()
    {
        var ripple = new IgbRipple();
        Assert.Equal("WebRipple", ripple.Type);
    }
}
