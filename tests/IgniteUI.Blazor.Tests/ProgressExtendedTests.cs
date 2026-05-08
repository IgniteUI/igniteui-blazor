using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Extended tests for Progress, Rating, List, Tabs, Card components.
/// </summary>
public class ProgressExtendedTests : BlazorComponentTestBase
{
    // LinearProgress extended
    [Fact]
    public void LinearProgress_HideLabel_RendersAttribute()
    {
        var cut = RenderComponent<IgbLinearProgress>(p =>
            p.Add(x => x.HideLabel, true));

        Assert.NotNull(cut.Find("igc-linear-progress").GetAttribute("hide-label"));
    }

    [Fact]
    public void LinearProgress_LabelFormat_RendersAttribute()
    {
        var cut = RenderComponent<IgbLinearProgress>(p =>
            p.Add(x => x.LabelFormat, "{0}%"));

        Assert.Equal("{0}%", cut.Find("igc-linear-progress").GetAttribute("label-format"));
    }

    [Fact]
    public void LinearProgress_AnimationDuration_RendersAttribute()
    {
        var cut = RenderComponent<IgbLinearProgress>(p =>
            p.Add(x => x.AnimationDuration, 500));

        Assert.Equal("500", cut.Find("igc-linear-progress").GetAttribute("animation-duration"));
    }

    // CircularProgress extended
    [Fact]
    public void CircularProgress_HideLabel_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularProgress>(p =>
            p.Add(x => x.HideLabel, true));

        Assert.NotNull(cut.Find("igc-circular-progress").GetAttribute("hide-label"));
    }

    [Fact]
    public void CircularProgress_LabelFormat_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularProgress>(p =>
            p.Add(x => x.LabelFormat, "{0} of {1}"));

        Assert.Equal("{0} of {1}", cut.Find("igc-circular-progress").GetAttribute("label-format"));
    }

    [Fact]
    public void CircularProgress_AnimationDuration_RendersAttribute()
    {
        var cut = RenderComponent<IgbCircularProgress>(p =>
            p.Add(x => x.AnimationDuration, 1000));

        Assert.Equal("1000", cut.Find("igc-circular-progress").GetAttribute("animation-duration"));
    }

    [Fact]
    public void CircularProgress_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCircularProgress>(p =>
            p.AddChildContent("<igc-circular-gradient offset=\"0%\" color=\"blue\"></igc-circular-gradient>"));

        Assert.Contains("igc-circular-gradient", cut.Find("igc-circular-progress").InnerHtml);
    }

    // Rating extended
    [Fact]
    public void Rating_ChildContent_RatingSymbol()
    {
        var cut = RenderComponent<IgbRating>(p =>
            p.AddChildContent("<igc-rating-symbol></igc-rating-symbol>"));

        Assert.Contains("igc-rating-symbol", cut.Find("igc-rating").InnerHtml);
    }

    // List extended
    [Fact]
    public void List_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbList>(p =>
            p.AddChildContent("<igc-list-item>First</igc-list-item>"));

        Assert.Contains("First", cut.Find("igc-list").InnerHtml);
    }

    [Fact]
    public void ListItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbListItem>(p =>
            p.AddChildContent("List content"));

        Assert.Contains("List content", cut.Find("igc-list-item").InnerHtml);
    }

    [Fact]
    public void ListHeader_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbListHeader>(p =>
            p.AddChildContent("Section A"));

        Assert.Contains("Section A", cut.Find("igc-list-header").InnerHtml);
    }

    // Tabs extended
    [Fact]
    public void Tab_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTab>(p =>
            p.AddChildContent("Tab Content"));

        Assert.Contains("Tab Content", cut.Find("igc-tab").InnerHtml);
    }

    [Fact]
    public void Tabs_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTabs>(p =>
            p.AddChildContent("<igc-tab>First</igc-tab>"));

        Assert.Contains("First", cut.Find("igc-tabs").InnerHtml);
    }

    // Card extended
    [Fact]
    public void Card_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCard>(p =>
            p.AddChildContent("<igc-card-header></igc-card-header>"));

        Assert.Contains("igc-card-header", cut.Find("igc-card").InnerHtml);
    }

    [Fact]
    public void CardHeader_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCardHeader>(p =>
            p.AddChildContent("<h3>Title</h3>"));

        Assert.Contains("Title", cut.Find("igc-card-header").InnerHtml);
    }

    [Fact]
    public void CardContent_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCardContent>(p =>
            p.AddChildContent("<p>Body text</p>"));

        Assert.Contains("Body text", cut.Find("igc-card-content").InnerHtml);
    }

    [Fact]
    public void CardActions_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCardActions>(p =>
            p.AddChildContent("<button>Action</button>"));

        Assert.Contains("Action", cut.Find("igc-card-actions").InnerHtml);
    }

    [Fact]
    public void CardMedia_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCardMedia>(p =>
            p.AddChildContent("<img src=\"test.png\" />"));

        Assert.Contains("img", cut.Find("igc-card-media").InnerHtml);
    }
}
