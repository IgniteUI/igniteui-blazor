using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class RangeSliderTests : ComponentWithContractTestBase<IgbRangeSlider>
{
    protected override ComponentContract<IgbRangeSlider> InteropContract { get; } = new ComponentContract<IgbRangeSlider>()
        .Event(c => c.Input,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"lower": 20, "upper": 80}}}""",
            assert: args =>
            {
                Assert.Equal(20, args.Detail.Lower);
                Assert.Equal(80, args.Detail.Upper);
            })
        .Event(c => c.Change,
            argsJson: """{"detail": {"retType": "object", "type": "", "value": {"lower": 25, "upper": 75}}}""",
            assert: args =>
            {
                Assert.Equal(25, args.Detail.Lower);
                Assert.Equal(75, args.Detail.Upper);
            })
        .Prop(c => c.Lower, 20.0)
        .Prop(c => c.Upper, 80.0)
        .Prop(c => c.Min, 10.0)
        .Prop(c => c.Max, 200.0)
        .Prop(c => c.Step, 5.0)
        .Prop(c => c.Disabled, true)
        .Prop(c => c.DiscreteTrack, true)
        .Prop(c => c.HideTooltip, true)
        .Prop(c => c.PrimaryTicks, 5.0)
        .Prop(c => c.ValueFormatOptions,
            new IgbNumberFormatOptions
            {
                Style = "currency",
                Currency = "EUR",
            },
            wire: new JsonSubset("""{"style": "currency", "currency": "EUR"}"""));

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void RangeSlider_RendersCorrectElement()
    {
        var cut = Render<IgbRangeSlider>();
        cut.Find("igc-range-slider").Should_Exist();
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
