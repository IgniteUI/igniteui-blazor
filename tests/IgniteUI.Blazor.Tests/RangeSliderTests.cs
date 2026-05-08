using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class RangeSliderTests : BlazorComponentTestBase
{
    [Fact]
    public void RangeSlider_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRangeSlider>();
        cut.Find("igc-range-slider").Should_Exist();
    }

    [Fact]
    public void RangeSlider_Lower_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Lower, 20));

        Assert.Equal("20", cut.Find("igc-range-slider").GetAttribute("lower"));
    }

    [Fact]
    public void RangeSlider_Upper_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Upper, 80));

        Assert.Equal("80", cut.Find("igc-range-slider").GetAttribute("upper"));
    }

    [Fact]
    public void RangeSlider_Min_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Min, 10));

        Assert.Equal("10", cut.Find("igc-range-slider").GetAttribute("min"));
    }

    [Fact]
    public void RangeSlider_Max_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Max, 200));

        Assert.Equal("200", cut.Find("igc-range-slider").GetAttribute("max"));
    }

    [Fact]
    public void RangeSlider_Step_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Step, 5));

        Assert.Equal("5", cut.Find("igc-range-slider").GetAttribute("step"));
    }

    [Fact]
    public void RangeSlider_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("disabled"));
    }

    [Fact]
    public void RangeSlider_DiscreteTrack_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.DiscreteTrack, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("discrete-track"));
    }

    [Fact]
    public void RangeSlider_HideTooltip_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.HideTooltip, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("hide-tooltip"));
    }

    [Fact]
    public void RangeSlider_PrimaryTicks_RendersAttribute()
    {
        var cut = RenderComponent<IgbRangeSlider>(p =>
            p.Add(x => x.PrimaryTicks, 5));

        Assert.Equal("5", cut.Find("igc-range-slider").GetAttribute("primary-ticks"));
    }

    [Fact]
    public void RangeSlider_InheritsFromSliderBase()
    {
        Assert.True(typeof(IgbRangeSlider).IsSubclassOf(typeof(IgbSliderBase)));
    }
}
