using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class TooltipTests : BlazorComponentTestBase
{
    [Fact]
    public void Tooltip_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTooltip>();
        cut.Find("igc-tooltip").Should_Exist();
    }

    [Fact]
    public void Tooltip_Open_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.Open, true));

        Assert.NotNull(cut.Find("igc-tooltip").GetAttribute("open"));
    }

    [Fact]
    public void Tooltip_WithArrow_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.WithArrow, true));

        Assert.NotNull(cut.Find("igc-tooltip").GetAttribute("with-arrow"));
    }

    [Fact]
    public void Tooltip_Offset_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.Offset, 10));

        Assert.Equal("10", cut.Find("igc-tooltip").GetAttribute("offset"));
    }

    [Fact]
    public void Tooltip_Anchor_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.Anchor, "my-button"));

        Assert.Equal("my-button", cut.Find("igc-tooltip").GetAttribute("anchor"));
    }

    [Fact]
    public void Tooltip_ShowDelay_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.ShowDelay, 500));

        Assert.Equal("500", cut.Find("igc-tooltip").GetAttribute("show-delay"));
    }

    [Fact]
    public void Tooltip_HideDelay_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.HideDelay, 300));

        Assert.Equal("300", cut.Find("igc-tooltip").GetAttribute("hide-delay"));
    }

    [Fact]
    public void Tooltip_Message_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.Message, "Tooltip text"));

        Assert.Equal("Tooltip text", cut.Find("igc-tooltip").GetAttribute("message"));
    }

    [Fact]
    public void Tooltip_Sticky_RendersAttribute()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.Add(x => x.Sticky, true));

        Assert.NotNull(cut.Find("igc-tooltip").GetAttribute("sticky"));
    }

    [Fact]
    public void Tooltip_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTooltip>(p =>
            p.AddChildContent("Custom tooltip content"));

        Assert.Contains("Custom tooltip content", cut.Find("igc-tooltip").InnerHtml);
    }
}
