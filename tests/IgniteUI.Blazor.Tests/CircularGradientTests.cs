using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CircularGradientTests : BlazorComponentTestBase
{
    [Fact]
    public void CircularGradient_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCircularGradient>();
        cut.Find("igc-circular-gradient").Should_Exist();
    }

    [Fact]
    public void CircularGradient_Offset_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularGradient>(p =>
            p.Add(x => x.Offset, "0%"));

        Assert.Equal("0%", cut.Find("igc-circular-gradient").GetAttribute("offset"));
    }

    [Fact]
    public void CircularGradient_Color_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularGradient>(p =>
            p.Add(x => x.Color, "#ff0000"));

        Assert.Equal("#ff0000", cut.Find("igc-circular-gradient").GetAttribute("color"));
    }

    [Fact]
    public void CircularGradient_Opacity_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularGradient>(p =>
            p.Add(x => x.Opacity, 0.5));

        Assert.Equal("0.5", cut.Find("igc-circular-gradient").GetAttribute("opacity"));
    }

    [Fact]
    public void CircularGradient_AllProperties()
    {
        var cut = RenderComponent<IgbCircularGradient>(p =>
            p.Add(x => x.Offset, "50%")
             .Add(x => x.Color, "blue")
             .Add(x => x.Opacity, 1));

        var el = cut.Find("igc-circular-gradient");
        Assert.Equal("50%", el.GetAttribute("offset"));
        Assert.Equal("blue", el.GetAttribute("color"));
        Assert.Equal("1", el.GetAttribute("opacity"));
    }
}
