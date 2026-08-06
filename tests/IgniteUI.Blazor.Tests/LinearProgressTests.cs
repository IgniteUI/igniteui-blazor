using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class LinearProgressTests : BlazorComponentTestBase
{
    [Fact]
    public void LinearProgress_RendersCorrectElement()
    {
        var cut = Render<IgbLinearProgress>();
        Assert.NotNull(cut.Find("igc-linear-progress"));
    }

    [Fact]
    public void LinearProgress_TypeMetadata_IsCorrect()
    {
        var progress = new IgbLinearProgress();
        Assert.Equal("WebLinearProgress", progress.Type);
    }

    [Fact]
    public void LinearProgress_InheritsFromProgressBase()
    {
        Assert.True(typeof(IgbLinearProgress).IsSubclassOf(typeof(IgbProgressBase)));
    }

    [Fact]
    public void LinearProgress_Value_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.Value, 75.0));

        var element = cut.Find("igc-linear-progress");
        Assert.Equal("75", element.GetAttribute("value"));
    }

    [Fact]
    public void LinearProgress_Max_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.Max, 200.0));

        var element = cut.Find("igc-linear-progress");
        Assert.Equal("200", element.GetAttribute("max"));
    }

    [Fact]
    public void LinearProgress_Striped_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.Striped, true));

        var element = cut.Find("igc-linear-progress");
        Assert.NotNull(element.GetAttribute("striped"));
    }

    [Fact]
    public void LinearProgress_Indeterminate_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.Indeterminate, true));

        var element = cut.Find("igc-linear-progress");
        Assert.NotNull(element.GetAttribute("indeterminate"));
    }

    [Fact]
    public void LinearProgress_HideLabel_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.HideLabel, true));

        var element = cut.Find("igc-linear-progress");
        Assert.NotNull(element.GetAttribute("hide-label"));
    }

    [Fact]
    public void LinearProgress_Variant_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.Variant, StyleVariant.Success));

        var element = cut.Find("igc-linear-progress");
        Assert.Equal("success", element.GetAttribute("variant"));
    }

    [Fact]
    public void LinearProgress_LabelFormat_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.LabelFormat, "{0}%"));

        var element = cut.Find("igc-linear-progress");
        Assert.Equal("{0}%", element.GetAttribute("label-format"));
    }

    [Fact]
    public void LinearProgress_AnimationDuration_RendersAttribute()
    {
        var cut = Render<IgbLinearProgress>(parameters =>
            parameters.Add(p => p.AnimationDuration, 500));

        var element = cut.Find("igc-linear-progress");
        Assert.Equal("500", element.GetAttribute("animation-duration"));
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbLinearProgress</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void LinearProgress_DefaultValues_MatchWebComponent()
    {
        var progress = new IgbLinearProgress();

        Assert.Equal(100, progress.Max);
        Assert.Equal(500, progress.AnimationDuration);
    }
}
