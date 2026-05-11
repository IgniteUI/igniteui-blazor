using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class SliderTests : BlazorComponentTestBase
{
    [Fact]
    public void Slider_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSlider>();
        Assert.NotNull(cut.Find("igc-slider"));
    }

    [Fact]
    public void Slider_TypeMetadata_IsCorrect()
    {
        var slider = new IgbSlider();
        Assert.Equal("WebSlider", slider.Type);
    }

    [Fact]
    public void Slider_InheritsFromSliderBase()
    {
        Assert.True(typeof(IgbSlider).IsSubclassOf(typeof(IgbSliderBase)));
    }

    [Fact]
    public void Slider_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.Value, 50.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("50", element.GetAttribute("value"));
    }

    [Fact]
    public void Slider_Min_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.Min, 10.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("10", element.GetAttribute("min"));
    }

    [Fact]
    public void Slider_Max_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.Max, 100.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("100", element.GetAttribute("max"));
    }

    [Fact]
    public void Slider_Step_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.Step, 5.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("5", element.GetAttribute("step"));
    }

    [Fact]
    public void Slider_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-slider");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Slider_DiscreteTrack_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.DiscreteTrack, true));

        var element = cut.Find("igc-slider");
        Assert.NotNull(element.GetAttribute("discrete-track"));
    }

    [Fact]
    public void Slider_HideTooltip_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.HideTooltip, true));

        var element = cut.Find("igc-slider");
        Assert.NotNull(element.GetAttribute("hide-tooltip"));
    }

    [Fact]
    public void Slider_LowerBound_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.LowerBound, 20.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("20", element.GetAttribute("lower-bound"));
    }

    [Fact]
    public void Slider_UpperBound_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(parameters =>
            parameters.Add(p => p.UpperBound, 80.0));

        var element = cut.Find("igc-slider");
        Assert.Equal("80", element.GetAttribute("upper-bound"));
    }
}
