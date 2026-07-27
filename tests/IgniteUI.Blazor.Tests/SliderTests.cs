using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SliderTests : ComponentWithContractTestBase<IgbSlider>
{
    // TODO: ValueFormatOptions/ValueFormat — Slider is direct-render BUG 35189
    protected override ComponentContract<IgbSlider> InteropContract { get; } = new ComponentContract<IgbSlider>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: 42.0)
        .Method(c => c.StepUpAsync(2), c => c.StepUp(2), "stepUp", args: [2.0], types: ["Number"])
        .Method(c => c.StepDownAsync(2), c => c.StepDown(2), "stepDown", args: [2.0], types: ["Number"])
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity")
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity")
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Input,
            argsJson: """{"detail": 3}""",
            assert: args => Assert.Equal(3, args.Detail))
        .Event(c => c.Change,
            argsJson: """{"detail": 5}""",
            assert: args => Assert.Equal(5, args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

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
