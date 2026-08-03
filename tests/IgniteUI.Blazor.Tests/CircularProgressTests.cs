using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CircularProgressTests : BlazorComponentTestBase
{
    [Fact]
    public void CircularProgress_RendersCorrectElement()
    {
        var cut = Render<IgbCircularProgress>();
        Assert.NotNull(cut.Find("igc-circular-progress"));
    }

    [Fact]
    public void CircularProgress_TypeMetadata_IsCorrect()
    {
        var progress = new IgbCircularProgress();
        Assert.Equal("WebCircularProgress", progress.Type);
    }

    [Fact]
    public void CircularProgress_InheritsFromProgressBase()
    {
        Assert.True(typeof(IgbCircularProgress).IsSubclassOf(typeof(IgbProgressBase)));
    }

    [Fact]
    public void CircularProgress_Value_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.Value, 60.0));

        var element = cut.Find("igc-circular-progress");
        Assert.Equal("60", element.GetAttribute("value"));
    }

    [Fact]
    public void CircularProgress_Indeterminate_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.Indeterminate, true));

        var element = cut.Find("igc-circular-progress");
        Assert.NotNull(element.GetAttribute("indeterminate"));
    }

    [Fact]
    public void CircularProgress_Max_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.Max, 150.0));

        var element = cut.Find("igc-circular-progress");
        Assert.Equal("150", element.GetAttribute("max"));
    }

    [Fact]
    public void CircularProgress_HideLabel_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.HideLabel, true));

        var element = cut.Find("igc-circular-progress");
        Assert.NotNull(element.GetAttribute("hide-label"));
    }

    [Fact]
    public void CircularProgress_LabelFormat_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.LabelFormat, "{0} of {1}"));

        var element = cut.Find("igc-circular-progress");
        Assert.Equal("{0} of {1}", element.GetAttribute("label-format"));
    }

    [Fact]
    public void CircularProgress_AnimationDuration_RendersAttribute()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.Add(p => p.AnimationDuration, 1000));

        var element = cut.Find("igc-circular-progress");
        Assert.Equal("1000", element.GetAttribute("animation-duration"));
    }

    [Fact]
    public void CircularProgress_ChildContent_Renders()
    {
        var cut = Render<IgbCircularProgress>(parameters =>
            parameters.AddChildContent("<igc-circular-gradient offset=\"0%\" color=\"blue\"></igc-circular-gradient>"));

        Assert.Contains("igc-circular-gradient", cut.Find("igc-circular-progress").InnerHtml);
    }
}
