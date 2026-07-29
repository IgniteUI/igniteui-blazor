using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class RatingTests : ComponentWithContractTestBase<IgbRating>
{
    protected override ComponentContract<IgbRating> InteropContract { get; } = new ComponentContract<IgbRating>()
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: 3.5)
        .Method(c => c.StepUpAsync(2), c => c.StepUp(2), "stepUp", args: [2.0], types: ["Number"])
        .Method(c => c.StepDownAsync(2), c => c.StepDown(2), "stepDown", args: [2.0], types: ["Number"])
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("custom message"), c => c.SetCustomValidity("custom message"),
            "setCustomValidity", args: ["custom message"], types: ["String"])
        .Event(c => c.Change,
            argsJson: """{"detail": 4}""",
            assert: args => Assert.Equal(4, args.Detail))
        .Event(c => c.Hover,
            argsJson: """{"detail": 2}""",
            assert: args => Assert.Equal(2, args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Rating_RendersCorrectElement()
    {
        var cut = Render<IgbRating>();
        Assert.NotNull(cut.Find("igc-rating"));
    }

    [Fact]
    public void Rating_TypeMetadata_IsCorrect()
    {
        var rating = new IgbRating();
        Assert.Equal("WebRating", rating.Type);
    }

    [Fact]
    public void Rating_Max_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Max, 10.0));

        var element = cut.Find("igc-rating");
        Assert.Equal("10", element.GetAttribute("max"));
    }

    [Fact]
    public void Rating_Value_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Value, 3.5));

        var element = cut.Find("igc-rating");
        Assert.Equal("3.5", element.GetAttribute("value"));
    }

    [Fact]
    public void Rating_Step_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Step, 0.5));

        var element = cut.Find("igc-rating");
        Assert.Equal("0.5", element.GetAttribute("step"));
    }

    [Fact]
    public void Rating_Label_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Label, "Product rating"));

        var element = cut.Find("igc-rating");
        Assert.Equal("Product rating", element.GetAttribute("label"));
    }

    [Fact]
    public void Rating_ReadOnly_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Rating_Disabled_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Rating_Single_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.Single, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("single"));
    }

    [Fact]
    public void Rating_AllowReset_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.AllowReset, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("allow-reset"));
    }

    [Fact]
    public void Rating_HoverPreview_RendersAttribute()
    {
        var cut = Render<IgbRating>(parameters =>
            parameters.Add(p => p.HoverPreview, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("hover-preview"));
    }

    [Fact]
    public void Rating_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbRating).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
