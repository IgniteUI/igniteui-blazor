using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CardTests : BlazorComponentTestBase
{
    [Fact]
    public void Card_RendersCorrectElement()
    {
        var cut = Render<IgbCard>();
        Assert.NotNull(cut.Find("igc-card"));
    }

    [Fact]
    public void Card_TypeMetadata_IsCorrect()
    {
        var card = new IgbCard();
        Assert.Equal("WebCard", card.Type);
    }

    [Fact]
    public void Card_Elevated_RendersAttribute()
    {
        var cut = Render<IgbCard>(parameters =>
            parameters.Add(p => p.Elevated, true));

        var element = cut.Find("igc-card");
        Assert.NotNull(element.GetAttribute("elevated"));
    }

    [Fact]
    public void Card_ChildContent_Renders()
    {
        var cut = Render<IgbCard>(parameters =>
            parameters.AddChildContent("<p>Card content</p>"));

        Assert.Contains("Card content", cut.Markup);
    }

    [Fact]
    public void Card_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbCard).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class CardHeaderTests : BlazorComponentTestBase
{
    [Fact]
    public void CardHeader_RendersCorrectElement()
    {
        var cut = Render<IgbCardHeader>();
        Assert.NotNull(cut.Find("igc-card-header"));
    }

    [Fact]
    public void CardHeader_TypeMetadata_IsCorrect()
    {
        var header = new IgbCardHeader();
        Assert.Equal("WebCardHeader", header.Type);
    }

    [Fact]
    public void CardHeader_ChildContent_Renders()
    {
        var cut = Render<IgbCardHeader>(parameters =>
            parameters.AddChildContent("<h3>Title</h3>"));

        Assert.Contains("Title", cut.Find("igc-card-header").InnerHtml);
    }
}

public class CardContentTests : BlazorComponentTestBase
{
    [Fact]
    public void CardContent_RendersCorrectElement()
    {
        var cut = Render<IgbCardContent>();
        Assert.NotNull(cut.Find("igc-card-content"));
    }

    [Fact]
    public void CardContent_TypeMetadata_IsCorrect()
    {
        var content = new IgbCardContent();
        Assert.Equal("WebCardContent", content.Type);
    }

    [Fact]
    public void CardContent_ChildContent_Renders()
    {
        var cut = Render<IgbCardContent>(parameters =>
            parameters.AddChildContent("<p>Body text</p>"));

        Assert.Contains("Body text", cut.Find("igc-card-content").InnerHtml);
    }
}

public class CardActionsTests : BlazorComponentTestBase
{
    [Fact]
    public void CardActions_RendersCorrectElement()
    {
        var cut = Render<IgbCardActions>();
        Assert.NotNull(cut.Find("igc-card-actions"));
    }

    [Fact]
    public void CardActions_TypeMetadata_IsCorrect()
    {
        var actions = new IgbCardActions();
        Assert.Equal("WebCardActions", actions.Type);
    }

    [Fact]
    public void CardActions_ChildContent_Renders()
    {
        var cut = Render<IgbCardActions>(parameters =>
            parameters.AddChildContent("<button>Action</button>"));

        Assert.Contains("Action", cut.Find("igc-card-actions").InnerHtml);
    }
}

public class CardMediaTests : BlazorComponentTestBase
{
    [Fact]
    public void CardMedia_RendersCorrectElement()
    {
        var cut = Render<IgbCardMedia>();
        Assert.NotNull(cut.Find("igc-card-media"));
    }

    [Fact]
    public void CardMedia_TypeMetadata_IsCorrect()
    {
        var media = new IgbCardMedia();
        Assert.Equal("WebCardMedia", media.Type);
    }

    [Fact]
    public void CardMedia_ChildContent_Renders()
    {
        var cut = Render<IgbCardMedia>(parameters =>
            parameters.AddChildContent("<img src=\"test.png\" />"));

        Assert.Contains("img", cut.Find("igc-card-media").InnerHtml);
    }
}
