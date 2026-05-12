using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests for Slider and SliderBase properties to increase coverage.
/// </summary>
public class SliderExtendedTests : BlazorComponentTestBase
{
    [Fact]
    public void Slider_LowerBound_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.LowerBound, 10));

        Assert.Equal("10", cut.Find("igc-slider").GetAttribute("lower-bound"));
    }

    [Fact]
    public void Slider_UpperBound_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.UpperBound, 90));

        Assert.Equal("90", cut.Find("igc-slider").GetAttribute("upper-bound"));
    }

    [Fact]
    public void Slider_HideTooltip_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.HideTooltip, true));

        Assert.NotNull(cut.Find("igc-slider").GetAttribute("hide-tooltip"));
    }

    [Fact]
    public void Slider_PrimaryTicks_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.PrimaryTicks, 5));

        Assert.Equal("5", cut.Find("igc-slider").GetAttribute("primary-ticks"));
    }

    [Fact]
    public void Slider_SecondaryTicks_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.SecondaryTicks, 3));

        Assert.Equal("3", cut.Find("igc-slider").GetAttribute("secondary-ticks"));
    }

    [Fact]
    public void Slider_HidePrimaryLabels_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.HidePrimaryLabels, true));

        Assert.NotNull(cut.Find("igc-slider").GetAttribute("hide-primary-labels"));
    }

    [Fact]
    public void Slider_HideSecondaryLabels_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.HideSecondaryLabels, true));

        Assert.NotNull(cut.Find("igc-slider").GetAttribute("hide-secondary-labels"));
    }

    [Fact]
    public void Slider_TickOrientation_Mirror()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.TickOrientation, SliderTickOrientation.Mirror));

        Assert.Equal("mirror", cut.Find("igc-slider").GetAttribute("tick-orientation"));
    }

    [Fact]
    public void Slider_Locale_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.Locale, "en-US"));

        Assert.Equal("en-US", cut.Find("igc-slider").GetAttribute("locale"));
    }

    [Fact]
    public void Slider_ValueFormat_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.ValueFormat, "{0}%"));

        Assert.Equal("{0}%", cut.Find("igc-slider").GetAttribute("value-format"));
    }

    [Fact]
    public void Slider_Invalid_RendersAttribute()
    {
        var cut = RenderComponent<IgbSlider>(p =>
            p.Add(x => x.Invalid, true));

        Assert.NotNull(cut.Find("igc-slider").GetAttribute("invalid"));
    }

    [Fact]
    public void SliderLabel_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbSliderLabel>();
        cut.Find("igc-slider-label").Should_Exist();
    }

    [Fact]
    public void SliderLabel_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbSliderLabel>(p =>
            p.AddChildContent("Low"));

        Assert.Contains("Low", cut.Find("igc-slider-label").InnerHtml);
    }

    [Fact]
    public void RatingSymbol_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRatingSymbol>();
        cut.Find("igc-rating-symbol").Should_Exist();
    }
}
