using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class RatingTests : BlazorComponentTestBase
{
    [Fact]
    public void Rating_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbRating>();
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
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Max, 10.0));

        var element = cut.Find("igc-rating");
        Assert.Equal("10", element.GetAttribute("max"));
    }

    [Fact]
    public void Rating_Value_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Value, 3.5));

        var element = cut.Find("igc-rating");
        Assert.Equal("3.5", element.GetAttribute("value"));
    }

    [Fact]
    public void Rating_Step_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Step, 0.5));

        var element = cut.Find("igc-rating");
        Assert.Equal("0.5", element.GetAttribute("step"));
    }

    [Fact]
    public void Rating_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Label, "Product rating"));

        var element = cut.Find("igc-rating");
        Assert.Equal("Product rating", element.GetAttribute("label"));
    }

    [Fact]
    public void Rating_ReadOnly_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.ReadOnly, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("readonly"));
    }

    [Fact]
    public void Rating_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Rating_Single_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.Single, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("single"));
    }

    [Fact]
    public void Rating_AllowReset_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
            parameters.Add(p => p.AllowReset, true));

        var element = cut.Find("igc-rating");
        Assert.NotNull(element.GetAttribute("allow-reset"));
    }

    [Fact]
    public void Rating_HoverPreview_RendersAttribute()
    {
        var cut = RenderComponent<IgbRating>(parameters =>
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
