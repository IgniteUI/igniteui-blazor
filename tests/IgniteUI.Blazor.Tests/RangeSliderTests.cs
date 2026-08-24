using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class RangeSliderTests : ComponentWithContractTestBase<IgbRangeSlider>
{
    // TODO: ValueFormatOptions/ValueFormat (config objects on a direct-render component —
    // they never cross as interop messages; BUG 35189 ).
    protected override ComponentContract<IgbRangeSlider> InteropContract { get; } = new ComponentContract<IgbRangeSlider>()
        .Event(c => c.Input,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"lower": 20, "upper": 80}}}""",
            assert: args =>
            {
                Assert.Equal(20, args!.Detail!.Lower);
                Assert.Equal(80, args.Detail.Upper);
            })
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"lower": 25, "upper": 75}}}""",
            assert: args =>
            {
                Assert.Equal(25, args!.Detail!.Lower);
                Assert.Equal(75, args.Detail.Upper);
            });

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void RangeSlider_RendersCorrectElement()
    {
        var cut = Render<IgbRangeSlider>();
        cut.Find("igc-range-slider").Should_Exist();
    }

    [Fact]
    public void RangeSlider_Lower_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Lower, 20));

        Assert.Equal("20", cut.Find("igc-range-slider").GetAttribute("lower"));
    }

    [Fact]
    public void RangeSlider_Upper_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Upper, 80));

        Assert.Equal("80", cut.Find("igc-range-slider").GetAttribute("upper"));
    }

    [Fact]
    public void RangeSlider_Min_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Min, 10));

        Assert.Equal("10", cut.Find("igc-range-slider").GetAttribute("min"));
    }

    [Fact]
    public void RangeSlider_Max_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Max, 200));

        Assert.Equal("200", cut.Find("igc-range-slider").GetAttribute("max"));
    }

    [Fact]
    public void RangeSlider_Step_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Step, 5));

        Assert.Equal("5", cut.Find("igc-range-slider").GetAttribute("step"));
    }

    [Fact]
    public void RangeSlider_Disabled_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("disabled"));
    }

    [Fact]
    public void RangeSlider_DiscreteTrack_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.DiscreteTrack, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("discrete-track"));
    }

    [Fact]
    public void RangeSlider_HideTooltip_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.HideTooltip, true));

        Assert.NotNull(cut.Find("igc-range-slider").GetAttribute("hide-tooltip"));
    }

    [Fact]
    public void RangeSlider_PrimaryTicks_RendersAttribute()
    {
        var cut = Render<IgbRangeSlider>(p =>
            p.Add(x => x.PrimaryTicks, 5));

        Assert.Equal("5", cut.Find("igc-range-slider").GetAttribute("primary-ticks"));
    }

    [Fact]
    public void RangeSlider_InheritsFromSliderBase()
    {
        Assert.True(typeof(IgbRangeSlider).IsSubclassOf(typeof(IgbSliderBase)));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbRangeSlider</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void RangeSlider_DefaultValues_MatchWebComponent()
    {
        var slider = new IgbRangeSlider();

        Assert.Equal(100, slider.Max);
        Assert.Equal(1, slider.Step);
    }
}
