using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SliderTests : ComponentWithContractTestBase<IgbSlider>
{
    protected override ComponentContract<IgbSlider> InteropContract { get; } = new ComponentContract<IgbSlider>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: 42.0)
        .Method(c => c.StepUpAsync(2), c => c.StepUp(2), "stepUp", args: [2.0], types: ["Number"])
        .Method(c => c.StepDownAsync(2), c => c.StepDown(2), "stepDown", args: [2.0], types: ["Number"])
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Input,
            argsJson: """{"detail": 3}""",
            assert: args => Assert.Equal(3, args.Detail))
        .Event(c => c.Change,
            argsJson: """{"detail": 5}""",
            assert: args => Assert.Equal(5, args.Detail))
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            argsJson: """{"detail": 5}""", expect: 5.0)
        .Prop(c => c.Value, 50.0)
        // The description's "name" is the renderer's id for the component, so Name crosses as "formName".
        .Prop(c => c.Name, "field", wire: "field", wireName: "formName")
        .Prop(c => c.Invalid, true)
        .Prop(c => c.Min, 10.0)
        .Prop(c => c.Max, 200.0)
        .Prop(c => c.LowerBound, 20.0)
        .Prop(c => c.UpperBound, 80.0)
        .Prop(c => c.Step, 5.0)
        .Prop(c => c.Disabled, true)
        .Prop(c => c.DiscreteTrack, true)
        .Prop(c => c.HideTooltip, true)
        .Prop(c => c.PrimaryTicks, 5.0)
        .Prop(c => c.SecondaryTicks, 3.0)
        .Prop(c => c.HidePrimaryLabels, true)
        .Prop(c => c.HideSecondaryLabels, true)
        .Prop(c => c.TickOrientation, SliderTickOrientation.Start, wire: "start")
        .Prop(c => c.TickOrientation, SliderTickOrientation.End, wire: "end")
        .Prop(c => c.TickOrientation, SliderTickOrientation.Mirror, wire: "mirror")
        .Prop(c => c.TickLabelRotation, SliderTickLabelRotation.Ninety, wire: "90")
        .Prop(c => c.Locale, "en-US")
        .Prop(c => c.ValueFormat, "{0} pts")
        .Prop(c => c.ValueFormatOptions,
            new IgbNumberFormatOptions
            {
                Style = "percent",
                MaximumFractionDigits = 1,
            },
            wire: new JsonSubset("""{"style": "percent", "maximumFractionDigits": 1}"""));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Slider_RendersCorrectElement()
    {
        var cut = Render<IgbSlider>();
        Assert.NotNull(cut.Find("igc-slider"));
    }

    /// <summary>
    /// Without it, the labels would render outside the slider element, which reads them from its own children.
    /// </summary>
    [Fact]
    public void Slider_Labels_RenderInsideTheElement()
    {
        var cut = Render<IgbSlider>(p => p.AddChildContent<IgbSliderLabel>(l => l.AddChildContent("Low")));

        Assert.Contains("Low", cut.Find("igc-slider > igc-slider-label").TextContent);
    }

    [Fact]
    public void Slider_TypeMetadata_IsCorrect()
    {
        var slider = new IgbSlider();
        Assert.Equal("WebSlider", slider.RendererType);
    }

    [Fact]
    public void Slider_InheritsFromSliderBase()
    {
        Assert.True(typeof(IgbSlider).IsSubclassOf(typeof(IgbSliderBase)));
    }
}

public class SliderLabelTests : BlazorComponentTestBase
{
    [Fact]
    public void SliderLabel_RendersCorrectElement()
    {
        var cut = Render<IgbSliderLabel>();
        cut.Find("igc-slider-label").Should_Exist();
    }

    [Fact]
    public void SliderLabel_ChildContent_Renders()
    {
        var cut = Render<IgbSliderLabel>(parameters =>
            parameters.AddChildContent("Low"));

        Assert.Contains("Low", cut.Find("igc-slider-label").InnerHtml);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbSlider</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Slider_DefaultValues_MatchWebComponent()
    {
        var slider = new IgbSlider();

        Assert.Equal(100, slider.Max);
        Assert.Equal(1, slider.Step);
    }
}
